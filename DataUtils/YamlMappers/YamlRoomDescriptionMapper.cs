using DataUtils.YamlModels;

namespace DataUtils.YamlMappers
{
    /// <summary>
    /// Mapper for RoomDescription (10 description variants)
    /// </summary>
    public static class YamlRoomDescriptionMapper
    {
        public static YamlRoomDescription ToYaml(RoomDescription desc)
        {
            if (desc == null) return new YamlRoomDescription();
            return new YamlRoomDescription
            {
                Main = desc.Main ?? "",
                Day = desc.Day ?? "",
                Night = desc.Night ?? "",
                WinterDay = desc.WinterDay ?? "",
                WinterNight = desc.WinterNight ?? "",
                SpringDay = desc.SpringDay ?? "",
                SpringNight = desc.SpringNight ?? "",
                SummerDay = desc.SummerDay ?? "",
                SummerNight = desc.SummerNight ?? "",
                AutumnDay = desc.AutumnDay ?? "",
                AutumnNight = desc.AutumnNight ?? "",
                DayR = desc.DayR,
                NightR = desc.NightR,
                WinterDayR = desc.WinterDayR,
                WinterNightR = desc.WinterNightR,
                SpringDayR = desc.SpringDayR,
                SpringNightR = desc.SpringNightR,
                SummerDayR = desc.SummerDayR,
                SummerNightR = desc.SummerNightR,
                AutumnDayR = desc.AutumnDayR,
                AutumnNightR = desc.AutumnNightR
            };
        }

        public static void FromYaml(YamlRoomDescription yaml, RoomDescription target)
        {
            if (yaml == null || target == null) return;
            target.Main = yaml.Main ?? "";
            target.Day = yaml.Day ?? "";
            target.Night = yaml.Night ?? "";
            target.WinterDay = yaml.WinterDay ?? "";
            target.WinterNight = yaml.WinterNight ?? "";
            target.SpringDay = yaml.SpringDay ?? "";
            target.SpringNight = yaml.SpringNight ?? "";
            target.SummerDay = yaml.SummerDay ?? "";
            target.SummerNight = yaml.SummerNight ?? "";
            target.AutumnDay = yaml.AutumnDay ?? "";
            target.AutumnNight = yaml.AutumnNight ?? "";
            target.DayR = yaml.DayR;
            target.NightR = yaml.NightR;
            target.WinterDayR = yaml.WinterDayR;
            target.WinterNightR = yaml.WinterNightR;
            target.SpringDayR = yaml.SpringDayR;
            target.SpringNightR = yaml.SpringNightR;
            target.SummerDayR = yaml.SummerDayR;
            target.SummerNightR = yaml.SummerNightR;
            target.AutumnDayR = yaml.AutumnDayR;
            target.AutumnNightR = yaml.AutumnNightR;
        }
    }
}
