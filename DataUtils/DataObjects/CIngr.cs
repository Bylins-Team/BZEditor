using System;

namespace DataUtils
{
    public class CIngr : CBaseDataObject
    {
        public string FName;
        public int FPercent;
        public int FStr;
        public Guid GUID = Guid.NewGuid();

        public CIngr(string Name, int Str, int Percent)
        {
            this.Name = Name;
            this.Str = Str;
            this.Percent = Percent;
        }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name
        {
            get { return FName; }
            set
            {
                FName = value;
                FireChangeEvent(this);
            }
        }

        public int Str
        {
            get { return FStr; }
            set
            {
                FStr = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Сила ингра
        /// </summary>
        public int Percent
        {
            get { return FPercent; }
            set
            {
                FPercent = value;
                FireChangeEvent(this);
            }
        }
    }
}