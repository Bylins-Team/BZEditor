namespace BZEditor
{
    using System;
    using System.Data;
    using System.Windows.Forms;

    public partial class SpellSelectForm : BaseDialog
    {
        public int SelectedSpell = -1;
        private readonly CBasesDataManager basesDataManager;

        public SpellSelectForm(CBasesDataManager basesDataManager)
        {
            this.basesDataManager = basesDataManager;
            InitializeComponent();
            lvMainList.MultiSelect = true;
            RefreshList();
        }

        private void RefreshList()
        {
            lvMainList.BeginUpdate();
            lvMainList.Items.Clear();
            foreach (DataRow dr in basesDataManager.Spells.Rows)
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
                if ((name.ToUpper() + val).IndexOf(tboxMainListFilter.Text.ToUpper()) >= 0)
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
            if (lvMainList.SelectedItems.Count > 0 && lvMainList.SelectedItems[0].Tag != null)
                SelectedSpell = Convert.ToInt32(lvMainList.SelectedItems[0].Tag);
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
            if (lvMainList.SelectedItems.Count > 0 && lvMainList.SelectedItems[0].Tag != null)
            SelectedSpell = Convert.ToInt32(lvMainList.SelectedItems[0].Tag);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void LvMainListSizeChanged(object sender, EventArgs e)
        {
            chMainListItemName.Width = -2;
        }
    }
}