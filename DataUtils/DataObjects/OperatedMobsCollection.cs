using System;

namespace DataUtils
{
    public class OperatedMobsCollection : BaseDataArrayList
    {
        public OperatedMob this[int vNum, int tmp] => GetMob(vNum);

        public OperatedMob this[Guid guid] => GetMob(guid);

        public void Add(int mobVNum, bool conditionFlag, int maxInRoom)
        {
            var mob = new OperatedMob(mobVNum)
                          {
                              ConditionFlag = conditionFlag,
                              MaxInRoom = maxInRoom
                          };
            mob.Changed += FireChangeEvent;
            Add(mob);
        }

        public bool MobExists(int vNum)
        {
            return (GetMob(vNum) != null);
        }

        public OperatedMob GetMob(Guid guid)
        {
            foreach (OperatedMob lm in this)
            {
                if (lm.Guid == guid)
                    return lm;
            }
            return null;
        }

        public OperatedMob GetMob(int vNum)
        {
            foreach (OperatedMob lm in this)
            {
                if (lm.VNum == vNum)
                    return lm;
            }
            return null;
        }

        public OperatedMob GetMob(int vNum, int pos)
        {
            for (int i = 0; i < Count; i++)
            {
                var lm = (OperatedMob) this[i];
                if (lm.VNum == vNum && i == pos)
                    return lm;
            }
            return null;
        }

        public void RemoveMob(int vNum, int pos)
        {
            OperatedMob mob = GetMob(vNum, pos);
            if (mob != null)
                Remove(mob);
        }

        public void RemoveMob(Guid guid)
        {
            OperatedMob mob = GetMob(guid);
            if (mob != null)
                Remove(mob);
        }

        /// <summary>
        /// Удаление без учета позиции например для списка мобов, удаляемых из зоны 
        /// </summary>
        /// <param name="vNum"></param>
        public void RemoveMob(int vNum)
        {
            OperatedMob mob = GetMob(vNum);
            if (mob != null)
                Remove(mob);
        }
    }
}