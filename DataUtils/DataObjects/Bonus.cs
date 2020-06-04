namespace DataUtils
{
    public class Bonus : BaseDataObject
    {
        private int bonusValue;

        public Bonus(int vNum, int bonusValue)
        {
            VNum = vNum;
            this.bonusValue = bonusValue;
        }

        /// <summary>
        /// Величина бонуса
        /// </summary>
        public int Value
        {
            get => bonusValue;
            set
            {
                if (bonusValue == value) return;
                bonusValue = value;
                FireChangeEvent(this);
            }
        }
    }
}