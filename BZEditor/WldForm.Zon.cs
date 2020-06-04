using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DataUtils;
using ExtControls;
using Object = DataUtils.Obj;

namespace BZEditor
{
    public partial class WldForm
    {
        private List<ZoneError> _errors;

        private void BtnValidateClick(object sender, EventArgs e)
        {
            ZoneValidator validator = new ZoneValidator();
            _errors =
                validator.Validate(ZoneDM, WindowParentForm.GetAllKnownMobs(), WindowParentForm.GetAllKnownObjects());
            RefreshErrorsList();
        }

        private void ErrVisibilityChanged(object sender, EventArgs e)
        {
            if (_errors == null)
            {
                ZoneValidator validator = new ZoneValidator();
                _errors =
                    validator.Validate(ZoneDM, WindowParentForm.GetAllKnownMobs(), WindowParentForm.GetAllKnownObjects());
            }
            RefreshErrorsList();
        }

        private void RefreshErrorsList()
        {
            btnValidate.Enabled = false;
            mlbValidationResult.Items.Clear();
            int cntr = 0;
            foreach (ZoneError err in _errors)
            {
                if ((err.ErrType == ParseMessageType.Ошибка && cbShowErrors.Checked)
                    || (err.ErrType == ParseMessageType.Предупреждение && cbShowWarnings.Checked)
                    || (err.ErrType == ParseMessageType.Информация && cbShowInfo.Checked))
                {
                    mlbValidationResult.Items.Add(new ParseMessageEventArgs(err.ErrType,
                                                                            "#" + cntr++ + " " + err.ErrCaption,
                                                                            err.ErrMessage, err.VNum, err.Action));
                }
            }
            mlbValidationResult.Invalidate();

            btnValidate.Enabled = true;
        }

        private void MlbValidationResultDoubleClick(object sender, EventArgs e)
        {
            if (mlbValidationResult.SelectedItems.Count > 0)
                Navigate((ParseMessageEventArgs)(mlbValidationResult.SelectedItems[0]));

        }

        private void NudZoneLevelValueChanged(object sender, EventArgs e)
        {
            ZoneDM.Zone.Level = Convert.ToInt32(nudZoneLevel.Value);
        }

        private void BtnChangeZoneNumberClick(object sender, EventArgs e)
        {
            if (!nudZoneNumber.Enabled)
            {
                nudZoneNumber.Enabled = true;
                btnChangeZoneNumber.Text = "Применить";
            }
            else
            {
                if (Convert.ToInt32(nudZoneNumber.Value) == ZoneDM.Zone.Number) return;
                if (WindowParentForm.IsZoneExists(Convert.ToInt32(nudZoneNumber.Value)))
                {
                    errorProvider.SetError(nudZoneNumber, "Зона с таким номером уже сужествует.\nВ случае необходимости, Вы можете удалить файлы имеющейся зоны с номером " + ZoneDM.Zone.Number + ", а зетем обновить список доступных зон.");
                    return;
                }
                nudZoneNumber.Enabled = false;
                errorProvider.SetError(nudZoneNumber, "");
                btnChangeZoneNumber.Text = "Изменить";
                int oldVnum = ZoneDM.Zone.Number;
                int newVnum = Convert.ToInt32(nudZoneNumber.Value);
                ZoneDM.ChangeZoneNumber(newVnum);
                TabText = string.Format("[{0}] {1}", newVnum, ZoneDM.Zone.Name);
                if (ZoneNumberChanged != null)
                    ZoneNumberChanged(oldVnum, newVnum);
                //надо как-то хитро менять все хранимые VNumы 
                //причем менять не только VNumы в этой зоне но и в других зонах, если в них
                //используется что-то из этой зоны
            }
        }

        private void TbZoneNameValidated(object sender, EventArgs e)
        {
            if (tbZoneName.Text == "") return;
            ZoneDM.Zone.Name = tbZoneName.Text;
            TabText = string.Format("[{0}] {1}", ZoneDM.Zone.Number, ZoneDM.Zone.Name);
            if (ZoneRenamed != null)
                ZoneRenamed(ZoneDM.Zone.Number, ZoneDM.Zone.Name);
        }

        private void TbZoneCommentValidated(object sender, EventArgs e)
        {
            ZoneDM.Zone.Comment = tbZoneComment.Text;
        }

        private void CboxZonTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboxZonType.SelectedItem != null)
                ZoneDM.Zone.Type = Convert.ToInt32(((TaggedComboBoxItem) (cboxZonType.SelectedItem)).Tag);
        }

        private void NudOptimalCharsInGroupValueChanged(object sender, EventArgs e)
        {
            ZoneDM.Zone.OptimalCharsInGroup = (int)nudOptimalCharsInGroup.Value;
        }

        private void CbZoneReopopTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbZoneReopopType.SelectedItem != null)
            {
                ZoneDM.Zone.RepopType = Convert.ToInt32(((TaggedComboBoxItem) (cbZoneReopopType.SelectedItem)).Tag);
                gbResetRelatedZones.Visible = ZoneDM.Zone.RepopType == 3;
            }
        }

        private void NudRepopTimerValueChanged(object sender, EventArgs e)
        {
            ZoneDM.Zone.RepopTimer = (int) (nudRepopTimer.Value);
        }

        internal void RefreshABLists(ZoneDataManager zoneDM)
        {
            RefreshAList(zoneDM);
            RefreshBList(zoneDM);
        }

        internal void RefreshAList(ZoneDataManager zoneDM)
        {
            lvAZones.BeginUpdate();
            lvAZones.Items.Clear();
            foreach (int relZoneNum in zoneDM.Zone.ResetA)
            {
                ListViewItem lvi = new ListViewItem(relZoneNum.ToString()) {Tag = relZoneNum};
                lvAZones.Items.Add(lvi);
            }
            lvAZones.EndUpdate();
        }

        internal void RefreshBList(ZoneDataManager zoneDM)
        {
            lvBZones.BeginUpdate();
            lvBZones.Items.Clear();
            foreach (int relZoneNum in zoneDM.Zone.ResetB)
            {
                ListViewItem lvi = new ListViewItem(relZoneNum.ToString()) {Tag = relZoneNum};
                lvBZones.Items.Add(lvi);
            }
            lvBZones.EndUpdate();
        }

        internal void RefreshDetails(ZoneDataManager zoneDM)
        {
            MobsCollection allmobs = WindowParentForm.GetAllKnownMobs();
            ObjsCollection allobjects = WindowParentForm.GetAllKnownObjects();

            lvDetails.BeginUpdate();
            ClearDetails();

            lvDetails.Groups.Add(new ListViewGroup("Мобы, удаляемые при ребуте", HorizontalAlignment.Left));
            foreach (LoadedMob m in zoneDM.Zone.MobsToRemove)
            {
                Mob curMob = allmobs[m.VNum, 0];
                lvDetails.Items.Add(new EXListViewItem("[" + m.VNum + "] " + ((curMob != null) ? curMob.Cases.Imen : ""))
                {
                    Tag = m.VNum,
                    Action = ActionType.GoToMob,
                    Group = lvDetails.Groups[0]
                });
            }

            lvDetails.Groups.Add(new ListViewGroup("Преметы, рассыпающиеся при репопе зоны", HorizontalAlignment.Left));
            foreach (Obj o in zoneDM.ObjectsCollection)
            {
                if (!o.Affects.Contains("D0")) continue;
                Obj curObj = allobjects[o.VNum, 0];
                lvDetails.Items.Add(new EXListViewItem("[" + o.VNum + "] " + ((curObj != null) ? curObj.Cases.Imen : ""))
                {
                    Tag = o.VNum,
                    Action = ActionType.GoToObject,
                    Group = lvDetails.Groups[1]
                });
            }

            lvDetails.Groups.Add(new ListViewGroup("Преметы, создаваемые при репопе зоны", HorizontalAlignment.Left));
            foreach (LoadedObj o in zoneDM.Zone.LoadedObjects)
            {
                Obj curObj = allobjects[o.VNum, 0];
                lvDetails.Items.Add(new EXListViewItem("[" + o.VNum + "] " + ((curObj != null) ? curObj.Cases.Imen : ""))
                {
                    Tag = o.VNum,
                    Action = ActionType.GoToObject,
                    Group = lvDetails.Groups[2]
                });
            }

            lvDetails.EndUpdate();
        }
    }
}