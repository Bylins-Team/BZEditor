namespace DataUtils
{
    public class OperatedObj : BaseDataObject
    {
        public readonly OperatedObjCollection ObjectsInObject = new OperatedObjCollection();
        private int probability = 100;
        private int loadType;
        public OperatedObj(int inVNum)
        {
            VNum = inVNum;
            ObjectsInObject.Changed += FireChangeEvent;
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
                if (value == -1) value = 100;
                probability = value;
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
    }
}