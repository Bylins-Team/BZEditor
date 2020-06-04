using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SystemFrameworks
{
    /// <summary>
    /// Обрабатывает исключения и производит необходимые записи в лог.
    /// </summary>
    /// 
    public class BZedExceptionCatcher
    {
        #region Declaration

        private Exception CurrentException;
        private EventLogEntryType ExceptionType;
        private readonly bool FIsConnectionLostError = false;
        public string FullMessage;
        public MessageBoxIcon icon;
        public string LogMessage;
        public int OracleCode = 0;
        public string UserMessage;
        public string WrappedMessage;

        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        public BZedExceptionCatcher(Exception InExeption)
        {
            CurrentException = InExeption;
            DecodeException();
        }

        public BZedExceptionCatcher()
        {
        }

        /// <summary>
        /// Определяет это ошибка потери связи
        /// </summary>
        public bool IsConnectionLostError
        {
            get { return FIsConnectionLostError; }
        }

        public string GetFullExceptionInfo()
        {
            return FullMessage;
        }

        public string GetExceptionSource()
        {
            return CurrentException.Source;
        }

        public string GetExceptionStack()
        {
            return CurrentException.StackTrace;
        }
		
        public string GetUserExceptionInfo()
        {
            return UserMessage;
        }

        public MessageBoxIcon GetExceptionIcon()
        {
            return icon;
        }

        /// <summary>
        /// Расшифровывает тип исключения.
        /// </summary>
        public void DecodeException()
        {
            FullMessage = " Сообщение=" + CurrentException.Message + " Стэк=" + CurrentException.StackTrace + " Источник=" +
                          CurrentException.Source;
            UserMessage = CurrentException.Message;
            LogMessage = FullMessage;

            // если есть вложенная ошибка
            if (CurrentException.InnerException != null)
            {
                WrappedMessage = CurrentException.InnerException.Message;
                LogMessage = FullMessage + "\n" + "Вложенное сообщение: " + WrappedMessage;
            }

            //если ошибка сгенерирована в коде Редактора
            if (IsNoLogingException())
            {
                BZedException e = (BZedException)CurrentException;
                ExceptionType = e.GetErrorType();
            }
            else
            {
                ExceptionType = EventLogEntryType.Error;
                UserMessage = CurrentException.Message;
            }
            icon = GetIconByEventLogEntryType(ExceptionType);
        }

        /// <summary>
        /// Определяет тип иконки по типу ошибки есиап
        /// </summary>
        private static MessageBoxIcon GetIconByEventLogEntryType(EventLogEntryType inExceptionType)
        {
            MessageBoxIcon res = MessageBoxIcon.Exclamation;
            switch (inExceptionType)
            {
                case EventLogEntryType.Information:
                    res = MessageBoxIcon.Information;
                    break;
                case EventLogEntryType.Warning:
                    res = MessageBoxIcon.Warning;
                    break;
                case EventLogEntryType.Error:
                    res = MessageBoxIcon.Error;
                    break;
            }
            return res;
        }

        /// <summary>
        /// Определяет ошибку Oracle
        /// </summary>
        public bool DecodeOracleError()
        {
            String s = CurrentException.Message;

            int oraPos = s.IndexOf("ORA-");

            if (oraPos != -1)
            {
                String numStr = s.Substring(oraPos + 4, s.IndexOf(":", oraPos) - oraPos - 4);
                int errNum = Convert.ToInt32(numStr);
                OracleCode = errNum;
                if (errNum >= 00000 && errNum <= 99999)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Проверяет, является ли ошибка записываемой в лог
        /// </summary>
        public bool IsNoLogingException()
        {
            if (CurrentException.GetType().ToString() == "SystemFrameworks.BZedException")
                return true;
            else
                return false;
        }

        public void WriteToErrorLog(int writeToLog)
        {
            if (!IsNoLogingException())
            {
                FileInfo ef = new FileInfo("Errors.log");
                string DirectoryName = ef.Directory.ToString();
                string FileName = ef.Name;

                string FullPath = DirectoryName + "\\" + FileName;

                StreamWriter sw = new StreamWriter(FullPath, true, Encoding.Default);
                sw.WriteLine(DateTime.Now + " " + LogMessage);
                sw.Close();
            }

            else
                return;
        }

        public void AppThreadException(object source, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
        }
    }
}