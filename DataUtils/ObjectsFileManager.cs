using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using SystemFrameworks;

namespace DataUtils
{
    public class ObjectsFileManager : BaseFileManager
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
                            if (input.IndexOf("~", StringComparison.Ordinal) >= 0)
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
                        //возможно сейчас уже тут пишется только число, а MagicFlags не пишется (уточнить бы)
                        if (parts[0] == "0")
                        {
                            curObject.TrenSkill = 0;
                            curObject.MagicFlags = "";
                        }
                        else if (StringUtils.IsUnsignedNumber(parts[0])) //Значит передано число
                        {
                            curObject.TrenSkill = StringUtils.ToIntFast(parts[0]);
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
                        curObject.Weight = StringUtils.ToIntFast(parts[0]);
                        curObject.Price = StringUtils.ToIntFast(parts[1]);
                        curObject.RentInv = StringUtils.ToIntFast(parts[2]);
                        curObject.RentWear = StringUtils.ToIntFast(parts[3]);

                        input = ReadLine(sr);
                        while (input[0] != '#' && input[0] != '$')
                        {
                            switch (input[0])
                            {
                                case 'M':
                                    parts = input.Split(' ');
                                    curObject.MaxInWorld = StringUtils.ToIntFast(parts[1]);
                                    break;
                                case 'R':
                                    parts = input.Split(' ');
                                    curObject.MinimumRemorts = StringUtils.ToIntFast(parts[1]);
                                    break;
                                case 'E':
                                    string aliases = ReadLine(sr).Replace("~", ""); // ключевое слово - строка заканчиваемая ~
                                    string extraDescTmp = "";
                                    input = ReadLine(sr);
                                    while (input != "~")
                                    {
										if (input.EndsWith("~"))
                                        {
                                            extraDescTmp += input.Replace("~", "");
                                            break;
                                        }
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
                                    break;
                                case 'A':
                                    input = ReadLine(sr);
                                    parts = input.Split(' ');
                                    curObject.AddBonus(StringUtils.ToIntFast(parts[0]), StringUtils.ToIntFast(parts[1]));
                                    break;
                                case 'S':
                                    input = ReadLine(sr);
                                    parts = input.Split(' ');
                                    curObject.AddSkillBonus(StringUtils.ToIntFast(parts[0]), StringUtils.ToIntFast(parts[1]));
                                    break;
                                case 'V':
                                    parts = input.Split(' ');
                                    if (parts.Length >= 3)
                                        curObject.ExtraValues[parts[1]] = StringUtils.ToIntFast(parts[2]);
                                    break;
                                case 'T':
                                    if (input.Length > 1)
                                    {
                                        string[] p = input.Split(' ');
                                        curObject.AddTrigger(StringUtils.ToIntFast(p[1]));
                                    }
                                    else
                                    {
                                        input = ReadLine(sr);
                                        curObject.AddTrigger(StringUtils.ToIntFast(input.Replace("#", "")));
                                    }
                                    break;
                            }
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
            //sw.WriteLine("* Сгенерировано BZEditor");
            //sw.WriteLine("* Количество объектов : " + objectsCollection.Count);
            //sw.WriteLine("* Сохранено " + DateTime.Now);

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
                    //сохранять флаги или скилл (надо ли это ?)
                    //string tmpparam1 = (curObject.Type == 25) ? curObject.MagicFlags : curObject.TrenSkill.ToString();
                    string tmpparam1 = curObject.TrenSkill.ToString();
                    sw.WriteLine(tmpparam1 + " " + curObject.MaxDurab + " " + curObject.CurrDurab + " " + curObject.Material);
                    sw.WriteLine(curObject.Sex + " " + curObject.Timer + " " + curObject.Spell + " " + curObject.SpellLevel);
                    tmpparam1 = (curObject.Affects == "") ? "0" : curObject.Affects;
                    string tmpparam2 = (curObject.CantTouch == "") ? "0" : curObject.CantTouch;
                    string tmpparam3 = (curObject.CantUse == "") ? "0" : curObject.CantUse;
                    /*13*/
                    sw.WriteLine(tmpparam1 + " " + tmpparam2 + " " + tmpparam3 + " ");
                    tmpparam1 = (curObject.ExctraEffects == "") ? "0" : curObject.ExctraEffects;
                    tmpparam2 = (curObject.WearFlags == "") ? "0" : curObject.WearFlags;
                    /*14*/
                    sw.WriteLine(curObject.Type + " " + tmpparam1 + " " + tmpparam2 + " ");
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
                    if (curObject.MaxInWorld != 0)
                        sw.WriteLine($"M {curObject.MaxInWorld}");
                    if (curObject.MinimumRemorts != 0)
                        sw.WriteLine($"R {curObject.MinimumRemorts}");
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
                    foreach (Bonus b in curObject.SkillBonusesCollection)
                    {
                        sw.WriteLine("S");
                        sw.WriteLine(b.VNum + " " + b.Value);
                    }
                    foreach (int t in curObject.TriggersList)
                        sw.WriteLine($"T {t}");
                    foreach (var ev in curObject.ExtraValues)
                        sw.WriteLine($"V {ev.Key} {ev.Value}");

                    curObject.Modifyed = false;
                }
            }
            sw.WriteLine("$");
            sw.WriteLine("$");
            sw.Close();
            fs.Close();
            sw.Dispose();
            fs.Dispose();
        }
    }
}