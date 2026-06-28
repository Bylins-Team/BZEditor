namespace DataUtils
{
    public class MobSkillsCollection : BaseDataArrayList
    {
        public MobSkill Get(int vNum)
        {
            foreach (MobSkill skill in this)
            {
                if (skill.VNum == vNum)
                    return skill;
            }
            return null;
        }

        public void Add(int vNum, int percent)
        {
            if (Get(vNum) != null) return;
            var ms = new MobSkill(vNum, percent);
            Add(ms);
        }

        public void Update(int vNum, int percent)
        {
            MobSkill skill = Get(vNum);
            if (skill != null)
                skill.Percent = percent;
        }

        public void Remove(int vNum)
        {
            MobSkill skill = Get(vNum);
            if (skill != null)
                Remove(skill);
        }

        public new MobSkillsCollection Clone()
        {
            var res = new MobSkillsCollection();
            foreach (MobSkill s in this)
                res.Add(s.VNum, s.Percent);
            return res;
        }
    }
}