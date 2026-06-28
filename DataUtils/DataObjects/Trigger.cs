namespace DataUtils
{
    public class Trigger : BaseDataObject
    {
        private string arg = string.Empty;
        private string body = string.Empty;
        private int triggerClass;
        private string name = string.Empty;
        private int numArg;
        private string type = string.Empty;

        public Trigger(int vNum)
        {
            VNum = vNum;
        }

        /// <summary>
        /// Текст триггера
        /// </summary>
        public string Body
        {
            get => body;
            set
            {
                if (body == value) return;
                body = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Наименование триггера
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
        /// Класс триггера
        /// </summary>
        public int Class
        {
            get => triggerClass;
            set
            {
                if (triggerClass == value) return;
                triggerClass = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Тип
        /// </summary>
        public string Type
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
        /// Числовой аргумент
        /// </summary>
        public int NumArg
        {
            get => numArg;
            set
            {
                if (numArg == value) return;
                numArg = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Строковый аргумент
        /// </summary>
        public string Arg
        {
            get => arg;
            set
            {
                if (arg == value) return;
                arg = value;
                FireChangeEvent(this);
            }
        }

        public override string ToString()
        {
            return string.Format("[{0}] {1}", VNum, name);
        }
    }
}