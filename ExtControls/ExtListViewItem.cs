using System;
using System.Windows.Forms;

namespace ExtControls
{
    public class ExtListViewItem : ListViewItem
    {
        public Guid ItemGuid {get;set;}
        public string FileName { get; set; }
        public string Type { get; set; }

        public ExtListViewItem(string name, string fileName, string type)
            : base(name)
        {
            FileName = fileName;
            Type = type;
            ImageIndex = -1;
            StateImageIndex = -1;
        }

        public ExtListViewItem(string text, string fileName, string type, int imageIndex, int stateImageIndex, Guid guid)
            : base(text)
        {
            FileName = fileName;
            Type = type;
            ImageIndex = imageIndex;
            StateImageIndex = stateImageIndex;
                //Это можно использовать для отображения измененной зоны и не сохраненной
            ItemGuid = guid;
        }
    }
}