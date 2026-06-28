namespace DataUtils.YamlModels
{
    /// <summary>
    /// YAML model for object equipped on/given to mob
    /// </summary>
    public class YamlMobObj
    {
        public int VNum { get; set; }
        public bool ConditionFlag { get; set; }
        public int MaxInWorld { get; set; }
        public int ObjPos { get; set; } = -1;
        public int Probability { get; set; } = 100;
    }

    /// <summary>
    /// YAML model for object dropped after mob death
    /// </summary>
    public class YamlMobObjAfterDeath
    {
        public int VNum { get; set; }
        public int Probability { get; set; } = 100;
        public int LoadType { get; set; }
        public int SpecParam { get; set; }
    }
}
