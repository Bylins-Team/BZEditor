using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using ExtControls;

namespace BZEditor
{
    public partial class UcTwoPanelsList : UserControl
    {
        #region Delegates

        public delegate void AddEvent(string[] val);

        public delegate void RemoveEvent(string[] val);

        public delegate void SelectionChangeEvent(object args);

        public delegate void ValueChangeEvent(object args);

        #endregion

        private DataTable availData;

        private ArrayList dataA = new ArrayList();
        private bool dataIsString = true;
        private string dataS = "";
        private ListItemDescCollection iDescColl = new ListItemDescCollection();
        private bool iGrouped;
        public bool MultiRoomsMode;

        #region Get/Set

        public ListItemDescCollection Descriptors
        {
            get { return iDescColl; }
            set { iDescColl = value; }
        }

        public bool Grouped
        {
            get { return iGrouped; }
            set { iGrouped = value; }
        }

        public string LeftListCaption
        {
            get { return lLeft.Text; }
            set { lLeft.Text = value; }
        }

        public string RightListCaption
        {
            get { return lRight.Text; }
            set { lRight.Text = value; }
        }

        public int SplitterDistance
        {
            get { return splitContainer.SplitterDistance; }
            set { splitContainer.SplitterDistance = value; }
        }

        #endregion

        public UcTwoPanelsList()
        {
            InitializeComponent();
        }

        public event RemoveEvent Removed;
        public event AddEvent Added;
        public event ValueChangeEvent ValueChanged;
        public event SelectionChangeEvent SelectionChanged;

        private void LastColumnAutosize(object sender, EventArgs e)
        {
            if (((ListView) sender).Columns.Count < 1) return;
            if (((ListView) sender).Columns[((ListView) sender).Columns.Count - 1].Width !=
                ((ListView) sender).Width - 22)
                ((ListView) sender).Columns[((ListView) sender).Columns.Count - 1].Width = ((ListView) sender).Width -
                                                                                           22;
        }

        public void SetData(string data, DataTable availableData)
        {
            dataIsString = true;
            dataS = data;
            availData = availableData;
            RefreshData();
        }

        public void SetData(ArrayList data, DataTable availableData)
        {
            dataIsString = false;
            dataA = data;
            availData = availableData;
            RefreshData();
        }

        public void RefreshData()
        {
            lvLeft.BeginUpdate();
            lvRight.BeginUpdate();
            lvLeft.Items.Clear();
            lvRight.Items.Clear();
            lvLeft.Groups.Clear();
            lvRight.Groups.Clear();
            foreach (DataRow dr in availData.Rows)
                ListAddRow(dr);
            lvLeft.EndUpdate();
            lvRight.EndUpdate();
        }

        private void ListAddRow(DataRow dr)
        {
            string val = dr["val"].ToString();
            string name = dr["desc"].ToString();
            string group = dr["group"].ToString();
            //ListViewItem lvi = new ListViewItem(new string[] { Name });
            var lvi = new GListViewItem {GName = group, Text = name};
            ListItemDesc itemDesc = Descriptors.Get(val);
            if (itemDesc != null)
            {
                //if (ItemDesc.ItemBGColor != null)
                lvi.BackColor = itemDesc.ItemBGColor;
                //if (ItemDesc.ItemFGColor != null)
                lvi.ForeColor = itemDesc.ItemFGColor;
            }

            lvi.Tag = val;

            bool result = dataIsString ? dataS.Contains(val) : dataA.Contains(Convert.ToInt32(val));
            if (result)
            {
                if (iGrouped)
                {
                    ListViewGroup lvg = lvLeft.Groups[group];
                    if (lvg == null)
                    {
                        lvg = new ListViewGroup(group, group);
                        lvLeft.Groups.Add(lvg);
                    }
                    lvi.Group = lvg;
                    lvLeft.Items.Add(lvi);
                }
                else
                    lvLeft.Items.Add(lvi);

                if (MultiRoomsMode)
                {
                    var lvi1 = (ListViewItem) (lvi.Clone());
                    if (iGrouped)
                    {
                        ListViewGroup lvg1 = lvRight.Groups[group];
                        if (lvg1 == null)
                        {
                            lvg1 = new ListViewGroup(group, group);
                            lvRight.Groups.Add(lvg1);
                        }
                        lvi1.Group = lvg1;
                    }
                    lvRight.Items.Add(lvi1);
                }
            }
            else
            {
                if (iGrouped)
                {
                    ListViewGroup lvg = lvRight.Groups[group];
                    if (lvg == null)
                    {
                        lvg = new ListViewGroup(group, group);
                        lvRight.Groups.Add(lvg);
                    }
                    lvi.Group = lvg;
                }
                lvRight.Items.Add(lvi);
            }
        }

        private object GetResult()
        {
            return dataIsString ? (object) dataS : dataA;
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            lvLeft.BeginUpdate();
            lvRight.BeginUpdate();
            var res = new List<string>();
            var moved = new List<GListViewItem>();
            foreach (GListViewItem lvi in lvRight.SelectedItems)
            {
                moved.Add(lvi);
                if (dataIsString)
                {
                    dataS += lvi.Tag.ToString();
                    res.Add(lvi.Tag.ToString());
                }
                else
                    dataA.Add(Convert.ToInt32(lvi.Tag));
            }
            foreach (GListViewItem lvi in moved)
            {
                lvRight.Items.Remove(lvi);
                if (iGrouped)
                {
                    ListViewGroup lvg = lvLeft.Groups[lvi.GName];
                    if (lvg == null)
                    {
                        lvg = new ListViewGroup(lvi.GName, lvi.GName);
                        lvLeft.Groups.Add(lvg);
                    }
                    lvi.Group = lvg;
                }
                lvLeft.Items.Add(lvi);
            }

            if (MultiRoomsMode)
            {
                Added?.Invoke(res.ToArray());
            }
            else
            {
                ValueChanged?.Invoke(GetResult());
            }
            lvLeft.EndUpdate();
            lvRight.EndUpdate();
        }

        private void tsbRemove_Click(object sender, EventArgs e)
        {
            lvLeft.BeginUpdate();
            lvRight.BeginUpdate();
            var res = new List<string>();
            var moved = new List<GListViewItem>();
            foreach (GListViewItem lvi in lvLeft.SelectedItems)
            {
                moved.Add(lvi);
                if (dataIsString)
                {
                    dataS = dataS.Replace(lvi.Tag.ToString(), "");
                    res.Add(lvi.Tag.ToString());
                }
                else
                    dataA.Remove(Convert.ToInt32(lvi.Tag));
            }
            foreach (GListViewItem lvi in moved)
            {
                lvLeft.Items.Remove(lvi);
                if (iGrouped)
                {
                    ListViewGroup lvg = lvRight.Groups[lvi.GName];
                    if (lvg == null)
                    {
                        lvg = new ListViewGroup(lvi.GName, lvi.GName);
                        lvRight.Groups.Add(lvg);
                    }
                    lvi.Group = lvg;
                }
                lvRight.Items.Add(lvi);
            }
            //RefreshData();
            if (MultiRoomsMode)
            {
                Removed?.Invoke(res.ToArray());
            }
            else
            {
                ValueChanged?.Invoke(GetResult());
            }
            lvLeft.EndUpdate();
            lvRight.EndUpdate();
        }

        private void lvRight_DoubleClick(object sender, EventArgs e)
        {
            tsbAdd_Click(null, null);
        }

        private void lvLeft_DoubleClick(object sender, EventArgs e)
        {
            tsbRemove_Click(null, null);
        }

        private void LvSelectedIndexChanged(object sender, EventArgs e)
        {
            var lv = sender as ListView;
            if (SelectionChanged != null)
            {
                if (lv == null || lv.SelectedItems.Count == 0)
                    SelectionChanged("");
                else
                    SelectionChanged(lv.SelectedItems[0].Tag.ToString());
            }
        }
    }

    internal class GListViewItem : ListViewItem
    {
        public string GName;
    }
}