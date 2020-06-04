namespace DataUtils
{
    public class SingleMapGenerator
    {
        //Использовать вместо встроенного в компонент карты примитивного генератора карты
        //Определять границы размещенного куска, совмещать один из переходов соответственно по X, Y, или Z и генерить другой кусок
        //Если следующий кусок зоны не имеет общих выходов с предыдущими, то генерить его отдельно, потом относительно размеров уже сегнеренного куска смещать его на свободное место

        /// <summary>
        /// Расставляет комнаты на карте
        /// </summary>
        /// <returns>Количество комнат оставшихся нерасставленными</returns>
        public void GenerateMap(RoomsCollection roomsCollection, int centerRoomVNum)
        {
            if (roomsCollection.Count == 0) return;
            Room startRoom = roomsCollection[centerRoomVNum, 0];
            if (startRoom == null) return;
            foreach (Room room in roomsCollection)
                room.PlacedOnMap = false;
            startRoom.Location = new Point3D(0, 0, 0);
            SetNearestRoomsLocation(roomsCollection, startRoom);
            RoomsCollection freeRooms = GetRooms(roomsCollection, false);
            while (freeRooms.Count != 0)
            {
                AddRoomsOnTheMap(roomsCollection, freeRooms);
                freeRooms = GetRooms(roomsCollection, false);
            }
        }

        private static void AddRoomsOnTheMap(RoomsCollection roomsCollection, RoomsCollection freeRooms)
        {
            Bounds zoneArea = GetBounds(GetRooms(roomsCollection, true));
            freeRooms[0].Location = new Point3D(0, 0, 0);
            SetNearestRoomsLocation(roomsCollection, freeRooms[0]);
            RoomsCollection placedRooms = GetRooms(freeRooms, true);
            Bounds placedArea = GetBounds(GetRooms(placedRooms, true));
            ShiftOverlay(zoneArea, placedArea, placedRooms);
        }

        /// <summary>
        /// Смещает placedArea таким образом, чтобы небыло наложения и при этом новые группы комнат располагались вогруг йцентра более или менее равномерно
        /// </summary>
        /// <param name="zoneArea"></param>
        /// <param name="placedArea"></param>
        /// <param name="placedRooms"></param>
        private static void ShiftOverlay(Bounds zoneArea, Bounds placedArea, RoomsCollection placedRooms)
        {
            int offsetX = 0;
            int offsetY = 0;
            int dX = zoneArea.RigthBottomX + zoneArea.LeftTopX;
            int dY = zoneArea.RigthBottomY + zoneArea.LeftTopY;
            int dXp = placedArea.RigthBottomX + placedArea.LeftTopX;
            int dYp = placedArea.RigthBottomY + placedArea.LeftTopY;

            if (dX <= dY) //вытянута в высоту или квадрат (размещать с горизонтальным смещением)
                if (dX <= 0) //Смещена влево или по центру (размещать со смещением вправо)
                    offsetX = -dX + dXp + 1;
                else //Смещена вправо (размещать со смещением влево)
                    offsetX = -dX - dXp - 1;
            else //Вытянута в ширину (размещать с вертикальным смещением)
                if (dY <= 0) //Смещена вверх или по центру (размещать со смещением вниз)
                    offsetY = -dY + dYp + 1;
                else //Смещена вниз (размещать со смещением вверх)
                    offsetY = -dY - dYp - 1;

            foreach (Room room in placedRooms)
            {
                room.X += offsetX;
                room.Y += offsetY;
            }
        }

        /// <summary>
        /// Расставляет коодинаты примыкающим комнатам
        /// </summary>
        private static void SetNearestRoomsLocation(RoomsCollection roomsCollection, Room room)
        {
            if (room.PlacedOnMap)
                return;
            room.PlacedOnMap = true;
            Room tmpRoom = roomsCollection[room.ExitNorth.RoomVNum, 0];
            if (tmpRoom != null && !tmpRoom.PlacedOnMap)
            {
                    tmpRoom.Location = new Point3D(room.Location.X, room.Location.Y - 1, room.Location.Z);
                    SetNearestRoomsLocation(roomsCollection, tmpRoom);
            }
            tmpRoom = roomsCollection[room.ExitSouth.RoomVNum, 0];
            if (tmpRoom != null && !tmpRoom.PlacedOnMap)
            {
                    tmpRoom.Location = new Point3D(room.Location.X, room.Location.Y + 1, room.Location.Z);
                    SetNearestRoomsLocation(roomsCollection, tmpRoom);
            }
            tmpRoom = roomsCollection[room.ExitEast.RoomVNum, 0];
            if (tmpRoom != null && !tmpRoom.PlacedOnMap)
            {
                    tmpRoom.Location = new Point3D(room.Location.X + 1, room.Location.Y, room.Location.Z);
                    SetNearestRoomsLocation(roomsCollection, tmpRoom);
            }
            tmpRoom = roomsCollection[room.ExitWest.RoomVNum, 0];
            if (tmpRoom != null && !tmpRoom.PlacedOnMap)
            {
                    tmpRoom.Location = new Point3D(room.Location.X - 1, room.Location.Y, room.Location.Z);
                    SetNearestRoomsLocation(roomsCollection, tmpRoom);
            }
            tmpRoom = roomsCollection[room.ExitUp.RoomVNum, 0];
            if (tmpRoom != null && !tmpRoom.PlacedOnMap)
            {
                    tmpRoom.Location = new Point3D(room.Location.X, room.Location.Y, room.Location.Z + 1);
                    SetNearestRoomsLocation(roomsCollection, tmpRoom);
            }
            tmpRoom = roomsCollection[room.ExitDown.RoomVNum, 0];
            if (tmpRoom != null && !tmpRoom.PlacedOnMap)
            {
                    tmpRoom.Location = new Point3D(room.Location.X, room.Location.Y, room.Location.Z - 1);
                    SetNearestRoomsLocation(roomsCollection, tmpRoom);
            }
        }

        private static RoomsCollection GetRooms(RoomsCollection roomsCollection, bool placedOnMap)
        {
            //Найти нерасставленные, сгенерить в отдельной коллекции координаты, сдвинуть на карте чтоб не было наложения
            RoomsCollection res = new RoomsCollection();
            foreach (Room room in roomsCollection)
                if (room.PlacedOnMap == placedOnMap)
                    res.Add(room);
            return res;
        }

        private static Bounds GetBounds(RoomsCollection roomsCollection)
        {
            Bounds res = new Bounds();
            foreach (Room room in roomsCollection)
            {
                if (room.X < res.LeftTopX)
                    res.LeftTopX = room.X;
                if (room.Y < res.LeftTopY)
                    res.LeftTopY = room.Y;
                if (room.X > res.RigthBottomX)
                    res.RigthBottomX = room.X;
                if (room.Y > res.RigthBottomY)
                    res.RigthBottomY = room.Y;
            }
            return res;
        }
    }
}
