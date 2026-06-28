using System;
using System.Windows.Forms;
using DataUtils;

namespace BZEditor
{
    public partial class TrgSelectForm : BaseDialog
    {
        private readonly int curZone;
        public TriggersCollection SelectedTriggers = new TriggersCollection();
        private readonly TriggersCollection triggersCollection;

        public TrgSelectForm(string caption, TriggersCollection triggersCollection, int curZone, bool allowMultiselect,
                             bool selectOnlyFromSameZone)
        {
            this.triggersCollection = triggersCollection;
            this.curZone = curZone;
            InitializeComponent();
            cbAhowAll.Enabled = !selectOnlyFromSameZone;
            lvMainList.MultiSelect = allowMultiselect;
            Text = caption;
            RefreshList();
        }

        private void RefreshList()
        {
            lvMainList.BeginUpdate();
            lvMainList.Items.Clear();
            foreach (Trigger trigger in triggersCollection)
            {
                ListViewItem lvi = new ListViewItem(new[] {trigger.VNum.ToString(), trigger.Name}) {Tag = trigger.VNum};
                if (cbAhowAll.Checked || trigger.VNum.ToString().IndexOf(curZone.ToString(), StringComparison.Ordinal) == 0)
                {
                    if (tboxMainListFilter.Text.Length > 0)
                    {
                        if (
                            (trigger.Name.ToUpper() + trigger.VNum.ToString().ToUpper()).IndexOf(
                                tboxMainListFilter.Text.ToUpper(), StringComparison.Ordinal) >= 0)
                            lvMainList.Items.Add(lvi);
                    }
                    else
                        lvMainList.Items.Add(lvi);
                }
            }
            lvMainList.EndUpdate();
        }

        private void cbAhowAllMobs_CheckedChanged(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void tboxMainListFilter_TextChanged(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SelectedTriggers = new TriggersCollection();
            if (nudVnum.Value == -1)
            {
                foreach (ListViewItem lvi in lvMainList.SelectedItems)
                    SelectedTriggers.Add(triggersCollection[Convert.ToInt32(lvi.Tag), 0]);
            }
            else
            {
                Trigger trigger = new Trigger(Convert.ToInt32(nudVnum.Value))
                {
                    Name = "Отсутствует в имеющихся зонах."
                };
                SelectedTriggers.Add(trigger);
            }
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
            SelectedTriggers = new TriggersCollection();
            foreach (ListViewItem lvi in lvMainList.SelectedItems)
                SelectedTriggers.Add(triggersCollection[Convert.ToInt32(lvi.Tag), 0]);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void lvMainList_SizeChanged(object sender, EventArgs e)
        {
            chMainListItemName.Width = -2;
        }

        private void lvMainList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (nudVnum.Value != -1)
                nudVnum.Value = -1;
        }
    }
}