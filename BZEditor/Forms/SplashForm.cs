using System.ComponentModel;
using System.Windows.Forms;
using ExtControls;

namespace BZEditor
{
    /// <summary>
    /// Summary description for SplashForm.
    /// </summary>
    public class SplashForm : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private readonly Container components = null;

        private ExtProgressBar extSplashProgressBar;
        private Label lVersion;

        public SplashForm()
        {
            InitializeComponent();
        }

        public int Position
        {
            get { return extSplashProgressBar.Position; }
        }

        public string Version
        {
            get { return lVersion.Text; }
            set { lVersion.Text = value; }
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void SetNextState(int position, string text)
        {
            extSplashProgressBar.Position = position;
            extSplashProgressBar.Text = text;
            extSplashProgressBar.Invalidate();
        }

        public void SetNextState(string text)
        {
            extSplashProgressBar.Text = text;
            extSplashProgressBar.Invalidate();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashForm));
            this.extSplashProgressBar = new ExtControls.ExtProgressBar();
            this.lVersion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // extSplashProgressBar
            // 
            this.extSplashProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(163)))), ((int)(((byte)(79)))));
            this.extSplashProgressBar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("extSplashProgressBar.BackgroundImage")));
            this.extSplashProgressBar.ColorBackGround = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(163)))), ((int)(((byte)(79)))));
            this.extSplashProgressBar.ColorBarBorder = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(88)))), ((int)(((byte)(23)))));
            this.extSplashProgressBar.ColorBarCenter = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(200)))), ((int)(((byte)(100)))));
            this.extSplashProgressBar.ColorText = System.Drawing.Color.White;
            this.extSplashProgressBar.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.extSplashProgressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.extSplashProgressBar.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.extSplashProgressBar.Location = new System.Drawing.Point(0, 172);
            this.extSplashProgressBar.Name = "extSplashProgressBar";
            this.extSplashProgressBar.Position = 0;
            this.extSplashProgressBar.PositionMax = 100;
            this.extSplashProgressBar.PositionMin = 0;
            this.extSplashProgressBar.Size = new System.Drawing.Size(300, 25);
            this.extSplashProgressBar.SteepDistance = ((byte)(1));
            this.extSplashProgressBar.TabIndex = 1;
            this.extSplashProgressBar.TabStop = false;
            this.extSplashProgressBar.Text = "Загрузка параметров";
            this.extSplashProgressBar.TextShadowAlpha = ((byte)(255));
            // 
            // lVersion
            // 
            this.lVersion.BackColor = System.Drawing.Color.Transparent;
            this.lVersion.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lVersion.ForeColor = System.Drawing.Color.White;
            this.lVersion.Image = global::BZEditor.Properties.Resources.splash;
            this.lVersion.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lVersion.Location = new System.Drawing.Point(-2, -2);
            this.lVersion.Name = "lVersion";
            this.lVersion.Size = new System.Drawing.Size(302, 198);
            this.lVersion.TabIndex = 2;
            this.lVersion.Text = "\r\n\r\nver. 1.0";
            this.lVersion.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // SplashForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(300, 197);
            this.Controls.Add(this.extSplashProgressBar);
            this.Controls.Add(this.lVersion);
            this.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SplashForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MUDMaperSplashForm";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion
    }
}