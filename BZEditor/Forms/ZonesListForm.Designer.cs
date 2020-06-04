namespace BZEditor
{
    partial class ZonesListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZonesListForm));
            this.imageListTree = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.tbQuickSearch = new System.Windows.Forms.TextBox();
            this.lvZones = new System.Windows.Forms.ListView();
            this.columnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsLoadedZones = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiEditLoaded = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiUnloadZone = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiSaveZone = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPrepareLoadedToSedind = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tpLoaded = new System.Windows.Forms.TabPage();
            this.tpAvailable = new System.Windows.Forms.TabPage();
            this.lvZonesAvail = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsZonesAvail = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiLoadAndEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLoadZone = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLoadZoneWin = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiRefreshList = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiPrepareAvailToSedind = new System.Windows.Forms.ToolStripMenuItem();
            this.tbQuickSearchAvail = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tpSketches = new System.Windows.Forms.TabPage();
            this.lvSketches = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsSketches = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiEditSketch = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCreateSketch = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiSaveSketch = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiRefreshSketchesList = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiRemoveSketch = new System.Windows.Forms.ToolStripMenuItem();
            this.tbQuickSearchSketch = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmsLoadedZones.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tpLoaded.SuspendLayout();
            this.tpAvailable.SuspendLayout();
            this.cmsZonesAvail.SuspendLayout();
            this.tpSketches.SuspendLayout();
            this.cmsSketches.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageListTree
            // 
            this.imageListTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTree.ImageStream")));
            this.imageListTree.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTree.Images.SetKeyName(0, "zonenormal.png");
            this.imageListTree.Images.SetKeyName(1, "zonenew.png");
            this.imageListTree.Images.SetKeyName(2, "zonenotsaved.png");
            this.imageListTree.Images.SetKeyName(3, "zoneineditmode.png");
            this.imageListTree.Images.SetKeyName(4, "zoneremoved.png");
            this.imageListTree.Images.SetKeyName(5, "available_zone.png");
            this.imageListTree.Images.SetKeyName(6, "sketch.png");
            this.imageListTree.Images.SetKeyName(7, "sketchineditmode.png");
            this.imageListTree.Images.SetKeyName(8, "button_editsketch.png");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Window;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Быстрый поиск зоны";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbQuickSearch
            // 
            this.tbQuickSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbQuickSearch.Location = new System.Drawing.Point(3, 16);
            this.tbQuickSearch.Name = "tbQuickSearch";
            this.tbQuickSearch.Size = new System.Drawing.Size(260, 20);
            this.tbQuickSearch.TabIndex = 3;
            this.tbQuickSearch.TextChanged += new System.EventHandler(this.TbQuickSearchTextChanged);
            this.tbQuickSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TbQuickSearchKeyUp);
            // 
            // lvZones
            // 
            this.lvZones.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader});
            this.lvZones.ContextMenuStrip = this.cmsLoadedZones;
            this.lvZones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvZones.FullRowSelect = true;
            this.lvZones.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvZones.HideSelection = false;
            this.lvZones.Location = new System.Drawing.Point(3, 36);
            this.lvZones.MultiSelect = false;
            this.lvZones.Name = "lvZones";
            this.lvZones.Size = new System.Drawing.Size(260, 364);
            this.lvZones.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvZones.StateImageList = this.imageListTree;
            this.lvZones.TabIndex = 5;
            this.lvZones.UseCompatibleStateImageBehavior = false;
            this.lvZones.View = System.Windows.Forms.View.Details;
            this.lvZones.SizeChanged += new System.EventHandler(this.LvZonesSizeChanged);
            this.lvZones.DoubleClick += new System.EventHandler(this.LvZonesDoubleClick);
            this.lvZones.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LvZonesMouseDown);
            // 
            // cmsLoadedZones
            // 
            this.cmsLoadedZones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEditLoaded,
            this.toolStripMenuItem1,
            this.tsmiUnloadZone,
            this.toolStripMenuItem3,
            this.tsmiSaveZone,
            this.tsmiPrepareLoadedToSedind});
            this.cmsLoadedZones.Name = "cmsZonesAvail";
            this.cmsLoadedZones.Size = new System.Drawing.Size(208, 104);
            // 
            // tsmiEditLoaded
            // 
            this.tsmiEditLoaded.Image = global::BZEditor.Properties.Resources.zoneineditmode;
            this.tsmiEditLoaded.Name = "tsmiEditLoaded";
            this.tsmiEditLoaded.Size = new System.Drawing.Size(207, 22);
            this.tsmiEditLoaded.Text = "Редактировать";
            this.tsmiEditLoaded.Click += new System.EventHandler(this.TsmiEditLoadedClick);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(204, 6);
            // 
            // tsmiUnloadZone
            // 
            this.tsmiUnloadZone.Image = global::BZEditor.Properties.Resources.zoneunload;
            this.tsmiUnloadZone.Name = "tsmiUnloadZone";
            this.tsmiUnloadZone.Size = new System.Drawing.Size(207, 22);
            this.tsmiUnloadZone.Text = "Выгрузить";
            this.tsmiUnloadZone.Click += new System.EventHandler(this.TsmiUnloadZoneClick);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(204, 6);
            // 
            // tsmiSaveZone
            // 
            this.tsmiSaveZone.Image = global::BZEditor.Properties.Resources.zonesave;
            this.tsmiSaveZone.Name = "tsmiSaveZone";
            this.tsmiSaveZone.Size = new System.Drawing.Size(207, 22);
            this.tsmiSaveZone.Text = "Сохранить";
            this.tsmiSaveZone.Click += new System.EventHandler(this.TsmiSaveZoneClick);
            // 
            // tsmiPrepareLoadedToSedind
            // 
            this.tsmiPrepareLoadedToSedind.Image = global::BZEditor.Properties.Resources.zonesend;
            this.tsmiPrepareLoadedToSedind.Name = "tsmiPrepareLoadedToSedind";
            this.tsmiPrepareLoadedToSedind.Size = new System.Drawing.Size(207, 22);
            this.tsmiPrepareLoadedToSedind.Text = "Приготовить к отправке";
            this.tsmiPrepareLoadedToSedind.Click += new System.EventHandler(this.TsmiPrepareLoadedToSedindClick);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tpLoaded);
            this.tabControl.Controls.Add(this.tpAvailable);
            this.tabControl.Controls.Add(this.tpSketches);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.ImageList = this.imageListTree;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(274, 430);
            this.tabControl.TabIndex = 6;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.TabControlSelectedIndexChanged);
            // 
            // tpLoaded
            // 
            this.tpLoaded.Controls.Add(this.lvZones);
            this.tpLoaded.Controls.Add(this.tbQuickSearch);
            this.tpLoaded.Controls.Add(this.label1);
            this.tpLoaded.ImageIndex = 0;
            this.tpLoaded.Location = new System.Drawing.Point(4, 23);
            this.tpLoaded.Name = "tpLoaded";
            this.tpLoaded.Padding = new System.Windows.Forms.Padding(3);
            this.tpLoaded.Size = new System.Drawing.Size(266, 403);
            this.tpLoaded.TabIndex = 0;
            this.tpLoaded.Text = "Загруженные";
            this.tpLoaded.ToolTipText = "Загруженные в память зоны";
            this.tpLoaded.UseVisualStyleBackColor = true;
            // 
            // tpAvailable
            // 
            this.tpAvailable.Controls.Add(this.lvZonesAvail);
            this.tpAvailable.Controls.Add(this.tbQuickSearchAvail);
            this.tpAvailable.Controls.Add(this.label2);
            this.tpAvailable.ImageIndex = 5;
            this.tpAvailable.Location = new System.Drawing.Point(4, 23);
            this.tpAvailable.Name = "tpAvailable";
            this.tpAvailable.Padding = new System.Windows.Forms.Padding(3);
            this.tpAvailable.Size = new System.Drawing.Size(266, 403);
            this.tpAvailable.TabIndex = 1;
            this.tpAvailable.Text = "Доступные";
            this.tpAvailable.ToolTipText = "Доступные для загрузки зоны";
            this.tpAvailable.UseVisualStyleBackColor = true;
            // 
            // lvZonesAvail
            // 
            this.lvZonesAvail.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvZonesAvail.ContextMenuStrip = this.cmsZonesAvail;
            this.lvZonesAvail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvZonesAvail.FullRowSelect = true;
            this.lvZonesAvail.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvZonesAvail.HideSelection = false;
            this.lvZonesAvail.Location = new System.Drawing.Point(3, 36);
            this.lvZonesAvail.MultiSelect = false;
            this.lvZonesAvail.Name = "lvZonesAvail";
            this.lvZonesAvail.Size = new System.Drawing.Size(260, 364);
            this.lvZonesAvail.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvZonesAvail.StateImageList = this.imageListTree;
            this.lvZonesAvail.TabIndex = 8;
            this.lvZonesAvail.UseCompatibleStateImageBehavior = false;
            this.lvZonesAvail.View = System.Windows.Forms.View.Details;
            this.lvZonesAvail.SizeChanged += new System.EventHandler(this.LvZonesAvailSizeChanged);
            this.lvZonesAvail.DoubleClick += new System.EventHandler(this.LvZonesAvailDoubleClick);
            // 
            // cmsZonesAvail
            // 
            this.cmsZonesAvail.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiLoadAndEdit,
            this.tsmiLoadZone,
            this.tsmiLoadZoneWin,
            this.toolStripMenuItem4,
            this.tsmiRefreshList,
            this.toolStripMenuItem2,
            this.tsmiPrepareAvailToSedind});
            this.cmsZonesAvail.Name = "cmsZonesAvail";
            this.cmsZonesAvail.Size = new System.Drawing.Size(207, 126);
            // 
            // tsmiLoadAndEdit
            // 
            this.tsmiLoadAndEdit.Image = global::BZEditor.Properties.Resources.zoneineditmode;
            this.tsmiLoadAndEdit.Name = "tsmiLoadAndEdit";
            this.tsmiLoadAndEdit.Size = new System.Drawing.Size(206, 22);
            this.tsmiLoadAndEdit.Text = "Редактировать";
            this.tsmiLoadAndEdit.Click += new System.EventHandler(this.TsmiLoadAndEditClick);
            // 
            // tsmiLoadZone
            // 
            this.tsmiLoadZone.Image = global::BZEditor.Properties.Resources.zoneload1;
            this.tsmiLoadZone.Name = "tsmiLoadZone";
            this.tsmiLoadZone.Size = new System.Drawing.Size(206, 22);
            this.tsmiLoadZone.Text = "Загрузить";
            this.tsmiLoadZone.Click += new System.EventHandler(this.TsmiLoadZoneClick);
            // 
            // tsmiLoadZoneWin
            // 
            this.tsmiLoadZoneWin.Image = global::BZEditor.Properties.Resources.zoneload1251;
            this.tsmiLoadZoneWin.Name = "tsmiLoadZoneWin";
            this.tsmiLoadZoneWin.Size = new System.Drawing.Size(206, 22);
            this.tsmiLoadZoneWin.Text = "Загрузить в Win1251";
            this.tsmiLoadZoneWin.Click += new System.EventHandler(this.TsmiLoadZoneWinClick);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(203, 6);
            // 
            // tsmiRefreshList
            // 
            this.tsmiRefreshList.Image = global::BZEditor.Properties.Resources.zoneslistrefresh;
            this.tsmiRefreshList.Name = "tsmiRefreshList";
            this.tsmiRefreshList.Size = new System.Drawing.Size(206, 22);
            this.tsmiRefreshList.Text = "Обновить список";
            this.tsmiRefreshList.Click += new System.EventHandler(this.TsmiRefreshListClick);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(203, 6);
            // 
            // tsmiPrepareAvailToSedind
            // 
            this.tsmiPrepareAvailToSedind.Image = global::BZEditor.Properties.Resources.zonesend;
            this.tsmiPrepareAvailToSedind.Name = "tsmiPrepareAvailToSedind";
            this.tsmiPrepareAvailToSedind.Size = new System.Drawing.Size(206, 22);
            this.tsmiPrepareAvailToSedind.Text = "Подготовить к отправке";
            this.tsmiPrepareAvailToSedind.Click += new System.EventHandler(this.TsmiPrepareAvailToSedindClick);
            // 
            // tbQuickSearchAvail
            // 
            this.tbQuickSearchAvail.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbQuickSearchAvail.Location = new System.Drawing.Point(3, 16);
            this.tbQuickSearchAvail.Name = "tbQuickSearchAvail";
            this.tbQuickSearchAvail.Size = new System.Drawing.Size(260, 20);
            this.tbQuickSearchAvail.TabIndex = 6;
            this.tbQuickSearchAvail.TextChanged += new System.EventHandler(this.TbQuickSearchAvailTextChanged);
            this.tbQuickSearchAvail.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TbQuickSearchAvailKeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Window;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Быстрый поиск зоны";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tpSketches
            // 
            this.tpSketches.Controls.Add(this.lvSketches);
            this.tpSketches.Controls.Add(this.tbQuickSearchSketch);
            this.tpSketches.Controls.Add(this.label3);
            this.tpSketches.ImageIndex = 6;
            this.tpSketches.Location = new System.Drawing.Point(4, 23);
            this.tpSketches.Name = "tpSketches";
            this.tpSketches.Padding = new System.Windows.Forms.Padding(3);
            this.tpSketches.Size = new System.Drawing.Size(266, 403);
            this.tpSketches.TabIndex = 2;
            this.tpSketches.Text = "Эскизы";
            this.tpSketches.ToolTipText = "Эскизы комплексов зон";
            this.tpSketches.UseVisualStyleBackColor = true;
            // 
            // lvSketches
            // 
            this.lvSketches.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.lvSketches.ContextMenuStrip = this.cmsSketches;
            this.lvSketches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSketches.FullRowSelect = true;
            this.lvSketches.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvSketches.HideSelection = false;
            this.lvSketches.Location = new System.Drawing.Point(3, 36);
            this.lvSketches.MultiSelect = false;
            this.lvSketches.Name = "lvSketches";
            this.lvSketches.Size = new System.Drawing.Size(260, 364);
            this.lvSketches.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvSketches.StateImageList = this.imageListTree;
            this.lvSketches.TabIndex = 8;
            this.lvSketches.UseCompatibleStateImageBehavior = false;
            this.lvSketches.View = System.Windows.Forms.View.Details;
            this.lvSketches.SizeChanged += new System.EventHandler(this.LvSketchesSizeChanged);
            this.lvSketches.DoubleClick += new System.EventHandler(this.LvSketchesDoubleClick);
            this.lvSketches.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LvSketchesMouseDown);
            // 
            // cmsSketches
            // 
            this.cmsSketches.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEditSketch,
            this.tsmiCreateSketch,
            this.toolStripMenuItem6,
            this.tsmiSaveSketch,
            this.toolStripMenuItem7,
            this.tsmiRefreshSketchesList,
            this.toolStripMenuItem5,
            this.tsmiRemoveSketch});
            this.cmsSketches.Name = "cmsSketches";
            this.cmsSketches.Size = new System.Drawing.Size(236, 132);
            // 
            // tsmiEditSketch
            // 
            this.tsmiEditSketch.Image = global::BZEditor.Properties.Resources.button_editsketch;
            this.tsmiEditSketch.Name = "tsmiEditSketch";
            this.tsmiEditSketch.Size = new System.Drawing.Size(235, 22);
            this.tsmiEditSketch.Text = "Редактировать эскиз";
            // 
            // tsmiCreateSketch
            // 
            this.tsmiCreateSketch.Image = global::BZEditor.Properties.Resources.button_addsketch;
            this.tsmiCreateSketch.Name = "tsmiCreateSketch";
            this.tsmiCreateSketch.Size = new System.Drawing.Size(235, 22);
            this.tsmiCreateSketch.Text = "Создать эскиз комплекса зон";
            this.tsmiCreateSketch.Click += new System.EventHandler(this.CreateSketchClick);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(232, 6);
            // 
            // tsmiSaveSketch
            // 
            this.tsmiSaveSketch.Image = global::BZEditor.Properties.Resources.button_savesketch;
            this.tsmiSaveSketch.Name = "tsmiSaveSketch";
            this.tsmiSaveSketch.Size = new System.Drawing.Size(235, 22);
            this.tsmiSaveSketch.Text = "Сохранить эскиз";
            this.tsmiSaveSketch.Click += new System.EventHandler(this.SaveSketchClick);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(232, 6);
            // 
            // tsmiRefreshSketchesList
            // 
            this.tsmiRefreshSketchesList.Image = global::BZEditor.Properties.Resources.button_refreshsktlist;
            this.tsmiRefreshSketchesList.Name = "tsmiRefreshSketchesList";
            this.tsmiRefreshSketchesList.Size = new System.Drawing.Size(235, 22);
            this.tsmiRefreshSketchesList.Text = "Обновить список";
            this.tsmiRefreshSketchesList.Click += new System.EventHandler(this.RefreshSketchesListClick);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(232, 6);
            // 
            // tsmiRemoveSketch
            // 
            this.tsmiRemoveSketch.Image = global::BZEditor.Properties.Resources.button_cancel;
            this.tsmiRemoveSketch.Name = "tsmiRemoveSketch";
            this.tsmiRemoveSketch.Size = new System.Drawing.Size(235, 22);
            this.tsmiRemoveSketch.Text = "Удалить эскиз";
            this.tsmiRemoveSketch.Click += new System.EventHandler(this.RemoveSketchClick);
            // 
            // tbQuickSearchSketch
            // 
            this.tbQuickSearchSketch.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbQuickSearchSketch.Location = new System.Drawing.Point(3, 16);
            this.tbQuickSearchSketch.Name = "tbQuickSearchSketch";
            this.tbQuickSearchSketch.Size = new System.Drawing.Size(260, 20);
            this.tbQuickSearchSketch.TabIndex = 6;
            this.tbQuickSearchSketch.TextChanged += new System.EventHandler(this.TbQuickSearchSketchTextChanged);
            this.tbQuickSearchSketch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TbQuickSearchSketchKeyUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Window;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Быстрый поиск эскиза";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ZonesListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(274, 430);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ZonesListForm";
            this.TabText = "Зоны и Эскизы";
            this.Text = "Зоны и Эскизы";
            this.ToolTipText = "Зоны и Эскизы";
            this.cmsLoadedZones.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tpLoaded.ResumeLayout(false);
            this.tpLoaded.PerformLayout();
            this.tpAvailable.ResumeLayout(false);
            this.tpAvailable.PerformLayout();
            this.cmsZonesAvail.ResumeLayout(false);
            this.tpSketches.ResumeLayout(false);
            this.tpSketches.PerformLayout();
            this.cmsSketches.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbQuickSearch;
        private System.Windows.Forms.ImageList imageListTree;
        private System.Windows.Forms.ListView lvZones;
        private System.Windows.Forms.ColumnHeader columnHeader;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tpLoaded;
        private System.Windows.Forms.TabPage tpAvailable;
        private System.Windows.Forms.TextBox tbQuickSearchAvail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip cmsLoadedZones;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditLoaded;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmiPrepareLoadedToSedind;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem tsmiUnloadZone;
        private System.Windows.Forms.ContextMenuStrip cmsZonesAvail;
        private System.Windows.Forms.ToolStripMenuItem tsmiLoadZone;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem tsmiPrepareAvailToSedind;
        private System.Windows.Forms.ToolStripMenuItem tsmiRefreshList;
        private System.Windows.Forms.ToolStripMenuItem tsmiSaveZone;
        private System.Windows.Forms.ToolStripMenuItem tsmiLoadAndEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmiLoadZoneWin;
        private System.Windows.Forms.ListView lvZonesAvail;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.TabPage tpSketches;
        private System.Windows.Forms.ListView lvSketches;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TextBox tbQuickSearchSketch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip cmsSketches;
        private System.Windows.Forms.ToolStripMenuItem tsmiCreateSketch;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem tsmiRemoveSketch;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem tsmiRefreshSketchesList;
        private System.Windows.Forms.ToolStripMenuItem tsmiEditSketch;
        private System.Windows.Forms.ToolStripMenuItem tsmiSaveSketch;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
    }
}