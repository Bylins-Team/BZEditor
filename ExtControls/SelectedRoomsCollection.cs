using System;
using System.Collections;
using DataUtils;

namespace ExtControls
{
    public class SelectedRoomsCollection : ArrayList
    {
        /// <summary>
        /// Если есть комната то она будет удалена, если нет - добавлена
        /// </summary>
        public void SmartAddRemoveRoom(int NewRoomVNum)
        {
            foreach (int VNum in this)
            {
                if (VNum == NewRoomVNum)
                {
                    Remove(VNum);
                    return;
                }
            }
            Add(NewRoomVNum);
        }

        public bool RoomExist(Room Room)
        {
            foreach (int VNum in this)
            {
                if (VNum == Room.VNum)
                    return true;
            }
            return false;
        }

        public bool RoomExist(int roomVNum)
        {
            foreach (int VNum in this)
            {
                if (VNum == roomVNum)
                    return true;
            }
            return false;
        }

        public int RoomIndex(int roomVNum)
        {
            for (int i = 0; i < Count; i++)
            {
                if (Convert.ToInt32(this[i]) == roomVNum)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Добавляет только новые не удаляя если такая уже есть
        /// </summary>
        /// <param name="NewRoomVNum"></param>
        public void SmartAddRoom(int NewRoomVNum)
        {
            if (!RoomExist(NewRoomVNum))
                Add(NewRoomVNum);
        }

        public void AddRoom(int NewRoomVNum)
        {
            Add(NewRoomVNum);
        }

        /* public void SmartAddRoom(CRoom NewRoom)
         {
             foreach (CRoom room in this)
             {
                 if (room.Equals(NewRoom))
                 {
                     return;
                 }
             }
             this.Add(NewRoom);
         }*/
    }
}