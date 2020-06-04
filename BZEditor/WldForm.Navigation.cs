using System;
using System.Windows.Forms;
using DataUtils;
using ExtControls;

namespace BZEditor
{
    public partial class WldForm
    {

        internal void ClearDetails()
        {
            lvDetails.Items.Clear();
            lvDetails.Groups.Clear();
        }

        internal void Navigate(EXListViewItem item)
        {
            tcListAndInfo.SelectedIndex = 0;
            switch (item.Action)
            {
                case ActionType.GoToMob:
                    tcMain.SelectedIndex = 3;
                    SelectInList(item.Tag);
                    tcMobs.SelectedIndex = 0;
                    break;
                case ActionType.GoToObject:
                    tcMain.SelectedIndex = 2;
                    SelectInList(item.Tag);
                    tcObject.SelectedIndex = 0;
                    break;
                case ActionType.GoToRoom:
                    SelectRoomInList(item.Tag);
                    tcRoom.SelectedIndex = 0;
                    break;
                case ActionType.GoToTrigger:
                    tcMain.SelectedIndex = 5;
                    SelectInList(item.Tag);
                    break;
                case ActionType.GoToRoomLoadedMobs:
                    SelectRoomInList(item.Tag);
                    tcRoom.SelectedIndex = 3;
                    tcRoom.Enabled = true;
                    tcRoom.Focus();
                    RefreshRoomData();
                    SelectInList(lvMobsInRoom, item.TrgVNum);
                    SelectInList(elvRoomMobObjects, item.TrgVNum2);
                    break;
                case ActionType.GoToRoomLoadedObjects:
                    SelectRoomInList(item.Tag);
                    tcRoom.SelectedIndex = 2;
                    RefreshRoomData();
                    break;
                case ActionType.GoToRoomUnloadedObjects:
                    SelectRoomInList(item.Tag);
                    tcRoom.SelectedIndex = 5;
                    RefreshRoomData();
                    break;
            }
        }

        internal void Navigate(ParseMessageEventArgs item)
        {
            tcListAndInfo.SelectedIndex = 0;
            switch (item.Action)
            {
                case ActionType.GoToMob:
                    tcMain.SelectedIndex = 3;
                    SelectInList(item.VNum);
                    tcMobs.SelectedIndex = 0;
                    break;
                case ActionType.GoToObject:
                    tcMain.SelectedIndex = 2;
                    SelectInList(item.VNum);
                    tcObject.SelectedIndex = 0;
                    break;
                case ActionType.GoToRoom:
                    tcMain.SelectedIndex = 1;
                    SelectRoomInList(item.VNum);
                    tcRoom.SelectedIndex = 0;
                break;
                case ActionType.GoToTrigger:
                    tcMain.SelectedIndex = 5;
                    SelectInList(item.VNum);
                    break;
            }
        }

        private void SelectInList(object vNum)
        {
            SelectInList(vNum.ToString());
        }

        private void SelectInList(int vNum)
        {
            SelectInList(vNum.ToString());
        }

        private void SelectInList(string vNum)
        {
            if (lvMainList.Items.Count <= 0) return;
            foreach (ListViewItem lvi in lvMainList.Items)
            {
                if (lvi.Tag.ToString() != vNum) continue;
                lvMainList.TopItem = lvi;
                lvi.Selected = true;
                return;
            }
        }

        private static void SelectInList(ListView listView, int vNum)
        {
            if (listView.Items.Count <= 0) return;
            foreach (ListViewItem lvi in listView.Items)
            {
                if (lvi.Tag.ToString() != vNum.ToString()) continue;
                listView.TopItem = lvi;
                lvi.Selected = true;
                return;
            }
        }

        private void SelectRoomInList(object vNum)
        {
            SelectRoomInList(vNum.ToString());
        }

        private void SelectRoomInList(int vNum)
        {
            SelectRoomInList(vNum.ToString());
        }

        private void SelectRoomInList(string vNum)
        {
            lvMainList.SelectedItems.Clear();
            wldMap.SelectedRooms.Clear();
            foreach (ListViewItem lvi in lvMainList.Items)
            {
                if (lvi.Tag.ToString() != vNum) continue;
                lvMainList.TopItem = lvi;
                lvi.Selected = true;
                Room room = ZoneDM.RoomsCollection[Convert.ToInt32(vNum), 0];
                wldMap.SelectedRooms.Add(room.VNum);
                wldMap.CenterRoomCoord = new Point3D(room.X, room.Y, room.Z);
                return;
            }
            ActiveRoom = ZoneDM.RoomsCollection[Convert.ToInt32(vNum), 0];
            wldMap.AddRoomToSelection(ActiveRoom.VNum);
            wldMap.CenterRoomCoord = new Point3D(ActiveRoom.X, ActiveRoom.Y, ActiveRoom.Z);
        }
    }
}