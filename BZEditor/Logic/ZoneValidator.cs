using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DataUtils;
using ExtControls;
using Object = DataUtils.Obj;

namespace BZEditor
{
    public enum ValidationErrLevel
    {
        AllErrors = 0,
        CriticalErrorsOnly = 1
    }

    public enum ErrTypes
    {
        Ошибка = 0,
        Предупреждение = 1
    }

    public enum ErrSourceTypes
    {
        Зона,
        Мобы,
        Предметы,
        Комнаты,
        Магазины,
        Триггеры,
        Прочее
    }

    public class ZoneError
    {
        public string ErrCaption;
        public string ErrMessage;
        public ErrSourceTypes ErrSourceType;
        public ParseMessageType ErrType;
        public int VNum;
        public ActionType Action = ActionType.DoNothing;

        public ZoneError(int vNum, string errCaption, string errMessage, ErrSourceTypes errSourceType)
            : this(vNum, errCaption, errMessage, errSourceType, ParseMessageType.Предупреждение, ActionType.DoNothing)
        {
        }

        public ZoneError(int vNum, string errCaption, string errMessage, ErrSourceTypes errSourceType, ActionType action)
            : this(vNum, errCaption, errMessage, errSourceType, ParseMessageType.Предупреждение, action)
        {
        }

        public ZoneError(int vNum, string errCaption, string errMessage, ErrSourceTypes errSourceType,
                          ParseMessageType errType, ActionType action)
        {
            VNum = vNum;
            ErrCaption = errCaption;
            ErrMessage = errMessage;
            ErrSourceType = errSourceType;
            ErrType = errType;
            Action = action;
        }
    }

    public class ZoneValidator
    {
        private MobsCollection allLoadedMobs;
        private ObjsCollection allLoadedObjects;
        private ZoneDataManager zoneDataManager;
        private readonly List<ZoneError> errors = new List<ZoneError>();

        /// <summary>
        /// Проверка зоны на наличие ошибок
        /// </summary>
        /// <returns>Список найденных ошибок</returns>
        public List<ZoneError> Validate(ZoneDataManager dm, MobsCollection inAllLoadedMobs, ObjsCollection inAllLoadedObjects)
        {
            allLoadedMobs = inAllLoadedMobs;
            allLoadedObjects = inAllLoadedObjects;
            errors.Clear();
            zoneDataManager = dm;
            ValidateRooms();
            ValidateMobs();
            ValidateObjects();
            ValidateTriggers();
            return errors;
        }

        private static readonly Regex IfTemplate = new Regex(@"\bif\b", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase);
        private static readonly Regex EndTemplate = new Regex(@"\bend\b", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase);
        private static readonly Regex WhileSwitchForeachTemplate = new Regex(@"\b(while|switch|foreach)\b", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase);
        private static readonly Regex DoneTemplate = new Regex(@"\bdone\b", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase);
        private static readonly Regex GlobalVarTemplate = new Regex(@"\bglobal\b %(\w|\d)+%", RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase);

        private void ValidateTriggers()
        {
            foreach (Trigger trigger in zoneDataManager.Triggers)
            {
                int lineIndex = 0;
                string ampersErrors = string.Empty;
                string barcErrors = string.Empty;
                string globalErrors = string.Empty;
                List<int> ifWhilePos = new List<int>();
                List<int> foreachSwitchWhilePos = new List<int>();
                List<int> endWithoutIfPos = new List<int>();
                List<int> doneWithoutForeachSwitchWhilePos = new List<int>();
                string[] trLines = trigger.Body.Replace("\r\n", "\n").Split('\n');
                foreach (string trLine in trLines)
                {
                    //Правило 1
                    if (trLine.Contains(" & "))
                        ampersErrors += "\n (№" + lineIndex + ") " + trLine;
                    //Правило 2
                    int openCntr = 0;
                    int closeCntr = 0;
                    foreach (char c in trLine)
                    {
                        switch (c)
                        {
                            case '(':
                                openCntr++;
                                break;
                            case ')':
                                closeCntr++;
                                break;
                        }
                    }
                    if (openCntr != closeCntr)
                        barcErrors += "\n (№" + lineIndex + ") " + trLine;
                    //Правило 3
                    if (IfTemplate.Match(trLine).Success)
                        ifWhilePos.Add(lineIndex);
                    if (EndTemplate.Match(trLine).Success)
                        if (ifWhilePos.Count > 0)
                            ifWhilePos.RemoveAt(ifWhilePos.Count - 1);
                        else
                            endWithoutIfPos.Add(lineIndex);
                    //Правило 4
                    if (WhileSwitchForeachTemplate.Match(trLine).Success)
                        foreachSwitchWhilePos.Add(lineIndex);
                    if (DoneTemplate.Match(trLine).Success)
                    {
                        if (foreachSwitchWhilePos.Count > 0)
                            foreachSwitchWhilePos.RemoveAt(foreachSwitchWhilePos.Count - 1);
                        else
                            doneWithoutForeachSwitchWhilePos.Add(lineIndex);
                    }
                    //Правило 5
                    if (GlobalVarTemplate.Match(trLine).Success)
                        globalErrors += "\n (№" + lineIndex + ") " + trLine;
                    lineIndex++;
                }

                if (endWithoutIfPos.Count > 0)
                {
                    string errorsList = string.Empty;
                    foreach (int pos in endWithoutIfPos)
                        errorsList += "\n (№" + pos + ") " + trLines[pos];

                    errors.Add(new ZoneError(trigger.VNum, "Возможно оператор \"end\" не имеет парного оператора \"if\"",
                                              "Триггер " + trigger + " >> Строки:" + errorsList,
                                              ErrSourceTypes.Триггеры, ParseMessageType.Ошибка, ActionType.GoToTrigger));
                }

                if (doneWithoutForeachSwitchWhilePos.Count > 0)
                {
                    string errorsList = string.Empty;
                    foreach (int pos in doneWithoutForeachSwitchWhilePos)
                        errorsList += "\n (№" + pos + ") " + trLines[pos];

                    errors.Add(new ZoneError(trigger.VNum, "Возможно оператор \"done\" не имеет парного оператора \"foreach\", \"while\" или \"switch\"",
                                              "Триггер " + trigger + " >> Строки:" + errorsList,
                                              ErrSourceTypes.Триггеры, ParseMessageType.Ошибка, ActionType.GoToTrigger));
                }

                if (ifWhilePos.Count > 0)
                {
                    string ifWhileErrors = string.Empty;
                    foreach (int pos in ifWhilePos)
                        ifWhileErrors += "\n (№" + pos + ") " + trLines[pos];

                    errors.Add(new ZoneError(trigger.VNum, "Возможно оператор не имеет парного оператора \"end\"",
                                              "Триггер " + trigger + " >> Строки:" + ifWhileErrors,
                                              ErrSourceTypes.Триггеры, ParseMessageType.Ошибка, ActionType.GoToTrigger));
                }

                if (foreachSwitchWhilePos.Count > 0)
                {
                    string foreachSwitchErrors = string.Empty;
                    foreach (int pos in foreachSwitchWhilePos)
                        foreachSwitchErrors += "\n (№" + pos + ") " + trLines[pos];

                    errors.Add(new ZoneError(trigger.VNum, "Возможно оператор не имеет парного оператора \"done\"",
                                              "Триггер " + trigger + " >> Строки:" + foreachSwitchErrors,
                                              ErrSourceTypes.Триггеры, ParseMessageType.Ошибка, ActionType.GoToTrigger));
                }
                if (barcErrors.Length > 0)
                    errors.Add(new ZoneError(trigger.VNum, "Возможно имеют место непарные скобки", "Триггер " + trigger + " >> Строки:" + barcErrors, ErrSourceTypes.Триггеры, ParseMessageType.Ошибка, ActionType.GoToTrigger));

                if (globalErrors.Length > 0)
                    errors.Add(new ZoneError(trigger.VNum, "Возможно неверное объявление глобальной переменной", "Триггер " + trigger + " >> Строки:" + globalErrors, ErrSourceTypes.Триггеры, ParseMessageType.Предупреждение, ActionType.GoToTrigger));

                if (ampersErrors.Length > 0)
                    errors.Add(new ZoneError(trigger.VNum, "Возможно вместо \"&&\" используется \"&\"", "Триггер " + trigger + " >> Строки:" + ampersErrors, ErrSourceTypes.Триггеры, ParseMessageType.Предупреждение, ActionType.GoToTrigger));
            }
        }

        private void ValidateRooms()
        {
            foreach (Room r in zoneDataManager.Rooms)
            {
                string tmpstr = string.Empty;
                Room tmpRoom = zoneDataManager.Rooms[r.ExitNorth.RoomVNum, 0];
                if (tmpRoom != null)
                {
                    if (tmpRoom.ExitSouth.RoomVNum != r.VNum)
                        tmpstr += "\n На север в комнату " + r.ExitNorth.RoomVNum;
                }
                tmpRoom = zoneDataManager.Rooms[r.ExitSouth.RoomVNum, 0];
                if (tmpRoom != null)
                {
                    if (tmpRoom.ExitNorth.RoomVNum != r.VNum)
                        tmpstr += "\n На юг в комнату " + r.ExitNorth.RoomVNum;
                }
                tmpRoom = zoneDataManager.Rooms[r.ExitWest.RoomVNum, 0];
                if (tmpRoom != null)
                {
                    if (tmpRoom.ExitEast.RoomVNum != r.VNum)
                        tmpstr += "\n На запад в комнату " + r.ExitNorth.RoomVNum;
                }
                tmpRoom = zoneDataManager.Rooms[r.ExitEast.RoomVNum, 0];
                if (tmpRoom != null)
                {
                    if (tmpRoom.ExitWest.RoomVNum != r.VNum)
                        tmpstr += "\n На восток в комнату " + r.ExitNorth.RoomVNum;
                }
                tmpRoom = zoneDataManager.Rooms[r.ExitUp.RoomVNum, 0];
                if (tmpRoom != null)
                {
                    if (tmpRoom.ExitDown.RoomVNum != r.VNum)
                        tmpstr += "\n Вверх в комнату " + r.ExitNorth.RoomVNum;
                }
                tmpRoom = zoneDataManager.Rooms[r.ExitDown.RoomVNum, 0];
                if (tmpRoom != null)
                {
                    if (tmpRoom.ExitUp.RoomVNum != r.VNum)
                        tmpstr += "\n Вниз в комнату " + r.ExitNorth.RoomVNum;
                }
                if (!string.IsNullOrEmpty(tmpstr))
                {
                    this.errors.Add(
                        new ZoneError(r.VNum, "Наличие непарных выходов",
                                       "Комната " + r + " >> Непарные выходы:" + tmpstr,
                                       ErrSourceTypes.Комнаты, ActionType.GoToRoom));
                }

                CheckRoomDescription(r.Description.Main, "Описание: Общее", r);
                CheckRoomDescription(r.Description.Day, "Описание: День", r);
                CheckRoomDescription(r.Description.Night, "Описание: Ночь", r);
                CheckRoomDescription(r.Description.AutumnDay, "Описание: Осень[Д]", r);
                CheckRoomDescription(r.Description.AutumnNight, "Описание: Осень[Н]", r);
                CheckRoomDescription(r.Description.WinterDay, "Описание: Зима[Д]", r);
                CheckRoomDescription(r.Description.WinterNight, "Описание: Зима[Н]", r);
                CheckRoomDescription(r.Description.SpringDay, "Описание: Весна[Д]", r);
                CheckRoomDescription(r.Description.SpringNight, "Описание: Весна[Н]", r);
                CheckRoomDescription(r.Description.SummerDay, "Описание: Лето[Д]", r);
                CheckRoomDescription(r.Description.SummerNight, "Описание: Лето[Н]", r);

                int summ = 0;
                List<ZoneError> errors = new List<ZoneError>();
                if (string.IsNullOrEmpty(r.Description.Main))
                {
                    if (string.IsNullOrEmpty(r.Description.Day))
                    {
                        if (string.IsNullOrEmpty(r.Description.AutumnDay))
                            errors.Add(
                                new ZoneError(r.VNum, "Комната не будет иметь никакого описания в период: \"Осень-День\"",
                                               "Комната " + r, ErrSourceTypes.Комнаты, ParseMessageType.Ошибка, ActionType.GoToRoom));
                        else summ++;
                        if (string.IsNullOrEmpty(r.Description.WinterDay))
                            errors.Add(
                                new ZoneError(r.VNum, "Комната не будет иметь никакого описания в период: \"Зима-День\"",
                                               "Комната " + r, ErrSourceTypes.Комнаты, ParseMessageType.Ошибка, ActionType.GoToRoom));
                        else summ++;
                        if (string.IsNullOrEmpty(r.Description.SpringDay))
                            errors.Add(
                                new ZoneError(r.VNum, "Комната не будет иметь никакого описания в период: \"Весна-День\"",
                                               "Комната " + r, ErrSourceTypes.Комнаты, ParseMessageType.Ошибка, ActionType.GoToRoom));
                        else summ++;
                        if (string.IsNullOrEmpty(r.Description.SummerDay))
                            errors.Add(
                                new ZoneError(r.VNum, "Комната не будет иметь никакого описания в период: \"Лето-День\"",
                                               "Комната " + r, ErrSourceTypes.Комнаты, ParseMessageType.Ошибка, ActionType.GoToRoom));
                        else summ++;
                    }
                    else summ++;
                    if (string.IsNullOrEmpty(r.Description.Night))
                    {
                        if (string.IsNullOrEmpty(r.Description.AutumnNight))
                            errors.Add(
                                new ZoneError(r.VNum, "Комната не будет иметь никакого описания в период: \"Осень-Ночь\"",
                                               "Комната " + r, ErrSourceTypes.Комнаты, ParseMessageType.Ошибка, ActionType.GoToRoom));
                        else summ++;
                        if (string.IsNullOrEmpty(r.Description.WinterNight))
                            errors.Add(
                                new ZoneError(r.VNum, "Комната не будет иметь никакого описания в период: \"Зима-Ночь\"",
                                               "Комната " + r, ErrSourceTypes.Комнаты, ParseMessageType.Ошибка, ActionType.GoToRoom));
                        else summ++;
                        if (string.IsNullOrEmpty(r.Description.SpringNight))
                            errors.Add(
                                new ZoneError(r.VNum, "Комната не будет иметь никакого описания в период: \"Весна-Ночь\"",
                                               "Комната " + r, ErrSourceTypes.Комнаты, ParseMessageType.Ошибка, ActionType.GoToRoom));
                        else summ++;
                        if (string.IsNullOrEmpty(r.Description.SummerNight))
                            errors.Add(
                                new ZoneError(r.VNum, "Комната не будет иметь никакого описания в период: \"Лето-Ночь\"",
                                               "Комната " + r, ErrSourceTypes.Комнаты, ParseMessageType.Ошибка, ActionType.GoToRoom));
                    }
                    else summ++;
                }
                else
                    summ++;
                if (summ == 0)
                    this.errors.Add(
                                new ZoneError(r.VNum, "Полностью отсутствует описание",
                                               "Комната " + r, ErrSourceTypes.Комнаты, ParseMessageType.Ошибка, ActionType.GoToRoom));
                else
                    this.errors.AddRange(errors);
            }

        }

        private void CheckRoomDescription(string text, string paramName, BaseDataObject r)
        {
            if (string.IsNullOrEmpty(text)) return;
            if (!text.StartsWith("   ") && !text.StartsWith("___"))
            //В описании отсутствуют 3 вводных пробела в комнате
            {
                errors.Add(
                    new ZoneError(r.VNum, "В описании отсутствуют 3 вводных пробела или знака \"_\"",
                                   "Комната " + r + ", Параметр : " + paramName, ErrSourceTypes.Комнаты, ActionType.GoToRoom));
            }
            string[] descr = text.Replace("\r", "").Split('\n');
            if (descr.Length < 3) //Основное описание комнаты короче 3 строк
            {
                errors.Add(
                    new ZoneError(r.VNum, "Основное описание комнаты короче 3 строк", "Комната " + r + ", Параметр : " + paramName,
                                   ErrSourceTypes.Комнаты, ActionType.GoToRoom));
            }
            string tmpstr = string.Empty;
            int cntr = 0;
            foreach (string str in descr)
            {
                cntr++;
                if (str.Length > 80)
                    tmpstr += "\n (№" + cntr + ") " + str.Replace(' ', (char)9679);
            }
            if (!string.IsNullOrEmpty(tmpstr)) //Превышение 80 символов в строке
            {
                errors.Add(
                    new ZoneError(r.VNum, "Строка длиннее 80 символов",
                                   "Комната " + r + ", Параметр : " + paramName + " >> Строки: " + tmpstr, ErrSourceTypes.Комнаты, ActionType.GoToRoom));
            }
        }

        private void ValidateObjects()
        {
            foreach (Obj obj in zoneDataManager.Objects)
            {
                if (obj.CurrDurab > 750 ) //Превышение прочности
                {
                    errors.Add(
                        new ZoneError(obj.VNum, "Превышение прочности предмета", "Предмет : " + obj + "\nПрочность : " + obj.CurrDurab,
                                       ErrSourceTypes.Предметы, ParseMessageType.Ошибка, ActionType.GoToObject));
                }
                if (obj.Type != 12 && string.IsNullOrEmpty(obj.WearFlags)) //Не указано куда можно одеть объект
                {
                    errors.Add(
                        new ZoneError(obj.VNum, "Не указано куда можно одеть предмет", "Предмет " + obj,
                                       ErrSourceTypes.Предметы, ParseMessageType.Ошибка, ActionType.GoToObject));
                }
                if (obj.MaxInWorld == -1)
                {
                    errors.Add(new ZoneError(obj.VNum,
                                              "Количество предметов данного типа в мире не ограничено", 
                                               "Предмет " + obj,
                                               ErrSourceTypes.Предметы, ParseMessageType.Предупреждение, ActionType.GoToObject));
                }
                else
                {
                    string tmp = ObjectsLoaded(obj);
                    if (!string.IsNullOrEmpty(tmp))
                        //Предметов данного типа в зоне установлено больше чем максимум в мире.
                    {
                        errors.Add(
                            new ZoneError(obj.VNum,
                                          "Предметов данного типа в зоне установлено больше чем максимум в мире",
                                          tmp, ErrSourceTypes.Предметы, ParseMessageType.Ошибка, ActionType.GoToObject));
                    }
                }
                if (obj.Timer > 21600) //Таймер предмета более 15 дней
                {
                    errors.Add(
                        new ZoneError(obj.VNum, "Таймер предмета более 15 дней", "Предмет " + obj,
                                       ErrSourceTypes.Предметы, ActionType.GoToObject));
                }
            }
        }

        /// <summary>
        /// Проверка превышения макс в мире
        /// </summary>
        /// <returns>Строка сообщения</returns>
        private string ObjectsLoaded(Object obj)
        {
            string res = string.Empty;
            int tcntr = 0;

            foreach (Room room in zoneDataManager.Rooms)
            {
                int cntr = 0;
                foreach (OperatedObj lo in room.LoadingObjectsCollection)
                {
                    if (lo.VNum == obj.VNum)
                        cntr++;
                }
                if (cntr > 0)
                {
                    res += "\n Локация: " + room + " (" + cntr + "шт.)";
                    tcntr += cntr;
                }

                foreach (OperatedMob lm in room.LoadingMobsCollection)
                {
                    cntr = 0;
                    foreach (MobObj mo in lm.Items)
                    {
                        if (mo.VNum == obj.VNum)
                            cntr++;
                    }
                    if (cntr > 0)
                    {
                        Mob curMob = allLoadedMobs[lm.VNum, 0];
                        string mobName = "[" + lm.VNum + "] из незагруженной зоны";
                        if (curMob != null)
                            mobName = curMob.ToString();
                        res += "\n Локация: " + room + " >> Моб: " + mobName + " (" + cntr + "шт.)";
                        tcntr += cntr;
                    }
                }

                foreach (OperatedObj lo in room.LoadingObjectsCollection)
                {
                    cntr = 0;
                    foreach (OperatedObj oio in lo.ObjectsInObject)
                    {
                        if (oio.VNum == obj.VNum)
                            cntr++;
                    }
                    if (cntr > 0)
                    {
                        Obj curCont = allLoadedObjects[lo.VNum, 0];
                        string contName = "[" + lo.VNum + "] из незагруженной зоны";
                        if (curCont != null)
                            contName = curCont.ToString();
                        res += "\n Локация: " + room + " >> Контейнер: " + contName + " (" + cntr + "шт.)";
                        tcntr += cntr;
                    }
                }
            }
            if (tcntr <= obj.MaxInWorld || obj.MaxInWorld == -1) 
                return string.Empty;
            return
                "Предметов типа " + obj + " (максимум в мире " + obj.MaxInWorld + " шт.) загружается " + tcntr +
                " шт.: " + res;
        }

        private void ValidateMobs()
        {
            foreach (Mob mob in zoneDataManager.Mobs)
            {
                //Слишком большое количество опыта получаемое за моба МОБ:такой-то ОПЫТ:такой-то (критерий генерации ошибки)
                if (mob.Exp > ConvertDices(mob.Hits) * 100)
                {
                    errors.Add(
                        new ZoneError(mob.VNum, "Слишком большое количество опыта получаемое за моба",
                                       "Моб " + mob + " Опыт: " + mob.Exp, ErrSourceTypes.Мобы, ActionType.GoToMob));
                }

                //Слишком большое количество денег получаемое за моба МОБ:такой-то ДЕНЕГ:формула (критерий генерации ошибки)
                if (ConvertDices(mob.Money) > mob.Level * 100)
                {
                    /*bool isShopkeeper = false;
                    foreach (Shop shp in _zoneDataManager.ShopsCollection)
                    {
                        if (shp.ShopkeeperVNum == mob.VNum)
                            isShopkeeper = true;
                    }
                    if (!isShopkeeper)*/
                    {
                        errors.Add(
                            new ZoneError(mob.VNum, "Слишком большое количество денег у моба",
                                           "Моб " + mob + " Максимум кун: " + mob.Money, ErrSourceTypes.Мобы, ActionType.GoToMob));
                    }
                }
                if (mob.Flags.Contains("q0")) //Установлен флаг !БАШ у моба
                {
                    errors.Add(
                        new ZoneError(mob.VNum, "Установлен флаг !БАШ у моба ", "Моб " + mob,
                                       ErrSourceTypes.Мобы, ParseMessageType.Информация, ActionType.GoToMob));
                }
                if (mob.Flags.Contains("e0")) //Установлен флаг !ЗАКОЛОТЬ у моба
                {
                    errors.Add(
                        new ZoneError(mob.VNum, "Установлен флаг !ЗАКОЛОТЬ у моба ", "Моб " + mob,
                                       ErrSourceTypes.Мобы, ParseMessageType.Информация, ActionType.GoToMob));
                }
                if (mob.Flags.Contains("t0")) //Установлен флаг !ХОЛД у моба
                {
                    errors.Add(
                        new ZoneError(mob.VNum, "Установлен флаг !ХОЛД у моба ", "Моб " + mob,
                                       ErrSourceTypes.Мобы, ParseMessageType.Информация, ActionType.GoToMob));
                }
                if (mob.Align == 600) //Выбрана наклонность СВЕТЛЫЙ у моба
                {
                    errors.Add(
                        new ZoneError(mob.VNum, "Выбрана наклонность СВЕТЛЫЙ у моба ", "Моб " + mob,
                                       ErrSourceTypes.Мобы, ParseMessageType.Информация, ActionType.GoToMob));
                }
                if (mob.Align == 0) //Выбрана наклонность НЕЙТРАЛЬНЫЙ у моба
                {
                    errors.Add(
                        new ZoneError(mob.VNum, "Выбрана наклонность НЕЙТРАЛЬНЫЙ у моба ", "Моб " + mob,
                                       ErrSourceTypes.Мобы, ParseMessageType.Информация, ActionType.GoToMob));
                }
                if (mob.DetailDescr.Length < 15) //Описание моба короче 15ти символов
                {
                    errors.Add(
                        new ZoneError(mob.VNum, "Детальное описание моба короче 15ти символов", "Моб " + mob,
                                       ErrSourceTypes.Мобы, ActionType.GoToMob));
                }
                //Убрано ибо хз что реально тут сравнивать...т.к. макс в мире свой для мобов загружаемых в разные комнаты
                /*string tmp = MobsLoaded(mob);
                if (!string.IsNullOrEmpty(tmp)) //Мобов данного типа в зоне установлено больше чем максимум в мире.
                {
                    errors.Add(
                        new CZoneError(mob.VNum, "Мобов данного типа в зоне установлено больше чем максимум в мире", tmp,
                                       ErrSourceTypes.Мобы, ParseMessageType.Ошибка, ActionType.GoToMob));
                }*/
            }
        }

        /*
                /// <summary>
                /// Проверка превышения макс в мире
                /// </summary>
                /// <returns>Строка сообщения</returns>
                private string MobsLoaded(CMob mob)
                {
                    string res = string.Empty;
                    int tcntr = 0;
                    foreach (CRoom room in DM.Rooms)
                    {
                        int cntr = 0;
                        foreach (CLoadedMob lm in room.LoadingMobsCollection)
                        {
                            if (lm.VNum == mob.VNum)
                                cntr++;
                        }
                        if (cntr > 0)
                        {
                            res += "\n Локация: " + room + " (" + cntr + "шт.)";
                            tcntr += cntr;
                        }
                    }
                    if (tcntr <= mob.MaxInWorld) return string.Empty;
                    return
                        "Мобов типа " + mob + " максимум в мире " + mob.MaxInWorld + " шт. загружается " + tcntr +
                        " шт.: " + res;
                }
        */

        private static readonly Regex TemlateConvertDices = new Regex("^(?<Param1>\\d+)\\w(?<Param2>\\d+)(?<sign>.)(?<ParamConst>\\d+)", RegexOptions.Compiled);

        private static double ConvertDices(string dices)
        {
            double res = 0;
            Match mval = TemlateConvertDices.Match(dices);
            if (mval.Success)
            {
                res = Convert.ToInt32(mval.Groups["Param1"].ToString().Trim());
                res = res * Convert.ToInt32(mval.Groups["Param2"].ToString().Trim());
                if (dices.IndexOf("+") > 0)
                    res += Convert.ToInt32(mval.Groups["ParamConst"].ToString().Trim());
                else
                    res -= Convert.ToInt32(mval.Groups["ParamConst"].ToString().Trim());
            }
            return res;
        }
    }
}

/*

Выбирать уровень отлавливаемых ошибок чтоб например не отлавливать пункт помеченный ***
И самое главное - по даблклику переход на соответствующую закладку и выбор моба в списке

//Мобы
Слишком большое количество опыта получаемое за моба МОБ:такой-то ОПЫТ:такой-то (критерий генерации ошибки)
Слишком большое количество денег получаемое за моба МОБ:такой-то ДЕНЕГ:формула (критерий генерации ошибки)
Установлен флаг !БАШ у моба НАЗВАНИЕ_МОБА
Установлен флаг !ЗАКОЛОТЬ у моба НАЗВАНИЕ_МОБА
Установлен флаг !ХОЛД у моба НАЗВАНИЕ_МОБА
Выбрана наклонность СВЕТЛЫЙ у моба НАЗВАНИЕ_МОБА
Выбрана наклонность НЕЙТРАЛЬНЫЙ у моба НАЗВАНИЕ_МОБА
Описание моба НАЗВАНИЕ_МОБА короче 15ти символов
Мобов типа МОБ в зоне установлено больше чем максимум в мире. Загружаются в комнатах: СПИСОК_КОМНАТ

//Комнаты
Непарный выход на НАПРАВЛЕНИЕ в комнате НАЗВАНИЕ_КОМНАТЫ
Описание менее трех строк в комнате НАЗВАНИЕ_КОМНАТЫ
В описании отсутствуют 3 вводных пробела в комнате НАЗВАНИЕ_КОМНАТЫ
В описании комнаты НАЗВАНИЕ_КОМНАТЫ длина строки превышает 80 символов

//Объекты
Не указано куда можно одеть объект НАЗВАНИЕ_ОБЪЕКТА (если у объекта тип "12" - Misc
	то его не показывать даже если не прописано куда надевается)
Предметов типа ОБЪЕКТ в зоне загружается в комнаты и выдается на руки мобам больше чем максимум в мире. Загружаются в комнатах/мобах: СПИСОК_КОМНАТ

Таймер объекта НАЗВАНИЕ_ОБЪЕКТА более 15 дней

//Общие
***Указанный триггер(комната, объект, моб и т.д.) отсутствует в загнруженных зонах
 
*/