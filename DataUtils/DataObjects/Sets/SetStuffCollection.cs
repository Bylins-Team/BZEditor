using System;

namespace DataUtils
{
    public class SetStuffCollection : BaseDataArrayList
    {
        public SetStuff this[Guid guid] => GetSetStuff(guid);

        public SetStuff this[int vNum, int tmp] => GetSetStuff(vNum);

        public void Add(int vNum)
        {
            Add(new SetStuff(vNum));
        }

        public SetStuff GetSetStuff(Guid guid)
        {
            foreach (SetStuff lo in this)
            {
                if (lo.Guid == guid)
                    return lo;
            }
            return null;
        }

        public SetStuff GetSetStuff(int vNum)
        {
            foreach (SetStuff lo in this)
            {
                if (lo.VNum == vNum)
                    return lo;
            }
            return null;
        }

        public void RemoveObj(Guid guid)
        {
            SetStuff set = GetSetStuff(guid);
            if (set != null)
                Remove(set);
        }

        public void RemoveObj(int vNum)
        {
            SetStuff set = GetSetStuff(vNum);
            if (set != null)
                Remove(set);
        }
    }
}