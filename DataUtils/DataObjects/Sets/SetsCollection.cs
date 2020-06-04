using System;

namespace DataUtils
{
    public class SetsCollection : BaseDataArrayList
    {
        public Set this[Guid guid] => GetSet(guid);

        public Set this[int vNum, int tmp] => GetSet(vNum);

        public void Add(int objectVNum, bool conditionFlag, int objPos, int probability)
        {
            var set = new Set(objectVNum);
            Add(set);
        }

        public Set GetSet(Guid guid)
        {
            foreach (Set lo in this)
            {
                if (lo.Guid == guid)
                    return lo;
            }
            return null;
        }

        public Set GetSet(int objectVNum)
        {
            foreach (Set lo in this)
            {
                if (lo.VNum == objectVNum)
                    return lo;
            }
            return null;
        }

        public void RemoveObj(Guid guid)
        {
            Set obj = GetSet(guid);
            if (obj != null)
                Remove(obj);
        }

        public void RemoveObj(int objectVNum)
        {
            Set obj = GetSet(objectVNum);
            if (obj != null)
                Remove(obj);
        }
    }
}