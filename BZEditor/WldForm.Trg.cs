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
        /// <summary>
        /// Раскраска выбранный условий срабатывания триггера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LvTrgActivationConditionsItemChecked(object sender, ItemCheckedEventArgs e)
        {
            e.Item.BackColor = e.Item.Checked ? Color.LightGreen : SystemColors.Window;
        }

        public bool BlockCodeEditorTextChanging;
        private void CodeEditorTextChanged(object sender, EventArgs e)
        {
            if (!BlockCodeEditorTextChanging)
                WriteTrigger();
        }

        private void TsbTrgClearClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы подтверждаете очистку текста триггера?", "Подтверждение очистки",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                codeEditor.SetText("");
        }

        private void TsbTrgUndoClick(object sender, EventArgs e)
        {
            codeEditor.Undo();
        }

        private void TsbTrgRedoClick(object sender, EventArgs e)
        {
            codeEditor.Redo();
        }

        private void TsbTrgToggleBookmarkClick(object sender, EventArgs e)
        {
            codeEditor.ToggleBookmark();
        }

        private void TsbTrgGoToPrevBookmarkClick(object sender, EventArgs e)
        {
            codeEditor.GotoNextBookmark();
        }

        private void TsbTrgGoToNextBookmarkClick(object sender, EventArgs e)
        {
            codeEditor.GotoPreviousBookmark();
        }

        private void TsbTrgGoToLineClick(object sender, EventArgs e)
        {
            codeEditor.ShowGotoLine();
        }

        private void TsbTrgSearchClick(object sender, EventArgs e)
        {
            codeEditor.ShowFind();
        }

        private void TsbTrgSearchNextClick(object sender, EventArgs e)
        {
            codeEditor.FindNext();
        }

        private void TsbTrgReplaceClick(object sender, EventArgs e)
        {
            codeEditor.ShowReplace();
        }

        private void TsbTrgIndentClick(object sender, EventArgs e)
        {
            codeEditor.Selection.Indent();
        }

        private void TsbTrgOutdentClick(object sender, EventArgs e)
        {
            codeEditor.Selection.Outdent();
        }

        private void TsbTrgCopyClick(object sender, EventArgs e)
        {
            codeEditor.Copy();
        }

        private void TsbTrgCutClick(object sender, EventArgs e)
        {
            codeEditor.Cut();
        }

        private void TsbTrgPasteClick(object sender, EventArgs e)
        {
            codeEditor.Paste();
        }

        private void tsmiCodeEditorCopy_Click(object sender, EventArgs e)
        {
            codeEditor.Copy();
        }

        private void tsmiCodeEditorCut_Click(object sender, EventArgs e)
        {
            codeEditor.Cut();
        }

        private void tsmiCodeEditorPaste_Click(object sender, EventArgs e)
        {
            codeEditor.Paste();
        }
        private void CboxTrgClassSelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshTrgActCondList(cboxTrgClass.SelectedIndex);
            if (lvMainList.SelectedItems.Count <= 0) return;
            Trigger trigger = ZoneDM.TriggersCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            trigger.Class = cboxTrgClass.SelectedIndex;
        }

        private void TbTrgNameValidated(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Trigger trigger = ZoneDM.TriggersCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            trigger.Name = tbTrgName.Text;
            lvMainList.SelectedItems[0].SubItems[1].Text = trigger.Name;
        }

        private void TbTrgArgsValidated(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Trigger trigger = ZoneDM.TriggersCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            trigger.Arg = tbTrgArgs.Text;
        }

        private void NudTrgNumArgValidated(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Trigger trigger = ZoneDM.TriggersCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            trigger.NumArg = Convert.ToInt32(nudTrgNumArg.Value);
        }

        private void LvTrgActivationConditionsLeave(object sender, EventArgs e)
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Trigger trigger = ZoneDM.TriggersCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            trigger.Type = "";
            foreach (ListViewItem lvi in lvTrgActivationConditions.CheckedItems)
            {
                if (lvi != null)
                    trigger.Type += lvi.Tag.ToString();
            }
        }

        private void WriteTrigger()
        {
            if (lvMainList.SelectedItems.Count <= 0) return;
            Trigger trigger = ZoneDM.TriggersCollection[LastTriggerEditedVNum, 0];
            if (trigger == null) return;
            trigger.Body = codeEditor.GetText();
        }

        #region Refresh

        public void RefreshTriggersList()
        {
            if (cboxMainListConditions.SelectedIndex == -1) return;
            lvMainList.BeginUpdate();
            lvMainList.Items.Clear();
            foreach (Trigger trigger in ZoneDM.TriggersCollection)
            {
                if (trigger.Class != cboxMainListConditions.SelectedIndex - 1 &&
                    cboxMainListConditions.SelectedIndex != 0) continue;
                ListViewItem lvi = new ListViewItem(new[] { trigger.VNum.ToString(), trigger.Name }) { Tag = trigger.VNum };
                if (trigger.Modifyed)
                    lvi.ImageIndex = 47;
                if (tboxMainListFilter.Text.Length > 0)
                {
                    if (
                        (trigger.Name.ToUpper() + trigger.VNum.ToString().ToUpper()).IndexOf(
                            tboxMainListFilter.Text.ToUpper()) >= 0)
                        lvMainList.Items.Add(lvi);
                }
                else
                    lvMainList.Items.Add(lvi);
            }
            lvMainList.EndUpdate();
        }

        public void RefreshTrgActCondList(int trgClass)
        {
            switch (trgClass)
            {
                case 0: //Тpиггеp для монстpов
                    BindListView(lvTrgActivationConditions, BasesDM.MobTriggerBitvector);
                    break;
                case 1: //Тpиггеp для обьектов
                    BindListView(lvTrgActivationConditions, BasesDM.ObjTriggerBitvector);
                    break;
                case 2: //Тpиггеp для комнат
                    BindListView(lvTrgActivationConditions, BasesDM.WldTriggerBitvector);
                    break;
            }
        }

        public void RefreshTriggerData()
        {
            Trigger trigger = ZoneDM.TriggersCollection[Convert.ToInt32(lvMainList.SelectedItems[0].Tag), 0];
            cboxTrgClass.SelectedIndex = trigger.Class;
            nudTrgNumArg.Value = trigger.NumArg;
            tbTrgName.Text = trigger.Name;
            tbTrgArgs.Text = trigger.Arg;
            BlockCodeEditorTextChanging = true;
            codeEditor.SetText(trigger.Body);
            BlockCodeEditorTextChanging = false;
            RefreshTrgActCondList(trigger.Class);
            lvTrgActivationConditions.BeginUpdate();
            foreach (ListViewItem lvi in lvTrgActivationConditions.Items)
                lvi.Checked = trigger.Type.IndexOf(lvi.Tag.ToString()) >= 0;
            LastTriggerEditedVNum = trigger.VNum;
            lvTrgActivationConditions.EndUpdate();

            LastSelectedTrg = trigger.VNum;
            RefreshDetailsAndLocations(trigger);
        }

        internal void RefreshDetailsAndLocations(Trigger trigger)
        {
            lvDetails.BeginUpdate();
            ClearDetails();
            wldMap.HighlightedRooms.Clear();
            lvDetails.Groups.Add(new ListViewGroup("Установлен для комнат", HorizontalAlignment.Left));
            foreach (Room r in ZoneDM.RoomsCollection)
            {
                if (!r.TriggersList.Contains(trigger.VNum)) continue;
                lvDetails.Items.Add(new EXListViewItem("[" + r.VNum + "] " + r.Name)
                {
                    Tag = r.VNum,
                    Action = ActionType.GoToRoom,
                    Group = lvDetails.Groups[0]
                });
                wldMap.HighlightedRooms.Add(Convert.ToInt32(r.VNum));
            }
            wldMap.RedrawBitmap();

            lvDetails.Groups.Add(new ListViewGroup("Назначен мобам", HorizontalAlignment.Left));
            foreach (Mob m in ZoneDM.MobsCollection)
            {
                if (!m.TriggersList.Contains(trigger.VNum)) continue;
                lvDetails.Items.Add(new EXListViewItem("[" + m.VNum + "] " + m.Cases.Imen)
                {
                    Tag = m.VNum,
                    Action = ActionType.GoToMob,
                    Group = lvDetails.Groups[1]
                });
            }

            lvDetails.Groups.Add(new ListViewGroup("Назначен объектам", HorizontalAlignment.Left));
            foreach (Obj o in ZoneDM.ObjectsCollection)
            {
                if (!o.TriggersList.Contains(trigger.VNum)) continue;
                lvDetails.Items.Add(new EXListViewItem("[" + o.VNum + "] " + o.Cases.Imen)
                {
                    Tag = o.VNum,
                    Action = ActionType.GoToObject,
                    Group = lvDetails.Groups[1]
                });
            }

            lvDetails.EndUpdate();
        }

        #endregion

        private void TsbInsertSpellNumberClick(object sender, EventArgs e)
        {
            SpellSelectForm ssf = new SpellSelectForm(BasesDM);
            if (ssf.ShowDialog() == DialogResult.OK)
                codeEditor.Selection.Text = ssf.SelectedSpell.ToString();
            ssf.Dispose();
        }
    }
}