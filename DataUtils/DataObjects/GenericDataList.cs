using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataUtils
{
    public class GenericDataList<T> : IReadOnlyCollection<T> where T : BaseDataObject
    {
        protected readonly List<T> _list;

        public GenericDataList()
        {
            _list = new List<T>();
        }

        #region Delegates

        public delegate void ChangeEvent(object sender);

        #endregion

        public event ChangeEvent Changed;
        public int Count => _list.Count;

        public virtual void FireChangeEvent(object sender)
        {
            if (Changed != null && StaticData.CanFireChangeEvent)
                Changed(this);
        }

        public void Add(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            value.Changed += FireChangeEvent;

            FireChangeEvent(this);
            _list.Add(value);
        }

        public void Remove(T value)
        {
            FireChangeEvent(this);
            _list.Remove(value);
        }

        public void Clear()
        {
            FireChangeEvent(this);
            _list.Clear();
        }

        public GenericDataList<T> Clone()
        {
            var result = new GenericDataList<T>();
            result._list.AddRange(_list);

            return result;
        }

        public int GetFirstFreeVNum(int zoneNum)
        {
            int res = -1;
            int cntr = zoneNum * 100;

            foreach (var vnum in _list.Select(element => element.VNum))
            {
                if (cntr - vnum < 0)
                {
                    res = cntr - 1;
                    break;
                }
                if (res < vnum)
                    res = vnum;
                cntr++;
            }

            return res < 0 ? zoneNum * 100 : res + 1;
        }

        public int GetLastVNum()
        {
            if (_list.Count == 0) return 0;

            var comparer = new GenericDataObjectComparer<T>();

            _list.Sort(comparer);

            return _list[_list.Count - 1].VNum;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
