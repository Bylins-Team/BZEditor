using System;
using System.Drawing;
using System.Windows.Forms;
using DataUtils;
using Object = DataUtils.Obj;

namespace BZEditor
{
    public partial class WldForm
    {
        private Shop currentShop
        {
            get { return ZoneDM.ShopsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];}
        }

        private void ToBuyList_CheckedChanged(object sender, EventArgs e)
        {
            cboxTypeToBuyList.Enabled = rbTypeToBuyList.Checked;
            tboxWordToBuyList.ReadOnly = !rbWordToBuyList.Checked;
        }

        private void ToChangeList_CheckedChanged(object sender, EventArgs e)
        {
            cboxTypeToChangeList.Enabled = rbTypeToChangeList.Checked;
            tboxWordToChangeList.ReadOnly = !rbWordToChangeList.Checked;
        }

        private void btnSelectShopSeller_Click(object sender, EventArgs e)
        {
            if (WindowParentForm == null) return;
            MobsCollection allMobs = WindowParentForm.GetAllKnownMobs();
            var msf = new MobSelectForm("Выберите моба-продавца", allMobs, ZoneDM.Zone.Number, false, true);
            DialogResult dres = msf.ShowDialog();
            if (dres == DialogResult.OK)
            {
                nbMobSellerVNum.Text = ((Mob) msf.SelectedMobs[0]).VNum.ToString();
                tboxMobSellerName.Text = ((Mob) msf.SelectedMobs[0]).Cases.Imen;
            }
            msf.Dispose();
        }

        private void nbMobSellerVNum_TextChanged(object sender, EventArgs e)
        {
            if (WindowParentForm == null) return;
            MobsCollection allMobs = WindowParentForm.GetAllKnownMobs();
            Mob mob = allMobs[Convert.ToInt32(nbMobSellerVNum.Value), 0];
            tboxMobSellerName.Text = (mob != null) ? mob.Cases.Imen : "";
            if (lvMainList.SelectedItems.Count <= 0) return;
            currentShop.ShopkeeperVNum = Convert.ToInt32(nbMobSellerVNum.Value);
        }

        private void lvShopBitvector_Validated(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            currentShop.Bitvector = "";
            foreach (ListViewItem lvi in lvShopBitvector.CheckedItems)
            {
                if (lvi != null)
                    currentShop.Bitvector += lvi.Tag.ToString();
            }
        }

        private void lvShopRaceBitvector_Validated(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            currentShop.NotTradeWithBitvector = "";
            foreach (ListViewItem lvi in lvShopRaceBitvector.CheckedItems)
            {
                if (lvi != null)
                    currentShop.NotTradeWithBitvector += lvi.Tag.ToString();
            }
        }

        private void tboxObjectNotInList_Validated(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            currentShop.Msg1 = tboxObjectNotInList.Text;
        }

        private void tboxObjectNotBuy_Validated(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            currentShop.Msg2 = tboxObjectNotBuy.Text;
        }

        private void tboxObjectNotChange_Validated(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            currentShop.Msg3 = tboxObjectNotChange.Text;
        }

        private void tboxShopkeeperNotEnouthMoney_Validated(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Shop shop = ZoneDM.ShopsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            shop.Msg4 = tboxShopkeeperNotEnouthMoney.Text;
        }

        private void tboxCharNotEnouthMoney_Validated(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            currentShop.Msg5 = tboxCharNotEnouthMoney.Text;
        }

        private void tboxHowMachMessage_Validated(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            currentShop.Msg6 = tboxHowMachMessage.Text;
        }

        private void tboxSellingMessage_Validated(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            currentShop.Msg7 = tboxSellingMessage.Text;
        }

        private void nudCoeffSell_ValueChanged(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            currentShop.SellCoeff = nudCoeffSell.Value;
        }

        private void nudCoeffBuy_ValueChanged(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            currentShop.BuyCoeff = nudCoeffBuy.Value;
        }

        private void nudCoeffChange_ValueChanged(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            currentShop.ChangeCoeff = nudCoeffChange.Value;
        }

        private void nudEmotion_ValueChanged(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            currentShop.Emotion = Convert.ToInt32(nudEmotion.Value);
        }

        private void nudOpeningTime1_ValueChanged(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            currentShop.OpeningTime1 = Convert.ToInt32(nudOpeningTime1.Value);
        }

        private void nudClosingTime1_ValueChanged(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            currentShop.ClosingTime1 = Convert.ToInt32(nudClosingTime1.Value);
        }

        private void nudOpeningTime2_ValueChanged(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            currentShop.OpeningTime2 = Convert.ToInt32(nudOpeningTime2.Value);
        }

        private void nudClosingTime2_ValueChanged(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            currentShop.ClosingTime2 = Convert.ToInt32(nudClosingTime2.Value);
        }

        private void btnAddSellingObjects_Click(object sender, EventArgs e)
        {
            ObjsCollection allObjects = WindowParentForm.GetAllKnownObjects();
            var osf =
                new ObjSelectForm("Выберите постоянно продаваемые предметы", allObjects, ZoneDM.Zone.Number, true, false);
            DialogResult dres = osf.ShowDialog();
            if (dres == DialogResult.OK)
            {
                if (lvMainList.SelectedItems.Count > 0)
                {
                    foreach (Obj obj in osf.SelectedObjects)
                    {
                        if (!currentShop.PermanentlySellingList.Contains(obj.VNum))
                            currentShop.PermanentlySellingList.Add(obj.VNum);
                    }
                    RefreshSellingList(currentShop);
                }
            }
            osf.Dispose();
        }

        private void btnRemoveSellingObjects_Click(object sender, EventArgs e)
        {
            if (lvSellingObjects.SelectedItems.Count <= 0 || lvMainList.SelectedItems.Count <= 0) return;
            int index = lvSellingObjects.SelectedIndices[0];
            foreach (ListViewItem lvi in lvSellingObjects.SelectedItems)
            {
                if (currentShop.PermanentlySellingList.Contains(Convert.ToInt32(lvi.Tag)))
                    currentShop.PermanentlySellingList.Remove(Convert.ToInt32(lvi.Tag));
            }
            RefreshSellingList(currentShop);
            if (lvSellingObjects.Items.Count <= 0) return;
            if (index < lvSellingObjects.Items.Count)
                lvSellingObjects.Items[index].Selected = true;
            else
                lvSellingObjects.Items[lvSellingObjects.Items.Count - 1].Selected = true;
        }

        private void lvSellingObjects_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
                btnRemoveSellingObjects_Click(null, null);
            else if (e.KeyData == Keys.Insert)
                btnAddSellingObjects_Click(null, null);
        }

        private void btnAddToBuyList_Click(object sender, EventArgs e)
        {
            if (!tboxWordToBuyList.ReadOnly && tboxWordToBuyList.Text.Length <= 0)
                return; //Чтоб пустоту попусту не добавлять :)
            string aValue = !tboxWordToBuyList.ReadOnly ? tboxWordToBuyList.Text : (cboxTypeToBuyList.SelectedIndex + 1).ToString();
            if (lvMainList.SelectedItems.Count <= 0) return;
            if (!currentShop.BuyingObjectsList.Contains(aValue))
                currentShop.BuyingObjectsList.Add(aValue);
            RefreshBuyingList(currentShop);
            if (lvItemsToBuy.Items.Count <= 0) return;
            lvItemsToBuy.Items[lvItemsToBuy.Items.Count - 1].Selected = true;
        }

        private void btnRemoveFromBuyList_Click(object sender, EventArgs e)
        {
            if (lvItemsToBuy.SelectedItems.Count <= 0 || lvMainList.SelectedItems.Count <= 0) return;
            int index = lvItemsToBuy.SelectedIndices[0];
            foreach (ListViewItem lvi in lvItemsToBuy.SelectedItems)
            {
                if (currentShop.BuyingObjectsList.Contains(lvi.Tag.ToString()))
                    currentShop.BuyingObjectsList.Remove(lvi.Tag.ToString());
            }
            RefreshBuyingList(currentShop);
            if (lvItemsToBuy.Items.Count <= 0) return;
            if (index < lvItemsToBuy.Items.Count)
                lvItemsToBuy.Items[index].Selected = true;
            else
                lvItemsToBuy.Items[lvItemsToBuy.Items.Count - 1].Selected = true;
        }

        private void btnAddToChangeList_Click(object sender, EventArgs e)
        {
            if (!tboxWordToChangeList.ReadOnly && tboxWordToChangeList.Text.Length <= 0)
                return; //Чтоб пустоту попусту не добавлять :)
            if (lvMainList.SelectedItems.Count <= 0) return;
            string aValue = !tboxWordToChangeList.ReadOnly ? tboxWordToChangeList.Text : (cboxTypeToChangeList.SelectedIndex + 1).ToString();
            if (!currentShop.ChangingObjectsList.Contains(aValue))
                currentShop.ChangingObjectsList.Add(aValue);
            RefreshChangingList(currentShop);
            if (lvItemsToChange.Items.Count <= 0) return;
            lvItemsToChange.Items[lvItemsToChange.Items.Count - 1].Selected = true;
        }

        private void btnRemoveFromChangeList_Click(object sender, EventArgs e)
        {
            if (lvItemsToChange.SelectedItems.Count <= 0 || lvMainList.SelectedItems.Count <= 0) return;
            int index = lvItemsToChange.SelectedIndices[0];
            foreach (ListViewItem lvi in lvItemsToChange.SelectedItems)
            {
                if (currentShop.ChangingObjectsList.Contains(lvi.Tag.ToString()))
                    currentShop.ChangingObjectsList.Remove(lvi.Tag.ToString());
            }
            RefreshChangingList(currentShop);
            if (lvItemsToChange.Items.Count <= 0) return;
            if (index < lvItemsToChange.Items.Count)
                lvItemsToChange.Items[index].Selected = true;
            else
                lvItemsToChange.Items[lvItemsToChange.Items.Count - 1].Selected = true;
        }

        private void lvItemsToBuy_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
                btnRemoveFromBuyList_Click(null, null);
            else if (e.KeyData == Keys.Insert)
            {
            }
            //btnAddSellingObjects_Click(null, null);
        }

        private void lvItemsToChange_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
                btnRemoveFromChangeList_Click(null, null);
            else if (e.KeyData == Keys.Insert)
            {
            }
            //btnAddSellingObjects_Click(null, null);
        }

        private void lvShopLocations_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
                btnRemoveLocationFromShopLocationsList_Click(null, null);
            else if (e.KeyData == Keys.Insert)
            {
            }
            //btnAddSellingObjects_Click(null, null);
        }

        private void btnAddLocationToShopLocationsList_Click(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count == 0) return;
            if (
                MessageBox.Show(
                    "При добавлении магазина в локацию, в эту локацию\nдобавляется моб-продавец с загружаемыми товарами.\nДобавть локацию?",
                    "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            if (currentShop.ShopLocationsList.Count != 0) return;
            //Добвление собственно локации
            currentShop.ShopLocationsList.Add(Convert.ToInt32(nudShopLocationVNum.Value));
            //Добавление моба-продавца
            Room room = WindowParentForm.GetAllKnownRooms()[Convert.ToInt32(nudShopLocationVNum.Value), 0];
            if (room != null)
            {
                var seller = new LoadedMob(Convert.ToInt32(nbMobSellerVNum.Value));
                room.LoadedMobsCollection.Add(seller);
                foreach (int vnum in currentShop.PermanentlySellingList)
                    seller.AddGoods(vnum, true, 100000);
            }
            else
            {
                MessageBox.Show(
                    "Локация, в которую добавляется магазин, в загруженных зонах не найдена.\nЛибо удалить локацию из списка и выберите новую, либо добавьте в дальнейшем моб-продавца самостоятельно.",
                    "Не найдена локация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            RefreshShopLocations(currentShop);
            if (lvShopLocations.Items.Count <= 0) return;
            lvShopLocations.Items[lvShopLocations.Items.Count - 1].Selected = true;
            nudShopLocationVNum.Value = 0;
            tboxShopLocationName.Text = "";
            /*if (!Shop.ShopLocationsList.Contains(Convert.ToInt32(nudShopLocationVNum.Value)))
                Shop.ShopLocationsList.Add(Convert.ToInt32(nudShopLocationVNum.Value));
            STLogic.RefreshShopLocations(Shop);
            if (lvShopLocations.Items.Count <= 0) return;
            lvShopLocations.Items[lvShopLocations.Items.Count - 1].Selected = true;*/
        }

        private void btnRemoveLocationFromShopLocationsList_Click(object sender, EventArgs e)
        {
            if (lvShopLocations.SelectedItems.Count <= 0) return;
            if (lvMainList.SelectedItems.Count == 0) return;
            int index = lvShopLocations.SelectedIndices[0];
            foreach (ListViewItem lvi in lvShopLocations.SelectedItems)
            {
                int locvnum = Convert.ToInt32(lvi.Tag);
                if (currentShop.ShopLocationsList.Contains(locvnum))
                    currentShop.ShopLocationsList.Remove(locvnum);
                //Удаляем из комнаты продавца
                MobsCollection allMobs = WindowParentForm.GetAllKnownMobs();
                Room room = WindowParentForm.GetAllKnownRooms()[locvnum, 0];
                if (room == null) continue;
                LoadedMob seller = null;
                foreach (LoadedMob mob in room.LoadedMobsCollection)
                {
                    foreach (MobObj mo in mob.Items)
                    {
                        if (mo.Probability == -1 /*&& mo.MaxInWorld >= 10000*/)
                            seller = mob;
                    }
                }
                if (seller != null)
                {
                    string tmp = "Удалить из локации [" + room.VNum + "]" + room.Name + " моба-продавца [" +
                                 seller.VNum + "]";
                    Mob m = allMobs[seller.VNum, 0];
                    if (m != null)
                        tmp += m.Cases.Imen;
                    if (
                        MessageBox.Show(tmp + "?", "Найден моб-продавец", MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == DialogResult.Yes)
                        room.LoadedMobsCollection.Remove(seller);
                }
            }
            RefreshShopLocations(currentShop);
            if (lvShopLocations.Items.Count <= 0) return;
            if (index < lvShopLocations.Items.Count)
                lvShopLocations.Items[index].Selected = true;
            else
                lvShopLocations.Items[lvShopLocations.Items.Count - 1].Selected = true;
        }

        private void nudShopLocationVNum_ValueChanged(object sender, EventArgs e)
        {
            if (WindowParentForm == null) return;
            RoomsCollection allRooms = WindowParentForm.GetAllKnownRooms();
            Room room = allRooms[Convert.ToInt32(nudShopLocationVNum.Value), 0];
            tboxShopLocationName.Text = (room != null) ? room.Name : "";
        }

        private void btnSelectLocationFromList_Click(object sender, EventArgs e)
        {
            if (WindowParentForm == null) return;
            RoomsCollection allMobs = WindowParentForm.GetAllKnownRooms();
            var rsf =
                new RoomSelectForm("Выберите место размещения магазина", allMobs, ZoneDM.Zone.Number, false, false);
            DialogResult dres = rsf.ShowDialog();
            if (dres == DialogResult.OK)
                nudShopLocationVNum.Value = ((Room) rsf.SelectedRooms[0]).VNum;
            rsf.Dispose();
        }

        private void btnSelectShpLocationFromMap_Click(object sender, EventArgs e)
        {
            if (wldMap.ExternalRoomRoomSelection) return;
            wldMap.RoomSelected += wldMap_ShopLocationSelected;
            wldMap.ExternalRoomRoomSelection = true;
        }

        private void wldMap_ShopLocationSelected(int vNum)
        {
            nudShopLocationVNum.Value = vNum;
            wldMap.RoomSelected -= wldMap_ShopLocationSelected;
        }

        #region Refresh

        public void RefreshShopsList()
        {
            lvMainList.BeginUpdate();
            lvMainList.Items.Clear();
            foreach (Shop shop in ZoneDM.ShopsCollection)
            {
                var lvi =
                    new ListViewItem(new[] {shop.VNum.ToString(), "Магазин №" + shop.VNum}) {Tag = shop.VNum};
                if (shop.Modifyed)
                    lvi.ImageIndex = 47;
                if (tboxMainListFilter.Text.Length > 0)
                {
                    if (shop.VNum.ToString().ToUpper().IndexOf(tboxMainListFilter.Text.ToUpper()) >= 0)
                        lvMainList.Items.Add(lvi);
                }
                else
                    lvMainList.Items.Add(lvi);
            }
            lvMainList.EndUpdate();
        }

        public void RefreshSellingList(Shop shop)
        {
            lvSellingObjects.Items.Clear();
            ObjsCollection objects = WindowParentForm.GetAllKnownObjects();
            foreach (int vNum in shop.PermanentlySellingList)
            {
                Obj o = objects[vNum, 0];
                string objName = (o != null) ? o.Cases.Imen : "Отбъект из незагруженной зоны";
                var lvi = new ListViewItem(new[] {vNum.ToString(), objName}) {Tag = vNum};
                lvSellingObjects.Items.Add(lvi);
            }
        }

        public void RefreshBuyingList(Shop shop)
        {
            cboxTypeToBuyList.SelectedIndex = 0;
            lvItemsToBuy.Items.Clear();
            foreach (string item in shop.BuyingObjectsList)
            {
                string objType = BasesDM.GetObjectType(item);
                ListViewItem lvi = objType != "" ? new ListViewItem(new[] {objType}) : new ListViewItem(new[] {item});
                lvi.Tag = item;
                lvItemsToBuy.Items.Add(lvi);
            }
        }

        public void RefreshChangingList(Shop shop)
        {
            cboxTypeToChangeList.SelectedIndex = 0;
            lvItemsToChange.Items.Clear();
            foreach (string item in shop.ChangingObjectsList)
            {
                string objType = BasesDM.GetObjectType(item);
                ListViewItem lvi = objType != "" ? new ListViewItem(new[] {objType}) : new ListViewItem(new[] {item});
                lvi.Tag = item;
                lvItemsToChange.Items.Add(lvi);
            }
            lvShopLocations.Items.Clear();
        }

        public void RefreshShopLocations(Shop shop)
        {
            nudShopLocationVNum.Value = 0;
            tboxShopLocationName.Text = "";
            lvShopLocations.Items.Clear();
            foreach (int vNum in shop.ShopLocationsList)
            {
                string roomName = "";
                if (ZoneDM.RoomsCollection[vNum, 0] != null) roomName = ZoneDM.RoomsCollection[vNum, 0].Name;
                var lvi = new ListViewItem(new[] {vNum.ToString(), roomName}) {Tag = vNum};
                lvShopLocations.Items.Add(lvi);
            }
        }

        public void RefreshShopData()
        {
            Shop shop = ZoneDM.ShopsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            /*if (Shop.ShopkeeperVNum < nudMobSellerVNum.Minimum || Shop.ShopkeeperVNum > nudMobSellerVNum.Maximum)
            {
                tboxMobSellerName.Text = "";
            }
            else*/
            nbMobSellerVNum.Text = shop.ShopkeeperVNum.ToString();
            tboxObjectNotInList.Text = shop.Msg1;
            tboxObjectNotBuy.Text = shop.Msg2;
            tboxObjectNotChange.Text = shop.Msg3;
            tboxShopkeeperNotEnouthMoney.Text = shop.Msg4;
            tboxCharNotEnouthMoney.Text = shop.Msg5;
            tboxHowMachMessage.Text = shop.Msg6;
            tboxSellingMessage.Text = shop.Msg7;
            nudCoeffSell.Value = shop.SellCoeff;
            nudCoeffBuy.Value = shop.BuyCoeff;
            nudCoeffChange.Value = shop.ChangeCoeff;
            nudEmotion.Value = shop.Emotion;
            nudOpeningTime1.Value = shop.OpeningTime1;
            nudClosingTime1.Value = shop.ClosingTime1;
            nudOpeningTime2.Value = shop.OpeningTime2;
            nudClosingTime2.Value = shop.ClosingTime2;

            lvShopBitvector.BeginUpdate();
            foreach (ListViewItem lvi in lvShopBitvector.Items)
                lvi.Checked = shop.Bitvector.IndexOf(lvi.Tag.ToString()) >= 0;
            lvShopBitvector.EndUpdate();

            lvShopRaceBitvector.BeginUpdate();
            foreach (ListViewItem lvi in lvShopRaceBitvector.Items)
                lvi.Checked = shop.NotTradeWithBitvector.IndexOf(lvi.Tag.ToString()) >= 0;

            lvShopRaceBitvector.EndUpdate();

            RefreshSellingList(shop);

            RefreshBuyingList(shop);

            RefreshChangingList(shop);

            RefreshShopLocations(shop);

            LastSelectedShp = shop.VNum;
        }

        #endregion
    }
}