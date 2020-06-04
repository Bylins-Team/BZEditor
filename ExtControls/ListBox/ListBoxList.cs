// ////////////////////////////////////////////////////////////////////////////
//
//  $RCSfile: ListBoxList.cs,v $
//
//  $Revision: 1.2 $
//
//  Last change:
//    $Author: Robert $
//    $Date: 2004/07/28 11:03:24 $
//
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
//
//  Original Code from Christian Tratz (via www.codeproject.com).
//  Changed by R. Lelieveld, SimVA GmbH.
//
// ////////////////////////////////////////////////////////////////////////////
using System.Collections;

namespace ExtControls
{
    /// <summary>
    /// EventHanlder for adding items.
    /// </summary>
    public delegate void AddedEventHandler();

    /// <summary>
    /// EventHandler for inserting items.
    /// </summary>
    public delegate void InsertEventHandler(int index);

    /// <summary>
    /// ArrayList with OnItemInserted event.
    /// </summary>
    public class ListBoxList
    {
        /// <summary>
        /// Internal messages list.
        /// </summary>
        private readonly ArrayList alMessages;

        /// <summary>
        /// Internal information about the messages.
        /// </summary>
        private readonly ArrayList alMessagesInfo;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ListBoxList()
        {
            alMessages = new ArrayList();
            alMessagesInfo = new ArrayList();
        }

        /// <summary>
        /// Number of items in the list.
        /// </summary>
        /// <returns></returns>
        public int Count
        {
            get { return alMessages.Count; }
        }

        /// <summary>
        /// Item at index.
        /// </summary>
        public ParseMessageEventArgs this[int index]
        {
            get { return (ParseMessageEventArgs) alMessages[index]; }
        }

        /// <summary>
        /// Item has been added.
        /// </summary>
        public event AddedEventHandler OnItemAdded;

        /// <summary>
        /// Item has been inserted.
        /// </summary>
        public event InsertEventHandler OnItemInserted;

        /// <summary>
        /// Add an item.
        /// </summary>
        /// <param name="pmea"></param>
        /// <returns></returns>
        public int Add(ParseMessageEventArgs pmea)
        {
            int index = alMessages.Add(pmea);
            alMessagesInfo.Add(new ItemInfo(pmea));
            OnAdd();
            return index;
        }

        /// <summary>
        /// Clears the list.
        /// </summary>
        public void Clear()
        {
            alMessages.Clear();
            alMessagesInfo.Clear();
        }

        /// <summary>
        /// Index of the object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int IndexOf(object obj)
        {
            return alMessages.IndexOf(obj);
        }

        /// <summary>
        /// Index of the item.
        /// </summary>
        /// <param name="pmea"></param>
        /// <returns></returns>
        public int IndexOf(ParseMessageEventArgs pmea)
        {
            return alMessages.IndexOf(pmea);
        }

        /// <summary>
        /// Returns more information (ItemInfo object) about an item.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ItemInfo Info(int index)
        {
            return (ItemInfo) alMessagesInfo[index];
        }

        /// <summary>
        /// Insert an item.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pmea"></param>
        public void Insert(int index, ParseMessageEventArgs pmea)
        {
            alMessages.Insert(index, pmea);
            alMessagesInfo.Insert(index, new ItemInfo(pmea));
            OnInsert(index);
        }

        /// <summary>
        /// Raises the 'OnItemAdded' event.
        /// </summary>
        private void OnAdd()
        {
            OnItemAdded?.Invoke();
        }

        /// <summary>
        /// Raises the 'OnItemInserted' event.
        /// </summary>
        /// <param name="index"></param>
        private void OnInsert(int index)
        {
            OnItemInserted?.Invoke(index);
        }
    }
}