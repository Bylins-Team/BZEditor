namespace DataUtils.YamlModels
{
    /// <summary>
    /// YAML model for room descriptions (10 variants: main + day/night + 4 seasons * 2)
    /// </summary>
    public class YamlRoomDescription
    {
        public string Main { get; set; } = "";
        public string Day { get; set; } = "";
        public string Night { get; set; } = "";
        public string WinterDay { get; set; } = "";
        public string WinterNight { get; set; } = "";
        public string SpringDay { get; set; } = "";
        public string SpringNight { get; set; } = "";
        public string SummerDay { get; set; } = "";
        public string SummerNight { get; set; } = "";
        public string AutumnDay { get; set; } = "";
        public string AutumnNight { get; set; } = "";

        // Flags indicating whether description was replaced
        public bool DayR { get; set; }
        public bool NightR { get; set; }
        public bool WinterDayR { get; set; }
        public bool WinterNightR { get; set; }
        public bool SpringDayR { get; set; }
        public bool SpringNightR { get; set; }
        public bool SummerDayR { get; set; }
        public bool SummerNightR { get; set; }
        public bool AutumnDayR { get; set; }
        public bool AutumnNightR { get; set; }
    }
}
