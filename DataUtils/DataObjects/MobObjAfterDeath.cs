using System;

namespace DataUtils
{
    public class MobObjAfterDeath : BaseDataObject
    {
        private int probability = 100;
        private int loadType = 0;
        private int specParam = 0;
        public Guid Guid = Guid.NewGuid();

        public MobObjAfterDeath(int vNum)
        {
            VNum = vNum;
        }

        /// <summary>
        /// Тип загрузки
        /// </summary>
        public int LoadType
        {
            get => loadType;
            set
            {
                if (loadType == value) return;
                loadType = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Спец.параметр
        /// </summary>
        public int SpecParam
        {
            get => specParam;
            set
            {
                if (specParam == value) return;
                specParam = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Вероятность загрузки
        /// </summary>
        public int Probability
        {
            get => probability;
            set
            {
                if (probability == value) return;
                probability = value;
                FireChangeEvent(this);
            }
        }
    }
}