using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using DataUtils.YamlMappers;
using DataUtils.YamlModels;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace DataUtils
{
    /// <summary>
    /// Format provider for YAML data format
    /// </summary>
    public class YamlFormatProvider : BaseFormatProvider
    {
        private readonly ISerializer serializer;
        private readonly IDeserializer deserializer;

        public override string FormatName => "yaml";
        public override string FormatDescription => "YAML Format (Human-Readable)";
        public override Encoding DefaultEncoding => Encoding.GetEncoding("koi8-r");

        public YamlFormatProvider()
        {
            // Engine (bylins/mud yaml_world_data_source.cpp) reads snake_case keys.
            // Do NOT omit default scalars: the engine applies its own (sometimes non-zero)
            // defaults for missing keys, and omitting a zero would corrupt such fields on
            // load. Only null (unset enum names / optional speed) and empty collections are
            // omitted. This also makes the editor's own YAML round-trip idempotent.
            serializer = new SerializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .WithTypeConverter(new NamedIntMapConverter())
                .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull
                    | DefaultValuesHandling.OmitEmptyCollections)
                .Build();

            deserializer = new DeserializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .WithTypeConverter(new NamedIntMapConverter())
                .IgnoreUnmatchedProperties()
                .Build();
        }

        private string GetZoneDir(string zoneNumber)
        {
            return Path.Combine(StaticData.WorldFolderPath, "zones", zoneNumber);
        }

        /// <summary>Entity rel-number the engine keys flat entries by: vnum % 100.</summary>
        private static int RelNum(int vnum)
        {
            return ((vnum % 100) + 100) % 100;
        }

        /// <summary>Zone number as int; entity vnum = zone * 100 + rel.</summary>
        private static int ParseZone(string zoneNumber)
        {
            int z;
            int.TryParse(zoneNumber, out z);
            return z;
        }

        private static bool IsIndexFile(string path)
        {
            return string.Equals(Path.GetFileName(path), "index.yaml", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Removes the top-level "vnum:" line from a serialized entity body. In the flat
        /// layout the vnum is the map key, not a body field (matches the engine emitter,
        /// which drops the internal vnum). Only a column-0 "vnum:" is removed; indented
        /// occurrences (e.g. inside a literal-block description) are left untouched.
        /// </summary>
        private static string StripVnumLine(string body)
        {
            var sb = new StringBuilder();
            foreach (string line in body.Replace("\r\n", "\n").Split('\n'))
            {
                if (line.StartsWith("vnum:", StringComparison.Ordinal)) continue;
                sb.Append(line).Append('\n');
            }
            return sb.ToString();
        }

        /// <summary>
        /// Nests one entity body under a bare-int rel key, indenting every non-empty line
        /// two spaces. Uniform indentation keeps literal-block indicators valid (they are
        /// relative to the parent key) -- this mirrors the engine/converter flat emitter.
        /// </summary>
        private static string NestUnderRel(int rel, string body)
        {
            var sb = new StringBuilder();
            sb.Append(rel).Append(":\n");
            foreach (string line in body.TrimEnd('\n').Replace("\r\n", "\n").Split('\n'))
            {
                if (line.Length == 0) sb.Append('\n');
                else sb.Append("  ").Append(line).Append('\n');
            }
            return sb.ToString();
        }

        /// <summary>
        /// Writes the flat zone file zones/&lt;z&gt;/&lt;sub&gt;.yaml as a "rel-number -> body"
        /// map (its own index -- no per-dir index.yaml). Self-cleaning: removes the old
        /// per-file &lt;sub&gt;/ directory, and deletes the flat file if there are no entries.
        /// </summary>
        private void WriteFlatZoneFile(string zoneNumber, string sub, string label,
            List<KeyValuePair<int, string>> entries)
        {
            string zoneDir = GetZoneDir(zoneNumber);
            Directory.CreateDirectory(zoneDir);
            string flatPath = Path.Combine(zoneDir, sub + ".yaml");
            string perFileDir = Path.Combine(zoneDir, sub);

            if (entries.Count == 0)
            {
                if (File.Exists(flatPath)) File.Delete(flatPath);
            }
            else
            {
                entries.Sort((a, b) => a.Key.CompareTo(b.Key));
                var sb = new StringBuilder();
                sb.Append("# ").Append(label).Append(" for zone ").Append(zoneNumber).Append('\n');
                foreach (var e in entries)
                {
                    sb.Append('\n');
                    sb.Append(NestUnderRel(e.Key, e.Value));
                }
                File.WriteAllText(flatPath, sb.ToString(), DefaultEncoding);
            }

            // A zone must not keep both layouts; the engine warns and ignores one otherwise.
            if (Directory.Exists(perFileDir))
            {
                try { Directory.Delete(perFileDir, true); } catch { }
            }
        }

        /// <summary>
        /// Ensures zones/index.yaml exists and contains the given zone vnum.
        /// </summary>
        private void EnsureZoneInGlobalIndex(int zoneVnum)
        {
            string zonesDir = Path.Combine(StaticData.WorldFolderPath, "zones");
            Directory.CreateDirectory(zonesDir);
            string indexPath = Path.Combine(zonesDir, "index.yaml");

            var zones = new List<int>();
            if (File.Exists(indexPath))
            {
                try
                {
                    var existing = deserializer.Deserialize<Dictionary<string, List<int>>>(
                        File.ReadAllText(indexPath, DefaultEncoding));
                    if (existing != null && existing.TryGetValue("zones", out var list) && list != null)
                        zones = list;
                }
                catch { /* malformed index is rebuilt below */ }
            }

            if (!zones.Contains(zoneVnum)) zones.Add(zoneVnum);
            zones.Sort();
            var index = new Dictionary<string, List<int>> { { "zones", zones } };
            File.WriteAllText(indexPath, serializer.Serialize(index), DefaultEncoding);
        }

        /// <summary>
        /// world_config.yaml is required by the engine; create a default if missing.
        /// </summary>
        private void EnsureWorldConfig()
        {
            string configPath = Path.Combine(StaticData.WorldFolderPath, "world_config.yaml");
            Directory.CreateDirectory(StaticData.WorldFolderPath);

            var config = new Dictionary<string, string>();
            if (File.Exists(configPath))
            {
                try
                {
                    var existing = deserializer.Deserialize<Dictionary<string, string>>(
                        File.ReadAllText(configPath, DefaultEncoding));
                    if (existing != null) config = existing;
                }
                catch { /* malformed config is rebuilt below */ }
            }

            if (!config.ContainsKey("line_endings")) config["line_endings"] = "unix";
            config["layout"] = "flat"; // the editor always writes the flat layout
            File.WriteAllText(configPath, serializer.Serialize(config), DefaultEncoding);
        }

        /// <summary>
        /// Builds the engine `commands` list (and typeA/typeB zone links) by reusing the
        /// existing legacy zone serializer: it writes a throwaway .zon, then reads the
        /// reset-command lines back. The engine accepts these letter-keyword strings
        /// verbatim; the trailing "(comment)" the editor appends is stripped.
        /// </summary>
        private void PopulateZoneCommands(YamlZone yamlZone, Zone zone, ObjsCollection objects,
            MobsCollection mobs, RoomsCollection rooms)
        {
            string tmp = Path.Combine(Path.GetTempPath(), "bzed_zon_" + zone.Number);
            string savedWorld = StaticData.WorldFolderPath;
            string[] lines;
            try
            {
                if (StaticData.CurrentEncoding == null) StaticData.CurrentEncoding = DefaultEncoding;
                if (Directory.Exists(tmp)) Directory.Delete(tmp, true);
                Directory.CreateDirectory(Path.Combine(tmp, "ZON"));
                StaticData.WorldFolderPath = tmp;
                new ZoneFileManager().Save(zone, objects, mobs, rooms);
            }
            catch (Exception ex)
            {
                // Reset commands are best-effort: never let a failure here abort the YAML save.
                FireExceptionEvent("Zone reset commands could not be generated for zone " + zone.Number
                    + " (the zone is saved without them).", ex, EventLogEntryType.Warning);
                return;
            }
            finally
            {
                StaticData.WorldFolderPath = savedWorld;
            }

            string zonPath = Path.Combine(Path.Combine(tmp, "ZON"), zone.Number + ".zon");
            if (!File.Exists(zonPath)) return;
            lines = File.ReadAllLines(zonPath, DefaultEncoding);
            try { Directory.Delete(tmp, true); } catch { }

            const string CommandLetters = "MOGEPDRTVQF";
            foreach (string raw in lines)
            {
                // A command line is "<LETTER> <digit|->..."; this excludes headers,
                // the zone name and the numeric repop line.
                if (raw.Length < 3 || raw[1] != ' ') continue;
                char c = raw[0];
                if (c < 'A' || c > 'Z') continue;
                char d = raw[2];
                if (d != '-' && (d < '0' || d > '9')) continue;

                // A/B (typeA/typeB zone links) are handled by the zone mapper, not here.
                // 'L' (mob death loot) is excluded: it lives in the mob's dead_load.
                if (CommandLetters.IndexOf(c) < 0) continue;

                string line = raw;
                int tab = line.IndexOf('\t');
                if (tab >= 0) line = line.Substring(0, tab);
                yamlZone.Commands.Add(line.TrimEnd());
            }
        }

        /// <summary>
        /// Reverse of PopulateZoneCommands: replays the command strings into the room spawn
        /// model by reusing the legacy parser. A throwaway .zon (minimal header + the commands)
        /// is written and loaded; spawns land on the real rooms, while a throwaway Zone absorbs
        /// the header. typeA/typeB links are copied onto the real zone afterwards.
        /// </summary>
        private void LoadZoneCommands(YamlZone yamlZone, Zone zone, MobsCollection mobs,
            RoomsCollection rooms, Encoding encoding)
        {
            // typeA/typeB zone links are restored by the zone mapper; here we only replay
            // the reset commands (including 'Q'/EXTRACT, which is not modelled by the mapper).
            if (yamlZone.Commands == null || yamlZone.Commands.Count == 0) return;

            string num = zone.Number.ToString();
            string tmp = Path.Combine(Path.GetTempPath(), "bzed_zonload_" + num);
            string savedWorld = StaticData.WorldFolderPath;
            try
            {
                if (Directory.Exists(tmp)) Directory.Delete(tmp, true);
                Directory.CreateDirectory(Path.Combine(tmp, "ZON"));

                var sb = new StringBuilder();
                sb.Append("#").Append(num).Append("\n");
                sb.Append("z~\n");
                sb.Append("#0 0 1\n");
                sb.Append(zone.Number * 100 + 99).Append(" ")
                  .Append(yamlZone.Lifespan).Append(" ")
                  .Append(yamlZone.ResetMode).Append(" ")
                  .Append(yamlZone.ResetIdle).Append("\n");
                foreach (string c in yamlZone.Commands) sb.Append(c).Append("\n");
                sb.Append("S\n$\n");

                File.WriteAllText(Path.Combine(Path.Combine(tmp, "ZON"), num + ".zon"), sb.ToString(), encoding);

                StaticData.WorldFolderPath = tmp;
                var throwaway = new Zone();
                new ZoneFileManager().Load(throwaway, mobs, rooms, num, encoding);

                // 'Q' (EXTRACT) commands are zone-level and not handled by the mapper.
                foreach (OperatedMob m in throwaway.MobsToRemove)
                    zone.MobsToRemove.Add(m.VNum, m.ConditionFlag, -1);
            }
            finally
            {
                StaticData.WorldFolderPath = savedWorld;
                try { Directory.Delete(tmp, true); } catch { }
            }
        }

        #region Load Operations

        public override bool LoadZone(Zone zone, MobsCollection mobs, RoomsCollection rooms, string zoneNumber, Encoding encoding)
        {
            try
            {
                string zonePath = Path.Combine(GetZoneDir(zoneNumber), "zone.yaml");
                if (!File.Exists(zonePath))
                {
                    FireExceptionEvent($"Zone file not found: {zonePath}", null, EventLogEntryType.Warning);
                    return false;
                }

                string yamlContent = File.ReadAllText(zonePath, encoding ?? DefaultEncoding);
                var yamlZone = deserializer.Deserialize<YamlZone>(yamlContent);
                YamlZoneMapper.FromYaml(yamlZone, zone);
                LoadZoneCommands(yamlZone, zone, mobs, rooms, encoding ?? DefaultEncoding);
                return true;
            }
            catch (Exception ex)
            {
                FireExceptionEvent($"Error loading YAML zone {zoneNumber}", ex, EventLogEntryType.Error);
                return false;
            }
        }

        public override bool LoadRooms(RoomsCollection rooms, string zoneNumber, Encoding encoding)
        {
            try
            {
                var enc = encoding ?? DefaultEncoding;

                // Flat layout (default): zones/<z>/rooms.yaml is a rel -> body map.
                string flatPath = Path.Combine(GetZoneDir(zoneNumber), "rooms.yaml");
                if (File.Exists(flatPath))
                {
                    int zone = ParseZone(zoneNumber);
                    var map = deserializer.Deserialize<Dictionary<int, YamlRoom>>(File.ReadAllText(flatPath, enc));
                    if (map != null)
                        foreach (var kv in map)
                        {
                            var yamlRoom = kv.Value;
                            if (yamlRoom == null) continue;
                            yamlRoom.VNum = zone * 100 + kv.Key;
                            var room = YamlRoomMapper.FromYaml(yamlRoom);
                            if (room != null) rooms.Add(room);
                        }
                    return true;
                }

                // Per-file layout (legacy): zones/<z>/rooms/NN.yaml + index.yaml.
                string roomsDir = Path.Combine(GetZoneDir(zoneNumber), "rooms");
                if (!Directory.Exists(roomsDir))
                {
                    // No rooms is not an error - zone may have no rooms yet
                    return true;
                }

                foreach (var file in Directory.GetFiles(roomsDir, "*.yaml"))
                {
                    if (IsIndexFile(file)) continue;
                    var yamlRoom = deserializer.Deserialize<YamlRoom>(File.ReadAllText(file, enc));
                    var room = YamlRoomMapper.FromYaml(yamlRoom);
                    if (room != null)
                        rooms.Add(room);
                }
                return true;
            }
            catch (Exception ex)
            {
                FireExceptionEvent($"Error loading YAML rooms for zone {zoneNumber}", ex, EventLogEntryType.Error);
                return false;
            }
        }

        public override bool LoadMobs(MobsCollection mobs, string zoneNumber, Encoding encoding)
        {
            try
            {
                var enc = encoding ?? DefaultEncoding;

                string flatPath = Path.Combine(GetZoneDir(zoneNumber), "mobs.yaml");
                if (File.Exists(flatPath))
                {
                    int zone = ParseZone(zoneNumber);
                    var map = deserializer.Deserialize<Dictionary<int, YamlMob>>(File.ReadAllText(flatPath, enc));
                    if (map != null)
                        foreach (var kv in map)
                        {
                            var yamlMob = kv.Value;
                            if (yamlMob == null) continue;
                            yamlMob.VNum = zone * 100 + kv.Key;
                            var mob = YamlMobMapper.FromYaml(yamlMob);
                            if (mob != null) mobs.Add(mob);
                        }
                    return true;
                }

                string mobsDir = Path.Combine(GetZoneDir(zoneNumber), "mobs");
                if (!Directory.Exists(mobsDir))
                {
                    return true;
                }

                foreach (var file in Directory.GetFiles(mobsDir, "*.yaml"))
                {
                    if (IsIndexFile(file)) continue;
                    var yamlMob = deserializer.Deserialize<YamlMob>(File.ReadAllText(file, enc));
                    var mob = YamlMobMapper.FromYaml(yamlMob);
                    if (mob != null)
                        mobs.Add(mob);
                }
                return true;
            }
            catch (Exception ex)
            {
                FireExceptionEvent($"Error loading YAML mobs for zone {zoneNumber}", ex, EventLogEntryType.Error);
                return false;
            }
        }

        public override bool LoadObjects(ObjsCollection objects, string zoneNumber, Encoding encoding)
        {
            try
            {
                var enc = encoding ?? DefaultEncoding;

                string flatPath = Path.Combine(GetZoneDir(zoneNumber), "objects.yaml");
                if (File.Exists(flatPath))
                {
                    int zone = ParseZone(zoneNumber);
                    var map = deserializer.Deserialize<Dictionary<int, YamlObj>>(File.ReadAllText(flatPath, enc));
                    if (map != null)
                        foreach (var kv in map)
                        {
                            var yamlObj = kv.Value;
                            if (yamlObj == null) continue;
                            yamlObj.VNum = zone * 100 + kv.Key;
                            var obj = YamlObjMapper.FromYaml(yamlObj);
                            if (obj != null) objects.Add(obj);
                        }
                    return true;
                }

                string objsDir = Path.Combine(GetZoneDir(zoneNumber), "objects");
                if (!Directory.Exists(objsDir))
                {
                    return true;
                }

                foreach (var file in Directory.GetFiles(objsDir, "*.yaml"))
                {
                    if (IsIndexFile(file)) continue;
                    var yamlObj = deserializer.Deserialize<YamlObj>(File.ReadAllText(file, enc));
                    var obj = YamlObjMapper.FromYaml(yamlObj);
                    if (obj != null)
                        objects.Add(obj);
                }
                return true;
            }
            catch (Exception ex)
            {
                FireExceptionEvent($"Error loading YAML objects for zone {zoneNumber}", ex, EventLogEntryType.Error);
                return false;
            }
        }

        public override bool LoadTriggers(TriggersCollection triggers, string zoneNumber, Encoding encoding)
        {
            try
            {
                var enc = encoding ?? DefaultEncoding;

                string flatPath = Path.Combine(GetZoneDir(zoneNumber), "triggers.yaml");
                if (File.Exists(flatPath))
                {
                    int zone = ParseZone(zoneNumber);
                    var map = deserializer.Deserialize<Dictionary<int, YamlTrigger>>(File.ReadAllText(flatPath, enc));
                    if (map != null)
                        foreach (var kv in map)
                        {
                            var yamlTrigger = kv.Value;
                            if (yamlTrigger == null) continue;
                            yamlTrigger.VNum = zone * 100 + kv.Key;
                            var trigger = YamlTriggerMapper.FromYaml(yamlTrigger);
                            if (trigger != null) triggers.Add(trigger);
                        }
                    return true;
                }

                string trigsDir = Path.Combine(GetZoneDir(zoneNumber), "triggers");
                if (!Directory.Exists(trigsDir))
                {
                    return true;
                }

                foreach (var file in Directory.GetFiles(trigsDir, "*.yaml"))
                {
                    if (IsIndexFile(file)) continue;
                    var yamlTrigger = deserializer.Deserialize<YamlTrigger>(File.ReadAllText(file, enc));
                    var trigger = YamlTriggerMapper.FromYaml(yamlTrigger);
                    if (trigger != null)
                        triggers.Add(trigger);
                }
                return true;
            }
            catch (Exception ex)
            {
                FireExceptionEvent($"Error loading YAML triggers for zone {zoneNumber}", ex, EventLogEntryType.Error);
                return false;
            }
        }

        public override bool LoadSketches(SketchRoomsCollection sketches, string zoneNumber, Encoding encoding)
        {
            // Sketches are stored in the room files, not separately
            // They are loaded as part of LoadRooms
            return true;
        }

        #endregion

        #region Save Operations

        public override void SaveZone(Zone zone, ObjsCollection objects, MobsCollection mobs, RoomsCollection rooms)
        {
            try
            {
                string zoneDir = GetZoneDir(zone.Number.ToString());
                Directory.CreateDirectory(zoneDir);

                var yamlZone = YamlZoneMapper.ToYaml(zone);
                PopulateZoneCommands(yamlZone, zone, objects, mobs, rooms);
                string yaml = serializer.Serialize(yamlZone);
                string zonePath = Path.Combine(zoneDir, "zone.yaml");
                File.WriteAllText(zonePath, yaml, DefaultEncoding);

                EnsureZoneInGlobalIndex(zone.Number);
                EnsureWorldConfig();
            }
            catch (Exception ex)
            {
                FireExceptionEvent($"Error saving YAML zone {zone.Number}", ex, EventLogEntryType.Error);
            }
        }

        public override void SaveRooms(RoomsCollection rooms, string zoneNumber)
        {
            try
            {
                var entries = new List<KeyValuePair<int, string>>();
                foreach (Room room in rooms)
                {
                    var yamlRoom = YamlRoomMapper.ToYaml(room);
                    string body = StripVnumLine(serializer.Serialize(yamlRoom));
                    entries.Add(new KeyValuePair<int, string>(RelNum(room.VNum), body));
                }
                WriteFlatZoneFile(zoneNumber, "rooms", "Rooms", entries);
            }
            catch (Exception ex)
            {
                FireExceptionEvent($"Error saving YAML rooms for zone {zoneNumber}", ex, EventLogEntryType.Error);
            }
        }

        public override void SaveMobs(MobsCollection mobs, string zoneNumber)
        {
            try
            {
                var entries = new List<KeyValuePair<int, string>>();
                foreach (Mob mob in mobs)
                {
                    var yamlMob = YamlMobMapper.ToYaml(mob);
                    string body = StripVnumLine(serializer.Serialize(yamlMob));
                    entries.Add(new KeyValuePair<int, string>(RelNum(mob.VNum), body));
                }
                WriteFlatZoneFile(zoneNumber, "mobs", "Mobs", entries);
            }
            catch (Exception ex)
            {
                FireExceptionEvent($"Error saving YAML mobs for zone {zoneNumber}", ex, EventLogEntryType.Error);
            }
        }

        public override void SaveObjects(ObjsCollection objects, string zoneNumber)
        {
            try
            {
                var entries = new List<KeyValuePair<int, string>>();
                foreach (Obj obj in objects)
                {
                    var yamlObj = YamlObjMapper.ToYaml(obj);
                    string body = StripVnumLine(serializer.Serialize(yamlObj));
                    entries.Add(new KeyValuePair<int, string>(RelNum(obj.VNum), body));
                }
                WriteFlatZoneFile(zoneNumber, "objects", "Objects", entries);
            }
            catch (Exception ex)
            {
                FireExceptionEvent($"Error saving YAML objects for zone {zoneNumber}", ex, EventLogEntryType.Error);
            }
        }

        public override void SaveTriggers(TriggersCollection triggers, string zoneNumber)
        {
            try
            {
                var entries = new List<KeyValuePair<int, string>>();
                foreach (Trigger trigger in triggers)
                {
                    var yamlTrigger = YamlTriggerMapper.ToYaml(trigger);
                    string body = StripVnumLine(serializer.Serialize(yamlTrigger));
                    entries.Add(new KeyValuePair<int, string>(RelNum(trigger.VNum), body));
                }
                WriteFlatZoneFile(zoneNumber, "triggers", "Triggers", entries);
            }
            catch (Exception ex)
            {
                FireExceptionEvent($"Error saving YAML triggers for zone {zoneNumber}", ex, EventLogEntryType.Error);
            }
        }

        public override void SaveSketches(SketchRoomsCollection sketches, string zoneNumber)
        {
            // Sketches are stored in the room files, not separately
            // They are saved as part of SaveRooms
        }

        #endregion

        public override bool CanLoadZone(string zoneNumber)
        {
            string zonePath = Path.Combine(GetZoneDir(zoneNumber), "zone.yaml");
            return File.Exists(zonePath);
        }
    }
}
