namespace DataUtils
{
    public class RoomDescription : BaseDataObject
    {
        private string autumnDay = "";
        private bool autumnDayR;
        private string sutumnNight = "";
        private bool sutumnNightR;
        private string day = "";
        private bool dayR;
        private string main = "";
        private string night = "";
        private bool nightR;
        private string springDay = "";
        private bool springDayR;
        private string springNight = "";
        private bool springNightR;
        private string summerDay = "";
        private bool summerDayR;
        private string summerNight = "";
        private bool summerNightR;
        private string winterDay = "";
        private bool winterDayR;
        private string winterNight = "";
        private bool winterNightR;

        /// <summary>
        /// Основное описание комнаты
        /// </summary>
        public string Main
        {
            get => main;
            set
            {
                if (main == value) return;
                main = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Основное описание комнаты днем
        /// </summary>
        public string Day
        {
            get => day;
            set
            {
                if (day == value) return;
                day = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Основное описание комнаты ночью
        /// </summary>
        public string Night
        {
            get => night;
            set
            {
                if (night == value) return;
                night = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Основное описание комнаты зимней ночью
        /// </summary>
        public string WinterNight
        {
            get => winterNight;
            set
            {
                if (winterNight == value) return;
                winterNight = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Основное описание комнаты зимним днем
        /// </summary>
        public string WinterDay
        {
            get => winterDay;
            set
            {
                if (winterDay == value) return;
                winterDay = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Основное описание комнаты весенней ночью
        /// </summary>
        public string SpringNight
        {
            get => springNight;
            set
            {
                if (springNight == value) return;
                springNight = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Основное описание комнаты весенним днем
        /// </summary>
        public string SpringDay
        {
            get => springDay;
            set
            {
                if (springDay == value) return;
                springDay = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Основное описание комнаты летней ночью
        /// </summary>
        public string SummerNight
        {
            get => summerNight;
            set
            {
                if (summerNight == value) return;
                summerNight = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Основное описание комнаты летним днем
        /// </summary>
        public string SummerDay
        {
            get => summerDay;
            set
            {
                if (summerDay == value) return;
                summerDay = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Основное описание комнаты осенней ночью
        /// </summary>
        public string AutumnNight
        {
            get => sutumnNight;
            set
            {
                if (sutumnNight == value) return;
                sutumnNight = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Основное описание комнаты осенним днем
        /// </summary>
        public string AutumnDay
        {
            get => autumnDay;
            set
            {
                if (autumnDay == value) return;
                autumnDay = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Флаг замещения общего описания дневным
        /// </summary>
        public bool DayR
        {
            get => dayR;
            set
            {
                if (dayR == value) return;
                dayR = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Флаг замещения общего описания ночным
        /// </summary>
        public bool NightR
        {
            get => nightR;
            set
            {
                if (nightR == value) return;
                nightR = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Флаг замещения описания ночным зимним
        /// </summary>
        public bool WinterNightR
        {
            get => winterNightR;
            set
            {
                if (winterNightR == value) return;
                winterNightR = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Флаг замещения описания дневным зимним
        /// </summary>
        public bool WinterDayR
        {
            get => winterDayR;
            set
            {
                if (winterDayR == value) return;
                winterDayR = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Флаг замещения описания ночным весенним
        /// </summary>
        public bool SpringNightR
        {
            get => springNightR;
            set
            {
                if (springNightR == value) return;
                springNightR = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Флаг замещения описания дневным весенним
        /// </summary>
        public bool SpringDayR
        {
            get => springDayR;
            set
            {
                if (springDayR == value) return;
                springDayR = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Флаг замещения описания ночным летним
        /// </summary>
        public bool SummerNightR
        {
            get => summerNightR;
            set
            {
                if (summerNightR == value) return;
                summerNightR = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Флаг замещения описания дневным летним
        /// </summary>
        public bool SummerDayR
        {
            get => summerDayR;
            set
            {
                if (summerDayR == value) return;
                summerDayR = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Флаг замещения описания ночным осенним
        /// </summary>
        public bool AutumnNightR
        {
            get => sutumnNightR;
            set
            {
                if (sutumnNightR == value) return;
                sutumnNightR = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Флаг замещения описания дневным осенним
        /// </summary>
        public bool AutumnDayR
        {
            get => autumnDayR;
            set
            {
                if (autumnDayR == value) return;
                autumnDayR = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Возвращает копию
        /// </summary>
        /// <returns>Копия</returns>
        public RoomDescription Clone()
        {
            var res = new RoomDescription
                          {
                              Main = Main,
                              Day = Day,
                              Night = Night,
                              WinterNight = WinterNight,
                              WinterDay = WinterDay,
                              SpringNight = SpringNight,
                              SpringDay = SpringDay,
                              SummerNight = SummerNight,
                              SummerDay = SummerDay,
                              AutumnNight = AutumnNight,
                              AutumnDay = AutumnDay,
                              DayR = DayR,
                              NightR = NightR,
                              WinterNightR = WinterNightR,
                              WinterDayR = WinterDayR,
                              SpringNightR = SpringNightR,
                              SpringDayR = SpringDayR,
                              SummerNightR = SummerNightR,
                              SummerDayR = SummerDayR,
                              AutumnNightR = AutumnNightR,
                              AutumnDayR = AutumnDayR
                          };

            return res;
        }
    }
}