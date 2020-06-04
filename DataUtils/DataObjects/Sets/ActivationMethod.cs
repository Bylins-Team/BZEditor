namespace DataUtils
{
    using System;
    using System.Collections.Generic;

    public class ActivationMethod : BaseDataObject
    {
        public Guid Guid = Guid.NewGuid();
        private readonly List<int> classes = new List<int>();
        private string aMsg;
        private string dMsg;
        private string rAmg;
        private string rDmg;
        private string affs;

        public ActivationMethod(int vNum)
        {
            VNum = vNum;
        }

        public List<int> Classes => classes;

        public string AMsg
        {
            get => aMsg;
            set
            {
                if (aMsg == value) return; 
                aMsg = value;
                FireChangeEvent(this);
            }
        }

        public string DMsg
        {
            get => dMsg;
            set
            {
                if (dMsg == value) return; 
                dMsg = value;
                FireChangeEvent(this);
            }
        }

        public string RAmg
        {
            get => rAmg;
            set
            {
                if (rAmg == value) return; 
                rAmg = value;
                FireChangeEvent(this);
            }
        }

        public string RDmg
        {
            get => rDmg;
            set
            {
                if (rDmg == value) return; 
                rDmg = value;
                FireChangeEvent(this);
            }
        }

        public string Affs
        {
            get => affs;
            set
            {
                if (affs == value) return;
                affs = value;
                FireChangeEvent(this);
            }
        }

        public void AddClass(int classNum)
        {
            throw new NotImplementedException();
        }

        public void RemoveClass(int classNum)
        {
            throw new NotImplementedException();
        }
    }
}