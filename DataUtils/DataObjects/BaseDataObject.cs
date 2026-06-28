using System;

namespace DataUtils
{
    public class BaseDataObject
    {
        #region Delegates

        public delegate void ChangeEvent(object sender);

        #endregion

        private int vNum;

        public bool Modifyed;

        public Guid InternaGuid = Guid.NewGuid();

        /// <summary>
        /// Виртуальный номер
        /// </summary>
        public int VNum
        {
            get => vNum;
            set
            {
                if (vNum == value) return;
                vNum = value;
                FireChangeEvent(this);
            }
        }

        public event ChangeEvent Changed;

        public virtual void FireChangeEvent(object sender)
        {
            if (!StaticData.CanFireChangeEvent) return;
            Modifyed = true;
            Changed?.Invoke(this);
        }
    }
}