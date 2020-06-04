using System;

namespace BZEditor
{
    public partial class DropMobParamsForm : BaseDropParamsForm
    {
        public DropMobParamsForm()
        {
            InitializeComponent();
        }

        public int MaxInWorld
        {
            get
            {
                return Convert.ToInt32(nudMaxInWorld.Value);
            }
            set
            {
                if (value < 1) value = 1;
                if (value < MaxInRoom) value = MaxInRoom;
                nudMaxInWorld.Value = value;
            }
        }

        public int MaxInRoom
        {
            get
            {
                return Convert.ToInt32(nudMaxInRoom.Value);
            }
            set
            {
                if (value < 1) value = 1;
                if (value > MaxInWorld) value = MaxInWorld;
                nudMaxInRoom.Value = value;
            }
        }
    }
}
