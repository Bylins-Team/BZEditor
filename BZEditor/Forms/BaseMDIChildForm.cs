using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DataUtils;
using WeifenLuo.WinFormsUI.Docking;

namespace BZEditor
{
    /// <summary>
    /// Summary description for BaseMDIChildForm.
    /// </summary>
    public class BaseMdiChildForm : DockContent
    {
        public CBasesDataManager BasesDm;

        public ZoneDataManager ZoneDm;

        public BaseMdiChildForm()
        {
            AutoScaleMode = AutoScaleMode.Dpi;
        }

        public void BindCheckedListBox(CheckedListBox clb, DataTable table)
        {
            BindCheckedListBox(clb, table, true);
        }

        public void BindCheckedListBox(CheckedListBox clb, DataTable table, bool mustClear)
        {
            if (mustClear)
                clb.Items.Clear();
            foreach (DataRow dr in table.Rows)
            {
                TaggedComboBoxItem tcbi = new TaggedComboBoxItem
                                               {
                                                   Text = dr["desc"].ToString(),
                                                   Tag = dr["val"].ToString()
                                               };
                clb.Items.Add(tcbi);
            }
            //cb.ValueMember = "val";
            //cb.DisplayMember = "desc";
        }

        public BaseMdiChildForm(ZoneDataManager ZoneDm, CBasesDataManager BasesDm)
        {
            InitializeComponent();
            this.ZoneDm = ZoneDm;
            this.BasesDm = BasesDm;
        }

        public void BindComboBox(ComboBox cb, DataTable table, int index)
        {
            BindComboBox(cb, table, true);
            if (cb.Items.Count > 0)
                cb.SelectedIndex = 0;
        }

        public void BindComboBox(ComboBox cb, DataTable table)
        {
            BindComboBox(cb, table, true);
        }

        public void BindComboBox(ComboBox cb, DataTable table, bool mustClear)
        {
            //чтоб не запариваться и не проставлять везде эту парашу
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
            //
            if (mustClear)
                cb.Items.Clear();
            foreach (DataRow dr in table.Rows)
            {
                TaggedComboBoxItem tcbi = new TaggedComboBoxItem
                                               {
                                                   Text = dr["desc"].ToString(),
                                                   Tag = dr["val"].ToString()
                                               };
                cb.Items.Add(tcbi);
            }
            //cb.ValueMember = "val";
            //cb.DisplayMember = "desc";
        }

        public void SetCBoxsSelectedItem(ComboBox cb, object sTag)
        {
            foreach (object tcbi in cb.Items)
            {
                if (((TaggedComboBoxItem) tcbi).Tag.ToString() != sTag.ToString()) continue;
                cb.SelectedItem = tcbi;
                return;
            }
        }

        public object GetCBoxsSelectedValue(ComboBox cb)
        {
            return ((TaggedComboBoxItem) (cb.SelectedItem)).Tag;
        }

        public void SetListViewItemChecked(ListView lv, object sTag, bool Checked)
        {
            foreach (ListViewItem lvi in lv.Items)
            {
                if (lvi.Tag.ToString() != sTag.ToString()) continue;
                lvi.Checked = Checked;
                return;
            }
        }

        public void SetNumericParam(NumericUpDown nud, object value)
        {
            if (value.ToString() == "")
                value = "0";
            if (Convert.ToInt32(value) < nud.Minimum)
                nud.Value = nud.Minimum;
            else nud.Value = Convert.ToInt32(value) > nud.Maximum ? nud.Maximum : Convert.ToInt32(value);
        }

        public void BindListView(ListView lv, DataTable table)
        {
            lv.BeginUpdate();
            lv.Items.Clear();
            foreach (DataRow dr in table.Rows)
            {
                ListViewItem lvi = new ListViewItem(dr["desc"].ToString())
                                       {
                                           Text = dr["desc"].ToString(),
                                           Tag = dr["val"].ToString()
                                       };
                //ListViewItem lvi = new ListViewItem();
                lv.Items.Add(lvi);
            }
            lv.EndUpdate();
        }

        public int GetLag(int val)
        {
            int res = 0;
            if ((val & 1) == 1) res += 1;
            if ((val & 2) == 2) res += 2;
            if ((val & 4) == 4) res += 4;
            if ((val & 8) == 8) res += 8;
            if ((val & 16) == 16) res += 16;
            if ((val & 32) == 32) res += 32;
            if ((val & 64) == 64) res += 64;
            if ((val & 128) == 128) res += 128;
            return res;
        }

        public int GetLevel(int val)
        {
            int res = 0;
            if ((val & 256) == 256) res += 1;
            if ((val & 512) == 512) res += 2;
            if ((val & 1024) == 1024) res += 4;
            if ((val & 2048) == 2048) res += 8;
            if ((val & 4096) == 4096) res += 16;
            return res;
        }

        public int CombineLagAndLevel(int lag, int level)
        {
            int res = lag;
            if ((level & 1) == 1) res += 256;
            if ((level & 2) == 2) res += 512;
            if ((level & 4) == 4) res += 1024;
            if ((level & 8) == 8) res += 2048;
            if ((level & 16) == 16) res += 4096;
            return res;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }

        public bool IsInteger(string data)
        {
            Regex t = new Regex("\\d+");
            return t.IsMatch(data);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // 
            // BaseMDIChildForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Name = "BaseMdiChildForm";
            this.Text = "BaseMDIChildForm";
        }

        #endregion
    }
}