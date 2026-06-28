namespace DataUtils
{
    public class ExtraDescCollection : BaseDataArrayList
    {
        public ExtraDesc Get(string aliases)
        {
            foreach (ExtraDesc extraDesc in this)
            {
                if (extraDesc.Aliases == aliases)
                    return extraDesc;
            }
            return null;
        }

        public void Add(string aliases, string description)
        {
            ExtraDesc extraDesc = Get(aliases);
            if (extraDesc == null)
            {
                var ed = new ExtraDesc(aliases, description);
                ed.Changed += FireChangeEvent;
                Add(ed);
            }
            else
                extraDesc.Description = description;
        }

        public void Replace(string trgAliases, string newAliases, string description)
        {
            ExtraDesc extraDesc = Get(trgAliases);
            if (extraDesc == null) return;
            extraDesc.Aliases = newAliases;
            extraDesc.Description = description;
        }

        public void Remove(string aliases)
        {
            ExtraDesc extraDesc = Get(aliases);
            if (extraDesc != null)
                Remove(extraDesc);
        }

        public override object Clone()
        {
            var res = new ExtraDescCollection();
            foreach (ExtraDesc ed in this)
                res.Add(ed.Aliases, ed.Description);
            return res;
        }
    }
}