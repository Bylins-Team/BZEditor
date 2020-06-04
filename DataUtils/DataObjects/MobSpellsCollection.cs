namespace DataUtils
{
    public class MobSpellsCollection : BaseDataArrayList
    {
        public MobSpell Get(int vNum)
        {
            foreach (MobSpell spell in this)
            {
                if (spell.VNum == vNum)
                    return spell;
            }
            return null;
        }

        public void AddIncr(int vNum)
        {
            MobSpell spell = Get(vNum);
            if (spell != null)
                spell.Count++;
            else
                Add(new MobSpell(vNum, 1));
        }

        public void Replace(int vNum, int count)
        {
            MobSpell spell = Get(vNum);
            if (spell != null)
                spell.Count = count;
        }

        public void Add(int vNum, int count)
        {
            MobSpell spell = Get(vNum);
            if (spell == null)
            {
                var s = new MobSpell(vNum, count);
                //s.Changed += new CBaseDataObject.ChangeEvent(FireChangeEvent);
                Add(s);
            }
        }

        public void Remove(int vNum)
        {
            MobSpell spell = Get(vNum);
            if (spell != null)
                Remove(spell);
        }

        public new MobSpellsCollection Clone()
        {
            var res = new MobSpellsCollection();
            foreach (MobSpell s in this)
                res.Add(s.VNum, s.Count);
            return res;
        }
    }
}