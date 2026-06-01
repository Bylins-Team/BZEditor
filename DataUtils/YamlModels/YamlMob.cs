using System.Collections.Generic;

namespace DataUtils.YamlModels
{
    /// <summary>
    /// YAML model for a mob (NPC) - matches reference format
    /// </summary>
    public class YamlMob
    {
        public int VNum { get; set; }

        /// <summary>
        /// Names in Russian grammatical cases
        /// Keys: imen (nominative), rod (genitive), dat (dative), vin (accusative), tvor (instrumental), pred (prepositional)
        /// </summary>
        public Dictionary<string, string> Names { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Descriptions: room_desc, short_desc
        /// </summary>
        public YamlMobDescriptions Descriptions { get; set; } = new YamlMobDescriptions();

        /// <summary>
        /// Keywords/aliases for mob
        /// </summary>
        public string Alias { get; set; } = "";

        /// <summary>
        /// Action flags as list of strings
        /// </summary>
        public List<string> ActionFlags { get; set; } = new List<string>();

        /// <summary>
        /// Affect flags as list of strings
        /// </summary>
        public List<string> AffectFlags { get; set; } = new List<string>();

        /// <summary>
        /// Alignment (-1000 to 1000)
        /// </summary>
        public int Alignment { get; set; }

        /// <summary>
        /// Mob type (S=simple, E=extended)
        /// </summary>
        public string MobType { get; set; } = "E";

        /// <summary>
        /// Basic stats: level, hitroll, armor, hp, damage
        /// </summary>
        public YamlMobStats Stats { get; set; } = new YamlMobStats();

        /// <summary>
        /// Gold (as dice)
        /// </summary>
        public YamlDice Gold { get; set; }

        /// <summary>
        /// Experience points
        /// </summary>
        public int Experience { get; set; }

        /// <summary>
        /// Position: default and start
        /// </summary>
        public YamlMobPosition Position { get; set; } = new YamlMobPosition();

        /// <summary>
        /// Sex (0=neutral, 1=male, 2=female)
        /// </summary>
        public int Sex { get; set; } = 1;

        /// <summary>
        /// Attributes (str, int, wis, dex, con, cha)
        /// </summary>
        public Dictionary<string, int> Attributes { get; set; } = new Dictionary<string, int>();

        /// <summary>
        /// Size
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Mob class
        /// </summary>
        public int MobClass { get; set; } = 100;

        /// <summary>
        /// Race
        /// </summary>
        public int Race { get; set; } = 100;

        /// <summary>
        /// Height
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Weight
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// Max in world (-1 = unlimited)
        /// </summary>
        public int MaxInWorld { get; set; } = -1;

        /// <summary>
        /// Skills as list of {skill_id, value}
        /// </summary>
        public List<YamlMobSkill> Skills { get; set; } = new List<YamlMobSkill>();

        /// <summary>
        /// Triggers (list of vnums)
        /// </summary>
        public List<int> Triggers { get; set; } = new List<int>();

        /// <summary>
        /// Enhanced E-spec fields
        /// </summary>
        public YamlMobEnhanced Enhanced { get; set; }

        /// <summary>
        /// Ingredients that mob can drop
        /// </summary>
        public List<YamlIngredient> Ingredients { get; set; } = new List<YamlIngredient>();

        /// <summary>
        /// Objects loaded after death
        /// </summary>
        public List<YamlLoadedObjAfterDeath> LoadedObjectAfterDeath { get; set; } = new List<YamlLoadedObjAfterDeath>();
    }

    /// <summary>
    /// Mob descriptions
    /// </summary>
    public class YamlMobDescriptions
    {
        public string RoomDesc { get; set; } = "";
        public string ShortDesc { get; set; } = "";
    }

    /// <summary>
    /// Mob position
    /// </summary>
    public class YamlMobPosition
    {
        public int Default { get; set; } = 8;
        public int Start { get; set; } = 8;
    }

    /// <summary>
    /// Mob skill entry
    /// </summary>
    public class YamlMobSkill
    {
        public int SkillId { get; set; }
        public int Value { get; set; }

        public YamlMobSkill() { }

        public YamlMobSkill(int skillId, int value)
        {
            SkillId = skillId;
            Value = value;
        }
    }

    /// <summary>
    /// Mob spell entry
    /// </summary>
    public class YamlMobSpell
    {
        public int SpellId { get; set; }
        public int Count { get; set; }

        public YamlMobSpell() { }

        public YamlMobSpell(int spellId, int count)
        {
            SpellId = spellId;
            Count = count;
        }
    }

    /// <summary>
    /// Enhanced E-spec mob fields
    /// </summary>
    public class YamlMobEnhanced
    {
        // Scalar fields
        public int? StrAdd { get; set; }
        public int? HpRegen { get; set; }
        public int? ArmourBonus { get; set; }
        public int? ManaRegen { get; set; }
        public int? CastSuccess { get; set; }
        public int? Morale { get; set; }
        public int? InitiativeAdd { get; set; }
        public int? Absorb { get; set; }
        public int? Aresist { get; set; }
        public int? Mresist { get; set; }
        public int? Presist { get; set; }
        public int? BareHandAttack { get; set; }
        public int? LikeWork { get; set; }
        public int? MaxFactor { get; set; }
        public int? ExtraAttack { get; set; }
        public int? MobRemort { get; set; }
        public string SpecialBitvector { get; set; }
        public int? Role { get; set; }

        // Array fields
        public List<int> Resistances { get; set; }
        public List<int> Saves { get; set; }
        public List<int> Feats { get; set; }
        public List<YamlMobSpell> Spells { get; set; }
        public List<int> Helpers { get; set; }
        public List<int> Destinations { get; set; }
    }
}
