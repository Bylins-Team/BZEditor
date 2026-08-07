using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace DataUtils
{
    public class BaseFileManager
    {
        #region Delegates & Events

        public delegate void ExceptionEvent(string message, Exception exception, EventLogEntryType type);

        public event ExceptionEvent ExceptionThrowed;

        #endregion

        public virtual void FireExceptionEvent(string message, Exception innerException, EventLogEntryType type)
        {
            ExceptionThrowed?.Invoke(message, innerException, type);
        }
    }

    internal class BaseDataObjectComparer : IComparer
    {
        #region Implementation of IComparer

        public int Compare(object x, object y)
        {
            return ((BaseDataObject)x).VNum - ((BaseDataObject)y).VNum;
        }

        #endregion
    }

    internal sealed class GenericDataObjectComparer<T> : IComparer<T> where T : BaseDataObject
    {
        #region Implementation of IComparer

        public int Compare(T x, T y)
        {
            return x.VNum - y.VNum;
        }

        #endregion
    }
}