using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using SystemFrameworks;

namespace DataUtils
{
    public class ZoneFileManager : BaseFileManager
    {
        private int filePos;
        private string lastString;

        private string ReadLine(TextReader sr)
        {
            lastString = sr.ReadLine();
            filePos++;
            return lastString;
        }

        /// <summary>
        /// Чтение данных из zon-файла
        /// </summary>
        /// <param name="zone">Объект в который будут загружены данные</param>
        /// <param name="mobsCollection">Коллекция мобов загружаемой зоны</param>
        /// <param name="roomsCollection">Коллекция комнат загружаемой зоны</param>
        /// <param name="zoneNumber">Номер загружаемой зоны</param>
        /// <param name="encoding">Кодировка</param>
        public bool Load(Zone zone, MobsCollection mobsCollection, RoomsCollection roomsCollection,
                         string zoneNumber, Encoding encoding)
        {
            filePos = 0;
            string additionalInfo = "отсутствует...";
            string filePath = Path.Combine(StaticData.WorldFolderPath + @"\ZON\", zoneNumber + ".zon");
            if (!File.Exists(filePath))
            {
                return true;
            }

            using (var sr = new StreamReader(filePath, encoding))
            {
                try
                {
                    string input = ReadLine(sr);
                    if (input == null)
                    {
                        return true; //если конец файла, прекращаем обработку файла
                    }

                    while (input.IndexOf("#", StringComparison.Ordinal) == -1) //Смещаемся на начало описания зоны
                    {
                        input = ReadLine(sr);
                        if (input == null)
                        {
                            return true; //если конец файла, то прекращаем искать начало след.объекта
                        }
                    }
                    zone.Number = StringUtils.ToIntFast(input.Replace("#", ""));
                    input = ReadLine(sr);
                    zone.Name = input.Replace("~", "");
                    additionalInfo = $"зона [{zone.Number}] {zone.Name}";
                    input = ReadLine(sr);

                    string[] parts;
                    var lastLoadedMob = new OperatedMob(-1);
                    var lastLoadedObject = new OperatedObj(-1);
                    bool exit = false;

                    while (!exit)
                    {
                        if (input.IndexOf('\t') > 0)
                        {
                            input = input.Remove(input.IndexOf('\t'));
                        }

                        switch (input[0])
                        {
                            case '^'://Комментарий
                                zone.Comment = input.Replace("~", "").Remove(0, 1);
                                break;
                            case '&'://Местоположение
                                zone.Location = input.Replace("~", "").Remove(0, 1);
                                break;
                            case '$'://Описание
                                zone.Description = input.Replace("~", "").Remove(0, 1);
                                break;
                            case '!'://Автор
                                zone.Author = input.Replace("~", "").Remove(0, 1);
                                break;
                            case '#': //Если решетка то новый формат зон с уровнем и типом
                                parts = input.Replace("#", "").Split(' ');
                                zone.Level = StringUtils.ToIntFast(parts[0]);
                                zone.Type = StringUtils.ToIntFast(parts[1]);
                                if (parts.Length == 3)//В новом формате зон признак того, что зона для группы
                                {
                                    zone.OptimalCharsInGroup = StringUtils.ToIntFast(parts[2]);
                                    if (zone.OptimalCharsInGroup == 0)
                                    {
                                        zone.OptimalCharsInGroup = 1;//0 до изменений означал зону не для группы
                                    }
                                }
                                input = ReadLine(sr);//Следующую строку только регуляркой ловить, потому тут так жестко
                                parts = input.Split(' ');
                                zone.LastRoomNumber = StringUtils.ToIntFast(parts[0]);
                                zone.RepopTimer = StringUtils.ToIntFast(parts[1]);
                                zone.RepopType = StringUtils.ToIntFast(parts[2]);
                                if (parts.Length > 3)
                                {
                                    if (parts[3] != "*" && parts[3].ToLower().IndexOf("test", StringComparison.Ordinal) < 0 &&
                                        parts[3].ToLower().IndexOf("locked", StringComparison.Ordinal) < 0)
                                    {
                                        zone.ResetIdle = StringUtils.ToIntFast(parts[3]);
                                    }

                                    zone.Test = input.ToLower().IndexOf("test", StringComparison.Ordinal) >= 0;
                                    zone.Locked = input.ToLower().IndexOf("locked", StringComparison.Ordinal) >= 0;
                                }
                                break;
                            case 'A'://Ресет зоны А
                                zone.ResetA.Add(StringUtils.ToIntFast(input.Remove(0, 2)));
                                break;
                            case 'B'://Ресет зоны B
                                zone.ResetB.Add(StringUtils.ToIntFast(input.Remove(0, 2)));
                                break;
                            case 'Q': //Мобы, удаляемые при перезапуске
                                parts = input.Split(' ');
                                zone.MobsToRemove.Add(StringUtils.ToIntFast(parts[2]), parts[1] == "1", -1);
                                break;
                            case 'M': //Мобы, заргужаемые в комнаты
                                parts = input.Split(' ');
                                lastLoadedMob = new OperatedMob(StringUtils.ToIntFast(parts[2]))
                                {
                                    ConditionFlag = (parts[1] == "1"),
                                    //MaxInWorld = StringUtils.ToIntFast(parts[3]),
                                    MaxInRoom = StringUtils.ToIntFast(parts[5])
                                };
                                var mobData = mobsCollection[lastLoadedMob.VNum, 0];
                                if (mobData != null)//непонятно почему максимум мобов в мире не вынесено в файл mob
                                                    //Кроме того это значение нигде не хранится и в бруске берется по максимуму в комнате :)
                                {
                                    var val = StringUtils.ToIntFast(parts[3]);
                                    if (val > mobData.MaxInWorld)
                                    {
                                        mobData.MaxInWorld = val;
                                    }
                                }

                                Room room = roomsCollection[StringUtils.ToIntFast(parts[4]), 0];
                                if (room == null)
                                {
                                    if (parts[4].EndsWith("99"))
                                    {
                                        //Виртуальная комната
                                        zone.MobsLoadedInVirtualRoom.Add(lastLoadedMob);
                                        break;
                                    }
                                    //кривой файл 
                                    additionalInfo += $"\nПопытка загрузить моба ({parts[2]}) в отсутствующую комнату с vnum ({parts[4]}).";
                                }
                                else
                                {
                                    room.LoadingMobsCollection.Add(lastLoadedMob);
                                }
                                break;
                            case 'O': //Объекты, заргужаемые в комнаты
                                parts = input.Split(' ');
                                lastLoadedObject = new OperatedObj(StringUtils.ToIntFast(parts[2]))
                                {
                                    LoadType = StringUtils.ToIntFast(parts[1]),
                                    Probability = StringUtils.ToIntFast(parts[5])
                                };
                                if (StringUtils.ToIntFast(parts[4]) >= 0)
                                {
                                    Room roomO = roomsCollection[StringUtils.ToIntFast(parts[4]), 0];
                                    if (roomO == null)
                                    {
                                        additionalInfo += $"\nПопытка загрузить моба ({parts[2]}) в отсутствующую комнату с vnum ({parts[4]}).";
                                    }
                                    else
                                    {
                                        roomO.LoadingObjectsCollection.Add(lastLoadedObject);
                                    }
                                }
                                break;
                            case 'F': //Мобы следуют в группе
                                parts = input.Split(' ');
                                /*(в коде, происходит следующим образом:
                                    1-й этап: перебираются НПС в комнате (room_vnum) сравнивает номер моба с mob_vnum_leader. 
                                          Первое совпадение и моб заносится в переменную leader
                                    2-й этап: перебираются НПС в комнате, находится первый моб с номером mob_vnum_follower, 
                                          проверяется обработан ли он уже(т.е. следует ли за искомым лидером, 
                                          для 2 и более одинаковых последователей) если нет, проставляется следование 
                                          за лидером из 1-го этапа.
                                    Про следования 2-х одинаковых групп за разными лидерами. Нельзя сделать в 1 комнате для 
                                    одинаковых мобов. Ситуация помоему аналогичная с сумоном - нельзя из группы одинаковых мобов 
                                    сумонить выборочно тех или других в зависимости от того к кому они приписаны, приходится создавать 
                                    различных мобов с одинаковыми названиями, как это к примеру реализовано в 3д-север.)
                                 */
                                //bool ifflag = (parts[1] == "1");
                                int roomVNum = StringUtils.ToIntFast(parts[2]);
                                int leaderVNum = StringUtils.ToIntFast(parts[3]);
                                int followerVNum = StringUtils.ToIntFast(parts[4]);
                                Room roomF = roomsCollection[roomVNum, 0];
                                bool success = false;
                                if (roomF != null) //Это хитрожопие сделано из за второй строки в зоне 277
                                {
                                    OperatedMob leader = null;
                                    foreach (OperatedMob mob in roomF.LoadingMobsCollection)
                                    {
                                        //Ищем лидера в комнате
                                        if (mob.VNum == leaderVNum && mob.Leader)
                                        {
                                            leader = mob;
                                            break;
                                        }
                                    }
                                    if (leader == null)
                                    {
                                        //если лидера нет, ищем потенциального лидера
                                        foreach (OperatedMob mob in roomF.LoadingMobsCollection)
                                        {
                                            if (mob.VNum == leaderVNum && !mob.Leader && mob.FollowsBy == -1)
                                            {
                                                leader = mob;
                                                leader.Leader = true;
                                                break;
                                            }
                                        }
                                    }
                                    if (leader != null)
                                    {
                                        foreach (OperatedMob mob in roomF.LoadingMobsCollection)
                                        {
                                            if (!mob.Leader && mob.FollowsBy == -1 && mob.VNum == followerVNum)
                                            {
                                                mob.FollowsBy = leaderVNum;
                                                success = true;
                                                break;
                                            }
                                        }
                                    }

                                    if (!success)
                                    {
                                        //кривой файл 
                                        additionalInfo += $"\nПроблема при добавлении последователя ({followerVNum}) мобу ({leaderVNum}).";
                                    }
                                }
                                break;
                            case 'P': //Поместить предмет в последний загруженный предмет
                                parts = input.Split(' ');
                                lastLoadedObject.ObjectsInObject.Add(StringUtils.ToIntFast(parts[2]),
                                                                     StringUtils.ToIntFast(parts[5]),
                                                                     StringUtils.ToIntFast(parts[1]));
                                break;
                            case 'L': //Загружать предмет в инвентарь после смерпти моба (который загружен предыдущей командой)
                                parts = input.Split(' ');
                                lastLoadedMob.AddObjectAfterDeath(StringUtils.ToIntFast(parts[1]),
                                                        StringUtils.ToIntFast(parts[2]),
                                                        StringUtils.ToIntFast(parts[3]),
                                                        StringUtils.ToIntFast(parts[4]));
                                break;
                            case 'G': //Дать предмет мобу (который загружен предыдущей командой)
                                parts = input.Split(' ');
                                lastLoadedMob.AddObject(StringUtils.ToIntFast(parts[2]),
                                                        (parts[1] == "1"),
                                                        StringUtils.ToIntFast(parts[5]));
                                break;
                            case 'E': //Экипировать предмет мобу (который загружен предыдущей командой)
                                parts = input.Split(' ');
                                lastLoadedMob.AddObject(StringUtils.ToIntFast(parts[2]),
                                                        (parts[1] == "1"),
                                                        StringUtils.ToIntFast(parts[4]),
                                                        StringUtils.ToIntFast(parts[5]));
                                break;
                            case 'R': //Удалить предмет из комнаты
                                parts = input.Split(' ');
                                var roomR = roomsCollection[StringUtils.ToIntFast(parts[2]), 0];
                                if (roomR != null)
                                {
                                    roomR.RemoovingObjects.Add(StringUtils.ToIntFast(parts[3]),
                                        0,
                                        -1);
                                }
                                else
                                {
                                    //кривой файл 
                                    additionalInfo += $"\nПопытка удалить моба ({parts[2]}) из отсутствующей комнаты с vnum ({parts[4]}).";
                                }
                                break;
                            case 'D': //Установить состояние существующего выхода
                                // 'D' <flag> <room_vnum> <door_pos> <door_state>
                                parts = input.Split(' ');
                                var roomD = roomsCollection[StringUtils.ToIntFast(parts[2]), 0];
                                var state = StringUtils.ToIntFast(parts[4]);
                                Exit curExit = null;
                                switch (parts[3])
                                {
                                    case "0":
                                        curExit = roomD.ExitNorth;
                                        break;
                                    case "1":
                                        curExit = roomD.ExitEast;
                                        break;
                                    case "2":
                                        curExit = roomD.ExitSouth;
                                        break;
                                    case "3":
                                        curExit = roomD.ExitWest;
                                        break;
                                    case "4":
                                        curExit = roomD.ExitUp;
                                        break;
                                    case "5":
                                        curExit = roomD.ExitDown;
                                        break;
                                }
                                if (curExit != null)
                                {
                                    curExit.ConditionFlag = parts[1] == "1";
                                    curExit.DoorState = state;
                                }
                                break;
                        }
                        if ((input = ReadLine(sr)) == null)
                        {
                            exit = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    FireExceptionEvent("Ошибка при загрузке зоны:\nФайл: \"" + filePath + "\"\nСтрока #" + filePos + ": " +
                            lastString + "\nДополнительная информация: " + additionalInfo, ex, EventLogEntryType.Warning);
                    sr.Close();
                    return false;
                }
                sr.Close();
                return true;
            }
        }

        // read a mobile
        // 'M' <flag> <mob_vnum> <max_in_world> <room_vnum> <max_in_room|-1>
        // Follow mobiles
        // 'F' <flag> <room_vnum> <leader_vnum> <mob_vnum>
        //удалить моба
        //Q flag vnum
        // read an object
        // 'O' <flag> <obj_vnum> <max_in_world> <room_vnum|-1> <load%|-1>
        // object to object
        // 'P' <flag> <obj_vnum> <max_in_world> <target_vnum> <load%|-1>
        // obj_to_char
        // 'G' <flag> <obj_vnum> <max_in_world> <-> <load%|-1>
        // object to equipment list
        // 'E' <flag> <obj_vnum> <max_in_world> <wear_pos> <load%|-1>
        // rem obj from room
        // 'R' <flag> <room_vnum> <obj_vnum>
        // set state of door
        // 'D' <flag> <room_vnum> <door_pos> <door_state>
        // trigger command; details to be filled in later
        // 'T' <flag> <trigger_type> <trigger_vnum> <room_vnum, для WLD_TRIGGER>
        //добавить глобальную переменную
        // 'V' <flag> <trigger_type> <room_vnum> <context> <var_name> <var_value>

        /// <summary>
        /// Сохранение данных в zon-файл
        /// </summary>
        /// <param name="zone">Объект, содержащий данные зоны</param>
        /// <param name="objectsCollection">Коллекция объектов сохраняемой зоны</param>
        /// <param name="mobsCollection">Коллекция мобов сохраняемой зоны</param>
        /// <param name="roomsCollection">Коллекция комнат сохраняемой зоны</param>
        public void Save(Zone zone, ObjsCollection objectsCollection,
                         MobsCollection mobsCollection, RoomsCollection roomsCollection)
        {
            var fs =
                new FileStream(StaticData.WorldFolderPath + @"\ZON\" + zone.Number + ".zon", FileMode.Create,
                               FileAccess.Write);
            var sw = new StreamWriter(fs, StaticData.CurrentEncoding) { NewLine = "\n" };
            //sw.WriteLine("* Сгенерировано BZEditor");
            //sw.WriteLine("* Сохранено " + DateTime.Now);
            sw.WriteLine("#" + zone.Number);
            sw.WriteLine(zone.Name + "~");
            if (!string.IsNullOrEmpty(zone.Comment))
            {
                sw.WriteLine("^" + zone.Comment + "~");
            }

            if (!string.IsNullOrEmpty(zone.Location))
            {
                sw.WriteLine("&" + zone.Location + "~");
            }

            if (!string.IsNullOrEmpty(zone.Author))
            {
                sw.WriteLine("!" + zone.Author + "~");
            }

            if (!string.IsNullOrEmpty(zone.Description))
            {
                sw.WriteLine("$" + zone.Description + "~");
            }

            sw.WriteLine("#" + zone.Level + " " + zone.Type + " " + zone.OptimalCharsInGroup);
            string flags = (zone.Test) ? " test" : " *";
            flags += (zone.Locked) ? " locked" : "";
            sw.WriteLine((zone.Number * 100 + 99) + " " + zone.RepopTimer + " " + zone.RepopType + " " +
                         zone.ResetIdle + flags + " ");
            /*[число4] (может отсутствовать, 
             * по умолчанию следует писать "0")
             * Булева переменная, показывающая,
             * очищать ли зону, если в нее никто 
             * не заходил после последней ее 
             * очистки*/
            if (zone.RepopType == 3)
            {
                foreach (int vnum in zone.ResetA)
                {
                    sw.WriteLine("A " + vnum);
                }

                foreach (int vnum in zone.ResetB)
                {
                    sw.WriteLine("B " + vnum);
                }
            }
            foreach (OperatedMob lm in zone.MobsToRemove)
            {
                var m = mobsCollection[lm.VNum, 0];
                var name = m != null ? m.Cases.Imen : "";
                var conditionFlag = lm.ConditionFlag ? "1" : "0";
                sw.WriteLine($"Q {conditionFlag} {lm.VNum} -1 -1 -1\t({name})");
            }

            var virualRoomVnum = zone.Number * 100 + 99;
            foreach (OperatedMob lm in zone.MobsLoadedInVirtualRoom)
            {
                var m = mobsCollection[lm.VNum, 0];
                var name = m?.Cases.Imen ?? "";
                var maxInWorld = m?.MaxInWorld ?? -1;
                string conditionFlag = (lm.ConditionFlag) ? "1" : "0";
                sw.WriteLine($"M {conditionFlag} {lm.VNum} {maxInWorld} {virualRoomVnum} {lm.MaxInRoom}\t({name}");
                if (lm.FollowsBy != -1)
                {
                    var mf = mobsCollection[lm.FollowsBy, 0];
                    var namef = mf?.Cases.Imen ?? "";
                    sw.WriteLine($"F {conditionFlag} {lm.VNum} {lm.FollowsBy} {lm.VNum} -1\t({namef}");
                }
                foreach (MobObj moin in lm.Items)
                {
                    var oin = objectsCollection[moin.VNum, 0];
                    var namein = oin?.Cases.Imen.ToLowerInvariant() ?? "";
                    var conditionFlagIn = (moin.ConditionFlag) ? "1" : "0"; //Мне кажется что тут всегда 1
                    var prob = moin.Probability;// == 100 ? -1 : moin.Probability;
                    if (moin.ObjPos == -1)
                    {
                        // 'G' <flag> <obj_vnum> <max_in_world> <-1> <load%|-1>
                        sw.WriteLine($"G {conditionFlagIn} {moin.VNum} {oin.MaxInWorld} -1 {prob}\t({namein}");
                    }
                    else
                    {
                        // 'E' <flag> <obj_vnum> <max_in_world> <wear_pos> <load%|-1>
                        sw.WriteLine($"E {conditionFlagIn} {moin.VNum} {oin.MaxInWorld} {moin.ObjPos} {prob}");
                    }
                }
                foreach (MobObjAfterDeath moin in lm.ItemsAfterDeath)
                {
                    var o = objectsCollection[moin.VNum, 0];
                    var namein = o?.Cases.Imen.ToLowerInvariant() ?? "";
                    sw.WriteLine($"L {moin.VNum} {moin.Probability} 0 {moin.LoadType} {moin.SpecParam}\t({namein})");
                }
            }
            foreach (Room r in roomsCollection)
            {
                foreach (OperatedObj lo in r.RemoovingObjects)
                {
                    var o = objectsCollection[lo.VNum, 0];
                    var name = o?.Cases.Imen.ToLowerInvariant() ?? "";
                    sw.WriteLine($"R 0 {r.VNum} {lo.VNum} -1 -1\t({name})");
                }
                foreach (OperatedObj lo in r.LoadingObjectsCollection)
                {
                    var o = objectsCollection[lo.VNum, 0];
                    var name = o?.Cases.Imen.ToLowerInvariant() ?? "";
                    var oMax = o?.MaxInWorld ?? -1;
                    // 'O' <flag> <obj_vnum> <max_in_world> <room_vnum|-1> <load%|-1> -1 = 100%
                    var prob = lo.Probability;// == 100 ? -1 : lo.Probability;
                    sw.WriteLine($"O {lo.LoadType} {lo.VNum} {oMax} {r.VNum} {prob}\t({name})");//Объект грузится безусловно
                    foreach (OperatedObj loin in lo.ObjectsInObject)
                    {
                        var oin = objectsCollection[loin.VNum, 0];
                        var namein = oin?.Cases.Imen ?? "";
                        var oinMax = oin?.MaxInWorld ?? -1;
                        // 'P' <flag> <obj_vnum> <max_in_world> <target_vnum> <load%|-1> -1 = 100%
                        var probin = loin.Probability;// == 100 ? -1 : loin.Probability;
                        sw.WriteLine($"P {loin.LoadType} {loin.VNum} {oinMax} {lo.VNum} {probin}\t({namein})");
                    }
                }
                foreach (OperatedMob lm in r.LoadingMobsCollection)
                {
                    var m = mobsCollection[lm.VNum, 0];
                    var name = m?.Cases.Imen ?? "";
                    var maxInWorld = m?.MaxInWorld ?? -1;
                    string conditionFlag = (lm.ConditionFlag) ? "1" : "0";
                    sw.WriteLine($"M {conditionFlag} {lm.VNum} {maxInWorld} {r.VNum} {lm.MaxInRoom}\t({name})");
                    if (lm.FollowsBy != -1)
                    {
                        var mf = mobsCollection[lm.FollowsBy, 0];
                        var namef = mf?.Cases.Imen ?? "";
                        sw.WriteLine($"F {conditionFlag} {r.VNum} {lm.FollowsBy} {lm.VNum} -1\t({namef})");
                    }
                    foreach (MobObj moin in lm.Items)
                    {
                        var oin = objectsCollection[moin.VNum, 0];
                        var namein = oin?.Cases.Imen.ToLowerInvariant() ?? "";
                        var conditionFlagIn = (moin.ConditionFlag) ? "1" : "0"; //Мне кажется что тут всегда 1
                        var prob = moin.Probability;// == 100 ? -1 : moin.Probability;

                        if (moin.ObjPos == -1)
                        {
                            // 'G' <flag> <obj_vnum> <max_in_world> <-1> <load%|-1>
                            sw.WriteLine($"G {conditionFlagIn} {moin.VNum} {oin.MaxInWorld} -1 {prob}\t({namein})");
                        }
                        else
                        {
                            // 'E' <flag> <obj_vnum> <max_in_world> <wear_pos> <load%|-1>
                            sw.WriteLine($"E {conditionFlagIn} {moin.VNum} {oin.MaxInWorld} {moin.ObjPos} {prob}\t({namein})");
                        }
                    }
                    foreach (MobObjAfterDeath moin in lm.ItemsAfterDeath)
                    {
                        var o = objectsCollection[moin.VNum, 0];
                        var namein = o?.Cases.Imen.ToLowerInvariant() ?? "";
                        var prob = moin.Probability;// == 100 ? -1 : moin.Probability;
                        sw.WriteLine($"L {moin.VNum} {prob} {moin.LoadType} {moin.SpecParam}\t({namein})");
                    }
                }
                //0 я хз что такое, дальше : номер комнаты, направление, состояние выхода, остальные аргументы отсутствуют
                if (r.ExitNorth.DoorState > 0)
                    sw.WriteLine($"D {(r.ExitNorth.ConditionFlag ? "1" : "0")} {r.VNum} 0 {r.ExitNorth.DoorState} -1\t({r.Name})");
                if (r.ExitEast.DoorState > 0)
                    sw.WriteLine($"D {(r.ExitEast.ConditionFlag ? "1" : "0")} {r.VNum} 1 {r.ExitEast.DoorState} -1\t({r.Name})");
                if (r.ExitSouth.DoorState > 0)
                    sw.WriteLine($"D {(r.ExitSouth.ConditionFlag ? "1" : "0")} {r.VNum} 2 {r.ExitSouth.DoorState} -1\t({r.Name})");
                if (r.ExitWest.DoorState > 0)
                    sw.WriteLine($"D {(r.ExitWest.ConditionFlag ? "1" : "0")} {r.VNum} 3 {r.ExitWest.DoorState} -1\t({r.Name})");
                if (r.ExitUp.DoorState > 0)
                    sw.WriteLine($"D {(r.ExitUp.ConditionFlag ? "1" : "0")} {r.VNum} 4 {r.ExitUp.DoorState} -1\t({r.Name})");
                if (r.ExitDown.DoorState > 0)
                    sw.WriteLine($"D {(r.ExitDown.ConditionFlag ? "1" : "0")} {r.VNum} 5 {r.ExitDown.DoorState} -1\t({r.Name})");
            }
            sw.WriteLine("S");
            sw.WriteLine("$");
            sw.Close();
            fs.Close();
            sw.Dispose();
            fs.Dispose();
        }
    }
}