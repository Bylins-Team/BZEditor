using DataUtils.YamlModels;

namespace DataUtils.YamlMappers
{
    /// <summary>
    /// Mapper for OperatedMob (mob placed in room)
    /// </summary>
    public static class YamlOperatedMobMapper
    {
        public static YamlOperatedMob ToYaml(OperatedMob opMob)
        {
            if (opMob == null) return null;

            var yaml = new YamlOperatedMob
            {
                VNum = opMob.VNum,
                ConditionFlag = opMob.ConditionFlag,
                MaxInRoom = opMob.MaxInRoom,
                Leader = opMob.Leader,
                FollowsBy = opMob.FollowsBy
            };

            // Items equipped/given to mob
            foreach (MobObj item in opMob.Items)
            {
                yaml.Items.Add(new YamlMobObj
                {
                    VNum = item.VNum,
                    ConditionFlag = item.ConditionFlag,
                    MaxInWorld = item.MaxInWorld,
                    ObjPos = item.ObjPos,
                    Probability = item.Probability
                });
            }

            // Items dropped after death
            foreach (MobObjAfterDeath item in opMob.ItemsAfterDeath)
            {
                yaml.ItemsAfterDeath.Add(new YamlMobObjAfterDeath
                {
                    VNum = item.VNum,
                    Probability = item.Probability,
                    LoadType = item.LoadType,
                    SpecParam = item.SpecParam
                });
            }

            return yaml;
        }

        public static OperatedMob FromYaml(YamlOperatedMob yaml)
        {
            if (yaml == null) return null;

            var opMob = new OperatedMob(yaml.VNum)
            {
                ConditionFlag = yaml.ConditionFlag,
                MaxInRoom = yaml.MaxInRoom,
                Leader = yaml.Leader,
                FollowsBy = yaml.FollowsBy
            };

            // Items equipped/given to mob
            if (yaml.Items != null)
            {
                foreach (var item in yaml.Items)
                {
                    opMob.AddObject(item.VNum, item.ConditionFlag, item.ObjPos, item.Probability);
                }
            }

            // Items dropped after death
            if (yaml.ItemsAfterDeath != null)
            {
                foreach (var item in yaml.ItemsAfterDeath)
                {
                    opMob.AddObjectAfterDeath(item.VNum, item.Probability, item.LoadType, item.SpecParam);
                }
            }

            return opMob;
        }
    }
}
