using DataUtils.YamlModels;

namespace DataUtils.YamlMappers
{
    /// <summary>
    /// Mapper for MobStats
    /// </summary>
    public static class YamlMobStatsMapper
    {
        public static YamlMobStats ToYaml(MobStats stats)
        {
            if (stats == null) return new YamlMobStats();
            return new YamlMobStats
            {
                Str = stats.Str,
                Int = stats.Int,
                Wis = stats.Wis,
                Dex = stats.Dex,
                Con = stats.Con,
                Cha = stats.Cha,
                Size = stats.Size,
                Height = stats.Height,
                Weight = stats.Weight
            };
        }

        public static void FromYaml(YamlMobStats yaml, MobStats target)
        {
            if (yaml == null || target == null) return;
            target.Str = yaml.Str;
            target.Int = yaml.Int;
            target.Wis = yaml.Wis;
            target.Dex = yaml.Dex;
            target.Con = yaml.Con;
            target.Cha = yaml.Cha;
            target.Size = yaml.Size;
            target.Height = yaml.Height;
            target.Weight = yaml.Weight;
        }
    }
}
