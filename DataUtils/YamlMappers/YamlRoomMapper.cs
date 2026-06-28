using System;
using System.Collections.Generic;
using DataUtils.YamlModels;

namespace DataUtils.YamlMappers
{
    /// <summary>
    /// Mapper for Room - matches reference format
    /// </summary>
    public static class YamlRoomMapper
    {
        private static readonly string[] DirectionNames = { "north", "east", "south", "west", "up", "down" };

        public static YamlRoom ToYaml(Room room)
        {
            if (room == null) return null;

            var yaml = new YamlRoom
            {
                VNum = room.VNum,
                Zone = room.ZoneNum,
                Name = room.Name ?? "",
                Description = (room.Description.Main ?? "").TrimEnd('\r', '\n'),
                Sector = EngineCodec.EnumName(room.SectorType, EngineDictionaries.Sectors)
            };

            // Flags as engine symbolic names
            foreach (var name in EngineCodec.FlagsToNames(room.Flags, EngineDictionaries.RoomFlags))
                yaml.Flags.Add(name);

            // Exits as list with direction names
            AddExitToList(yaml.Exits, "north", room.ExitNorth);
            AddExitToList(yaml.Exits, "east", room.ExitEast);
            AddExitToList(yaml.Exits, "south", room.ExitSouth);
            AddExitToList(yaml.Exits, "west", room.ExitWest);
            AddExitToList(yaml.Exits, "up", room.ExitUp);
            AddExitToList(yaml.Exits, "down", room.ExitDown);

            // Extra descriptions
            foreach (ExtraDesc ed in room.ExtraDescriptions)
            {
                yaml.ExtraDescriptions.Add(new YamlExtraDesc
                {
                    Keywords = ed.Aliases ?? "",
                    Description = (ed.Description ?? "").TrimEnd('\r', '\n')
                });
            }

            // Triggers
            foreach (var trig in room.TriggersList)
            {
                if (trig is int t)
                    yaml.Triggers.Add(t);
            }

            // BZEditor-specific fields
            if (room.PlacedOnMap)
            {
                yaml.X = room.X;
                yaml.Y = room.Y;
                yaml.Z = room.Z;
                yaml.PlacedOnMap = true;
            }

            // Multi-variant room description (if any variants are set)
            if (!string.IsNullOrEmpty(room.Description.Day) ||
                !string.IsNullOrEmpty(room.Description.Night))
            {
                yaml.RoomDescriptions = YamlRoomDescriptionMapper.ToYaml(room.Description);
            }

            // Ingredients
            foreach (Ingredient ingr in room.Ingredients)
            {
                yaml.Ingredients.Add(new YamlIngredient
                {
                    TypeName = ingr.TypeName,
                    Power = ingr.Power,
                    Probability = ingr.Probability,
                    PowerAuto = ingr.PowerAuto
                });
            }

            return yaml;
        }

        private static void AddExitToList(List<YamlExit> exits, string direction, Exit exit)
        {
            // Only add exit if it has a valid destination
            if (exit == null || exit.RoomVNum < 0)
                return;

            exits.Add(new YamlExit
            {
                Direction = direction,
                Description = string.IsNullOrEmpty(exit.Description) ? null : exit.Description,
                Keywords = string.IsNullOrEmpty(exit.Aliases) ? null : exit.Aliases,
                ExitFlags = exit.ExitFlag,
                Key = exit.Key,
                ToRoom = exit.RoomVNum,
                LockComplexity = exit.LockLevel
            });
        }

        public static Room FromYaml(YamlRoom yaml)
        {
            if (yaml == null) return null;

            var room = new Room(yaml.VNum)
            {
                Name = yaml.Name ?? "",
                ZoneNum = yaml.Zone,
                SectorType = EngineCodec.EnumValue(yaml.Sector, EngineDictionaries.Sectors)
            };

            // Description
            room.Description.Main = yaml.Description ?? "";

            // Flags from engine names back to asciiflag string
            room.Flags = EngineCodec.NamesToFlags(yaml.Flags, EngineDictionaries.RoomFlags);

            // Exits from list
            if (yaml.Exits != null)
            {
                foreach (var exit in yaml.Exits)
                {
                    Exit targetExit = GetExitByDirection(room, exit.Direction);
                    if (targetExit != null)
                    {
                        targetExit.Description = exit.Description ?? "";
                        targetExit.Aliases = exit.Keywords ?? "";
                        targetExit.ExitFlag = exit.ExitFlags;
                        targetExit.Key = exit.Key;
                        targetExit.RoomVNum = exit.ToRoom;
                        targetExit.LockLevel = exit.LockComplexity;
                    }
                }
            }

            // Extra descriptions
            if (yaml.ExtraDescriptions != null)
            {
                foreach (var ed in yaml.ExtraDescriptions)
                    room.ExtraDescriptions.Add(new ExtraDesc(ed.Keywords ?? "", ed.Description ?? ""));
            }

            // Triggers
            if (yaml.Triggers != null)
            {
                foreach (var trig in yaml.Triggers)
                    room.TriggersList.Add(trig);
            }

            // BZEditor-specific fields
            if (yaml.X.HasValue) room.X = yaml.X.Value;
            if (yaml.Y.HasValue) room.Y = yaml.Y.Value;
            if (yaml.Z.HasValue) room.Z = yaml.Z.Value;
            if (yaml.PlacedOnMap.HasValue) room.PlacedOnMap = yaml.PlacedOnMap.Value;

            // Multi-variant room description
            if (yaml.RoomDescriptions != null)
            {
                YamlRoomDescriptionMapper.FromYaml(yaml.RoomDescriptions, room.Description);
            }

            // Ingredients
            if (yaml.Ingredients != null)
            {
                foreach (var ingr in yaml.Ingredients)
                {
                    var ingredient = ingr.PowerAuto
                        ? new Ingredient(ingr.TypeName, ingr.Probability)
                        : new Ingredient(ingr.TypeName, ingr.Power, ingr.Probability);
                    room.Ingredients.Add(ingredient);
                }
            }

            return room;
        }

        private static Exit GetExitByDirection(Room room, string direction)
        {
            if (string.IsNullOrEmpty(direction))
                return null;

            switch (direction.ToLowerInvariant())
            {
                case "north": case "0": return room.ExitNorth;
                case "east": case "1": return room.ExitEast;
                case "south": case "2": return room.ExitSouth;
                case "west": case "3": return room.ExitWest;
                case "up": case "4": return room.ExitUp;
                case "down": case "5": return room.ExitDown;
                default: return null;
            }
        }
    }
}
