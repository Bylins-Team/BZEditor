using System;
using System.Windows.Forms;
using DataUtils;

namespace BZEditor
{
    public partial class MobSelectForm : BaseDialog
    {
        private readonly int curZone;
        private readonly MobsCollection mobsCollection;
        public MobsCollection SelectedMobs { get; set; } = new MobsCollection();

        public MobSelectForm(string caption, MobsCollection mobsCollection, int curZone, bool allowMultiselect,
                             bool selectOnlyFromSameZone)
        {
            this.mobsCollection = mobsCollection;
            this.curZone = curZone;
            InitializeComponent();
            cbAhowAllMobs.Enabled = !selectOnlyFromSameZone;
            lvMainList.MultiSelect = allowMultiselect;
            Text = caption;
            RefreshList();
        }

        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        private void RefreshList()
        {
            lvMainList.BeginUpdate();
            lvMainList.Items.Clear();
            foreach (Mob mob in mobsCollection)
            {
                ListViewItem lvi =
                    new ListViewItem(new[] {mob.VNum.ToString(), mob.Cases.Imen}) {Tag = mob.VNum};
                if (cbAhowAllMobs.Checked || mob.VNum.ToString().IndexOf(curZone.ToString(), StringComparison.Ordinal) == 0)
                {
                    if (tboxMainListFilter.Text.Length > 0)
                    {
                        if (
                            (mob.Cases.Imen.ToUpper() + mob.VNum.ToString().ToUpper()).IndexOf(
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
            SelectedMobs = new MobsCollection();
            if (nudMobVnum.Value == -1)
            {
                foreach (ListViewItem lvi in lvMainList.SelectedItems)
                    SelectedMobs.Add(mobsCollection[Convert.ToInt32(lvi.Tag), 0]);
            }
            else
            {
                Mob mob = new Mob(Convert.ToInt32(nudMobVnum.Value));
                mob.Cases.Imen = "Отсутствует в имеющихся зонах.";
                SelectedMobs.Add(mob);
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
            SelectedMobs = new MobsCollection();
            foreach (ListViewItem lvi in lvMainList.SelectedItems)
                SelectedMobs.Add(mobsCollection[Convert.ToInt32(lvi.Tag), 0]);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void lvMainList_SizeChanged(object sender, EventArgs e)
        {
            chMainListItemName.Width = -2;
        }

        private void lvMainList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (nudMobVnum.Value != -1)
                nudMobVnum.Value = -1;
        }
    }
}