using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace BZEditor
{
    public partial class IngrSelectForm : BaseDialog
    {
        public List<string> SelectedIngredients { get; set; } = new List<string>(1);
        private readonly DataTable basesDataTable;

        public IngrSelectForm(string caption, DataTable basesDataTable)
        {
            this.basesDataTable = basesDataTable;
            InitializeComponent();
            lvMainList.MultiSelect = true;
            Text = caption;
            RefreshList();
        }

        private void RefreshList()
        {
            lvMainList.BeginUpdate();
            lvMainList.Items.Clear();
            foreach (DataRow dr in basesDataTable.Rows)
            {
                ListAddRow(dr);                
            }
            lvMainList.EndUpdate();
        }

        private void ListAddRow(DataRow dr)
        {
            string val = dr["val"].ToString();
            string name = dr["desc"].ToString();
            //string group = dr["group"].ToString();

            ListViewItem lvi = new ListViewItem(new[] { val, name }) { Tag = val };
            if (tboxMainListFilter.Text.Length > 0)
            {
                if ((name.ToUpper() + val).IndexOf(tboxMainListFilter.Text.ToUpper(), StringComparison.Ordinal) >= 0)
                    lvMainList.Items.Add(lvi);
            }
            else
                lvMainList.Items.Add(lvi);            
        }

        private void TboxMainListFilterTextChanged(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void BtnOkClick(object sender, EventArgs e)
        {
            SelectedIngredients.Clear();
            foreach (ListViewItem lvi in lvMainList.SelectedItems)
                SelectedIngredients.Add(lvi.Tag.ToString());
            if (lvMainList.SelectedItems.Count > 0 && lvMainList.SelectedItems[0].Tag != null)
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
            SelectedIngredients.Clear();
            foreach (ListViewItem lvi in lvMainList.SelectedItems)
                SelectedIngredients.Add(lvi.Tag.ToString());
            if (lvMainList.SelectedItems.Count > 0 && lvMainList.SelectedItems[0].Tag != null)
            DialogResult = DialogResult.OK;
            Close();
        }

        private void LvMainListSizeChanged(object sender, EventArgs e)
        {
            chMainListItemName.Width = -2;
        }
    }
}