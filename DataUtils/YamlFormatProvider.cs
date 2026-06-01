using System;
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
            serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull | DefaultValuesHandling.OmitDefaults)
                .Build();

            deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .IgnoreUnmatchedProperties()
                .Build();
        }

        private string GetZoneDir(string zoneNumber)
        {
            return Path.Combine(StaticData.WorldFolderPath, "zones", zoneNumber);
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

                foreach (Room room in rooms)
                {
                    var yamlRoom = YamlRoomMapper.ToYaml(room);
                    string yaml = serializer.Serialize(yamlRoom);
                    string filePath = Path.Combine(roomsDir, $"{room.VNum}.yaml");
                    File.WriteAllText(filePath, yaml, DefaultEncoding);
                }
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

                foreach (Mob mob in mobs)
                {
                    var yamlMob = YamlMobMapper.ToYaml(mob);
                    string yaml = serializer.Serialize(yamlMob);
                    string filePath = Path.Combine(mobsDir, $"{mob.VNum}.yaml");
                    File.WriteAllText(filePath, yaml, DefaultEncoding);
                }
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

                foreach (Obj obj in objects)
                {
                    var yamlObj = YamlObjMapper.ToYaml(obj);
                    string yaml = serializer.Serialize(yamlObj);
                    string filePath = Path.Combine(objsDir, $"{obj.VNum}.yaml");
                    File.WriteAllText(filePath, yaml, DefaultEncoding);
                }
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

                foreach (Trigger trigger in triggers)
                {
                    var yamlTrigger = YamlTriggerMapper.ToYaml(trigger);
                    string yaml = serializer.Serialize(yamlTrigger);
                    string filePath = Path.Combine(trigsDir, $"{trigger.VNum}.yaml");
                    File.WriteAllText(filePath, yaml, DefaultEncoding);
                }
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
