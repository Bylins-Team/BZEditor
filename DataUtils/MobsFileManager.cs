using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using SystemFrameworks;

namespace DataUtils
{
    public class MobsFileManager : BaseFileManager
    {
        private int filePos;
        private string lastString;

        private string ReadLine(TextReader sr)
        {
            lastString = sr.ReadLine();
            filePos++;
            return lastString;
        }

        public bool Load(MobsCollection mobsCollection, string zoneNumber, Encoding encoding)
        {
            filePos = 0;
            string additionalInfo = "";
            string filePath = StaticData.WorldFolderPath + @"\MOB\" + zoneNumber + ".mob";
            if (!File.Exists(filePath))
                return true;
            var tnum = new Regex("#(?<Num>\\d+)");
            //var tingr = new Regex("^I (?<Name>\\w+),(?<Str>\\d+):(?<Proc>\\d+)$");
            using (var sr = new StreamReader(filePath, encoding))
            {
                string input = "";
                //int CurMobNum = -1;
                try
                {
                    while (true)
                    {
                        additionalInfo = "отсутствует...";
                        var mob = new Mob(-1);

                        while (input.IndexOf("#", StringComparison.Ordinal) == -1) //Смещаемся на начало описания объекта
                        {
                            input = ReadLine(sr);
                            if (input == null) break; //если конец файла, то прекращаем искать начало след.объекта
                        }
                        if (input == null) break; //если конец файла, прекращаем обработку файла

                        Match m = tnum.Match(input);
                        if (m.Success)
                        {
                            mob = new Mob(StringUtils.ToIntFast(m.Groups["Num"].ToString()));
                            additionalInfo = "моб [" + m.Groups["Num"] + "]";
                        }
                        mob.Alias = ReadLine(sr).Replace("~", "");
                        mob.Cases.Imen = ReadLine(sr).Replace("~", "");
                        additionalInfo += " " + mob.Cases.Imen;
                        mob.Cases.Rod = ReadLine(sr).Replace("~", "");
                        mob.Cases.Dat = ReadLine(sr).Replace("~", "");
                        mob.Cases.Vin = ReadLine(sr).Replace("~", "");
                        mob.Cases.Tvor = ReadLine(sr).Replace("~", "");
                        mob.Cases.Pred = ReadLine(sr).Replace("~", "");
                        input = ReadLine(sr);
                        while (input != "~") //Читаем все описание моба до завершающей тильды
                        {
                            if (input.IndexOf("~", StringComparison.Ordinal) >= 0)
                            {
                                mob.Desc += input.Replace("~", "");
                                input = "~";
                            }
                            else
                            {
                                if (mob.Desc.Length > 0) mob.Desc += "\r\n";
                                mob.Desc += input;
                                input = ReadLine(sr);
                            }
                        }
                        input = ReadLine(sr);
                        while (input != "~") //Читаем все описание моба до завершающей тильды
                        {
                            if (input.IndexOf("~", StringComparison.Ordinal) >= 0)
                            {
                                mob.DetailDescr += input.Replace("~", "");
                                input = "~";
                            }
                            else
                            {
                                if (mob.DetailDescr.Length > 0) mob.DetailDescr += "\r\n";
                                mob.DetailDescr += input;
                                input = ReadLine(sr);
                            }
                        }

                        input = ReadLine(sr);
                        string[] parts = input.Split(' ');
                        mob.Flags = parts[0] == "0" ? "" : parts[0]; //флаги
                        mob.Affects = parts[1] == "0" ? "" : parts[1]; //аффекты
                        mob.Align = StringUtils.ToIntFast(parts[2]);

                        input = ReadLine(sr);
                        parts = input.Split(' ');
                        mob.Level = StringUtils.ToIntFast(parts[0]);
                        mob.Hitroll = 20-StringUtils.ToIntFast(parts[1]);//20-значение для того, чтоб отображать как в ОЛЦ
                        mob.Ac = StringUtils.ToIntFast(parts[2]);
                        mob.Hits = parts[3];
                        mob.Damage = parts[4];

                        input = ReadLine(sr);
                        parts = input.Split(' ');
                        mob.Money = parts[0];
                        mob.Exp = StringUtils.ToIntFast(parts[1]);
                        if (mob.Exp < 0) mob.Exp = 0;//Глюк в старом эдиторе, например  зона 114 моб 11400 с экспой -1 - ошибка

                        input = ReadLine(sr);
                        parts = input.Split(' ');
                        mob.PosLoad = StringUtils.ToIntFast(parts[0]);
                        mob.PosDefault = StringUtils.ToIntFast(parts[1]);
                        mob.Sex = StringUtils.ToIntFast(parts[2]);
                        mob.Speed = parts.Length > 3 ? StringUtils.ToIntFast(parts[3]) : -1;

                        input = ReadLine(sr);
                        while (input[0] != '#' && input[0] != '$') //Читаем все параметры моба
                        {
                            parts = input.Split(' ');
                            switch (input.Split(':')[0])
                            {
                                case "Saves":
                                    mob.SaveParalyzeCast = StringUtils.ToIntFast(parts[1]);
                                    mob.SaveMagBreathes = StringUtils.ToIntFast(parts[2]);
                                    mob.SaveMagDamages = StringUtils.ToIntFast(parts[3]);
                                    mob.SaveFightSkills = StringUtils.ToIntFast(parts[4]);
                                    break;
                                case "Resistances":
                                    mob.ResistFromFire = StringUtils.ToIntFast(parts[1]);
                                    mob.ResistFromAir = StringUtils.ToIntFast(parts[2]);
                                    mob.ResistFromWater = StringUtils.ToIntFast(parts[3]);
                                    mob.ResistFromEarth = StringUtils.ToIntFast(parts[4]);
                                    mob.Vitality = StringUtils.ToIntFast(parts[5]);
                                    mob.Mind = StringUtils.ToIntFast(parts[6]);
                                    mob.Immunitet = StringUtils.ToIntFast(parts[7]);
                                    if (parts.Length == 9) // 8й резист (от тьмы) не во всех зонах сохранен пока
                                        mob.ResistDark = StringUtils.ToIntFast(parts[8]);
                                    break;
                                case "HPreg":
                                    mob.HPreg = StringUtils.ToIntFast(parts[1]);
                                    break;
                                case "Armour":
                                    mob.Armour = StringUtils.ToIntFast(parts[1]);
                                    break;
                                case "PlusMem":
                                    mob.PlusMem = StringUtils.ToIntFast(parts[1]);
                                    break;
                                case "CastSuccess":
                                    mob.CastSuccess = StringUtils.ToIntFast(parts[1]);
                                    break;
                                case "Success":
                                    mob.Luck = StringUtils.ToIntFast(parts[1]);
                                    break;
                                case "Initiative":
                                    mob.Initiative = StringUtils.ToIntFast(parts[1]);
                                    break;
                                case "Absorbe":
                                    mob.Absorbe = StringUtils.ToIntFast(parts[1]);
                                    break;
                                case "AResist":
                                    mob.AResist = StringUtils.ToIntFast(parts[1]);
                                    break;
                                case "MResist":
                                    mob.MResist = StringUtils.ToIntFast(parts[1]);
                                    break;
                                case "PResist":
                                    mob.PResist = StringUtils.ToIntFast(parts[1]);
                                    break;
                                case "BareHandAttack":
                                    if (parts.Length == 2)
                                        mob.BareHandAttack = StringUtils.ToIntFast(parts[1]);
                                    else if (parts.Length == 3)
                                        // Клюк в зонах 1101 и еще каких то, ибо там схранено __0, _13 типо форматировано непонятно нахуя
                                        mob.BareHandAttack = StringUtils.ToIntFast(parts[2]);
                                    break;
                                case "Destination":
                                    mob.Destination.Add(StringUtils.ToIntFast(parts[1]));
                                    break;
                                case "Str":
                                    mob.Stats.Str = StringUtils.ToIntFast(parts[1]);
                                    break;
                                case "Int":
                                    mob.Stats.Int = StringUtils.ToIntFast(parts[1]);
                                    break;
                                case "Wis":
                                    mob.Stats.Wis = StringUtils.ToIntFast(parts[1]);
                                    break;
                                case "Dex":
                                    mob.Stats.Dex = StringUtils.ToIntFast(parts[1]);
                                    break;
                                case "Con":
                                    mob.Stats.Con = StringUtils.ToIntFast(parts[1]);
                                    break;
                                case "Cha":
                                    mob.Stats.Cha = StringUtils.ToIntFast(parts[1]);
                                    break;
                                case "LikeWork":
                                    mob.LikeWork = StringUtils.ToIntFast(parts[1]);
                                    break;
                                case "MaxFactor":
                                    mob.MaxFactor = StringUtils.ToIntFast(parts[1]);
                                    break;
                                case "ExtraAttack":
                                    mob.ExtraAttack = StringUtils.ToIntFast(parts[1]);
                                    break;
                                case "Class":
                                    mob.Class = StringUtils.ToIntFast(parts[1]);
                                    break;
                                case "Race":
                                    mob.Race = StringUtils.ToIntFast(parts[1]);
                                    break;
                                case "Size":
                                    mob.Stats.Size = StringUtils.ToIntFast(parts[1]);
                                    break;
                                case "Height":
                                    mob.Stats.Height = StringUtils.ToIntFast(parts[1]);
                                    break;
                                case "Weight":
                                    mob.Stats.Weight = StringUtils.ToIntFast(parts[1]);
                                    break;
                                case "Special_Bitvector":
                                    mob.SpecialBitvector = parts[1];
                                    break;
                                case "Spell":
                                    mob.Spells.AddIncr(StringUtils.ToIntFast(parts[1]));
                                    break;
                                case "Skill":
                                    mob.Skills.Add(StringUtils.ToIntFast(parts[1]), StringUtils.ToIntFast(parts[2]));
                                    break;
                                case "Helper":
                                    mob.Helpers.Add(StringUtils.ToIntFast(parts[1]));
                                    break;
                                case "Feat":
                                    mob.Feats.Add(StringUtils.ToIntFast(parts[1]));
                                    break;
                                case "Role":
                                    var cntr = parts[1].Length;
                                    foreach (char bit in parts[1])
                                    {
                                        if (bit == '1')
                                        {
                                            mob.Roles.Add(cntr);
                                        }
                                        cntr--;
                                    }
                                    break;

                            }
                            switch (parts[0])
                            {
                                case "L":
                                    mob.LoadedObjectAfterDeath.Add(StringUtils.ToIntFast(parts[1]), StringUtils.ToIntFast(parts[2]),
                                        StringUtils.ToIntFast(parts[3]), StringUtils.ToIntFast(parts[4]));
                                    break;
                                case "I"://I зубы,10:3 или I крылья:5
                                    parts = parts[1].Split(':');
                                    var prob = StringUtils.ToIntFast(parts[1]);
                                    parts = parts[0].Split(',');
                                    var typeName = parts[0];
                                    if (parts.Length == 2)
                                        mob.Ingredients.Add(typeName, StringUtils.ToIntFast(parts[1]), prob);
                                    else
                                        mob.Ingredients.Add(typeName, prob);
                                    break;
                                case "T":
                                    mob.TriggersList.Add(StringUtils.ToIntFast(parts[1]));
                                    break;
                            }
                            input = ReadLine(sr);
                        }                      

                        mobsCollection.Add(mob);
                    }
                }
                catch (Exception ex)
                {
                    FireExceptionEvent("Ошибка при загрузке мобов:\nФайл: \"" + filePath + "\"\nСтрока #" + filePos + ": " +
                            lastString + "\nДополнительная информация: " + additionalInfo, ex, EventLogEntryType.Warning);
                    sr.Close();
                    return false;
                }
                sr.Close();
                return true;
            }
        }

        public void Save(MobsCollection mobsCollection, string zoneNumber)
        {
            var fs =
                new FileStream(StaticData.WorldFolderPath + @"\MOB\" + zoneNumber + ".mob", FileMode.Create,
                               FileAccess.Write);
            var sw = new StreamWriter(fs, StaticData.CurrentEncoding) {NewLine = "\n"};
            //sw.WriteLine("* Сгенерировано BZEditor");
            //sw.WriteLine("* Количество мобов : " + mobsCollection.Count);
            //sw.WriteLine("* Сохранено " + DateTime.Now);
            if (mobsCollection.Count > 0)
            {
                mobsCollection.Sort(new BaseDataObjectComparer());
                Mob mob = mobsCollection.GetFirst();
                int lastMobVnum = mobsCollection.GetLastVNum();
                bool finished = false;
                while (!finished)
                {
                    sw.WriteLine("#" + mob.VNum);
                    sw.WriteLine(mob.Alias + "~");
                    sw.WriteLine(mob.Cases.Imen + "~");
                    sw.WriteLine(mob.Cases.Rod + "~");
                    sw.WriteLine(mob.Cases.Dat + "~");
                    sw.WriteLine(mob.Cases.Vin + "~");
                    sw.WriteLine(mob.Cases.Tvor + "~");
                    sw.WriteLine(mob.Cases.Pred + "~");
                    string[] parts = mob.Desc.Replace("\r", "").TrimEnd('\n').Split('\n'); //Описание
                    foreach (string s in parts)
                        sw.WriteLine(s);
                    sw.WriteLine("~");
                    parts = mob.DetailDescr.Replace("\r", "").TrimEnd('\n').Split('\n'); //Детальное описание
                    foreach (string s in parts)
                        sw.WriteLine(s);
                    sw.WriteLine("~");
                    string flags = (mob.Flags == "") ? "0" : mob.Flags;
                    string affects = (mob.Affects == "") ? "0" : mob.Affects;
                    sw.WriteLine(flags + " " + affects + " " + mob.Align + " E"); //Флаги, Аффекты, Наклонность                         
                    sw.WriteLine(mob.Level + " " +
                        (20 - mob.Hitroll) //Так сделано для того, чтоб хитролл отображался как в ОЛЦ
                        + " " + mob.Ac + " " + mob.Hits + " " + mob.Damage);//Уровень, Хитролл, АС, Хиты, Повреждения
                    sw.WriteLine(mob.Money + " " + mob.Exp); //Бабло, Экспа
                    sw.WriteLine(mob.PosLoad + " " + mob.PosDefault + " " + mob.Sex + (mob.Speed != -1 ? " " + mob.Speed : ""));//Позиция при загрузке, Позиция по умолчанию, Пол
                    //спас-броски: Если все спас-броски равны нулю, то строка не пишется.
                    if (mob.SaveParalyzeCast != 0 || mob.SaveMagBreathes != 0 || mob.SaveMagDamages != 0 ||
                        mob.SaveFightSkills != 0)
                    {
                        sw.WriteLine($"Saves: {mob.SaveParalyzeCast} {mob.SaveMagBreathes} {mob.SaveMagDamages} {mob.SaveFightSkills}");
                    }
                    //Резисты
                    if (mob.ResistFromFire != 0 || mob.ResistFromAir != 0 || mob.ResistFromWater != 0 ||
                        mob.ResistFromEarth != 0 || mob.Vitality != 0 || mob.Mind != 0 || mob.Immunitet != 0)
                    {
                        sw.WriteLine($"Resistances: {mob.ResistFromFire} {mob.ResistFromAir} {mob.ResistFromWater} {mob.ResistFromEarth} {mob.Vitality} {mob.Mind} {mob.Immunitet} {mob.ResistDark}");
                    }
                    if (mob.HPreg != 0)
                        sw.WriteLine($"HPreg: {mob.HPreg}"); //Регенерация  хитов
                    if (mob.Armour != 0)
                        sw.WriteLine($"Armour: {mob.Armour}"); //Броня
                    if (mob.PlusMem != 0)
                        sw.WriteLine($"PlusMem: {mob.PlusMem}"); //Бонус мема
                    if (mob.CastSuccess != 0)
                        sw.WriteLine($"CastSuccess: {mob.CastSuccess}"); //Успех каста
                    if (mob.Luck != 0)
                        sw.WriteLine($"Success: {mob.Luck}"); //Удача
                    if (mob.Initiative != 0)
                        sw.WriteLine($"Initiative: {mob.Initiative}"); //Инициатива
                    if (mob.Absorbe != 0)
                        sw.WriteLine($"Absorbe: {mob.Absorbe}"); //Поглощение
                    if (mob.AResist != 0)
                        sw.WriteLine($"AResist: {mob.AResist}"); //Имуннитет к маг.афф.
                    if (mob.MResist != 0)
                        sw.WriteLine($"MResist: {mob.MResist}"); //Имуннитет к маг.повр.
                    if (mob.PResist != 0)
                        sw.WriteLine($"PResist: {mob.PResist}"); //Имуннитет к физ.повр.
                    if (mob.BareHandAttack != 0)
                        sw.WriteLine($"BareHandAttack: {mob.BareHandAttack}"); //Тип атаки
                    foreach (int d in mob.Destination)
                        sw.WriteLine($"Destination: {d}");
                    if (mob.Stats.Str != 11)
                        sw.WriteLine($"Str: {mob.Stats.Str}"); //Сила
                    if (mob.Stats.Dex != 11)
                        sw.WriteLine($"Dex: {mob.Stats.Dex}"); //Ловкость
                    if (mob.Stats.Int != 11)
                        sw.WriteLine($"Int: {mob.Stats.Int}"); //Интеллект
                    if (mob.Stats.Wis != 11)
                        sw.WriteLine($"Wis: {mob.Stats.Wis}"); //Мудрость
                    if (mob.Stats.Con != 11)
                        sw.WriteLine($"Con: {mob.Stats.Con}"); //Тело
                    if (mob.Stats.Cha != 11)
                        sw.WriteLine($"Cha: {mob.Stats.Cha}"); //Обаяние
                    //if (mob.Stats.Size != 50) // (пока решили выводить все размеры, включая дефолтные)
                    sw.WriteLine($"Size: {mob.Stats.Size}"); //Размер 
                    if (mob.LikeWork > 0)
                        sw.WriteLine($"LikeWork: {mob.LikeWork}"); //
                    if (mob.MaxFactor > 0)
                        sw.WriteLine($"MaxFactor: {mob.MaxFactor}"); //
                    if (mob.ExtraAttack > 0)
                        sw.WriteLine($"ExtraAttack: {mob.ExtraAttack}"); //
                    sw.WriteLine($"Class: {mob.Class}"); //Класс
                    sw.WriteLine($"Race: {mob.Race}"); //Тип
                    sw.WriteLine($"Height: {mob.Stats.Height}"); //Рост
                    sw.WriteLine($"Weight: {mob.Stats.Weight}"); //Вес
                    if (mob.SpecialBitvector != "")
                        sw.WriteLine($"Special_Bitvector: {mob.SpecialBitvector} "); //
                    foreach (int f in mob.Feats)
                        sw.WriteLine($"Feat: {f}");
                    foreach (MobSkill s in mob.Skills)
                        sw.WriteLine($"Skill: {s.VNum} {s.Percent}");
                    foreach (MobSpell s in mob.Spells)
                    {
                        for (int i = 0; i < s.Count; i++)
                            sw.WriteLine($"Spell: {s.VNum}");
                    }

                    foreach (int h in mob.Helpers)
                        sw.WriteLine($"Helper: {h}");

                    var bitVectorRoles = 0;
                    foreach (int r in mob.Roles)
                    {
                        bitVectorRoles += Convert.ToInt32(Math.Pow(2, r - 1));
                    }
                    if (bitVectorRoles > 0)
                    {
                        string roles = BinaryUtils.NumberToBinary(bitVectorRoles, 9);
                        sw.WriteLine($"Role: {roles}");
                    }
                    sw.WriteLine("E");
                    foreach (LoadedObjAfterDeath o in mob.LoadedObjectAfterDeath)
                        sw.WriteLine($"L {o.VNum} {o.LoadProb} {o.LoadType} {o.SpecParam}");
                    foreach (var t in mob.TriggersList)
                        sw.WriteLine($"T {t}");
                    foreach (Ingredient ingr in mob.Ingredients)
                        if (ingr.PowerAuto)
                            sw.WriteLine($"I {ingr.TypeName}:{ingr.Probability}");
                        else
                            sw.WriteLine($"I {ingr.TypeName},{ingr.Power}:{ingr.Probability}");

                    mob.Modifyed = false;
                    
                    if (mob.VNum < lastMobVnum)
                    {
                        mob = mobsCollection.GetNext(mob.VNum);
                    }
                    else
                        finished = true;
                }
            }
            sw.WriteLine("$");
            sw.Close();
            fs.Close();
            sw.Dispose();
            fs.Dispose();
        }
    }
}