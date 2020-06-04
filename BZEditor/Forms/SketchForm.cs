namespace BZEditor
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using DataUtils;
    using ExtControls;

    public partial class SketchForm : BaseMdiChildForm
    {
        #region Delegates & Events

        public delegate void ChangeEvent(Guid windowId, GlobalSketch sender);
        public event ChangeEvent Changed;

        public delegate void SaveEvent(Guid windowId, GlobalSketch sender);
        public event SaveEvent Saved;

        public delegate void ZonesGenerateEvent(Guid windowId, List<ZoneDataManager> zones);
        public event ZonesGenerateEvent ZonesGenerated;

        #endregion

        public string SketchName;

        public SketchForm()
        {
            InitializeComponent();
            wldSketch.SketchRoomAdded += SketchRoomAdded;
            wldSketch.SketchRoomReplaced += SketchRoomReplaced;
            wldSketch.SketchRoomRemoved += SketchRoomRemoved;
            wldSketch.SketchZoneSelected += SketchZoneSelected;
        }

        public SketchForm(GlobalSketch sketch):this()
        {
            Sketch = sketch;
            wldSketch.Sketch = Sketch;
            Sketch.Changed += SketchChanged;
            Sketch.Saved += SketchSaved;
            RefreahList();
        }

        public GlobalSketch Sketch { get; set; }

        void SketchChanged(GlobalSketch sender)
        {
            Changed?.Invoke((Guid)Tag, sender);
        }

        private void SketchSaved(GlobalSketch sender)
        {
            Saved?.Invoke((Guid)Tag, sender);
        }

        /// <summary>
        /// Обработка хотекеев
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int idx;
            switch (keyData)
            {
                #region Ctrl mod

                case Keys.Control | Keys.NumPad1:
                case Keys.Control | Keys.D1:
                    idx = 0;
                    break;
                case Keys.Control | Keys.NumPad2:
                case Keys.Control | Keys.D2:
                    idx = 1;
                    break;
                case Keys.Control | Keys.NumPad3:
                case Keys.Control | Keys.D3:
                    idx = 2;
                    break;
                case Keys.Control | Keys.NumPad4:
                case Keys.Control | Keys.D4:
                    idx = 3;
                    break;
                case Keys.Control | Keys.NumPad5:
                case Keys.Control | Keys.D5:
                    idx = 4;
                    break;
                case Keys.Control | Keys.NumPad6:
                case Keys.Control | Keys.D6:
                    idx = 5;
                    break;
                case Keys.Control | Keys.NumPad7:
                case Keys.Control | Keys.D7:
                    idx = 6;
                    break;
                case Keys.Control | Keys.NumPad8:
                case Keys.Control | Keys.D8:
                    idx = 7;
                    break;
                case Keys.Control | Keys.NumPad9:
                case Keys.Control | Keys.D9:
                    idx = 8;
                    break;
                case Keys.Control | Keys.NumPad0:
                case Keys.Control | Keys.D0:
                    idx = 9;
                    break;

                    #endregion

                default:
                    return base.ProcessCmdKey(ref msg, keyData);

            }
            if (idx > -1 && elvMainList.Items.Count > idx)
            {
                elvMainList.Items[idx].Selected = true;
                elvMainList.Items[idx].EnsureVisible();
            }
            return false;
        }

        private int lastEnteredNum;

        private void MenuAddZoneClick(object sender, EventArgs e)
        {
            AddZone();
        }

        private void MenuRemoveZoneClick(object sender, EventArgs e)
        {
            RemoveZone();
        }

        private void AddZoneClick(object sender, EventArgs e)
        {
            AddZone();
        }

        private void AddZone()
        {
            CreateSketchZoneForm cszf = new CreateSketchZoneForm(Sketch)
                                            {
                                                ZoneName = "Зона №" + Sketch.ZonesNames.Count,
                                                ZoneNum = lastEnteredNum + 1
                                            };
            if (cszf.ShowDialog() == DialogResult.OK)
            {
                Sketch.AddZone(cszf.ZoneNum, cszf.ZoneName, cszf.ZoneSketchColor);
                lastEnteredNum = cszf.ZoneNum;
                wldSketch.CurZoneNum = cszf.ZoneNum;
                elvMainList.Items.Add(PrepareListVievItem(cszf.ZoneNum));
                elvMainList.Items[elvMainList.Items.Count - 1].Selected = true;
                elvMainList.Items[elvMainList.Items.Count - 1].EnsureVisible();
            }
        }

        private void RemoveZone()
        {
            if (elvMainList.SelectedItems.Count == 0) return;
            int lastIdx = elvMainList.SelectedItems[0].Index;
            Sketch.RemoveZone((int) elvMainList.SelectedItems[0].Tag);
            elvMainList.Items.RemoveAt(lastIdx);
            if (elvMainList.Items.Count > lastIdx)
            {
                elvMainList.Items[lastIdx].Selected = true;
                elvMainList.Items[lastIdx].EnsureVisible();
            }
            else if (elvMainList.Items.Count > 0)
            {
                elvMainList.Items[0].Selected = true;
                elvMainList.Items[0].EnsureVisible();
            }
            wldSketch.RedrawBitmap();
        }

        private void StbZoomOutClick(object sender, EventArgs e)
        {
            wldSketch.MapScale--;
            if (wldSketch.MapScale == 1)
                stbZoomOut.Enabled = false;
            if (wldSketch.MapScale < 25 && !stbZoomIn.Enabled)
                stbZoomIn.Enabled = true;
        }

        private void StbZoomInClick(object sender, EventArgs e)
        {
            wldSketch.MapScale++;
            if (wldSketch.MapScale == 25)
                stbZoomIn.Enabled = false;
            if (wldSketch.MapScale > 1 && !stbZoomOut.Enabled)
                stbZoomOut.Enabled = true;
        }

        private void TsbZDecClick(object sender, EventArgs e)
        {
            if (wldSketch.CenterRoomZ > StaticData.MinZ)
                wldSketch.CenterRoomZ--;
        }

        private void TsbZIncClick(object sender, EventArgs e)
        {
            if (wldSketch.CenterRoomZ < StaticData.MaxZ)
                wldSketch.CenterRoomZ++;
        }

        private void TsbMapToCenterRoomClick(object sender, EventArgs e)
        {
            wldSketch.ToZeroPoint();
        }

        private void RefreahList()
        {
            elvMainList.BeginUpdate();
            elvMainList.Items.Clear();
            foreach (int num in Sketch.ZonesNumbers)
            {
                elvMainList.Items.Add(PrepareListVievItem(num));
            }
            elvMainList.EndUpdate();
        }

        private ExListViewItem PrepareListVievItem(int num)
        {
            ExListViewItem lvi = new ExListViewItem("   ") { Tag = num };
            lvi.SubItems.Add(Sketch.GetRoomsCount(num).ToString());
            lvi.SubItems.Add(num.ToString());
            lvi.SubItems.Add(Sketch.ZonesNames[num]);
            lvi.SubItems[0] = (new ExListViewSubItemColor(Sketch.ZonesColors[num]));
            return lvi;
        }

        private void LastColumnAutosize(object sender, EventArgs e)
        {
            if (((ListView)sender).Columns.Count < 1) return;
            int delta = 0;
            if (((ListView)sender).Columns.Count > 1)
            {
                for (int i = 0; i < ((ListView)sender).Columns.Count - 1; i++)
                    delta += ((ListView)sender).Columns[i].Width;
            }
            // ReSharper disable RedundantCheckBeforeAssignment
            if (((ListView)sender).Columns[((ListView)sender).Columns.Count - 1].Width !=
                ((ListView)sender).Width - 22 - delta)
            // ReSharper restore RedundantCheckBeforeAssignment
            {
                ((ListView)sender).Columns[((ListView)sender).Columns.Count - 1].Width = ((ListView)sender).Width -
                                                                                           22 - delta;
            }
        }

        private void MainListSelectedIndexChanged(object sender, EventArgs e)
        {
            if (elvMainList.SelectedItems.Count == 0) return;
            int num = (int)elvMainList.SelectedItems[0].Tag;
            wldSketch.CurZoneNum = num;
        }

        private void SketchRoomAdded(int zoneNum)
        {
            foreach (ExListViewItem lvi in elvMainList.Items)
            {
                if ((int)lvi.Tag == zoneNum)
                    lvi.SubItems[1].Text = Sketch.GetRoomsCount(zoneNum).ToString();
            }
        }

        private void SketchRoomReplaced(int zoneFromNum, int zoneToNum)
        {
            foreach (ExListViewItem lvi in elvMainList.Items)
            {
                if ((int)lvi.Tag == zoneFromNum)
                    lvi.SubItems[1].Text = Sketch.GetRoomsCount(zoneFromNum).ToString();
                if ((int)lvi.Tag == zoneToNum)
                    lvi.SubItems[1].Text = Sketch.GetRoomsCount(zoneToNum).ToString();

            }
        }

        private void SketchRoomRemoved(int zoneFromNum)
        {
            foreach (ExListViewItem lvi in elvMainList.Items)
            {
                if ((int)lvi.Tag == zoneFromNum)
                    lvi.SubItems[1].Text = Sketch.GetRoomsCount(zoneFromNum).ToString();
            }
        }

        private void SketchZoneSelected(int zoneNum)
        {
            foreach (ExListViewItem lvi in elvMainList.Items)
            {
                if ((int)lvi.Tag == zoneNum)
                {
                    lvi.Selected = true;
                    lvi.EnsureVisible();
                }
            }
        }

        private void SaveSketchClick(object sender, EventArgs e)
        {
            Sketch.SaveData();
        }

        private void GenerateComplexClick(object sender, EventArgs e)
        {
            ZonesComplexGenerator generator = new ZonesComplexGenerator();
            List<ZoneDataManager> zones = generator.Generate(Sketch);
            ZonesGenerated?.Invoke((Guid)Tag, zones);
        }

        private void MainListMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (elvMainList.SelectedItems.Count == 0) return;
            ExListViewItem lvi = (ExListViewItem)elvMainList.SelectedItems[0];
            int num = (int)lvi.Tag;
            CreateSketchZoneForm cszf = new CreateSketchZoneForm(Sketch)
            {
                ZoneName = Sketch.ZonesNames[num],
                ZoneNum = num,
                ZoneSketchColor = Sketch.ZonesColors[num]
            };
            if (cszf.ShowDialog() == DialogResult.OK)
            {
                Sketch.UpdateZone(num, cszf.ZoneNum, cszf.ZoneName, cszf.ZoneSketchColor);
                lvi.SubItems[0] = (new ExListViewSubItemColor(cszf.ZoneSketchColor));
                lvi.SubItems[2].Text = cszf.ZoneNum.ToString();
                lvi.Tag = cszf.ZoneNum;
                lvi.SubItems[3].Text = cszf.ZoneName;
                wldSketch.RedrawBitmap();
            }
        }
    }
}
