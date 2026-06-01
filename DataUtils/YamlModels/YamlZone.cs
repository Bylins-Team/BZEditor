using System.Collections.Generic;

namespace DataUtils.YamlModels
{
    /// <summary>
    /// YAML model for zone - matches reference format
    /// </summary>
    public class YamlZone
    {
        public int VNum { get; set; }
        public string Name { get; set; } = "";

        /// <summary>
        /// Metadata: comment, location, author, description
        /// </summary>
        public YamlZoneMetadata Metadata { get; set; }

        /// <summary>
        /// Builders (string)
        /// </summary>
        public string Builders { get; set; }

        /// <summary>
        /// First room vnum in zone
        /// </summary>
        public int FirstRoom { get; set; }

        /// <summary>
        /// Last/top room vnum in zone
        /// </summary>
        public int TopRoom { get; set; }

        /// <summary>
        /// Zone mode
        /// </summary>
        public int Mode { get; set; }

        /// <summary>
        /// Zone type
        /// </summary>
        public int ZoneType { get; set; }

        /// <summary>
        /// Zone group
        /// </summary>
        public int ZoneGroup { get; set; }

        /// <summary>
        /// Entrance room vnum
        /// </summary>
        public int Entrance { get; set; }

        /// <summary>
        /// Lifespan/reset interval
        /// </summary>
        public int Lifespan { get; set; } = 60;

        /// <summary>
        /// Reset mode (0=never, 1=no_players, 2=always)
        /// </summary>
        public int ResetMode { get; set; } = 1;

        /// <summary>
        /// Reset when idle
        /// </summary>
        public int ResetIdle { get; set; }

        /// <summary>
        /// Under construction flag
        /// </summary>
        public bool UnderConstruction { get; set; }

        /// <summary>
        /// Type A zone list
        /// </summary>
        public List<int> TypeAZones { get; set; }

        /// <summary>
        /// Type B zone list
        /// </summary>
        public List<int> TypeBZones { get; set; }

        /// <summary>
        /// Zone reset commands
        /// </summary>
        public List<YamlZoneCommand> Commands { get; set; } = new List<YamlZoneCommand>();
    }

    /// <summary>
    /// Zone metadata
    /// </summary>
    public class YamlZoneMetadata
    {
        public string Comment { get; set; }
        public string Location { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
    }

    /// <summary>
    /// Zone reset command
    /// </summary>
    public class YamlZoneCommand
    {
        /// <summary>
        /// Command type: M=mob, O=obj, P=put_obj, G=give, E=equip, D=door, R=remove, T=trigger, V=variable, Q=follow
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// If-flag (0=always, 1=if last command succeeded)
        /// </summary>
        public int IfFlag { get; set; }

        // Vnums depending on command type
        public int? MobVnum { get; set; }
        public int? ObjVnum { get; set; }
        public int? RoomVnum { get; set; }
        public int? ContainerVnum { get; set; }
        public int? TriggerVnum { get; set; }
        public int? LeaderMobVnum { get; set; }
        public int? FollowerMobVnum { get; set; }

        // Other parameters
        public int? WearPos { get; set; }
        public int? Direction { get; set; }
        public int? MaxWorld { get; set; }
        public int? MaxRoom { get; set; }
        public int? Max { get; set; }
        public int? LoadProb { get; set; }
        public int? State { get; set; }
        public int? TriggerType { get; set; }
        public int? EntityVnum { get; set; }
        public int? Context { get; set; }
        public int? VarVnum { get; set; }
        public string VarName { get; set; }
        public string VarValue { get; set; }
    }
}
