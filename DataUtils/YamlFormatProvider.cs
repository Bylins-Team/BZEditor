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
            serializer = new SerializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull | DefaultValuesHandling.OmitDefaults)
                .Build();

            deserializer = new DeserializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .IgnoreUnmatchedProperties()
                .Build();
        }

        private string GetZoneDir(string zoneNumber)
        {
            return Path.Combine(StaticData.WorldFolderPath, "zones", zoneNumber);
        }

        /// <summary>
        /// Entity file name as the engine expects it: rel = vnum % 100, zero-padded to 2 digits.
        /// </summary>
        private static string RelFileName(int vnum)
        {
            return (((vnum % 100) + 100) % 100).ToString("00") + ".yaml";
        }

        private static bool IsIndexFile(string path)
        {
            return string.Equals(Path.GetFileName(path), "index.yaml", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Writes a per-directory index.yaml: { &lt;key&gt;: [rel, rel, ...] } sorted ascending.
        /// </summary>
        private void WriteEntityIndex(string dir, string key, List<int> vnums)
        {
            var rels = new List<int>();
            foreach (int v in vnums)
            {
                int rel = ((v % 100) + 100) % 100;
                if (!rels.Contains(rel)) rels.Add(rel);
            }
            rels.Sort();
            var index = new Dictionary<string, List<int>> { { key, rels } };
            File.WriteAllText(Path.Combine(dir, "index.yaml"), serializer.Serialize(index), DefaultEncoding);
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
            if (File.Exists(configPath)) return;
            Directory.CreateDirectory(StaticData.WorldFolderPath);
            var config = new Dictionary<string, string> { { "line_endings", "unix" } };
            File.WriteAllText(configPath, serializer.Serialize(config), DefaultEncoding);
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
                string roomsDir = Path.Combine(GetZoneDir(zoneNumber), "rooms");
                if (!Directory.Exists(roomsDir))
                {
                    // No rooms directory is not an error - zone may have no rooms yet
                    return true;
                }

                foreach (var file in Directory.GetFiles(roomsDir, "*.yaml"))
                {
                    if (IsIndexFile(file)) continue;
                    string yamlContent = File.ReadAllText(file, encoding ?? DefaultEncoding);
                    var yamlRoom = deserializer.Deserialize<YamlRoom>(yamlContent);
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
                string mobsDir = Path.Combine(GetZoneDir(zoneNumber), "mobs");
                if (!Directory.Exists(mobsDir))
                {
                    return true;
                }

                foreach (var file in Directory.GetFiles(mobsDir, "*.yaml"))
                {
                    if (IsIndexFile(file)) continue;
                    string yamlContent = File.ReadAllText(file, encoding ?? DefaultEncoding);
                    var yamlMob = deserializer.Deserialize<YamlMob>(yamlContent);
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
                string objsDir = Path.Combine(GetZoneDir(zoneNumber), "objects");
                if (!Directory.Exists(objsDir))
                {
                    return true;
                }

                foreach (var file in Directory.GetFiles(objsDir, "*.yaml"))
                {
                    if (IsIndexFile(file)) continue;
                    string yamlContent = File.ReadAllText(file, encoding ?? DefaultEncoding);
                    var yamlObj = deserializer.Deserialize<YamlObj>(yamlContent);
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
                string trigsDir = Path.Combine(GetZoneDir(zoneNumber), "triggers");
                if (!Directory.Exists(trigsDir))
                {
                    return true;
                }

                foreach (var file in Directory.GetFiles(trigsDir, "*.yaml"))
                {
                    if (IsIndexFile(file)) continue;
                    string yamlContent = File.ReadAllText(file, encoding ?? DefaultEncoding);
                    var yamlTrigger = deserializer.Deserialize<YamlTrigger>(yamlContent);
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
                string roomsDir = Path.Combine(GetZoneDir(zoneNumber), "rooms");
                Directory.CreateDirectory(roomsDir);

                // Clean up old files
                foreach (var oldFile in Directory.GetFiles(roomsDir, "*.yaml"))
                {
                    File.Delete(oldFile);
                }

                var vnums = new List<int>();
                foreach (Room room in rooms)
                {
                    var yamlRoom = YamlRoomMapper.ToYaml(room);
                    string yaml = serializer.Serialize(yamlRoom);
                    File.WriteAllText(Path.Combine(roomsDir, RelFileName(room.VNum)), yaml, DefaultEncoding);
                    vnums.Add(room.VNum);
                }
                WriteEntityIndex(roomsDir, "rooms", vnums);
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
                string mobsDir = Path.Combine(GetZoneDir(zoneNumber), "mobs");
                Directory.CreateDirectory(mobsDir);

                // Clean up old files
                foreach (var oldFile in Directory.GetFiles(mobsDir, "*.yaml"))
                {
                    File.Delete(oldFile);
                }

                var vnums = new List<int>();
                foreach (Mob mob in mobs)
                {
                    var yamlMob = YamlMobMapper.ToYaml(mob);
                    string yaml = serializer.Serialize(yamlMob);
                    File.WriteAllText(Path.Combine(mobsDir, RelFileName(mob.VNum)), yaml, DefaultEncoding);
                    vnums.Add(mob.VNum);
                }
                WriteEntityIndex(mobsDir, "mobs", vnums);
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
                string objsDir = Path.Combine(GetZoneDir(zoneNumber), "objects");
                Directory.CreateDirectory(objsDir);

                // Clean up old files
                foreach (var oldFile in Directory.GetFiles(objsDir, "*.yaml"))
                {
                    File.Delete(oldFile);
                }

                var vnums = new List<int>();
                foreach (Obj obj in objects)
                {
                    var yamlObj = YamlObjMapper.ToYaml(obj);
                    string yaml = serializer.Serialize(yamlObj);
                    File.WriteAllText(Path.Combine(objsDir, RelFileName(obj.VNum)), yaml, DefaultEncoding);
                    vnums.Add(obj.VNum);
                }
                WriteEntityIndex(objsDir, "objects", vnums);
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
                string trigsDir = Path.Combine(GetZoneDir(zoneNumber), "triggers");
                Directory.CreateDirectory(trigsDir);

                // Clean up old files
                foreach (var oldFile in Directory.GetFiles(trigsDir, "*.yaml"))
                {
                    File.Delete(oldFile);
                }

                var vnums = new List<int>();
                foreach (Trigger trigger in triggers)
                {
                    var yamlTrigger = YamlTriggerMapper.ToYaml(trigger);
                    string yaml = serializer.Serialize(yamlTrigger);
                    File.WriteAllText(Path.Combine(trigsDir, RelFileName(trigger.VNum)), yaml, DefaultEncoding);
                    vnums.Add(trigger.VNum);
                }
                WriteEntityIndex(trigsDir, "triggers", vnums);
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
