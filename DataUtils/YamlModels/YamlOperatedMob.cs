using System.Collections.Generic;

namespace DataUtils.YamlModels
{
    /// <summary>
    /// YAML model for operated mob (mob placed in a room)
    /// </summary>
    public class YamlOperatedMob
    {
        public int VNum { get; set; }
        public bool ConditionFlag { get; set; }
        public int MaxInRoom { get; set; } = 1;
        public bool Leader { get; set; }
        public int FollowsBy { get; set; } = -1;
        public List<YamlMobObj> Items { get; set; } = new List<YamlMobObj>();
        public List<YamlMobObjAfterDeath> ItemsAfterDeath { get; set; } = new List<YamlMobObjAfterDeath>();
    }
}
