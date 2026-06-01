namespace DataUtils.YamlModels
{
    /// <summary>
    /// YAML model for dice notation (e.g., hp, damage, gold)
    /// Format: {dice_count}d{dice_size}+{bonus}
    /// </summary>
    public class YamlDice
    {
        public int DiceCount { get; set; }
        public int DiceSize { get; set; }
        public int Bonus { get; set; }

        public YamlDice() { }

        public YamlDice(int diceCount, int diceSize, int bonus)
        {
            DiceCount = diceCount;
            DiceSize = diceSize;
            Bonus = bonus;
        }

        /// <summary>
        /// Parse dice string like "2d6+5" into components
        /// </summary>
        public static YamlDice Parse(string diceString)
        {
            if (string.IsNullOrEmpty(diceString))
                return new YamlDice();

            var result = new YamlDice();

            // Format: XdY+Z or XdY-Z
            int dPos = diceString.IndexOf('d');
            if (dPos < 0)
            {
                // Just a number
                int val;
                if (int.TryParse(diceString, out val))
                    result.Bonus = val;
                return result;
            }

            // Parse dice count
            if (dPos > 0)
            {
                int diceCount;
                if (int.TryParse(diceString.Substring(0, dPos), out diceCount))
                    result.DiceCount = diceCount;
            }

            string rest = diceString.Substring(dPos + 1);
            int plusPos = rest.IndexOf('+');
            int minusPos = rest.IndexOf('-');
            int signPos = plusPos >= 0 ? plusPos : minusPos;

            if (signPos >= 0)
            {
                int diceSize;
                if (int.TryParse(rest.Substring(0, signPos), out diceSize))
                    result.DiceSize = diceSize;

                int bonus;
                if (int.TryParse(rest.Substring(signPos), out bonus))
                    result.Bonus = bonus;
            }
            else
            {
                int diceSize;
                if (int.TryParse(rest, out diceSize))
                    result.DiceSize = diceSize;
            }

            return result;
        }

        /// <summary>
        /// Convert to dice string format
        /// </summary>
        public override string ToString()
        {
            if (Bonus >= 0)
                return string.Format("{0}d{1}+{2}", DiceCount, DiceSize, Bonus);
            return string.Format("{0}d{1}{2}", DiceCount, DiceSize, Bonus);
        }
    }
}
