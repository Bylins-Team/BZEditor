using System;

namespace DataUtils
{
    public class Set : BaseDataObject
    {
        public Guid Guid = Guid.NewGuid();
        private string name;
        private readonly SetStuffCollection stuffCollection = new SetStuffCollection();

        public Set(int vNum)
        {
            VNum = vNum;
            stuffCollection.Changed += FireChangeEvent;
        }

        public string Name
        {
            get => name;
            set
            {
                if (name == value) return;
                name = value;
                FireChangeEvent(this);
            }
        }

        public SetStuffCollection StuffCollection => stuffCollection;
    }
}