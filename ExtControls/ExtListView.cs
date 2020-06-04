using System;
using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ExtControls
{
    public class ExtListView : ListView
    {
        #region Delegates

        public delegate void ItemValueChangeEvent(ExListViewItem item, int subItemNumber, string prevVal, string newVal);

        #endregion

        private const UInt32 LvmFirst = 0x1000;
        private const UInt32 LvmScroll = (LvmFirst + 20);
        private const int WmHscroll = 0x114;
        private const int WmMousewheel = 0x020A;
        private const int WmVscroll = 0x115;
        private readonly TextBox txtBox; //the default edit control
        private ExListViewItem clickeditem; //clicked ListViewItem

        private ListViewItem.ListViewSubItem clickedsubitem; //clicked ListViewSubItem
        private int clickedSubItemNumber;
        private string clickedSubItemText;
        //private object _clickeditemtag;
        private int col; //index of doubleclicked ListViewSubItem
        private Brush highlightBrush; //color of highlighted items
        private int sortCol; //index of clicked ColumnHeader
        private Brush sortcolBrush; //color of items in sorted column

        public ExtListView()
        {
            sortCol = -1;
            sortcolBrush = SystemBrushes.ControlLight;
            highlightBrush = SystemBrushes.Highlight;
            OwnerDraw = true;
            FullRowSelect = true;
            View = View.Details;
            MouseDown += ThisMouseDown;
            MouseDoubleClick += ThisMouseDoubleClick;
            DrawColumnHeader += ThisDrawColumnHeader;
            DrawSubItem += ThisDrawSubItem;
            MouseMove += ThisMouseMove;
            ColumnClick += ThisColumnClick;
            txtBox = new TextBox {Visible = false};
            Controls.Add(txtBox);
            txtBox.Leave += CLeave;
            txtBox.KeyPress += TxtbxKeyPress;
        }

        public Brush MySortBrush
        {
            get { return sortcolBrush; }
            set { sortcolBrush = value; }
        }

        public Brush MyHighlightBrush
        {
            get { return highlightBrush; }
            set { highlightBrush = value; }
        }

        public event ItemValueChangeEvent ItemValueChanged;

        [DllImport("user32.dll")]
        private static extern bool SendMessage(IntPtr hWnd, UInt32 m, int wParam, int lParam);

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WmHscroll || m.Msg == WmVscroll || m.Msg == WmMousewheel)
                Focus();
            base.WndProc(ref m);
        }

        private void ScrollMe(int x, int y)
        {
            SendMessage(Handle, LvmScroll, x, y);
        }

        private void TxtbxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char) Keys.Return) return;
            clickedsubitem.Text = txtBox.Text;
            txtBox.Visible = false;
            clickeditem.Tag = null;
        }

        private void CLeave(object sender, EventArgs e)
        {
            var c = (Control) sender;
            ItemValueChanged?.Invoke(clickeditem, clickedSubItemNumber, clickedSubItemText, c.Text);
            clickedsubitem.Text = c.Text;
            c.Visible = false;
            clickeditem.Tag = null;
        }

        private void ThisMouseDown(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo lstvinfo = HitTest(e.X, e.Y);
            ListViewItem.ListViewSubItem subitem = lstvinfo.SubItem;
            if (subitem == null) return;
            int subx = subitem.Bounds.Left;
            if (subx < 0)
                ScrollMe(subx, 0);
        }

        private void ThisMouseDoubleClick(object sender, MouseEventArgs e)
        {
            ExListViewItem lstvItem = GetItemAt(e.X, e.Y) as ExListViewItem;
            if (lstvItem == null) return;
            clickeditem = lstvItem;
            int x = lstvItem.Bounds.Left;
            int i;
            for (i = 0; i < Columns.Count; i++)
            {
                x = x + Columns[i].Width;
                if (x <= e.X) continue;

                x = x - Columns[i].Width;
                clickedsubitem = lstvItem.SubItems[i];
                clickedSubItemNumber = i;
                clickedSubItemText = lstvItem.SubItems[i].Text;
                this.col = i;
                break;
            }
            if (!(Columns[i] is ExColumnHeader)) return;
            var col = (ExColumnHeader) Columns[i];
            if (col.GetType() == typeof (ExEditableColumnHeader))
            {
                var editcol = (ExEditableColumnHeader) col;
                if (editcol.MyControl != null)
                {
                    Control c = editcol.MyControl;
                    if (c.Tag != null)
                    {
                        Controls.Add(c);
                        c.Tag = null;
                        if (c is ComboBox)
                            ((ComboBox) c).SelectedValueChanged += CmbxSelectedValueChanged;
                        c.Leave += CLeave;
                    }
                    c.Location = new Point(x, GetItemRect(Items.IndexOf(lstvItem)).Y);
                    c.Width = Columns[i].Width;
                    if (c.Width > Width) c.Width = ClientRectangle.Width;
                    c.Text = clickedsubitem.Text;
                    c.Visible = true;
                    c.BringToFront();
                    c.Focus();
                }
                else
                {
                    txtBox.Location = new Point(x, GetItemRect(Items.IndexOf(lstvItem)).Y);
                    txtBox.Width = Columns[i].Width;
                    if (txtBox.Width > Width) txtBox.Width = ClientRectangle.Width;
                    txtBox.Text = clickedsubitem.Text;
                    txtBox.Visible = true;
                    txtBox.BringToFront();
                    txtBox.Focus();
                }
            }
            else if (col.GetType() == typeof (ExBoolColumnHeader))
            {
                var boolcol = (ExBoolColumnHeader) col;
                if (boolcol.Editable)
                {
                    var boolsubitem = (ExBoolListViewSubItem) clickedsubitem;
                    boolsubitem.BoolValue = !boolsubitem.BoolValue;
                    Invalidate(boolsubitem.Bounds);
                }
            }
        }

        private void CmbxSelectedValueChanged(object sender, EventArgs e)
        {
            if (((Control) sender).Visible == false || clickedsubitem == null) return;
            if (sender.GetType() == typeof (ExtComboBox))
            {
                var excmbx = (ExtComboBox) sender;
                object item = excmbx.SelectedItem;
                //Is this an combobox item with one image?
                if (item.GetType() == typeof (ExtComboBox.EXImageItem))
                {
                    var imgitem = (ExtComboBox.EXImageItem) item;
                    //Is the first column clicked -- in that case it's a ListViewItem
                    if (col == 0)
                    {
                        if (clickeditem.GetType() == typeof (ExImageListViewItem))
                            ((ExImageListViewItem) clickeditem).MyImage = imgitem.MyImage;
                        else if (clickeditem.GetType() == typeof (ExMultipleImagesListViewItem))
                        {
                            var imglstvitem = (ExMultipleImagesListViewItem) clickeditem;
                            imglstvitem.MyImages.Clear();
                            imglstvitem.MyImages.AddRange(new object[] {imgitem.MyImage});
                        }
                        //another column than the first one is clicked, so we have a ListViewSubItem
                    }
                    else
                    {
                        if (clickedsubitem.GetType() == typeof (ExImageListViewSubItem))
                        {
                            var imgsub = (ExImageListViewSubItem) clickedsubitem;
                            imgsub.MyImage = imgitem.MyImage;
                        }
                        else if (clickedsubitem.GetType() == typeof (ExMultipleImagesListViewSubItem))
                        {
                            var imgsub = (ExMultipleImagesListViewSubItem) clickedsubitem;
                            imgsub.MyImages.Clear();
                            imgsub.MyImages.Add(imgitem.MyImage);
                            imgsub.MyValue = imgitem.MyValue;
                        }
                    }
                    //or is this a combobox item with multiple images?
                }
                else if (item.GetType() == typeof (ExtComboBox.EXMultipleImagesItem))
                {
                    var imgitem = (ExtComboBox.EXMultipleImagesItem) item;
                    if (col == 0)
                    {
                        if (clickeditem.GetType() == typeof (ExImageListViewItem))
                            ((ExImageListViewItem) clickeditem).MyImage = (Image) imgitem.MyImages[0];
                        else if (clickeditem.GetType() == typeof (ExMultipleImagesListViewItem))
                        {
                            var imglstvitem = (ExMultipleImagesListViewItem) clickeditem;
                            imglstvitem.MyImages.Clear();
                            imglstvitem.MyImages.AddRange(imgitem.MyImages);
                        }
                    }
                    else
                    {
                        if (clickedsubitem.GetType() == typeof (ExImageListViewSubItem))
                        {
                            var imgsub = (ExImageListViewSubItem) clickedsubitem;
                            if (imgitem.MyImages != null)
                                imgsub.MyImage = (Image) imgitem.MyImages[0];
                        }
                        else if (clickedsubitem.GetType() == typeof (ExMultipleImagesListViewSubItem))
                        {
                            var imgsub = (ExMultipleImagesListViewSubItem) clickedsubitem;
                            imgsub.MyImages.Clear();
                            imgsub.MyImages.AddRange(imgitem.MyImages);
                            imgsub.MyValue = imgitem.MyValue;
                        }
                    }
                }
            }
            var c = (ComboBox) sender;
            clickedsubitem.Text = c.Text;
            c.Visible = false;
            clickeditem.Tag = null;
        }

        private void ThisMouseMove(object sender, MouseEventArgs e)
        {
            ListViewItem item = GetItemAt(e.X, e.Y);
            if (item == null || item.Tag != null) return;
            Invalidate(item.Bounds);
            item.Tag = "t";
        }

        private static void ThisDrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void ThisDrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawBackground();
            if (e.ColumnIndex == sortCol)
                e.Graphics.FillRectangle(sortcolBrush, e.Bounds);
            if ((e.ItemState & ListViewItemStates.Selected) != 0)
                e.Graphics.FillRectangle(highlightBrush, e.Bounds);
            int fonty = e.Bounds.Y + (e.Bounds.Height/2) - (e.SubItem.Font.Height/2);
            int x = e.Bounds.X + 2;
            if (e.ColumnIndex == 0)
            {
                if (e.SubItem is ExListViewSubItemColor)
                {
                    e.Graphics.FillRectangle(new SolidBrush(e.SubItem.ForeColor), e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);                  
                }
                else
                {
                    var item = (ExListViewItem)e.Item;
                    if (item.GetType() == typeof(ExImageListViewItem))
                    {
                        var imageitem = (ExImageListViewItem) item;
                        if (imageitem.MyImage != null)
                        {
                            Image img = imageitem.MyImage;
                            int imgy = e.Bounds.Y + (e.Bounds.Height/2) - (img.Height/2);
                            e.Graphics.DrawImage(img, x, imgy, img.Width, img.Height);
                            x += img.Width + 2;
                        }
                    }
                    e.Graphics.DrawString(e.SubItem.Text, e.SubItem.Font, new SolidBrush(e.SubItem.ForeColor), x, fonty);
                }
                return;
            }
            var subitem = e.SubItem as ExListViewSubItemAb;
            if (subitem == null)
                e.DrawDefault = true;
            else
            {
                x = subitem.DoDraw(e, x, Columns[e.ColumnIndex] as ExColumnHeader);
                e.Graphics.DrawString(e.SubItem.Text, e.SubItem.Font, new SolidBrush(e.SubItem.ForeColor), x, fonty);
            }
        }

        private void ThisColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (Items.Count == 0) return;
            for (int i = 0; i < Columns.Count; i++)
                Columns[i].ImageKey = null;
            for (int i = 0; i < Items.Count; i++)
                Items[i].Tag = null;
            if (e.Column != sortCol)
            {
                sortCol = e.Column;
                Sorting = SortOrder.Ascending;
                Columns[e.Column].ImageKey = "up";
            }
            else
            {
                if (Sorting == SortOrder.Ascending)
                {
                    Sorting = SortOrder.Descending;
                    Columns[e.Column].ImageKey = "down";
                }
                else
                {
                    Sorting = SortOrder.Ascending;
                    Columns[e.Column].ImageKey = "up";
                }
            }
            if (sortCol == 0)
            {
                //ListViewItem
                if (Items[0].GetType() == typeof (ExListViewItem))
                {
                    //sorting on text
                    ListViewItemSorter = new ListViewItemComparerText(e.Column, Sorting);
                }
                else
                {
                    //sorting on value
                    ListViewItemSorter = new ListViewItemComparerValue(e.Column, Sorting);
                }
            }
            else
            {
                //ListViewSubItem
                if (Items[0].SubItems[sortCol].GetType() == typeof (ExListViewSubItemAb))
                {
                    //sorting on text
                    ListViewItemSorter = new ListViewSubItemComparerText(e.Column, Sorting);
                }
                else
                {
                    //sorting on value
                    ListViewItemSorter = new ListViewSubItemComparerValue(e.Column, Sorting);
                }
            }
        }

        #region Nested type: ListViewItemComparerText

        private class ListViewItemComparerText : IComparer
        {
            private readonly SortOrder order = SortOrder.Ascending;
            //private int col;

            /*public ListViewItemComparerText()
            {
                col = 0;
                Order = SortOrder.Ascending;
            }*/

            public ListViewItemComparerText(int col, SortOrder order)
            {
                //col = col;
                this.order = order;
            }

            #region IComparer Members

            public int Compare(object x, object y)
            {
                int returnVal;

                string xstr = ((ListViewItem) x).Text;
                string ystr = ((ListViewItem) y).Text;

                if (Decimal.TryParse(xstr, out decimal decX) && Decimal.TryParse(ystr, out var decY))
                    returnVal = Decimal.Compare(decX, decY);
                else if (DateTime.TryParse(xstr, out var datX) && DateTime.TryParse(ystr, out var datY))
                    returnVal = DateTime.Compare(datX, datY);
                else
                    returnVal = string.CompareOrdinal(xstr, ystr);
                if (order == SortOrder.Descending) returnVal *= -1;
                return returnVal;
            }

            #endregion
        }

        #endregion

        #region Nested type: ListViewItemComparerValue

        private class ListViewItemComparerValue : IComparer
        {
            private readonly SortOrder order = SortOrder.Ascending;
            private int col;

            /*public ListViewItemComparerValue()
            {
                col = 0;
                Order = SortOrder.Ascending;
            }*/

            public ListViewItemComparerValue(int col, SortOrder order)
            {
                this.col = col;
                this.order = order;
            }

            #region IComparer Members

            public int Compare(object x, object y)
            {
                int returnVal;

                string xstr = ((ExListViewItem) x)?.MyValue;
                string ystr = ((ExListViewItem) y)?.MyValue;

                if (Decimal.TryParse(xstr, out decimal decX) && Decimal.TryParse(ystr, out decimal decY))
                    returnVal = Decimal.Compare(decX, decY);
                else if (DateTime.TryParse(xstr, out var datX) && DateTime.TryParse(ystr, out var datY))
                    returnVal = DateTime.Compare(datX, datY);
                else
                    returnVal = String.CompareOrdinal(xstr, ystr);
                if (order == SortOrder.Descending) returnVal *= -1;
                return returnVal;
            }

            #endregion
        }

        #endregion

        #region Nested type: ListViewSubItemComparerText

        private class ListViewSubItemComparerText : IComparer
        {
            private readonly int col;
            private readonly SortOrder order;

            /*public ListViewSubItemComparerText()
            {
                col = 0;
                order = SortOrder.Ascending;
            }*/

            public ListViewSubItemComparerText(int col, SortOrder order)
            {
                this.col = col;
                this.order = order;
            }

            #region IComparer Members

            public int Compare(object x, object y)
            {
                int returnVal;

                string xstr = ((ListViewItem) x).SubItems[col].Text;
                string ystr = ((ListViewItem) y).SubItems[col].Text;


                if (Decimal.TryParse(xstr, out decimal decX) && Decimal.TryParse(ystr, out decimal decY))
                    returnVal = Decimal.Compare(decX, decY);
                else if (DateTime.TryParse(xstr, out DateTime datX) && DateTime.TryParse(ystr, out DateTime datY))
                    returnVal = DateTime.Compare(datX, datY);
                else
                    returnVal = String.Compare(xstr, ystr);
                if (order == SortOrder.Descending) returnVal *= -1;
                return returnVal;
            }

            #endregion
        }

        #endregion

        #region Nested type: ListViewSubItemComparerValue

        private class ListViewSubItemComparerValue : IComparer
        {
            private readonly int col;
            private readonly SortOrder order = SortOrder.Ascending;

            /*public ListViewSubItemComparerValue()
            {
                col = 0;
                order = SortOrder.Ascending;
            }*/

            public ListViewSubItemComparerValue(int col, SortOrder order)
            {
                this.col = col;
                this.order = order;
            }

            #region IComparer Members

            public int Compare(object x, object y)
            {
                int returnVal;

                string xstr = ((ExListViewSubItemAb) ((ListViewItem) x).SubItems[col]).MyValue;
                string ystr = ((ExListViewSubItemAb) ((ListViewItem) y).SubItems[col]).MyValue;

                if (Decimal.TryParse(xstr, out var decX) && Decimal.TryParse(ystr, out var decY))
                    returnVal = Decimal.Compare(decX, decY);
                else if (DateTime.TryParse(xstr, out var datX) && DateTime.TryParse(ystr, out var datY))
                    returnVal = DateTime.Compare(datX, datY);
                else
                    returnVal = String.Compare(xstr, ystr);
                if (order == SortOrder.Descending) returnVal *= -1;
                return returnVal;
            }

            #endregion
        }

        #endregion
    }

    public class ExColumnHeader : ColumnHeader
    {
        public ExColumnHeader()
        {
        }

        public ExColumnHeader(string text)
        {
            Text = text;
        }

        public ExColumnHeader(string text, int width)
        {
            Text = text;
            Width = width;
        }
    }

    public class ExEditableColumnHeader : ExColumnHeader
    {
        private Control curControl;

        public ExEditableColumnHeader()
        {
        }

        public ExEditableColumnHeader(string text)
        {
            Text = text;
        }

        public ExEditableColumnHeader(string text, int width)
        {
            Text = text;
            Width = width;
        }

        public ExEditableColumnHeader(string text, Control control)
        {
            Text = text;
            MyControl = control;
        }

        public ExEditableColumnHeader(string text, Control control, int width)
        {
            Text = text;
            MyControl = control;
            Width = width;
        }

        public Control MyControl
        {
            get { return curControl; }
            set
            {
                curControl = value;
                curControl.Visible = false;
                curControl.Tag = "not_init";
            }
        }
    }

    public class ExBoolColumnHeader : ExColumnHeader
    {
        private bool isEditable;

        public ExBoolColumnHeader()
        {
            Init();
        }

        public ExBoolColumnHeader(string text)
        {
            Init();
            Text = text;
        }

        public ExBoolColumnHeader(string text, int width)
        {
            Init();
            Text = text;
            Width = width;
        }

        public ExBoolColumnHeader(string text, Image trueimage, Image falseimage)
        {
            Init();
            Text = text;
            TrueImage = trueimage;
            FalseImage = falseimage;
        }

        public ExBoolColumnHeader(string text, Image trueimage, Image falseimage, int width)
        {
            Init();
            Text = text;
            TrueImage = trueimage;
            FalseImage = falseimage;
            Width = width;
        }

        public Image TrueImage { get; set; }

        public Image FalseImage { get; set; }

        public bool Editable
        {
            get => isEditable;
            set => isEditable = value;
        }

        private void Init()
        {
            isEditable = false;
        }
    }

    public abstract class ExListViewSubItemAb : ListViewItem.ListViewSubItem
    {
        private string value = "";

        protected ExListViewSubItemAb()
        {
        }

        protected ExListViewSubItemAb(string text)
        {
            Text = text;
        }

        public string MyValue
        {
            get { return value; }
            set { this.value = value; }
        }

        //return the new x coordinate
        public abstract int DoDraw(DrawListViewSubItemEventArgs e, int x, ExColumnHeader ch);
    }

    public class ExListViewSubItem : ExListViewSubItemAb
    {
        public ExListViewSubItem()
        {
        }

        public ExListViewSubItem(string text)
        {
            Text = text;
        }

        public override int DoDraw(DrawListViewSubItemEventArgs e, int x, ExColumnHeader ch)
        {
            return x;
        }
    }

    public class ExListViewSubItemColor : ExListViewSubItemAb
    {
        public ExListViewSubItemColor()
        {
        }

        public ExListViewSubItemColor(Color color)
        {
            ForeColor = color;
        }

        public override int DoDraw(DrawListViewSubItemEventArgs e, int x, ExColumnHeader ch)
        {
            e.Graphics.FillRectangle(new SolidBrush(ForeColor), e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);
            return x;
        }
    }

    public class ExImageListViewSubItem : ExListViewSubItemAb
    {
        public ExImageListViewSubItem()
        {
        }

        public ExImageListViewSubItem(string text)
        {
            Text = text;
        }

        public ExImageListViewSubItem(Image image)
        {
            MyImage = image;
        }

        public ExImageListViewSubItem(Image image, string value)
        {
            MyImage = image;
            MyValue = value;
        }

        public ExImageListViewSubItem(string text, Image image, string value)
        {
            Text = text;
            MyImage = image;
            MyValue = value;
        }

        public Image MyImage { get; set; }

        public override int DoDraw(DrawListViewSubItemEventArgs e, int x, ExColumnHeader ch)
        {
            if (MyImage != null)
            {
                Image img = MyImage;
                int imgy = e.Bounds.Y + (e.Bounds.Height/2) - (img.Height/2);
                e.Graphics.DrawImage(img, x, imgy, img.Width, img.Height);
                x += img.Width + 2;
            }
            return x;
        }
    }

    public class ExMultipleImagesListViewSubItem : ExListViewSubItemAb
    {
        public ExMultipleImagesListViewSubItem()
        {
        }

        public ExMultipleImagesListViewSubItem(string text)
        {
            Text = text;
        }

        public ExMultipleImagesListViewSubItem(ArrayList images)
        {
            MyImages = images;
        }

        public ExMultipleImagesListViewSubItem(ArrayList images, string value)
        {
            MyImages = images;
            MyValue = value;
        }

        public ExMultipleImagesListViewSubItem(string text, ArrayList images, string value)
        {
            Text = text;
            MyImages = images;
            MyValue = value;
        }

        public ArrayList MyImages { get; set; }

        public override int DoDraw(DrawListViewSubItemEventArgs e, int x, ExColumnHeader ch)
        {
            if (MyImages != null && MyImages.Count > 0)
            {
                for (int i = 0; i < MyImages.Count; i++)
                {
                    var img = (Image) MyImages[i];
                    int imgy = e.Bounds.Y + (e.Bounds.Height/2) - (img.Height/2);
                    e.Graphics.DrawImage(img, x, imgy, img.Width, img.Height);
                    x += img.Width + 2;
                }
            }
            return x;
        }
    }

    public class ExBoolListViewSubItem : ExListViewSubItemAb
    {
        private bool value;

        public ExBoolListViewSubItem()
        {
        }

        public ExBoolListViewSubItem(bool val)
        {
            value = val;
            MyValue = val.ToString();
        }

        public bool BoolValue
        {
            get { return value; }
            set
            {
                this.value = value;
                MyValue = value.ToString();
            }
        }

        public override int DoDraw(DrawListViewSubItemEventArgs e, int x, ExColumnHeader ch)
        {
            var boolcol = (ExBoolColumnHeader) ch;
            Image boolimg = BoolValue ? boolcol.TrueImage : boolcol.FalseImage;
            int imgy = e.Bounds.Y + (e.Bounds.Height/2) - (boolimg.Height/2);
            e.Graphics.DrawImage(boolimg, x, imgy, boolimg.Width, boolimg.Height);
            x += boolimg.Width + 2;
            return x;
        }
    }

    /// <summary>
    /// Тип выполняемого действия
    /// </summary>
    public enum ActionType
    {
        DoNothing,
        GoToRoom,
        GoToRoomLoadedMobs,
        GoToRoomLoadedObjects,
        GoToRoomUnloadedObjects,
        GoToMob,
        GoToObject,
        GoToTrigger
    }

    public class ExListViewItem : ListViewItem
    {
        public ActionType Action = ActionType.DoNothing;
        public Guid Guid;
        public int TrgVNum;
        public int TrgVNum2;

        public ExListViewItem(string[] items)
            : base(items)
        {
            Text = items[0];
        }

        public ExListViewItem()
        {
        }

        public ExListViewItem(string text)
        {
            Text = text;
        }

        public string MyValue { get; set; }
        public bool HighlightSelected { get; set; }
    }

    public class ExImageListViewItem : ExListViewItem
    {
        public ExImageListViewItem()
        {
        }

        public ExImageListViewItem(string text)
        {
            Text = text;
        }

        public ExImageListViewItem(Image image)
        {
            MyImage = image;
        }

        public ExImageListViewItem(string text, Image image)
        {
            MyImage = image;
            Text = text;
        }

        public ExImageListViewItem(string text, Image image, string value)
        {
            Text = text;
            MyImage = image;
            MyValue = value;
        }

        public Image MyImage { get; set; }
    }

    public class ExMultipleImagesListViewItem : ExListViewItem
    {
        public ExMultipleImagesListViewItem()
        {
        }

        public ExMultipleImagesListViewItem(string text)
        {
            Text = text;
        }

        public ExMultipleImagesListViewItem(ArrayList images)
        {
            MyImages = images;
        }

        public ExMultipleImagesListViewItem(string text, ArrayList images)
        {
            Text = text;
            MyImages = images;
        }

        public ExMultipleImagesListViewItem(string text, ArrayList images, string value)
        {
            Text = text;
            MyImages = images;
            MyValue = value;
        }

        public ArrayList MyImages { get; set; }
    }
}