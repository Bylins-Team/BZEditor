using System;
using System.Windows.Forms;

namespace ExtControls
{
    public class ExtTreeNode : TreeNode
    {
        public Guid ItemGuid;
        public int Num;
        public string Type;

        public ExtTreeNode(string Name, int Num, string Type)
            : base(Name)
        {
            this.Num = Num;
            this.Type = Type;
            ImageIndex = -1;
            SelectedImageIndex = -1;
        }

        public ExtTreeNode(string Name, int Num, string Type, int ImageIndex, int SelectedImageIndex, Guid guid)
            : base(Name)
        {
            this.Num = Num;
            this.Type = Type;
            this.ImageIndex = ImageIndex;
            this.SelectedImageIndex = SelectedImageIndex;
            ItemGuid = guid;
        }
    }
}