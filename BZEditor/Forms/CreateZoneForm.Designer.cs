namespace BZEditor
{
    partial class CreateZoneForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateZoneForm));
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nbCount = new Fireball.Windows.Forms.NumericBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.flcAlert = new Fireball.Windows.Forms.FormatLabelControl();
            this.cbOpenCreatedZone = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.Location = new System.Drawing.Point(131, 80);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 0;
            this.btnCreate.Text = "Создать";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(211, 80);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Номер создаваемой зоны";
            // 
            // nbCount
            // 
            this.nbCount.AllowError = true;
            this.nbCount.DecimalPlace = 0;
            this.nbCount.Location = new System.Drawing.Point(148, 42);
            this.nbCount.Name = "nbCount";
            this.nbCount.Size = new System.Drawing.Size(54, 17);
            this.nbCount.TabIndex = 4;
            this.nbCount.Text = "1";
            this.nbCount.TextChanged += new System.EventHandler(this.nbCount_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::BZEditor.Properties.Resources.alert;
            this.pictureBox1.Location = new System.Drawing.Point(4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(35, 33);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // flcAlert
            // 
            this.flcAlert.AutoSizeHorizontal = false;
            this.flcAlert.AutoSizeVertical = false;
            this.flcAlert.BackColor = System.Drawing.SystemColors.Control;
            this.flcAlert.BorderColor = System.Drawing.SystemColors.Control;
            this.flcAlert.BorderStyle = Fireball.Windows.Forms.ControlBorderStyle.FixedSingle;
            this.flcAlert.ImageList = null;
            this.flcAlert.LabelMargin = 0;
            this.flcAlert.Link_Color = System.Drawing.Color.Blue;
            this.flcAlert.Link_Color_Hover = System.Drawing.Color.Blue;
            this.flcAlert.Link_UnderLine = true;
            this.flcAlert.Link_UnderLine_Hover = true;
            this.flcAlert.Location = new System.Drawing.Point(45, 4);
            this.flcAlert.Name = "flcAlert";
            this.flcAlert.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.flcAlert.Size = new System.Drawing.Size(244, 36);
            this.flcAlert.TabIndex = 7;
            this.flcAlert.Text = "Перед созданием зоны свяжитесь со Стрибогом и получите номер зоны";
            this.flcAlert.WordWrap = true;
            this.flcAlert.ClickLink += new Fireball.Windows.Forms.ClickLinkEventHandler(this.flcAlert_ClickLink);
            // 
            // cbOpenCreatedZone
            // 
            this.cbOpenCreatedZone.AutoSize = true;
            this.cbOpenCreatedZone.Checked = true;
            this.cbOpenCreatedZone.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOpenCreatedZone.Location = new System.Drawing.Point(4, 59);
            this.cbOpenCreatedZone.Name = "cbOpenCreatedZone";
            this.cbOpenCreatedZone.Size = new System.Drawing.Size(261, 17);
            this.cbOpenCreatedZone.TabIndex = 8;
            this.cbOpenCreatedZone.Text = "Открыть созданную зону для редактирования";
            this.cbOpenCreatedZone.UseVisualStyleBackColor = true;
            // 
            // CreateZoneForm
            // 
            this.AcceptButton = this.btnCreate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(291, 106);
            this.Controls.Add(this.cbOpenCreatedZone);
            this.Controls.Add(this.flcAlert);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.nbCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(297, 130);
            this.Name = "CreateZoneForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Добавление зоны";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private Fireball.Windows.Forms.NumericBox nbCount;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Fireball.Windows.Forms.FormatLabelControl flcAlert;
        private System.Windows.Forms.CheckBox cbOpenCreatedZone;
    }
}