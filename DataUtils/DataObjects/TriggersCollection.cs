using System.Linq;

namespace DataUtils
{
    public sealed class TriggersCollection : GenericDataList<Trigger>
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
            return _list.FirstOrDefault(trigger => trigger.VNum == vNum);
        }

        public int AddTrigger(int zoneNum)
        {
            int vnum = GetFirstFreeVNum(zoneNum);
            if (vnum < 0) return 0;

            Trigger trigger = new Trigger(vnum) { Name = ("Новый триггер " + vnum) };
            trigger.Changed += FireChangeEvent;
            Add(trigger);
            var comparer = new GenericDataObjectComparer<Trigger>();
            _list.Sort(comparer);
            return trigger.VNum;
        }
    }
}
