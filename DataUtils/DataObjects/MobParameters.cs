namespace DataUtils
{
    public class MobStats : BaseDataObject
    {
        private int cha = 11;
        private int con = 11;
        private int dex = 11;
        private int height = 175;
        private int intell = 11;
        private int size = 50;
        private int str = 11;
        private int weight = 140;
        private int wis = 11;

        /// <summary>
        /// Сила
        /// </summary>
        public int Str
        {
            get => str;
            set
            {
                if (str == value) return;
                str = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Интеллект
        /// </summary>
        public int Int
        {
            get => intell;
            set
            {
                if (intell == value) return;
                intell = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Мудрость
        /// </summary>
        public int Wis
        {
            get => wis;
            set
            {
                if (wis == value) return;
                wis = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Ловкость
        /// </summary>
        public int Dex
        {
            get => dex;
            set
            {
                if (dex == value) return;
                dex = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Тело
        /// </summary>
        public int Con
        {
            get => con;
            set
            {
                if (con == value) return;
                con = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Обаяние
        /// </summary>
        public int Cha
        {
            get => cha;
            set
            {
                if (cha == value) return;
                cha = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Размер
        /// </summary>
        public int Size
        {
            get => size;
            set
            {
                if (size == value) return;
                size = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Рост
        /// </summary>
        public int Height
        {
            get => height;
            set
            {
                if (height == value) return;
                height = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Вес
        /// </summary>
        public int Weight
        {
            get => weight;
            set
            {
                if (weight == value) return;
                weight = value;
                FireChangeEvent(this);
            }
        }

        public void CopyTo(MobStats newStats)
        {
            newStats.Cha = str;
            newStats.Con = con;
            newStats.Dex = dex;
            newStats.Height = height;
            newStats.Int = intell;
            newStats.Size = size;
            newStats.Str = str;
            newStats.Weight = weight;
            newStats.Wis = wis;
        }

        public MobStats Clone()
        {
            var res = new MobStats
                          {
                              Cha = str,
                              Con = con,
                              Dex = dex,
                              Height = height,
                              Int = intell,
                              Size = size,
                              Str = str,
                              Weight = weight,
                              Wis = wis
                          };
            return res;
        }
    }
}