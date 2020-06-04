namespace BZEditor
{
    partial class UcTwoPanelsList
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.lvLeft = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.lLeft = new System.Windows.Forms.Label();
            this.lvRight = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.lRight = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbRemove = new System.Windows.Forms.ToolStripButton();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.lvLeft);
            this.splitContainer.Panel1.Controls.Add(this.lLeft);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.lvRight);
            this.splitContainer.Panel2.Controls.Add(this.lRight);
            this.splitContainer.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer.Size = new System.Drawing.Size(429, 419);
            this.splitContainer.SplitterDistance = 202;
            this.splitContainer.TabIndex = 2;
            // 
            // lvLeft
            // 
            this.lvLeft.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvLeft.FullRowSelect = true;
            this.lvLeft.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvLeft.Location = new System.Drawing.Point(0, 13);
            this.lvLeft.Name = "lvLeft";
            this.lvLeft.Size = new System.Drawing.Size(200, 404);
            this.lvLeft.TabIndex = 107;
            this.lvLeft.UseCompatibleStateImageBehavior = false;
            this.lvLeft.View = System.Windows.Forms.View.Details;
            this.lvLeft.SelectedIndexChanged += new System.EventHandler(this.LvSelectedIndexChanged);
            this.lvLeft.SizeChanged += new System.EventHandler(this.LastColumnAutosize);
            this.lvLeft.DoubleClick += new System.EventHandler(this.lvLeft_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 193;
            // 
            // lLeft
            // 
            this.lLeft.Dock = System.Windows.Forms.DockStyle.Top;
            this.lLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lLeft.Location = new System.Drawing.Point(0, 0);
            this.lLeft.Name = "lLeft";
            this.lLeft.Size = new System.Drawing.Size(200, 13);
            this.lLeft.TabIndex = 106;
            this.lLeft.Text = "Установленные значения";
            this.lLeft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lvRight
            // 
            this.lvRight.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.lvRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvRight.FullRowSelect = true;
            this.lvRight.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvRight.Location = new System.Drawing.Point(24, 13);
            this.lvRight.Name = "lvRight";
            this.lvRight.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lvRight.Size = new System.Drawing.Size(197, 404);
            this.lvRight.TabIndex = 1;
            this.lvRight.UseCompatibleStateImageBehavior = false;
            this.lvRight.View = System.Windows.Forms.View.Details;
            this.lvRight.SelectedIndexChanged += new System.EventHandler(this.LvSelectedIndexChanged);
            this.lvRight.SizeChanged += new System.EventHandler(this.LastColumnAutosize);
            this.lvRight.DoubleClick += new System.EventHandler(this.lvRight_DoubleClick);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 193;
            // 
            // lRight
            // 
            this.lRight.Dock = System.Windows.Forms.DockStyle.Top;
            this.lRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lRight.Location = new System.Drawing.Point(24, 0);
            this.lRight.Name = "lRight";
            this.lRight.Size = new System.Drawing.Size(197, 13);
            this.lRight.TabIndex = 2;
            this.lRight.Text = "Доступные значения";
            this.lRight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAdd,
            this.tsbRemove});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(24, 417);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbAdd
            // 
            this.tsbAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAdd.Image = global::BZEditor.Properties.Resources.button_add1;
            this.tsbAdd.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Size = new System.Drawing.Size(21, 20);
            this.tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
            // 
            // tsbRemove
            // 
            this.tsbRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRemove.Image = global::BZEditor.Properties.Resources.button_remove1;
            this.tsbRemove.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tsbRemove.Name = "tsbRemove";
            this.tsbRemove.Size = new System.Drawing.Size(21, 20);
            this.tsbRemove.Click += new System.EventHandler(this.tsbRemove_Click);
            // 
            // UcTwoPanelsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Name = "UcTwoPanelsList";
            this.Size = new System.Drawing.Size(429, 419);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            this.splitContainer.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAdd;
        private System.Windows.Forms.ToolStripButton tsbRemove;
        private System.Windows.Forms.Label lRight;
        private System.Windows.Forms.ListView lvRight;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label lLeft;
        private System.Windows.Forms.ListView lvLeft;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}
