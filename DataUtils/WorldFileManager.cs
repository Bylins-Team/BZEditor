using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using SystemFrameworks;

namespace DataUtils
{
    public class WorldFileManager : BaseFileManager
    {
        private int filePos;
        private string lastString;

        private string ReadLine(TextReader sr)
        {
            lastString = sr.ReadLine();
            filePos++;
            return lastString;
        }

        public bool Load(RoomsCollection roomsCollection, string zoneNumber, Encoding encoding)
        {
            filePos = 0;
            string additionalInfo = "";
            string filePath = StaticData.WorldFolderPath + @"\WLD\" + zoneNumber + ".wld";
            if (!File.Exists(filePath))
                return true;
            var tnum = new Regex("#(?<Num>\\d+)");
            var tdescday = new Regex("<day>(?<data>.+)<day>", RegexOptions.Singleline);
            var tdescnight = new Regex("<night>(?<data>.+)<night>", RegexOptions.Singleline);
            var tdescwinternight = new Regex("<winternight>(?<data>.+)<winternight>", RegexOptions.Singleline);
            var tdescwinterday = new Regex("<winterday>(?<data>.+)<winterday>", RegexOptions.Singleline);
            var tdescspringnight = new Regex("<springnight>(?<data>.+)<springnight>", RegexOptions.Singleline);
            var tdescspringday = new Regex("<springday>(?<data>.+)<springday>", RegexOptions.Singleline);
            var tdescsummernight = new Regex("<summernight>(?<data>.+)<summernight>", RegexOptions.Singleline);
            var tdescsummerday = new Regex("<summerday>(?<data>.+)<summerday>", RegexOptions.Singleline);
            var tdescautumnnight = new Regex("<autumnnight>(?<data>.+)<autumnnight>", RegexOptions.Singleline);
            var tdescautumnday = new Regex("<autumnday>(?<data>.+)<autumnday>", RegexOptions.Singleline);
            using (var sr = new StreamReader(filePath, encoding))
            {
                string input = "";
                try
                {
                    while (true)
                    {
                        additionalInfo = "отсутствует...";
                        var room = new Room(-1);

                        var desc = new [] {"", "", "", "", "", ""};
                        var aliases = new [] {"", "", "", "", "", ""};
                        var exitNamesVin = new [] {"", "", "", "", "", ""};
                        var exitFlags = new [] {"", "", "", "", "", ""};
                        var keys = new [] {"", "", "", "", "", ""};
                        var lockLevel = new [] {"", "", "", "", "", ""};
                        var exitRooms = new [] {"", "", "", "", "", ""};

                        while (input.IndexOf("#", StringComparison.Ordinal) == -1) //Смещаемся на начало описания объекта
                        {
                            input = ReadLine(sr);
                            if (input == null) break; //если конец файла, то прекращаем искать начало след.объекта
                        }
                        if (input == null) break; //если конец файла, прекращаем обработку файла

                        Match m = tnum.Match(input);
                        if (m.Success)
                        {
                            room = new Room(Convert.ToInt32(m.Groups["Num"].ToString()));
                            additionalInfo = "комната [" + m.Groups["Num"] + "]";
                        }
                        room.Name = ReadLine(sr).Replace("~", "");
                        additionalInfo += " " + room.Name;

                        string fullDesc = "";
                        input = ReadLine(sr);
                        while (input != "~") //Читаем все описание зоны до завершающей тильды
                        {
                            if (input.IndexOf("~", StringComparison.Ordinal) >= 0)
                            {
                                fullDesc += input.Replace("~", "");
                                input = "~";
                            }
                            else
                            {
                                if (fullDesc.Length > 0) fullDesc += "\r\n";
                                fullDesc += input;
                                input = ReadLine(sr);
                            }
                        }
                        if (fullDesc.IndexOf("<", StringComparison.Ordinal) >= 0)
                        {
                            if (fullDesc.IndexOf("<", StringComparison.Ordinal) > 1)
                                room.Description.Main = fullDesc.Substring(0, fullDesc.IndexOf("<") - 1);

                            m = tdescday.Match(fullDesc);
                            room.Description.Day = m.Groups["data"].ToString();
                            if (room.Description.Day.Length > 0)
                            {
                                if (room.Description.Day[0] == 'R')
                                {
                                    room.Description.DayR = true;
                                    room.Description.Day = room.Description.Day.Remove(0, 1);
                                }
                            }

                            m = tdescnight.Match(fullDesc);
                            room.Description.Night = m.Groups["data"].ToString();
                            if (room.Description.Night.Length > 0)
                            {
                                if (room.Description.Night[0] == 'R')
                                {
                                    room.Description.NightR = true;
                                    room.Description.Night = room.Description.Night.Remove(0, 1);
                                }
                            }

                            m = tdescwinternight.Match(fullDesc);
                            room.Description.WinterNight = m.Groups["data"].ToString();
                            if (room.Description.WinterNight.Length > 0)
                            {
                                if (room.Description.WinterNight[0] == 'R')
                                {
                                    room.Description.WinterNightR = true;
                                    room.Description.WinterNight = room.Description.WinterNight.Remove(0, 1);
                                }
                            }

                            m = tdescwinterday.Match(fullDesc);
                            room.Description.WinterDay = m.Groups["data"].ToString();
                            if (room.Description.WinterDay.Length > 0)
                            {
                                if (room.Description.WinterDay[0] == 'R')
                                {
                                    room.Description.WinterDayR = true;
                                    room.Description.WinterDay = room.Description.WinterDay.Remove(0, 1);
                                }
                            }

                            m = tdescspringnight.Match(fullDesc);
                            room.Description.SpringNight = m.Groups["data"].ToString();
                            if (room.Description.SpringNight.Length > 0)
                            {
                                if (room.Description.SpringNight[0] == 'R')
                                {
                                    room.Description.SpringNightR = true;
                                    room.Description.SpringNight = room.Description.SpringNight.Remove(0, 1);
                                }
                            }

                            m = tdescspringday.Match(fullDesc);
                            room.Description.SpringDay = m.Groups["data"].ToString();
                            if (room.Description.SpringDay.Length > 0)
                            {
                                if (room.Description.SpringDay[0] == 'R')
                                {
                                    room.Description.SpringDayR = true;
                                    room.Description.SpringDay = room.Description.SpringDay.Remove(0, 1);
                                }
                            }

                            m = tdescsummernight.Match(fullDesc);
                            room.Description.SummerNight = m.Groups["data"].ToString();
                            if (room.Description.SummerNight.Length > 0)
                            {
                                if (room.Description.SummerNight[0] == 'R')
                                {
                                    room.Description.SummerNightR = true;
                                    room.Description.SummerNight = room.Description.SummerNight.Remove(0, 1);
                                }
                            }

                            m = tdescsummerday.Match(fullDesc);
                            room.Description.SummerDay = m.Groups["data"].ToString();
                            if (room.Description.SummerDay.Length > 0)
                            {
                                if (room.Description.SummerDay[0] == 'R')
                                {
                                    room.Description.SummerDayR = true;
                                    room.Description.SummerDay = room.Description.SummerDay.Remove(0, 1);
                                }
                            }

                            m = tdescautumnnight.Match(fullDesc);
                            room.Description.AutumnNight = m.Groups["data"].ToString();
                            if (room.Description.AutumnNight.Length > 0)
                            {
                                if (room.Description.AutumnNight[0] == 'R')
                                {
                                    room.Description.AutumnNightR = true;
                                    room.Description.AutumnNight = room.Description.AutumnNight.Remove(0, 1);
                                }
                            }

                            m = tdescautumnday.Match(fullDesc);
                            room.Description.AutumnDay = m.Groups["data"].ToString();
                            if (room.Description.AutumnDay.Length > 0)
                            {
                                if (room.Description.AutumnDay[0] == 'R')
                                {
                                    room.Description.AutumnDayR = true;
                                    room.Description.AutumnDay = room.Description.AutumnDay.Remove(0, 1);
                                }
                            }
                        }
                        else
                            room.Description.Main = fullDesc;

                        input = ReadLine(sr);
                        string[] parts = input.Replace("  ", " ").Split(' '); //Replace фиксит баг ОЛС
                        room.ZoneNum = Convert.ToInt32(parts[0]); //Номер зоны

                        room.Flags = parts[1] == "0" ? "" : parts[1]; //флаги комнаты
                        room.SectorType = -1;
                        if (parts.Length == 3) //Если параметров 2 то формат файла старый
                            room.SectorType = parts[2].Length > 0 ? Convert.ToInt32(parts[2]) : -1; //тип сектора

                        input = ReadLine(sr);
                        while (input[0].ToString() == "D") //Читаем выходы
                        {
                            int exitIndex = Convert.ToInt32(input[1].ToString());
                            //Полоучаем индекс направления c 0 до 5 (север восток юг запад вверх вниз)
                            input = ReadLine(sr);
                            while (input != "~") //Читаем все описание до завершающей тильды
                            {
                                if (input.IndexOf("~", StringComparison.Ordinal) >= 0)
                                {
                                    desc[exitIndex] += input.Replace("~", "");
                                    input = "~";
                                }
                                else
                                {
                                    if (desc[exitIndex].Length > 0) desc[exitIndex] += "\r\n";
                                    desc[exitIndex] += input;
                                    input = ReadLine(sr);
                                }
                            }
                            //Desc[exitIndex] = ReadLine(sr).Replace("~","");
                            string[] exitnameparts = ReadLine(sr).Replace("~", "").Split('|');
                            if (exitnameparts.Length > 0)
                                aliases[exitIndex] = exitnameparts[0];
                            if (exitnameparts.Length > 1)
                                exitNamesVin[exitIndex] = exitnameparts[1];
                            input = ReadLine(sr); //Последняя строка описания выхода
                            parts = input.Split(' ');
                            exitFlags[exitIndex] = parts[0];
                            keys[exitIndex] = parts[1];
                            exitRooms[exitIndex] = parts[2];
                            if (parts.Length == 4) //В старых зонах не было параметра "сложность замка"
                                lockLevel[exitIndex] = parts[3];
                            input = ReadLine(sr);
                        }
                        room.ExitNorth.Description = desc[0];
                        room.ExitEast.Description = desc[1];
                        room.ExitSouth.Description = desc[2];
                        room.ExitWest.Description = desc[3];
                        room.ExitUp.Description = desc[4];
                        room.ExitDown.Description = desc[5];

                        room.ExitNorth.Aliases = aliases[0];
                        room.ExitEast.Aliases = aliases[1];
                        room.ExitSouth.Aliases = aliases[2];
                        room.ExitWest.Aliases = aliases[3];
                        room.ExitUp.Aliases = aliases[4];
                        room.ExitDown.Aliases = aliases[5];

                        room.ExitNorth.ExinNameVin = exitNamesVin[0];
                        room.ExitEast.ExinNameVin = exitNamesVin[1];
                        room.ExitSouth.ExinNameVin = exitNamesVin[2];
                        room.ExitWest.ExinNameVin = exitNamesVin[3];
                        room.ExitUp.ExinNameVin = exitNamesVin[4];
                        room.ExitDown.ExinNameVin = exitNamesVin[5];

                        room.ExitNorth.ExitFlag = exitFlags[0] != "" ? Convert.ToInt32(exitFlags[0]) : 0;
                        room.ExitEast.ExitFlag = exitFlags[1] != "" ? Convert.ToInt32(exitFlags[1]) : 0;
                        room.ExitSouth.ExitFlag = exitFlags[2] != "" ? Convert.ToInt32(exitFlags[2]) : 0;
                        room.ExitWest.ExitFlag = exitFlags[3] != "" ? Convert.ToInt32(exitFlags[3]) : 0;
                        room.ExitUp.ExitFlag = exitFlags[4] != "" ? Convert.ToInt32(exitFlags[4]) : 0;
                        room.ExitDown.ExitFlag = exitFlags[5] != "" ? Convert.ToInt32(exitFlags[5]) : 0;

                        room.ExitNorth.Key = keys[0].Length > 0 ? Convert.ToInt32(keys[0]) : -1;
                        room.ExitEast.Key = keys[1].Length > 0 ? Convert.ToInt32(keys[1]) : -1;
                        room.ExitSouth.Key = keys[2].Length > 0 ? Convert.ToInt32(keys[2]) : -1;
                        room.ExitWest.Key = keys[3].Length > 0 ? Convert.ToInt32(keys[3]) : -1;
                        room.ExitUp.Key = keys[4].Length > 0 ? Convert.ToInt32(keys[4]) : -1;
                        room.ExitDown.Key = keys[5].Length > 0 ? Convert.ToInt32(keys[5]) : -1;

                        room.ExitNorth.RoomVNum = exitRooms[0].Length > 0 ? Convert.ToInt32(exitRooms[0]) : -1;
                        room.ExitEast.RoomVNum = exitRooms[1].Length > 0 ? Convert.ToInt32(exitRooms[1]) : -1;
                        room.ExitSouth.RoomVNum = exitRooms[2].Length > 0 ? Convert.ToInt32(exitRooms[2]) : -1;
                        room.ExitWest.RoomVNum = exitRooms[3].Length > 0 ? Convert.ToInt32(exitRooms[3]) : -1;
                        room.ExitUp.RoomVNum = exitRooms[4].Length > 0 ? Convert.ToInt32(exitRooms[4]) : -1;
                        room.ExitDown.RoomVNum = exitRooms[5].Length > 0 ? Convert.ToInt32(exitRooms[5]) : -1;

                        room.ExitNorth.LockLevel = lockLevel[0].Length > 0 ? Convert.ToInt32(lockLevel[0]) : 0;
                        room.ExitEast.LockLevel = lockLevel[1].Length > 0 ? Convert.ToInt32(lockLevel[1]) : 0;
                        room.ExitSouth.LockLevel = lockLevel[2].Length > 0 ? Convert.ToInt32(lockLevel[2]) : 0;
                        room.ExitWest.LockLevel = lockLevel[3].Length > 0 ? Convert.ToInt32(lockLevel[3]) : 0;
                        room.ExitUp.LockLevel = lockLevel[4].Length > 0 ? Convert.ToInt32(lockLevel[4]) : 0;
                        room.ExitDown.LockLevel = lockLevel[5].Length > 0 ? Convert.ToInt32(lockLevel[5]) : 0;

                        while (input[0] != '#' && input[0] != '$')
                        {
                            switch (input[0])
                            {
                                case 'E': //Читаем дополнения (Extra)
                                    string aliasesTmp = ReadLine(sr).Replace("~", "");
                                    string extraDesc = "";
                                    input = ReadLine(sr);
                                    while (input != "~") //Читаем все до завершающей тильды
                                    {
                                        if (input.IndexOf("~", StringComparison.Ordinal) >= 0)
                                        {
                                            extraDesc += input.Replace("~", "");
                                            input = "~";
                                        }
                                        else
                                        {
                                            if (extraDesc.Length > 0) extraDesc += "\r\n";
                                            extraDesc += input;
                                            input = ReadLine(sr);
                                        }
                                    }
                                    room.AddExtraDescription(aliasesTmp, extraDesc);
                                    break;
                                case 'S': //Смещаемся
                                    break;
                                case 'T': //Читаем триггеры комнаты
                                    var triggerVNum = input.Split(' ')[1];
                                    room.TriggersList.Add(StringUtils.ToIntFast(triggerVNum));
                                    break;
                                case 'I': //Читаем триггеры комнаты
                                    parts = input.Split(' ');
                                    parts = parts[1].Split(':');
                                    var prob = StringUtils.ToIntFast(parts[1]);
                                    parts = parts[0].Split(',');
                                    var typeName = parts[0];
                                    if (parts.Length == 2)
                                        room.Ingredients.Add(typeName, StringUtils.ToIntFast(parts[1]), prob);
                                    else
                                        room.Ingredients.Add(typeName, prob);
                                    break;
                            }

                            input = ReadLine(sr);
                        }                        
                        roomsCollection.Add(room);
                    }
                }
                catch (Exception ex)
                {
                    sr.Close();
                    FireExceptionEvent("Ошибка при загрузке комнат:\nФайл: \"" + filePath + "\"\nСтрока #" + filePos + ": " +
                            lastString + "\nДополнительная информация: " + additionalInfo, ex, EventLogEntryType.Warning);
                    return false;
                }
                sr.Close();
            }
            //Загрузка map-файла
            filePos = 0;
            filePath = StaticData.WorldFolderPath + @"\WLD\" + zoneNumber + ".map";
            if (!File.Exists(filePath))
                return true;
            using (var sr = new StreamReader(filePath, encoding))
            {
                try
                {
                    string input;
                    while ((input = ReadLine(sr)) != null)
                    {
                        additionalInfo = "отсутствует...";
                        if (input[0] == '*') continue;
                        //Match mval = t.Match(input);
                        string[] parts = input.Split(' ');
                        if (parts.Length == 4)
                        {
                            int vnum = Convert.ToInt32(parts[0].Trim());
                            Room room = roomsCollection[vnum, 0];
                            additionalInfo = "комната номер [" + vnum + "]";
                            if (room != null)
                            {
                                room.X = Convert.ToInt32(parts[1].Trim());
                                room.Y = Convert.ToInt32(parts[2].Trim());
                                room.Z = Convert.ToInt32(parts[3].Trim());
                                room.PlacedOnMap = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    sr.Close();
                    FireExceptionEvent("Ошибка при загрузке карты:\nФайл: \"" + filePath + "\"\nСтрока #" + filePos + ": " +
                            lastString + "\nДополнительная информация: " + additionalInfo, ex, EventLogEntryType.Warning);
                }
                sr.Close();
                return true;
            }
        }

        public void Save(RoomsCollection roomsCollection, string zoneNumber)
        {
            var fswld =
                new FileStream(StaticData.WorldFolderPath + @"\WLD\" + zoneNumber + ".wld", FileMode.Create,
                               FileAccess.Write);
            var swwld = new StreamWriter(fswld, StaticData.CurrentEncoding) {NewLine = "\n"};
            var fsmap =
                new FileStream(StaticData.WorldFolderPath + @"\WLD\" + zoneNumber + ".map", FileMode.Create,
                               FileAccess.Write);
            var swmap = new StreamWriter(fsmap, StaticData.CurrentEncoding);
            //swwld.WriteLine("* Сгенерировано BZEditor");
            //swwld.WriteLine("* Количество комнат : " + roomsCollection.Count);
            //swwld.WriteLine("* Сохранено " + DateTime.Now);

            /*if (roomsCollection.Count > 0)
            {
                CRoom room = roomsCollection.GetFirst();
                int lastVnum = roomsCollection.GetLastVNum();
                bool finished = false;
                while (!finished)
                {
                    if (room.VNum < lastVnum)
                        room = roomsCollection.GetNext(room.VNum);
                    else
                        finished = true;
                }
            }*/

            foreach (Room room in roomsCollection)
            {
                if (room.PlacedOnMap)
                    swmap.WriteLine(room.VNum + " " + room.X + " " + room.Y + " " + room.Z);

                swwld.WriteLine("#" + room.VNum);
                swwld.WriteLine(room.Name + "~");

                #region Сохранение опиcаний комнаты

                /*string[] parts1 = room.Description.Main.Replace("\r", "").TrimEnd('\n').Split('\n');
                for (int i = parts1.Length - 1; i > 0; i--)
                {
                    if (parts1[i] != "")
                        i = 0;
                    else
                        parts1[i] = "#IGNORETHISLINE";
                }
                foreach (string s in parts1)
                {
                    if (s != "#IGNORETHISLINE")
                        swwld.WriteLine(s);
                }*/
                //swwld.WriteLine("");
                WriteRoomDesc(swwld, room.Description.Main, false, "");
                WriteRoomDesc(swwld, room.Description.Day, room.Description.DayR, "<day>");
                WriteRoomDesc(swwld, room.Description.Night, room.Description.NightR, "<night>");
                WriteRoomDesc(swwld, room.Description.WinterDay, room.Description.WinterDayR, "<winterday>");
                WriteRoomDesc(swwld, room.Description.WinterNight, room.Description.WinterNightR, "<winternight>");
                WriteRoomDesc(swwld, room.Description.SpringDay, room.Description.SpringDayR, "<springday>");
                WriteRoomDesc(swwld, room.Description.SpringNight, room.Description.SpringNightR, "<springnight>");
                WriteRoomDesc(swwld, room.Description.SummerDay, room.Description.SummerDayR, "<summerday>");
                WriteRoomDesc(swwld, room.Description.SummerNight, room.Description.SummerNightR, "<summernight>");
                WriteRoomDesc(swwld, room.Description.AutumnDay, room.Description.AutumnDayR, "<autumnday>");
                WriteRoomDesc(swwld, room.Description.AutumnNight, room.Description.AutumnNightR, "<autumnnight>");

                swwld.WriteLine("~");

                #endregion

                string flags = room.Flags == "" ? "0" : room.Flags;
                string sectortype = room.SectorType == -1 ? "" : " " + room.SectorType;
                swwld.WriteLine(room.ZoneNum + " " + flags + " " + sectortype);

                WriteRoomExit(swwld, room.ExitNorth, Direction.North);
                WriteRoomExit(swwld, room.ExitEast, Direction.East);
                WriteRoomExit(swwld, room.ExitSouth, Direction.South);
                WriteRoomExit(swwld, room.ExitWest, Direction.West);
                WriteRoomExit(swwld, room.ExitUp, Direction.Up);
                WriteRoomExit(swwld, room.ExitDown, Direction.Down);

                foreach (ExtraDesc ed in room.ExtraDescriptions)
                {
                    swwld.WriteLine("E");
                    swwld.WriteLine(ed.Aliases + "~");
                    var parts2 = ed.Description.Replace("\r", "").TrimEnd('\n').Split('\n');
                    foreach (string s in parts2)
                        swwld.WriteLine(s);
                    swwld.WriteLine("~");
                }
                swwld.WriteLine("S");

                foreach (int i in room.TriggersList)
                    swwld.WriteLine($"T {i}");
                foreach (Ingredient ingr in room.Ingredients)
                    if (ingr.PowerAuto)
                        swwld.WriteLine($"I {ingr.TypeName}:{ingr.Probability}");
                    else
                        swwld.WriteLine($"I {ingr.TypeName},{ingr.Power}:{ingr.Probability}");

                room.Modifyed = false;
            }
            swwld.WriteLine("$");
            swwld.WriteLine("$");
            swwld.Close();
            swmap.Close();
            swwld.Dispose();
            swmap.Dispose();
            fswld.Close();
            fsmap.Close();
            fswld.Dispose();
            fsmap.Dispose();
        }

        private void WriteRoomExit(TextWriter swwld, Exit exit, Direction direction)
        {
            if (exit.RoomVNum == -1) return;

            swwld.WriteLine($"D{(int)direction}");
            if (!string.IsNullOrEmpty(exit.Description))
            {
                var parts = exit.Description.Replace("\r", "").TrimEnd('\n').Split('\n');
                foreach (string s in parts)
                    swwld.WriteLine(s);
            }
            swwld.WriteLine("~");

            var tmp = exit.Aliases.Length > 0 ? exit.Aliases + "|" : "";
            tmp += exit.ExinNameVin != "" ? exit.ExinNameVin : exit.Aliases;
            swwld.WriteLine(tmp + "~");
            swwld.WriteLine(exit.ExitFlag + " " + exit.Key + " " + exit.RoomVNum + " " + exit.LockLevel);
        }

        private static void WriteRoomDesc(TextWriter swwld, string description, bool replaceFlag, string tag)
        {
            if (string.IsNullOrEmpty(description) || string.IsNullOrEmpty(description.TrimStart(' '))) return;
            string tmpparam = tag;   
            if (replaceFlag)
                tmpparam += "R";
            tmpparam += description;
            tmpparam += tag;
            string[] parts = tmpparam.Replace("\r", "").TrimEnd('\n').Split('\n');
            for (int i = parts.Length - 1; i > 0; i--)
            {
                if (parts[i] != "")
                    i = 0;
                else
                    parts[i] = "#IGNORETHISLINE";
            }
            foreach (string s in parts)
            {
                if (s != "#IGNORETHISLINE")
                    swwld.WriteLine(s);
            }
            //swwld.WriteLine("");
        }
    }
}