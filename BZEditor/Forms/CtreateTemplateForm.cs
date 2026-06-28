using System;
using System.Windows.Forms;

namespace BZEditor
{
    public partial class CtreateTemplateForm : BaseDialog
    {
        public CtreateTemplateForm(string param)
        {
            InitializeComponent();
            tboxTemplateName.Text = param;
        }

        /// <summary>
        /// Обработка хотекеев
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Enter:
                    btnCreate_Click(null, null);
                    return true;
            }
            return false;
        }

        public string TemplateName
        {
            get { return tboxTemplateName.Text; }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (tboxTemplateName.Text == "")
                tboxTemplateName.Text = "Тему впиши, ДА ?";
            else
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}