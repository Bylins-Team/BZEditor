using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace BZEditor
{
    public partial class CreateZoneForm : BaseDialog
    {
        public CreateZoneForm()
        {
            InitializeComponent();
        }

        public int ZoneNum
        {
            get { return Convert.ToInt32(nbCount.Value); }
        }

        public bool OpenNewZone
        {
            get { return cbOpenCreatedZone.Checked; }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void nbCount_TextChanged(object sender, EventArgs e)
        {
            btnCreate.Enabled = (nbCount.Text != "");
        }

        private void flcAlert_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(
                "mailto:belobog@mud.ru?subject=Жажду билдить, дайте номер&body=Ну дайте же мне скорее номер зоны!!!НУ ПАЖАЛАСТА!!!Я такой ленивый что даже не написал ни строчки сам");
        }
    }
}