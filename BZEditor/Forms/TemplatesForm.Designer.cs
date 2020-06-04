using WeifenLuo.WinFormsUI.Docking;

namespace BZEditor
{
    partial class TemplatesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemplatesForm));
            this.tabControlTemplates = new System.Windows.Forms.TabControl();
            this.tabPageObj = new System.Windows.Forms.TabPage();
            this.listViewObjTemplates = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.tabPageMob = new System.Windows.Forms.TabPage();
            this.listViewMobTemplates = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.cmsTemplates = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiApplyTemlate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiRemoveTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlTemplates.SuspendLayout();
            this.tabPageObj.SuspendLayout();
            this.tabPageMob.SuspendLayout();
            this.cmsTemplates.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlTemplates
            // 
            this.tabControlTemplates.Controls.Add(this.tabPageObj);
            this.tabControlTemplates.Controls.Add(this.tabPageMob);
            this.tabControlTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlTemplates.Location = new System.Drawing.Point(0, 0);
            this.tabControlTemplates.Multiline = true;
            this.tabControlTemplates.Name = "tabControlTemplates";
            this.tabControlTemplates.SelectedIndex = 0;
            this.tabControlTemplates.Size = new System.Drawing.Size(225, 527);
            this.tabControlTemplates.TabIndex = 0;
            // 
            // tabPageObj
            // 
            this.tabPageObj.Controls.Add(this.listViewObjTemplates);
            this.tabPageObj.Location = new System.Drawing.Point(4, 22);
            this.tabPageObj.Name = "tabPageObj";
            this.tabPageObj.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageObj.Size = new System.Drawing.Size(217, 501);
            this.tabPageObj.TabIndex = 0;
            this.tabPageObj.Text = "Объектов";
            this.tabPageObj.UseVisualStyleBackColor = true;
            // 
            // listViewObjTemplates
            // 
            this.listViewObjTemplates.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.listViewObjTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewObjTemplates.FullRowSelect = true;
            this.listViewObjTemplates.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewObjTemplates.HideSelection = false;
            this.listViewObjTemplates.Location = new System.Drawing.Point(3, 3);
            this.listViewObjTemplates.Name = "listViewObjTemplates";
            this.listViewObjTemplates.Size = new System.Drawing.Size(211, 495);
            this.listViewObjTemplates.TabIndex = 0;
            this.listViewObjTemplates.UseCompatibleStateImageBehavior = false;
            this.listViewObjTemplates.View = System.Windows.Forms.View.Details;
            this.listViewObjTemplates.SizeChanged += new System.EventHandler(this.LastColumnAutosize);
            this.listViewObjTemplates.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ListViewObjTemplatesMouseUp);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 207;
            // 
            // tabPageMob
            // 
            this.tabPageMob.Controls.Add(this.listViewMobTemplates);
            this.tabPageMob.Location = new System.Drawing.Point(4, 22);
            this.tabPageMob.Name = "tabPageMob";
            this.tabPageMob.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMob.Size = new System.Drawing.Size(217, 501);
            this.tabPageMob.TabIndex = 1;
            this.tabPageMob.Text = "Мобов";
            this.tabPageMob.UseVisualStyleBackColor = true;
            // 
            // listViewMobTemplates
            // 
            this.listViewMobTemplates.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listViewMobTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewMobTemplates.FullRowSelect = true;
            this.listViewMobTemplates.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewMobTemplates.HideSelection = false;
            this.listViewMobTemplates.Location = new System.Drawing.Point(3, 3);
            this.listViewMobTemplates.Name = "listViewMobTemplates";
            this.listViewMobTemplates.Size = new System.Drawing.Size(211, 495);
            this.listViewMobTemplates.TabIndex = 1;
            this.listViewMobTemplates.UseCompatibleStateImageBehavior = false;
            this.listViewMobTemplates.View = System.Windows.Forms.View.Details;
            this.listViewMobTemplates.SizeChanged += new System.EventHandler(this.LastColumnAutosize);
            this.listViewMobTemplates.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ListViewMobTemplatesMouseUp);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 207;
            // 
            // cmsTemplates
            // 
            this.cmsTemplates.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiApplyTemlate,
            this.toolStripMenuItem1,
            this.tsmiRemoveTemplate});
            this.cmsTemplates.Name = "cmsTemplates";
            this.cmsTemplates.Size = new System.Drawing.Size(215, 76);
            // 
            // tsmiApplyTemlate
            // 
            this.tsmiApplyTemlate.Image = global::BZEditor.Properties.Resources.button_usetemplate;
            this.tsmiApplyTemlate.Name = "tsmiApplyTemlate";
            this.tsmiApplyTemlate.Size = new System.Drawing.Size(214, 22);
            this.tsmiApplyTemlate.Text = "Применить к выбранному";
            this.tsmiApplyTemlate.Click += new System.EventHandler(this.TsmiApplyTemlateClick);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(211, 6);
            // 
            // tsmiRemoveTemplate
            // 
            this.tsmiRemoveTemplate.Image = global::BZEditor.Properties.Resources.button_removesmth;
            this.tsmiRemoveTemplate.Name = "tsmiRemoveTemplate";
            this.tsmiRemoveTemplate.Size = new System.Drawing.Size(214, 22);
            this.tsmiRemoveTemplate.Text = "Удалить шаблон";
            this.tsmiRemoveTemplate.Click += new System.EventHandler(this.TsmiRemoveTemplateClick);
            // 
            // TemplatesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(225, 527);
            this.Controls.Add(this.tabControlTemplates);
            /*this.DockableAreas = ((DockAreas)(((((DockAreas.Float | DockAreas.DockLeft)
                        | DockAreas.DockRight)
                        | DockAreas.DockTop)
                        | DockAreas.DockBottom)));*/
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TemplatesForm";
            this.TabText = "Шаблоны";
            this.Text = "Шаблоны";
            this.tabControlTemplates.ResumeLayout(false);
            this.tabPageObj.ResumeLayout(false);
            this.tabPageMob.ResumeLayout(false);
            this.cmsTemplates.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlTemplates;
        private System.Windows.Forms.TabPage tabPageObj;
        private System.Windows.Forms.TabPage tabPageMob;
        private System.Windows.Forms.ListView listViewObjTemplates;
        private System.Windows.Forms.ListView listViewMobTemplates;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ContextMenuStrip cmsTemplates;
        private System.Windows.Forms.ToolStripMenuItem tsmiApplyTemlate;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmiRemoveTemplate;







    }
}