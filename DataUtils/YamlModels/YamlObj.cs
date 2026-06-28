using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace DataUtils.YamlModels
{
    /// <summary>
    /// YAML model for an object (item) - matches reference format
    /// </summary>
    public class YamlObj
    {
        [YamlMember(Alias = "vnum")]
        public int VNum { get; set; }

        /// <summary>
        /// Names: aliases, nominative, genitive, dative, accusative, instrumental, prepositional
        /// </summary>
        public Dictionary<string, string> Names { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Short description (room description)
        /// </summary>
        public string ShortDesc { get; set; } = "";

        /// <summary>
        /// Action description (when used)
        /// </summary>
        public string ActionDesc { get; set; } = "";

        /// <summary>
        /// Object type number
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Extra flags as list of strings
        /// </summary>
        public List<string> ExtraFlags { get; set; } = new List<string>();

        /// <summary>
        /// Wear flags as list of strings
        /// </summary>
        public List<string> WearFlags { get; set; } = new List<string>();

        /// <summary>
        /// No-touch flags as list of strings
        /// </summary>
        public List<string> NoFlags { get; set; } = new List<string>();

        /// <summary>
        /// Anti-use flags as list of strings
        /// </summary>
        public List<string> AntiFlags { get; set; } = new List<string>();

        /// <summary>
        /// Affect flags as list of strings
        /// </summary>
        public List<string> AffectFlags { get; set; } = new List<string>();

        /// <summary>
        /// Material type
        /// </summary>
        public int Material { get; set; }

        /// <summary>
        /// Object-specific values (depends on type)
        /// </summary>
        public List<int> Values { get; set; } = new List<int>();

        /// <summary>
        /// Weight
        /// </summary>
        public int Weight { get; set; } = 1;

        /// <summary>
        /// Cost/price
        /// </summary>
        public int Cost { get; set; } = 1;

        /// <summary>
        /// Rent when in inventory
        /// </summary>
        public int RentOff { get; set; } = 1;

        /// <summary>
        /// Rent when worn/equipped
        /// </summary>
        public int RentOn { get; set; } = 1;

        /// <summary>
        /// Spec param (special parameter)
        /// </summary>
        public int SpecParam { get; set; }

        /// <summary>
        /// Maximum durability
        /// </summary>
        public int MaxDurability { get; set; } = 75;

        /// <summary>
        /// Current durability
        /// </summary>
        public int CurDurability { get; set; } = 75;

        /// <summary>
        /// Timer (decay time)
        /// </summary>
        public int Timer { get; set; } = 1440;

        /// <summary>
        /// Spell number attached to object
        /// </summary>
        public int Spell { get; set; } = -1;

        /// <summary>
        /// Spell level
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Sex (0=neutral, 1=male, 2=female, 3=plural)
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// Applies (bonuses) as list of {location, modifier}
        /// </summary>
        public List<YamlObjApply> Applies { get; set; } = new List<YamlObjApply>();

        /// <summary>
        /// Skills granted by the object (legacy 'S' lines) as list of {skill_id, value}
        /// </summary>
        public List<YamlMobSkill> Skills { get; set; } = new List<YamlMobSkill>();

        /// <summary>
        /// Extra descriptions
        /// </summary>
        public List<YamlExtraDesc> ExtraDescriptions { get; set; } = new List<YamlExtraDesc>();

        /// <summary>
        /// Extra values (potion/liquid spell data, the legacy V-lines) as an engine-named
        /// map. Null when absent so it is omitted; preserved verbatim so saving never drops it.
        /// </summary>
        public Dictionary<string, int> ExtraValues { get; set; }

        /// <summary>
        /// Max in world
        /// </summary>
        public int MaxInWorld { get; set; }

        /// <summary>
        /// Minimum remorts required
        /// </summary>
        public int MinimumRemorts { get; set; }

        /// <summary>
        /// Triggers (list of vnums)
        /// </summary>
        public List<int> Triggers { get; set; } = new List<int>();
    }

    /// <summary>
    /// Object apply/bonus entry
    /// </summary>
    public class YamlObjApply
    {
        public int Location { get; set; }
        public int Modifier { get; set; }

        public YamlObjApply() { }

        public YamlObjApply(int location, int modifier)
        {
            Location = location;
            Modifier = modifier;
        }
    }
}
