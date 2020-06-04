namespace BZEditor
{
    partial class DropObjectParamsForm
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
            this.nudProbability = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudProbability)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(165, 33);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(84, 33);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Вероятность загрузки в комнату";
            // 
            // nudProbability
            // 
            this.nudProbability.Location = new System.Drawing.Point(183, 7);
            this.nudProbability.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudProbability.Name = "nudProbability";
            this.nudProbability.Size = new System.Drawing.Size(57, 20);
            this.nudProbability.TabIndex = 4;
            this.nudProbability.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // DropObjectParamsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 60);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudProbability);
            this.Name = "DropObjectParamsForm";
            this.Text = "Параметры загрузки объекта";
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.nudProbability, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.nudProbability)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudProbability;
    }
}