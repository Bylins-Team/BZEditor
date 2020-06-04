using System;
using System.Collections;
using System.Diagnostics;
using System.Text;

namespace DataUtils
{
    public class ZoneDataManager
    {
        #region Delegates

        public delegate void ChangeEvent(string name, object changedClass, object sender);

        public delegate void ExceptionEvent(string message, Exception exception, EventLogEntryType type);

        public delegate void SaveEvent(string name);

        #endregion

        private readonly Encoding encoding = Encoding.Default;

        /// <summary>
        /// Название зоны (по номеру)
        /// </summary>
        private readonly string zoneName;

        /// <summary>
        /// Коллекция мобов зоны
        /// </summary>
        public MobsCollection Mobs;

        /// <summary>
        /// Коллекция предметов зоны
        /// </summary>
        public ObjsCollection Objects;

        /// <summary>
        /// Коллекция комнат зоны
        /// </summary>
        public RoomsCollection Rooms;

        /// <summary>
        /// Комнаты эскиза
        /// </summary>
        public SketchRoomsCollection SketchRooms;

        /// <summary>
        /// Коллекция триггеров зоны
        /// </summary>
        public TriggersCollection Triggers;

        /// <summary>
        /// Объект, хранящий параметры зоны
        /// </summary>
        public Zone Zone;

        /// <summary>
        /// Основной класс работы с данными зон и файлами в которых они хранятся
        /// </summary>
        public ZoneDataManager(string zoneName, Encoding currentEncoding)
        {
            encoding = currentEncoding;
            this.zoneName = zoneName;
            Recreate();
        }

        public event ChangeEvent Changed;

        public void FireChangeEvent(object changedClass, object sender)
        {
            Zone.Modifyed = true;
            Changed?.Invoke(zoneName, changedClass, sender);
            //StaticData.CanFireChangeEvent = false;
        }

        public event SaveEvent Saved;

        public void FireSaveEvent()
        {
            Zone.Modifyed = false;
            Saved?.Invoke(zoneName);
        }

        public event ExceptionEvent ExceptionThrowed;

        public virtual void FireZoneLoadingExceptionEvent(string message, Exception exception, EventLogEntryType type)
        {
            ExceptionThrowed?.Invoke("Зона не может быть загружена. Ошибка парсинга:\n" + message, exception, type);
        }

        private void Recreate()
        {
            Zone = new Zone();
            Rooms = new RoomsCollection();
            Mobs = new MobsCollection();
            Objects = new ObjsCollection();
            Triggers = new TriggersCollection();
            SketchRooms = new SketchRoomsCollection();
            Zone.Changed += FireChangeEvent;
            Rooms.Changed += FireChangeEvent;
            Mobs.Changed += FireChangeEvent;
            Objects.Changed += FireChangeEvent;
            Triggers.Changed += FireChangeEvent;
            SketchRooms.Changed += FireChangeEvent;
            //SaveData();
        }

        #region [!] LoadData [!]

        /// <summary>
        /// Загрузка всех файлов зоны
        /// </summary>
        public bool LoadData()
        {
            StaticData.CanFireChangeEvent = false;
            Recreate();
            var tfm = new TriggersFileManager();
            tfm.ExceptionThrowed += FireZoneLoadingExceptionEvent;
            if (!tfm.Load(Triggers, zoneName, encoding))
                return false;
            var ofm = new ObjectsFileManager();
            ofm.ExceptionThrowed += FireZoneLoadingExceptionEvent;
            if (!ofm.Load(Objects, zoneName, encoding))
                return false;
            var wfm = new WorldFileManager();
            wfm.ExceptionThrowed += FireZoneLoadingExceptionEvent;
            if (!wfm.Load(Rooms, zoneName, encoding))
                return false;
            var sktfm = new SketchFileManager();
            sktfm.ExceptionThrowed += FireZoneLoadingExceptionEvent;
            if (!sktfm.Load(SketchRooms, zoneName, encoding))
                return false;
            var mfm = new MobsFileManager();
            mfm.ExceptionThrowed += FireZoneLoadingExceptionEvent;
            if (!mfm.Load(Mobs, zoneName, encoding))
                return false;
            var zfm = new ZoneFileManager();
            zfm.ExceptionThrowed += FireZoneLoadingExceptionEvent;
            if (!zfm.Load(Zone, Mobs, Rooms, zoneName, encoding)) 
                return false;
            StaticData.CurrentEncoding = Encoding.GetEncoding("koi8-r");
            CheckMapForZLimit();
            StaticData.CanFireChangeEvent = true;
            return true;
        }

        public void CheckMapForZLimit()
        {
            foreach (Room r in Rooms)
            {
                if (r.Z < -3 || r.Z > 3) r.PlacedOnMap = false;
            }
        }

        #endregion

        #region [!] SaveData [!]

        /// <summary>
        /// Сохранение всех файлов зоны
        /// </summary>
        public void SaveData()
        {            
            SaveMobs();
            SaveObjects();
            SaveTriggers();
            SaveRooms();
            SaveZone();
            FireSaveEvent();
        }

        /// <summary>
        /// Сохранение мобов
        /// </summary>
        public void SaveMobs()
        {
            var mfm = new MobsFileManager();
            //mfm.ExceptionThrowed += FireExceptionEvent;
            mfm.Save(Mobs, Zone.Number.ToString());
        }

        /// <summary>
        /// Сохранение объектов
        /// </summary>
        public void SaveObjects()
        {
            var ofm = new ObjectsFileManager();
            //ofm.ExceptionThrowed += FireExceptionEvent;
            ofm.Save(Objects, Zone.Number.ToString());
        }

        /// <summary>
        /// Сохранение триггеров
        /// </summary>
        public void SaveTriggers()
        {
            var tfm = new TriggersFileManager();
            //tfm.ExceptionThrowed += FireExceptionEvent;
            tfm.Save(Triggers, Zone.Number.ToString());
        }

        /// <summary>
        /// Сохранение комнат и эскиза
        /// </summary>
        public void SaveRooms()
        {
            var wfm = new WorldFileManager();
            //wfm.ExceptionThrowed += FireExceptionEvent;
            wfm.Save(Rooms, Zone.Number.ToString());
            var sktfm = new SketchFileManager();
            //sktfm.ExceptionThrowed += FireExceptionEvent;
            sktfm.Save(SketchRooms, Zone.Number.ToString());
        }

        /// <summary>
        /// Сохранение данных зоны
        /// </summary>
        public void SaveZone()
        {
            var zfm = new ZoneFileManager();
            //zfm.ExceptionThrowed += FireExceptionEvent;
            zfm.Save(Zone, Objects, Mobs, Rooms);
        }


        #endregion

        #region [!] ChangeZoneNumber [!]

        /// <summary>
        /// Изменение номера текущей зоны
        /// </summary>
        /// <param name="newNumber">Новый номер зоны</param>
        public void ChangeZoneNumber(int newNumber)
        {
            int oldNum = Zone.Number*100;
            int newNum = newNumber*100;
            int delta = newNum - oldNum;
            foreach (Room r in Rooms)
            {
                //Выходы
                r.ExitDown.Key = (r.ExitDown.Key != -1 && Math.Abs(r.ExitDown.Key - oldNum) < 100)
                                     ? r.ExitDown.Key + delta
                                     : r.ExitDown.Key;
                r.ExitDown.RoomVNum = (r.ExitDown.RoomVNum != -1 && Math.Abs(r.ExitDown.RoomVNum - oldNum) < 100)
                                          ? r.ExitDown.RoomVNum + delta
                                          : r.ExitDown.RoomVNum;
                r.ExitEast.Key = (r.ExitEast.Key != -1 && Math.Abs(r.ExitEast.Key - oldNum) < 100)
                                     ? r.ExitEast.Key + delta
                                     : r.ExitEast.Key;
                r.ExitEast.RoomVNum = (r.ExitEast.RoomVNum != -1 && Math.Abs(r.ExitEast.RoomVNum - oldNum) < 100)
                                          ? r.ExitEast.RoomVNum + delta
                                          : r.ExitEast.RoomVNum;
                r.ExitNorth.Key = (r.ExitNorth.Key != -1 && Math.Abs(r.ExitNorth.Key - oldNum) < 100)
                                      ? r.ExitNorth.Key + delta
                                      : r.ExitNorth.Key;
                r.ExitNorth.RoomVNum = (r.ExitNorth.RoomVNum != -1 && Math.Abs(r.ExitNorth.RoomVNum - oldNum) < 100)
                                           ? r.ExitNorth.RoomVNum + delta
                                           : r.ExitNorth.RoomVNum;
                r.ExitSouth.Key = (r.ExitSouth.Key != -1 && Math.Abs(r.ExitSouth.Key - oldNum) < 100)
                                      ? r.ExitSouth.Key + delta
                                      : r.ExitSouth.Key;
                r.ExitSouth.RoomVNum = (r.ExitSouth.RoomVNum != -1 && Math.Abs(r.ExitSouth.RoomVNum - oldNum) < 100)
                                           ? r.ExitSouth.RoomVNum + delta
                                           : r.ExitSouth.RoomVNum;
                r.ExitUp.Key = (r.ExitUp.Key != -1 && Math.Abs(r.ExitUp.Key - oldNum) < 100)
                                   ? r.ExitUp.Key + delta
                                   : r.ExitUp.Key;
                r.ExitUp.RoomVNum = (r.ExitUp.RoomVNum != -1 && Math.Abs(r.ExitUp.RoomVNum - oldNum) < 100)
                                        ? r.ExitUp.RoomVNum + delta
                                        : r.ExitUp.RoomVNum;
                r.ExitWest.Key = (r.ExitWest.Key != -1 && Math.Abs(r.ExitWest.Key - oldNum) < 100)
                                     ? r.ExitWest.Key + delta
                                     : r.ExitWest.Key;
                r.ExitWest.RoomVNum = (r.ExitWest.RoomVNum != -1 && Math.Abs(r.ExitWest.RoomVNum - oldNum) < 100)
                                          ? r.ExitWest.RoomVNum + delta
                                          : r.ExitWest.RoomVNum;
                //Загружаемые в комнату мобы
                foreach (OperatedMob lm in r.LoadingMobsCollection)
                {
                    lm.FollowsBy = (lm.FollowsBy != -1 && Math.Abs(lm.FollowsBy - oldNum) < 100)
                                       ? lm.FollowsBy + delta
                                       : lm.FollowsBy;
                    lm.VNum = (Math.Abs(lm.VNum - oldNum) < 100) ? lm.VNum + delta : lm.VNum;
                    foreach (MobObj mlo in lm.Items)
                        mlo.VNum = (Math.Abs(mlo.VNum - oldNum) < 100) ? mlo.VNum + delta : mlo.VNum;
                }
                //Загружаемые в комнату объекты
                foreach (OperatedObj lo in r.LoadingObjectsCollection)
                {
                    lo.VNum = (Math.Abs(lo.VNum - oldNum) < 100) ? lo.VNum + delta : lo.VNum;
                    foreach (OperatedObj olo in lo.ObjectsInObject)
                        olo.VNum = (Math.Abs(olo.VNum - oldNum) < 100) ? olo.VNum + delta : olo.VNum;
                }
                //Удаляемые из комнаты объекты
                foreach (OperatedObj lo in r.RemoovingObjects)
                    lo.VNum = (Math.Abs(lo.VNum - oldNum) < 100) ? lo.VNum + delta : lo.VNum;
                //Триггеры комнаты
                if (r.TriggersList.Count > 0)
                {
                    for (int i = 0; i < r.TriggersList.Count; i++)
                    {
                        r.TriggersList[i] = (Math.Abs(((int) (r.TriggersList[i])) - oldNum) < 100)
                                                ? ((int) (r.TriggersList[i])) + delta
                                                : ((int) (r.TriggersList[i]));
                    }
                }
                r.VNum += delta;
                r.ZoneNum = newNum;
            }
            foreach (Obj o in Objects)
            {
                //Триггеры объекта
                if (o.TriggersList.Count > 0)
                {
                    for (int i = 0; i < o.TriggersList.Count; i++)
                    {
                        o.TriggersList[i] = (Math.Abs(((int) (o.TriggersList[i])) - oldNum) < 100)
                                                ? ((int) (o.TriggersList[i])) + delta
                                                : ((int) (o.TriggersList[i]));
                    }
                }
                o.VNum += delta;
            }
            foreach (Mob m in Mobs)
            {
                //Путь моба
                if (m.Destination.Count > 0)
                {
                    for (int i = 0; i < m.Destination.Count; i++)
                    {
                        m.Destination[i] = (Math.Abs(((int) (m.Destination[i])) - oldNum) < 100)
                                               ? ((int) (m.Destination[i])) + delta
                                               : ((int) (m.Destination[i]));
                    }
                }
                //Асистеры
                if (m.Helpers.Count > 0)
                {
                    for (int i = 0; i < m.Helpers.Count; i++)
                    {
                        m.Helpers[i] = (Math.Abs(((int) (m.Helpers[i])) - oldNum) < 100)
                                           ? ((int) (m.Helpers[i])) + delta
                                           : ((int) (m.Helpers[i]));
                    }
                }
                //Триггеры моба
                if (m.TriggersList.Count > 0)
                {
                    for (int i = 0; i < m.TriggersList.Count; i++)
                    {
                        m.TriggersList[i] = (Math.Abs(((int) (m.TriggersList[i])) - oldNum) < 100)
                                                ? ((int) (m.TriggersList[i])) + delta
                                                : ((int) (m.TriggersList[i]));
                    }
                }
                m.VNum += delta;
            }
            foreach (Trigger t in Triggers)
            {
                //возможно производить замену и в теле триггера
                t.VNum += delta;
            }
            //Удаляемые мобы
            foreach (OperatedMob lm in Zone.MobsToRemove)
                lm.VNum = (Math.Abs(lm.VNum - oldNum) < 100) ? lm.VNum + delta : lm.VNum;
            Zone.Number = newNumber;
        }

        #endregion

        #region [!] Removing [!]

        /// <summary>
        /// Удаление комнаты
        /// </summary>
        /// <param name="vNum">Виртуальный номер удаляемой комнаты</param>
        public void RemoveRoom(int vNum)
        {
            //После этого надо перерисовывать комнаты
            var rooms = new ArrayList();
            foreach (Room r in Rooms)
            {
                if (r.ExitDown.RoomVNum == vNum)
                    r.ExitDown.RoomVNum = -1;
                if (r.ExitEast.RoomVNum == vNum)
                    r.ExitEast.RoomVNum = -1;
                if (r.ExitNorth.RoomVNum == vNum)
                    r.ExitNorth.RoomVNum = -1;
                if (r.ExitSouth.RoomVNum == vNum)
                    r.ExitSouth.RoomVNum = -1;
                if (r.ExitUp.RoomVNum == vNum)
                    r.ExitUp.RoomVNum = -1;
                if (r.ExitWest.RoomVNum == vNum)
                    r.ExitWest.RoomVNum = -1;
                if (r.VNum == vNum)
                    rooms.Add(r);
            }
            //путь моба
            foreach (Mob m in Mobs)
            {
                if (m.Destination.Contains(vNum))
                    m.Destination.Remove(vNum);
            }
            foreach (Room r in rooms)
                Rooms.Remove(r);
        }

        /// <summary>
        /// Удаление объекта
        /// </summary>
        /// <param name="vNum">Виртуальный номер удаляемого объекта</param>
        public void RemoveObject(int vNum)
        {
            var objects = new ArrayList();
            foreach (Obj o in Objects)
            {
                if (o.VNum == vNum)
                    objects.Add(o);
            }
            foreach (Obj o in objects)
                Objects.Remove(o);
            foreach (Room r in Rooms)
            {
                foreach (OperatedMob lm in r.LoadingMobsCollection)
                {
                    var otmp = new ArrayList();
                    foreach (MobObj mo in lm.Items)
                    {
                        if (mo.VNum == vNum)
                            otmp.Add(mo);
                    }
                    foreach (MobObj delobj in otmp)
                        lm.Items.Remove(delobj);
                }
                var lotmp = new ArrayList();
                foreach (OperatedObj lo in r.LoadingObjectsCollection)
                {
                    if (lo.VNum == vNum)
                        lotmp.Add(lo);
                    var otmp = new ArrayList();
                    foreach (OperatedObj loi in lo.ObjectsInObject)
                    {
                        if (loi.VNum == vNum)
                            otmp.Add(loi);
                    }
                    foreach (OperatedObj delobj in otmp)
                        lo.ObjectsInObject.Remove(delobj);
                }
                foreach (OperatedObj delobj in lotmp)
                    r.LoadingObjectsCollection.Remove(delobj);
                lotmp = new ArrayList();
                foreach (OperatedObj lo in r.RemoovingObjects)
                {
                    if (lo.VNum == vNum)
                        lotmp.Add(lo);
                }
                foreach (OperatedObj delobj in lotmp)
                    r.RemoovingObjects.Remove(delobj);
            }
        }

        /// <summary>
        /// Удаление моба
        /// </summary>
        /// <param name="vNum">Виртуальный номер удаляемого моба</param>
        public void RemoveMob(int vNum)
        {
            var delmobs = new ArrayList();
            foreach (Mob m in Mobs)
            {
                if (m.VNum == vNum)
                    delmobs.Add(m);
                if (m.Helpers.Contains(vNum))
                    m.Helpers.Remove(vNum);
            }
            foreach (Mob m in delmobs)
                Mobs.Remove(m);
            foreach (Room r in Rooms)
            {
                var lmtmp = new ArrayList();
                foreach (OperatedMob lm in r.LoadingMobsCollection)
                {
                    if (lm.VNum == vNum)
                        lmtmp.Add(lm);
                    if (lm.FollowsBy == vNum)
                        lm.FollowsBy = -1;
                }
                foreach (OperatedMob delmob in lmtmp)
                    r.LoadingMobsCollection.Remove(delmob);
            }
            var rmtmp = new ArrayList();
            foreach (OperatedMob lm in Zone.MobsToRemove)
            {
                if (lm.VNum == vNum)
                    rmtmp.Add(lm);
            }
            foreach (OperatedMob delmob in rmtmp)
                Zone.MobsToRemove.Remove(delmob);
        }

        /// <summary>
        /// Удаление триггера
        /// </summary>
        /// <param name="vNum">Виртуальный номер удаляемого триггера</param>
        public void RemoveTrigger(int vNum)
        {
            var deltriggers = new ArrayList();
            int trigType = -1;
            foreach (Trigger t in Triggers)
            {
                if (t.VNum != vNum) continue;
                deltriggers.Add(t);
                trigType = t.Class;
            }
            foreach (Trigger t in deltriggers)
                Triggers.Remove(t);
            switch (trigType)
            {
                case 2:
                    foreach (Room r in Rooms)
                    {
                        if (r.TriggersList.Contains(vNum))
                            r.TriggersList.Remove(vNum);
                    }
                    break;
                case 0:
                    foreach (Mob m in Mobs)
                    {
                        if (m.TriggersList.Contains(vNum))
                            m.TriggersList.Remove(vNum);
                    }
                    break;
                case 1:
                    foreach (Obj o in Objects)
                    {
                        if (o.TriggersList.Contains(vNum))
                            o.TriggersList.Remove(vNum);
                    }
                    break;
            }
        }

        #endregion
    }
}