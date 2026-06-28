namespace DataUtils
{
    public class ShopsCollection : BaseDataArrayList
    {
        #region Delegates

        public delegate void ChangeEvent(object changedClass, object sender);

        #endregion

        public new event ChangeEvent Changed;

        public override void FireChangeEvent(object sender)
        {
            if (Changed != null && StaticData.CanFireChangeEvent)
                Changed(this, sender);
        }

        /// <summary>
        /// Возвращает ссылку
        /// </summary>
        public Shop this[int vNum, int tmp]
        {
            get { return GetShop(vNum); }
        }

        private Shop GetShop(int vNum)
        {
            foreach (Shop shop in this)
            {
                if (shop.VNum == vNum)
                    return shop;
            }
            return null;
        }

        public int AddShop(int zoneNum)
        {
            int vnum = GetFirstFreeVNum(zoneNum);
            if (vnum < 0) return 0;

            var shop = new Shop(vnum);
            shop.Changed += FireChangeEvent;
            Add(shop);
            Sort(new BaseDataObjectComparer());
            return shop.VNum;
        }
    }
}