using System;
using System.Windows.Forms;
using DataUtils;
using Object = DataUtils.Obj;

namespace BZEditor
{
    public partial class ObjSelectForm : BaseDialog
    {
        private readonly int curZone;
        private readonly ObjsCollection objectsCollection;
        public ObjsCollection SelectedObjects = new ObjsCollection();

        public ObjSelectForm(string caption, ObjsCollection objectsCollection, int curZone, bool allowMultiselect,
                             bool selectFromAllZones)
        {
            this.objectsCollection = objectsCollection;
            this.curZone = curZone;
            InitializeComponent();
            cbAllowAllObj.Enabled = !selectFromAllZones;
            lvMainList.MultiSelect = allowMultiselect;
            base.Text = caption;
            RefreshList();
        }

        private void RefreshList()
        {
            lvMainList.BeginUpdate();
            lvMainList.Items.Clear();
            foreach (Obj obj in objectsCollection)
            {
                ListViewItem lvi = new ListViewItem(new[] { obj.VNum.ToString(), obj.Cases.Imen })
                                       {Tag = obj.VNum};
                if (cbAllowAllObj.Checked || obj.VNum.ToString().IndexOf(curZone.ToString()) == 0)
                {
                    if (tboxMainListFilter.Text.Length > 0)
                    {
                        if (
                            (obj.Cases.Imen.ToUpper() + obj.VNum.ToString().ToUpper()).IndexOf(
                                tboxMainListFilter.Text.ToUpper()) >= 0)
                            lvMainList.Items.Add(lvi);
                    }
                    else
                        lvMainList.Items.Add(lvi);
                }
            }
            lvMainList.EndUpdate();
        }

        private void CbAhowAllMobsCheckedChanged(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void TboxMainListFilterTextChanged(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void BtnOkClick(object sender, EventArgs e)
        {
            SelectedObjects = new ObjsCollection();
            if (nudObjVnum.Value == -1)
            {
                foreach (ListViewItem lvi in lvMainList.SelectedItems)
                    SelectedObjects.Add(objectsCollection[Convert.ToInt32(lvi.Tag), 0]);
            }
            else
            {
                Obj obj = new Object(Convert.ToInt32(nudObjVnum.Value))
                                 {Cases = {Imen = "Отсутствует в имеющихся зонах."}};
                SelectedObjects.Add(obj);
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void LvMainListDoubleClick(object sender, EventArgs e)
        {
            SelectedObjects = new ObjsCollection();
            foreach (ListViewItem lvi in lvMainList.SelectedItems)
                SelectedObjects.Add(objectsCollection[Convert.ToInt32(lvi.Tag), 0]);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void LvMainListSizeChanged(object sender, EventArgs e)
        {
            chMainListItemName.Width = -2;
        }

        private void LvMainListSelectedIndexChanged(object sender, EventArgs e)
        {
            if (nudObjVnum.Value != -1)
                nudObjVnum.Value = -1;
        }
    }
}