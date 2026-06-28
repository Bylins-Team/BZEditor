using YamlDotNet.Serialization;

namespace DataUtils.YamlModels
{
    /// <summary>
    /// YAML model for objects loaded after mob death (engine: dead_load entries)
    /// </summary>
    public class YamlLoadedObjAfterDeath
    {
        [YamlMember(Alias = "obj_vnum")]
        public int ObjVNum { get; set; }

        [YamlMember(Alias = "load_prob")]
        public int Probability { get; set; }

        [YamlMember(Alias = "load_type")]
        public int LoadType { get; set; }

        [YamlMember(Alias = "spec_param")]
        public int SpecParam { get; set; }
    }
}
