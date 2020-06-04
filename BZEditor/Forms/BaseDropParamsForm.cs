using System.Windows.Forms;

namespace BZEditor
{
    public partial class BaseDropParamsForm : Form
    {
        public BaseDropParamsForm()
        {
            InitializeComponent();
            btnOk.Focus();
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
