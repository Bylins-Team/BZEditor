using System;

namespace DataUtils
{
    public class MobObjsCollection : BaseDataArrayList
    {
        public MobObj this[Guid guid] => GetObj(guid);

        public MobObj this[int vNum, int tmp] => GetObj(vNum);

        public void Add(int objectVNum, bool conditionFlag /*, int MaxInWorld*/, int objPos, int probability)
        {
            var obj = new MobObj(objectVNum)
                             {
                                 ConditionFlag = conditionFlag,
                                 ObjPos = objPos,
                                 Probability = probability
                             };
            //Object.MaxInWorld = MaxInWorld;
            Add(obj);
        }

        public MobObj GetObj(Guid guid)
        {
            foreach (MobObj lo in this)
            {
                if (lo.Guid == guid)
                    return lo;
            }
            return null;
        }

        public MobObj GetObj(int objectVNum)
        {
            foreach (MobObj lo in this)
            {
                if (lo.VNum == objectVNum)
                    return lo;
            }
            return null;
        }

        public void RemoveObj(Guid guid)
        {
            MobObj obj = GetObj(guid);
            if (obj != null)
                Remove(obj);
        }

        public void RemoveObj(int objectVNum)
        {
            MobObj obj = GetObj(objectVNum);
            if (obj != null)
                Remove(obj);
        }

        public void ReplaceObjProb(int objectVNum, int prevVal, int newVal)
        {
            foreach (MobObj mo in this)
            {
                if (mo.VNum != objectVNum || mo.Probability != prevVal) continue;
                mo.Probability = newVal;
                return;
            }
        }

        public void ReplaceObjPos(int objectVNum, int prevVal, int newVal)
        {
            foreach (MobObj lo in this)
            {
                if (lo.VNum != objectVNum || lo.ObjPos != prevVal) continue;
                lo.ObjPos = newVal;
                return;
            }
        }
    }
}