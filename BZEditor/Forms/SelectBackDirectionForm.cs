using System;
using System.Windows.Forms;
using DataUtils;

namespace BZEditor
{
    public partial class SelectBackDirectionForm : Form
    {
        public string Res = "";
        private readonly Room room;

        public SelectBackDirectionForm()
        {
            InitializeComponent();
        }

        public SelectBackDirectionForm(Room room) : this()
        {
            //InitializeComponent();
            if (room.Img != null)
                pictureBox.BackgroundImage = room.Img;
            this.room = room;
            cbFreeExitsOnly.Checked = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnDir_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Res = ((Button) sender).Tag.ToString();
            Close();
        }

        private void cbFreeExitsOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (cbFreeExitsOnly.Checked)
            {
                btnConfigExitNorth.Enabled = (room.ExitNorth.RoomVNum == -1);
                btnConfigExitSouth.Enabled = (room.ExitSouth.RoomVNum == -1);
                btnConfigExitWest.Enabled = (room.ExitWest.RoomVNum == -1);
                btnConfigExitEast.Enabled = (room.ExitEast.RoomVNum == -1);
                btnConfigExitUp.Enabled = (room.ExitUp.RoomVNum == -1);
                btnConfigExitDown.Enabled = (room.ExitDown.RoomVNum == -1);
            }
            else
            {
                btnConfigExitNorth.Enabled = true;
                btnConfigExitSouth.Enabled = true;
                btnConfigExitWest.Enabled = true;
                btnConfigExitEast.Enabled = true;
                btnConfigExitUp.Enabled = true;
                btnConfigExitDown.Enabled = true;
            }
        }
    }
}