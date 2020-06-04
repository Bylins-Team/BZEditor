using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace DataUtils
{
    public class GlobalSketchFileManager : BaseFileManager
    {
        private int filePos;
        private string lastString;

        private string ReadLine(TextReader sr)
        {
            lastString = sr.ReadLine();
            filePos++;
            return lastString;
        }

        public bool Load(GlobalSketch sketch)
        {
            filePos = 0;
            const string additionalInfo = "отсутствует...";
            //Загрузка скетч-файла
            string filePath = StaticData.WorldFolderPath + @"\GSKT\" + sketch.FileName + ".gskt";
            if (!File.Exists(filePath))
                return true;
            string input;
            using (var sr = new StreamReader(filePath, Encoding.Default))
            {
                try
                {
                    while ((input = ReadLine(sr)) != null)
                    {
                        if (input[0] == '*') continue;
                        string[] parts = (input.Contains("\t")) ? input.Split('\t') : input.Split(' ');
                        //Распарсить список зон с цветами
                        if (parts.Length == 2)
                        {
                            switch (parts[0])
                            {
                                case "Name":
                                    sketch.Name = parts[1];
                                    break;
                            }
                        }
                        if (parts.Length == 3)
                        {
                            switch (parts[0])
                            {
                                case "zone":
                                    int zNum = int.Parse(parts[1]);
                                    sketch.ZonesNumbers.Add(zNum);
                                    sketch.ZonesColors.Add(zNum, Color.FromArgb(int.Parse(parts[2])));
                                    input = ReadLine(sr);
                                    string[] parts1 = input.Split(' ');
                                    if (parts1[0] == "zname")
                                        sketch.ZonesNames.Add(zNum, parts1[1]);
                                    else
                                        sketch.ZonesNames.Add(zNum, "Зона №" + zNum);
                                    break;
                            }
                        }
                        else if (parts.Length == 4)
                        {
                            sketch.AddSketchRoom(
                                Convert.ToInt32(parts[0].Trim()),
                                Convert.ToInt32(parts[1].Trim()),
                                Convert.ToInt32(parts[2].Trim()),
                                Convert.ToInt32(parts[3].Trim()));
                        }
                    }
                }
                catch (Exception ex)
                {
                    FireExceptionEvent("Ошибка при загрузке эскиза:\nФайл: \"" + filePath + "\"\nСтрока #" + filePos + ": " +
                            lastString + "\nДополнительная информация: " + additionalInfo, ex, EventLogEntryType.Warning);
                    sr.Close();
                    return false;
                }
                sr.Close();
                return true;
            }
        }

        public bool Save(GlobalSketch sketch)
        {
            string filePath = StaticData.WorldFolderPath + @"\GSKT\" + sketch.FileName + ".gskt";
            Utils.EnsureDirectory(StaticData.WorldFolderPath + @"\GSKT\");
            const string additionalInfo = "отсутствует...";
            try
            {
                using (
                    FileStream fsskt =
                        new FileStream(filePath, FileMode.Create,
                                       FileAccess.Write))
                using (var swskt = new StreamWriter(fsskt, Encoding.Default))
                {
                    swskt.WriteLine("* Сгенерировано BZEditor");
                    swskt.WriteLine("* Эскиз комплекса зон \"" + sketch.Name + "\"");
                    swskt.WriteLine("* Количество комнат на эскизе: " + sketch.Count);
                    swskt.WriteLine("Name\t" + sketch.Name);
                    //swskt.WriteLine("LastSketchColor " + sketch.LastSketchColor.ToArgb());
                    //Сохранить список зон с цветами
                    foreach (int zNum in sketch.ZonesNumbers)
                    {
                        swskt.WriteLine("zone " + zNum + " " + sketch.ZonesColors[zNum].ToArgb());
                        swskt.WriteLine("zname " + sketch.ZonesNames[zNum]);
                    }
                    foreach (GlobalSketchRoom room in sketch)
                        swskt.WriteLine(room.X + " " + room.Y + " " + room.Z + " " + room.ZoneNum);
                    swskt.Close();
                }
            }
            catch (Exception ex)
            {
                FireExceptionEvent("Ошибка при сохранении эскиза:\nФайл: \"" + filePath + "\"\nСтрока #" + filePos + ": " +
                    lastString + "\nДополнительная информация: " + additionalInfo, ex, EventLogEntryType.Warning);
                return false;
            }
            return true;
        }
    }
}