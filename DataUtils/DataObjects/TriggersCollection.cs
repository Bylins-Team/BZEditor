namespace DataUtils
{
    public class TriggersCollection : BaseDataArrayList
    {
        #region Delegates

        public new delegate void ChangeEvent(object changedClass, object sender);

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
        public Trigger this[int vNum, int tmp] => GetTrigger(vNum);

        public Trigger GetTrigger(int vNum)
        {
            foreach (Trigger trigger in this)
            {
                if (trigger.VNum == vNum)
                    return trigger;
            }
            return null;
        }

        public int AddTrigger(int zoneNum)
        {
            int vnum = GetFirstFreeVNum(zoneNum);
            if (vnum < 0) return 0;

            Trigger trigger = new Trigger(vnum) { Name = ("Новый триггер " + vnum) };
            trigger.Changed += FireChangeEvent;
            Add(trigger);
            Sort(new BaseDataObjectComparer());
            return trigger.VNum;
        }
    }
}