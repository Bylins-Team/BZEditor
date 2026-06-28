namespace BZEditor
{
    partial class RoomSelectForm
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
            this.lvMainList = new System.Windows.Forms.ListView();
            this.chMainListVNum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMainListItemName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tboxMainListFilter = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.cbAhowAllRooms = new System.Windows.Forms.CheckBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lvMainList
            // 
            this.lvMainList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvMainList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chMainListVNum,
            this.chMainListItemName});
            this.lvMainList.FullRowSelect = true;
            this.lvMainList.GridLines = true;
            this.lvMainList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvMainList.HideSelection = false;
            this.lvMainList.Location = new System.Drawing.Point(2, 42);
            this.lvMainList.Name = "lvMainList";
            this.lvMainList.ShowGroups = false;
            this.lvMainList.ShowItemToolTips = true;
            this.lvMainList.Size = new System.Drawing.Size(330, 176);
            this.lvMainList.TabIndex = 104;
            this.lvMainList.UseCompatibleStateImageBehavior = false;
            this.lvMainList.View = System.Windows.Forms.View.Details;
            this.lvMainList.SelectedIndexChanged += new System.EventHandler(this.lvMainList_SelectedIndexChanged);
            this.lvMainList.SizeChanged += new System.EventHandler(this.lvMainList_SizeChanged);
            this.lvMainList.DoubleClick += new System.EventHandler(this.lvMainList_DoubleClick);
            // 
            // chMainListVNum
            // 
            this.chMainListVNum.Text = "Номер";
            this.chMainListVNum.Width = 50;
            // 
            // chMainListItemName
            // 
            this.chMainListItemName.Text = "Название";
            this.chMainListItemName.Width = 274;
            // 
            // tboxMainListFilter
            // 
            this.tboxMainListFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxMainListFilter.Location = new System.Drawing.Point(46, 3);
            this.tboxMainListFilter.Name = "tboxMainListFilter";
            this.tboxMainListFilter.Size = new System.Drawing.Size(286, 20);
            this.tboxMainListFilter.TabIndex = 106;
            this.tboxMainListFilter.TextChanged += new System.EventHandler(this.tboxMainListFilter_TextChanged);
            // 
            // label29
            // 
            this.label29.Location = new System.Drawing.Point(-1, 4);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(49, 16);
            this.label29.TabIndex = 105;
            this.label29.Text = "Фильтр";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbAhowAllRooms
            // 
            this.cbAhowAllRooms.AutoSize = true;
            this.cbAhowAllRooms.Location = new System.Drawing.Point(2, 23);
            this.cbAhowAllRooms.Name = "cbAhowAllRooms";
            this.cbAhowAllRooms.Size = new System.Drawing.Size(198, 17);
            this.cbAhowAllRooms.TabIndex = 107;
            this.cbAhowAllRooms.Text = "Отображать комнаты из всех зон";
            this.cbAhowAllRooms.UseVisualStyleBackColor = true;
            this.cbAhowAllRooms.CheckedChanged += new System.EventHandler(this.cbAhowAllRooms_CheckedChanged);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(176, 221);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 108;
            this.btnOk.Text = "Ок";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(257, 221);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 108;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // RoomSelectForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(334, 246);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cbAhowAllRooms);
            this.Controls.Add(this.lvMainList);
            this.Controls.Add(this.tboxMainListFilter);
            this.Controls.Add(this.label29);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(350, 280);
            this.Name = "RoomSelectForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SelectForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvMainList;
        private System.Windows.Forms.ColumnHeader chMainListVNum;
        private System.Windows.Forms.ColumnHeader chMainListItemName;
        private System.Windows.Forms.TextBox tboxMainListFilter;
        public System.Windows.Forms.Label label29;
        private System.Windows.Forms.CheckBox cbAhowAllRooms;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}