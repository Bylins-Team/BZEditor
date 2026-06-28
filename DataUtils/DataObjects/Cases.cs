namespace DataUtils
{
    public class Cases : BaseDataObject
    {
        private string dat = "";
        private string imen = "";
        private string pred = "";
        private string rod = "";
        private string tvor = "";
        private string vin = "";

        /// <summary>
        /// Именительный падеж
        /// </summary>
        public string Imen
        {
            get => imen;
            set
            {
                if (imen == value) return;
                imen = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Родительный падеж
        /// </summary>
        public string Rod
        {
            get => rod;
            set
            {
                if (rod == value) return;
                rod = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Дательный падеж
        /// </summary>
        public string Dat
        {
            get => dat;
            set
            {
                if (dat == value) return;
                dat = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Винительный падеж
        /// </summary>
        public string Vin
        {
            get => vin;
            set
            {
                if (vin == value) return;
                vin = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Творительный падеж
        /// </summary>
        public string Tvor
        {
            get => tvor;
            set
            {
                if (tvor == value) return;
                tvor = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Предложный падеж
        /// </summary>
        public string Pred
        {
            get => pred;
            set
            {
                if (pred == value) return;
                pred = value;
                FireChangeEvent(this);
            }
        }

        public Cases Clone()
        {
            var res = new Cases {Imen = Imen, Rod = Rod, Dat = Dat, Vin = Vin, Tvor = Tvor, Pred = Pred};
            return res;
        }
    }
}