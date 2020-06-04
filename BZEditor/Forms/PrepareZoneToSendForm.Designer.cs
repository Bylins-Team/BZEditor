namespace BZEditor
{
    partial class PrepareZoneToSendForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrepareZoneToSendForm));
            this.lInfo = new System.Windows.Forms.Label();
            this.bContinuePreparing = new System.Windows.Forms.Button();
            this.bCancelPreparing = new System.Windows.Forms.Button();
            this.tbArcName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.lStatus = new System.Windows.Forms.Label();
            this.cbMapIncluding = new System.Windows.Forms.CheckBox();
            this.cbSktIncluding = new System.Windows.Forms.CheckBox();
            this.listBoxOutput = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // lInfo
            // 
            this.lInfo.AutoSize = true;
            this.lInfo.Location = new System.Drawing.Point(2, 1);
            this.lInfo.Name = "lInfo";
            this.lInfo.Size = new System.Drawing.Size(38, 13);
            this.lInfo.TabIndex = 0;
            this.lInfo.Text = "Зона: ";
            this.lInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bContinuePreparing
            // 
            this.bContinuePreparing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bContinuePreparing.Location = new System.Drawing.Point(73, 222);
            this.bContinuePreparing.Name = "bContinuePreparing";
            this.bContinuePreparing.Size = new System.Drawing.Size(179, 23);
            this.bContinuePreparing.TabIndex = 1;
            this.bContinuePreparing.Tag = "0";
            this.bContinuePreparing.Text = "Продолжить >>";
            this.bContinuePreparing.UseVisualStyleBackColor = true;
            this.bContinuePreparing.Click += new System.EventHandler(this.bContinuePreparing_Click);
            // 
            // bCancelPreparing
            // 
            this.bCancelPreparing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancelPreparing.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancelPreparing.Location = new System.Drawing.Point(254, 222);
            this.bCancelPreparing.Name = "bCancelPreparing";
            this.bCancelPreparing.Size = new System.Drawing.Size(74, 23);
            this.bCancelPreparing.TabIndex = 2;
            this.bCancelPreparing.Tag = "0";
            this.bCancelPreparing.Text = "Отмена";
            this.bCancelPreparing.UseVisualStyleBackColor = true;
            this.bCancelPreparing.Click += new System.EventHandler(this.bCancelPreparing_Click);
            // 
            // tbArcName
            // 
            this.tbArcName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.errorProvider.SetIconAlignment(this.tbArcName, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.errorProvider.SetIconPadding(this.tbArcName, -18);
            this.tbArcName.Location = new System.Drawing.Point(5, 33);
            this.tbArcName.Name = "tbArcName";
            this.tbArcName.Size = new System.Drawing.Size(323, 20);
            this.tbArcName.TabIndex = 4;
            this.tbArcName.TextChanged += new System.EventHandler(this.tbArcName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Название архива:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // lStatus
            // 
            this.lStatus.AutoSize = true;
            this.lStatus.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lStatus.Location = new System.Drawing.Point(2, 93);
            this.lStatus.Name = "lStatus";
            this.lStatus.Size = new System.Drawing.Size(153, 13);
            this.lStatus.TabIndex = 5;
            this.lStatus.Text = "Статус: Выбор имени архива";
            this.lStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbMapIncluding
            // 
            this.cbMapIncluding.AutoSize = true;
            this.cbMapIncluding.Checked = true;
            this.cbMapIncluding.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMapIncluding.Location = new System.Drawing.Point(5, 59);
            this.cbMapIncluding.Name = "cbMapIncluding";
            this.cbMapIncluding.Size = new System.Drawing.Size(253, 17);
            this.cbMapIncluding.TabIndex = 6;
            this.cbMapIncluding.Text = "Включая файлы расположения комнат (map)";
            this.cbMapIncluding.UseVisualStyleBackColor = true;
            // 
            // cbSktIncluding
            // 
            this.cbSktIncluding.AutoSize = true;
            this.cbSktIncluding.Location = new System.Drawing.Point(5, 75);
            this.cbSktIncluding.Name = "cbSktIncluding";
            this.cbSktIncluding.Size = new System.Drawing.Size(178, 17);
            this.cbSktIncluding.TabIndex = 6;
            this.cbSktIncluding.Text = "Включая файлы эскизов (skt) ";
            this.cbSktIncluding.UseVisualStyleBackColor = true;
            // 
            // listBoxOutput
            // 
            this.listBoxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxOutput.FormattingEnabled = true;
            this.listBoxOutput.HorizontalScrollbar = true;
            this.listBoxOutput.Location = new System.Drawing.Point(5, 110);
            this.listBoxOutput.Name = "listBoxOutput";
            this.listBoxOutput.ScrollAlwaysVisible = true;
            this.listBoxOutput.Size = new System.Drawing.Size(323, 108);
            this.listBoxOutput.TabIndex = 7;
            // 
            // PrepareZoneToSendForm
            // 
            this.AcceptButton = this.bContinuePreparing;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancelPreparing;
            this.ClientSize = new System.Drawing.Size(334, 246);
            this.Controls.Add(this.listBoxOutput);
            this.Controls.Add(this.cbSktIncluding);
            this.Controls.Add(this.cbMapIncluding);
            this.Controls.Add(this.lStatus);
            this.Controls.Add(this.bCancelPreparing);
            this.Controls.Add(this.bContinuePreparing);
            this.Controls.Add(this.lInfo);
            this.Controls.Add(this.tbArcName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(350, 280);
            this.Name = "PrepareZoneToSendForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Подготовка архива зоны к отправке";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lInfo;
        private System.Windows.Forms.Button bContinuePreparing;
        private System.Windows.Forms.Button bCancelPreparing;
        private System.Windows.Forms.TextBox tbArcName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Label lStatus;
        private System.Windows.Forms.CheckBox cbMapIncluding;
        private System.Windows.Forms.CheckBox cbSktIncluding;
        private System.Windows.Forms.ListBox listBoxOutput;
    }
}