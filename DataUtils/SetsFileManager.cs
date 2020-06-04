/*
 * ;СЕТОВЫЕ ПРЕДМЕТЫ И ИХ АКТИВАЦИИ

;Общие положения:
;Перед обработкой каждой строки из нее выбрасывается комментарий (;<текст комментария>)
;После этой операции файл может содержать пустые строки
;Далее начальные и конечные пробельные символы отбрасываются
;Значимая информация в строке должна иметь следующий формат:
;#<номер сета> или <тег>:<параметры>
;Между символом ?#? и номером сета не должно быть пробельных символов
;Каждый тег состоит из 4 символов, за ним обязательно должно следовать двоеточие
;Начальные пробельные символы после двоеточия отбрасываются
;Поле <параметры> не должно быть пусто
;
;Пояснение полей:
;#<номер сета> - номер сета пока нигде не используется, но они не могут повторяться
;Name:<имя сета> - имя сетапри. опознании пишется
;Vnum:<vnum предмета> - указывает, что объект с данными vnum?ом принадлежит сету
;Предмет может принадлежать лишь одному сету
;Oqty:<количество предметов> - количество предметов для активации
;Clss:<список профессий> или Clss:all ? список профессий, для которых предусмотрена данная активация (0 ? лекарь и т. д.)
;Если в списке указан номер NUM_CLASSES * NUM_KIN (на момент реализации он был равен 42), то активация также предусмотрена для мобов
;Если используется форма Clss:all, то активация предусмотрена для всех классов и для мобов
;Amsg:<сообщение> - сообщение чару при активации
;Dmsg:<сообщение> - сообщение чару при деактивации
;Ramg:<сообщение> - сообщение в комнату при активации
;Rdmg:<сообщение> - сообщение в комнату при деактивации
;Affs:<текст> - текстовое представление битвектора аффектов, таких как невидимость и т. п. (аналогично ОЛЦ)
;Afcn:<локация> <значение> - указывает какой стат на сколько изменить (аналогично дополнительным аффектам в ОЛЦ типа +сила и т. п.)
;Dice: <значение1> <значение2> - Средние повреждения оружия (значение1 d значение2) (если 0d0 - то не сохраняется)
;Wght:<значение> - Вес предмета (если 0 - то не сохраняется)
;Учитываются только первые 6 полей Afcn
;
;Формат файла:
;Файл состоит из сетовых блоков, каждый из которых начинается строкой:
;#<номер сета>
;Далее следует:
;Name:<имя сета>
;Далее следуют блоки Vnum (должен быть хотя бы один такой блок)
;Блок Vnum может быть пуст (это характерно для сетовых шмоток, у которых отсутствует активация)
;Блок Vnum, если он не пуст, состоит из блоков Oqty, которые характеризуют количество необходимых шмоток для активации
;Каждый блок Oqty должен содержать хотя бы один блок Clss
;Внутри одного блока Oqty блоки Clss не должны пересекаться по профессиям, ибо это означает, что для данного кол-ва предметов
;и данной профессии существует сразу несколько активаций
;Внутри каждого блока Clss находятся следующие поля, характеризующие активацию: Amsg, Dmsg, Ramg, Rdmg, Affs, Afcn
;Поля Amsg, Dmsg, Ramg и Rdmg могут присутствовать или отсутствовать только одновременно
;Поля Affs и Afcn могут присутствовать, а могут отсутствовать
;Dice: <значение1> <значение2> - Средние повреждения оружия (значение1 d значение2) (если 0d0 - то не сохраняется)
;Wght:<значение> - Вес предмета (если 0 - то не сохраняется)
;
;Замечания:
;1. Защиту и броню предмета можно изменять только через дополнительные аффекты (относится к ключам активации)
;2. Вес предмета и средние поврежденияф оружия можно менять
;3. Не ставить аффекты дыхание водой, полет, водохождение и т. п. на предметы, которые можно насильно снять с персонажа во время битвы,
;например, обезоружить, сбить точкой (относится к шлемам, оружию)

 */
namespace DataUtils
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    public class SetsFileManager : BaseFileManager
    {
        private int filePos;
        private string lastString;

        private string ReadLine(TextReader sr)
        {
            lastString = sr.ReadLine();
            filePos++;
            return lastString;
        }

        public bool Load(ObjsCollection objectsCollection, string zoneNumber, Encoding encoding)
        {
            filePos = 0;
            string additionalInfo = "";
            string filePath = StaticData.WorldFolderPath + @"\OBJ\" + zoneNumber + ".obj";
            if (!File.Exists(filePath))
                return true;
            var tnum = new Regex("#(?<Num>\\d+)");
            var tdigits = new Regex("^\\d+$");
            using (var sr = new StreamReader(filePath, encoding))
            {
                string input = "";
                try
                {
                    while (true)
                    {
                        additionalInfo = "отсутствует...";
                        var curObject = new Obj(-1);
                        while (input.IndexOf("#") == -1) //Смещаемся на начало описания объекта
                        {
                            input = ReadLine(sr);
                            if (input == null) break; //если конец файла, то прекращаем искать начало след.объекта
                        }
                        if (input == null) break; //если конец файла, прекращаем обработку файла

                        Match m = tnum.Match(input);
                        if (m.Success)
                            //строка - виртуальный номер предмета = Номер зоны * 100 + номер предмета в зоне - начинается с #
                        {
                            curObject = new Obj(Convert.ToInt32(m.Groups["Num"].ToString()));
                            additionalInfo = "объект [" + m.Groups["Num"] + "]";
                        }

                        curObject.Alias = ReadLine(sr).Replace("~", "");
                        //синонимы предмета - на какие названия он будет откликаться - заканчивается ~
                        curObject.Cases.Imen = ReadLine(sr).Replace("~", "");
                        //имя предмета в именительном падеже - заканчивается ~
                        additionalInfo += " " + curObject.Cases.Imen;
                        curObject.Cases.Rod = ReadLine(sr).Replace("~", "");
                        //имя предмета в родительном падеже - заканчивается ~
                        curObject.Cases.Dat = ReadLine(sr).Replace("~", "");
                        //имя предмета в дательном падеже - заканчивается ~
                        curObject.Cases.Vin = ReadLine(sr).Replace("~", "");
                        //имя предмета в винительном падеже - заканчивается ~
                        curObject.Cases.Tvor = ReadLine(sr).Replace("~", "");
                        //имя предмета в творительном падеже - заканчивается ~
                        curObject.Cases.Pred = ReadLine(sr).Replace("~", "");
                        //имя предмета в предложном падеже - заканчивается ~
                        curObject.Desc = ReadLine(sr).Replace("~", "");
                        //описание предмета, если он лежит в комнате - заканчивается ~
                        curObject.ActionDesc = "";
                        input = ReadLine(sr);
                        while (input != "~")
                        {
                            if (input.IndexOf("~") >= 0)
                            {
                                curObject.ActionDesc += input.Replace("~", "");
                                input = "~";
                            }
                            else
                            {
                                if (curObject.ActionDesc.Length > 0) curObject.ActionDesc += "\r\n";
                                curObject.ActionDesc += input;
                                input = ReadLine(sr);
                            }
                        }
                        input = ReadLine(sr);
                        string[] parts = input.Split(' ');
                        m = tdigits.Match(parts[0]);
                        if (parts[0] == "0")
                        {
                            curObject.TrenSkill = 0;
                            curObject.MagicFlags = "";
                        }
                        else if (m.Success) //Значит передано число
                        {
                            curObject.TrenSkill = Convert.ToInt32(parts[0]);
                            curObject.MagicFlags = "";
                        }
                        else //Передана строка
                        {
                            curObject.TrenSkill = 0;
                            curObject.MagicFlags = parts[0];
                        }

                        curObject.MaxDurab = Convert.ToInt32(parts[1]); //Максимальная прочность предмета
                        curObject.CurrDurab = Convert.ToInt32(parts[2]); //Текущая прочность предмета
                        curObject.Material = Convert.ToInt32(parts[3]); //Материал, из которого сделан предмет

                        input = ReadLine(sr);
                        parts = input.Split(' ');
                        curObject.Sex = Convert.ToInt32(parts[0]); //пол предмета				
                        curObject.Timer = Convert.ToInt32(parts[1]); //время жизни предмета в тиках
                        curObject.Spell = Convert.ToInt32(parts[2]); //спел, кастуемый предметом
                        curObject.SpellLevel = Convert.ToInt32(parts[3]); //уровень кастуемого спела

                        input = ReadLine(sr);
                        parts = input.Split(' ');
                        //if (Object.VNum == 74822) Debug.WriteLine("here");
                        curObject.Affects = parts[0] == "0" ? "" : parts[0];//аффекты, накладываемые при надевании предмета				
                        curObject.CantTouch = parts[1] == "0" ? "" : parts[1]; //флаги неудобств (не может одеть на себя)
                        curObject.CantUse = parts[2] == "0" ? "" : parts[2]; //флаги запретов (не может даже взять в руки)

                        input = ReadLine(sr);
                        parts = input.Split(' ');
                        curObject.Type = Convert.ToInt32(parts[0]); //Тип предмета	
                        /*if (Kind == "25" && TrenSkill != "0") //Для маг.ингр.
                         * такая же фигня нужна для контейнера для жидкости
                         * тут такая вот обработка:
                    MagicVector := '';
                    for i := 0 to 5 do
                     if (TrenSkill and (1 shl i)) <> 0 then //1 * 2^i
                      MagicVector := MagicVector + Chr(i+Ord('a')) + '0';
                    TrenSkill := 0;
                        {
                            MagicVector = "";
                            for (int i = 0; i<5; i++)
                            {
						
                            }
                        }*/
                        curObject.ExctraEffects = parts[1]; //экстрафлаги
                        curObject.WearFlags = parts[2]; //флаги, куда можно одеть

                        input = ReadLine(sr);
                        parts = input.Split(' ');
                        curObject.Param1 = parts[0];
                        /*Есть в коде старого редактора такая обработка на случай если флаги не числом а строкой
                        * Val(Param1, i, j);
                        if j <> 0 then
                        begin
                            j := 0;
                            for i := 1 to (Length(Param1) div 2) do 
                            j := j + (1 shl (Ord(Param1[i*2-1]) - Ord('a')));
                            Param1 := IntToStr(j);
                        end;*/
                        if (curObject.Type == 2 || curObject.Type == 3 || curObject.Type == 4 || curObject.Type == 10)
                            //для этих типов если не задан закл, то -1
                        {
                            curObject.Param2 = (parts[1] == "-1") ? "0" : parts[1];
                            curObject.Param3 = (parts[2] == "-1") ? "0" : parts[2];
                            curObject.Param4 = (parts[3] == "-1") ? "0" : parts[3];
                        }
                        else
                        {
                            curObject.Param2 = parts[1];
                            curObject.Param3 = parts[2];
                            curObject.Param4 = parts[3];
                        }
                        input = ReadLine(sr);
                        parts = input.Split(' ');
                        curObject.Weight = Convert.ToInt32(parts[0]);
                        curObject.Price = Convert.ToInt32(parts[1]);
                        curObject.RentInv = Convert.ToInt32(parts[2]);
                        curObject.RentWear = Convert.ToInt32(parts[3]);

                        input = ReadLine(sr);
                        while (input[0] == 'M')
                        {
                            parts = input.Split(' ');
                            curObject.MaxInWorld = Convert.ToInt32(parts[1]);
                            input = ReadLine(sr);
                        }

                        while (input.IndexOf("E") != -1)
                        {
                            string aliases = ReadLine(sr).Replace("~", ""); // ключевое слово - строка заканчиваемая ~
                            string extraDescTmp = "";
                            input = ReadLine(sr);
                            while (input != "~")
                            {
                                if (input.Length == 0)
                                {
                                    extraDescTmp += "\r\n";
                                    input = ReadLine(sr);
                                }
                                else
                                {
                                    if (input[0] == 'E')
                                    {
                                        extraDescTmp += input.Replace("~", "");
                                        input = "~";
                                    }
                                    else
                                    {
                                        if (extraDescTmp.Length > 0) extraDescTmp += "\r\n";
                                        extraDescTmp += input;
                                        input = ReadLine(sr);
                                    }
                                }
                            }
                            curObject.AddExtraDescription(aliases, extraDescTmp);
                            input = ReadLine(sr);
                        }
                        while (input[0] == 'A')
                        {
                            input = ReadLine(sr);
                            parts = input.Split(' ');
                            curObject.AddBonus(Convert.ToInt32(parts[0]), Convert.ToInt32(parts[1]));
                            input = ReadLine(sr);
                        }
                        while (input[0] == 'T')
                        {
                            if (input.Length > 1)
                            {
                                string[] p = input.Split(' ');
                                curObject.AddTrigger(Convert.ToInt32(p[1]));
                            }
                            else
                            {
                                input = ReadLine(sr);
                                curObject.AddTrigger(Convert.ToInt32(input.Replace("#", "")));
                            }
                            input = ReadLine(sr);
                        }
                        while (input[0] == 'S')
                        {
                            input = ReadLine(sr);
                            parts = input.Split(' ');
                            curObject.AddSkillBonus(Convert.ToInt32(parts[0]), Convert.ToInt32(parts[1]));
                            input = ReadLine(sr);
                        }
                        objectsCollection.Add(curObject);
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

        public void Save(ObjsCollection objectsCollection, string zoneNumber)
        {
            var fs =
                new FileStream(StaticData.WorldFolderPath + @"\OBJ\" + zoneNumber + ".obj", FileMode.Create,
                               FileAccess.Write);
            var sw = new StreamWriter(fs, StaticData.CurrentEncoding) {NewLine = "\n"};
            sw.WriteLine("* Сгенерировано BZEditor");
            sw.WriteLine("* Количество объектов : " + objectsCollection.Count);
            sw.WriteLine("* Сохранено " + DateTime.Now);

            if (objectsCollection.Count > 0)
            {
                objectsCollection.Sort(new BaseDataObjectComparer());
                foreach (Obj curObject in objectsCollection)
                {
                    sw.WriteLine("#" + curObject.VNum);
                    sw.WriteLine(curObject.Alias + "~");
                    sw.WriteLine(curObject.Cases.Imen + "~");
                    sw.WriteLine(curObject.Cases.Rod + "~");
                    sw.WriteLine(curObject.Cases.Dat + "~");
                    sw.WriteLine(curObject.Cases.Vin + "~");
                    sw.WriteLine(curObject.Cases.Tvor + "~");
                    sw.WriteLine(curObject.Cases.Pred + "~");
                    sw.WriteLine(curObject.Desc + "~");
                    string[] parts = curObject.ActionDesc.Replace("\r", "").TrimEnd('\n').Split('\n');
                    if (curObject.ActionDesc != "")
                    {
                        foreach (string s in parts)
                            sw.WriteLine(s);
                    }
                    sw.WriteLine("~");
                    //сохранять флаги или скилл
                    string tmpparam1 = (curObject.Type == 25) ? curObject.MagicFlags : curObject.TrenSkill.ToString();
                    sw.WriteLine(tmpparam1 + " " + curObject.MaxDurab + " " + curObject.CurrDurab + " " + curObject.Material);
                    sw.WriteLine(curObject.Sex + " " + curObject.Timer + " " + curObject.Spell + " " + curObject.SpellLevel);
                    tmpparam1 = (curObject.Affects == "") ? "0" : curObject.Affects;
                    string tmpparam2 = (curObject.CantTouch == "") ? "0" : curObject.CantTouch;
                    string tmpparam3 = (curObject.CantUse == "") ? "0" : curObject.CantUse;
                    /*13*/
                    sw.WriteLine(tmpparam1 + " " + tmpparam2 + " " + tmpparam3);
                    tmpparam1 = (curObject.ExctraEffects == "") ? "0" : curObject.ExctraEffects;
                    tmpparam2 = (curObject.WearFlags == "") ? "0" : curObject.WearFlags;
                    /*14*/
                    sw.WriteLine(curObject.Type + " " + tmpparam1 + " " + tmpparam2);
                    if (curObject.Type == 2 || curObject.Type == 3 || curObject.Type == 4 || curObject.Type == 10)
                    //для этих типов если не задан закл, то -1
                    {
                        tmpparam1 = (curObject.Param2 == "0") ? "-1" : curObject.Param2;
                        tmpparam2 = (curObject.Param3 == "0") ? "-1" : curObject.Param3;
                        tmpparam3 = (curObject.Param4 == "0") ? "-1" : curObject.Param4;
                        /*15*/
                        sw.WriteLine(curObject.Param1 + " " + tmpparam1 + " " + tmpparam2 + " " + tmpparam3);
                    }
                    else
                        /*15*/
                        sw.WriteLine(curObject.Param1 + " " + curObject.Param2 + " " + curObject.Param3 + " " + curObject.Param4);
                    sw.WriteLine(curObject.Weight + " " + curObject.Price + " " + curObject.RentInv + " " + curObject.RentWear);
                    sw.WriteLine("M " + curObject.MaxInWorld);
                    foreach (ExtraDesc ed in curObject.ExtraDescriptions)
                    {
                        sw.WriteLine("E");
                        sw.WriteLine(ed.Aliases + "~");
                        string[] edparts = ed.Description.Replace("\r", "").TrimEnd('\n').Split('\n');
                        foreach (string s in edparts)
                            sw.WriteLine(s);
                        sw.WriteLine("~");
                    }
                    foreach (Bonus b in curObject.BonusesCollection)
                    {
                        sw.WriteLine("A");
                        sw.WriteLine(b.VNum + " " + b.Value);
                    }
                    foreach (int t in curObject.TriggersList)
                        sw.WriteLine("T " + t);
                    foreach (Bonus b in curObject.SkillBonusesCollection)
                    {
                        sw.WriteLine("S");
                        sw.WriteLine(b.VNum + " " + b.Value);
                    }
                }
            }
            //if (objectsCollection.Count > 0)
            //{
            //    objectsCollection.Sort(new BaseDataObjectComparer());
            //    CObject Object = objectsCollection.GetFirst();
            //    int LastVnum = objectsCollection.GetLastVNum();
            //    bool finished = false;
            //    while (!finished)
            //    {
            //        sw.WriteLine("#" + Object.VNum);
            //        sw.WriteLine(Object.Alias + "~");
            //        sw.WriteLine(Object.Cases.Imen + "~");
            //        sw.WriteLine(Object.Cases.Rod + "~");
            //        sw.WriteLine(Object.Cases.Dat + "~");
            //        sw.WriteLine(Object.Cases.Vin + "~");
            //        sw.WriteLine(Object.Cases.Tvor + "~");
            //        sw.WriteLine(Object.Cases.Pred + "~");
            //        sw.WriteLine(Object.Desc + "~");
            //        string[] parts = Object.ActionDesc.Replace("\r", "").TrimEnd('\n').Split('\n');
            //        if (Object.ActionDesc != "")
            //        {
            //            foreach (string s in parts)
            //                sw.WriteLine(s);
            //        }
            //        sw.WriteLine("~");
            //        //сохранять флаги или скилл
            //        string tmpparam1 = (Object.Type == 25) ? Object.MagicFlags : Object.TrenSkill.ToString();
            //        sw.WriteLine(tmpparam1 + " " + Object.MaxDurab + " " + Object.CurrDurab + " " + Object.Material);
            //        sw.WriteLine(Object.Sex + " " + Object.Timer + " " + Object.Spell + " " + Object.SpellLevel);
            //        tmpparam1 = (Object.Affects == "") ? "0" : Object.Affects;
            //        string tmpparam2 = (Object.CantTouch == "") ? "0" : Object.CantTouch;
            //        string tmpparam3 = (Object.CantUse == "") ? "0" : Object.CantUse;
            //        /*13*/
            //        sw.WriteLine(tmpparam1 + " " + tmpparam2 + " " + tmpparam3);
            //        tmpparam1 = (Object.ExctraEffects == "") ? "0" : Object.ExctraEffects;
            //        tmpparam2 = (Object.WearFlags == "") ? "0" : Object.WearFlags;
            //        /*14*/
            //        sw.WriteLine(Object.Type + " " + tmpparam1 + " " + tmpparam2);
            //        if (Object.Type == 2 || Object.Type == 3 || Object.Type == 4 || Object.Type == 10)
            //            //для этих типов если не задан закл, то -1
            //        {
            //            tmpparam1 = (Object.Param2 == "0") ? "-1" : Object.Param2;
            //            tmpparam2 = (Object.Param3 == "0") ? "-1" : Object.Param3;
            //            tmpparam3 = (Object.Param4 == "0") ? "-1" : Object.Param4;
            //            /*15*/
            //            sw.WriteLine(Object.Param1 + " " + tmpparam1 + " " + tmpparam2 + " " + tmpparam3);
            //        }
            //        else
            //            /*15*/
            //            sw.WriteLine(Object.Param1 + " " + Object.Param2 + " " + Object.Param3 + " " + Object.Param4);
            //        sw.WriteLine(Object.Weight + " " + Object.Price + " " + Object.RentInv + " " + Object.RentWear);
            //        sw.WriteLine("M " + Object.MaxInWorld);
            //        foreach (CExtraDesc ed in Object.ExtraDescriptions)
            //        {
            //            sw.WriteLine("E");
            //            sw.WriteLine(ed.Aliases + "~");
            //            string[] edparts = ed.Description.Replace("\r", "").TrimEnd('\n').Split('\n');
            //            foreach (string s in edparts)
            //                sw.WriteLine(s);
            //            sw.WriteLine("~");
            //        }
            //        foreach (CBonus b in Object.BonusesCollection)
            //        {
            //            sw.WriteLine("A");
            //            sw.WriteLine(b.VNum + " " + b.Value);
            //        }
            //        foreach (int t in Object.TriggersList)
            //            sw.WriteLine("T " + t);
            //        if (Object.VNum < LastVnum)
            //            Object = objectsCollection.GetNext(Object.VNum);
            //        else
            //            finished = true;
            //    }
            //}
            sw.WriteLine("$");
            sw.WriteLine("$");
            sw.Close();
            fs.Close();
            sw.Dispose();
            fs.Dispose();
        }
    }
}