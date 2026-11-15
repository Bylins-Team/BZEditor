using System;

namespace DataUtils
{
    public class MobObj : BaseDataObject
    {
        private bool conditionFlag;
        private int maxInWorld;
        private int objPos = -1;
        private int probability = 100;
        public Guid Guid = Guid.NewGuid();

        public MobObj(int vNum)
        {
            VNum = vNum;
        }

        /// <summary>
        /// The reset command's if_flag as read (-1 = not loaded). Bit 1 (CHECK_SUCCESS) is what
        /// ConditionFlag edits; bit 2 (FLAG_PERSIST: "don't change the success state") has no UI
        /// and is carried through here so a save does not turn 3 into 1 or 2 into 0.
        /// </summary>
        public int IfFlagRaw { get; set; } = -1;

        /// <summary>
        /// Флаг условия загрузки
        /// </summary>
        public bool ConditionFlag
        {
            get => conditionFlag;
            set
            {
                if (conditionFlag == value) return;
                conditionFlag = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Максимум в миреы
        /// </summary>
        public int MaxInWorld
        {
            get => maxInWorld;
            set
            {
                if (maxInWorld == value) return;
                maxInWorld = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Позиция объекта
        /// </summary>
        public int ObjPos
        {
            get => objPos;
            set
            {
                if (objPos == value) return;
                objPos = value;
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