using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace DataUtils.YamlModels
{
    /// <summary>
    /// YAML model for zone - matches reference format
    /// </summary>
    public class YamlZone
    {
        [YamlMember(Alias = "vnum")]
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
        /// Under construction flag (engine reads it as an int: 0/1)
        /// </summary>
        public int UnderConstruction { get; set; }

        /// <summary>
        /// Type A zone list
        /// </summary>
        public List<int> TypeAZones { get; set; }

        /// <summary>
        /// Type B zone list
        /// </summary>
        public List<int> TypeBZones { get; set; }

        /// <summary>
        /// Zone reset commands as engine-readable strings, e.g. "M 0 7356 1 7370 1".
        /// The engine accepts both the letter (M/O/G/E/P/D/R/T/V/Q/F) and word
        /// (MOB/OBJECT/...) keyword forms and ignores any trailing "# comment".
        /// </summary>
        public List<string> Commands { get; set; } = new List<string>();
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
}
