using System;
using System.Windows.Forms;
using DataUtils;

namespace BZEditor
{
    public partial class CreateSketchForm : BaseDialog
    {
        public GlobalSketch Sketch { get; set; }

        public CreateSketchForm()
        {
            InitializeComponent();
            Sketch = new GlobalSketch();
        }

        private void CancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void CreateClick(object sender, EventArgs e)
        {
            Sketch.FileName = Utils.PrepareFileName(string.IsNullOrEmpty(tbFileName.Text) ? tbSketchName.Text : tbFileName.Text);
            DialogResult = DialogResult.OK;
        }

        private void SketchNameTextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbSketchName.Text))
            {
                errorProvider.SetError(tbSketchName, "Не задано наименование эскиза");
                return;
            }
            errorProvider.SetError(tbSketchName, "");
            Sketch.Name = tbSketchName.Text;
            if (tbFileName.ReadOnly)
                tbFileName.Text = Utils.PrepareFileName(tbSketchName.Text);
        }

        private void FileNameTextChanged(object sender, EventArgs e)
        {
            if (!tbFileName.ReadOnly && string.IsNullOrEmpty(tbFileName.Text))
            {
                errorProvider.SetError(tbFileName, "Не задано имя файла эскиза");
                return;
            }
            errorProvider.SetError(tbFileName, "");
        }

        private void FileNameMouseDown(object sender, MouseEventArgs e)
        {
            tbFileName.ReadOnly = !tbFileName.ReadOnly;
            if (tbFileName.ReadOnly)
                tbFileName.Text = Utils.PrepareFileName(tbSketchName.Text);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (DialogResult == DialogResult.OK && (string.IsNullOrEmpty(Sketch.Name) || string.IsNullOrEmpty(Sketch.FileName)))
                e.Cancel = true;
            else
                base.OnClosing(e);
        }
    }
}