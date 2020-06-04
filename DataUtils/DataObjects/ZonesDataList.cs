using System;

namespace DataUtils
{
    [Serializable]
    public sealed class ZonesDataList : BaseDataArrayList
    {
        public new ZoneData this[int index] => (ZoneData) (base[index]);

        public ZoneData this[string fileName]
        {
            get
            {
                foreach (ZoneData zd in this)
                {
                    if (zd.FileName == fileName)
                        return zd;
                }
                return null;
            }
        }
    }
}