using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace DataUtils
{
    public class SketchFileManager : BaseFileManager
    {
        private int filePos;
        private string lastString;

        private string ReadLine(TextReader sr)
        {
            lastString = sr.ReadLine();
            filePos++;
            return lastString;
        }

        public bool Load(SketchRoomsCollection sketchRoomsCollection, string zoneNumber, Encoding encoding)
        {
            filePos = 0;
            const string additionalInfo = "отсутствует...";
            //Загрузка скетч-файла
            string filePath = StaticData.WorldFolderPath + @"\WLD\" + zoneNumber + ".skt";
            if (!File.Exists(filePath))
                return true;
            string input;
            using (var sr = new StreamReader(filePath, encoding))
            {
                try
                {
                    while ((input = ReadLine(sr)) != null)
                    {
                        if (input[0] == '*') continue;
                        string[] parts = input.Split(' ');
                        if (parts.Length == 4)
                        {
                            sketchRoomsCollection.AddSketchRoom(
                                Convert.ToInt32(parts[0].Trim()),
                                Convert.ToInt32(parts[1].Trim()),
                                Convert.ToInt32(parts[2].Trim()),
                                Color.FromArgb(Convert.ToInt32(parts[3].Trim())));
                        }
                        else if (parts.Length == 2 && parts[0] == "LastSketchColor")
                            sketchRoomsCollection.LastSketchColor =
                                Color.FromArgb(Convert.ToInt32(parts[1].Trim()));
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

        public void Save(SketchRoomsCollection sketchRoomsCollection, string zoneNumber)
        {
            using (
                var fsskt =
                    new FileStream(StaticData.WorldFolderPath + @"\WLD\" + zoneNumber + ".skt", FileMode.Create,
                                   FileAccess.Write))
            using (var swskt = new StreamWriter(fsskt, StaticData.CurrentEncoding))
            {
                swskt.WriteLine("* Сгенерировано BZEditor");
                swskt.WriteLine("* Количество комнат на эскизе: " + sketchRoomsCollection.Count);
                swskt.WriteLine("LastSketchColor " + sketchRoomsCollection.LastSketchColor.ToArgb());
                foreach (SketchRoom room in sketchRoomsCollection)
                {
                    swskt.WriteLine(room.X + " " + room.Y + " " + room.Z + " " + room.RoomColor.ToArgb());
                }
                swskt.Close();
            }
        }
    }
}