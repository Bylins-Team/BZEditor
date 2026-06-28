using DataUtils.YamlModels;

namespace DataUtils.YamlMappers
{
    /// <summary>
    /// Mapper for Exit (room exit)
    /// </summary>
    public static class YamlExitMapper
    {
        public static YamlExit ToYaml(Exit exit)
        {
            if (exit == null) return new YamlExit();
            return new YamlExit
            {
                Description = exit.Description ?? "",
                Aliases = exit.Aliases ?? "",
                ExinNameVin = exit.ExinNameVin ?? "",
                ExitFlag = exit.ExitFlag,
                Key = exit.Key,
                LockLevel = exit.LockLevel,
                RoomVNum = exit.RoomVNum,
                ConditionFlag = exit.ConditionFlag,
                DoorState = exit.DoorState,
                Visibility = exit.Visibility
            };
        }

        public static void FromYaml(YamlExit yaml, Exit target)
        {
            if (yaml == null || target == null) return;
            target.Description = yaml.Description ?? "";
            target.Aliases = yaml.Aliases ?? "";
            target.ExinNameVin = yaml.ExinNameVin ?? "";
            target.ExitFlag = yaml.ExitFlag;
            target.Key = yaml.Key;
            target.LockLevel = yaml.LockLevel;
            target.RoomVNum = yaml.RoomVNum;
            target.ConditionFlag = yaml.ConditionFlag;
            target.DoorState = yaml.DoorState;
            target.Visibility = yaml.Visibility;
        }
    }
}
