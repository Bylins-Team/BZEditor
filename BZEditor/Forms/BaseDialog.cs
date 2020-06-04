using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace BZEditor
{
    public partial class BaseDialog : Form
    {
        private readonly Configuration config;

        public BaseDialog()
        {
            InitializeComponent();
            config = new Configuration(Path.Combine(Application.StartupPath, @"Configurations\CommonConfig.xml"));
            config.Open();
        }

        protected override void OnLoad(EventArgs e)
        {
            ApplyConfig();
            base.OnLoad(e);
        }

        private void ApplyConfig()
        {
            Size = config.Read(Name + "Size", Size);
            Location = config.Read(Name + "Location", Location);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            SaveConfig();
            base.OnClosing(e);
        }

        private void SaveConfig()
        {
            if (WindowState == FormWindowState.Normal)
            {
                config.Write(Name + "Size", Size);
                config.Write(Name + "Location", Location);
            }
            config.Save();
        }
    }
}
