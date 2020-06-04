using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DataUtils;
using ExtControls;
using Object = DataUtils.Obj;

namespace BZEditor
{
    public partial class WldForm
    {
        #region OBJ

        private Object CurrentObject
        {
            get
            {
                if (lvMainList.SelectedItems.Count == 0) return null;
                return ZoneDM.ObjectsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            }
        }

        private void TabControlObjectSelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshSelectedObjectTabData();
        }

        private void CbCreateObjAfterReloadMouseClick(object sender, MouseEventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Obj curObject = ZoneDM.ObjectsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            if (cbCreateObjAfterReload.Checked)
                ZoneDM.Zone.LoadedObjects.Add(curObject.VNum, false, /*-1,*/ -1);
            else
                ZoneDM.Zone.LoadedObjects.RemooveObj(curObject.VNum);
        }

        /*private void cbCreateObjAfterReload_CheckedChanged(object sender, EventArgs e)
        {
        }*/

        private void CBoxObjTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Obj curObject = ZoneDM.ObjectsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            curObject.Type = cboxObjType.SelectedIndex + 1;
            foreach (Control c in gbObjType.Controls)
            {
                if (c is Panel)
                    c.Visible = false;
            }
            tboxObjActionDesc.Visible = false;
            label35.Visible = false;
            cboxObjSkill.Enabled = true;

            if (!MustRefreshTypeSpecParams) return;
            MustUpdateTypeSpecParams = false;

            RefreshDefaultSpecParams(curObject);

            MustUpdateTypeSpecParams = true;
        }

        private void ObjValueValidated(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0 || !MustUpdateObjData) return;
            Obj curObject = ZoneDM.ObjectsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            switch (((Control)sender).Name)
            {
                case "cboxObjectGender":
                    curObject.Sex = Convert.ToInt32(((TaggedComboBoxItem)(cboxObjectGender.SelectedItem)).Tag);
                    break;
                case "tboxObjAliases":
                    curObject.Alias = tboxObjAliases.Text;
                    break;
                case "tboxObjImen":
                    curObject.Cases.Imen = tboxObjImen.Text;
                    lvMainList.SelectedItems[0].SubItems[1].Text = curObject.Cases.Imen;
                    break;
                case "tboxObjRod":
                    curObject.Cases.Rod = tboxObjRod.Text;
                    break;
                case "tboxObjDat":
                    curObject.Cases.Dat = tboxObjDat.Text;
                    break;
                case "tboxObjVin":
                    curObject.Cases.Vin = tboxObjVin.Text;
                    break;
                case "tboxObjTvor":
                    curObject.Cases.Tvor = tboxObjTvor.Text;
                    break;
                case "tboxObjPredl":
                    curObject.Cases.Pred = tboxObjPredl.Text;
                    break;
                case "tboxObjDesc":
                    curObject.Desc = tboxObjDesc.Text;
                    break;
                case "tboxObjActionDesc":
                    curObject.ActionDesc = tboxObjActionDesc.Text;
                    break;
                case "nudObjRentPriceEquip":
                    curObject.RentWear = Convert.ToInt32(nudObjRentPriceEquip.Value);
                    break;
                case "nudObjRentPriceInv":
                    curObject.RentInv = Convert.ToInt32(nudObjRentPriceInv.Value);
                    break;
                case "nudObjPrice":
                    curObject.Price = Convert.ToInt32(nudObjPrice.Value);
                    break;
                case "nudObjWeight":
                    curObject.Weight = Convert.ToInt32(nudObjWeight.Value);
                    break;
                case "nudObjMaxInWorld":
                    curObject.MaxInWorld = Convert.ToInt32(nudObjMaxInWorld.Value);
                    break;
                //case "nudObjTimer":
                //    Object.Timer = Convert.ToInt32(nudObjTimer.Value);
                //    break;
                case "cboxObjMaxStructHits":
                    curObject.MaxDurab = Convert.ToInt32(GetCBoxsSelectedValue(cboxObjMaxStructHits));
                    break;
                case "nudObjCurStructHits":
                    curObject.CurrDurab = Convert.ToInt32(nudObjCurStructHits.Value);
                    break;
                case "cboxObjSkill":
                    curObject.TrenSkill = Convert.ToInt32(GetCBoxsSelectedValue(cboxObjSkill));
                    break;
                case "cboxObjMatherial":
                    curObject.Material = Convert.ToInt32(GetCBoxsSelectedValue(cboxObjMatherial));
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private void CboxObjTimerUOMSelectedIndexChanged(object sender, EventArgs e)
        {
            _mustApplyObjTimerChanges = false;
            if (lvMainList.SelectedItems.Count <= 0) return;
            Obj curObject = ZoneDM.ObjectsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            switch (cboxObjTimerUOM.SelectedIndex)
            {
                case 0:
                    nudObjTimer.Value = curObject.Timer;
                    break;
                case 1:
                    // ReSharper disable PossibleLossOfFraction
                    nudObjTimer.Value = Math.Floor((decimal)(curObject.Timer / 60));
                    // ReSharper restore PossibleLossOfFraction
                    break;
                case 2:
                    // ReSharper disable PossibleLossOfFraction
                    nudObjTimer.Value = Math.Floor((decimal)(curObject.Timer / 1440));
                    // ReSharper restore PossibleLossOfFraction
                    break;
            }
            _mustApplyObjTimerChanges = true;
        }

        private void NudObjTimerValueChanged(object sender, EventArgs e)
        {
            if (!_mustApplyObjTimerChanges) return;
            if (lvMainList.SelectedItems.Count <= 0) return;
            Obj curObject = ZoneDM.ObjectsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            switch (cboxObjTimerUOM.SelectedIndex)
            {
                case 0:
                    curObject.Timer = Convert.ToInt32(nudObjTimer.Value);
                    break;
                case 1:
                    curObject.Timer = Convert.ToInt32(nudObjTimer.Value * 60);
                    break;
                case 2:
                    curObject.Timer = Convert.ToInt32(nudObjTimer.Value * 1440);
                    break;
            }
        }

        private void BtnObjSetAutoCasesClick(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Obj curObject = ZoneDM.ObjectsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            AutoCases ac = new AutoCases();
            bool edChislo = true;
            int gender = cboxObjectGender.SelectedIndex - 1;
            switch (gender)
            {
                case -1:
                    gender = 2;
                    break;
                case 2:
                    gender = 0;
                    edChislo = false;
                    break;
            }
            //if (tboxObjAliases.Text == "")
            {
                tboxObjAliases.Text = Utils.RemovePredlog(tboxObjImen.Text);
                curObject.Alias = tboxObjAliases.Text;
            }
            //if (tboxObjRod.Text == "")
            {
                tboxObjRod.Text = ac.Rpad(tboxObjImen.Text, edChislo, gender);
                curObject.Cases.Rod = tboxObjRod.Text;
            }
            //if (tboxObjDat.Text == "")
            {
                tboxObjDat.Text = ac.Dpad(tboxObjImen.Text, edChislo, gender);
                curObject.Cases.Dat = tboxObjDat.Text;
            }
            //if (tboxObjVin.Text == "")
            {
                tboxObjVin.Text = ac.Vpad(tboxObjImen.Text, edChislo, false, gender);
                curObject.Cases.Vin = tboxObjVin.Text;
            }
            //if (tboxObjTvor.Text == "")
            {
                tboxObjTvor.Text = ac.Tpad(tboxObjImen.Text, edChislo, gender);
                curObject.Cases.Tvor = tboxObjTvor.Text;
            }
            //if (tboxObjPredl.Text == "")
            {
                tboxObjPredl.Text = ac.Ppad(tboxObjImen.Text, edChislo, gender);
                curObject.Cases.Pred = tboxObjPredl.Text;
            }
        }

        private void TplObjEffectsValueChanged(object args)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Obj curObject = ZoneDM.ObjectsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            curObject.ExctraEffects = ((string)args);
        }

        private void TplObjAffectsValueChanged(object args)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Obj curObject = ZoneDM.ObjectsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            curObject.Affects = ((string)args);
        }

        private void TplObjWearToValueChanged(object args)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Obj curObject = ZoneDM.ObjectsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            curObject.WearFlags = ((string)args);
        }

        private void TplObjCantTouchValueChanged(object args)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Obj curObject = ZoneDM.ObjectsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            curObject.CantTouch = ((string)args);
        }

        private void TplObjCantUseValueChanged(object args)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Obj curObject = ZoneDM.ObjectsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            curObject.CantUse = ((string)args);
        }

        private void BtnAddObjTriggerClick(object sender, EventArgs e)
        {
            CTriggersCollection allTriggers = WindowParentForm.GetAllKnownTriggers(1);
            TrgSelectForm tsf =
                new TrgSelectForm("Выберите триггеры для объекта", allTriggers, ZoneDM.Zone.Number, true, false);
            DialogResult dres = tsf.ShowDialog();
            if (dres == DialogResult.OK)
            {
                if (lvMainList.SelectedItems.Count > 0)
                {
                    Obj curObject = ZoneDM.ObjectsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                    foreach (Trigger trigger in tsf.SelectedTriggers)
                        curObject.TriggersList.Add(trigger.VNum);
                    RefreshObjTriggersList(curObject);
                }
            }
            tsf.Dispose();
        }

        private void BtnObjRemoveTriggerClick(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Obj curObject = ZoneDM.ObjectsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            foreach (ListViewItem lvi in lvObjTriggers.SelectedItems)
                curObject.TriggersList.Remove(Convert.ToInt32(lvi.Tag));
            RefreshObjTriggersList(curObject);
            if (lvObjTriggers.Items.Count <= 0) return;
            lvObjTriggers.Items[lvObjTriggers.Items.Count - 1].Selected = true;
        }

        private void LvObjTriggersKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
                BtnObjRemoveTriggerClick(null, null);
        }

        private void TsbObjAdditAffectAddClick(object sender, EventArgs e)
        {
            AddBonuses();
            if (lvObjBonuses.Items.Count <= 0) return;
            lvObjBonuses.Items[lvObjBonuses.Items.Count - 1].Selected = true;
        }

        private void lvAvailAddAffects_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            switch (lvAvailAddAffects.Sorting)
            {
                case SortOrder.None:
                    lvAvailAddAffects.Sorting = SortOrder.Descending;
                    lvAvailAddAffects.Sorting = SortOrder.Ascending;
                    lvAvailAddAffects.Sorting = SortOrder.Descending;
                    chObjAddAffectAvail.Text = "Доступные доп.аффекты v";
                    break;
                case SortOrder.Descending:
                    lvAvailAddAffects.Sorting = SortOrder.Ascending;
                    chObjAddAffectAvail.Text = "Доступные доп.аффекты ^";
                    break;
                case SortOrder.Ascending:
                    lvAvailAddAffects.Sorting = SortOrder.None;
                    chObjAddAffectAvail.Text = "Доступные доп.аффекты";
                    if (lvMainList.SelectedItems.Count <= 0) return;
                    Obj curObject = ZoneDM.ObjectsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                    lvAvailAddAffects.BeginUpdate();
                    lvAvailAddAffects.Items.Clear();
                    foreach (DataRow dr in BasesDM.Bonus.Rows)
                    {
                        Bonus b = curObject.BonusesCollection.Get(Convert.ToInt32(dr["val"]));
                        if (b != null) continue;

                        ListViewItem lvi = new ListViewItem {Text = dr["desc"].ToString(), Tag = dr["val"].ToString()};
                        string group = dr["group"].ToString();
                        ListViewGroup lvg = lvAvailAddAffects.Groups[group];
                        if (lvg == null)
                        {
                            lvg = new ListViewGroup(group, group);
                            lvAvailAddAffects.Groups.Add(lvg);
                        }
                        lvi.Group = lvg;
                        lvAvailAddAffects.Items.Add(lvi);
                    }
                    lvAvailAddAffects.EndUpdate();
                    break;
            }
        }

        private void TsbObjAdditAffectRemoveClick(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Obj curObject = ZoneDM.ObjectsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            foreach (ListViewItem lvi in lvObjBonuses.SelectedItems)
                curObject.BonusesCollection.Remove(Convert.ToInt32(lvi.Tag));
            RefreshObjBonusesList(curObject);
            if (lvObjBonuses.Items.Count <= 0) return;
            lvObjBonuses.Items[lvObjBonuses.Items.Count - 1].Selected = true;
        }

        private void tsbEditAddAffectValue_Click(object sender, EventArgs e)
        {
            EditLineOnDoubleClick(sender);
        }

        private void LvObjAdditionalAffectKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
                TsbObjAdditAffectRemoveClick(null, null);
        }

        private void LvObjBonusesAfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label == null) return;
            if (e.Label.Length <= 0)
            {
                e.CancelEdit = true;
                return;
            }
            if (lvMainList.SelectedItems.Count <= 0 || !IsInteger(e.Label)) return;

            Obj curObject = ZoneDM.ObjectsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            curObject.BonusesCollection.Replace(Convert.ToInt32(lvObjBonuses.Items[e.Item].Tag),
                                                Convert.ToInt32(e.Label));
        }

        private void LvAvailAddAffectsDoubleClick(object sender, EventArgs e)
        {
            AddBonuses();
        }

        public void AddBonuses()
        {
            if (lvMainList.SelectedItems.Count == 0 || lvAvailAddAffects.SelectedItems.Count == 0) return;
            Obj curObject = ZoneDM.ObjectsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            if (curObject == null) return;
            foreach (ListViewItem lvi in lvAvailAddAffects.SelectedItems)
            {
                int bonus = Convert.ToInt32(lvi.Tag);
                curObject.AddBonus(bonus, 0);
            }
            RefreshObjBonusesList(curObject);
        }

        private void ToolStripButton1Click(object sender, EventArgs e)
        {
            if (lvObjBonuses.SelectedItems.Count == 0) return;
            lvObjBonuses.SelectedItems[0].BeginEdit();
        }

        private void BtnObjAddAddDescClick(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count == 0) return;
            Obj curObject = ZoneDM.ObjectsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            curObject.ExtraDescriptions.Add(tboxAddDescAliases.Text, rtbObjAddDesc.Text);
            RefreshObjAddDescList(curObject);
            tboxAddDescAliases.Text = "";
            rtbObjAddDesc.Text = "";
        }

        private void BtnObjReplaceAddDescClick(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0 || lvObjAddDesc.SelectedItems.Count <= 0) return;
            Obj curObject = ZoneDM.ObjectsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            ListViewItem lvi = lvObjAddDesc.SelectedItems[0];
            curObject.ExtraDescriptions.Replace(lvi.Tag.ToString(), tboxAddDescAliases.Text, rtbObjAddDesc.Text);
            RefreshObjAddDescList(curObject);
        }

        private void BtnObjRemoveAddDescClick(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Obj curObject = ZoneDM.ObjectsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            foreach (ListViewItem lvi in lvObjAddDesc.SelectedItems)
                curObject.ExtraDescriptions.Remove(lvi.Tag.ToString());
            RefreshObjAddDescList(curObject);
            if (lvObjAddDesc.Items.Count <= 0) return;
            lvObjAddDesc.Items[lvObjAddDesc.Items.Count - 1].Selected = true;
        }

        private void LvObjAddDescKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
                BtnObjRemoveAddDescClick(null, null);
        }

        private void LvObjAddDescSelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvObjAddDesc.SelectedItems.Count != 1) return;
            tboxAddDescAliases.Text = lvObjAddDesc.SelectedItems[0].SubItems[0].Text;
            rtbObjAddDesc.Text = lvObjAddDesc.SelectedItems[0].SubItems[1].Text;
        }

        private void TboxObjImenKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Enter) return;
            if (lvMainList.SelectedItems.Count <= 0 || !MustUpdateTypeSpecParams) return;
            Obj curObject = ZoneDM.ObjectsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            tboxObjRod.Text = tboxObjImen.Text;
            curObject.Cases.Rod = tboxObjImen.Text;
            tboxObjDat.Text = tboxObjImen.Text;
            curObject.Cases.Dat = tboxObjImen.Text;
            tboxObjTvor.Text = tboxObjImen.Text;
            curObject.Cases.Tvor = tboxObjImen.Text;
            tboxObjVin.Text = tboxObjImen.Text;
            curObject.Cases.Vin = tboxObjImen.Text;
            tboxObjPredl.Text = tboxObjImen.Text;
            curObject.Cases.Pred = tboxObjImen.Text;
        }

        private void ObjTypeSpecParamChanged(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0 || !MustUpdateTypeSpecParams) return;
            Obj curObject = ZoneDM.ObjectsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            switch (((Control)sender).Name)
            {
                //Источник света
                case "nudObjLighterValue":
                    curObject.Param1 = nudObjLighterValue.Value.ToString();
                    break;
                //Магический свиток
                case "nudObjMagScrollMinLev":
                    curObject.Param1 = nudObjMagScrollMinLev.Value.ToString();
                    break;
                case "cboxObjMagScrollSpell1":
                    curObject.Param2 = GetCBoxsSelectedValue(cboxObjMagScrollSpell1).ToString();
                    break;
                case "cboxObjMagScrollSpell2":
                    curObject.Param3 = GetCBoxsSelectedValue(cboxObjMagScrollSpell2).ToString();
                    break;
                case "cboxObjMagScrollSpell3":
                    curObject.Param4 = GetCBoxsSelectedValue(cboxObjMagScrollSpell3).ToString();
                    break;
                //Волшебная палочка
                case "nudObjMagWandMinLev":
                    curObject.Param1 = nudObjMagWandMinLev.Value.ToString();
                    break;
                case "nudObjMagWandZarCnt":
                    curObject.Param2 = nudObjMagWandZarCnt.Value.ToString();
                    break;
                case "nudObjMagWandZarCntCurr":
                    curObject.Param3 = nudObjMagWandZarCntCurr.Value.ToString();
                    break;
                case "cboxObjMagWandSpell":
                    curObject.Param4 = GetCBoxsSelectedValue(cboxObjMagWandSpell).ToString();
                    break;
                //Магический посох
                case "nudObjMagStaffMinLev":
                    curObject.Param1 = nudObjMagStaffMinLev.Value.ToString();
                    break;
                case "nudObjMagStaffZarCnt":
                    curObject.Param2 = nudObjMagStaffZarCnt.Value.ToString();
                    break;
                case "nudObjMagStaffZarCntCur":
                    curObject.Param3 = nudObjMagStaffZarCntCur.Value.ToString();
                    break;
                case "cboxObjMagStaffSpell":
                    curObject.Param4 = GetCBoxsSelectedValue(cboxObjMagStaffSpell).ToString();
                    break;
                //Оружие
                case "nudObjWeaponD1":
                    curObject.Param2 = nudObjWeaponD1.Value.ToString();
                    lObjAverageDam.Text = "Ср: " +
                                          (((nudObjWeaponD1.Value * nudObjWeaponD2.Value) - nudObjWeaponD1.Value) / 2 +
                                           nudObjWeaponD1.Value);
                    break;
                case "nudObjWeaponD2":
                    curObject.Param3 = nudObjWeaponD2.Value.ToString();
                    lObjAverageDam.Text = "Ср: " +
                                          (((nudObjWeaponD1.Value * nudObjWeaponD2.Value) - nudObjWeaponD1.Value) / 2 +
                                           nudObjWeaponD1.Value);
                    break;
                case "cboxObjWeaponSrikeType":
                    curObject.Param4 = GetCBoxsSelectedValue(cboxObjWeaponSrikeType).ToString();
                    break;
                //Броня
                case "nudObjArmorAC":
                    curObject.Param1 = nudObjArmorAC.Value.ToString();
                    break;
                case "nudObjArmorArm":
                    curObject.Param2 = nudObjArmorArm.Value.ToString();
                    break;
                //Магический напиток
                case "nudObjPotionMinLev":
                    curObject.Param1 = nudObjPotionMinLev.Value.ToString();
                    break;
                case "cboxObjPotionSpell1":
                    curObject.Param2 = GetCBoxsSelectedValue(cboxObjPotionSpell1).ToString();
                    break;
                case "cboxObjPotionSpell2":
                    curObject.Param3 = GetCBoxsSelectedValue(cboxObjPotionSpell2).ToString();
                    break;
                case "cboxObjPotionSpell3":
                    curObject.Param4 = GetCBoxsSelectedValue(cboxObjPotionSpell3).ToString();
                    break;
                //Контейнер
                case "nudObjContainerValue":
                    curObject.Param1 = nudObjContainerValue.Value.ToString();
                    break;
                case "nudObjContainerKeyVNum":
                    curObject.Param3 = nudObjContainerKeyVNum.Value.ToString();
                    break;
                case "nudObjLockVal":
                    curObject.Param4 = nudObjLockVal.Value.ToString();
                    break;
                case "lvObjContainerFlags":
                    int param = 0;
                    foreach (ListViewItem lvi in lvObjContainerFlags.CheckedItems)
                        param += Convert.ToInt32(lvi.Tag);
                    curObject.Param2 = param.ToString();
                    break;
                //Контейнер для жидкостей
                case "nudObjLiquidContainerMaxVal":
                    curObject.Param1 = nudObjLiquidContainerMaxVal.Value.ToString();
                    break;
                case "nudObjLiquidContainerCurVal":
                    curObject.Param2 = nudObjLiquidContainerCurVal.Value.ToString();
                    break;
                case "cboxObjLiquidContainerDrinkType":
                    curObject.Param3 = GetCBoxsSelectedValue(cboxObjLiquidContainerDrinkType).ToString();
                    btnSelectPotionProtoVNum.Visible =
                        (Convert.ToInt32(GetCBoxsSelectedValue(cboxObjLiquidContainerDrinkType)) >= 16);
                    nudPotionProtoVNum.Visible = btnSelectPotionProtoVNum.Visible;
                    if (!nudPotionProtoVNum.Visible)
                        nudPotionProtoVNum.Value = 0;
                    break;
                case "nudObjLiquidContainerPoison":
                    curObject.Param4 = nudObjLiquidContainerPoison.Value.ToString();
                    break;
                case "nudPotionProtoVNum":
                    curObject.TrenSkill = Convert.ToInt32(nudPotionProtoVNum.Value); //Хранится вместо тренируемого скила
                    break;
                //Корм
                case "nudObjFoodVal":
                    curObject.Param1 = nudObjFoodVal.Value.ToString();
                    break;
                case "nudObjFoodPoison":
                    curObject.Param2 = nudObjFoodPoison.Value.ToString();
                    break;
                //Бабло
                case "nudObjMoneyValue":
                    curObject.Param1 = nudObjMoneyValue.Value.ToString();
                    break;
                //Фонтан
                case "nudObjFontanMaxVal":
                    curObject.Param1 = nudObjFontanMaxVal.Value.ToString();
                    break;
                case "nudObjFontanCurVal":
                    curObject.Param2 = nudObjFontanCurVal.Value.ToString();
                    break;
                case "cboxObjFontanDrinkType":
                    curObject.Param3 = GetCBoxsSelectedValue(cboxObjFontanDrinkType).ToString();
                    btnSelectFontPorionProto.Visible = Convert.ToInt32(GetCBoxsSelectedValue(cboxObjFontanDrinkType)) >=
                                                       16;
                    nudFontPorionProtoVNum.Visible = btnSelectFontPorionProto.Visible;
                    if (!nudFontPorionProtoVNum.Visible)
                        nudFontPorionProtoVNum.Value = 0;
                    break;
                case "nudObjFontanPoison":
                    curObject.Param4 = nudObjFontanPoison.Value.ToString();
                    break;
                case "nudFontPorionProtoVNum":
                    curObject.TrenSkill = Convert.ToInt32(nudFontPorionProtoVNum.Value);
                    //Хранится вместо тренируемого скила
                    break;
                //Магическая книга
                case "cboxObjMagBookSpell":
                    curObject.Param1 = GetCBoxsSelectedValue(cboxObjMagBookSpell).ToString();
                    break;
                //Магический ингредиент
                case "lvObjMagIngrFlags":
                    curObject.MagicFlags = "";
                    foreach (ListViewItem lvi in lvObjMagIngrFlags.CheckedItems)
                        curObject.MagicFlags += lvi.Tag.ToString();
                    break;
                case "nudObjMagIngrLag":
                case "nudObjMagIngrMinLev":
                    curObject.Param1 =
                        CombineLagAndLevel(Convert.ToInt32(nudObjMagIngrLag.Value),
                                           Convert.ToInt32(nudObjMagIngrMinLev.Value)).ToString();
                    break;
                case "nudObjMagIngrPrototype":
                    curObject.Param2 = nudObjMagIngrPrototype.Value.ToString();
                    break;
                case "nudObjMagIngrUseRemain":
                    curObject.Param3 = nudObjMagIngrUseRemain.Value.ToString();
                    break;
                //Бинт
                case "nudObjBandageValue":
                    curObject.Param1 = nudObjBandageValue.Value.ToString();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        #endregion

        private void LvSkillBonusesKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
                TsbRemoveSkillBonusClick(null, null);
        }

        private void TsbAddSkillBonusClick(object sender, EventArgs e)
        {
            AddSkillBonusesToObject();
        }

        private void LvAvailSkillBonusesDoubleClick(object sender, EventArgs e)
        {
            AddSkillBonusesToObject();
        }

        private void TsbRemoveSkillBonusClick(object sender, EventArgs e)
        {
            Obj curObject = CurrentObject;
            if (CurrentObject == null) return;
            foreach (ListViewItem lvi in lvSkillBonuses.SelectedItems)
                curObject.SkillBonusesCollection.Remove(Convert.ToInt32(lvi.Tag));
            RefreshObjSkillBonusesList(curObject);
            if (lvSkillBonuses.Items.Count <= 0) return;
            lvSkillBonuses.Items[lvSkillBonuses.Items.Count - 1].Selected = true;
        }

        private void LvSkillBonusesAfterLabelEdit(object sender, LabelEditEventArgs e)
        {

            if (e.Label == null) return;
            if (e.Label.Length <= 0)
            {
                e.CancelEdit = true;
                return;
            }
            int val = Convert.ToInt32(e.Label);
            if (val > 200 || val < -200)
            {
                MessageBox.Show("Значение бонуса к уровню владения умением должно быть в пределах от -200 до +200!",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.CancelEdit = true;
            }
            if (!IsInteger(e.Label)) return;
            Obj curObject = CurrentObject;
            if (curObject == null) return;
            curObject.SkillBonusesCollection.Replace(Convert.ToInt32(lvSkillBonuses.Items[e.Item].Tag),
                                                 val);
        }

        public void AddSkillBonusesToObject()
        {
            if (lvAvailSkillBonuses.SelectedItems.Count == 0) return;
            Obj curObject = CurrentObject;
            if (curObject == null) return;
            foreach (ListViewItem lvi in lvAvailSkillBonuses.SelectedItems)
                curObject.AddSkillBonus(Convert.ToInt32(lvi.Tag), 0);
            RefreshObjSkillBonusesList(curObject);
        }

        private void tsbEditSkillBonus_Click(object sender, EventArgs e)
        {
            EditLineOnDoubleClick(sender);
        }

        private static void EditLineOnDoubleClick(object sender)
        {
            ListView lv = sender as ListView;
            if (lv == null) return;
            if (lv.SelectedItems.Count > 0)
                lv.SelectedItems[0].BeginEdit();

        }

        private void LvObjBonusesMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvObjBonuses.SelectedItems.Count > 0)
                lvObjBonuses.SelectedItems[0].BeginEdit();
        }

        private void LvSkillBonusesMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvSkillBonuses.SelectedItems.Count > 0)
                lvSkillBonuses.SelectedItems[0].BeginEdit();
        }

        #region Refresh

        internal void RefreshObjectsList()
        {
            lvMainList.BeginUpdate();
            lvMainList.Items.Clear();
            foreach (Obj obj in ZoneDM.ObjectsCollection)
            {
                ListViewItem lvi = new ListViewItem(new[] { obj.VNum.ToString(), obj.Cases.Imen }) { Tag = obj.VNum };
                if (obj.Modifyed)
                    //lvi.BackColor = Color.FromArgb(255, 234, 234);
                    lvi.ImageIndex = 47;
                if (tboxMainListFilter.Text.Length > 0)
                {
                    if (
                        (obj.Cases.Imen.ToUpper() + obj.VNum.ToString().ToUpper()).IndexOf(
                            tboxMainListFilter.Text.ToUpper()) >= 0)
                        lvMainList.Items.Add(lvi);
                }
                else
                    lvMainList.Items.Add(lvi);
            }
            lvMainList.EndUpdate();
        }

        internal void RefreshSpecParams(Object obj)
        {
            switch (cboxObjType.SelectedIndex)
            {
                case 0: //Источник света
                    SetNumericParam(nudObjLighterValue, obj.Param1);
                    pObjLighter.Visible = true;
                    break;
                case 1: //Магический свиток
                    SetNumericParam(nudObjMagScrollMinLev, obj.Param1);
                    SetCBoxsSelectedItem(cboxObjMagScrollSpell1, obj.Param2);
                    SetCBoxsSelectedItem(cboxObjMagScrollSpell2, obj.Param3);
                    SetCBoxsSelectedItem(cboxObjMagScrollSpell3, obj.Param4);
                    pObjMagicScroll.Visible = true;
                    tboxObjActionDesc.Visible = true;
                    label35.Visible = true;
                    break;
                case 2: //Волшебная палочка
                    SetNumericParam(nudObjMagWandMinLev, obj.Param1);
                    SetNumericParam(nudObjMagWandZarCnt, obj.Param2);
                    SetNumericParam(nudObjMagWandZarCntCurr, obj.Param3);
                    SetCBoxsSelectedItem(cboxObjMagWandSpell, obj.Param4);
                    pObjMagWand.Visible = true;
                    tboxObjActionDesc.Visible = true;
                    label35.Visible = true;
                    break;
                case 3: //Магический посох
                    SetNumericParam(nudObjMagStaffMinLev, obj.Param1);
                    SetNumericParam(nudObjMagStaffZarCnt, obj.Param2);
                    SetNumericParam(nudObjMagStaffZarCntCur, obj.Param3);
                    SetCBoxsSelectedItem(cboxObjMagStaffSpell, obj.Param4);
                    pObjMagStaff.Visible = true;
                    tboxObjActionDesc.Visible = true;
                    label35.Visible = true;
                    break;
                case 4: //Оружие
                    SetNumericParam(nudObjWeaponD1, obj.Param2);
                    SetNumericParam(nudObjWeaponD2, obj.Param3);
                    lObjAverageDam.Text = "Ср: " +
                                               (((nudObjWeaponD1.Value * nudObjWeaponD2.Value) -
                                                 nudObjWeaponD1.Value) / 2 + nudObjWeaponD1.Value);
                    SetCBoxsSelectedItem(cboxObjWeaponSrikeType, obj.Param4);
                    pObjWeapon.Visible = true;
                    break;
                case 8: //Броня
                    SetNumericParam(nudObjArmorAC, obj.Param1);
                    SetNumericParam(nudObjArmorArm, obj.Param2);
                    pObjArmor.Visible = true;
                    break;
                case 9: //Магический напиток
                    SetNumericParam(nudObjPotionMinLev, obj.Param1);
                    SetCBoxsSelectedItem(cboxObjPotionSpell1, obj.Param2);
                    SetCBoxsSelectedItem(cboxObjPotionSpell2, obj.Param3);
                    SetCBoxsSelectedItem(cboxObjPotionSpell3, obj.Param4);
                    pObjPotion.Visible = true;
                    tboxObjActionDesc.Visible = true;
                    label35.Visible = true;
                    break;
                case 14: //Контейнер
                    SetNumericParam(nudObjContainerValue, obj.Param1);
                    SetNumericParam(nudObjLockVal, obj.Param4);
                    SetNumericParam(nudObjContainerKeyVNum, obj.Param3);
                    int param = Convert.ToInt32(obj.Param2);
                    if ((param & 1) == 1)
                        SetListViewItemChecked(lvObjContainerFlags, 1, true);
                    else
                        SetListViewItemChecked(lvObjContainerFlags, 1, false);
                    if ((param & 2) == 2)
                        SetListViewItemChecked(lvObjContainerFlags, 2, true);
                    else
                        SetListViewItemChecked(lvObjContainerFlags, 2, false);
                    if ((param & 4) == 4)
                        SetListViewItemChecked(lvObjContainerFlags, 4, true);
                    else
                        SetListViewItemChecked(lvObjContainerFlags, 4, false);
                    if ((param & 8) == 8)
                        SetListViewItemChecked(lvObjContainerFlags, 8, true);
                    else
                        SetListViewItemChecked(lvObjContainerFlags, 8, false);
                    pObjContainer.Visible = true;
                    break;
                case 16: //Контейнер для жидкостей
                    SetNumericParam(nudObjLiquidContainerMaxVal, obj.Param1);
                    SetNumericParam(nudObjLiquidContainerCurVal, obj.Param2);
                    SetCBoxsSelectedItem(cboxObjLiquidContainerDrinkType, obj.Param3);
                    SetNumericParam(nudObjLiquidContainerPoison, obj.Param4);
                    //Хранится вместо тренируемого скила
                    //cboxObjLiquidContainerDrink - vnum зелья, например некоторое зелье из какой-то зоны
                    nudPotionProtoVNum.Value = obj.TrenSkill;
                    cboxObjSkill.Enabled = false;
                    pObjLiquidContainer.Visible = true;
                    break;
                case 18: //Корм
                    SetNumericParam(nudObjFoodVal, obj.Param1);
                    SetNumericParam(nudObjFoodPoison, obj.Param2);
                    pObjFood.Visible = true;
                    break;
                case 19: //Бабло
                    SetNumericParam(nudObjMoneyValue, obj.Param1);
                    pObjMoney.Visible = true;
                    break;
                case 22: //Фонтан
                    SetNumericParam(nudObjFontanMaxVal, obj.Param1);
                    SetNumericParam(nudObjFontanCurVal, obj.Param2);
                    SetCBoxsSelectedItem(cboxObjFontanDrinkType, obj.Param3);
                    SetNumericParam(nudObjFontanPoison, obj.Param4);
                    pObjFontan.Visible = true;
                    break;
                case 23: //Магическая книга
                    SetCBoxsSelectedItem(cboxObjMagBookSpell, obj.Param2);
                    pObjMagBook.Visible = true;
                    break;
                case 24: //Магический ингредиент
                    foreach (ListViewItem lvi in lvObjMagIngrFlags.Items)
                        lvi.Checked = obj.MagicFlags.ToLower().Contains(lvi.Tag.ToString().ToLower());
                    string param1 = obj.Param1;
                    if (param1 == "")
                        param1 = "0";
                    string param2 = obj.Param2;
                    if (param2 == "")
                        param2 = "0";
                    string param3 = obj.Param3;
                    if (param3 == "")
                        param3 = "0";
                    SetNumericParam(nudObjMagIngrLag, GetLag(Convert.ToInt32(param1)));
                    SetNumericParam(nudObjMagIngrMinLev, GetLevel(Convert.ToInt32(param1)));
                    SetNumericParam(nudObjMagIngrPrototype, param2);
                    SetNumericParam(nudObjMagIngrUseRemain, param3);
                    pObjMagIngr.Visible = true;
                    break;
                case 25: //Ингридиент для отвара
                    break;
                case 26: //Бинт
                    SetNumericParam(nudObjBandageValue, obj.Param1);
                    pObjBandage.Visible = true;
                    break;
            }
        }

        internal void RefreshDefaultSpecParams(Object obj)
        {
            switch (cboxObjType.SelectedIndex)
            {
                case 0: //Источник света
                    /*SetNumericParam(nudObjLighterValue, Object.Param1);
                    pObjLighter.Visible = true;*/
                    break;
                case 1: //Магический свиток
                    /*SetNumericParam(nudObjMagScrollMinLev, Object.Param1);
                    SetCBoxsSelectedItem(cboxObjMagScrollSpell1, Object.Param2);
                    SetCBoxsSelectedItem(cboxObjMagScrollSpell2, Object.Param3);
                    SetCBoxsSelectedItem(cboxObjMagScrollSpell3, Object.Param4);
                    pObjMagicScroll.Visible = true;
                    tboxObjActionDesc.Visible = true;
                    label35.Visible = true;*/
                    break;
                case 2: //Волшебная палочка
                    /*SetNumericParam(nudObjMagWandMinLev, Object.Param1);
                    SetNumericParam(nudObjMagWandZarCnt, Object.Param2);
                    SetNumericParam(nudObjMagWandZarCntCurr, Object.Param3);
                    SetCBoxsSelectedItem(cboxObjMagWandSpell, Object.Param4);
                    pObjMagWand.Visible = true;
                    tboxObjActionDesc.Visible = true;
                    label35.Visible = true;*/
                    break;
                case 3: //Магический посох
                    /*SetNumericParam(nudObjMagStaffMinLev, Object.Param1);
                    SetNumericParam(nudObjMagStaffZarCnt, Object.Param2);
                    SetNumericParam(nudObjMagStaffZarCntCur, Object.Param3);
                    SetCBoxsSelectedItem(cboxObjMagStaffSpell, Object.Param4);
                    pObjMagStaff.Visible = true;
                    tboxObjActionDesc.Visible = true;
                    label35.Visible = true;*/
                    break;
                case 4: //Оружие
                    /*SetNumericParam(nudObjWeaponD1, Object.Param2);
                    SetNumericParam(nudObjWeaponD2, Object.Param3);
                    lObjAverageDam.Text = "Ср: " + (((nudObjWeaponD1.Value * nudObjWeaponD2.Value) - nudObjWeaponD1.Value) / 2 + nudObjWeaponD1.Value).ToString();
                    SetCBoxsSelectedItem(cboxObjWeaponSrikeType, Object.Param4);
                    pObjWeapon.Visible = true;*/
                    break;
                case 8: //Броня
                    /*SetNumericParam(nudObjArmorAC, Object.Param1);
                    SetNumericParam(nudObjArmorArm, Object.Param2);
                    pObjArmor.Visible = true;*/
                    break;
                case 9: //Магический напиток
                    /*SetNumericParam(nudObjPotionMinLev, Object.Param1);
                    SetCBoxsSelectedItem(cboxObjPotionSpell1, Object.Param2);
                    SetCBoxsSelectedItem(cboxObjPotionSpell2, Object.Param3);
                    SetCBoxsSelectedItem(cboxObjPotionSpell3, Object.Param4);
                    pObjPotion.Visible = true;
                    tboxObjActionDesc.Visible = true;
                    label35.Visible = true;*/
                    break;
                case 14: //Контейнер
                    obj.Param2 = "0";
                    obj.Param1 = "0";
                    obj.Param3 = "-1";
                    //int Param = Convert.ToInt32(Object.Param2);
                    break;
                case 16: //Контейнер для жидкостей
                    /*SetNumericParam(nudObjLiquidContainerMaxVal, Object.Param1);
                    SetNumericParam(nudObjLiquidContainerCurVal, Object.Param2);
                    SetCBoxsSelectedItem(cboxObjLiquidContainerDrinkType, Object.Param3);
                    SetNumericParam(nudObjLiquidContainerPoison, Object.Param4);
                    //Хранится вместо тренируемого скила
                    //cboxObjLiquidContainerDrink - vnum зелья, например некоторое зелье из какой-то зоны
                    nudPotionProtoVNum.Value = Object.TrenSkill;
                    cboxObjSkill.Enabled = false;
                    pObjLiquidContainer.Visible = true;*/
                    break;
                case 18: //Корм
                    /*SetNumericParam(nudObjFoodVal, Object.Param1);
                    SetNumericParam(nudObjFoodPoison, Object.Param2);
                    pObjFood.Visible = true;*/
                    break;
                case 19: //Бабло
                    /*SetNumericParam(nudObjMoneyValue, Object.Param1);
                    pObjMoney.Visible = true;*/
                    break;
                case 22: //Фонтан
                    /*SetNumericParam(nudObjFontanMaxVal, Object.Param1);
                    SetNumericParam(nudObjFontanCurVal, Object.Param2);
                    SetCBoxsSelectedItem(cboxObjFontanDrinkType, Object.Param3);
                    SetNumericParam(nudObjFontanPoison, Object.Param4);
                    pObjFontan.Visible = true;*/
                    break;
                case 23: //Магическая книга
                    /*SetCBoxsSelectedItem(cboxObjMagBookSpell, Object.Param2);
                    pObjMagBook.Visible = true;*/
                    break;
                case 24: //Магический ингредиент
                    /*foreach (ListViewItem lvi in lvObjMagIngrFlags.Items)
                    {
                        if (Object.MagicFlags.ToLower().Contains(lvi.Tag.ToString().ToLower()))
                            lvi.Checked = true;
                        else
                            lvi.Checked = false;
                    }
                    string Param1 = Object.Param1;
                    if (Param1 == "")
                        Param1 = "0";
                    string Param2 = Object.Param2;
                    if (Param2 == "")
                        Param2 = "0";
                    string Param3 = Object.Param3;
                    if (Param3 == "")
                        Param3 = "0";
                    SetNumericParam(nudObjMagIngrLag, GetLag(Convert.ToInt32(Param1)));
                    SetNumericParam(nudObjMagIngrMinLev, GetLevel(Convert.ToInt32(Param1)));
                    SetNumericParam(nudObjMagIngrPrototype, Param2);
                    SetNumericParam(nudObjMagIngrUseRemain, Param3);
                    pObjMagIngr.Visible = true;*/
                    break;
            }
            RefreshSpecParams(obj);
        }

        internal void RefreshObjEffectsList(Object obj)
        {
            tplObjEffects.SetData(obj.ExctraEffects, BasesDM.ExtraEffect);
        }

        internal void RefreshObjAffectsList(Object obj)
        {
            tplObjAffects.SetData(obj.Affects, BasesDM.Affect);
        }

        internal void RefreshObjWearToList(Object obj)
        {
            tplObjWearTo.SetData(obj.WearFlags, BasesDM.Wear);
        }

        internal void RefreshObjCantTouchList(Object obj)
        {
            tplObjCantTouch.SetData(obj.CantTouch, BasesDM.NoTake);
        }

        internal void RefreshObjCantUseList(Object obj)
        {
            tplObjCantUse.SetData(obj.CantUse, BasesDM.NoUse);
        }

        public void RefreshObjTriggersList(Object obj)
        {
            lvObjTriggers.Items.Clear();
            CTriggersCollection allTriggers = WindowParentForm.GetAllKnownTriggers(1);
            foreach (int vNum in obj.TriggersList)
            {
                Trigger t = allTriggers.GetTrigger(vNum);
                string triggerName = (t != null) ? t.Name : "Триггер из незагруженной зоны";
                //string TriggerName = AllTriggers.GetTrigger(VNum).Name;
                ListViewItem lvi = new ListViewItem(new[] { vNum.ToString(), triggerName }) { Tag = vNum };
                lvObjTriggers.Items.Add(lvi);
            }
        }

        internal void RefreshObjBonusesList(Object trgObject)
        {
            lvAvailAddAffects.BeginUpdate();
            lvObjBonuses.BeginUpdate();
            lvAvailAddAffects.Items.Clear();
            lvAvailAddAffects.Groups.Clear();
            lvObjBonuses.Items.Clear();
            foreach (Bonus bonus in trgObject.BonusesCollection)
            {
                ListViewItem lvi =
                    new ListViewItem(new[] { bonus.Value.ToString(), BasesDM.GetBonusNameByNum(bonus.VNum) }) { Tag = bonus.VNum };
                lvObjBonuses.Items.Add(lvi);
            }
            foreach (DataRow dr in BasesDM.Bonus.Rows)
            {
                Bonus b = trgObject.BonusesCollection.Get(Convert.ToInt32(dr["val"]));
                if (b != null) continue;

                ListViewItem lvi = new ListViewItem { Text = dr["desc"].ToString(), Tag = dr["val"].ToString() };
                string group = dr["group"].ToString();
                ListViewGroup lvg = lvAvailAddAffects.Groups[group];
                if (lvg == null)
                {
                    lvg = new ListViewGroup(group, group);
                    lvAvailAddAffects.Groups.Add(lvg);
                }
                lvi.Group = lvg;
                lvAvailAddAffects.Items.Add(lvi);
            }
            lvAvailAddAffects.EndUpdate();
            lvObjBonuses.EndUpdate();
        }

        private void lvAvailSkillBonuses_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            switch (lvAvailSkillBonuses.Sorting)
            {
                case SortOrder.None:
                    lvAvailSkillBonuses.Sorting = SortOrder.Descending;
                    lvAvailSkillBonuses.Sorting = SortOrder.Ascending;
                    lvAvailSkillBonuses.Sorting = SortOrder.Descending;
                    chObjAddSkillAvail.Text = "Доступные умения v";
                    break;
                case SortOrder.Descending:
                    lvAvailSkillBonuses.Sorting = SortOrder.Ascending;
                    chObjAddSkillAvail.Text = "Доступные умения ^";
                    break;
                case SortOrder.Ascending:
                    lvAvailSkillBonuses.Sorting = SortOrder.None;
                    chObjAddSkillAvail.Text = "Доступные умения";
                    lvAvailSkillBonuses.BeginUpdate();
                    lvAvailSkillBonuses.Items.Clear();
                    lvAvailSkillBonuses.Groups.Clear();
                    Obj curObject = CurrentObject;
                    if (CurrentObject == null) return;
                    foreach (DataRow dr in BasesDM.CharSkills.Rows)
                    {
                        Bonus b = curObject.SkillBonusesCollection.Get(Convert.ToInt32(dr["val"]));
                        if (b != null) continue;

                        ListViewItem lvi = new ListViewItem {Text = dr["desc"].ToString(), Tag = dr["val"].ToString()};
                        string group = dr["group"].ToString();
                        ListViewGroup lvg = lvAvailSkillBonuses.Groups[group];
                        if (lvg == null)
                        {
                            lvg = new ListViewGroup(group, group);
                            lvAvailSkillBonuses.Groups.Add(lvg);
                        }
                        lvi.Group = lvg;
                        lvAvailSkillBonuses.Items.Add(lvi);
                    }
                    lvAvailSkillBonuses.EndUpdate();
                    break;
            }
        }

        internal void RefreshObjSkillBonusesList(Object trgObject)
        {
            lvAvailSkillBonuses.BeginUpdate();
            lvSkillBonuses.BeginUpdate();
            lvAvailSkillBonuses.Items.Clear();
            lvAvailSkillBonuses.Groups.Clear();
            lvSkillBonuses.Items.Clear();
            foreach (Bonus bonus in trgObject.SkillBonusesCollection)
            {
                ListViewItem lvi =
                    new ListViewItem(new[] { bonus.Value.ToString(), BasesDM.GetSkillBonusNameByNum(bonus.VNum) }) { Tag = bonus.VNum };
                lvSkillBonuses.Items.Add(lvi);
            }
            foreach (DataRow dr in BasesDM.CharSkills.Rows)
            {
                Bonus b = trgObject.SkillBonusesCollection.Get(Convert.ToInt32(dr["val"]));
                if (b != null) continue;

                ListViewItem lvi = new ListViewItem { Text = dr["desc"].ToString(), Tag = dr["val"].ToString() };
                string group = dr["group"].ToString();
                ListViewGroup lvg = lvAvailSkillBonuses.Groups[group];
                if (lvg == null)
                {
                    lvg = new ListViewGroup(group, group);
                    lvAvailSkillBonuses.Groups.Add(lvg);
                }
                lvi.Group = lvg;
                lvAvailSkillBonuses.Items.Add(lvi);
            }
            lvAvailSkillBonuses.EndUpdate();
            lvSkillBonuses.EndUpdate();
        }

        internal void RefreshObjAddDescList(Object trgObject)
        {
            lvObjAddDesc.Items.Clear();
            foreach (ExtraDesc extraDesc in trgObject.ExtraDescriptions)
            {
                ListViewItem lvi = new ListViewItem(new[] { extraDesc.Aliases, extraDesc.Description }) { Tag = extraDesc.Aliases };
                lvObjAddDesc.Items.Add(lvi);
            }
        }

        internal void RefreshObjectData()
        {
            MustUpdateObjData = false;
            Obj curObject = ZoneDM.ObjectsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            if (curObject == null) return;
            cbCreateObjAfterReload.Checked = ZoneDM.Zone.LoadedObjects.ObjExists(curObject.VNum);
            SetCBoxsSelectedItem(cboxObjectGender, curObject.Sex);
            MustRefreshTypeSpecParams = false;
            cboxObjType.SelectedIndex = curObject.Type - 1;
            MustRefreshTypeSpecParams = true;
            RefreshSpecParams(curObject);
            if (curObject.Type - 1 != 16)
                SetCBoxsSelectedItem(cboxObjSkill, curObject.TrenSkill);
            SetCBoxsSelectedItem(cboxObjMatherial, curObject.Material);
            SetCBoxsSelectedItem(cboxObjMaxStructHits, curObject.MaxDurab);
            nudObjCurStructHits.Value = curObject.CurrDurab;

            tboxObjAliases.Text = curObject.Alias;
            tboxObjImen.Text = curObject.Cases.Imen;
            tboxObjRod.Text = curObject.Cases.Rod;
            tboxObjDat.Text = curObject.Cases.Dat;
            tboxObjVin.Text = curObject.Cases.Vin;
            tboxObjTvor.Text = curObject.Cases.Tvor;
            tboxObjPredl.Text = curObject.Cases.Pred;
            tboxObjDesc.Text = curObject.Desc;
            tboxObjActionDesc.Text = curObject.ActionDesc;

            RefreshObjectTabsData(curObject);

            RefreshDetailsAndLocations(curObject);

            LastSelectedObj = curObject.VNum;

            MustUpdateObjData = true;
        }

        internal void RefreshSelectedObjectTabData()
        {
            if (lvMainList.SelectedItems.Count == 0) return;
            Obj obj = ZoneDM.ObjectsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            if (obj == null) return;
            MustUpdateObjData = false;
            RefreshObjectTabsData(obj);
            MustUpdateObjData = true;
        }

        private void RefreshObjectTabsData(Object obj)
        {
            switch (tcObject.SelectedTab.Name)
            {
                case "tpObjParams":
                    nudObjRentPriceEquip.Value = obj.RentWear;
                    nudObjRentPriceInv.Value = obj.RentInv;
                    nudObjPrice.Value = obj.Price;
                    nudObjWeight.Value = obj.Weight;
                    nudObjMaxInWorld.Value = obj.MaxInWorld;
                    //Тут обработку для автовыбора единиц измерения
                    if (obj.Timer > 1440)
                    {
                        cboxObjTimerUOM.SelectedIndex = 2;
                        // ReSharper disable PossibleLossOfFraction
                        nudObjTimer.Value = Math.Floor((decimal)(obj.Timer / 1440));
                        // ReSharper restore PossibleLossOfFraction
                    }
                    else if (obj.Timer > 60)
                    {
                        cboxObjTimerUOM.SelectedIndex = 1;
                        // ReSharper disable PossibleLossOfFraction
                        nudObjTimer.Value = Math.Floor((decimal)(obj.Timer / 60));
                        // ReSharper restore PossibleLossOfFraction
                    }
                    else
                    {
                        cboxObjTimerUOM.SelectedIndex = 0;
                        nudObjTimer.Value = obj.Timer;
                    }
                    //nudObjTimer.Value = Object.Timer;
                    break;
                case "tpObjEffects":
                    RefreshObjEffectsList(obj);
                    break;
                case "tpObjAffects":
                    RefreshObjAffectsList(obj);
                    break;
                case "tpObjWearTo":
                    RefreshObjWearToList(obj);
                    break;
                case "tpObjCantTouch":
                    RefreshObjCantTouchList(obj);
                    break;
                case "tpObjCantUse":
                    RefreshObjCantUseList(obj);
                    break;
                case "tpObjTriggers":
                    RefreshObjTriggersList(obj);
                    break;
                case "tpObjAddDescs":
                    RefreshObjAddDescList(obj);
                    break;
                case "tpObjAddAffects":
                    RefreshObjBonusesList(obj);
                    break;
                case "tpObjSkillBonus":
                    RefreshObjSkillBonusesList(obj);
                    break;
            }
        }

        #endregion

        internal void RefreshDetailsAndLocations(Object curObject)
        {
            lvDetails.BeginUpdate();
            ClearDetails();
            wldMap.HighlightedRooms.Clear();
            lvDetails.Groups.Add(new ListViewGroup("Загружается в комнаты", HorizontalAlignment.Left));
            lvDetails.Groups.Add(new ListViewGroup("Помещается в контейнеры в комнатах", HorizontalAlignment.Left));
            foreach (Room r in ZoneDM.RoomsCollection)
            {
                foreach (LoadedObj lo in r.LoadedObjectsCollection)
                {
                    if (lo.VNum == curObject.VNum)
                    {
                        lvDetails.Items.Add(new EXListViewItem("[" + r.VNum + "] " + r.Name + " <" + lo.Probability + "%>")
                        {
                            Tag = r.VNum,
                            Action = ActionType.GoToRoomLoadedObjects,
                            Group = lvDetails.Groups[0]
                        });
                        wldMap.HighlightedRooms.Add(r.VNum);
                    }
                    foreach (LoadedObj lino in lo.ObjectsInObject)
                    {
                        if (lino.VNum != curObject.VNum) continue;
                        lvDetails.Items.Add(new EXListViewItem("[" + r.VNum + "] " + r.Name + ", в контейнер [" + lo.VNum + "] <" +
                                               lino.Probability + "%>")
                        {
                            Tag = r.VNum,
                            Action = ActionType.GoToRoomLoadedObjects,
                            Group = lvDetails.Groups[1]
                        });
                        wldMap.HighlightedRooms.Add(r.VNum);
                    }
                }
            }

            lvDetails.Groups.Add(new ListViewGroup("Помещается в мобов в комнатах", HorizontalAlignment.Left));
            foreach (Room r in ZoneDM.RoomsCollection)
            {
                foreach (LoadedMob lm in r.LoadedMobsCollection)
                {
                    foreach (MobObj mo in lm.Items)
                    {
                        if (mo.VNum != curObject.VNum) continue;
                        lvDetails.Items.Add(
                            new EXListViewItem("[" + r.VNum + "] " + r.Name + ", в моба [" + lm.VNum + "] <" +
                                               mo.Probability + "%>")
                            {
                                TrgVNum = lm.VNum,
                                TrgVNum2 = mo.VNum,
                                Tag = r.VNum,
                                Action = ActionType.GoToRoomLoadedMobs,
                                Group = lvDetails.Groups[2]
                            });
                        wldMap.HighlightedRooms.Add(r.VNum);
                    }
                }
            }
            wldMap.RedrawBitmap();

            lvDetails.Groups.Add(new ListViewGroup("Удаляется из комнат", HorizontalAlignment.Left));
            foreach (Room r in ZoneDM.RoomsCollection)
            {
                foreach (LoadedObj ro in r.RemoovingObjects)
                {
                    if (ro.VNum != curObject.VNum) continue;
                    lvDetails.Items.Add(new EXListViewItem("[" + r.VNum + "] " + r.Name)
                    {
                        Tag = r.VNum,
                        Action = ActionType.GoToRoomUnloadedObjects,
                        Group = lvDetails.Groups[3]
                    });
                }
            }

            lvDetails.Groups.Add(new ListViewGroup("Триггеры", HorizontalAlignment.Left));
            foreach (int vnum in curObject.TriggersList)
            {
                Trigger t = ZoneDM.TriggersCollection[vnum, 0];
                if (t == null) continue;
                lvDetails.Items.Add(new EXListViewItem("[" + t.VNum + "] " + t.Name)
                {
                    Tag = t.VNum,
                    Action = ActionType.GoToTrigger,
                    Group = lvDetails.Groups[4]
                });
            }

            lvDetails.EndUpdate();
        }
    }
}