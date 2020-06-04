using System.Collections;

namespace DataUtils
{
    public class BaseDataArrayList : ArrayList
    {
        #region Delegates

        public delegate void ChangeEvent(object sender);

        #endregion

        public event ChangeEvent Changed;

        public virtual void FireChangeEvent(object sender)
        {
            if (Changed != null && StaticData.CanFireChangeEvent)
                Changed(this);
        }

        public override int Add(object value)
        {
            if (value is BaseDataObject)
                ((BaseDataObject) value).Changed += FireChangeEvent;
            FireChangeEvent(this);
            return base.Add(value);
        }

        public override void Remove(object obj)
        {
            FireChangeEvent(this);
            base.Remove(obj);
        }

        public override void Clear()
        {
            FireChangeEvent(this);
            base.Clear();
        }

        public override object Clone()
        {
            var result = new BaseDataArrayList();
            foreach (object o in this)
                result.Add(o);
            return result;
        }

        public int GetFirstFreeVNum(int zoneNum)
        {
            int res = -1;
            int cntr = zoneNum * 100;
            foreach (BaseDataObject o in this)
            {
                if (cntr - o.VNum < 0)
                {
                    res = cntr - 1;
                    break;
                }
                if (res < o.VNum)
                    res = o.VNum;
                cntr++;
            }
            return res < 0 ? zoneNum * 100 : res + 1;
        }

        public int GetLastVNum()
        {
            if (Count == 0) return 0;
            Sort(new BaseDataObjectComparer());
            return ((BaseDataObject)this[Count - 1]).VNum;
        }
    }
}