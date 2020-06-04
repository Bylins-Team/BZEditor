namespace DataUtils
{
    public class Ingredient : BaseDataObject
    {
        private string typeName;
        private int probability;
        private int power;
        private bool powerAuto;

        public Ingredient(string typeName, int power, int probability)
        {
            this.typeName = typeName;
            this.probability = probability;
            this.power = power;
        }

        public Ingredient(string typeName, int probability)
        {
            this.typeName = typeName;
            this.probability = probability;
            powerAuto = true;
        }

        /// <summary>
        /// Наименование типа
        /// </summary>
        public string TypeName
        {
            get => typeName;
            set
            {
                if (typeName == value) return;
                typeName = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Сила ингра
        /// </summary>
        public int Power
        {
            get => power;
            set
            {
                if (power == value) return;
                power = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Сила ингра определяется движком
        /// </summary>
        public bool PowerAuto
        {
            get => powerAuto;
            set
            {
                if (powerAuto == value) return;
                powerAuto = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Процент падения ингра
        /// </summary>
        public int Probability
        {
            get => probability;
            set
            {
                if (probability == value) return;
                probability = value;
                FireChangeEvent(this);
            }
        }
    }
}