namespace ExtControls
{
    /// <summary>
    /// This class provides more informations about the items in the listbox.
    /// </summary>
    public class ItemInfo
    {
        /// <summary>
        /// Is the height valid?
        /// </summary>
        private bool bHeightValid;

        /// <summary>
        /// Height of the item.
        /// </summary>
        private int iHeight;

        /// <summary>
        /// Message from user.
        /// </summary>
        private readonly ParseMessageEventArgs pmeaMessage;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ItemInfo(ParseMessageEventArgs pmea)
        {
            iHeight = 0;
            bHeightValid = false;
            pmeaMessage = pmea;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public ItemInfo(int height, bool heightValid, ParseMessageEventArgs pmea)
        {
            iHeight = height;
            bHeightValid = heightValid;
            pmeaMessage = pmea;
        }

        /// <summary>
        /// Height of the item.
        /// </summary>
        public int Height
        {
            get { return iHeight; }
            set
            {
                iHeight = value;
                bHeightValid = true;
            }
        }

        /// <summary>
        /// Is the height valid?
        /// </summary>
        public bool HeightValid
        {
            get { return bHeightValid; }
            set { bHeightValid = value; }
        }

        /// <summary>
        /// Message from user.
        /// </summary>
        public ParseMessageEventArgs Message
        {
            get { return pmeaMessage; }
        }
    }
}