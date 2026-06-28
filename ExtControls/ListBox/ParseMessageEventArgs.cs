using System;

namespace ExtControls
{
    /// <summary>
    /// 
    /// </summary>
    public class ParseMessageEventArgs : EventArgs
    {
        private ParseMessageType msgType = ParseMessageType.Отсутствует;
        public int VNum;
        public ActionType Action = ActionType.DoNothing;

        public ParseMessageEventArgs()
        {
        }

        public ParseMessageEventArgs(ParseMessageType msgType, string messageHeader, string messageText, int vNum, ActionType action)
        {
            LineHeader = messageHeader;
            MessageText = messageText;
            this.msgType = msgType;
            VNum = vNum;
            Action = action;
        }

        public ParseMessageEventArgs(ParseMessageType msgType, string lineHeader, string messageText, int vNum, ActionType action, string source)
            : this(msgType, lineHeader, messageText, vNum, action)
        {
            Source = source;
        }

        public string MessageText { get; set; }

        public string Source { get; set; }

        public string LineHeader { get; set; }

        public ParseMessageType MessageType
        {
            get { return msgType; }
            set { msgType = value; }
        }
    }

    public enum ParseMessageType
    {
        Отсутствует = -1,
        Информация = 0,
        Предупреждение = 1,
        Ошибка = 2
    } ;
}