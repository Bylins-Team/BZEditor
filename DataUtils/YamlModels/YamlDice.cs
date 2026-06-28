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

            // After 'd': dice size is the leading digits, the rest is the (signed) bonus.
            // A negative bonus is written by the engine/converter as "+-N" (e.g. "0d0+-56"),
            // so the leading '+' separator must be stripped before parsing the signed value.
            string rest = diceString.Substring(dPos + 1);
            int i = 0;
            while (i < rest.Length && char.IsDigit(rest[i])) i++;

            int diceSize;
            if (int.TryParse(rest.Substring(0, i), out diceSize))
                result.DiceSize = diceSize;

            string bonusStr = rest.Substring(i);
            if (bonusStr.StartsWith("+"))
                bonusStr = bonusStr.Substring(1); // "+-56" -> "-56", "+56" -> "56"

            int bonus;
            if (bonusStr.Length > 0 && int.TryParse(bonusStr, out bonus))
                result.Bonus = bonus;

            return result;
        }

        /// <summary>
        /// Convert to dice string format
        /// </summary>
        public override string ToString()
        {
            // Legacy format always uses an explicit '+' separator; a negative bonus
            // therefore renders as "+-N" (e.g. "0d0+-56"), matching the engine/converter.
            return string.Format("{0}d{1}+{2}", DiceCount, DiceSize, Bonus);
        }
    }
}
