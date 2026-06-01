namespace DataUtils.YamlModels
{
    /// <summary>
    /// YAML model for a room exit - matches reference format
    /// </summary>
    public class YamlExit
    {
        /// <summary>
        /// Direction as string: north, east, south, west, up, down
        /// </summary>
        public string Direction { get; set; } = "";

        /// <summary>
        /// Exit description
        /// </summary>
        public string Description { get; set; } = "";

        /// <summary>
        /// Keywords for door/exit
        /// </summary>
        public string Keywords { get; set; } = "";

        /// <summary>
        /// Exit flags (door type, etc.)
        /// </summary>
        public int ExitFlags { get; set; }

        /// <summary>
        /// Key vnum (-1 = no key)
        /// </summary>
        public int Key { get; set; } = -1;

        /// <summary>
        /// Target room vnum
        /// </summary>
        public int ToRoom { get; set; } = -1;

        /// <summary>
        /// Lock complexity level
        /// </summary>
        public int LockComplexity { get; set; }
    }
}
