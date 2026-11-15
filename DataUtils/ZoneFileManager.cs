using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Collections.Generic;
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
            string filePath = Path.Combine(StaticData.WorldFolderPath, "ZON", zoneNumber + ".zon");
            // The zone number must be correct even when the .zon is missing or empty (some
            // worlds ship a placeholder zone without a header); the "#<n>" line below, when
            // present, overrides this with the same value.
            zone.Number = StringUtils.ToIntFast(zoneNumber);
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
                    // A command the room model cannot hold (spawn into a room missing from the zone,
                    // an unresolvable FOLLOW) is kept verbatim in zone.UnmodeledCommands instead of being
                    // dropped; the G/E/P that hang off such a spawn go with it.
                    bool lastMobVerbatim = false, lastObjVerbatim = false;
                    var pendingFollows = new List<string>();
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
                                ProcessMobsRemovedAtRepop(zone, input);
                                break;
                            case 'M': //Мобы, заргужаемые в комнаты
                                parts = input.Split(' ');
                                lastLoadedMob = new OperatedMob(StringUtils.ToIntFast(parts[2]))
                                {
                                    ConditionFlag = (StringUtils.ToIntFast(parts[1]) & 1) != 0,
                                    IfFlagRaw = StringUtils.ToIntFast(parts[1]),
                                    CommandMaxInWorld = StringUtils.ToIntFast(parts[3]),
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
                                    mobData.ZoneMaxAtLoad = mobData.MaxInWorld;
                                }

                                Room room = roomsCollection[StringUtils.ToIntFast(parts[4]), 0];
                                if (room == null)
                                {
                                    if (parts[4].EndsWith("99"))
                                    {
                                        //Виртуальная комната
                                        zone.MobsLoadedInVirtualRoom.Add(lastLoadedMob);
                                        lastMobVerbatim = false;
                                        break;
                                    }
                                    KeepVerbatim(zone, input);
                                    lastMobVerbatim = true;
                                }
                                else
                                {
                                    room.LoadingMobsCollection.Add(lastLoadedMob);
                                    lastMobVerbatim = false;
                                }
                                break;
                            case 'O': //Объекты, заргужаемые в комнаты
                                parts = input.Split(' ');
                                var part5 = GetPartOrDefault(parts, 5);
                                lastLoadedObject = new OperatedObj(StringUtils.ToIntFast(parts[2]))
                                {
                                    LoadType = StringUtils.ToIntFast(parts[1]),
                                    Probability = StringUtils.ToIntFast(part5)
                                };
                                if (StringUtils.ToIntFast(parts[4]) >= 0)
                                {
                                    Room roomO = roomsCollection[StringUtils.ToIntFast(parts[4]), 0];
                                    if (roomO == null)
                                    {
                                        KeepVerbatim(zone, input);
                                        lastObjVerbatim = true;
                                    }
                                    else
                                    {
                                        roomO.LoadingObjectsCollection.Add(lastLoadedObject);
                                        lastObjVerbatim = false;
                                    }
                                }
                                else
                                {
                                    KeepVerbatim(zone, input);
                                    lastObjVerbatim = true;
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
                                // A FOLLOW can precede the M of its follower; retry those once the
                                // whole file is read, and keep only the truly unresolvable verbatim
                                // (same-vnum groups, virtual-room leaders): the engine resolves them at reset.
                                if (!TryFollow(roomsCollection, zone, parts)) pendingFollows.Add(input);
                                break;
                            case 'T': //Триггер (TRIGGER) и переменная триггера (VAR)
                            case 'V':
                                {
                                    // <flag> <type> ...: type 0 = mob (binds to the last loaded mob),
                                    // 1 = object (last loaded object), 2 = room (uses its room argument).
                                    parts = input.Split(' ');
                                    int trigType = parts.Length > 2 ? StringUtils.ToIntFast(parts[2]) : 2;
                                    if (trigType == 0 && lastLoadedMob.VNum >= 0 && !lastMobVerbatim)
                                        lastLoadedMob.AnchoredCommands.Add(input.Trim());
                                    else if (trigType == 1 && lastLoadedObject.VNum >= 0 && !lastObjVerbatim)
                                        lastLoadedObject.AnchoredCommands.Add(input.Trim());
                                    else
                                        KeepVerbatim(zone, input); // room type, or its anchor is itself verbatim
                                }
                                break;
                            case 'P': //Поместить предмет в последний загруженный предмет
                                if (lastObjVerbatim) { KeepVerbatim(zone, input); break; }
                                parts = input.Split(' ');
                                part5 = GetPartOrDefault(parts, 5);
                                lastLoadedObject.ObjectsInObject.Add(StringUtils.ToIntFast(parts[2]),
                                                                     StringUtils.ToIntFast(part5),
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
                                if (lastMobVerbatim) { KeepVerbatim(zone, input); break; }
                                parts = input.Split(' ');
                                lastLoadedMob.AddObject(StringUtils.ToIntFast(parts[2]),
                                                        (StringUtils.ToIntFast(parts[1]) & 1) != 0,
                                                        StringUtils.ToIntFast(parts[5]));
                                SetLastItemIfFlag(lastLoadedMob, parts[1]);
                                break;
                            case 'E' when input.StartsWith("EXTRACT", StringComparison.OrdinalIgnoreCase): //Мобы, удаляемые при перезапуске
                                ProcessMobsRemovedAtRepop(zone, input);
                                break;
                            case 'E': //Экипировать предмет мобу (который загружен предыдущей командой)
                                if (lastMobVerbatim) { KeepVerbatim(zone, input); break; }
                                parts = input.Split(' ');
                                lastLoadedMob.AddObject(StringUtils.ToIntFast(parts[2]),
                                                        (StringUtils.ToIntFast(parts[1]) & 1) != 0,
                                                        StringUtils.ToIntFast(parts[4]),
                                                        StringUtils.ToIntFast(parts[5]));
                                SetLastItemIfFlag(lastLoadedMob, parts[1]);
                                break;
                            case 'R': //Удалить предмет из комнаты
                                parts = input.Split(' ');
                                var roomR = roomsCollection[StringUtils.ToIntFast(parts[2]), 0];
                                if (roomR != null)
                                {
                                    // LoadType carries the REMOVE command's if_flag (written back as-is).
                                    roomR.RemoovingObjects.Add(StringUtils.ToIntFast(parts[3]),
                                        0,
                                        StringUtils.ToIntFast(parts[1]));
                                }
                                else
                                {
                                    KeepVerbatim(zone, input);
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
                    foreach (string follow in pendingFollows)
                        if (!TryFollow(roomsCollection, zone, follow.Split(' ')))
                            KeepVerbatim(zone, follow);
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
                new FileStream(Path.Combine(StaticData.WorldFolderPath, "ZON", zone.Number + ".zon"), FileMode.Create,
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
            var virtualFollowLines = new List<string>();
            foreach (OperatedMob lm in zone.MobsLoadedInVirtualRoom)
            {
                var m = mobsCollection[lm.VNum, 0];
                var name = m?.Cases.Imen ?? "";
                var maxInWorld = CommandMax(lm, m);
                string conditionFlag = IfFlag(lm.ConditionFlag, lm.IfFlagRaw);
                sw.WriteLine($"M {conditionFlag} {lm.VNum} {maxInWorld} {virualRoomVnum} {lm.MaxInRoom}\t({name}");
                foreach (string anchored in lm.AnchoredCommands) sw.WriteLine(anchored);
                if (lm.FollowsBy != -1)
                {
                    var mf = mobsCollection[lm.FollowsBy, 0];
                    var namef = mf?.Cases.Imen ?? "";
                    // Откладываем F до конца виртуальной комнаты, как и в цикле по комнатам ниже:
                    // F связывает двух уже загруженных мобов, поэтому M лидера должна стоять раньше.
                    // Заодно F не разрывает пару "M моба -- его G/E": у вещи с битом 1 предыдущей
                    // командой должна остаться загрузка самого моба, а не следование.
                    virtualFollowLines.Add($"F {FollowFlag(lm)} {virualRoomVnum} {lm.FollowsBy} {lm.VNum} -1\t({namef})");
                }
                foreach (MobObj moin in lm.Items)
                {
                    var oin = objectsCollection[moin.VNum, 0];
                    var namein = oin?.Cases.Imen.ToLowerInvariant() ?? "";
                    var conditionFlagIn = IfFlag(moin.ConditionFlag, moin.IfFlagRaw); //Мне кажется что тут всегда 1
                    var prob = moin.Probability;// == 100 ? -1 : moin.Probability;
                    if (moin.ObjPos == -1)
                    {
                        // 'G' <flag> <obj_vnum> <max_in_world> <-1> <load%|-1>
                        sw.WriteLine($"G {conditionFlagIn} {moin.VNum} {oin?.MaxInWorld ?? -1} -1 {prob}\t({namein}");
                    }
                    else
                    {
                        // 'E' <flag> <obj_vnum> <max_in_world> <wear_pos> <load%|-1>
                        sw.WriteLine($"E {conditionFlagIn} {moin.VNum} {oin?.MaxInWorld ?? -1} {moin.ObjPos} {prob}");
                    }
                }
                foreach (MobObjAfterDeath moin in lm.ItemsAfterDeath)
                {
                    var o = objectsCollection[moin.VNum, 0];
                    var namein = o?.Cases.Imen.ToLowerInvariant() ?? "";
                    // Раскладка 'L' общая с циклом по комнатам: <vnum> <шанс> <тип> <параметр>.
                    // Лишнее поле сдвигало тип и параметр при следующей загрузке.
                    sw.WriteLine($"L {moin.VNum} {moin.Probability} {moin.LoadType} {moin.SpecParam}\t({namein})");
                }
            }
            foreach (var followLine in virtualFollowLines)
                sw.WriteLine(followLine);
            foreach (Room r in roomsCollection)
            {
                foreach (OperatedObj lo in r.RemoovingObjects)
                {
                    var o = objectsCollection[lo.VNum, 0];
                    var name = o?.Cases.Imen.ToLowerInvariant() ?? "";
                    sw.WriteLine($"R {(lo.LoadType > 0 ? lo.LoadType : 0)} {r.VNum} {lo.VNum} -1 -1\t({name})");
                }
                foreach (OperatedObj lo in r.LoadingObjectsCollection)
                {
                    var o = objectsCollection[lo.VNum, 0];
                    var name = o?.Cases.Imen.ToLowerInvariant() ?? "";
                    var oMax = o?.MaxInWorld ?? -1;
                    // 'O' <flag> <obj_vnum> <max_in_world> <room_vnum|-1> <load%|-1> -1 = 100%
                    var prob = lo.Probability;// == 100 ? -1 : lo.Probability;
                    sw.WriteLine($"O {lo.LoadType} {lo.VNum} {oMax} {r.VNum} {prob}\t({name})");//Объект грузится безусловно
                    foreach (string anchored in lo.AnchoredCommands) sw.WriteLine(anchored);
                    foreach (OperatedObj loin in lo.ObjectsInObject)
                    {
                        var oin = objectsCollection[loin.VNum, 0];
                        var namein = oin?.Cases.Imen ?? "";
                        // 'P' <flag> <obj_vnum> <room_vnum|-1|0> <target_vnum> <load%|-1> -1 = 100%
                        var probin = loin.Probability;// == 100 ? -1 : loin.Probability;
                        sw.WriteLine($"P {loin.LoadType} {loin.VNum} {r.VNum} {lo.VNum} {probin}\t({namein})");
                    }
                }
                var followLines = new List<string>();
                foreach (OperatedMob lm in r.LoadingMobsCollection)
                {
                    var m = mobsCollection[lm.VNum, 0];
                    var name = m?.Cases.Imen ?? "";
                    var maxInWorld = CommandMax(lm, m);
                    string conditionFlag = IfFlag(lm.ConditionFlag, lm.IfFlagRaw);
                    sw.WriteLine($"M {conditionFlag} {lm.VNum} {maxInWorld} {r.VNum} {lm.MaxInRoom}\t({name})");
                    foreach (string anchored in lm.AnchoredCommands) sw.WriteLine(anchored);
                    if (lm.FollowsBy != -1)
                    {
                        var mf = mobsCollection[lm.FollowsBy, 0];
                        var namef = mf?.Cases.Imen ?? "";
                        // Defer F until after every M in this room: F links two already-loaded
                        // mobs, so the leader's M must precede it (otherwise both the engine and
                        // the editor's reader drop the follow). See zone 326.
                        followLines.Add($"F {FollowFlag(lm)} {r.VNum} {lm.FollowsBy} {lm.VNum} -1\t({namef})");
                    }
                    foreach (MobObj moin in lm.Items)
                    {
                        var oin = objectsCollection[moin.VNum, 0];
                        var namein = oin?.Cases.Imen.ToLowerInvariant() ?? "";
                        var conditionFlagIn = IfFlag(moin.ConditionFlag, moin.IfFlagRaw); //Мне кажется что тут всегда 1
                        var prob = moin.Probability;// == 100 ? -1 : moin.Probability;

                        if (moin.ObjPos == -1)
                        {
                            // 'G' <flag> <obj_vnum> <max_in_world> <-1> <load%|-1>
                            sw.WriteLine($"G {conditionFlagIn} {moin.VNum} {oin?.MaxInWorld ?? -1} -1 {prob}\t({namein})");
                        }
                        else
                        {
                            // 'E' <flag> <obj_vnum> <max_in_world> <wear_pos> <load%|-1>
                            sw.WriteLine($"E {conditionFlagIn} {moin.VNum} {oin?.MaxInWorld ?? -1} {moin.ObjPos} {prob}\t({namein})");
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
                foreach (var followLine in followLines)
                    sw.WriteLine(followLine);
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

        private static void ProcessMobsRemovedAtRepop(Zone zone, string input)
        {
            var parts = input.Split(' ');
            zone.MobsToRemove.Add(StringUtils.ToIntFast(parts[2]), parts[1] == "1", -1);
        }

        // Мобы виртуальной комнаты зоны (NN99) лежат не в комнате, а в своей коллекции зоны:
        // настоящей комнаты NN99 в мире нет, поэтому FOLLOW там не разбирался вовсе и уходил
        // в дословные команды, а выставленное в UI следование писалось второй такой же строкой.
        private static OperatedMobsCollection MobsOfRoom(RoomsCollection roomsCollection, Zone zone, int roomVNum)
        {
            if (zone != null && roomVNum == zone.Number * 100 + 99)
                return zone.MobsLoadedInVirtualRoom;
            return roomsCollection[roomVNum, 0]?.LoadingMobsCollection;
        }

        private static bool TryFollow(RoomsCollection roomsCollection, Zone zone, string[] parts)
        {
            int roomVNum = StringUtils.ToIntFast(parts[2]);
            int leaderVNum = StringUtils.ToIntFast(parts[3]);
            int followerVNum = StringUtils.ToIntFast(parts[4]);
            OperatedMobsCollection roomMobs = MobsOfRoom(roomsCollection, zone, roomVNum);
            bool success = false;
            if (roomMobs != null) //Это хитрожопие сделано из за второй строки в зоне 277
            {
                OperatedMob leader = null;
                foreach (OperatedMob mob in roomMobs)
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
                    foreach (OperatedMob mob in roomMobs)
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
                    foreach (OperatedMob mob in roomMobs)
                    {
                        if (!mob.Leader && mob.FollowsBy == -1 && mob.VNum == followerVNum)
                        {
                            mob.FollowsBy = leaderVNum;
                            mob.FollowIfFlagRaw = StringUtils.ToIntFast(parts[1]);
                            success = true;
                            break;
                        }
                    }
                }

            }
            return success;
        }

        private static void KeepVerbatim(Zone zone, string letterLine) =>
            zone.UnmodeledCommands.Add(YamlFormatProvider.ToEngineCommand(letterLine.Trim()));

        private static string IfFlag(bool condition, int raw) =>
            ((raw >= 0 ? raw & ~1 : 0) | (condition ? 1 : 0)).ToString();

        // FOLLOW keeps its own if_flag when it was read; a follow added in the UI inherits the
        // mob's condition, as before.
        private static string FollowFlag(OperatedMob lm) =>
            lm.FollowIfFlagRaw >= 0 ? lm.FollowIfFlagRaw.ToString() : IfFlag(lm.ConditionFlag, lm.IfFlagRaw);

        private static void SetLastItemIfFlag(OperatedMob mob, string flag)
        {
            if (mob.Items.Count > 0 && mob.Items[mob.Items.Count - 1] is MobObj item)
                item.IfFlagRaw = StringUtils.ToIntFast(flag);
        }

        private static int CommandMax(OperatedMob lm, Mob m)
        {
            int mobMax = m?.MaxInWorld ?? -1;
            if (lm.CommandMaxInWorld <= 0) return mobMax;
            if (m != null && m.ZoneMaxAtLoad != int.MinValue && m.MaxInWorld != m.ZoneMaxAtLoad)
                return mobMax; // edited in the UI: the new limit applies to every spawn, as before
            return mobMax > 0 ? Math.Min(lm.CommandMaxInWorld, mobMax) : lm.CommandMaxInWorld;
        }

        private static string GetPartOrDefault(string[] parts, int index, string defaultValue = "-1")
        {
            // Was 'index + 1': it needed a trailing token after the field, so engine strings
            // (no trailing comment) lost every load% and every O/P command loaded at 100%.
            return parts.Length > index ? parts[index].Split('\t')[0] : defaultValue;
        }
    }
}