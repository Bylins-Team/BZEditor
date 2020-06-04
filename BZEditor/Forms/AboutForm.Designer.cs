namespace BZEditor
{
    partial class AboutForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lVers = new System.Windows.Forms.Label();
            this.llMailToMe = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::BZEditor.Properties.Resources.ktip;
            this.pictureBox1.Location = new System.Drawing.Point(2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(62, 70);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // lVers
            // 
            this.lVers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lVers.BackColor = System.Drawing.Color.Transparent;
            this.lVers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lVers.ForeColor = System.Drawing.Color.White;
            this.lVers.Location = new System.Drawing.Point(70, 29);
            this.lVers.Name = "lVers";
            this.lVers.Size = new System.Drawing.Size(228, 17);
            this.lVers.TabIndex = 6;
            this.lVers.Text = "v.";
            this.lVers.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // llMailToMe
            // 
            this.llMailToMe.AutoSize = true;
            this.llMailToMe.BackColor = System.Drawing.Color.Transparent;
            this.llMailToMe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.llMailToMe.LinkColor = System.Drawing.Color.White;
            this.llMailToMe.Location = new System.Drawing.Point(-1, 174);
            this.llMailToMe.Name = "llMailToMe";
            this.llMailToMe.Size = new System.Drawing.Size(161, 13);
            this.llMailToMe.TabIndex = 7;
            this.llMailToMe.TabStop = true;
            this.llMailToMe.Text = "mail to screamu@pisem.net";
            this.llMailToMe.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llMailToMe_LinkClicked);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(224, 171);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 23);
            this.label1.TabIndex = 6;
            this.label1.Text = "Закрыть";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Click += new System.EventHandler(this.CreateZoneForm_Click);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::BZEditor.Properties.Resources.splash;
            this.ClientSize = new System.Drawing.Size(301, 198);
            this.Controls.Add(this.llMailToMe);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lVers);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AboutForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "О программе...";
            this.Click += new System.EventHandler(this.CreateZoneForm_Click);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lVers;
        private System.Windows.Forms.LinkLabel llMailToMe;
        private System.Windows.Forms.Label label1;
    }
}