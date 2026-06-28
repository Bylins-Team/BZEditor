namespace BZEditor
{
    partial class CreateSketchZoneForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateSketchZoneForm));
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nbCount = new System.Windows.Forms.NumericUpDown();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.flcAlert = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.tbZoneName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSketchColor = new System.Windows.Forms.Button();
            this.btnGenerateRndColor = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.Location = new System.Drawing.Point(214, 113);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 4;
            this.btnCreate.Text = "Добавить";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(294, 113);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Номер зоны";
            // 
            // nbCount
            // 
            this.nbCount.DecimalPlaces = 0;
            this.nbCount.Location = new System.Drawing.Point(77, 43);
            this.nbCount.Name = "nbCount";
            this.nbCount.Size = new System.Drawing.Size(54, 17);
            this.nbCount.TabIndex = 0;
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
            this.flcAlert.BackColor = System.Drawing.SystemColors.Control;
            this.flcAlert.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flcAlert.LinkColor = System.Drawing.Color.Blue;
            this.flcAlert.Location = new System.Drawing.Point(45, 4);
            this.flcAlert.Name = "flcAlert";
            this.flcAlert.Size = new System.Drawing.Size(244, 36);
            this.flcAlert.TabIndex = 7;
            this.flcAlert.Text = "Перед созданием зоны свяжитесь с <a href=\'http://www.mud.ru\'><b>Белобогом</b></a>" +
                " и получите номер зоны";
            this.flcAlert.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.flcAlert_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Название зоны";
            // 
            // tbZoneName
            // 
            this.tbZoneName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbZoneName.Location = new System.Drawing.Point(93, 62);
            this.tbZoneName.Name = "tbZoneName";
            this.tbZoneName.Size = new System.Drawing.Size(276, 20);
            this.tbZoneName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Цвет для эскиза";
            // 
            // btnSketchColor
            // 
            this.btnSketchColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSketchColor.BackColor = System.Drawing.Color.YellowGreen;
            this.btnSketchColor.Location = new System.Drawing.Point(99, 83);
            this.btnSketchColor.Name = "btnSketchColor";
            this.btnSketchColor.Size = new System.Drawing.Size(83, 23);
            this.btnSketchColor.TabIndex = 2;
            this.btnSketchColor.UseVisualStyleBackColor = false;
            this.btnSketchColor.Click += new System.EventHandler(this.btnSketchColor_Click);
            // 
            // btnGenerateRndColor
            // 
            this.btnGenerateRndColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerateRndColor.Location = new System.Drawing.Point(188, 83);
            this.btnGenerateRndColor.Name = "btnGenerateRndColor";
            this.btnGenerateRndColor.Size = new System.Drawing.Size(181, 23);
            this.btnGenerateRndColor.TabIndex = 3;
            this.btnGenerateRndColor.Text = "Сгенерировать случайный цвет";
            this.btnGenerateRndColor.UseVisualStyleBackColor = true;
            this.btnGenerateRndColor.Click += new System.EventHandler(this.btnGenerateRndColor_Click);
            // 
            // CreateSketchZoneForm
            // 
            this.AcceptButton = this.btnCreate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(374, 139);
            this.Controls.Add(this.btnGenerateRndColor);
            this.Controls.Add(this.btnSketchColor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbZoneName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.flcAlert);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.nbCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(297, 130);
            this.Name = "CreateSketchZoneForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Добавление зоны в эскиз";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nbCount;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel flcAlert;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbZoneName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSketchColor;
        private System.Windows.Forms.Button btnGenerateRndColor;
    }
}