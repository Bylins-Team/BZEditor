namespace BZEditor
{
    partial class SketchForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SketchForm));
            this.wldSketch = new ExtControls.WldSketch();
            this.elvMainList = new ExtControls.ExtListView();
            this.chColor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAddZone = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRemoveZone = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolbar = new System.Windows.Forms.ToolStrip();
            this.tsbSaveSketch = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.addZone = new System.Windows.Forms.ToolStripButton();
            this.removeZone = new System.Windows.Forms.ToolStripButton();
            this.stbZoomIn = new System.Windows.Forms.ToolStripButton();
            this.stbZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbZDec = new System.Windows.Forms.ToolStripButton();
            this.tsbMapToCenterRoom = new System.Windows.Forms.ToolStripButton();
            this.tsbZInc = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbGenerateComplex = new System.Windows.Forms.ToolStripButton();
            this.contextMenu.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // wldSketch
            // 
            this.wldSketch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wldSketch.FocusHighlightColor = System.Drawing.SystemColors.ActiveCaption;
            this.wldSketch.GridColor = System.Drawing.SystemColors.ActiveBorder;
            this.wldSketch.Location = new System.Drawing.Point(0, 0);
            this.wldSketch.MapScale = 4;
            this.wldSketch.Name = "wldSketch";
            this.wldSketch.Size = new System.Drawing.Size(500, 469);
            this.wldSketch.SolidGridLines = false;
            this.wldSketch.TabIndex = 0;
            // 
            // elvMainList
            // 
            this.elvMainList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.elvMainList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chColor,
            this.chCount,
            this.chNumber,
            this.chName});
            this.elvMainList.ContextMenuStrip = this.contextMenu;
            this.elvMainList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elvMainList.FullRowSelect = true;
            this.elvMainList.GridLines = true;
            this.elvMainList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.elvMainList.HideSelection = false;
            this.elvMainList.Location = new System.Drawing.Point(0, 0);
            this.elvMainList.MultiSelect = false;
            this.elvMainList.Name = "elvMainList";
            this.elvMainList.OwnerDraw = true;
            this.elvMainList.Size = new System.Drawing.Size(212, 469);
            this.elvMainList.TabIndex = 1;
            this.elvMainList.UseCompatibleStateImageBehavior = false;
            this.elvMainList.View = System.Windows.Forms.View.Details;
            this.elvMainList.SelectedIndexChanged += new System.EventHandler(this.MainListSelectedIndexChanged);
            this.elvMainList.SizeChanged += new System.EventHandler(this.LastColumnAutosize);
            this.elvMainList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.MainListMouseDoubleClick);
            // 
            // chColor
            // 
            this.chColor.Text = "Цвет";
            this.chColor.Width = 39;
            // 
            // chCount
            // 
            this.chCount.Text = "Комнат";
            this.chCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chCount.Width = 50;
            // 
            // chNumber
            // 
            this.chNumber.Text = "Номер";
            this.chNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chNumber.Width = 47;
            // 
            // chName
            // 
            this.chName.Text = "Название зоны";
            this.chName.Width = 100;
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAddZone,
            this.tsmiRemoveZone});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(155, 48);
            // 
            // tsmiAddZone
            // 
            this.tsmiAddZone.Image = global::BZEditor.Properties.Resources.zoneload1;
            this.tsmiAddZone.Name = "tsmiAddZone";
            this.tsmiAddZone.Size = new System.Drawing.Size(154, 22);
            this.tsmiAddZone.Text = "Добавить зону";
            this.tsmiAddZone.Click += new System.EventHandler(this.MenuAddZoneClick);
            // 
            // tsmiRemoveZone
            // 
            this.tsmiRemoveZone.Image = global::BZEditor.Properties.Resources.zoneunload;
            this.tsmiRemoveZone.Name = "tsmiRemoveZone";
            this.tsmiRemoveZone.Size = new System.Drawing.Size(154, 22);
            this.tsmiRemoveZone.Text = "Удалить зону";
            this.tsmiRemoveZone.Click += new System.EventHandler(this.MenuRemoveZoneClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.elvMainList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.wldSketch);
            this.splitContainer1.Size = new System.Drawing.Size(720, 471);
            this.splitContainer1.SplitterDistance = 214;
            this.splitContainer1.TabIndex = 2;
            // 
            // toolbar
            // 
            this.toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSaveSketch,
            this.toolStripSeparator1,
            this.addZone,
            this.removeZone,
            this.stbZoomIn,
            this.stbZoomOut,
            this.toolStripSeparator2,
            this.tsbZDec,
            this.tsbMapToCenterRoom,
            this.tsbZInc,
            this.toolStripSeparator3,
            this.tsbGenerateComplex});
            this.toolbar.Location = new System.Drawing.Point(0, 0);
            this.toolbar.Name = "toolbar";
            this.toolbar.Size = new System.Drawing.Size(720, 25);
            this.toolbar.TabIndex = 3;
            this.toolbar.Text = "toolStrip";
            // 
            // tsbSaveSketch
            // 
            this.tsbSaveSketch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSaveSketch.Image = global::BZEditor.Properties.Resources.button_save;
            this.tsbSaveSketch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveSketch.Name = "tsbSaveSketch";
            this.tsbSaveSketch.Size = new System.Drawing.Size(23, 22);
            this.tsbSaveSketch.Text = "Сохранить эскиз";
            this.tsbSaveSketch.Click += new System.EventHandler(this.SaveSketchClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // addZone
            // 
            this.addZone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addZone.Image = global::BZEditor.Properties.Resources.zoneload1;
            this.addZone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addZone.Name = "addZone";
            this.addZone.Size = new System.Drawing.Size(23, 22);
            this.addZone.Text = "Добавить зону";
            this.addZone.Click += new System.EventHandler(this.AddZoneClick);
            // 
            // removeZone
            // 
            this.removeZone.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.removeZone.Image = global::BZEditor.Properties.Resources.zoneunload;
            this.removeZone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removeZone.Name = "removeZone";
            this.removeZone.Size = new System.Drawing.Size(23, 22);
            this.removeZone.Text = "Удалить зону";
            // 
            // stbZoomIn
            // 
            this.stbZoomIn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.stbZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stbZoomIn.Image = global::BZEditor.Properties.Resources.ZoomIn;
            this.stbZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stbZoomIn.Name = "stbZoomIn";
            this.stbZoomIn.Size = new System.Drawing.Size(23, 22);
            this.stbZoomIn.Text = "Приблизить";
            this.stbZoomIn.Click += new System.EventHandler(this.StbZoomInClick);
            // 
            // stbZoomOut
            // 
            this.stbZoomOut.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.stbZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stbZoomOut.Image = global::BZEditor.Properties.Resources.ZoomOut;
            this.stbZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stbZoomOut.Name = "stbZoomOut";
            this.stbZoomOut.Size = new System.Drawing.Size(23, 22);
            this.stbZoomOut.Text = "Удалить";
            this.stbZoomOut.Click += new System.EventHandler(this.StbZoomOutClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbZDec
            // 
            this.tsbZDec.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbZDec.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZDec.Image = global::BZEditor.Properties.Resources.button_zdec;
            this.tsbZDec.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZDec.Name = "tsbZDec";
            this.tsbZDec.Size = new System.Drawing.Size(23, 22);
            this.tsbZDec.Text = "На уровень ниже по оcи Z";
            this.tsbZDec.Click += new System.EventHandler(this.TsbZDecClick);
            // 
            // tsbMapToCenterRoom
            // 
            this.tsbMapToCenterRoom.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbMapToCenterRoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbMapToCenterRoom.Image = global::BZEditor.Properties.Resources.button_to0room;
            this.tsbMapToCenterRoom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMapToCenterRoom.Name = "tsbMapToCenterRoom";
            this.tsbMapToCenterRoom.Size = new System.Drawing.Size(23, 22);
            this.tsbMapToCenterRoom.Text = "К центру зоны";
            this.tsbMapToCenterRoom.Click += new System.EventHandler(this.TsbMapToCenterRoomClick);
            // 
            // tsbZInc
            // 
            this.tsbZInc.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbZInc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZInc.Image = global::BZEditor.Properties.Resources.button_zinc;
            this.tsbZInc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbZInc.Name = "tsbZInc";
            this.tsbZInc.Size = new System.Drawing.Size(23, 22);
            this.tsbZInc.Text = "На уровень выше по оcи Z";
            this.tsbZInc.Click += new System.EventHandler(this.TsbZIncClick);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbGenerateComplex
            // 
            this.tsbGenerateComplex.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbGenerateComplex.Image = global::BZEditor.Properties.Resources.zonengenerate;
            this.tsbGenerateComplex.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbGenerateComplex.Name = "tsbGenerateComplex";
            this.tsbGenerateComplex.Size = new System.Drawing.Size(23, 22);
            this.tsbGenerateComplex.Text = "Создать комплекс зон";
            this.tsbGenerateComplex.Click += new System.EventHandler(this.GenerateComplexClick);
            // 
            // SketchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 496);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolbar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SketchForm";
            this.TabText = "SketchForm";
            this.Text = "Комплекс зон:";
            this.contextMenu.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.toolbar.ResumeLayout(false);
            this.toolbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ExtControls.WldSketch wldSketch;
        private ExtControls.ExtListView elvMainList;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip toolbar;
        private System.Windows.Forms.ToolStripButton addZone;
        private System.Windows.Forms.ToolStripButton removeZone;
        private System.Windows.Forms.ToolStripButton tsbSaveSketch;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbZInc;
        private System.Windows.Forms.ToolStripButton tsbZDec;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton stbZoomIn;
        private System.Windows.Forms.ToolStripButton stbZoomOut;
        private System.Windows.Forms.ToolStripButton tsbMapToCenterRoom;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbGenerateComplex;
        private System.Windows.Forms.ColumnHeader chColor;
        private System.Windows.Forms.ColumnHeader chNumber;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddZone;
        private System.Windows.Forms.ToolStripMenuItem tsmiRemoveZone;
        private System.Windows.Forms.ColumnHeader chCount;
    }
}