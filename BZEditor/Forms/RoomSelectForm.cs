using System;
using System.Windows.Forms;
using DataUtils;

namespace BZEditor
{
    public partial class RoomSelectForm : BaseDialog
    {
        private readonly int curZone;
        private readonly RoomsCollection roomsCollection;
        public RoomsCollection SelectedRooms { get; set; } = new RoomsCollection();

        public RoomSelectForm(string caption, RoomsCollection roomsCollection, int curZone, bool allowMultiSelect,
                              bool selectOnlyFromSameZone)
        {
            this.roomsCollection = roomsCollection;
            this.curZone = curZone;
            InitializeComponent();
            cbAhowAllRooms.Enabled = !selectOnlyFromSameZone;
            lvMainList.MultiSelect = allowMultiSelect;
            Text = caption;
            RefreshList();
        }

        private void RefreshList()
        {
            lvMainList.BeginUpdate();
            lvMainList.Items.Clear();
            foreach (Room room in roomsCollection)
            {
                ListViewItem lvi = new ListViewItem(new[] { room.VNum.ToString(), room.Name })
                {
                    Tag = room.VNum
                };
                if (cbAhowAllRooms.Checked || room.VNum.ToString().IndexOf(curZone.ToString(), StringComparison.Ordinal) == 0)
                {
                    if (tboxMainListFilter.Text.Length > 0)
                    {
                        if ((room.Name.ToUpper() + room.VNum.ToString().ToUpper()).IndexOf(
                                tboxMainListFilter.Text.ToUpper(), StringComparison.Ordinal) >= 0)
                            lvMainList.Items.Add(lvi);
                    }
                    else
                        lvMainList.Items.Add(lvi);
                }
            }
            lvMainList.EndUpdate();
        }

        private void cbAhowAllRooms_CheckedChanged(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void tboxMainListFilter_TextChanged(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SelectedRooms = new RoomsCollection();
            foreach (ListViewItem lvi in lvMainList.SelectedItems)
                SelectedRooms.Add(roomsCollection[Convert.ToInt32(lvi.Tag), 0]);
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
            SelectedRooms = new RoomsCollection();
            foreach (ListViewItem lvi in lvMainList.SelectedItems)
                SelectedRooms.Add(roomsCollection[Convert.ToInt32(lvi.Tag), 0]);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void lvMainList_SizeChanged(object sender, EventArgs e)
        {
            chMainListItemName.Width = -2;
        }

        private void lvMainList_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnOk.Enabled = lvMainList.SelectedItems.Count > 0;
        }
    }
}