namespace BZEditor
{
    public class TaggedComboBoxItem : object
    {
        public int Number;
        public object Tag;
        public string Text;

        public TaggedComboBoxItem()
        {
        }

        public TaggedComboBoxItem(object tag, string text, int number)
        {
            Tag = tag;
            Text = text;
            Number = number;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}