using System;

namespace DataUtils
{
    public class MobObjsAfterDeathCollection : BaseDataArrayList
    {
        public MobObjAfterDeath this[Guid guid] => GetObj(guid);

        public MobObjAfterDeath this[int vNum, int tmp] => GetObj(vNum);

        public void Add(int objectVNum, int probability, int loadType, int specParam)
        {
            var obj = new MobObjAfterDeath(objectVNum)
                             {
                                 Probability = probability,
                                 LoadType = loadType,
                                 SpecParam = specParam
            };
            Add(obj);
        }

        public MobObjAfterDeath GetObj(Guid guid)
        {
            foreach (MobObjAfterDeath lo in this)
            {
                if (lo.Guid == guid)
                    return lo;
            }
            return null;
        }

        public MobObjAfterDeath GetObj(int objectVNum)
        {
            foreach (MobObjAfterDeath lo in this)
            {
                if (lo.VNum == objectVNum)
                    return lo;
            }
            return null;
        }

        public void RemoveObj(Guid guid)
        {
            MobObjAfterDeath obj = GetObj(guid);
            if (obj != null)
                Remove(obj);
        }

        public void RemoveObj(int objectVNum)
        {
            MobObjAfterDeath obj = GetObj(objectVNum);
            if (obj != null)
                Remove(obj);
        }

        public void ReplaceObjProb(int objectVNum, int prevVal, int newVal)
        {
            foreach (MobObjAfterDeath mo in this)
            {
                if (mo.VNum != objectVNum || mo.Probability != prevVal) continue;
                mo.Probability = newVal;
                FireChangeEvent(this);
                return;
            }
        }

        public void ReplaceLoadType(int objectVNum, int prevVal, int newVal)
        {
            foreach (MobObjAfterDeath lo in this)
            {
                if (lo.VNum != objectVNum || lo.LoadType != prevVal) continue;
                lo.LoadType = newVal;
                FireChangeEvent(this);
                return;
            }
        }

        public void ReplaceSpecParam(int objectVNum, int prevVal, int newVal)
        {
            foreach (MobObjAfterDeath lo in this)
            {
                if (lo.VNum != objectVNum || lo.SpecParam != prevVal) continue;
                lo.SpecParam = newVal;
                FireChangeEvent(this);
                return;
            }
        }
    }
}