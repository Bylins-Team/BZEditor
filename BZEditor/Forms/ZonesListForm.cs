using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DataUtils;
using ExtControls;
using WeifenLuo.WinFormsUI.Docking;

namespace BZEditor
{
    public partial class ZonesListForm : DockContent
    {
        #region Delegates & Events

        public delegate void ItemDoubleClickEvent(object sender, EventArgs e);

        public delegate void ZoneLoadingActivateEvent(string zoneNum, Encoding enc, bool resave);

        public delegate void ZonePrepareToSendActivateEvent(string zoneNum, string zoneName);

        public delegate void ZoneSavingActivateEvent(string zoneNum);

        public delegate void ZoneUnloadingActivateEvent(string zoneNum);

        public event ItemDoubleClickEvent ItemDoubleClicked;

        public event ZoneLoadingActivateEvent ZoneLoadingActivated;

        public event ZoneUnloadingActivateEvent ZoneUnloadingActivated;

        public event ZoneSavingActivateEvent ZoneSavingActivated;

        public event ZonePrepareToSendActivateEvent ZonePrepareToSendActivated;

        public delegate void SketchCreatingEvent(GlobalSketch sketch);
        public event SketchCreatingEvent SketchCreated;

        public delegate void SketchSavingActivateEvent(string sketchName);
        public event SketchSavingActivateEvent SketchSavingActivated;

        public delegate void SketchRemovingEvent(string sketchName);
        public event SketchRemovingEvent SketchRemoved;

        #endregion

        private readonly FileListsDataManager fileListsDm;

        public ZonesListForm(ref FileListsDataManager fileListsDm)
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;
            this.fileListsDm = fileListsDm;
            RefreshZonesList();
            RefreshSketchesList();
        }

        public void RefreshZonesList()
        {
            lvZones.BeginUpdate();
            lvZonesAvail.BeginUpdate();
            lvZones.Items.Clear();
            lvZonesAvail.Items.Clear();
            List<ExtListViewItem> loaded = new List<ExtListViewItem>(fileListsDm.ZonesDataList.Count);
            List<ExtListViewItem> available = new List<ExtListViewItem>(fileListsDm.ZonesDataList.Count);
            foreach (ZoneData zd in fileListsDm.ZonesDataList)
            {
                ExtListViewItem lvi =
                    new ExtListViewItem("[" + Space(4 - zd.FileName.Length) + zd.FileName + "] " + zd.Name, zd.FileName, "Wld",
                                        0, 0, Guid.NewGuid()) { Tag = zd.Name };
                if (zd.Preloading || zd.State == ZoneState.NotFound)
                    loaded.Add(lvi);
                else
                    available.Add(lvi);
                lvi.ImageIndex = (int)zd.State;
                lvi.StateImageIndex = (int)zd.State;
            }
            lvZones.Items.AddRange(loaded.ToArray());
            lvZonesAvail.Items.AddRange(available.ToArray());
            lvZones.EndUpdate();
            lvZonesAvail.EndUpdate();
        }

        private void RefreshSketchesList()
        {
            lvSketches.BeginUpdate();

            List<ExtListViewItem> toDelete = new List<ExtListViewItem>(lvSketches.Items.Count);
            foreach (ExtListViewItem elvi in lvSketches.Items)
                if (fileListsDm.SketchesDataList[elvi.FileName] == null)
                    toDelete.Add(elvi);
            foreach (ExtListViewItem del in toDelete)
                lvSketches.Items.Remove(del);

            List<ExtListViewItem> sketches = new List<ExtListViewItem>(fileListsDm.SketchesDataList.Count);
            foreach (ZoneData zd in fileListsDm.SketchesDataList)
            {
                if (!ItemExists(lvSketches, zd.FileName))
                {
                    ExtListViewItem lvi =
                        new ExtListViewItem(zd.Name, zd.FileName, "Skt", 6, 6, Guid.NewGuid()) {Tag = zd.FileName};
                    sketches.Add(lvi);
                }
            }
        
            lvSketches.Items.AddRange(sketches.ToArray());
            lvSketches.EndUpdate();
        }

        private static string Space(int cnt)
        {
            if (cnt <= 0) return "";
            string res = "";
            for (int i = 0; i < cnt; i++)
                res += " ";
            return res;
        }

        public Guid AddItem(string inNum, string inName)
        {
            Guid g = Guid.NewGuid();
            ExtListViewItem tn =
                new ExtListViewItem("[" + Space(4 - inNum.Length) + inNum + "] " + inName, inNum, "Wld", 0,
                                    0, g);
            lvZones.Items.Add(tn);
            return g;
        }

        private static void FindItemInList(ListView lv, string filter)
        {
            foreach (ListViewItem lvi in lv.Items)
            {
                if (lvi.Text.ToUpper().IndexOf(filter.ToUpper()) >= 0)
                {
                    lvi.Selected = true;
                    lvi.EnsureVisible();
                    return;
                }
            }
            if (lv.Items.Count > 0)
            {
                lv.Items[0].Selected = true;
                lv.Items[0].EnsureVisible();
            }
        }

        private static ExtListViewItem FindItemByGuid(ListView lv, Guid guid)
        {
            foreach (ExtListViewItem elvi in lv.Items)
            {
                if (elvi.ItemGuid == guid)
                    return elvi;
            }
            return null;
        }

        private static bool ItemExists(ListView lv, string fileName)
        {
            foreach (ExtListViewItem elvi in lv.Items)
            {
                if (elvi.FileName == fileName)
                    return true;
            }
            return false;
        }

        public int GetZoneState(Guid guid)
        {
            ExtListViewItem elvi = FindItemByGuid(lvZones, guid);
            return elvi != null ? elvi.StateImageIndex : 0;
        }

        public void SetZoneState(Guid guid, ZoneState state)
        {
            ExtListViewItem elvi = FindItemByGuid(lvZones, guid);
            if (elvi == null) return;
            elvi.StateImageIndex = (int)state;
            foreach (ZoneData zd in fileListsDm.ZonesDataList)
            {
                if (zd.FileName == elvi.FileName)
                    zd.State = state;
            }
        }

        public int GetSketchState(Guid guid)
        {
            ExtListViewItem elvi = FindItemByGuid(lvSketches, guid);
            return elvi != null ? elvi.StateImageIndex : 0;
        }

        public void SetSketchState(Guid guid, int state)
        {
            ExtListViewItem elvi = FindItemByGuid(lvSketches, guid);
            if (elvi == null) return;
            elvi.StateImageIndex = state;
        }

        public Guid GetZoneGuidByZoneNum(string zoneNumber)
        {
            ExtListViewItem elvi = FindZoneByNumber(zoneNumber);
            return (elvi != null) ? elvi.ItemGuid : Guid.Empty;
        }

        private ExtListViewItem FindZoneByNumber(string zoneNumber)
        {
            foreach (ExtListViewItem elvi in lvZones.Items)
            {
                if (elvi.FileName == zoneNumber)
                    return elvi;
            }
            return null;
        }

        public void SetLoadedZoneState(string zoneNumber, ZoneState state)
        {
            foreach (ZoneData zd in fileListsDm.ZonesDataList)
            {
                if (zd.FileName == zoneNumber)
                    zd.State = state;
            }
            ExtListViewItem elvi = FindZoneByNumber(zoneNumber);
            if (elvi == null) return;
            elvi.StateImageIndex = (int)state;
        }

        private void TbQuickSearchTextChanged(object sender, EventArgs e)
        {
            if (tbQuickSearch.Text.Length > 0)
                FindItemInList(lvZones, tbQuickSearch.Text);
            else if (lvZones.Items.Count > 0)
                lvZones.Items[0].Selected = true;
        }

        private void TbQuickSearchKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && ItemDoubleClicked != null)
                ItemDoubleClicked(lvZones, null);
        }

        private void LvZonesDoubleClick(object sender, EventArgs e)
        {
            ItemDoubleClicked?.Invoke(sender, e);
        }

        private void TsmiEditLoadedClick(object sender, EventArgs e)
        {
            ItemDoubleClicked(lvZones, null);
        }

        private void TsmiLoadZoneClick(object sender, EventArgs e)
        {
            if (lvZonesAvail.SelectedItems.Count == 0) return;
            ExtListViewItem elvi = ((ExtListViewItem)(lvZonesAvail.SelectedItems[0]));
            if (ZoneLoadingActivated == null) return;
            fileListsDm.AddZoneToLoadedList(elvi.FileName);
            RefreshZonesList();
            ZoneLoadingActivated(elvi.FileName, null, false);
        }

        private void TsmiRefreshListClick(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            fileListsDm.LoadAvailZones();
            Cursor = Cursors.AppStarting;
            RefreshZonesList();
            Cursor = Cursors.Default;
        }

        private void TsmiUnloadZoneClick(object sender, EventArgs e)
        {
            if (lvZones.SelectedItems.Count == 0) return;
            ExtListViewItem elvi = ((ExtListViewItem)(lvZones.SelectedItems[0]));
            if (ZoneUnloadingActivated == null) return;
            fileListsDm.RemoveZoneFromLoadedList(elvi.FileName);
            RefreshZonesList();
            ZoneUnloadingActivated(elvi.FileName);
        }

        private void LvZonesMouseDown(object sender, MouseEventArgs e)
        {
            ExtListViewItem elvi = ((ExtListViewItem)(lvZones.GetItemAt(e.X, e.Y)));
            if (elvi == null) return;
            tsmiUnloadZone.Enabled = (elvi.StateImageIndex == 0 || elvi.StateImageIndex == 4);
            tsmiUnloadZone.Text = (elvi.StateImageIndex == 4) ? "Убрать из списка" : "Выгрузить";
            tsmiSaveZone.Enabled = (elvi.StateImageIndex == 3);
            tsmiEditLoaded.Enabled = (elvi.StateImageIndex != 2);
            tsmiPrepareAvailToSedind.Enabled = (elvi.StateImageIndex != 3 && elvi.StateImageIndex != 4);
        }

        private void TsmiSaveZoneClick(object sender, EventArgs e)
        {
            if (lvZones.SelectedItems.Count == 0) return;
            ExtListViewItem elvi = ((ExtListViewItem)(lvZones.SelectedItems[0]));
            ZoneSavingActivated?.Invoke(elvi.FileName);
        }

        private void TsmiPrepareLoadedToSedindClick(object sender, EventArgs e)
        {
            if (lvZones.SelectedItems.Count == 0) return;
            ExtListViewItem elvi = ((ExtListViewItem)(lvZones.SelectedItems[0]));
            ZonePrepareToSendActivated?.Invoke(elvi.FileName, elvi.Tag.ToString());
        }

        private void TsmiPrepareAvailToSedindClick(object sender, EventArgs e)
        {
            if (lvZonesAvail.SelectedItems.Count == 0) return;
            ExtListViewItem elvi = ((ExtListViewItem)(lvZonesAvail.SelectedItems[0]));
            ZonePrepareToSendActivated?.Invoke(elvi.FileName, elvi.Tag.ToString());
        }

        private void LvZonesAvailDoubleClick(object sender, EventArgs e)
        {
            if (lvZonesAvail.SelectedItems.Count == 0) return;
            TsmiLoadZoneClick(null, null);
        }

        private void TbQuickSearchAvailKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                LvZonesAvailDoubleClick(lvZonesAvail, null);
        }

        private void TbQuickSearchAvailTextChanged(object sender, EventArgs e)
        {
            if (tbQuickSearchAvail.Text.Length > 0)
                FindItemInList(lvZonesAvail, tbQuickSearchAvail.Text);
            else if (lvZonesAvail.Items.Count > 0)
                lvZonesAvail.Items[0].Selected = true;
        }

        private void TsmiLoadAndEditClick(object sender, EventArgs e)
        {
            if (lvZonesAvail.SelectedItems.Count == 0) return;
            string itemId = ((ExtListViewItem) (lvZonesAvail.SelectedItems[0])).FileName;
            TsmiLoadZoneClick(null, null);
            ExtListViewItem elvi = FindZoneByNumber(itemId);
            if (elvi == null) return;
            elvi.Selected = true;
            LvZonesDoubleClick(lvZones, null);
        }

        private void TsmiLoadZoneWinClick(object sender, EventArgs e)
        {
            if (lvZonesAvail.SelectedItems.Count == 0) return;
            ExtListViewItem elvi = ((ExtListViewItem)(lvZonesAvail.SelectedItems[0]));
            ZoneLoadingActivated?.Invoke(elvi.FileName, Encoding.GetEncoding(1251), true);
        }

        public void AddZoneToLoadedList(int number, string newName)
        {
            fileListsDm.AddZoneToLoadedList(number.ToString(), newName);
            RefreshZonesList();
        }

        private void LvZonesSizeChanged(object sender, EventArgs e)
        {
            if (lvZones.Columns[0].Width + 20 < lvZones.Width)
                lvZones.Columns[0].Width = -2;
        }

        private void LvZonesAvailSizeChanged(object sender, EventArgs e)
        {
            if (lvZonesAvail.Columns[0].Width + 20 < lvZonesAvail.Width)
                lvZonesAvail.Columns[0].Width = -2;
        }

        private void TabControlSelectedIndexChanged(object sender, EventArgs e)
        {
            //Если при обн6овлении списка зон были какие-то изменения, то рефрешим листвью
            if (fileListsDm.LoadAvailZones())
                RefreshZonesList();
        }

       private void CreateSketchClick(object sender, EventArgs e)
        {
            CreateSketchForm csf = new CreateSketchForm();
            if (csf.ShowDialog(this) == DialogResult.OK)
            {
                string res = fileListsDm.AddSketchToList(csf.Sketch.FileName, csf.Sketch.Name);
                if (!string.IsNullOrEmpty(res))
                {
                    MessageBox.Show(this, res, "Ошибка");
                    return;
                }
                RefreshSketchesList();
                SketchCreated?.Invoke(csf.Sketch);
            }
        }

        private void RemoveSketchClick(object sender, EventArgs e)
        {
            if (lvSketches.SelectedItems.Count == 0) return;
            ExtListViewItem elvi = ((ExtListViewItem)(lvSketches.SelectedItems[0]));
            if (MessageBox.Show(this, "Подтверждаете удаление эскиза \"" + elvi.Text + "\"?", "Удаление эскиза", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            fileListsDm.RemoveSketch(elvi.FileName);
            RefreshSketchesList();
            lvSketches.Items.Remove(elvi);
            SketchRemoved?.Invoke(elvi.Text);
        }

        private void RefreshSketchesListClick(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            fileListsDm.ReloadSketchesList();
            RefreshSketchesList();
            Cursor = Cursors.Default;
        }

        public Guid GetSketchGuidByFileName(string fileName)
        {
            foreach (ExtListViewItem elvi in lvSketches.Items)
            {
                if (elvi.FileName == fileName)
                    return elvi.ItemGuid;
            }
            return Guid.Empty;
        }

        private void LvSketchesSizeChanged(object sender, EventArgs e)
        {
            lvSketches.Columns[0].Width = -2;
        }

        private void TbQuickSearchSketchTextChanged(object sender, EventArgs e)
        {
            if (tbQuickSearchSketch.Text.Length > 0)
                FindItemInList(lvSketches, tbQuickSearchSketch.Text);
            else if (lvSketches.Items.Count > 0)
                lvSketches.Items[0].Selected = true;
        }

        private void LvSketchesDoubleClick(object sender, EventArgs e)
        {
            ItemDoubleClicked?.Invoke(sender, e);
        }

        private void TbQuickSearchSketchKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                LvSketchesDoubleClick(lvZonesAvail, null);
        }

        private void SaveSketchClick(object sender, EventArgs e)
        {
            if (lvSketches.SelectedItems.Count == 0) return;
            ExtListViewItem elvi = ((ExtListViewItem)(lvSketches.SelectedItems[0]));
            SketchSavingActivated?.Invoke(elvi.FileName);
        }

        private void LvSketchesMouseDown(object sender, MouseEventArgs e)
        {
            ExtListViewItem elvi = ((ExtListViewItem)(lvSketches.GetItemAt(e.X, e.Y)));
            if (elvi == null) return;
            tsmiSaveSketch.Enabled = (elvi.StateImageIndex == 3);
        }

        public string FindUnsavedSketches()
        {
            string res = string.Empty;
            foreach (ExtListViewItem elvi in lvSketches.Items)
                if (elvi.StateImageIndex == 8)
                    res += "\"" + elvi.Text + "\",";
            return res;
        }
    }
}