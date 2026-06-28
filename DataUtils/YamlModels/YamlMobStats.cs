namespace DataUtils.YamlModels
{
    /// <summary>
    /// YAML model for mob basic stats - matches reference format
    /// </summary>
    public class YamlMobStats
    {
        /// <summary>
        /// Mob level
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Hitroll penalty (lower is better for mob)
        /// </summary>
        public int HitrollPenalty { get; set; } = 20;

        /// <summary>
        /// Armor class (lower is better)
        /// </summary>
        public int Armor { get; set; } = 100;

        /// <summary>
        /// HP as dice (dice_count, dice_size, bonus)
        /// </summary>
        public YamlDice Hp { get; set; }

        /// <summary>
        /// Damage as dice (dice_count, dice_size, bonus)
        /// </summary>
        public YamlDice Damage { get; set; }
    }
}
