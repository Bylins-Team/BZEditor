using System;

namespace DataUtils
{
    public class ActivationMethodsCollection : BaseDataArrayList
    {
        public ActivationMethod this[Guid guid] => GetStuffActivation(guid);

        public ActivationMethod this[int vNum, int tmp] => GetSActivationMethod(vNum);

        public void Add(int vNum)
        {
            Add(new ActivationMethod(vNum));
        }

        public ActivationMethod GetStuffActivation(Guid guid)
        {
            foreach (ActivationMethod lo in this)
            {
                if (lo.Guid == guid)
                    return lo;
            }
            return null;
        }

        public ActivationMethod GetSActivationMethod(int vNum)
        {
            foreach (ActivationMethod lo in this)
            {
                if (lo.VNum == vNum)
                    return lo;
            }
            return null;
        }

        public void RemoveActivationMethod(Guid guid)
        {
            ActivationMethod set = GetStuffActivation(guid);
            if (set != null)
                Remove(set);
        }

        public void RemoveActivationMethod(int vNum)
        {
            ActivationMethod set = GetSActivationMethod(vNum);
            if (set != null)
                Remove(set);
        }
    }
}