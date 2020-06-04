namespace DataUtils
{
    public class BonusesCollection : BaseDataArrayList
    {
        public Bonus Get(int vNum)
        {
            foreach (Bonus bonus in this)
            {
                if (bonus.VNum == vNum)
                    return bonus;
            }
            return null;
        }

        public void Add(int vNum, int value)
        {
            Bonus bonus = Get(vNum);
            if (bonus == null)
            {
                var b = new Bonus(vNum, value);
                b.Changed += FireChangeEvent;
                Add(b);
            }
        }

        public void Replace(int vNum, int value)
        {
            Bonus bonus = Get(vNum);
            if (bonus != null)
                bonus.Value = value;
        }

        public void Remove(int vNum)
        {
            Bonus bonus = Get(vNum);
            if (bonus != null)
                Remove(bonus);
        }

        public new BonusesCollection Clone()
        {
            var res = new BonusesCollection();
            foreach (Bonus b in this)
                res.Add(b.VNum, b.Value);
            return res;
        }
    }
}