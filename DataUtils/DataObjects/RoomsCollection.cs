using System;

namespace DataUtils
{
    public class RoomsCollection : BaseDataArrayList
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
        /// Возвращает ссылку на комнату
        /// </summary>
        public new Room this[int index] => (Room) base[index];

        /// <summary>
        /// Возвращает ссылку на комнату c указанным vNum
        /// </summary>
        public Room this[int vNum, int tmp] => GetRoom(vNum);

        /// <summary>
        /// Возвращает ссылку на комнату (РАЗМЕЩЁННУЮ НА КАРТЕ) по указанным координатам
        /// </summary>
        public Room this[int x, int y, int z] => GetRoom(x, y, z);

        private Room GetRoom(int vNum)
        {
            foreach (Room room in this)
            {
                if (room.VNum == vNum)
                    return room;
            }
            return null;
        }

        private Room GetRoom(int x, int y, int z)
        {
            foreach (Room room in this)
            {
                if (room.X == x && room.Y == y && room.Z == z && room.PlacedOnMap)
                    return room;
            }
            return null;
        }

        /// <summary>
        /// Создает заданное количество новых комнат
        /// </summary>
        /// <param name="count">Требуемое количество комнат</param>
        /// <param name="zoneNum"></param>
        /// <param name="type">Тип создаваемых комнат (-1 не задано)</param>
        /// <returns>Количество созданных комнат</returns>
        public int AddRooms(int count, int zoneNum, int type)
        {
            int firstId = -1;
            for (int i = 0; i < count; i++)
            {
                int vnum = GetFirstFreeVNum(zoneNum);
                if (vnum < 0) break;
                if (firstId == -1)
                    firstId = vnum;
                string svnum = vnum.ToString();
                if (Convert.ToInt32(svnum.Substring(svnum.Length - 2, 2)) >= 98)
                    break;

                var room = new Room(vnum)
                               {
                                   ZoneNum = zoneNum,
                                   Name = "Новая комната " + vnum
                               };
                if (type != -1)
                    room.SectorType = type;
                Add(room);
            }
            Sort(new BaseDataObjectComparer());
            return firstId;
        }

        public Room AddDefRoom(int zoneNum, int type)
        {
            Room room = AddRoom(zoneNum);
            if (room != null)
            {
                room.Name = "Новая комната " + room.VNum;
                room.ZoneNum = zoneNum;
                if (type != -1)
                    room.SectorType = type;
            }
            Sort(new BaseDataObjectComparer());
            return room;
        }

        public Room AddRoom(int zoneNum)
        {
            int vnum = GetFirstFreeVNum(zoneNum);
            if (vnum < 0) return null;

            string svnum = vnum.ToString();
            if (Convert.ToInt32(svnum.Substring(svnum.Length - 2, 2)) > 98)
                return null;

            var room = new Room(vnum);
            Add(room);
            Sort(new BaseDataObjectComparer());
            return room;
        }

        /// <summary>
        /// Получение коллекции расставленных или нерасставленных на карте комнат
        /// </summary>
        /// <param name="placed"></param>
        /// <returns></returns>
        public RoomsCollection GetByState(bool placed)
        {
            var res = new RoomsCollection();
            foreach (Room room in this)
                if (room.PlacedOnMap == placed)
                    res.Add(room);
            return res;
        }

        /// <summary>
        /// Получение количества расставленных или нерасставленных на карте комнат
        /// </summary>
        /// <param name="placed"></param>
        /// <returns></returns>
        public int CountByState(bool placed)
        {
            int res = 0;
            foreach (Room room in this)
                if (room.PlacedOnMap == placed)
                    res++;
            return res;
        }

        /// <summary>
        /// Определяет наличие комнаты в заданной точке
        /// </summary>
        /// <param name="point3D"></param>
        /// <returns></returns>
        public bool Exists(Point3D point3D)
        {
            return Exists(point3D.X, point3D.Y, point3D.Z);
        }

        /// <summary>
        /// Определяет наличие комнаты по указанным координатам
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public bool Exists(int x, int y, int z)
        {
            foreach (Room room in this)
            {
                if (room.X == x && room.Y == y && room.Z == z)
                    return true;
            }
            return false;
        }

        public Point3D GetPointOfCenterRoomPlacedOnMap()
        {
            Room trgRoom = null;
            foreach (Room room in this)
                if (room.PlacedOnMap)
                    if (trgRoom == null || (Math.Abs(room.X)+Math.Abs(room.Y)+Math.Abs(room.Z) < Math.Abs(trgRoom.X)+Math.Abs(trgRoom.Y)+Math.Abs(trgRoom.Z)))
                        trgRoom = room;
            return (trgRoom != null)?trgRoom.Location:new Point3D(0,0,0);
        }
    }
}