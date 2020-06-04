namespace DataUtils
{
    public class MobSpell : BaseDataObject
    {
        private int count;

        public MobSpell(int vNum, int count)
        {
            VNum = vNum;
            this.count = count;
        }

        /// <summary>
        ///  оличество заклинаний данного типа
        /// </summary>
        public int Count
        {
            get => count;
            set
            {
                if (count == value || value < 0) return;
                count = value;
                FireChangeEvent(this);
            }
        }
    }
}