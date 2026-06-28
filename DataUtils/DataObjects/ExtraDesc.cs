namespace DataUtils
{
    public class ExtraDesc : BaseDataObject
    {
        private string aliases = "";
        private string description = "";

        public ExtraDesc(string aliases, string description)
        {
            this.aliases = aliases;
            this.description = description;
        }

        /// <summary>
        /// Алиасы доп.описания
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
        /// Текст доп.описания
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
    }
}