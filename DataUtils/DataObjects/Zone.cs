namespace DataUtils
{
    public class Zone : BaseDataObject
    {
        private int lastRoomNumber;
        private int level;
        private bool locked;
        private string name = "";
        private string author = "";
        private string comment = "";
        private string location = "";
        private string description = "";
        private int number;
        private int optimalCharsInGroup = 1;
        private int repopTimer = 60;
        private int repopType = 1;
        private int resetIdle;
        private bool test;
        private int type;

        /// <summary>
        /// Коллекция мобов, удаляемух при перезагрузке
        /// </summary>
        public readonly OperatedMobsCollection MobsToRemove = new OperatedMobsCollection();

        /// <summary>
        /// Коллекция мобов, загружаемых в виртуальную комнату
        /// </summary>
        public readonly OperatedMobsCollection MobsLoadedInVirtualRoom = new OperatedMobsCollection();

        public readonly BaseDataArrayList ResetA = new BaseDataArrayList();
        public readonly BaseDataArrayList ResetB = new BaseDataArrayList();

        #region Delegates

        public new delegate void ChangeEvent(object changedClass, object sender);

        #endregion

        public new event ChangeEvent Changed;

        public new virtual void FireChangeEvent(object sender)
        {
            if (Changed != null && StaticData.CanFireChangeEvent)
                Changed(this, sender);
        }

        public Zone()
        {
            MobsToRemove.Changed += FireChangeEvent;
            ResetA.Changed += FireChangeEvent;
            ResetB.Changed += FireChangeEvent;
        }

        /// <summary>
        /// Номер зоны
        /// </summary>
        public int Number
        {
            get => number;
            set
            {
                if (number == value) return;
                number = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Название зоны
        /// </summary>
        public string Name
        {
            get => name;
            set
            {
                if (name == value) return;
                name = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Автор зоны
        /// </summary>
        public string Author
        {
            get => author;
            set
            {
                if (author == value) return;
                author = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment
        {
            get => comment;
            set
            {
                if (comment == value) return;
                comment = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Местоположение
        /// </summary>
        public string Location
        {
            get => location;
            set
            {
                if (location == value) return;
                location = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Описание
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
        /// Номер последней комнаты меньше 98 принудительно
        /// </summary>
        public int LastRoomNumber
        {
            get => lastRoomNumber;
            set
            {
                if (lastRoomNumber == value) return;
                lastRoomNumber = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Уровень зоны
        /// </summary>
        public int Level
        {
            get => level;
            set
            {
                if (level == value) return;
                level = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Тип зоны
        /// </summary>
        public int Type
        {
            get => type;
            set
            {
                if (type == value) return;
                type = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Оптимальное количество игроков в группе для групповых зон
        /// </summary>
        public int OptimalCharsInGroup
        {
            get => optimalCharsInGroup;
            set
            {
                if (optimalCharsInGroup == value) return;
                optimalCharsInGroup = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Тип перезагрузки зоны
        /// </summary>
        public int RepopType
        {
            get => repopType;
            set
            {
                if (repopType == value) return;
                repopType = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Булева переменная, показывающая,очищать ли зону, если в нее никто не заходил после последней ее очистки.
        /// </summary>
        public int ResetIdle
        {
            get => resetIdle;
            set
            {
                if (resetIdle == value) return;
                resetIdle = value;
                FireChangeEvent(this);
            }
        }

        public bool Test
        {
            get => test;
            set
            {
                if (test == value) return;
                test = value;
                FireChangeEvent(this);
            }
        }

        public bool Locked
        {
            get => locked;
            set
            {
                if (locked == value) return;
                locked = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Таймер перезагрузки зоны
        /// </summary>
        public int RepopTimer
        {
            get => repopTimer;
            set
            {
                if (repopTimer == value) return;
                repopTimer = value;
                FireChangeEvent(this);
            }
        }
    }
}