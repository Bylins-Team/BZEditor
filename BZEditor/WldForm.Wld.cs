using System;
using System.Drawing;
using System.Windows.Forms;
using DataUtils;
using ExtControls;
using Object = DataUtils.Obj;

namespace BZEditor
{
    public partial class WldForm
    {
        private void SetRoomReadOnly()
        {
            tbDoorAlias.Enabled = false;
            tbDoorNameVin.Enabled = false;
            tbDoorDesc.Enabled = false;
            gbDoorType.Enabled = false;
            nudDoorKeyVNum.Enabled = false;

            bool enabledFlag;
            enabledFlag = lvMainList.SelectedItems.Count == 0
                              ? SelectedRooms.Count == 1
                              : lvMainList.SelectedItems.Count == 1;

            #region Установка ридонли

            gboxExits.Enabled = enabledFlag;
            //ОБЪЕКТЫ
            splitContainerRoomObjects.Enabled = enabledFlag;
            /*lvObjectsInRoom.Enabled = DisabledFlag;
            btnAddObjInRoom.Enabled = DisabledFlag;
            btnEditObjInRoom.Enabled = DisabledFlag;
            btnRemoveObjFromRoom.Enabled = DisabledFlag;
            lvObjInObj.Enabled = DisabledFlag;
            btnAddObiInObj.Enabled = DisabledFlag;
            btnEditObjInObj.Enabled = DisabledFlag;
            btnRomoveObjFromObj.Enabled = DisabledFlag;*/
            //МОБЫ
            splitContainerRoomMobs.Enabled = enabledFlag;
            /*lvMobsInRoom.Enabled = DisabledFlag;
            btnAddMobInRoom.Enabled = DisabledFlag;
            btnEditMobInRoom.Enabled = DisabledFlag;
            btnRemoveMobFromRoom.Enabled = DisabledFlag;
            cboxMobFollowBy.Enabled = DisabledFlag;
            nudMaxInRoom.Enabled = DisabledFlag;
            lvObjInMob.Enabled = DisabledFlag;
            btnAddObjInMob.Enabled = DisabledFlag;
            btnEditObjInMob.Enabled = DisabledFlag;
            btnRemoveObjFromMob.Enabled = DisabledFlag;*/
            //ТРИГГЕРЫ
            lvRoomTriggers.Enabled = enabledFlag;
            btnAddRoomTrigger.Enabled = enabledFlag;
            btnRemoveRoomTrigger.Enabled = enabledFlag;
            //УДАЛЯЕМЫЕ ОБЪЕКТЫ
            lvObjectsToRemove.Enabled = enabledFlag;
            btnAddRoomObjectToRemove.Enabled = enabledFlag;
            btnRemoveRoomObjectToRemove.Enabled = enabledFlag;
            //ДОП.ОПИСАНИЯ
            tbRoomAddDescAliases.Enabled = enabledFlag;
            cbRoomAddDescWordwrap.Enabled = enabledFlag;
            rtbRoomAddDescText.Enabled = enabledFlag;
            btnAddRoomAddDesc.Enabled = enabledFlag;
            btnRemoveRoomAddDesc.Enabled = enabledFlag;
            lvRoomAddDescriptions.Enabled = enabledFlag;
            //ДВЕРИ
            pDoors.Enabled = enabledFlag;

            #endregion
        }

        private void TcRoomSelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshSelectedRoomTabData();
        }

        private void WldMapRoomsSelectionChanged(SelectedRoomsCollection rooms)
        {
            if (tcMain.SelectedIndex != 1) return;
            if (rooms.Count == 0)
            {
                if (tcMain.SelectedTab.Name == "tpRooms")
                {
                    lvMainList.SelectedItems.Clear();
                    splitContainerRooms.Panel1.Enabled = false;
                    splitContainerRooms.Panel2.Enabled = false;
                    splitContainerRoomsDesc.Panel2.Enabled = false;
                }
                return;
            }
            if (tcMain.SelectedTab.Name == "tpRooms")
            {
                splitContainerRooms.Panel1.Enabled = (rooms.Count > 0 || lvMainList.SelectedItems.Count > 0);
                splitContainerRooms.Panel2.Enabled = splitContainerRooms.Panel1.Enabled;
                splitContainerRoomsDesc.Panel2.Enabled = splitContainerRooms.Panel1.Enabled;
                nudMaxInRoom.Value = 1;
                nudMaxInWorldForRoom.Value = 1;
                elvRoomMobObjects.Items.Clear();
                cboxMobFollowBy.Items.Clear();
            }
            lvMainList.BeginUpdate();
            lvMainList.SelectedItems.Clear();

            if (lvMainList.Items.Count > 0)
                foreach (ListViewItem lvi in lvMainList.Items)
                {
                    lvi.Selected = false;
                }
            SelectedRooms.Clear();

            _canDolvMainListSelectedIndexChanged = false;
            foreach (int r in rooms)
            {
                foreach (ListViewItem lvi in lvMainList.Items)
                {
                    if (lvi.Tag.ToString() == r.ToString())
                        lvi.Selected = true;
                }
                SelectedRooms.Add(ZoneDM.RoomsCollection[r, 0]);
            }
            if (lvMainList.SelectedItems.Count > 0)
                lvMainList.TopItem = lvMainList.SelectedItems[0];
            _canDolvMainListSelectedIndexChanged = true;
            LvMainListSelectedIndexChanged(lvMainList, null);
            ActiveRoom = ZoneDM.RoomsCollection[((int)(rooms[0])), 0];
            //lastFirstItemTag = ActiveRoom.VNum;
            if (lvMainList.Items.Count == 0)
                SetRoomReadOnly();
            lvMainList.EndUpdate();
            RefreshRoomData();
        }

        private void BtnAddRoomTriggerClick(object sender, EventArgs e)
        {
            if (WindowParentForm == null) return;
            CTriggersCollection allTriggers = WindowParentForm.GetAllKnownTriggers(2);
            var tsf =
                new TrgSelectForm("Выберите триггеры для комнаты", allTriggers, ZoneDM.Zone.Number, true, false);
            DialogResult dres = tsf.ShowDialog();
            if (dres == DialogResult.OK)
            {
                if (ActiveRoom != null)
                {
                    //CRoom Room = ZoneDM.RoomsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                    foreach (Trigger trigger in tsf.SelectedTriggers)
                        ActiveRoom.TriggersList.Add(trigger.VNum);
                    RefreshRoomTriggersList(ActiveRoom);
                    wldMap.RecreateRoomBitmap(ActiveRoom);
                    wldMap.RedrawBitmap();
                }
            }
            tsf.Dispose();
        }

        private void BtnRemoveRoomTriggerClick(object sender, EventArgs e)
        {
            if (ActiveRoom == null) return;
            //CRoom Room = ZoneDM.RoomsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            foreach (ListViewItem lvi in lvRoomTriggers.SelectedItems)
                ActiveRoom.TriggersList.Remove(Convert.ToInt32(lvi.Tag));
            wldMap.RecreateRoomBitmap(ActiveRoom);
            wldMap.RedrawBitmap();
            RefreshRoomTriggersList(ActiveRoom);
            if (lvRoomTriggers.Items.Count <= 0) return;
            lvRoomTriggers.Items[lvRoomTriggers.Items.Count - 1].Selected = true;
        }

        private void BtnRoomAddObjClick(object sender, EventArgs e)
        {
            if (WindowParentForm == null) return;
            ObjsCollection allObjects = WindowParentForm.GetAllKnownObjects();
            var osf = new ObjSelectForm("Выберите предмет", allObjects, ZoneDM.Zone.Number, false, false);
            DialogResult dres = osf.ShowDialog();
            if (dres == DialogResult.OK)
            {
                if (ActiveRoom != null)
                {
                    //CRoom Room = ZoneDM.RoomsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                    if (!ActiveRoom.LoadedObjectsCollection.Contains(((Object)(osf.SelectedObjects[0])).VNum))
                        ActiveRoom.LoadedObjectsCollection.Add(((Object)(osf.SelectedObjects[0])).VNum, true, /*1,*/
                                                               100);
                    RefreshRoomObjectsList(ActiveRoom);
                    wldMap.RecreateRoomBitmap(ActiveRoom);
                    wldMap.RedrawBitmap();
                }
            }
            osf.Dispose();
        }

        private void BtnRoomRemoveObjClick(object sender, EventArgs e)
        {
            if (ActiveRoom == null) return;
            //CRoom Room = ZoneDM.RoomsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            foreach (EXListViewItem elvi in elvObjectsInRoom.SelectedItems)
            {
                ActiveRoom.LoadedObjectsCollection.RemooveObj(Convert.ToInt32(elvi.SubItems[1].Text));
                wldMap.RecreateRoomBitmap(ActiveRoom);
                wldMap.RedrawBitmap();
            }
            RefreshRoomObjectsList(ActiveRoom);
            if (elvObjectsInRoom.Items.Count <= 0)
            {
                gbObjInObj.Enabled = false;
                return;
            }
            elvObjectsInRoom.Items[elvObjectsInRoom.Items.Count - 1].Selected = true;
        }

        private void ElvObjectsInRoomItemValueChanged(ListViewItem item, int subItemNum, string prevVal, string newVal)
        {
            //if (lvMainList.SelectedItems.Count <= 0) return;
            //CRoom Room = ZoneDM.RoomsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            if (ActiveRoom == null) return;
            ActiveRoom.LoadedObjectsCollection.ReplaceObjProb(Convert.ToInt32(item.SubItems[1].Text),
                                                              Convert.ToInt32(prevVal), Convert.ToInt32(newVal));
            RefreshRoomObjectsList(ActiveRoom);
        }

        private void BtnRoomAddObjInObjClick(object sender, EventArgs e)
        {
            //if (lvMainList.SelectedItems.Count <= 0) return;
            //CRoom Room = ZoneDM.RoomsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            if (ActiveRoom == null) return;
            if (WindowParentForm == null) return;
            ObjsCollection allObjects = WindowParentForm.GetAllKnownObjects();
            var osf = new ObjSelectForm("Выберите предмет", allObjects, ZoneDM.Zone.Number, false, false);
            DialogResult dres = osf.ShowDialog();
            if (dres == DialogResult.OK)
            {
                LoadedObj lObj =
                    ActiveRoom.LoadedObjectsCollection[
                        Convert.ToInt32(elvObjectsInRoom.SelectedItems[0].SubItems[1].Text), 0];
                if (lObj != null)
                {
                    lObj.ObjectsInObject.Add(((Object)(osf.SelectedObjects[0])).VNum, true, /*1,*/ 100);
                    RefreshObjInObjList(lObj);
                }
            }
            osf.Dispose();
        }

        private void BtnRoomRemoveObjFromObjClick(object sender, EventArgs e)
        {
            if (elvObjectsInRoom.SelectedItems.Count <= 0) return;
            if (ActiveRoom == null) return;
            LoadedObj roomObj =
                ActiveRoom.LoadedObjectsCollection[
                    Convert.ToInt32(elvObjectsInRoom.SelectedItems[0].SubItems[1].Text), 0];
            foreach (EXListViewItem elvi in elvRoomObjInObj.SelectedItems)
                roomObj.ObjectsInObject.RemooveObj(Convert.ToInt32(elvi.SubItems[1].Text));
            RefreshObjInObjList(roomObj);
            if (elvRoomObjInObj.Items.Count <= 0)
                return;
            elvRoomObjInObj.Items[elvRoomObjInObj.Items.Count - 1].Selected = true;
        }

        private void ElvObjectsInRoomClick(object sender, EventArgs e)
        {
        }

        private void ElvObjectsInRoomSelectedIndexChanged(object sender, EventArgs e)
        {
            gbObjInObj.Enabled = false;
            elvRoomObjInObj.Items.Clear();
            if (elvObjectsInRoom.SelectedItems.Count <= 0 || WindowParentForm == null)
                return;
            //Загрузка списка объектов выбранного объекта по тагу
            ObjsCollection allObjects = WindowParentForm.GetAllKnownObjects();
            //Проверка, в контейнер ли добавляем объект
            Obj lObj = allObjects[Convert.ToInt32(elvObjectsInRoom.SelectedItems[0].SubItems[1].Text), 0];
            if (lObj != null)
            {
                if (lObj.Type == 15)
                    gbObjInObj.Enabled = true;
                else
                {
                    gbObjInObj.Enabled = false;
                    return;
                }
            }
            else
            {
                gbObjInObj.Enabled = false;
                return;
            }

            gbObjInObj.Text = "Объекты загружаемые в объект [" + elvObjectsInRoom.SelectedItems[0].SubItems[1].Text +
                              "] " + elvObjectsInRoom.SelectedItems[0].SubItems[1].Text;
            if (ActiveRoom != null)
            {
                RefreshObjInObjList(
                    ActiveRoom.LoadedObjectsCollection[
                        Convert.ToInt32(elvObjectsInRoom.SelectedItems[0].SubItems[1].Text), 0]);
            }
        }

        private void ElvRoomObjInObjItemValueChanged(ListViewItem item, int subItemNum, string prevVal, string newVal)
        {
            if (elvObjectsInRoom.SelectedItems.Count <= 0) return;
            if (ActiveRoom == null) return;
            LoadedObj roomObj =
                ActiveRoom.LoadedObjectsCollection[
                    Convert.ToInt32(elvObjectsInRoom.SelectedItems[0].SubItems[1].Text), 0];
            if (roomObj == null) return;
            roomObj.ObjectsInObject.ReplaceObjProb(Convert.ToInt32(item.SubItems[1].Text), Convert.ToInt32(prevVal),
                                                   Convert.ToInt32(newVal));
            RefreshObjInObjList(roomObj);
        }

        private void TplRoomFlagsValueChanged(object args)
        {
            if (ActiveRoom == null) return;
            ActiveRoom.Flags = ((string)args);
            wldMap.RecreateRoomBitmap(ActiveRoom);
            wldMap.RedrawBitmap();
        }

        private void TplRoomFlagsAdded(string[] val)
        {
            AddRoomFlags(val);
        }

        private void TplRoomFlagsRemoved(string[] val)
        {
            RemoveRoomFlags(val);
        }

        private void TplRoomFlagsSelectionChanged(object args)
        {
            if (!cbShowRoomsWithFlags.Checked) return;
            wldMap.HighlightedRooms.Clear();
            foreach (Room room in ZoneDM.RoomsCollection)
            {
                if (room.Flags.Contains(args.ToString()))
                    wldMap.HighlightedRooms.AddRoom(room.VNum);
            }
            wldMap.RedrawBitmap();
        }

        private void CbShowRoomsWithFlagsCheckedChanged(object sender, EventArgs e)
        {
            if (cbShowRoomsWithFlags.Checked) return;
            wldMap.HighlightedRooms.Clear();
            wldMap.RedrawBitmap();
        }

        private void FormatRoomDescription(bool oneParagraph)
        {
            var tf = new TextFormater(StaticData.OptimalTextWidth);
            rtbRoomDesc.Text = tf.GetFormatedText(rtbRoomDesc.Text, oneParagraph, cbAllowHyp.Checked,
                                                  cbInsertSpaces.Checked);
            SetRoomDescription();
        }

        private void LvMobsInRoomSelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvMobsInRoom.SelectedItems.Count <= 0)
            {
                splitContainerRoomMobs.Panel2.Enabled = false;
                nudMaxInRoom.Value = 1;
                nudMaxInWorldForRoom.Value = 1;
                nudMaxInRoom.Enabled = false;
                nudMaxInWorldForRoom.Enabled = false;
                elvRoomMobObjects.Items.Clear();
                cboxMobFollowBy.Items.Clear();
                return;
            }
            nudMaxInRoom.Enabled = true;
            nudMaxInWorldForRoom.Enabled = true;
            _mustUpdateMobInRoomData = false;
            cboxMobFollowBy.BeginUpdate();
            splitContainerRoomMobs.Panel2.Enabled = true;
            if (ActiveRoom != null)
            {
                LoadedMob lMob =
                    ActiveRoom.LoadedMobsCollection[((EXListViewItem)(lvMobsInRoom.SelectedItems[0])).GUID];

                //Готовим список лидеров
                cboxMobFollowBy.Items.Add(new TaggedComboBoxItem(-1, "[-1] Сам по себе", -1));
                for (int i = 0; i < lvMobsInRoom.Items.Count; i++)
                {
                    ListViewItem lvi = lvMobsInRoom.Items[i];
                    if (lvi.Selected) continue;
                    var tcbi =
                        new TaggedComboBoxItem(lvi.Tag, "[" + lvi.Text + "] " + lvi.SubItems[1].Text, i);
                    cboxMobFollowBy.Items.Add(tcbi);
                }
                SetCBoxsSelectedItem(cboxMobFollowBy, lMob.FollowsBy);
                //Отключено из за нелепости параметра максимум_в_мире
                /*nudMaxInRoom.Maximum = 10000;
                if (Mob != null)
                {
                    nudMaxInRoom.Maximum = Mob.MaxInWorld;
                }
                else if (LMob != null)
                {
                    nudMaxInRoom.Maximum = LMob.MaxInWorld;
                }
                nudMaxInRoom.Value = LMob.MaxInRoom;*/
                RefreshMobsObjList(lMob);
            }
            cboxMobFollowBy.EndUpdate();
            _mustUpdateMobInRoomData = true;
        }

        private void ElvRoomMobObjectsItemValueChanged(ListViewItem item, int number, string prevVal, string newVal)
        {
            if (ActiveRoom == null) return;
            LoadedMob lMob = ActiveRoom.LoadedMobsCollection[((EXListViewItem)(lvMobsInRoom.SelectedItems[0])).GUID];
            //CLoadedMob lMob = ActiveRoom.LoadedMobsCollection[Convert.ToInt32(lvMobsInRoom.SelectedItems[0].Tag), 0];
            switch (number)
            {
                case 0:
                    lMob.Items.ReplaceObjProb(Convert.ToInt32(item.SubItems[1].Text), Convert.ToInt32(prevVal),
                                                Convert.ToInt32(newVal));
                    break;
                case 3:
                    int newV = BasesDM.GetObjPosNumByName(newVal);
                    if (CanSetPosition(newV, Convert.ToInt32(item.SubItems[1].Text)))
                        lMob.Items.ReplaceObjPos(Convert.ToInt32(item.SubItems[1].Text), BasesDM.GetObjPosNumByName(prevVal), newV);
                    RefreshMobsObjList(lMob);
                    break;
            }
        }

        /// <summary>
        /// Проверить, можно ли это экипировать туда куда пытаюсь экипировать
        /// Не запрещать выбирать любую позицию если объект не найден в списке и нет возможности проверить
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="vNum"></param>
        /// <returns></returns>
        private bool CanSetPosition(int pos, int vNum)
        {
            if (WindowParentForm == null) return true;
            ObjsCollection allObjects = WindowParentForm.GetAllKnownObjects();
            Obj curObject = allObjects[vNum, 0];
            if (curObject == null) return true;
            switch (pos)
            {
                case -1: //В инвентаре
                    return true;
                case 0: //Свет
                    return (curObject.Type == 1);
                case 1: //Одето на пальце правой руки
                case 2: //Одето на пальце левой руки
                    return curObject.WearFlags.ToLower().Contains("b0");
                case 3: //Первый предмет, надетый вокруг шеи
                case 4: //Второй предмет, надетый вокруг шеи
                    return curObject.WearFlags.ToLower().Contains("с0");
                case 5: //Одето на теле
                    return curObject.WearFlags.ToLower().Contains("d0");
                case 6: //Одето на голове
                    return curObject.WearFlags.ToLower().Contains("e0");
                case 7: //Одето на ногах
                    return curObject.WearFlags.ToLower().Contains("f0");
                case 8: //Одето на ступнях
                    return curObject.WearFlags.ToLower().Contains("g0");
                case 9: //Одето на кистях рук
                    return curObject.WearFlags.ToLower().Contains("h0");
                case 10: //Одето на руках
                    return curObject.WearFlags.ToLower().Contains("i0");
                case 11: //Используется как щит
                    return curObject.WearFlags.ToLower().Contains("j0");
                case 12: //Накинуто на плечи
                    return curObject.WearFlags.ToLower().Contains("k0");
                case 13: //Одето вокруг талии
                    return curObject.WearFlags.ToLower().Contains("l0");
                case 14: //Одето вокруг правого запястья
                case 15: //Одето вокруг левого запяться
                    return curObject.WearFlags.ToLower().Contains("m0");
                case 16: //Моб вооружен предметом в правой руке
                    return curObject.WearFlags.ToLower().Contains("n0");
                case 17: //Моб держит предмет в левой руке
                    return (
                               curObject.WearFlags.ToLower().Contains("o0") ||
                               curObject.Type == 2 || //Магический свиток
                               curObject.Type == 3 || //Волшебная палочка
                               curObject.Type == 10 //Магический напиток
                           );
                case 18: //Моб вооружен предметом в обеих руках
                    return curObject.WearFlags.ToLower().Contains("p0");
            }
            return true;
        }

        private void BtnRoomAddMobClick(object sender, EventArgs e)
        {
            if (WindowParentForm == null) return;
            MobsCollection allMobs = WindowParentForm.GetAllKnownMobs();
            var msf = new MobSelectForm("Выберите моба", allMobs, ZoneDM.Zone.Number, true, false);
            DialogResult dres = msf.ShowDialog();
            if (dres == DialogResult.OK)
            {
                /*if (lvMainList.SelectedItems.Count <= 0)
                    return;
                CRoom Room = ZoneDM.RoomsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];*/
                if (ActiveRoom == null) return;
                foreach (Mob mob in msf.SelectedMobs)
                {
                    ActiveRoom.LoadedMobsCollection.Add(mob.VNum, false, mob.MaxInWorld, mob.MaxInWorld);
                    //пока по умолчанию макс в комнате равно макс в мире
                }
                RefreshRoomMobsList(ActiveRoom);
                lvMobsInRoom.Items[lvMobsInRoom.Items.Count - 1].Selected = true;
                wldMap.RecreateRoomBitmap(ActiveRoom);
                wldMap.RedrawBitmap();
            }
            msf.Dispose();
        }

        private void BtnRoomRemoveMobClick(object sender, EventArgs e)
        {
            if (ActiveRoom == null) return;
            //CRoom Room = ZoneDM.RoomsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            if (lvMobsInRoom.SelectedItems.Count > 0)
            {
                foreach (LoadedMob lm in ActiveRoom.LoadedMobsCollection)
                {
                    if (lm.FollowsBy == Convert.ToInt32(lvMobsInRoom.SelectedItems[0].Tag))
                        lm.FollowsBy = -1;
                }
                ActiveRoom.LoadedMobsCollection.RemoveMob(((EXListViewItem)(lvMobsInRoom.SelectedItems[0])).GUID);
                wldMap.RecreateRoomBitmap(ActiveRoom);
                wldMap.RedrawBitmap();
            }
            RefreshRoomMobsList(ActiveRoom);
            if (lvMobsInRoom.Items.Count <= 0)
            {
                splitContainerRoomMobs.Panel2.Enabled = false;
                return;
            }
            lvMobsInRoom.Items[lvMobsInRoom.Items.Count - 1].Selected = true;
        }

        private void BtnRoomAddObjToMobClick(object sender, EventArgs e)
        {
            /*if (lvMainList.SelectedItems.Count <= 0) return;
            CRoom Room = ZoneDM.RoomsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];*/
            if (ActiveRoom == null) return;
            if (WindowParentForm == null) return;
            ObjsCollection allObjects = WindowParentForm.GetAllKnownObjects();
            var osf = new ObjSelectForm("Выберите предмет", allObjects, ZoneDM.Zone.Number, true, false);
            DialogResult dres = osf.ShowDialog();
            if (dres == DialogResult.OK)
            {
                //CLoadedMob LMob = ActiveRoom.LoadedMobsCollection[Convert.ToInt32(lvMobsInRoom.SelectedItems[0].Tag), 0];
                LoadedMob lMob =
                    ActiveRoom.LoadedMobsCollection[((EXListViewItem)(lvMobsInRoom.SelectedItems[0])).GUID];
                if (lMob != null)
                {
                    foreach (Obj selectedObject in osf.SelectedObjects)
                        lMob.Items.Add(selectedObject.VNum, true, -1, 100);
                    RefreshMobsObjList(lMob);
                }
            }
            osf.Dispose();
        }

        private void BtnRoomRomoveObjFromMobClick(object sender, EventArgs e)
        {
            if (lvMobsInRoom.SelectedItems.Count <= 0) return;
            if (ActiveRoom == null) return;
            //CLoadedMob RoomMob = ActiveRoom.LoadedMobsCollection[Convert.ToInt32(lvMobsInRoom.SelectedItems[0].Tag), 0];
            LoadedMob roomMob =
                ActiveRoom.LoadedMobsCollection[((EXListViewItem)(lvMobsInRoom.SelectedItems[0])).GUID];
            foreach (EXListViewItem elvi in elvRoomMobObjects.SelectedItems)
                roomMob.Items.RemoveObj(elvi.GUID);
            RefreshMobsObjList(roomMob);
            if (elvRoomMobObjects.Items.Count <= 0)
                return;
            elvRoomMobObjects.Items[elvRoomMobObjects.Items.Count - 1].Selected = true;
        }

        private void NudMaxInRoomValidated(object sender, EventArgs e)
        {
            if (!_mustUpdateMobInRoomData) return;
            if (ActiveRoom == null) return;
            if (lvMobsInRoom.SelectedItems.Count == 0) return;
            //CLoadedMob LMob = ActiveRoom.LoadedMobsCollection[Convert.ToInt32(lvMobsInRoom.SelectedItems[0].Tag), 0];
            LoadedMob lMob = ActiveRoom.LoadedMobsCollection[((EXListViewItem)(lvMobsInRoom.SelectedItems[0])).GUID];
            lMob.MaxInRoom = Convert.ToInt32(nudMaxInRoom.Value);
        }

        private void NudMaxInWorldForRoomValidated(object sender, EventArgs e)
        {
            if (!_mustUpdateMobInRoomData) return;
            if (ActiveRoom == null) return;
            if (lvMobsInRoom.SelectedItems.Count == 0) return;
            //CLoadedMob LMob = ActiveRoom.LoadedMobsCollection[Convert.ToInt32(lvMobsInRoom.SelectedItems[0].Tag), 0];
            LoadedMob lMob = ActiveRoom.LoadedMobsCollection[((EXListViewItem)(lvMobsInRoom.SelectedItems[0])).GUID];
            lMob.MaxInWorld = Convert.ToInt32(nudMaxInWorldForRoom.Value);
        }

        private void BtnAddRoomObjectToRemoveClick(object sender, EventArgs e)
        {
            if (ActiveRoom == null) return;
            if (WindowParentForm == null) return;
            ObjsCollection allObjects = WindowParentForm.GetAllKnownObjects();
            var osf =
                new ObjSelectForm("Выберите предмет для удаления", allObjects, ZoneDM.Zone.Number, true, false);
            DialogResult dres = osf.ShowDialog();
            if (dres == DialogResult.OK)
            {
                foreach (Obj selectedObject in osf.SelectedObjects)
                    ActiveRoom.RemoovingObjects.Add(selectedObject.VNum, false, /*-1,*/ -1);
                RefreshRoomRemovingObjectsList(ActiveRoom);
            }
            osf.Dispose();
        }

        private void BtnRemoveRoomObjectToRemoveClick(object sender, EventArgs e)
        {
            if (ActiveRoom == null) return;
            foreach (ListViewItem lvi in lvObjectsToRemove.SelectedItems)
                ActiveRoom.RemoovingObjects.RemooveObj(Convert.ToInt32(lvi.Tag));
            RefreshRoomRemovingObjectsList(ActiveRoom);
            if (lvObjectsToRemove.Items.Count <= 0) return;
            lvObjectsToRemove.Items[lvObjectsToRemove.Items.Count - 1].Selected = true;
        }

        private void CbRoomAddDescWordwrapCheckedChanged(object sender, EventArgs e)
        {
            rtbRoomAddDescText.WordWrap = cbRoomAddDescWordwrap.Checked;
        }

        private void BtnAddRoomAddDescClick(object sender, EventArgs e)
        {
            if (tbRoomAddDescAliases.Text.Length <= 0 || rtbRoomAddDescText.Text.Length <= 0)
                return;
            //CRoom Room = ZoneDM.RoomsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            if (ActiveRoom == null) return;
            ActiveRoom.ExtraDescriptions.Add(tbRoomAddDescAliases.Text, rtbRoomAddDescText.Text);
            tbRoomAddDescAliases.Text = "";
            rtbRoomAddDescText.Text = "";
            RefreshRoomAddDescList(ActiveRoom);
        }

        private void BtnRemoveRoomAddDescClick(object sender, EventArgs e)
        {
            if (lvRoomAddDescriptions.SelectedItems.Count <= 0) return;
            //CRoom Room = ZoneDM.RoomsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            if (ActiveRoom == null) return;
            ActiveRoom.ExtraDescriptions.Remove(lvRoomAddDescriptions.SelectedItems[0].SubItems[0].Text);
            RefreshRoomAddDescList(ActiveRoom);
        }

        private void LvRoomAddDescriptionsDoubleClick(object sender, EventArgs e)
        {
            if (lvRoomAddDescriptions.SelectedItems.Count <= 0) return;
            tbRoomAddDescAliases.Text = lvRoomAddDescriptions.SelectedItems[0].SubItems[0].Text;
            rtbRoomAddDescText.Text = lvRoomAddDescriptions.SelectedItems[0].SubItems[1].Text;
        }

        private void BSelectExitNorthClick(object sender, EventArgs e)
        {
            if (wldMap.ExternalRoomRoomSelection) return;
            wldMap.RoomSelected += WldMapExitNorthSelected;
            wldMap.ExternalRoomRoomSelection = true;
        }

        private void WldMapExitNorthSelected(int vNum)
        {
            tbExitNorth.Text = vNum.ToString();
            ExitDirChanged(tbExitNorth, null);
            wldMap.RoomSelected -= WldMapExitNorthSelected;
        }

        private void BSelectExitSouthClick(object sender, EventArgs e)
        {
            if (wldMap.ExternalRoomRoomSelection) return;
            wldMap.RoomSelected += WldMapExitSouthSelected;
            wldMap.ExternalRoomRoomSelection = true;
        }

        private void WldMapExitSouthSelected(int vNum)
        {
            tbExitSouth.Text = vNum.ToString();
            ExitDirChanged(tbExitSouth, null);
            wldMap.RoomSelected -= WldMapExitSouthSelected;
        }

        private void BSelectExitEastClick(object sender, EventArgs e)
        {
            if (wldMap.ExternalRoomRoomSelection) return;
            wldMap.RoomSelected += WldMapExitEastSelected;
            wldMap.ExternalRoomRoomSelection = true;
        }

        private void WldMapExitEastSelected(int vNum)
        {
            tbExitEast.Text = vNum.ToString();
            ExitDirChanged(tbExitEast, null);
            wldMap.RoomSelected -= WldMapExitEastSelected;
        }

        private void BSelectExitWestClick(object sender, EventArgs e)
        {
            if (wldMap.ExternalRoomRoomSelection) return;
            wldMap.RoomSelected += WldMapExitWestSelected;
            wldMap.ExternalRoomRoomSelection = true;
        }

        private void WldMapExitWestSelected(int vNum)
        {
            tbExitWest.Text = vNum.ToString();
            ExitDirChanged(tbExitWest, null);
            wldMap.RoomSelected -= WldMapExitWestSelected;
        }

        private void BSelectExitUpClick(object sender, EventArgs e)
        {
            if (wldMap.ExternalRoomRoomSelection) return;
            wldMap.RoomSelected += WldMapExitUpSelected;
            wldMap.ExternalRoomRoomSelection = true;
        }

        private void WldMapExitUpSelected(int vNum)
        {
            tbExitUp.Text = vNum.ToString();
            ExitDirChanged(tbExitUp, null);
            wldMap.RoomSelected -= WldMapExitUpSelected;
        }

        private void BSelectExitDownClick(object sender, EventArgs e)
        {
            if (wldMap.ExternalRoomRoomSelection) return;
            wldMap.RoomSelected += WldMapExitDownSelected;
            wldMap.ExternalRoomRoomSelection = true;
        }

        private void WldMapExitDownSelected(int vNum)
        {
            tbExitDown.Text = vNum.ToString();
            ExitDirChanged(tbExitDown, null);
            wldMap.RoomSelected -= WldMapExitDownSelected;
        }

        private void ExitDirChanged(object sender, EventArgs e)
        {
            if (IgnoreExitDirChanged) return;
            if (ActiveRoom == null) return;
            int newVNum = (((TextBox)sender).Text.Length > 0) ? Convert.ToInt32(((TextBox)sender).Text) : -1;
            Room trgRoom = ZoneDM.RoomsCollection[newVNum, 0];
            if (tsbSetOppositeExit.Checked && trgRoom != null)
            {
                var sbdf = new SelectBackDirectionForm(trgRoom);
                DialogResult dres = sbdf.ShowDialog();
                if (dres == DialogResult.OK)
                {
                    switch (sbdf.Res)
                    {
                        case "North":
                            trgRoom.ExitNorth.RoomVNum = ActiveRoom.VNum;
                            break;
                        case "South":
                            trgRoom.ExitSouth.RoomVNum = ActiveRoom.VNum;
                            break;
                        case "West":
                            trgRoom.ExitWest.RoomVNum = ActiveRoom.VNum;
                            break;
                        case "East":
                            trgRoom.ExitEast.RoomVNum = ActiveRoom.VNum;
                            break;
                        case "Up":
                            trgRoom.ExitUp.RoomVNum = ActiveRoom.VNum;
                            break;
                        case "Down":
                            trgRoom.ExitDown.RoomVNum = ActiveRoom.VNum;
                            break;
                    }
                    wldMap.RecreateRoomBitmap(trgRoom);
                }
                /*else
                    return; //Выход если нажата кнопка ОТМЕНА*/
                sbdf.Dispose();
            }
            Exit exit = null;
            switch (((TextBox)sender).Name)
            {
                case "tbExitNorth":
                    exit = ActiveRoom.ExitNorth;
                    break;
                case "tbExitSouth":
                    exit = ActiveRoom.ExitSouth;
                    break;
                case "tbExitEast":
                    exit = ActiveRoom.ExitEast;
                    break;
                case "tbExitWest":
                    exit = ActiveRoom.ExitWest;
                    break;
                case "tbExitUp":
                    exit = ActiveRoom.ExitUp;
                    break;
                case "tbExitDown":
                    exit = ActiveRoom.ExitDown;
                    break;
            }
            if (exit != null && exit.RoomVNum != newVNum)
            {
                exit.RoomVNum = newVNum;
                wldMap.RecreateRoomBitmap(ActiveRoom);
                wldMap.RedrawBitmap();
            }
            RefreshExitsDirections(ActiveRoom);
        }

        private void TbRoomNameValidated(object sender, EventArgs e)
        {
            if (ActiveRoom == null) return;
            foreach (Room r in SelectedRooms)
            {
                r.Name = tbRoomName.Text;
                UpdateRoomInList(r);
            }
            lRoomDesc.Text = "[" + ActiveRoom.VNum + "] " + ActiveRoom.Name;
            //RefreshRoomsList();
            //ReselectMainListRooms();
        }

        private void CboxSectorTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ActiveRoom == null) return;
            if (cboxSectorType.SelectedItem == null) return;
            foreach (Room r in SelectedRooms)
                r.SectorType = Convert.ToInt32(((TaggedComboBoxItem)(cboxSectorType.SelectedItem)).Tag);
        }

        private void BtnSelectDoorKeyClick(object sender, EventArgs e)
        {
            if (WindowParentForm == null) return;
            ObjsCollection allObjects = WindowParentForm.GetAllKnownObjects(18); //Только ключи
            var osf = new ObjSelectForm("Выберите ключ", allObjects, ZoneDM.Zone.Number, false, false);
            DialogResult dres = osf.ShowDialog();
            if (dres == DialogResult.OK)
            {
                nudDoorKeyVNum.Value = ((Object)osf.SelectedObjects[0]).VNum;
                tbRoomDoorKeyName.Text = ((Object)osf.SelectedObjects[0]).Cases.Imen;
            }
            osf.Dispose();
        }

        private void NudDoorKeyVNumValueChanged(object sender, EventArgs e)
        {
            if (WindowParentForm == null) return;
            ObjsCollection allObjects = WindowParentForm.GetAllKnownObjects(18); //Только ключи
            Obj keyObject = allObjects[Convert.ToInt32(nudDoorKeyVNum.Value), 0];
            if (nudDoorKeyVNum.Value == -1)
                tbRoomDoorKeyName.Text = "Ключ здесь не надо";
            else tbRoomDoorKeyName.Text = keyObject != null ? keyObject.Cases.Imen : "!!!Ключа с таким номером не найдено.";
            /*if (lvMainList.SelectedItems.Count <= 0) return;
            CRoom Room = ZoneDM.RoomsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];*/
            if (ActiveRoom == null) return;
            switch (_exitDir)
            {
                case "btnConfigExitNorth":
                    ActiveRoom.ExitNorth.Key = Convert.ToInt32(nudDoorKeyVNum.Value);
                    break;
                case "btnConfigExitSouth":
                    ActiveRoom.ExitSouth.Key = Convert.ToInt32(nudDoorKeyVNum.Value);
                    break;
                case "btnConfigExitEast":
                    ActiveRoom.ExitEast.Key = Convert.ToInt32(nudDoorKeyVNum.Value);
                    break;
                case "btnConfigExitWest":
                    ActiveRoom.ExitWest.Key = Convert.ToInt32(nudDoorKeyVNum.Value);
                    break;
                case "btnConfigExitUp":
                    ActiveRoom.ExitUp.Key = Convert.ToInt32(nudDoorKeyVNum.Value);
                    break;
                case "btnConfigExitDown":
                    ActiveRoom.ExitDown.Key = Convert.ToInt32(nudDoorKeyVNum.Value);
                    break;
            }
        }

        private void DoorDirectionSelect(object sender, EventArgs e)
        {
            tbDoorAlias.Enabled = true;
            tbDoorAlias.Text = string.Empty;
            tbDoorNameVin.Enabled = true;
            tbDoorNameVin.Text = string.Empty;
            tbDoorDesc.Enabled = true;
            tbDoorDesc.Text = string.Empty;
            gbDoorType.Enabled = true;
            nudDoorKeyVNum.Enabled = true;
            btnConfigExitNorth.Enabled = true;
            btnConfigExitSouth.Enabled = true;
            btnConfigExitWest.Enabled = true;
            btnConfigExitEast.Enabled = true;
            btnConfigExitUp.Enabled = true;
            btnConfigExitDown.Enabled = true;
            _blockApplying = true;
            cbExitVisible.Checked = false;
            cbExitHidden.Checked = false;
            cbExitDoor.Checked = false;
            cbDoorClosed.Checked = false;
            cbDoorLocked.Checked = false;
            cbDoorPeekproof.Checked = false;
            _blockApplying = false;
            if (ActiveRoom == null) return;
            ((Button)sender).Enabled = false;
            _exitDir = ((Control)sender).Name;
            Exit curExit = null;
            switch (_exitDir)
            {
                case "btnConfigExitNorth":
                    curExit = ActiveRoom.ExitNorth;
                    break;
                case "btnConfigExitSouth":
                    curExit = ActiveRoom.ExitSouth;
                    break;
                case "btnConfigExitEast":
                    curExit = ActiveRoom.ExitEast;
                    break;
                case "btnConfigExitWest":
                    curExit = ActiveRoom.ExitWest;
                    break;
                case "btnConfigExitUp":
                    curExit = ActiveRoom.ExitUp;
                    break;
                case "btnConfigExitDown":
                    curExit = ActiveRoom.ExitDown;
                    break;
            }
            if (curExit == null) return;
            tbDoorAlias.Text = curExit.Aliases;
            tbDoorNameVin.Text = curExit.ExinNameVin;
            tbDoorDesc.Text = curExit.Description;
            SetDoorTypeAndStateFlags(curExit);
            nudDoorKeyVNum.Value = curExit.Key;
            nudLockLevel.Value = curExit.LockLevel;
        }

        public void SetDoorTypeAndStateFlags(Exit exit)
        {
            _blockApplying = true;
            cbExitDoor.Checked = false;
            cbDoorPeekproof.Checked = false;
            cbExitHidden.Checked = false;
            int flagval = 16;
            int flag = exit.ExitFlag;
            while (flag > 0)
            {
                if (flag - flagval >= 0)
                {
                    switch (flagval)
                    {
                        case 1:
                            cbExitDoor.Checked = true;
                            break;
                        case 8:
                            cbDoorPeekproof.Checked = true;
                            break;
                        case 16:
                            cbExitHidden.Checked = true;
                            break;
                    }
                    flag -= flagval;
                }
                flagval = (flagval > 1) ? flagval / 2 : 0;
            }
            if (exit.Visibility == 3)
                cbExitHidden.Checked = true;
            else if (exit.Visibility == 4)
                cbExitVisible.Checked = true;
            if (exit.DoorState == 1)
                cbDoorClosed.Checked = true;
            else if (exit.DoorState == 2)
                cbDoorLocked.Checked = true;
            _blockApplying = false;
        }

        private void ExitHiddenCheckedChanged(object sender, EventArgs e)
        {
            if (cbExitHidden.Checked)
                cbExitVisible.Checked = false;
            ApplyDoorTypeAndDefStateFlags();
        }

        private void ExitVisibleCheckedChanged(object sender, EventArgs e)
        {
            if (cbExitVisible.Checked)
                cbExitHidden.Checked = false;
            ApplyDoorTypeAndDefStateFlags();
        }

        private bool dontUpdateExit;

        private void ExitDoorCheckedChanged(object sender, EventArgs e)
        {
            ApplyDoorTypeAndDefStateFlags();
            if (!cbExitDoor.Checked)
            {
                cbDoorClosed.Checked = false;
                dontUpdateExit = true;
                nudLockLevel.Value = 0;
                dontUpdateExit = false;
            }
        }

        private void DoorClosedCheckedChanged(object sender, EventArgs e)
        {
            if (cbDoorClosed.Checked)
                cbExitDoor.Checked = true;
            else
                cbDoorLocked.Checked = false;
            ApplyDoorTypeAndDefStateFlags();
        }

        private void DoorLockedCheckedChanged(object sender, EventArgs e)
        {
            if (cbDoorLocked.Checked)
                cbDoorClosed.Checked = true;
            else
                cbDoorPeekproof.Checked = false;
            ApplyDoorTypeAndDefStateFlags();
        }

        private void DoorPeekproofCheckedChanged(object sender, EventArgs e)
        {
            if (cbDoorPeekproof.Checked)
                cbDoorLocked.Checked = true;
            ApplyDoorTypeAndDefStateFlags();
        }

        private void LockLevelValueChanged(object sender, EventArgs e)
        {
            if (dontUpdateExit) return;
            Exit curExit = null;
            switch (_exitDir)
            {
                case "btnConfigExitNorth":
                    curExit = ActiveRoom.ExitNorth;
                    break;
                case "btnConfigExitSouth":
                    curExit = ActiveRoom.ExitSouth;
                    break;
                case "btnConfigExitEast":
                    curExit = ActiveRoom.ExitEast;
                    break;
                case "btnConfigExitWest":
                    curExit = ActiveRoom.ExitWest;
                    break;
                case "btnConfigExitUp":
                    curExit = ActiveRoom.ExitUp;
                    break;
                case "btnConfigExitDown":
                    curExit = ActiveRoom.ExitDown;
                    break;
            }
            if (curExit != null)
                curExit.LockLevel = Convert.ToInt32(nudLockLevel.Value);

            if (nudLockLevel.Value > 0 && !cbExitDoor.Checked)
                cbExitDoor.Checked = true;
        }

        bool _blockApplying;

        private void ApplyDoorTypeAndDefStateFlags()
        {
            if (ActiveRoom == null || _blockApplying) return;

            if (!cbExitDoor.Checked)
                nudDoorKeyVNum.Value = -1;
            nudDoorKeyVNum.Enabled = cbExitDoor.Checked;
            btnSelectDoorKey.Enabled = cbExitDoor.Checked;

            //Обработка изменений типа и состояния по умолчанию для выхода

            int doorFlag = 0;
            int doorDefaultValue = -1;
            int doorVisibility = -1;
            if (cbExitHidden.Checked)
            {
                doorFlag += 16;
                doorVisibility = 3;
            }
            if (cbExitVisible.Checked)
                doorVisibility = 4;
            if (cbExitDoor.Checked)
            {
                doorFlag += 1;
                doorDefaultValue = 0;
            }
            if (cbDoorClosed.Checked)
                doorDefaultValue = 1;
            if (cbDoorLocked.Checked)
                doorDefaultValue = 2;
            if (cbDoorPeekproof.Checked)
                doorFlag += 8;
#if DEBUG
            gbDoorType.Text = "Тип выхода:: doorFlag|" + doorFlag + " doorDefaultValue|" + doorDefaultValue;
#endif
            Exit curExit = null;
            switch (_exitDir)
            {
                case "btnConfigExitNorth":
                    curExit = ActiveRoom.ExitNorth;
                    break;
                case "btnConfigExitSouth":
                    curExit = ActiveRoom.ExitSouth;
                    break;
                case "btnConfigExitEast":
                    curExit = ActiveRoom.ExitEast;
                    break;
                case "btnConfigExitWest":
                    curExit = ActiveRoom.ExitWest;
                    break;
                case "btnConfigExitUp":
                    curExit = ActiveRoom.ExitUp;
                    break;
                case "btnConfigExitDown":
                    curExit = ActiveRoom.ExitDown;
                    break;
            }
            if (curExit != null)
            {
                curExit.ExitFlag = Convert.ToInt32(doorFlag);
                curExit.DoorState = doorDefaultValue;
                curExit.Visibility = doorVisibility;
            }
        }

        private void TbDoorAliasValidated(object sender, EventArgs e)
        {
            if (ActiveRoom == null) return;
            string value = tbDoorAlias.Text;
            switch (_exitDir)
            {
                case "btnConfigExitNorth":
                    ActiveRoom.ExitNorth.Aliases = value;
                    break;
                case "btnConfigExitSouth":
                    ActiveRoom.ExitSouth.Aliases = value;
                    break;
                case "btnConfigExitEast":
                    ActiveRoom.ExitEast.Aliases = value;
                    break;
                case "btnConfigExitWest":
                    ActiveRoom.ExitWest.Aliases = value;
                    break;
                case "btnConfigExitUp":
                    ActiveRoom.ExitUp.Aliases = value;
                    break;
                case "btnConfigExitDown":
                    ActiveRoom.ExitDown.Aliases = value;
                    break;
            }
        }

        private void TbDoorNameVinValidated(object sender, EventArgs e)
        {
            if (ActiveRoom == null) return;
            string value = tbDoorNameVin.Text;
            switch (_exitDir)
            {
                case "btnConfigExitNorth":
                    ActiveRoom.ExitNorth.ExinNameVin = value;
                    break;
                case "btnConfigExitSouth":
                    ActiveRoom.ExitSouth.ExinNameVin = value;
                    break;
                case "btnConfigExitEast":
                    ActiveRoom.ExitEast.ExinNameVin = value;
                    break;
                case "btnConfigExitWest":
                    ActiveRoom.ExitWest.ExinNameVin = value;
                    break;
                case "btnConfigExitUp":
                    ActiveRoom.ExitUp.ExinNameVin = value;
                    break;
                case "btnConfigExitDown":
                    ActiveRoom.ExitDown.ExinNameVin = value;
                    break;
            }
        }

        private void TbDoorDescValidated(object sender, EventArgs e)
        {
            if (ActiveRoom == null) return;
            string value = tbDoorDesc.Text;
            switch (_exitDir)
            {
                case "btnConfigExitNorth":
                    ActiveRoom.ExitNorth.Description = value;
                    break;
                case "btnConfigExitSouth":
                    ActiveRoom.ExitSouth.Description = value;
                    break;
                case "btnConfigExitEast":
                    ActiveRoom.ExitEast.Description = value;
                    break;
                case "btnConfigExitWest":
                    ActiveRoom.ExitWest.Description = value;
                    break;
                case "btnConfigExitUp":
                    ActiveRoom.ExitUp.Description = value;
                    break;
                case "btnConfigExitDown":
                    ActiveRoom.ExitDown.Description = value;
                    break;
            }
        }

        private void BtnSelectPorionProtoClick(object sender, EventArgs e)
        {
            if (WindowParentForm == null) return;
            ObjsCollection allObjects = WindowParentForm.GetAllKnownObjects(10); //10.Магический напиток
            var osf =
                new ObjSelectForm("Выберите прототип напитка", allObjects, ZoneDM.Zone.Number, false, true);
            DialogResult dres = osf.ShowDialog();
            if (dres == DialogResult.OK)
            {
                switch (((Control)sender).Name)
                {
                    case "btnSelectFontPorionProto":
                        nudFontPorionProtoVNum.Value = ((Object)osf.SelectedObjects[0]).VNum;
                        break;
                    case "btnSelectPotionProtoVNum":
                        nudPotionProtoVNum.Value = ((Object)osf.SelectedObjects[0]).VNum;
                        break;
                }
            }
            osf.Dispose();
        }

        private void SetRoomDescription()
        {
            if (ActiveRoom == null) return;
            foreach (Room r in SelectedRooms)
            {
                switch (tabControlRoomDescriptions.SelectedTab.Name)
                {
                    case "tpRoomDesc":
                        r.Description.Main = rtbRoomDesc.Text;
                        break;
                    case "tpRoomDescDay":
                        r.Description.Day = rtbRoomDesc.Text;
                        break;
                    case "tpRoomDescNight":
                        r.Description.Night = rtbRoomDesc.Text;
                        break;
                    case "tpRoomDescSpringDay":
                        r.Description.SpringDay = rtbRoomDesc.Text;
                        break;
                    case "tpRoomDescSpringNight":
                        r.Description.SpringNight = rtbRoomDesc.Text;
                        break;
                    case "tpRoomDescAutumnDay":
                        r.Description.AutumnDay = rtbRoomDesc.Text;
                        break;
                    case "tpRoomDescAutumnNight":
                        r.Description.AutumnNight = rtbRoomDesc.Text;
                        break;
                    case "tpRoomDescSummerDay":
                        r.Description.SummerDay = rtbRoomDesc.Text;
                        break;
                    case "tpRoomDescSummerNight":
                        r.Description.SummerNight = rtbRoomDesc.Text;
                        break;
                    case "tpRoomDescWinterDay":
                        r.Description.WinterDay = rtbRoomDesc.Text;
                        break;
                    case "tpRoomDescWinterNight":
                        r.Description.WinterNight = rtbRoomDesc.Text;
                        break;
                }
            }
        }

        public void RefreshRoomDescription()
        {
            if (ActiveRoom == null) return;
            cbDescReplace.Visible = tabControlRoomDescriptions.SelectedTab.Name != "tpRoomDesc";
            switch (tabControlRoomDescriptions.SelectedTab.Name)
            {
                case "tpRoomDesc":
                    rtbRoomDesc.Text = ActiveRoom.Description.Main;
                    break;
                case "tpRoomDescDay":
                    rtbRoomDesc.Text = ActiveRoom.Description.Day;
                    cbDescReplace.Checked = ActiveRoom.Description.DayR;
                    break;
                case "tpRoomDescNight":
                    rtbRoomDesc.Text = ActiveRoom.Description.Night;
                    cbDescReplace.Checked = ActiveRoom.Description.NightR;
                    break;
                case "tpRoomDescSpringDay":
                    rtbRoomDesc.Text = ActiveRoom.Description.SpringDay;
                    cbDescReplace.Checked = ActiveRoom.Description.SpringDayR;
                    break;
                case "tpRoomDescSpringNight":
                    rtbRoomDesc.Text = ActiveRoom.Description.SpringNight;
                    cbDescReplace.Checked = ActiveRoom.Description.SpringNightR;
                    break;
                case "tpRoomDescAutumnDay":
                    rtbRoomDesc.Text = ActiveRoom.Description.AutumnDay;
                    cbDescReplace.Checked = ActiveRoom.Description.AutumnDayR;
                    break;
                case "tpRoomDescAutumnNight":
                    rtbRoomDesc.Text = ActiveRoom.Description.AutumnNight;
                    cbDescReplace.Checked = ActiveRoom.Description.AutumnNightR;
                    break;
                case "tpRoomDescSummerDay":
                    rtbRoomDesc.Text = ActiveRoom.Description.SummerDay;
                    cbDescReplace.Checked = ActiveRoom.Description.SummerDayR;
                    break;
                case "tpRoomDescSummerNight":
                    rtbRoomDesc.Text = ActiveRoom.Description.SummerNight;
                    cbDescReplace.Checked = ActiveRoom.Description.SummerNightR;
                    break;
                case "tpRoomDescWinterDay":
                    rtbRoomDesc.Text = ActiveRoom.Description.WinterDay;
                    cbDescReplace.Checked = ActiveRoom.Description.WinterDayR;
                    break;
                case "tpRoomDescWinterNight":
                    rtbRoomDesc.Text = ActiveRoom.Description.WinterNight;
                    cbDescReplace.Checked = ActiveRoom.Description.WinterNightR;
                    break;
            }
        }

        public void RefreshDescriptionTabsIcons()
        {
            tpRoomDesc.ImageIndex = !string.IsNullOrEmpty(ActiveRoom.Description.Main) ? 46 : -1;
            tpRoomDescDay.ImageIndex = !string.IsNullOrEmpty(ActiveRoom.Description.Day) ? 46 : -1;
            tpRoomDescNight.ImageIndex = !string.IsNullOrEmpty(ActiveRoom.Description.Night) ? 46 : -1;
            tpRoomDescSpringDay.ImageIndex = !string.IsNullOrEmpty(ActiveRoom.Description.SpringDay) ? 46 : -1;
            tpRoomDescSpringNight.ImageIndex = !string.IsNullOrEmpty(ActiveRoom.Description.SpringNight) ? 46 : -1;
            tpRoomDescAutumnDay.ImageIndex = !string.IsNullOrEmpty(ActiveRoom.Description.AutumnDay) ? 46 : -1;
            tpRoomDescAutumnNight.ImageIndex = !string.IsNullOrEmpty(ActiveRoom.Description.AutumnNight) ? 46 : -1;
            tpRoomDescSummerDay.ImageIndex = !string.IsNullOrEmpty(ActiveRoom.Description.SummerDay) ? 46 : -1;
            tpRoomDescSummerNight.ImageIndex = !string.IsNullOrEmpty(ActiveRoom.Description.SummerNight) ? 46 : -1;
            tpRoomDescWinterDay.ImageIndex = !string.IsNullOrEmpty(ActiveRoom.Description.WinterDay) ? 46 : -1;
            tpRoomDescWinterNight.ImageIndex = !string.IsNullOrEmpty(ActiveRoom.Description.WinterNight) ? 46 : -1;
        }

        public void RefreshRoomTabsData()
        {
            switch (tcRoom.SelectedTab.Name)
            {
                case "tpRoomFlags":
                    RefreshRoomFlagsList(ActiveRoom);
                    break;
                case "tpRoomDoors":
                    btnConfigExitNorth.Enabled = true;
                    btnConfigExitSouth.Enabled = true;
                    btnConfigExitWest.Enabled = true;
                    btnConfigExitEast.Enabled = true;
                    btnConfigExitUp.Enabled = true;
                    btnConfigExitDown.Enabled = true;
                    RefreshExitsDirections(ActiveRoom);
                    break;
                case "tpRoomTrgs":
                    RefreshRoomTriggersList(ActiveRoom);
                    break;
                case "tpRoomObjs":
                    RefreshRoomObjectsList(ActiveRoom);
                    break;
                case "tpRoomMobs":
                    RefreshRoomMobsList(ActiveRoom);
                    nudMaxInRoom.Enabled = false;
                    nudMaxInWorldForRoom.Enabled = false;
                    break;
                case "tpRoomDelObjs":
                    RefreshRoomRemovingObjectsList(ActiveRoom);
                    break;
                case "tpRoomAddDescrs":
                    RefreshRoomAddDescList(ActiveRoom);
                    break;
            }
            IgnoreExitDirChanged = false;
        }

        private void SetDescReplaceState()
        {
            if (ActiveRoom == null) return;
            foreach (Room r in SelectedRooms)
            {
                switch (tabControlRoomDescriptions.SelectedTab.Name)
                {
                    case "tpRoomDescDay":
                        r.Description.DayR = cbDescReplace.Checked;
                        break;
                    case "tpRoomDescNight":
                        r.Description.NightR = cbDescReplace.Checked;
                        break;
                    case "tpRoomDescSpringDay":
                        r.Description.SpringDayR = cbDescReplace.Checked;
                        break;
                    case "tpRoomDescSpringNight":
                        r.Description.SpringNightR = cbDescReplace.Checked;
                        break;
                    case "tpRoomDescAutumnDay":
                        r.Description.AutumnDayR = cbDescReplace.Checked;
                        break;
                    case "tpRoomDescAutumnNight":
                        r.Description.AutumnNightR = cbDescReplace.Checked;
                        break;
                    case "tpRoomDescSummerDay":
                        r.Description.SummerDayR = cbDescReplace.Checked;
                        break;
                    case "tpRoomDescSummerNight":
                        r.Description.SummerNightR = cbDescReplace.Checked;
                        break;
                    case "tpRoomDescWinterDay":
                        r.Description.WinterDayR = cbDescReplace.Checked;
                        break;
                    case "tpRoomDescWinterNight":
                        r.Description.WinterNightR = cbDescReplace.Checked;
                        break;
                }
            }
        }

        private void CboxMobFollowBySelectedValueChanged(object sender, EventArgs e)
        {
            if (!_mustUpdateMobInRoomData) return;
            if (ActiveRoom == null) return;
            LoadedMob lMob = ActiveRoom.LoadedMobsCollection[((EXListViewItem)(lvMobsInRoom.SelectedItems[0])).GUID];
            if (lMob == null) return;
            //ToDo: надо ли следующую строчку?
            lMob.Leader = false;
            lMob.FollowsBy = Convert.ToInt32(((TaggedComboBoxItem)(cboxMobFollowBy.SelectedItem)).Tag);
        }

        private void TsmiShowRoomOnMapClick(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count == 1)
            {
                Room room = ZoneDM.RoomsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                if (room.PlacedOnMap)
                    wldMap.CenterRoomCoord = room.Location;
                else
                    MessageBox.Show(this, "Комната \"" + room.Name + "\" не размещена на карте.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (lvMainList.SelectedItems.Count > 1)
                MessageBox.Show(this, "Перейти на карте к нескольким выбранным комнатам невозможно!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void RefreshRoomsList()
        {
            lvMainList.BeginUpdate();
            lvMainList.Items.Clear();
            foreach (Room room in ZoneDM.RoomsCollection)
            {
                if (tboxMainListFilter.Text.Length > 0)
                {
                    if (room.Name.ToUpper().IndexOf(tboxMainListFilter.Text.ToUpper()) >= 0 ||
                        room.VNum.ToString().ToUpper().IndexOf(tboxMainListFilter.Text.ToUpper()) >= 0)
                    {
                        if (cboxMainListConditions.SelectedIndex == 0 || !room.PlacedOnMap)
                        {
                            ListViewItem lvi = new ListViewItem(new[] {room.VNum.ToString(), room.Name})
                                                   {Tag = room.VNum.ToString()};
                            if (room.Modifyed)
                                lvi.ImageIndex = 47;
                            lvMainList.Items.Add(lvi);
                        }
                    }
                }
                else
                {
                    if (cboxMainListConditions.SelectedIndex == 0 || !room.PlacedOnMap)
                    {
                        ListViewItem lvi = new ListViewItem(new[] {room.VNum.ToString(), room.Name})
                                               {Tag = room.VNum.ToString()};
                        if (room.Modifyed)
                            lvi.ImageIndex = 47;
                        lvMainList.Items.Add(lvi);
                    }
                }
            }
            lvMainList.EndUpdate();
        }

        public void UpdateRoomInList(Room room)
        {
            lvMainList.BeginUpdate();
            foreach (ListViewItem lvi in lvMainList.Items)
                if (lvi.Tag.ToString() == room.VNum.ToString())
                {
                    lvi.SubItems[1].Text = room.Name;
                }
            lvMainList.EndUpdate();
        }

        public void RefreshRoomFlagsList(Room room)
        {
            bool multiRoomsMode = SelectedRooms.Count > 1;
            tplRoomFlags.MultiRoomsMode = multiRoomsMode;
            string allFlags = "";
            if (!multiRoomsMode)
                allFlags = room.Flags;
            else
            {
                foreach (Room r in SelectedRooms)
                    allFlags += r.Flags;
            }
            tplRoomFlags.SetData(allFlags, BasesDM.RoomVector);
        }

        public void RefreshRoomTriggersList(Room room)
        {
            lvRoomTriggers.Items.Clear();
            CTriggersCollection allTriggers = WindowParentForm.GetAllKnownTriggers(2);
            foreach (int vNum in room.TriggersList)
            {
                Trigger t = allTriggers.GetTrigger(vNum);
                string triggerName = (t != null) ? t.Name : "Триггер из незагруженной зоны";
                //string TriggerName = AllTriggers.GetTrigger(VNum).Name;
                ListViewItem lvi = new ListViewItem(new[] { vNum.ToString(), triggerName }) { Tag = vNum };
                lvRoomTriggers.Items.Add(lvi);
            }
        }

        public void RefreshRoomRemovingObjectsList(Room room)
        {
            lvObjectsToRemove.Items.Clear();
            ObjsCollection allObjects = WindowParentForm.GetAllKnownObjects();
            foreach (LoadedObj obj in room.RemoovingObjects)
            {
                Obj o = allObjects.GetObject(obj.VNum);
                string objectName = (o != null) ? o.Cases.Imen : "Отбъект из незагруженной зоны";
                //string ObjectName = AllObjects.GetObject(Object.VNum).Cases.Imen;
                ListViewItem lvi = new ListViewItem(new[] { obj.VNum.ToString(), objectName }) { Tag = obj.VNum };
                lvObjectsToRemove.Items.Add(lvi);
            }
        }

        public void RefreshRoomObjectsList(Room room)
        {
            gbObjInObj.Enabled = false;
            elvRoomObjInObj.Items.Clear();
            elvObjectsInRoom.Items.Clear();
            ObjsCollection allObjects = WindowParentForm.GetAllKnownObjects();
            foreach (LoadedObj obj in room.LoadedObjectsCollection)
            {
                Obj o = allObjects.GetObject(obj.VNum);
                string objectName = (o != null) ? o.Cases.Imen : "Отбъект из незагруженной зоны";
                //string ObjectName = AllObjects.GetObject(Object.VNum).Cases.Imen;
                EXListViewItem elvi = new EXListViewItem(obj.Probability.ToString()) { Tag = obj.VNum };
                elvi.SubItems.Add(new EXListViewSubItem(obj.VNum.ToString()));
                elvi.SubItems.Add(new EXListViewSubItem(objectName));
                elvObjectsInRoom.Items.Add(elvi);
            }
        }

        public void RefreshObjInObjList(LoadedObj loadedObject)
        {
            elvRoomObjInObj.Items.Clear();
            ObjsCollection allObjects = WindowParentForm.GetAllKnownObjects();
            foreach (LoadedObj obj in loadedObject.ObjectsInObject)
            {
                string objectName = allObjects.GetObject(obj.VNum).Cases.Imen;
                EXListViewItem elvi = new EXListViewItem(obj.Probability.ToString()) { Tag = obj.VNum };
                elvi.SubItems.Add(new EXListViewSubItem(obj.VNum.ToString()));
                elvi.SubItems.Add(new EXListViewSubItem(objectName));
                elvRoomObjInObj.Items.Add(elvi);
            }
        }

        public void RefreshRoomMobsList(Room room)
        {
            elvRoomMobObjects.Items.Clear();
            lvMobsInRoom.Items.Clear();
            cboxMobFollowBy.Items.Clear();
            nudMaxInRoom.Value = 1;
            nudMaxInWorldForRoom.Value = 1;
            MobsCollection allMobs = WindowParentForm.GetAllKnownMobs();
            foreach (LoadedMob mob in room.LoadedMobsCollection)
            {
                Mob m = allMobs[mob.VNum, 0];
                string mobName = (m != null) ? m.Cases.Imen : "Моб из незагруженной зоны";
                EXListViewItem lvi = new EXListViewItem(new[] { mob.VNum.ToString(), mobName })
                                         {
                                             GUID = mob.Guid,
                                             Tag = mob.VNum
                                         };
                lvMobsInRoom.Items.Add(lvi);
            }
        }

        public void RefreshMobsObjList(LoadedMob loadedMob)
        {
            elvRoomMobObjects.Items.Clear();
            nudMaxInRoom.Value = loadedMob.MaxInRoom;
            nudMaxInWorldForRoom.Value = loadedMob.MaxInWorld;
            ObjsCollection allObjects = WindowParentForm.GetAllKnownObjects();
            foreach (MobObj obj in loadedMob.Items)
            {
                Obj o = allObjects.GetObject(obj.VNum);
                string objectName = (o != null) ? o.Cases.Imen : "Отбъект из незагруженной зоны";
                EXListViewItem elvi = new EXListViewItem(obj.Probability.ToString())
                                          {
                                              Tag = obj.VNum,
                                              GUID = obj.Guid
                                          };
                elvi.SubItems.Add(new EXListViewSubItem(obj.VNum.ToString()));
                elvi.SubItems.Add(new EXListViewSubItem(objectName));
                elvi.SubItems.Add(new EXListViewSubItem(BasesDM.GetObjPosNameByNum(obj.ObjPos)));
                elvRoomMobObjects.Items.Add(elvi);
            }
        }

        public void RefreshRoomAddDescList(Room room)
        {
            lvRoomAddDescriptions.Items.Clear();
            foreach (ExtraDesc extraDesc in room.ExtraDescriptions)
            {
                ListViewItem lvi = new ListViewItem(new[] { extraDesc.Aliases, extraDesc.Description }) { Tag = extraDesc.Aliases };
                lvRoomAddDescriptions.Items.Add(lvi);
            }
        }

        public void RefreshExitsDirections(Room room)
        {
            btnConfigExitNorth.Visible = (room.ExitNorth.RoomVNum != -1);
            btnConfigExitSouth.Visible = (room.ExitSouth.RoomVNum != -1);
            btnConfigExitWest.Visible = (room.ExitWest.RoomVNum != -1);
            btnConfigExitEast.Visible = (room.ExitEast.RoomVNum != -1);
            btnConfigExitUp.Visible = (room.ExitUp.RoomVNum != -1);
            btnConfigExitDown.Visible = (room.ExitDown.RoomVNum != -1);
            tbDoorAlias.Text = string.Empty;
            tbDoorNameVin.Text = string.Empty;
            tbDoorDesc.Text = string.Empty;
            nudDoorKeyVNum.Value = -1;
            _blockApplying = true;
            cbExitVisible.Checked = false;
            cbExitHidden.Checked = false;
            cbExitDoor.Checked = false;
            cbDoorClosed.Checked = false;
            cbDoorLocked.Checked = false;
            cbDoorPeekproof.Checked = false;
            _blockApplying = false;
        }

        public void RefreshRoomData()
        {
            IgnoreExitDirChanged = true;
            //для упрощения кода сначала выставляю ридонли, а потом полный рефреш всех данных по первому выбранному номеру комнаты
            //Параметры, загружаемые всегда (по первой выбранной комнате)
            //CRoom Room = ZoneDM.RoomsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            if (ActiveRoom == null) return;

            //Следующий лейбл добавлен по просьбе Свентовита
            lRoomDesc.Text = "[" + ActiveRoom.VNum + "] " + ActiveRoom.Name;
            tbRoomName.Text = ActiveRoom.Name;
            cboxSectorType.SelectedIndex = ActiveRoom.SectorType;

            //Описание комнаты по сезонам
            RefreshRoomDescription();
            RefreshDescriptionTabsIcons();

            tbExitNorth.Text = (ActiveRoom.ExitNorth.RoomVNum != -1)
                                        ? ActiveRoom.ExitNorth.RoomVNum.ToString()
                                        : "";
            tbExitEast.Text = (ActiveRoom.ExitEast.RoomVNum != -1)
                                       ? ActiveRoom.ExitEast.RoomVNum.ToString()
                                       : "";
            tbExitSouth.Text = (ActiveRoom.ExitSouth.RoomVNum != -1)
                                        ? ActiveRoom.ExitSouth.RoomVNum.ToString()
                                        : "";
            tbExitWest.Text = (ActiveRoom.ExitWest.RoomVNum != -1)
                                       ? ActiveRoom.ExitWest.RoomVNum.ToString()
                                       : "";
            tbExitUp.Text = (ActiveRoom.ExitUp.RoomVNum != -1)
                                     ? ActiveRoom.ExitUp.RoomVNum.ToString()
                                     : "";
            tbExitDown.Text = (ActiveRoom.ExitDown.RoomVNum != -1)
                                       ? ActiveRoom.ExitDown.RoomVNum.ToString()
                                       : "";

            RefreshRoomTabsData();

            IgnoreExitDirChanged = false;
        }

        public void RefreshSelectedRoomTabData()
        {
            if (ActiveRoom == null) return;
            IgnoreExitDirChanged = true;
            RefreshRoomTabsData();
            IgnoreExitDirChanged = false;
        }

        public void RemoveRoomFlags(string[] values)
        {
            foreach (Room r in SelectedRooms)
            {
                foreach (string s in values)
                    r.Flags = r.Flags.Replace(s, "");
                wldMap.RecreateRoomBitmap(r);
            }
            wldMap.RedrawBitmap();
        }

        public void AddRoomFlags(string[] values)
        {
            foreach (Room r in SelectedRooms)
            {
                foreach (string s in values)
                {
                    if (!r.Flags.Contains(s))
                        r.Flags += s;
                }
                wldMap.RecreateRoomBitmap(r);
            }
            wldMap.RedrawBitmap();
        }

        private void WldMapRoomCreated()
        {
            RefreshMainList();
        }
    }
}