namespace DataUtils
{
    public class LoadedObjAfterDeath : BaseDataObject
    {
        private int loadProb = 100;
        private int loadType;
        private int specParam;

        public LoadedObjAfterDeath(int vNum)
        {
            VNum = vNum;
        }

        /// <summary>
        /// процент загрузки
        /// </summary>
        public int LoadProb
        {
            get => loadProb;
            set
            {
                if (loadProb == value) return;
                loadProb = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// 0 - загружать всегда.
        /// 1 - загружать если предыдущий предмет списка был загружен.
        /// 2 - загружать всегда, не менять результата предыдущей загрузки.
        /// 3 - загружать если был загружен предыдущий, не менять результата.
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
        /// 0 - загружать всегда.
        /// 1 - загружать с убывающей вероятностью.
        /// 2 - загружать при освежевании трупа NPC.
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
    }
}