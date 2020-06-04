namespace DataUtils
{
    using System.Collections.Generic;

    public class ZonesComplexGenerator
    {
        //Гнерировать map (координаты расположения комнат на основе эскиза)
        //Генерировать зоны на основе эскиза (с переходами)
        //генерировать межзонные переходы

        public List<ZoneDataManager> Generate(GlobalSketch sketch)
        {
            List<ZoneDataManager> zones = new List<ZoneDataManager>(sketch.ZonesNumbers.Count);
            foreach(int zoneNum in sketch.ZonesNumbers)
            {
               zones.Add(GenerateZone(zoneNum, sketch));
            }
            LinkZones(zones, sketch);
            //Тут надо переводить в координаты зон (от 0:0:0) либо переводить по завершении генерации, брать первю клетку зоны за центр, получать отклонения от 000 и применять ко всем комнатам зоны
            CorectCoordinates(zones);
            return zones;
        }

        private static void CorectCoordinates(IEnumerable<ZoneDataManager> zones)
        {
            foreach (ZoneDataManager zone in zones)
            {
                Point3D loc = ((Room) zone.Rooms[0]).Location;
                foreach (Room r in zone.Rooms)
                {
                    r.X -= loc.X;
                    r.Y -= loc.Y;
                    r.Z -= loc.Z;
                }
            }

        }

        private static ZoneDataManager GenerateZone(int zoneNum, GlobalSketch sketch)
        {
            ZoneDataManager zone = new ZoneDataManager(zoneNum.ToString(), StaticData.CurrentEncoding)
                                        {
                                            Zone =
                                                {
                                                    Number = zoneNum,
                                                    Name = sketch.ZonesNames[zoneNum],
                                                    Comment = "Из комплекса зон\"" + sketch.Name + "\""
                                                }
                                        };
            GenerateWld(zoneNum, zone, sketch);
            return zone;
        }

        /// <summary>
        /// Генерирует расположение комнат и переходов между ними
        /// </summary>
        private static void GenerateWld(int zoneNum, ZoneDataManager zone, GlobalSketch sketch)
        {
            foreach (GlobalSketchRoom sr in sketch)
                if (sr.ZoneNum == zoneNum)
                {
                    Room zr = zone.Rooms.AddRoom(zoneNum);
                    zr.Location = sr.Location.Copy();
                    zr.PlacedOnMap = true;
                    sr.WldRoomVNum = zr.VNum;//Возможно не пригодится если генерить выходы без предварительного конфигурирования их на эскизе
                }
            //WldGenerateExits(zone.Rooms);
        }

        /// <summary>
        /// Генерирует все переходы
        /// </summary>
        private static void LinkZones(IEnumerable<ZoneDataManager> zones, GlobalSketch sketch)
        {
            foreach (GlobalSketchRoom sR in sketch)
            {
                LinkRoom(sR, zones);
            }
        }

        private static void LinkRoom(GlobalSketchRoom sR, IEnumerable<ZoneDataManager> zones)
        {
            Room r = GetRoom(zones, sR.X, sR.Y, sR.Z);
            Room rN = GetRoom(zones, sR.X, sR.Y - 1, sR.Z);
            if (rN != null)
                ConfigureExits(r.ExitNorth, r.VNum, rN.ExitSouth, rN.VNum);
            Room rE = GetRoom(zones, r.X + 1, r.Y, r.Z);
            if (rE != null)
                ConfigureExits(r.ExitEast, r.VNum, rE.ExitWest, rE.VNum);
            Room rS = GetRoom(zones, r.X, r.Y + 1, r.Z);
            if (rS != null)
                ConfigureExits(r.ExitSouth, r.VNum, rS.ExitNorth, rS.VNum);
            Room rW = GetRoom(zones, r.X - 1, r.Y, r.Z);
            if (rW != null)
                ConfigureExits(r.ExitWest, r.VNum, rW.ExitEast, rW.VNum);
            Room rU = GetRoom(zones, r.X, r.Y, r.Z + 1);
            if (rU != null)
                ConfigureExits(r.ExitUp, r.VNum, rU.ExitDown, rU.VNum);
            Room rD = GetRoom(zones, r.X, r.Y, r.Z - 1);
            if (rD != null)
                ConfigureExits(r.ExitDown, r.VNum, rD.ExitUp, rD.VNum);
        }

        private static Room GetRoom(IEnumerable<ZoneDataManager> zones, int x, int y, int z)
        {
            foreach (ZoneDataManager zone in zones)
            {
                Room r = zone.Rooms[x, y, z];
                if (r != null)
                    return r;
            }
            return null;
        }

        /// <summary>
        /// Устанавливает переходы
        /// </summary>
        private static void ConfigureExits(Exit exitFrom, int vNumFrom, Exit exitTo, int vNumTo)
        {
            //Можно добавить типы переходов
            if (exitFrom.RoomVNum == -1)
            {
                exitFrom.RoomVNum = vNumTo;
                exitFrom.ExitFlag = 0;                
            }
            if (exitTo.RoomVNum == -1)
            {
                exitTo.RoomVNum = vNumFrom;
                exitTo.ExitFlag = 0;
            }
        }
    }
}
