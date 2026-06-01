using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace DataUtils.YamlModels
{
    /// <summary>
    /// YAML model for a room - matches reference format
    /// </summary>
    public class YamlRoom
    {
        [YamlMember(Alias = "vnum")]
        public int VNum { get; set; }

        /// <summary>
        /// Zone number this room belongs to
        /// </summary>
        public int Zone { get; set; } = -1;

        /// <summary>
        /// Room name/title
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// Room description
        /// </summary>
        public string Description { get; set; } = "";

        /// <summary>
        /// Room flags as list of strings
        /// </summary>
        public List<string> Flags { get; set; } = new List<string>();

        /// <summary>
        /// Sector type (terrain)
        /// </summary>
        public int Sector { get; set; }

        /// <summary>
        /// Exits as list with direction names
        /// </summary>
        public List<YamlExit> Exits { get; set; } = new List<YamlExit>();

        /// <summary>
        /// Extra descriptions
        /// </summary>
        public List<YamlExtraDesc> ExtraDescriptions { get; set; } = new List<YamlExtraDesc>();

        /// <summary>
        /// Triggers (list of vnums)
        /// </summary>
        public List<int> Triggers { get; set; } = new List<int>();

        // BZEditor-specific fields for map visualization (not in reference)
        /// <summary>
        /// X coordinate on map
        /// </summary>
        public int? X { get; set; }

        /// <summary>
        /// Y coordinate on map
        /// </summary>
        public int? Y { get; set; }

        /// <summary>
        /// Z coordinate on map
        /// </summary>
        public int? Z { get; set; }

        /// <summary>
        /// Whether room is placed on visual map
        /// </summary>
        public bool? PlacedOnMap { get; set; }

        /// <summary>
        /// Multi-variant room description for weather/time
        /// </summary>
        public YamlRoomDescription RoomDescriptions { get; set; }

        /// <summary>
        /// Ingredients found in room
        /// </summary>
        public List<YamlIngredient> Ingredients { get; set; } = new List<YamlIngredient>();
    }
}
