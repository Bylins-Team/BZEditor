using System;
using System.Diagnostics;

namespace SystemFrameworks
{
	/// <summary>
	/// Ошибка Есиап.
	/// </summary>
	public class TEsiapException : System.ApplicationException
	{
		// Тип ошибки есиап.(Если заданн тип Error, тогда сообщение об ошибке будет записанно в БД.)
		private EventLogEntryType ErrorType;
		private Exception initException;

		public TEsiapException(string in_message):base(in_message)
		{
			ErrorType =EventLogEntryType.Information;    
			//	isWrap = false;
			initException = null;
		}

		public TEsiapException(string in_message,EventLogEntryType ErType):base(in_message)
		{
			ErrorType = ErType;
			//	isWrap = false;
			initException = null;
		}

		public TEsiapException(Exception inExcept,string in_message,EventLogEntryType ErType):base(in_message)
		{
			ErrorType = ErType;
			initException = inExcept;
			//	isWrap = true;
		}

		/// <summary>
		/// Возвращает тип исключительной ситуации
		/// </summary>
		/// <returns>тип исключительной ситуации</returns>
		public EventLogEntryType GetErrorType()
		{
			return ErrorType;
		}

		/// <summary>
		/// Указывает является ли данное исключение оберткой для системного исключения
		/// </summary>
		public bool isWrapped
		{
			get
			{
				if (initException!=null) return true;
				else return false;
			}
		}
		/// <summary>
		/// Возвращает информацию первоначального исключения
		/// </summary>
		/// <returns></returns>
		public string GetWrappInfo()
		{
			return " Вложенная ошибка\n\n Message="+initException.Message+" Stack = "+initException.StackTrace+" Source = "+initException.Source;
		}
	}
}
