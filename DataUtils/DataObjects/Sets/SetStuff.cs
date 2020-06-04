using System;

namespace DataUtils
{
    public class SetStuff : BaseDataObject
    {
        public Guid Guid = Guid.NewGuid();
        private string comment;
        private readonly StuffActivationsCollection activations = new StuffActivationsCollection();

        public SetStuff(int vNum)
        {
            VNum = vNum;
            activations.Changed += FireChangeEvent;
        }

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

        public StuffActivationsCollection Activations => activations;
    }
}