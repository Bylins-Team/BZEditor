using System;

namespace DataUtils
{
    public class CIngrsCollection : CBaseDataArrayList
    {
        public CIngr Get(string Name)
        {
            foreach (CIngr Ingr in this)
            {
                if (Ingr.Name == Name)
                    return Ingr;
            }
            return null;
        }

        public CIngr Get(Guid GUID)
        {
            foreach (CIngr Ingr in this)
            {
                if (Ingr.GUID == GUID)
                    return Ingr;
            }
            return null;
        }

        public void Add(string Name, int Str, int Percent)
        {
            CIngr Ingr = Get(Name);
            if (Ingr != null) return;
            var i = new CIngr(Name, Str, Percent);
            i.Changed += FireChangeEvent;
            Add(i);
        }

        public void Replace(Guid GUID, int Str, int Percent)
        {
            CIngr Ingr = Get(GUID);
            if (Ingr == null) return;
            Ingr.Str = Str;
            Ingr.Percent = Percent;
        }

        public void Remove(Guid GUID)
        {
            CIngr Ingr = Get(GUID);
            if (Ingr == null) return;
            Remove(Ingr);
        }

        public new CIngrsCollection Clone()
        {
            var res = new CIngrsCollection();
            foreach (CIngr i in this)
                res.Add(i.FName, i.FStr, i.FPercent);
            return res;
        }
    }
}