using System.Drawing;

namespace ExtControls
{
    public class ListItemDesc
    {
        private Color iItemBGColor;
        private Color iItemFGColor;
        private string iVal = "";
        public int Order;

        public ListItemDesc(string Val)
        {
            this.Val = Val;
        }

        public ListItemDesc(string Val, Color ItemFGColor, Color ItemBGColor, int Order)
        {
            this.Val = Val;
            this.ItemFGColor = ItemFGColor;
            this.ItemBGColor = ItemBGColor;
            this.Order = Order;
        }

        public string Val
        {
            get { return iVal; }
            set { iVal = value; }
        }

        public Color ItemFGColor
        {
            get { return iItemFGColor; }
            set { iItemFGColor = value; }
        }

        public Color ItemBGColor
        {
            get { return iItemBGColor; }
            set { iItemBGColor = value; }
        }
    }
}