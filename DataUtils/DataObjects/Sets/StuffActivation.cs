using System;

namespace DataUtils
{
    public class StuffActivation : BaseDataObject
    {
        public Guid Guid = Guid.NewGuid();
        private int objectsQuontity;

        public StuffActivation(int vNum)
        {
            VNum = vNum;
        }

        /// <summary>
        /// Количество необходимых шмоток для активации
        /// </summary>
        public int ObjectsQuontity
        {
            get => objectsQuontity;
            set
            {
                if (objectsQuontity == value) return;
                objectsQuontity = value;
                FireChangeEvent(this);
            }
        }
    }
}