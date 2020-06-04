using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DataUtils;

namespace BZEditor
{
    public partial class ZoneSelectForm : BaseDialog
    {
        //private int CurZone;
        public List<int> SelectedZones = new List<int>();
        private readonly FileListsDataManager fileListsDm;

        public ZoneSelectForm(FileListsDataManager fileListsDm)
        {
            this.fileListsDm = fileListsDm;
            InitializeComponent();
            lvMainList.MultiSelect = true;
            RefreshList();
        }

        private void RefreshList()
        {
            lvMainList.BeginUpdate();
            lvMainList.Items.Clear();
            foreach (ZoneData zd in fileListsDm.ZonesDataList)
            {
                ListViewItem lvi = new ListViewItem(new[] { zd.FileName, zd.Name }) { Tag = zd.FileName };
                if (tboxMainListFilter.Text.Length > 0)
                {
                    if ((zd.Name.ToUpper() + zd.FileName).IndexOf(tboxMainListFilter.Text.ToUpper(), StringComparison.Ordinal) >= 0)
                        lvMainList.Items.Add(lvi);
                }
                else
                    lvMainList.Items.Add(lvi);
            }
            lvMainList.EndUpdate();
        }

        private void tboxMainListFilter_TextChanged(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SelectedZones = new List<int>();
            if (nudMobVnum.Value == -1)
            {
                foreach (ListViewItem lvi in lvMainList.SelectedItems)
                    SelectedZones.Add(Convert.ToInt32(lvi.Tag));
            }
            else
                SelectedZones.Add(Convert.ToInt32(nudMobVnum.Value));
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void lvMainList_DoubleClick(object sender, EventArgs e)
        {
            SelectedZones = new List<int>();
            foreach (ListViewItem lvi in lvMainList.SelectedItems)
                SelectedZones.Add(Convert.ToInt32(lvi.Tag));
            DialogResult = DialogResult.OK;
            Close();
        }

        private void lvMainList_SizeChanged(object sender, EventArgs e)
        {
            chMainListItemName.Width = -2;
        }

        private void lvMainList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (nudMobVnum != null)
                //if (nudMobVnum.Value != -1)
                    nudMobVnum.Value = -1;
        }
    }
}