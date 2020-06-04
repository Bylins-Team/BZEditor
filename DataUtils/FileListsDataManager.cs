using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace DataUtils
{
    public class FileListsDataManager
    {
        private int loadedZonesCount;
        private ZonesDataList zonesFileList;
        private ZonesDataList sketchesFileList;

        public FileListsDataManager()
        {
            zonesFileList = new ZonesDataList();
            sketchesFileList = new ZonesDataList();
        }

        public ZonesDataList ZonesDataList
        {
            get { return zonesFileList; }
        }

        public ZonesDataList SketchesDataList
        {
            get { return sketchesFileList; }
        }

        public int LoadedZonesCount
        {
            get { return loadedZonesCount; }
        }

        public void SaveData()
        {
            Utils.EnsureDirectory(StaticData.ConfigFolder);
            using (FileStream fs = new FileStream(Path.Combine(StaticData.ConfigFolder, "ZonesList.xml"), FileMode.Create))
            {
                using (TextWriter writer = new StreamWriter(fs))
                {
                    try
                    {
                        var ser = new XmlSerializer(typeof (ZonesDataList));
                        ser.Serialize(writer, zonesFileList);
                    }
                    catch (SerializationException e)
                    {
                        throw new Exception("Ошибка сериализации при сохранении списка зон.", e);
                    }
                    finally
                    {
                        writer.Close();
                        fs.Close();
                    }
                }
            }
            using (FileStream fs = new FileStream(Path.Combine(StaticData.ConfigFolder, "SketchesList.xml"), FileMode.Create))
            {
                using (TextWriter writer = new StreamWriter(fs))
                {
                    try
                    {
                        var ser = new XmlSerializer(typeof(ZonesDataList));
                        ser.Serialize(writer, sketchesFileList);
                    }
                    catch (SerializationException e)
                    {
                        throw new Exception("Ошибка сериализации при сохранении списка эскизов.", e);
                    }
                    finally
                    {
                        writer.Close();
                        fs.Close();
                    }
                }
            }
        }

        public void LoadData()
        {
            if (File.Exists(Path.Combine(StaticData.ConfigFolder, "ZonesList.xml")))
            {
                var fs = new FileStream(Path.Combine(StaticData.ConfigFolder, "ZonesList.xml"), FileMode.Open);

                try
                {
                    var ser = new XmlSerializer(typeof(ZonesDataList));
                    zonesFileList = (ZonesDataList)ser.Deserialize(fs);
                }
                catch (SerializationException e)
                {
                    throw new Exception("Ошибка десериализации при загрузке списка зон.", e);
                }
                finally
                {
                    fs.Close();
                }
            }
            if (File.Exists(Path.Combine(StaticData.ConfigFolder, "SketchesList.xml")))
            {
                var fs = new FileStream(Path.Combine(StaticData.ConfigFolder, "SketchesList.xml"), FileMode.Open);

                try
                {
                    var ser = new XmlSerializer(typeof(ZonesDataList));
                    sketchesFileList = (ZonesDataList)ser.Deserialize(fs);
                }
                catch (SerializationException e)
                {
                    throw new Exception("Ошибка десериализации при загрузке списка эскизов.", e);
                }
                finally
                {
                    fs.Close();
                }
            }
            /*if (_zonesDataList.Count > 0)
                RefreshAvailZones();
            else*/
                LoadAvailZones();
            loadedZonesCount = RecalcLoadedZonesCount();
        }

        private int RecalcLoadedZonesCount()
        {
            int res = 0;
            foreach (ZoneData zd in zonesFileList)
            {
                if (zd.Preloading)
                    res++;
            }
            return res;
        }

        /*public void RefreshAvailZones()
        {
            string path = StaticData.WorldFolderPath + @"\ZON\";
            foreach (CZoneData zd in _zonesDataList)
            {
                if (!File.Exists(Path.Combine(path, zd.Number + ".zon")) && zd.State == ZoneState.Preloading)
                    zd.State = ZoneState.NotFound;
            }
        }*/

        readonly Regex num = new Regex("^#(?<Num>\\d+)$", RegexOptions.Compiled);
        readonly Regex name = new Regex("^(?<Name>.+)~", RegexOptions.Compiled);

        public bool LoadAvailZones()
        {
            //Не очищается полностью так как этот список делится на загруженные и доступные и флаг загруженности хранится в списке
            var targetFolder = new DirectoryInfo(StaticData.WorldFolderPath + @"\ZON\");
            bool res = false;
            List<ZoneData> toRemove = new List<ZoneData>(zonesFileList.Count);
            foreach (ZoneData zd in zonesFileList)
            {
                if (!File.Exists(StaticData.WorldFolderPath + @"\ZON\" + zd.FileName + ".zon"))
                {
                    //Если зона была загружена при закрытии а потом пропала, то просто метим как ненайденную
                    if (zd.Preloading)
                        zd.State = ZoneState.NotFound;
                    else //Иначе удаляем из списка доступных без следа
                        toRemove.Add(zd);
                    res = true;
                }
            }
            if (toRemove.Count > 0)
                foreach (ZoneData zd in toRemove)
                    zonesFileList.Remove(zd);

            foreach (FileInfo nextFile in targetFolder.GetFiles())
            {
                if (nextFile.Extension == ".zon")
                {
                    using (var sr = new StreamReader(nextFile.FullName, StaticData.CurrentEncoding))
                    {
                        string input;
                        string newnumber = "";
                        string newname = "";
                        while ((input = sr.ReadLine()) != null)
                        {
                            if (input == "$")
                                break;
                            if (input.IndexOf("*") == 0)
                                continue;
                            Match match = num.Match(input);
                            if (match.Success)
                            {
                                newnumber = match.Groups["Num"].ToString();
                                continue;
                            }
                            match = name.Match(input);
                            if (match.Success)
                            {
                                newname = match.Groups["Name"].ToString();
                                break;
                            }
                        }
                        ZoneData zd = zonesFileList[newnumber];
                        if (zd == null)
                        {
                            zd = new ZoneData(newnumber, newname) {State = ZoneState.Available};
                            zonesFileList.Add(zd);
                            res = true;
                        }
                        else if (zd.Name != newname)
                        {
                            zd.Name = newname;
                            res = true;
                        }
                        sr.Close();
                    }
                }
            }

            return res;
        }

        public void AddZoneToLoadedList(string number)
        {
            ZoneData zd = zonesFileList[number];
            if (zd != null)
            {
                zd.Preloading = true;
                SaveData();
            }
        }

        public void AddZoneToLoadedList(string number, string newName)
        {
            ZoneData zd = zonesFileList[number];
            if (zd != null)
            {
                zd.Name = newName;
                zd.Preloading = true;
                SaveData();
            }
        }

        public void AddZoneToList(string number)
        {
            AddZoneToList(number, "Новая зона");
        }

        public void AddZoneToList(string number, string newName)
        {
            if (zonesFileList[number] == null)
            {
                var zd = new ZoneData(number, newName) {Preloading = true};
                zonesFileList.Add(zd);
                loadedZonesCount++;
            }
            SaveData();
        }

        public void RemoveZoneFromLoadedList(string number)
        {
            ZoneData zd = zonesFileList[number];
            if (zd != null)
            {
                zd.State = ZoneState.Available;
                loadedZonesCount--;
            }
            SaveData();
        }

        public void RemoveZoneFromList(string number)
        {
            ZoneData zd = zonesFileList[number];
            if (zd != null)
            {
                zonesFileList.Remove(zd);
                loadedZonesCount--;
            }
            SaveData();
        }

        public void RenameZone(string number, string newName)
        {
            ZoneData zd = zonesFileList[number];
            if (zd != null)
                zd.Name = newName;
            SaveData();
        }

        public void ChangeZoneNumber(int oldVnum, int newVnum)
        {
            ZoneData zd = zonesFileList[oldVnum.ToString()];
            if (zd != null)
                zd.VNum = newVnum;
            SaveData();
        }

        /// <summary>
        /// Удаление файлов зоны с диска
        /// </summary>
        /// <param name="number"></param>
        public void RemoveZone(string number)
        {
            if (File.Exists(StaticData.WorldFolderPath + @"\ZON\" + number + ".zon"))
                File.Delete(StaticData.WorldFolderPath + @"\ZON\" + number + ".zon");
            if (File.Exists(StaticData.WorldFolderPath + @"\WLD\" + number + ".wld"))
                File.Delete(StaticData.WorldFolderPath + @"\WLD\" + number + ".wld");
            if (File.Exists(StaticData.WorldFolderPath + @"\WLD\" + number + ".skt"))
                File.Delete(StaticData.WorldFolderPath + @"\WLD\" + number + ".skt");
            if (File.Exists(StaticData.WorldFolderPath + @"\WLD\" + number + ".map"))
                File.Delete(StaticData.WorldFolderPath + @"\WLD\" + number + ".map");
            if (File.Exists(StaticData.WorldFolderPath + @"\TRG\" + number + ".trg"))
                File.Delete(StaticData.WorldFolderPath + @"\TRG\" + number + ".trg");
            if (File.Exists(StaticData.WorldFolderPath + @"\SHP\" + number + ".shp"))
                File.Delete(StaticData.WorldFolderPath + @"\SHP\" + number + ".shp");
            if (File.Exists(StaticData.WorldFolderPath + @"\OBJ\" + number + ".obj"))
                File.Delete(StaticData.WorldFolderPath + @"\OBJ\" + number + ".obj");
            if (File.Exists(StaticData.WorldFolderPath + @"\MOB\" + number + ".mob"))
                File.Delete(StaticData.WorldFolderPath + @"\MOB\" + number + ".mob");
        }

        public string ZoneName(string number)
        {
            ZoneData zd = zonesFileList[number];
            return (zd != null)?zonesFileList[number].Name:"";
        }

        public void ReloadSketchesList()
        {
            var targetFolder = new DirectoryInfo(StaticData.WorldFolderPath + @"\GSKT\");
            //Не очищается полностью так как этот список делится на загруженные и доступные и флаг загруженности хранится в списке
            List<ZoneData> toRemove = new List<ZoneData>(sketchesFileList.Count);
            foreach (ZoneData zd in sketchesFileList)
                if (!File.Exists(targetFolder + zd.FileName + ".gskt"))
                    toRemove.Add(zd);
            foreach (ZoneData zd in toRemove)
                sketchesFileList.Remove(zd);

            foreach (FileInfo nextFile in targetFolder.GetFiles())
            {
                if (nextFile.Extension == ".gskt")
                {
                    using (var sr = new StreamReader(nextFile.FullName, Encoding.Default))
                    {
                        string input;
                        while ((input = sr.ReadLine()) != null)
                        {
                            if (input.IndexOf("*") == 0)
                                continue;
                            string[] parts = input.Split('\t');
                            string filename = Path.GetFileNameWithoutExtension(nextFile.FullName);
                            if (parts.Length == 2 && parts[0] == "Name")
                            {
                                ZoneData zd = sketchesFileList[filename];
                                if (zd == null)
                                    AddSketchToList(filename, parts[1]);
                                break;
                            }
                        }
                        sr.Close();
                    }
                }
            }
        }

        public string AddSketchToList(string fileName, string newName)
        {
            if (sketchesFileList[fileName] == null)
            {
                var zd = new ZoneData(fileName, newName);
                sketchesFileList.Add(zd);
            }
            else
                return "Эскиз с именем "+ newName +" уже существует";
            SaveData();
            return string.Empty;
        }
        
        public void RemoveSketch(string fileName)
        {
            if (File.Exists(StaticData.WorldFolderPath + @"\GSKT\" + fileName + ".gskt"))
                File.Delete(StaticData.WorldFolderPath + @"\GSKT\" + fileName + ".gskt");
            sketchesFileList.Remove(sketchesFileList[fileName]);
            SaveData();            
        }

        public string GetSketchFileName(string sketchName)
        {
            foreach (ZoneData sd in sketchesFileList)
                if (sd.Name == sketchName)
                    return sd.FileName;
            return string.Empty;
        }

        public string GetSketchName(string sketchFileName)
        {
            foreach (ZoneData sd in sketchesFileList)
                if (sd.FileName == sketchFileName)
                    return sd.Name;
            return string.Empty;
        }
    }
}