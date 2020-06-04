namespace DataUtils
{
    public class LoadedObjAfterDeathCollection : BaseDataArrayList
    {
        public LoadedObjAfterDeath this[int vNum, int tmp] => GetObj(vNum);

        public void Add(int objectVNum, int loadProb, int loadType, int specParam)
        {
            var obj = new LoadedObjAfterDeath(objectVNum)
                             {
                                 LoadProb = loadProb,
                                 LoadType = loadType,
                                 SpecParam = specParam
                             };
            obj.Changed += FireChangeEvent;
            Add(obj);
        }

        public bool ObjExists(int objVNum)
        {
            return (GetObj(objVNum) != null);
        }

        public LoadedObjAfterDeath GetObj(int objectVNum)
        {
            foreach (LoadedObjAfterDeath lo in this)
            {
                if (lo.VNum == objectVNum)
                    return lo;
            }
            return null;
        }

        public void RemooveObj(int objectVNum)
        {
            LoadedObjAfterDeath obj = GetObj(objectVNum);
            if (obj != null)
                Remove(obj);
        }

        public void ReplaceObjProb(int objectVNum, int prevVal, int newVal)
        {
            foreach (LoadedObjAfterDeath lo in this)
            {
                if (lo.VNum != objectVNum || lo.LoadProb != prevVal) continue;
                lo.LoadProb = newVal;
                return;
            }
        }
    }
}