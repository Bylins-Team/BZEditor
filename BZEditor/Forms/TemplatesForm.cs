using System;
using System.Windows.Forms;
using DataUtils;
using WeifenLuo.WinFormsUI.Docking;
using Object = DataUtils.Obj;

namespace BZEditor
{
    public partial class TemplatesForm : DockContent
    {
        #region Delegates

        public delegate void TemplateApplyingFireEvent(Guid guid, TemplatesDataManager.TemplatesType type);

        #endregion

        private readonly TemplatesDataManager templatesDm;

        public TemplatesForm(ref TemplatesDataManager templatesDm)
        {
            InitializeComponent();
            this.templatesDm = templatesDm;
            templatesDm.TemplatesStateChanged += TemplatesDM_TemplatesStateChanged;
            Refreshlists(TemplatesDataManager.TemplatesType.Both);
        }

        public event TemplateApplyingFireEvent TemplateApplyingFired;

        private void TemplatesDM_TemplatesStateChanged(TemplatesDataManager.TemplatesType type)
        {
            Refreshlists(type);
        }

        private void Refreshlists(TemplatesDataManager.TemplatesType type)
        {
            ListViewGroup lvg;
            switch (type)
            {
                case TemplatesDataManager.TemplatesType.Object:
                    listViewObjTemplates.Items.Clear();
                    listViewObjTemplates.Groups.Clear();

                    lvg = new ListViewGroup("Предустановленные", HorizontalAlignment.Left) {Tag = "Const"};
                    listViewObjTemplates.Groups.Add(lvg);
                    AddObjTemplates(templatesDm.ConstObjectsTemplates, lvg);

                    lvg = new ListViewGroup("Пользовательские", HorizontalAlignment.Left) {Tag = "User"};
                    listViewObjTemplates.Groups.Add(lvg);
                    AddObjTemplates(templatesDm.UserObjectsTemplates, lvg);
                    break;
                case TemplatesDataManager.TemplatesType.Mob:
                    listViewMobTemplates.Items.Clear();
                    listViewMobTemplates.Groups.Clear();

                    lvg = new ListViewGroup("Предустановленные", HorizontalAlignment.Left) {Tag = "Const"};
                    listViewMobTemplates.Groups.Add(lvg);
                    AddMobTemplates(templatesDm.ConstMobsTemplates, lvg);

                    lvg = new ListViewGroup("Пользовательские", HorizontalAlignment.Left) {Tag = "User"};
                    listViewMobTemplates.Groups.Add(lvg);
                    AddMobTemplates(templatesDm.UserMobsTemplates, lvg);
                    break;
                case TemplatesDataManager.TemplatesType.Both:
                    listViewObjTemplates.Items.Clear();
                    listViewObjTemplates.Groups.Clear();
                    listViewMobTemplates.Items.Clear();
                    listViewMobTemplates.Groups.Clear();

                    lvg = new ListViewGroup("Предустановленные", HorizontalAlignment.Left) {Tag = "Const"};
                    listViewObjTemplates.Groups.Add(lvg);
                    AddObjTemplates(templatesDm.ConstObjectsTemplates, lvg);

                    lvg = new ListViewGroup("Пользовательские", HorizontalAlignment.Left) {Tag = "User"};
                    listViewObjTemplates.Groups.Add(lvg);
                    AddObjTemplates(templatesDm.UserObjectsTemplates, lvg);

                    lvg = new ListViewGroup("Предустановленные", HorizontalAlignment.Left) {Tag = "Const"};
                    listViewMobTemplates.Groups.Add(lvg);
                    AddMobTemplates(templatesDm.ConstMobsTemplates, lvg);

                    lvg = new ListViewGroup("Пользовательские", HorizontalAlignment.Left) {Tag = "User"};
                    listViewMobTemplates.Groups.Add(lvg);
                    AddMobTemplates(templatesDm.UserMobsTemplates, lvg);
                    break;
            }
        }

        private void AddObjTemplates(ObjsCollection oc, ListViewGroup lvg)
        {
            foreach (Obj obj in oc)
            {
                ListViewItem lvi = new ListViewItem(new [] {obj.Cases.Imen}, lvg) {Tag = obj.Guid};
                listViewObjTemplates.Items.Add(lvi);
            }
        }

        private void AddMobTemplates(MobsCollection mc, ListViewGroup lvg)
        {
            foreach (Mob mob in mc)
            {
                ListViewItem lvi = new ListViewItem(new [] {mob.Cases.Imen}, lvg) {Tag = mob.Guid};
                listViewMobTemplates.Items.Add(lvi);
            }
        }

        private void LastColumnAutosize(object sender, EventArgs e)
        {
            if (((ListView) sender).Columns.Count < 1) return;
            /*int delta = 0;*/
            /*if (((ListView)sender).Columns.Count > 1)
                for (int i = 0; i < ((ListView)sender).Columns.Count - 1; i++)
                {
                    delta += ((ListView)sender).Columns[i].Width;
                }*/
            if (((ListView) sender).Columns[((ListView) sender).Columns.Count - 1].Width !=
                ((ListView) sender).Width - 22/*- delta*/)
                ((ListView) sender).Columns[((ListView) sender).Columns.Count - 1].Width = ((ListView) sender).Width -
                                                                                           22/* - delta*/;
        }

        public void SetSelectedTab(int index)
        {
            tabControlTemplates.SelectedIndex = index;
        }

        private void TsmiRemoveTemplateClick(object sender, EventArgs e)
        {
            if (tabControlTemplates.SelectedIndex == 0)
            {
                if (listViewObjTemplates.SelectedItems.Count > 0)
                {
                    Guid resGuid = (Guid) (listViewObjTemplates.SelectedItems[0].Tag);
                    templatesDm.RemoveObjTemplate(new [] {resGuid});
                }
            }
            else if (tabControlTemplates.SelectedIndex == 1)
            {
                if (listViewMobTemplates.SelectedItems.Count > 0)
                {
                    Guid resGuid = (Guid) (listViewMobTemplates.SelectedItems[0].Tag);
                    templatesDm.RemoveMobTemplate(new [] {resGuid});
                }
            }
        }

        private void ListViewObjTemplatesMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            if (listViewObjTemplates.SelectedItems.Count <= 0) return;
            tsmiRemoveTemplate.Enabled = (listViewObjTemplates.SelectedItems[0].Group.Tag.ToString() != "Const");
            cmsTemplates.Show(listViewObjTemplates, e.Location);
        }

        private void ListViewMobTemplatesMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            if (listViewMobTemplates.SelectedItems.Count <= 0) return;
            tsmiRemoveTemplate.Enabled = (listViewMobTemplates.SelectedItems[0].Group.Tag.ToString() != "Const");
            cmsTemplates.Show(listViewMobTemplates, e.Location);
        }

        private void TsmiApplyTemlateClick(object sender, EventArgs e)
        {
            if (tabControlTemplates.SelectedIndex == 0)
            {
                if (TemplateApplyingFired != null && listViewObjTemplates.SelectedItems.Count > 0)
                {
                    Guid resGuid = (Guid) (listViewObjTemplates.SelectedItems[0].Tag);
                    TemplateApplyingFired(resGuid, TemplatesDataManager.TemplatesType.Object);
                }
            }
            else if (tabControlTemplates.SelectedIndex == 1)
            {
                if (TemplateApplyingFired != null && listViewMobTemplates.SelectedItems.Count > 0)
                {
                    Guid resGuid = (Guid) (listViewMobTemplates.SelectedItems[0].Tag);
                    TemplateApplyingFired(resGuid, TemplatesDataManager.TemplatesType.Mob);
                }
            }
        }
    }
}