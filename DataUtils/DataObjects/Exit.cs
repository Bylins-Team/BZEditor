namespace DataUtils
{
    public class Exit : BaseDataObject
    {
        private string aliases = "";
        private bool conditionFlag;
        private string description = "";
        private int doorDefault;
        private int visibility = -1;
        private string exinNameVin = "";
        private int exitFlag;
        private int key = -1;
        private int lockLevel;
        private int roomVNum = -1;

        /// <summary>
        /// Описание выхода
        /// </summary>
        public string Description
        {
            get => description;
            set
            {
                if (description == value) return;
                description = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Альясы выхода
        /// </summary>
        public string Aliases
        {
            get => aliases;
            set
            {
                if (aliases == value) return;
                aliases = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Название выхода в винительном падеже
        /// </summary>
        public string ExinNameVin
        {
            get => exinNameVin;
            set
            {
                if (exinNameVin == value) return;
                exinNameVin = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Флаг выхода
        /// </summary>
        public int ExitFlag
        {
            get => exitFlag;
            set
            {
                if (exitFlag == value) return;
                exitFlag = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Виртуальный номер ключа
        /// </summary>
        public int Key
        {
            get => key;
            set
            {
                if (key == value) return;
                key = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Уровень замка
        /// </summary>
        public int LockLevel
        {
            get => lockLevel;
            set
            {
                if (lockLevel == value) return;
                lockLevel = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Виртуальный номер комнаты в которую ведет выход
        /// </summary>
        public int RoomVNum
        {
            get => roomVNum;
            set
            {
                if (roomVNum == value) return;
                roomVNum = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Флаг выхода
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
        /// Состояние двери по умолчанию
        /// </summary>
        public int DoorState
        {
            get => doorDefault;
            set
            {
                if (doorDefault == value) return;
                doorDefault = value;
                FireChangeEvent(this);
            }
        }

        public int Visibility
        {
            get => visibility;
            set
            {
                if (visibility == value) return;
                visibility = value;
                FireChangeEvent(this);
            }
        }

        public Exit Clone()
        {
            var res = new Exit
                          {
                              ConditionFlag = ConditionFlag,
                              Description = Description,
                              DoorState = DoorState,
                              ExitFlag = ExitFlag,
                              Key = Key,
                              Aliases = Aliases,
                              RoomVNum = RoomVNum
                          };
            return res;
        }
    }
}