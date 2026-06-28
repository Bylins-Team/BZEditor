using System;

namespace DataUtils
{
    public class StuffActivationsCollection : BaseDataArrayList
    {
        public StuffActivation this[Guid guid] => GetStuffActivation(guid);

        public StuffActivation this[int vNum, int tmp] => GetStuffActivation(vNum);

        public void Add(int vNum)
        {
            Add(new StuffActivation(vNum));
        }

        public StuffActivation GetStuffActivation(Guid guid)
        {
            foreach (StuffActivation lo in this)
            {
                if (lo.Guid == guid)
                    return lo;
            }
            return null;
        }

        public StuffActivation GetStuffActivation(int vNum)
        {
            foreach (StuffActivation lo in this)
            {
                if (lo.VNum == vNum)
                    return lo;
            }
            return null;
        }

        public void RemoveActivation(Guid guid)
        {
            StuffActivation set = GetStuffActivation(guid);
            if (set != null)
                Remove(set);
        }

        public void RemoveActivation(int setVNum)
        {
            StuffActivation set = GetStuffActivation(VNum);
            if (set != null)
                Remove(set);
        }

        public Guid VNum { get; set; }
    }
}