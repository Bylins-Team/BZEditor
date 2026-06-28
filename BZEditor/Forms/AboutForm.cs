using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace BZEditor
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        public string Version
        {
            get { return lVers.Text; }
            set { lVers.Text = value; }
        }

        private void CreateZoneForm_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void llMailToMe_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("mailto:screamu@pisem.net?subject=BZEditor&body=BZ Editor " + lVers + "\r\n");
        }
    }
}