namespace BZEditor
{
    partial class DropMobParamsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.nudMaxInWorld = new System.Windows.Forms.NumericUpDown();
            this.nudMaxInRoom = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxInWorld)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxInRoom)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(106, 59);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(25, 59);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Максимум в мире";
            // 
            // nudMaxInWorld
            // 
            this.nudMaxInWorld.Location = new System.Drawing.Point(124, 7);
            this.nudMaxInWorld.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudMaxInWorld.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMaxInWorld.Name = "nudMaxInWorld";
            this.nudMaxInWorld.Size = new System.Drawing.Size(57, 20);
            this.nudMaxInWorld.TabIndex = 4;
            this.nudMaxInWorld.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudMaxInRoom
            // 
            this.nudMaxInRoom.Location = new System.Drawing.Point(124, 33);
            this.nudMaxInRoom.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudMaxInRoom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMaxInRoom.Name = "nudMaxInRoom";
            this.nudMaxInRoom.Size = new System.Drawing.Size(57, 20);
            this.nudMaxInRoom.TabIndex = 4;
            this.nudMaxInRoom.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Максимум в комнате";
            // 
            // DropMobParamsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(186, 89);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudMaxInRoom);
            this.Controls.Add(this.nudMaxInWorld);
            this.Name = "DropMobParamsForm";
            this.Text = "Параметры загрузки моба";
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.nudMaxInWorld, 0);
            this.Controls.SetChildIndex(this.nudMaxInRoom, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxInWorld)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxInRoom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudMaxInWorld;
        private System.Windows.Forms.NumericUpDown nudMaxInRoom;
        private System.Windows.Forms.Label label2;
    }
}