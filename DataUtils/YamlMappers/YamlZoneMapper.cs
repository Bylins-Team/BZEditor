using System.Collections.Generic;
using DataUtils.YamlModels;

namespace DataUtils.YamlMappers
{
    /// <summary>
    /// Mapper for Zone - matches reference format
    /// </summary>
    public static class YamlZoneMapper
    {
        public static YamlZone ToYaml(Zone zone)
        {
            if (zone == null) return null;

            var yaml = new YamlZone
            {
                VNum = zone.Number,
                Name = zone.Name ?? "",
                FirstRoom = zone.Number * 100, // Default based on zone number
                TopRoom = zone.LastRoomNumber,
                Mode = zone.Level,
                ZoneType = zone.Type,
                ZoneGroup = zone.OptimalCharsInGroup,
                Entrance = zone.Number * 100, // Default entrance
                Lifespan = zone.RepopTimer,
                ResetMode = zone.RepopType,
                ResetIdle = zone.ResetIdle,
                UnderConstruction = zone.Test ? 1 : 0
            };

            // Metadata
            if (!string.IsNullOrEmpty(zone.Comment) ||
                !string.IsNullOrEmpty(zone.Location) ||
                !string.IsNullOrEmpty(zone.Author) ||
                !string.IsNullOrEmpty(zone.Description))
            {
                yaml.Metadata = new YamlZoneMetadata
                {
                    Comment = zone.Comment,
                    Location = zone.Location,
                    Author = zone.Author,
                    Description = zone.Description
                };
            }

            // Type A zones
            if (zone.ResetA.Count > 0)
            {
                yaml.TypeAZones = new List<int>();
                foreach (var z in zone.ResetA)
                {
                    if (z is int zn)
                        yaml.TypeAZones.Add(zn);
                }
            }

            // Type B zones
            if (zone.ResetB.Count > 0)
            {
                yaml.TypeBZones = new List<int>();
                foreach (var z in zone.ResetB)
                {
                    if (z is int zn)
                        yaml.TypeBZones.Add(zn);
                }
            }

            // Note: Zone commands (mob/obj loading) are now stored in zone.yaml
            // This requires additional implementation to collect commands from rooms
            // For now, we don't export commands as they're stored per-room in BZEditor

            return yaml;
        }

        public static void FromYaml(YamlZone yaml, Zone target)
        {
            if (yaml == null || target == null) return;

            target.Number = yaml.VNum;
            target.Name = yaml.Name ?? "";
            target.LastRoomNumber = yaml.TopRoom;
            target.Level = yaml.Mode;
            target.Type = yaml.ZoneType;
            target.OptimalCharsInGroup = yaml.ZoneGroup;
            target.RepopTimer = yaml.Lifespan;
            target.RepopType = yaml.ResetMode;
            target.ResetIdle = yaml.ResetIdle;
            target.Test = yaml.UnderConstruction != 0;

            // Metadata
            if (yaml.Metadata != null)
            {
                target.Comment = yaml.Metadata.Comment ?? "";
                target.Location = yaml.Metadata.Location ?? "";
                target.Author = yaml.Metadata.Author ?? "";
                target.Description = yaml.Metadata.Description ?? "";
            }

            // Type A zones
            target.ResetA.Clear();
            if (yaml.TypeAZones != null)
            {
                foreach (var z in yaml.TypeAZones)
                    target.ResetA.Add(z);
            }

            // Type B zones
            target.ResetB.Clear();
            if (yaml.TypeBZones != null)
            {
                foreach (var z in yaml.TypeBZones)
                    target.ResetB.Add(z);
            }

            // Note: Zone commands need to be distributed to rooms
            // This requires additional implementation
        }
    }
}
