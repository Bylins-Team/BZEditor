using System.Collections;

namespace ExtControls
{
    public class ListItemDescCollection : ArrayList
    {
        public ListItemDesc Get(string Val)
        {
            foreach (ListItemDesc ListItemDesc in this)
            {
                if (ListItemDesc.Val == Val)
                    return ListItemDesc;
            }
            return null;
        }
    }
}