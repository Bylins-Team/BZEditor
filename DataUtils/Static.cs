using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataUtils
{
    public static class StaticData
    {
        public static int MaxRoomsPerZone = 98;

        /// <summary>
        ///   Максимальное количество символов в описании
        /// </summary>
        public static int MaxTextWidth = 80;
        
        /// <summary>
        ///   Оптимальное количество символов в описании
        /// </summary>
        public static int OptimalTextWidth = 70;


        /// <summary>
        /// Максимальное ограничение по оси Z
        /// </summary>
        public static int MaxZ = 3;

        /// <summary>
        /// Минимальное ограничение по оси Z
        /// </summary>
        public static int MinZ = -3;

        /// <summary>
        /// Переменная хранит состояние сети сообщений об изменениях объектов зоны
        /// true - сообщения генерируются
        /// false - сообщения не генерируются
        /// Значение переменной надо устанавливать каждый раз при выборе новой закладки с зоной
        /// </summary>        
        public static bool CanFireChangeEvent;

        /// <summary>
        /// Путь к файлам настроек
        /// </summary>
        public static string ConfigFolder = "";

        /// <summary>
        /// Кодировка в которой будут сохранятся зоны
        /// </summary>
        public static Encoding CurrentEncoding = Encoding.Default;

        /// <summary>
        /// Путь к файлам зон
        /// </summary>
        public static string WorldFolderPath = "";

        public static bool ScrollingForTouchpad;
        /// <summary>
        /// Необходимость бэкапа зоны перед сохранением
        /// </summary>
        public static bool BackupZones;
    }

    public static class Utils
    {
        public static string PrepareFileName(string name)
        {
            name = name.Replace(' ', '_');
            foreach (char c in Path.GetInvalidFileNameChars())
                name = name.Replace(c, '_');
            return name;
        }

        public static void EnsureDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public static string RemovePredlog(string inStr)
        {
            string res = "";
            var toremove =
                new List<string>(
                    new[] {"у", "к", "за", "в", "во", "по", "с", "от", "о", "не", "ни", "а", "и", "но"});
            var chars =
                new[]
                    {
                        ',', '.', ';', ':', '"', '\'', '?', '!', '-', '_', '+', '-', '*', '/', '1', '2', '3', '4', '5',
                        '6'
                        , '7', '8', '9', '0'
                    };
            foreach (char c in chars)
                inStr = inStr.Replace(c.ToString(), "");
            string[] parts = inStr.ToLower().Split(' ');
            foreach (string s in parts)
            {
                if (!toremove.Contains(s))
                    res += (res == "") ? s : " " + s;
            }
            return res;
        }
    }
}