namespace BZEditor
{
    partial class SelectBackDirectionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectBackDirectionForm));
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbFreeExitsOnly = new System.Windows.Forms.CheckBox();
            this.btnConfigExitDown = new System.Windows.Forms.Button();
            this.btnConfigExitSouth = new System.Windows.Forms.Button();
            this.btnConfigExitEast = new System.Windows.Forms.Button();
            this.btnConfigExitWest = new System.Windows.Forms.Button();
            this.btnConfigExitUp = new System.Windows.Forms.Button();
            this.btnConfigExitNorth = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(3, 120);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(187, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbFreeExitsOnly
            // 
            this.cbFreeExitsOnly.AutoSize = true;
            this.cbFreeExitsOnly.Location = new System.Drawing.Point(3, 37);
            this.cbFreeExitsOnly.Name = "cbFreeExitsOnly";
            this.cbFreeExitsOnly.Size = new System.Drawing.Size(190, 17);
            this.cbFreeExitsOnly.TabIndex = 4;
            this.cbFreeExitsOnly.Text = "Только незянятые направления";
            this.cbFreeExitsOnly.UseVisualStyleBackColor = true;
            this.cbFreeExitsOnly.CheckedChanged += new System.EventHandler(this.cbFreeExitsOnly_CheckedChanged);
            // 
            // btnConfigExitDown
            // 
            this.btnConfigExitDown.Location = new System.Drawing.Point(129, 95);
            this.btnConfigExitDown.Name = "btnConfigExitDown";
            this.btnConfigExitDown.Size = new System.Drawing.Size(60, 20);
            this.btnConfigExitDown.TabIndex = 115;
            this.btnConfigExitDown.Tag = "Down";
            this.btnConfigExitDown.Text = "Низ";
            this.btnConfigExitDown.UseVisualStyleBackColor = true;
            this.btnConfigExitDown.Click += new System.EventHandler(this.btnDir_Click);
            // 
            // btnConfigExitSouth
            // 
            this.btnConfigExitSouth.Location = new System.Drawing.Point(47, 95);
            this.btnConfigExitSouth.Name = "btnConfigExitSouth";
            this.btnConfigExitSouth.Size = new System.Drawing.Size(60, 20);
            this.btnConfigExitSouth.TabIndex = 116;
            this.btnConfigExitSouth.Tag = "South";
            this.btnConfigExitSouth.Text = "Юг";
            this.btnConfigExitSouth.UseVisualStyleBackColor = true;
            this.btnConfigExitSouth.Click += new System.EventHandler(this.btnDir_Click);
            // 
            // btnConfigExitEast
            // 
            this.btnConfigExitEast.Location = new System.Drawing.Point(87, 75);
            this.btnConfigExitEast.Name = "btnConfigExitEast";
            this.btnConfigExitEast.Size = new System.Drawing.Size(60, 20);
            this.btnConfigExitEast.TabIndex = 117;
            this.btnConfigExitEast.Tag = "East";
            this.btnConfigExitEast.Text = "Восток";
            this.btnConfigExitEast.UseVisualStyleBackColor = true;
            this.btnConfigExitEast.Click += new System.EventHandler(this.btnDir_Click);
            // 
            // btnConfigExitWest
            // 
            this.btnConfigExitWest.Location = new System.Drawing.Point(3, 75);
            this.btnConfigExitWest.Name = "btnConfigExitWest";
            this.btnConfigExitWest.Size = new System.Drawing.Size(60, 20);
            this.btnConfigExitWest.TabIndex = 112;
            this.btnConfigExitWest.Tag = "West";
            this.btnConfigExitWest.Text = "Запад";
            this.btnConfigExitWest.UseVisualStyleBackColor = true;
            this.btnConfigExitWest.Click += new System.EventHandler(this.btnDir_Click);
            // 
            // btnConfigExitUp
            // 
            this.btnConfigExitUp.Location = new System.Drawing.Point(129, 55);
            this.btnConfigExitUp.Name = "btnConfigExitUp";
            this.btnConfigExitUp.Size = new System.Drawing.Size(60, 20);
            this.btnConfigExitUp.TabIndex = 113;
            this.btnConfigExitUp.Tag = "Up";
            this.btnConfigExitUp.Text = "Верх";
            this.btnConfigExitUp.UseVisualStyleBackColor = true;
            this.btnConfigExitUp.Click += new System.EventHandler(this.btnDir_Click);
            // 
            // btnConfigExitNorth
            // 
            this.btnConfigExitNorth.Location = new System.Drawing.Point(47, 55);
            this.btnConfigExitNorth.Name = "btnConfigExitNorth";
            this.btnConfigExitNorth.Size = new System.Drawing.Size(60, 20);
            this.btnConfigExitNorth.TabIndex = 114;
            this.btnConfigExitNorth.Tag = "North";
            this.btnConfigExitNorth.Text = "Север";
            this.btnConfigExitNorth.UseVisualStyleBackColor = true;
            this.btnConfigExitNorth.Click += new System.EventHandler(this.btnDir_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox.Location = new System.Drawing.Point(0, 2);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(193, 29);
            this.pictureBox.TabIndex = 118;
            this.pictureBox.TabStop = false;
            // 
            // SelectBackDirectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(193, 146);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.btnConfigExitDown);
            this.Controls.Add(this.btnConfigExitSouth);
            this.Controls.Add(this.btnConfigExitEast);
            this.Controls.Add(this.btnConfigExitWest);
            this.Controls.Add(this.btnConfigExitUp);
            this.Controls.Add(this.btnConfigExitNorth);
            this.Controls.Add(this.cbFreeExitsOnly);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SelectBackDirectionForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Установка втречного выхода";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox cbFreeExitsOnly;
        private System.Windows.Forms.Button btnConfigExitDown;
        private System.Windows.Forms.Button btnConfigExitSouth;
        private System.Windows.Forms.Button btnConfigExitEast;
        private System.Windows.Forms.Button btnConfigExitWest;
        private System.Windows.Forms.Button btnConfigExitUp;
        private System.Windows.Forms.Button btnConfigExitNorth;
        private System.Windows.Forms.PictureBox pictureBox;
    }
}