using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DataUtils;
using ExtControls;
using System.Text.RegularExpressions;

namespace BZEditor
{
    public partial class WldForm
    {
        private void tcMobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshSelectedMobTabData();
        }

        private void cborMobRemoveOnReload_MouseClick(object sender, MouseEventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Mob mob = ZoneDM.MobsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            if (cborMobRemoveOnReload.Checked)
                ZoneDM.Zone.MobsToRemove.Add(mob.VNum, false, -1, -1);
            else
                ZoneDM.Zone.MobsToRemove.RemoveMob(mob.VNum);
        }

        /*private void cborMobRemoveOnReload_CheckedChanged(object sender, EventArgs e)
        {
        }*/

        private void cboxWWrapMobDesc_CheckedChanged(object sender, EventArgs e)
        {
            rtboxMobDetailDescr.WordWrap = cboxWWrapMobDesc.Checked;
        }

        private void btnSetAutoCases_Click(object sender, EventArgs e)
        {
            AutoCases ac = new AutoCases();
            bool edChislo = true;
            int gender = cboxMobSex.SelectedIndex - 1;
            if (gender == -1)
                gender = 2;
            else if (gender == 2)
            {
                gender = 0;
                edChislo = false;
            }
            //if (tboxMobAliases.Text == "")
            tboxMobAliases.Text = Utils.RemovePredlog(tboxMobNameImen.Text);
            //if (tboxMobNameRod.Text == "")
            tboxMobNameRod.Text = ac.Rpad(tboxMobNameImen.Text, edChislo, gender);
            //if (tboxMobNameDat.Text == "")
            tboxMobNameDat.Text = ac.Dpad(tboxMobNameImen.Text, edChislo, gender);
            //if (tboxMobNameVin.Text == "")
            tboxMobNameVin.Text = ac.Vpad(tboxMobNameImen.Text, edChislo, true, gender);
            //if (tboxMobNameTvor.Text == "")
            tboxMobNameTvor.Text = ac.Tpad(tboxMobNameImen.Text, edChislo, gender);
            //if (tboxMobNamePred.Text == "")
            tboxMobNamePred.Text = ac.Ppad(tboxMobNameImen.Text, edChislo, gender);
        }

        private void tboxMobNameImen_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Enter) return;
            if (lvMainList.SelectedItems.Count <= 0 || !MustUpdateMobData) return;
            Mob mob = ZoneDM.MobsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            tboxMobNameRod.Text = tboxMobNameImen.Text;
            mob.Cases.Rod = tboxMobNameImen.Text;
            tboxMobNameDat.Text = tboxMobNameImen.Text;
            mob.Cases.Dat = tboxMobNameImen.Text;
            tboxMobNameTvor.Text = tboxMobNameImen.Text;
            mob.Cases.Tvor = tboxMobNameImen.Text;
            tboxMobNameVin.Text = tboxMobNameImen.Text;
            mob.Cases.Vin = tboxMobNameImen.Text;
            tboxMobNamePred.Text = tboxMobNameImen.Text;
            mob.Cases.Pred = tboxMobNameImen.Text;
        }

        private void MobValueChanged(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0 || !MustUpdateMobData) return;
            Mob mob = ZoneDM.MobsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            switch (((Control) sender).Name)
            {
                case "tboxMobAliases":
                    mob.Alias = tboxMobAliases.Text;
                    break;
                case "cboxMobSex":
                    mob.Sex = Convert.ToInt32(((TaggedComboBoxItem) (cboxMobSex.SelectedItem)).Tag);
                    break;
                case "cboxMobClass":
                    mob.Class = Convert.ToInt32(((TaggedComboBoxItem)(cboxMobClass.SelectedItem)).Tag);
                    break;
                case "cboxMobRace":
                    mob.Race = Convert.ToInt32(((TaggedComboBoxItem)(cboxMobRace.SelectedItem)).Tag);
                    break;
                case "tboxMobNameImen":
                    mob.Cases.Imen = tboxMobNameImen.Text;
                    lvMainList.SelectedItems[0].SubItems[1].Text = mob.Cases.Imen;
                    break;
                case "tboxMobNameRod":
                    mob.Cases.Rod = tboxMobNameRod.Text;
                    break;
                case "tboxMobNameDat":
                    mob.Cases.Dat = tboxMobNameDat.Text;
                    break;
                case "tboxMobNameVin":
                    mob.Cases.Vin = tboxMobNameVin.Text;
                    break;
                case "tboxMobNameTvor":
                    mob.Cases.Tvor = tboxMobNameTvor.Text;
                    break;
                case "tboxMobNamePred":
                    mob.Cases.Pred = tboxMobNamePred.Text;
                    break;
                case "tboxMobDesc":
                    mob.Desc = tboxMobDesc.Text;
                    break;
                case "rtboxMobDetailDescr":
                    mob.DetailDescr = rtboxMobDetailDescr.Text;
                    break;
                case "nudMobStr":
                    mob.Stats.Str = Convert.ToInt32(nudMobStr.Value);
                    break;
                case "nudMobInt":
                    mob.Stats.Int = Convert.ToInt32(nudMobInt.Value);
                    break;
                case "nudMobWis":
                    mob.Stats.Wis = Convert.ToInt32(nudMobWis.Value);
                    break;
                case "nudMobDex":
                    mob.Stats.Dex = Convert.ToInt32(nudMobDex.Value);
                    break;
                case "nudMobCon":
                    mob.Stats.Con = Convert.ToInt32(nudMobCon.Value);
                    break;
                case "nudMobCha":
                    mob.Stats.Cha = Convert.ToInt32(nudMobCha.Value);
                    break;
                case "nudMobLevel":
                    mob.Level = Convert.ToInt32(nudMobLevel.Value);
                    break;
                case "nudMobSize":
                    mob.Stats.Size = Convert.ToInt32(nudMobSize.Value);
                    break;
                case "nudMobHeight":
                    mob.Stats.Height = Convert.ToInt32(nudMobHeight.Value);
                    break;
                case "nudMobWeight":
                    mob.Stats.Weight = Convert.ToInt32(nudMobWeight.Value);
                    break;
                case "nudMobAC":
                    mob.AC = Convert.ToInt32(nudMobAC.Value);
                    break;
                case "nudMobHitroll":
                    mob.Hitroll = Convert.ToInt32(nudMobHitroll.Value);
                    break;
                case "nudMobMaxInWorld":
                    mob.MaxInWorld = Convert.ToInt32(nudMobMaxInWorld.Value);
                    break;
                case "dctrlMobHP":
                    mob.Hits = dctrlMobHP.Value;
                    break;
                case "dctrlMobAttack":
                    mob.Damage = dctrlMobAttack.Value;
                    break;
                case "cboxMobAlign":
                    mob.Align = Convert.ToInt32(((TaggedComboBoxItem) (cboxMobAlign.SelectedItem)).Tag);
                    break;
                case "cboxMobAttackType":
                    mob.BareHandAttack = Convert.ToInt32(((TaggedComboBoxItem) (cboxMobAttackType.SelectedItem)).Tag);
                    break;
                case "nudMobExtraAttack":
                    mob.ExtraAttack = Convert.ToInt32(nudMobExtraAttack.Value);
                    break;
                case "nudMobLikeWork":
                    mob.LikeWork = Convert.ToInt32(nudMobLikeWork.Value);
                    break;
                case "cboxMobStartPosition":
                    mob.PosLoad = Convert.ToInt32(((TaggedComboBoxItem) (cboxMobStartPosition.SelectedItem)).Tag);
                    break;
                case "cboxMobDefPosition":
                    mob.PosDefault = Convert.ToInt32(((TaggedComboBoxItem) (cboxMobDefPosition.SelectedItem)).Tag);
                    break;
                case "nudMobExpa":
                    mob.Exp = Convert.ToInt32(nudMobExpa.Value);
                    break;
                case "nudMobMaxFactor":
                    mob.MaxFactor = Convert.ToInt32(nudMobMaxFactor.Value);
                    break;
                case "dctrlMobMoney":
                    mob.Money = dctrlMobMoney.Value;
                    break;
                case "nudSaveParalyze":
                    mob.SaveParalyzeCast = Convert.ToInt32(nudSaveParalyze.Value);
                    break;
                case "nudSaveMagDam":
                    mob.SaveMagDamages = Convert.ToInt32(nudSaveMagDam.Value);
                    break;
                case "nudSaveMagBreathe":
                    mob.SaveMagBreathes = Convert.ToInt32(nudSaveMagBreathe.Value);
                    break;
                case "nudSaveFightSkills":
                    mob.SaveFightSkills = Convert.ToInt32(nudSaveFightSkills.Value);
                    break;
                case "nudResFire":
                    mob.ResistFromFire = Convert.ToInt32(nudResFire.Value);
                    break;
                case "nudResAir":
                    mob.ResistFromAir = Convert.ToInt32(nudResAir.Value);
                    break;
                case "nudResWater":
                    mob.ResistFromWater = Convert.ToInt32(nudResWater.Value);
                    break;
                case "nudResEarth":
                    mob.ResistFromEarth = Convert.ToInt32(nudResEarth.Value);
                    break;
                case "nudVitality":
                    mob.Vitality = Convert.ToInt32(nudVitality.Value);
                    break;
                case "nudRegeneration":
                    mob.HPreg = Convert.ToInt32(nudRegeneration.Value);
                    break;
                case "nudArmour":
                    mob.Armour = Convert.ToInt32(nudArmour.Value);
                    break;
                case "nudAdsorb":
                    mob.Absorbe = Convert.ToInt32(nudAdsorb.Value);
                    break;
                case "nudMind":
                    mob.Mind = Convert.ToInt32(nudMind.Value);
                    break;
                case "nudMem":
                    mob.PlusMem = Convert.ToInt32(nudMem.Value);
                    break;
                case "nudCastSuccess":
                    mob.CastSuccess = Convert.ToInt32(nudCastSuccess.Value);
                    break;
                case "nudInitiative":
                    mob.Initiative = Convert.ToInt32(nudInitiative.Value);
                    break;
                case "nudSuccess":
                    mob.Luck = Convert.ToInt32(nudSuccess.Value);
                    break;
                case "nudImmun":
                    mob.Immunitet = Convert.ToInt32(nudImmun.Value);
                    break;
                case "nudAResist":
                    mob.AResist = Convert.ToInt32(nudAResist.Value);
                    break;
                case "nudMResist":
                    mob.MResist = Convert.ToInt32(nudMResist.Value);
                    break;
                case "nudPResist":
                    mob.PResist = Convert.ToInt32(nudPResist.Value);
                    break;

                default: //nudMobExpa
                    throw new NotImplementedException();
            }
        }

        private void lvMobSkills_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label == null) return;
            if (e.Label.Length <= 0)
            {
                e.CancelEdit = true;
                return;
            }
            if (lvMainList.SelectedItems.Count <= 0 || !IsInteger(e.Label)) return;
            Mob mob = ZoneDM.MobsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            mob.Skills.Update(Convert.ToInt32(lvMobSkills.Items[e.Item].Tag),
                              Convert.ToInt32(e.Label.Replace("%", "")));
            RefreshMobSkillsList(mob);
        }

        private void tsbMobEditSkill_Click(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0 || lvMobSkills.SelectedItems.Count <= 0) return;
            lvMobSkills.SelectedItems[0].BeginEdit();
        }

        private void tsbMobRemoveSkill_Click(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Mob mob = ZoneDM.MobsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            foreach (ListViewItem lvi in lvMobSkills.SelectedItems)
                mob.Skills.Remove(Convert.ToInt32(lvi.Tag));
            RefreshMobSkillsList(mob);
            if (lvMobSkills.Items.Count <= 0) return;
            lvMobSkills.Items[lvMobSkills.Items.Count - 1].Selected = true;
        }

        private void tsbMobAddSkill_Click(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Mob mob = ZoneDM.MobsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            foreach (ListViewItem lvi in lvAvailMobSkills.SelectedItems)
                mob.Skills.Add(Convert.ToInt32(lvi.Tag.ToString()), 0);
            RefreshMobSkillsList(mob);
            if (lvMobSkills.Items.Count <= 0) return;
            lvMobSkills.Items[lvMobSkills.Items.Count - 1].Selected = true;
        }

        private void lvMobSkills_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
                tsbMobRemoveSkill_Click(null, null);
        }

        private void lvMobSkills_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0 || lvMobSkills.SelectedItems.Count <= 0) return;
            lvMobSkills.SelectedItems[0].BeginEdit();
        }

        private void lvAvailMobSkills_DoubleClick(object sender, EventArgs e)
        {
            tsbMobAddSkill_Click(null, null);
        }

        private void lvAvailMobSkills_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            switch (lvAvailMobSkills.Sorting)
            {
                case SortOrder.None:
                    lvAvailMobSkills.Sorting = SortOrder.Ascending;
                    chMobSkillAvail.Text = "Доступные умения v";
                    break;
                case SortOrder.Ascending:
                    lvAvailMobSkills.Sorting = SortOrder.Descending;
                    chMobSkillAvail.Text = "Доступные умения ^";
                    break;
                case SortOrder.Descending:
                    lvAvailMobSkills.Sorting = SortOrder.None;
                    chMobSkillAvail.Text = "Доступные умения";
                    if (lvMainList.SelectedItems.Count <= 0) break;
                    lvAvailMobSkills.BeginUpdate();
                    lvAvailMobSkills.Items.Clear();
                    Mob mob = ZoneDM.MobsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                    foreach (DataRow dr in BasesDM.MobSkills.Rows)
                    {
                        MobSkill skill = mob.Skills.Get(Convert.ToInt32(dr["val"]));
                        if (skill != null) continue; 
                        ListViewItem lvi = new ListViewItem { Text = dr["desc"].ToString(), Tag = dr["val"].ToString() };
                        lvAvailMobSkills.Items.Add(lvi);
                    }
                    lvAvailMobSkills.EndUpdate();
                    break;
            }
        }

        private void lvMobSpells_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label == null) return;
            if (e.Label.Length <= 0)
            {
                e.CancelEdit = true;
                return;
            }
            if (lvMainList.SelectedItems.Count <= 0 || !IsInteger(e.Label)) return;
            Mob mob = ZoneDM.MobsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            mob.Spells.Replace(Convert.ToInt32(lvMobSpells.Items[e.Item].Tag),
                               Convert.ToInt32(e.Label.Replace("%", "")));
            RefreshMobSpellsList(mob);
        }

        private void lvMobSpells_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
                tsbMobRemoveSpell_Click(null, null);
        }


        private void lvMobSpells_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tsbMobRemoveSpell_Click(null, null);
        }

        private void lvMobAvailSpells_DoubleClick(object sender, EventArgs e)
        {
            tsbMobAddSpell_Click(null, null);
        }

        private void tsbMobAddSpell_Click(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Mob mob = ZoneDM.MobsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            foreach (ListViewItem lvi in lvMobAvailSpells.SelectedItems)
            {
                MobSpell spell = mob.Spells.Get(Convert.ToInt32(lvi.Tag));
                if (spell != null)
                    spell.Count++;
                else
                    mob.Spells.Add(Convert.ToInt32(lvi.Tag), 1);
            }
            RefreshMobSpellsList(mob);
            if (lvMobSpells.Items.Count <= 0) return;
            lvMobSpells.Items[lvMobSpells.Items.Count - 1].Selected = true;
        }

        private void tsbMobRemoveSpell_Click(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Mob mob = ZoneDM.MobsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            foreach (ListViewItem lvi in lvMobSpells.SelectedItems)
            {
                MobSpell spell = mob.Spells.Get(Convert.ToInt32(lvi.Tag));
                if (spell != null)
                {
                    if (spell.Count > 1)
                        spell.Count--;
                    else
                        mob.Spells.Remove(Convert.ToInt32(lvi.Tag));
                }
            }
            RefreshMobSpellsList(mob);
            if (lvMobSpells.Items.Count <= 0) return;
            lvMobSpells.Items[lvMobSpells.Items.Count - 1].Selected = true;
        }

        private void tsbMobEditSpell_Click(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0 || lvMobSpells.SelectedItems.Count <= 0) return;
            lvMobSpells.SelectedItems[0].BeginEdit();
        }

        private void lvMobAvailSpells_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            switch (lvMobAvailSpells.Sorting)
            {
                case SortOrder.None:
                    lvMobAvailSpells.Sorting = SortOrder.Ascending;
                    chAvailMobSpellName.Text = "Заклинание v";
                    break;
                case SortOrder.Ascending:
                    lvMobAvailSpells.Sorting = SortOrder.Descending;
                    chAvailMobSpellName.Text = "Заклинание ^";
                    break;
                case SortOrder.Descending:
                    lvMobAvailSpells.Sorting = SortOrder.None;
                    chAvailMobSpellName.Text = "Заклинание";
                    lvMobAvailSpells.BeginUpdate();
                    lvMobAvailSpells.Items.Clear();
                    foreach (DataRow dr in BasesDM.MobSpells.Rows)
                    {
                        ListViewItem lvi = new ListViewItem {Text = dr["desc"].ToString(), Tag = dr["val"].ToString()};
                        lvMobAvailSpells.Items.Add(lvi);
                    }
                    lvMobAvailSpells.EndUpdate();
                    break;
            }
        }

        private void tplMobFeats_ValueChanged(object args)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Mob mob = ZoneDM.MobsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            mob.Feats = ((BaseDataArrayList) args);
        }

        private void tplMobAffects_ValueChanged(object args)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Mob mob = ZoneDM.MobsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            mob.Affects = ((string) args);
        }

        private void tplMobFlags_ValueChanged(object args)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Mob mob = ZoneDM.MobsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            mob.Flags = ((string) args);
            RecolorizeMobsList();
        }

        private void tplMobSpecFlags_ValueChanged(object args)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Mob mob = ZoneDM.MobsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            mob.SpecialBitvector = ((string) args);
            RecolorizeMobsList();
        }

        private void btnMobAddHelper_Click(object sender, EventArgs e)
        {
            MobsCollection allMobs = WindowParentForm.GetAllKnownMobs();
            MobSelectForm msf = new MobSelectForm("Выберите мобов-помощников", allMobs, ZoneDM.Zone.Number, true, false);
            DialogResult dres = msf.ShowDialog();
            if (dres == DialogResult.OK)
            {
                if (lvMainList.SelectedItems.Count > 0)
                {
                    Mob mob = ZoneDM.MobsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                    foreach (Mob mobHelper in msf.SelectedMobs)
                        mob.Helpers.Add(mobHelper.VNum);
                    RefreshMobHelpersList(mob);
                }
            }
            msf.Dispose();
        }

        private void btnRemoveHelpersList_Click(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Mob mob = ZoneDM.MobsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            foreach (ListViewItem lvi in lvMobHelpers.SelectedItems)
                mob.Helpers.Remove(Convert.ToInt32(lvi.Tag));
            RefreshMobHelpersList(mob);
            if (lvMobHelpers.Items.Count <= 0) return;
            lvMobHelpers.Items[lvMobHelpers.Items.Count - 1].Selected = true;
        }

        private void lvMobHelpers_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
                btnRemoveHelpersList_Click(null, null);
        }

        private void btnAddMobTrigger_Click(object sender, EventArgs e)
        {
            CTriggersCollection allTriggers = WindowParentForm.GetAllKnownTriggers(0);
            TrgSelectForm tsf =
                new TrgSelectForm("Выберите триггеры для моба", allTriggers, ZoneDM.Zone.Number, true, false);
            DialogResult dres = tsf.ShowDialog();
            if (dres == DialogResult.OK)
            {
                if (lvMainList.SelectedItems.Count > 0)
                {
                    Mob mob = ZoneDM.MobsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                    foreach (Trigger trigger in tsf.SelectedTriggers)
                        mob.TriggersList.Add(trigger.VNum);
                    RefreshMobTriggersList(mob);
                }
            }
            tsf.Dispose();
        }

        private void btnMobRemoveTrigger_Click(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Mob mob = ZoneDM.MobsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            foreach (ListViewItem lvi in lvMobTriggers.SelectedItems)
                mob.TriggersList.Remove(Convert.ToInt32(lvi.Tag));
            RefreshMobTriggersList(mob);
            if (lvMobTriggers.Items.Count <= 0) return;
            lvMobTriggers.Items[lvMobTriggers.Items.Count - 1].Selected = true;
        }

        private void lvMobTriggers_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
                btnMobRemoveTrigger_Click(null, null);
        }

        public void btnSelectMobPath_Click(object sender, EventArgs e)
        {
            if (btnSelectMobPath.Text == "Изменить")
            {
                btnSelectMobPath.Text = "Сохранить";
                if (wldMap.ExternalPathSelection == false)
                {
                    wldMap.PathChanged += wldMap_PathChanged;
                    wldMap.ExternalPathSelection = true;
                    wldMap.SelectedPath = tboxMobDestination.Text;
                    tboxMobDestination.ReadOnly = false;
                }
            }
            else
            {
                btnSelectMobPath.Text = "Изменить";
                if (wldMap.ExternalPathSelection)
                {
                    wldMap.PathChanged -= wldMap_PathChanged;
                    wldMap.ExternalPathSelection = false;
                    if (lvMainList.SelectedItems.Count <= 0) return;
                    Mob mob = ZoneDM.MobsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
                    if (mob != null)
                    {
                        //Тут делать валидацию пути
                        mob.Destination.Clear();
                        string[] parts = tboxMobDestination.Text.Split('/');
                        foreach (string s in parts)
                            if (Regex.Match(s, "\\d+").Success)
                                mob.Destination.Add(Convert.ToInt32(s));
                    }
                    tboxMobDestination.ReadOnly = true;
                }
            }
        }

        private void wldMap_PathChanged()
        {
            tboxMobDestination.Text = wldMap.SelectedPath;
        }

        private void tboxMobDestination_TextChanged(object sender, EventArgs e)
        {
            if (wldMap.ExternalPathSelection)
                wldMap.SelectedPath = tboxMobDestination.Text;
        }

        #region Refresh

        public void RefreshMobsList()
        {
            lvMainList.BeginUpdate();
            lvMainList.Items.Clear();
            foreach (Mob mob in ZoneDM.MobsCollection)
            {
                var lvi = new ListViewItem(new[] { mob.VNum.ToString(), mob.Cases.Imen }) { Tag = mob.VNum };

                if (mob.Modifyed)
                    lvi.ImageIndex = 47;

                if (mob.Flags.Contains("f0") ||
                    mob.Flags.Contains("v0") || mob.Flags.Contains("w0") ||
                    mob.Flags.Contains("r2") || mob.Flags.Contains("s2") || mob.Flags.Contains("t2") ||
                    mob.Flags.Contains("i0") || mob.Flags.Contains("j0") || mob.Flags.Contains("k0") ||
                    mob.Flags.Contains("d1") || mob.Flags.Contains("e1") || mob.Flags.Contains("f1") ||
                    mob.Flags.Contains("g1"))
                    lvi.BackColor = Color.FromArgb(255, 115, 115);
                else if (mob.SpecialBitvector.Contains("y0"))
                    lvi.BackColor = Color.FromArgb(115, 255, 115);
                else if (mob.SpecialBitvector.Contains("i3"))
                    lvi.BackColor = Color.FromArgb(115, 115, 255);
                else if (mob.TriggersList.Count > 0)
                    lvi.BackColor = Color.FromArgb(255, 250, 115);

                if (tboxMainListFilter.Text.Length > 0)
                {
                    if (
                        (mob.Cases.Imen.ToUpper() + mob.VNum.ToString().ToUpper()).IndexOf(
                            tboxMainListFilter.Text.ToUpper()) >= 0)
                        lvMainList.Items.Add(lvi);
                }
                else
                    lvMainList.Items.Add(lvi);
            }
            lvMainList.EndUpdate();
        }

        public void RecolorizeMobsList()
        {
            lvMainList.BeginUpdate();
            foreach (ListViewItem lvi in lvMainList.Items)
            {
                int vNum = int.Parse(lvi.Tag.ToString());
                Mob mob = ZoneDM.MobsCollection[vNum, 0];
                if (mob == null) continue;
                if (mob.Flags.Contains("f0") ||
                    mob.Flags.Contains("v0") || mob.Flags.Contains("w0") ||
                    mob.Flags.Contains("r2") || mob.Flags.Contains("s2") || mob.Flags.Contains("t2") ||
                    mob.Flags.Contains("i0") || mob.Flags.Contains("j0") || mob.Flags.Contains("k0") ||
                    mob.Flags.Contains("d1") || mob.Flags.Contains("e1") || mob.Flags.Contains("f1") ||
                    mob.Flags.Contains("g1"))
                    lvi.BackColor = Color.FromArgb(255, 115, 115);
                else if (mob.SpecialBitvector.Contains("y0"))
                    lvi.BackColor = Color.FromArgb(115, 255, 115);
                else if (mob.SpecialBitvector.Contains("i3"))
                    lvi.BackColor = Color.FromArgb(115, 115, 255);
                else if (mob.TriggersList.Count > 0)
                    lvi.BackColor = Color.FromArgb(255, 250, 115);
            }
            lvMainList.EndUpdate();
        }

        public void RefreshMobHelpersList(Mob mob)
        {
            lvMobHelpers.Items.Clear();
            //этот поиск названия моба по номеру можно выкинуть в датаменеджер и делать 1 раз
            MobsCollection mobs = WindowParentForm.GetAllKnownMobs();
            foreach (int vNum in mob.Helpers)
            {
                string name = "";
                if (mobs[vNum, 0] != null) name = mobs[vNum, 0].Cases.Imen;
                var lvi = new ListViewItem(new[] { vNum.ToString(), name }) { Tag = vNum };
                lvMobHelpers.Items.Add(lvi);
            }
        }

        public void RefreshMobSkillsList(Mob mob)
        {
            lvMobSkills.BeginUpdate();
            lvAvailMobSkills.BeginUpdate();
            lvMobSkills.Items.Clear();
            lvAvailMobSkills.Items.Clear();
            foreach (MobSkill skill in mob.Skills)
            {
                var lvi =
                    new ListViewItem(new[] { skill.Percent.ToString(), BasesDM.GetSkillNameByNum(skill.VNum) }) { Tag = skill.VNum };
                lvMobSkills.Items.Add(lvi);
            }
            foreach (DataRow dr in BasesDM.MobSkills.Rows)
            {
                MobSkill skill = mob.Skills.Get(Convert.ToInt32(dr["val"]));
                if (skill != null) continue;
                ListViewItem lvi = new ListViewItem { Text = dr["desc"].ToString(), Tag = dr["val"].ToString() };
                lvAvailMobSkills.Items.Add(lvi);
            }
            lvMobSkills.EndUpdate();
            lvAvailMobSkills.EndUpdate();
        }

        public void RefreshMobSpellsList(Mob mob)
        {
            lvMobSpells.BeginUpdate();
            lvMobSpells.Items.Clear();
            foreach (MobSpell spell in mob.Spells)
            {
                var lvi =
                    new ListViewItem(new[] { spell.Count.ToString(), BasesDM.GetSpellNameByNum(spell.VNum) }) { Tag = spell.VNum };
                lvMobSpells.Items.Add(lvi);
            }
            lvMobSpells.EndUpdate();
            if (lvMobAvailSpells.Items.Count == 0)
            {
                lvMobAvailSpells.BeginUpdate();
                foreach (DataRow dr in BasesDM.MobSpells.Rows)
                {
                    /*MobSpell spell = mob.Spells.Get(Convert.ToInt32(dr["val"]));
                    if (spell != null) continue;*/
                    ListViewItem lvi = new ListViewItem {Text = dr["desc"].ToString(), Tag = dr["val"].ToString()};
                    lvMobAvailSpells.Items.Add(lvi);
                }
                lvMobAvailSpells.EndUpdate();
            }
        }

        public void RefreshMobFeatsList(Mob mob)
        {
            tplMobFeats.SetData(mob.Feats, BasesDM.MobFeatures);
        }

        public void RefreshMobAffectsList(Mob mob)
        {
            tplMobAffects.SetData(mob.Affects, BasesDM.MobAffects);
        }

        public void RefreshMobFlagsList(Mob mob)
        {
            tplMobFlags.SetData(mob.Flags, BasesDM.MobFlags);
        }

        public void RefreshMobSpecFlagsList(Mob mob)
        {
            tplMobSpecFlags.SetData(mob.SpecialBitvector, BasesDM.MobSpecBitvector);
        }

        public void RefreshMobTriggersList(Mob mob)
        {
            lvMobTriggers.Items.Clear();
            CTriggersCollection allTriggers = WindowParentForm.GetAllKnownTriggers(0);
            foreach (int vNum in mob.TriggersList)
            {
                string triggerName = "!!!Триггер с таким номером не найден";
                Trigger trg = allTriggers.GetTrigger(vNum);
                if (trg != null)
                    triggerName = trg.Name;
                var lvi = new ListViewItem(new[] { vNum.ToString(), triggerName }) { Tag = vNum };
                lvMobTriggers.Items.Add(lvi);
            }
        }

        public void RefreshMobData()
        {
            Mob mob = ZoneDM.MobsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            if (mob == null) return;
            cborMobRemoveOnReload.Checked = ZoneDM.Zone.MobsToRemove.MobExists(mob.VNum);
            cboxMobSex.SelectedIndex = mob.Sex;
            SetCBoxsSelectedItem(cboxMobClass, mob.Class);
            SetCBoxsSelectedItem(cboxMobRace, mob.Race);
            tboxMobAliases.Text = mob.Alias;
            tboxMobNameImen.Text = mob.Cases.Imen;
            tboxMobNameRod.Text = mob.Cases.Rod;
            tboxMobNameDat.Text = mob.Cases.Dat;
            tboxMobNameVin.Text = mob.Cases.Vin;
            tboxMobNameTvor.Text = mob.Cases.Tvor;
            tboxMobNamePred.Text = mob.Cases.Pred;
            tboxMobDesc.Text = mob.Desc;
            rtboxMobDetailDescr.Text = mob.DetailDescr;

            RefreshDetailsAndLocations(mob);

            RefreshMobTabsData(mob);

            LastSelectedMob = mob.VNum;
        }

        public void RefreshSelectedMobTabData()
        {
            if (lvMainList.SelectedItems.Count == 0) return;
            Mob mob = ZoneDM.MobsCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            if (mob == null) return;
            RefreshMobTabsData(mob);
        }

        private void RefreshMobTabsData(Mob mob)
        {
            MustUpdateMobData = false;
            switch (tcMobs.SelectedTab.Name)
            {
                case "tpMobParameters":
                    if (btnSelectMobPath.Text == "Сохранить")
                        btnSelectMobPath_Click(null, null);
                    //MustUpdateMobData = false;
                    nudMobStr.Value = mob.Stats.Str;
                    nudMobInt.Value = mob.Stats.Int;
                    nudMobWis.Value = mob.Stats.Wis;
                    nudMobDex.Value = mob.Stats.Dex;
                    nudMobCon.Value = mob.Stats.Con;
                    nudMobCha.Value = mob.Stats.Cha;
                    nudMobLevel.Value = mob.Level;
                    nudMobSize.Value = mob.Stats.Size;
                    nudMobHeight.Value = mob.Stats.Height;
                    nudMobWeight.Value = mob.Stats.Weight;
                    nudMobAC.Value = mob.AC;
                    nudMobHitroll.Value = mob.Hitroll;
                    nudMobMaxInWorld.Value = mob.MaxInWorld;
                    if (mob.Align < 0)
                        cboxMobAlign.SelectedIndex = 0;
                    else cboxMobAlign.SelectedIndex = mob.Align == 0 ? 1 : 2;
                    cboxMobAttackType.SelectedIndex = mob.BareHandAttack;
                    nudMobExtraAttack.Value = mob.ExtraAttack;
                    nudMobLikeWork.Value = mob.LikeWork;
                    cboxMobStartPosition.SelectedIndex = mob.PosLoad;
                    cboxMobDefPosition.SelectedIndex = mob.PosDefault;
                    nudMobExpa.Value = mob.Exp;
                    nudMobMaxFactor.Value = mob.MaxFactor;
                    dctrlMobAttack.Value = mob.Damage;
                    dctrlMobHP.Value = mob.Hits;
                    dctrlMobMoney.Value = mob.Money;
                    tboxMobDestination.Text = "";
                    foreach (int i in mob.Destination)
                    {
                        tboxMobDestination.Text = (tboxMobDestination.Text.Length > 0)
                                                           ? tboxMobDestination.Text + "/" + i
                                                           : tboxMobDestination.Text + i;
                    }
                    //MustUpdateMobData = true;
                    break;
                case "tpMobSkills":
                    RefreshMobSkillsList(mob);
                    break;
                case "tpMobSpells":
                    RefreshMobSpellsList(mob);
                    break;
                case "tpMobFeatures":
                    RefreshMobFeatsList(mob);
                    break;
                case "tpMobAffects":
                    RefreshMobAffectsList(mob);
                    break;
                case "tpMobFlags":
                    RefreshMobFlagsList(mob);
                    break;
                case "tpMobSpecFlags":
                    RefreshMobSpecFlagsList(mob);
                    break;
                case "tpMobHelpers":
                    RefreshMobHelpersList(mob);
                    break;
                case "tpMobTriggers":
                    RefreshMobTriggersList(mob);
                    break;
                case "tpMobResists":
                    nudSaveParalyze.Value = mob.SaveParalyzeCast;
                    nudSaveMagBreathe.Value = mob.SaveMagBreathes;
                    nudSaveMagDam.Value = mob.SaveMagDamages;
                    nudSaveFightSkills.Value = mob.SaveFightSkills;
                    nudResFire.Value = mob.ResistFromFire;
                    nudResAir.Value = mob.ResistFromAir;
                    nudResWater.Value = mob.ResistFromWater;
                    nudResEarth.Value = mob.ResistFromEarth;

                    nudVitality.Value = mob.Vitality;
                    nudRegeneration.Value = mob.HPreg;
                    nudArmour.Value = mob.Armour;
                    nudAdsorb.Value = mob.Absorbe;
                    nudMind.Value = mob.Mind;
                    nudMem.Value = mob.PlusMem;
                    nudCastSuccess.Value = mob.CastSuccess;
                    nudInitiative.Value = mob.Initiative;
                    nudSuccess.Value = mob.Luck;
                    nudImmun.Value = mob.Immunitet;
                    nudAResist.Value = mob.AResist;
                    nudMResist.Value = mob.MResist;
                    nudPResist.Value = mob.PResist;
                    break;
            }
            MustUpdateMobData = true;
        }

        #endregion

        internal void RefreshDetailsAndLocations(Mob mob)
        {
            lvDetails.BeginUpdate();
            ClearDetails();
            wldMap.HighlightedRooms.Clear();
            lvDetails.Groups.Add(new ListViewGroup("Загружается в комнаты", HorizontalAlignment.Left));
            foreach (Room r in ZoneDM.RoomsCollection)
            {
                foreach (LoadedMob lm in r.LoadedMobsCollection)
                {
                    if (lm.VNum != mob.VNum) continue;
                    lvDetails.Items.Add(new EXListViewItem("[" + r.VNum + "] " + r.Name)
                    {
                        Tag = r.VNum,
                        Action = ActionType.GoToRoomLoadedMobs,
                        Group = lvDetails.Groups[0]
                    });
                    wldMap.HighlightedRooms.Add(Convert.ToInt32(r.VNum));
                }
            }
            wldMap.RedrawBitmap();

            lvDetails.Groups.Add(new ListViewGroup("Асистит мобам", HorizontalAlignment.Left));
            foreach (Mob m in ZoneDM.MobsCollection)
            {
                if (!m.Helpers.Contains(mob.VNum)) continue;
                lvDetails.Items.Add(new EXListViewItem("[" + m.VNum + "] " + m.Cases.Imen)
                {
                    Tag = m.VNum,
                    Action = ActionType.GoToMob,
                    Group = lvDetails.Groups[1]
                });
            }

            lvDetails.Groups.Add(new ListViewGroup("Триггеры", HorizontalAlignment.Left));
            foreach (int vnum in mob.TriggersList)
            {
                Trigger t = ZoneDM.TriggersCollection[vnum, 0];
                if (t == null) continue;
                lvDetails.Items.Add(new EXListViewItem("[" + t.VNum + "] " + t.Name)
                {
                    Tag = t.VNum,
                    Action = ActionType.GoToTrigger,
                    Group = lvDetails.Groups[2]
                });
            }

            lvDetails.EndUpdate();
        }

    }
}