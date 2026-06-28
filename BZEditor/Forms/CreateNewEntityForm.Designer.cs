namespace BZEditor
{
    partial class CreateNewEntityForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateNewEntityForm));
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbUseTemplate = new System.Windows.Forms.CheckBox();
            this.cboxTemplate = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nbCount = new System.Windows.Forms.NumericUpDown();
            this.SuspendLayout();
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.Location = new System.Drawing.Point(110, 66);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 0;
            this.btnCreate.Text = "Создать";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.BtnCreateClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(190, 66);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
            // 
            // cbUseTemplate
            // 
            this.cbUseTemplate.AutoSize = true;
            this.cbUseTemplate.Location = new System.Drawing.Point(5, 25);
            this.cbUseTemplate.Name = "cbUseTemplate";
            this.cbUseTemplate.Size = new System.Drawing.Size(111, 17);
            this.cbUseTemplate.TabIndex = 5;
            this.cbUseTemplate.Text = "Выбрать шаблон";
            this.cbUseTemplate.UseVisualStyleBackColor = true;
            this.cbUseTemplate.CheckedChanged += new System.EventHandler(this.CbUseTemplateCheckedChanged);
            // 
            // cboxTemplate
            // 
            this.cboxTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxTemplate.Enabled = false;
            this.cboxTemplate.FormattingEnabled = true;
            this.cboxTemplate.Location = new System.Drawing.Point(5, 42);
            this.cboxTemplate.Name = "cboxTemplate";
            this.cboxTemplate.Size = new System.Drawing.Size(260, 21);
            this.cboxTemplate.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(242, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Количество создаваемых объектов от 1 до 98";
            // 
            // nbCount
            // 
            this.nbCount.DecimalPlaces = 0;
            this.nbCount.Location = new System.Drawing.Point(243, 7);
            this.nbCount.Name = "nbCount";
            this.nbCount.Size = new System.Drawing.Size(22, 17);
            this.nbCount.TabIndex = 4;
            this.nbCount.Text = "1";
            this.nbCount.TextChanged += new System.EventHandler(this.NbCountTextChanged);
            // 
            // CreateNewEntityForm
            // 
            this.AcceptButton = this.btnCreate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(270, 92);
            this.Controls.Add(this.nbCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboxTemplate);
            this.Controls.Add(this.cbUseTemplate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(286, 126);
            this.Name = "CreateNewEntityForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Добавление мобов";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox cbUseTemplate;
        private System.Windows.Forms.ComboBox cboxTemplate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nbCount;
    }
}