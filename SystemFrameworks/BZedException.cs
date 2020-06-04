using System;
using System.Diagnostics;

namespace SystemFrameworks
{
    public class BZedException : ApplicationException
    {
        private readonly EventLogEntryType errorType;
        private readonly Exception initException;

        public BZedException(string inMessage) : base(inMessage)
        {
            errorType = EventLogEntryType.Information;
            //	isWrap = false;
            initException = null;
        }

        public BZedException(string inMessage, EventLogEntryType erType) : base(inMessage)
        {
            errorType = erType;
            //	isWrap = false;
            initException = null;
        }

        public BZedException(Exception inExcept, string inMessage, EventLogEntryType erType) : base(inMessage)
        {
            errorType = erType;
            initException = inExcept;
            //	isWrap = true;
        }

        /// <summary>
        /// Указывает является ли данное исключение оберткой для системного исключения
        /// </summary>
        public bool IsWrapped
        {
            get
            {
                if (initException != null) return true;
                else return false;
            }
        }

        /// <summary>
        /// Возвращает тип исключительной ситуации
        /// </summary>
        /// <returns>тип исключительной ситуации</returns>
        public EventLogEntryType GetErrorType()
        {
            return errorType;
        }

        /// <summary>
        /// Возвращает информацию первоначального исключения
        /// </summary>
        /// <returns></returns>
        public string GetWrappInfo()
        {
            return $" Вложенная ошибка\n\n Message={initException.Message} Stack = {initException.StackTrace} Source = {initException.Source}";
        }
    }
}