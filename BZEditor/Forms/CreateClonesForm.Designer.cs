namespace BZEditor
{
    partial class CreateClonesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateClonesForm));
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbChangeName = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nbCount = new System.Windows.Forms.NumericUpDown();
            this.tbNewName = new System.Windows.Forms.TextBox();
            this.cboxFullCopy = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.Location = new System.Drawing.Point(97, 84);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(83, 23);
            this.btnCreate.TabIndex = 0;
            this.btnCreate.Text = "Клонировать";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.BtnCreateClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(185, 84);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
            // 
            // cbChangeName
            // 
            this.cbChangeName.AutoSize = true;
            this.cbChangeName.Location = new System.Drawing.Point(5, 40);
            this.cbChangeName.Name = "cbChangeName";
            this.cbChangeName.Size = new System.Drawing.Size(128, 17);
            this.cbChangeName.TabIndex = 4;
            this.cbChangeName.Text = "Изменить название";
            this.cbChangeName.UseVisualStyleBackColor = true;
            this.cbChangeName.CheckedChanged += new System.EventHandler(this.CbChangeNameCheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Количество создаваемых клонов от 1 до 98";
            // 
            // nbCount
            // 
            this.nbCount.DecimalPlaces = 0;
            this.nbCount.Location = new System.Drawing.Point(234, 8);
            this.nbCount.Name = "nbCount";
            this.nbCount.Size = new System.Drawing.Size(22, 17);
            this.nbCount.TabIndex = 2;
            this.nbCount.Text = "1";
            this.nbCount.TextChanged += new System.EventHandler(this.NbCountTextChanged);
            // 
            // tbNewName
            // 
            this.tbNewName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNewName.Location = new System.Drawing.Point(5, 58);
            this.tbNewName.Name = "tbNewName";
            this.tbNewName.ReadOnly = true;
            this.tbNewName.Size = new System.Drawing.Size(255, 20);
            this.tbNewName.TabIndex = 5;
            // 
            // cboxFullCopy
            // 
            this.cboxFullCopy.AutoSize = true;
            this.cboxFullCopy.Location = new System.Drawing.Point(5, 24);
            this.cboxFullCopy.Name = "cboxFullCopy";
            this.cboxFullCopy.Size = new System.Drawing.Size(95, 17);
            this.cboxFullCopy.TabIndex = 3;
            this.cboxFullCopy.Text = "Точная копия";
            this.cboxFullCopy.UseVisualStyleBackColor = true;
            // 
            // CreateClonesForm
            // 
            this.AcceptButton = this.btnCreate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(264, 110);
            this.Controls.Add(this.tbNewName);
            this.Controls.Add(this.nbCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboxFullCopy);
            this.Controls.Add(this.cbChangeName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(280, 144);
            this.Name = "CreateClonesForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Клонирование";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox cbChangeName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nbCount;
        private System.Windows.Forms.TextBox tbNewName;
        private System.Windows.Forms.CheckBox cboxFullCopy;
    }
}