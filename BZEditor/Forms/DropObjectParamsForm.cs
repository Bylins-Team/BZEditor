using System;

namespace BZEditor
{
    public partial class DropObjectParamsForm : BaseDropParamsForm
    {
        public DropObjectParamsForm()
        {
            InitializeComponent();
        }

        public int Probability
        {
            get
            {
                return Convert.ToInt32(nudProbability.Value);
            }
            set
            {
                if (value < 1) value = 1;
                if (value > 100) value = 100;
                nudProbability.Value = value;
            }
        }
    }
}
