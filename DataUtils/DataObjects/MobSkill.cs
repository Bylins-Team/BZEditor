namespace DataUtils
{
    public class MobSkill : BaseDataObject
    {
        private int percent;

        public MobSkill(int vNum, int percent)
        {
            VNum = vNum;
            this.percent = percent;
        }

        /// <summary>
        /// Процент владения скиллом
        /// </summary>
        public int Percent
        {
            get => percent;
            set
            {
                if (percent == value) return;
                percent = value;
                FireChangeEvent(this);
            }
        }
    }
}