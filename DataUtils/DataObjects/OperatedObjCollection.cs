namespace DataUtils
{
    public class OperatedObjCollection : BaseDataArrayList
    {
        /// <summary>
        /// Возарвщает загружаемый объект по его виртуальному номеру
        /// </summary>
        /// <param name="vNum">Виртуальный номер</param>
        /// <param name="tmp">не используется</param>
        /// <returns>Загружаемый объект, если он не найден, то null</returns>
        public OperatedObj this[int vNum, int tmp] => GetObj(vNum);

        /// <summary>
        /// Добвление объекта в коллекцию загружаемых объектров
        /// </summary>
        /// <param name="objectVNum">Виртуальный номер</param>
        /// <param name="probability">Вероятность загрузки</param>
        public void Add(int objectVNum, int probability, int loadType)
        {
            var obj = new OperatedObj(objectVNum)
            {
                LoadType = loadType,
                Probability = probability
            };
            obj.Changed += FireChangeEvent;
            Add(obj);
        }

        /// <summary>
        /// Проверка наличия объекта с заданным виртуальным номером в коллекции
        /// </summary>
        /// <param name="objVNum">Виртуальный номер</param>
        /// <returns>true если обеъкт присутствует в коллекции</returns>
        public bool ObjExists(int objVNum)
        {
            return (GetObj(objVNum) != null);
        }

        /// <summary>
        /// Возарвщает загружаемый объект по его виртуальному номеру
        /// </summary>
        /// <param name="objectVNum">Виртуальный номер</param>
        /// <returns>Загружаемый объект, если он не найден, то null</returns>
        public OperatedObj GetObj(int objectVNum)
        {
            foreach (OperatedObj lo in this)
            {
                if (lo.VNum == objectVNum)
                    return lo;
            }
            return null;
        }

        public void RemooveObj(int objectVNum)
        {
            OperatedObj obj = GetObj(objectVNum);
            if (obj != null)
                Remove(obj);
        }

        public void ReplaceObjProb(int objectVNum, int prevVal, int newVal)
        {
            foreach (OperatedObj lo in this)
            {
                if (lo.VNum != objectVNum || lo.Probability != prevVal) continue;
                lo.Probability = newVal;
                return;
            }
        }

        public void ReplaceLoadType(int objectVNum, int prevVal, int newVal)
        {
            foreach (OperatedObj lo in this)
            {
                if (lo.VNum != objectVNum || lo.LoadType != prevVal) continue;
                lo.LoadType = newVal;
                return;
            }
        }
    }
}