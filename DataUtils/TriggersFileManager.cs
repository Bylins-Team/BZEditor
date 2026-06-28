using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace DataUtils
{
    public class TriggersFileManager : BaseFileManager
    {
        public bool Load(TriggersCollection triggersCollection, string zoneNumber, Encoding encoding)
        {
            if (!File.Exists(StaticData.WorldFolderPath + @"\TRG\" + zoneNumber + ".trg"))
                return true;

            var tnum = new Regex("#(?<Num>\\d+)");
            var tname = new Regex("(?<Name>.+)~");
            var tparam = new Regex("^(?<trig_class>\\d+) (?<trig_type>.+)? (?<num_arg>\\d+)");
            var sr = new StreamReader(StaticData.WorldFolderPath + @"\TRG\" + zoneNumber + ".trg", encoding);
            string input = sr.ReadLine();
            int trigPos = -1;
            int curTrigNum = -1;
            string trigBody = "";
            string trigName = "";
            int trigClass = -1;
            string trigType = "";
            int trigNumArg = -1;
            string trigArg = "";
            while (true)
            {
                if (input == null) break; //если конец файла, прекращаем обработку файла
                if (input.IndexOf("* Сгенерировано BZEditor") == -1 && input.IndexOf("* Количество триггеров : ") == -1 &&
                    input.IndexOf("* Сохранено ") == -1)
                {
                    Match m = tnum.Match(input);
                    Match m1 = tname.Match(input);
                    Match m2 = tparam.Match(input);
                    if (m.Success)
                    {
                        //Запись предыдущего триггера trignumber если таковой был прочитан
                        if (curTrigNum != -1)
                        {
                            var trigger = new Trigger(curTrigNum)
                                              {
                                                  Name = trigName,
                                                  Class = trigClass,
                                                  Type = trigType.Replace("0", ""),
                                                  NumArg = trigNumArg,
                                                  Arg = trigArg,
                                                  Body = trigBody
                                              };
                            triggersCollection.Add(trigger);
                            //TrigBody = "";
                            trigName = "";
                            trigClass = -1;
                            trigType = "";
                            trigNumArg = -1;
                            trigArg = "";
                        }

                        //Сброс позиции в читаемом триггере
                        trigPos = 0;
                        curTrigNum = Convert.ToInt32(m.Groups["Num"].ToString());
                        if (curTrigNum == 75574) Debug.WriteLine("test");
                        trigBody = "";
                    }
                        //распознавание всех строк завершающихся ~ с учетом позиции в триггере
                    else if (m1.Success)
                    {
                        string text = m1.Groups["Name"].ToString();
                        if (trigPos == 0) //Название триггера					
                        {
                            trigPos = 1;
                            trigName = text;
                        }
                        else if (trigPos == 2) //аргумент
                        {
                            trigPos = 3;
                            trigArg = text;
                        }
                    }
                    else if (m2.Success)
                    {
                        trigPos = 2;
                        GroupCollection gcoll = m2.Groups;
                        trigClass = Convert.ToInt32(gcoll["trig_class"].ToString());
                        trigType = gcoll["trig_type"].ToString();
                        trigNumArg = Convert.ToInt32(gcoll["num_arg"].ToString());
                    }
                    else //Тело триггера
                    {
                        trigPos++;
                        if (input != "~" && input != "$")
                            //$ обрезается чтоб в последнем триггере не появлялись обозначения конца файла
                        {
                            if (trigBody != "") trigBody += "\r\n";
                            trigBody += input;
                        }
                    }
                }
                input = sr.ReadLine();
            }
            if (curTrigNum != -1)
            {
                var trigger = new Trigger(curTrigNum)
                                  {
                                      Name = trigName,
                                      Class = trigClass,
                                      Type = trigType.Replace("0", ""),
                                      NumArg = trigNumArg,
                                      Arg = trigArg,
                                      Body = trigBody
                                  };
                triggersCollection.Add(trigger);
            }
            sr.Close();

            return true;
        }

        public void Save(TriggersCollection triggersCollection, string zoneNumber)
        {
            var fs =
                new FileStream(StaticData.WorldFolderPath + @"\TRG\" + zoneNumber + ".trg", FileMode.Create,
                               FileAccess.Write);
            var sw = new StreamWriter(fs, StaticData.CurrentEncoding) {NewLine = "\n"};
            //sw.WriteLine("* Сгенерировано BZEditor");
            //sw.WriteLine("* Количество триггеров : " + triggersCollection.Count);
            //sw.WriteLine("* Сохранено " + DateTime.Now);

            if (triggersCollection.Count > 0)
            {
                triggersCollection.Sort(new BaseDataObjectComparer());
                foreach (Trigger trigger in triggersCollection)
                {
                    sw.WriteLine("#" + trigger.VNum);
                    sw.WriteLine(trigger.Name + "~");
                    //string type = string.IsNullOrEmpty(trigger.Type) ? " " : trigger.Type;
                    sw.WriteLine(trigger.Class + " " + trigger.Type + " " + trigger.NumArg);
                    sw.WriteLine(trigger.Arg + "~");
                    string[] parts = trigger.Body.Replace("\r", "").TrimEnd('\n').Split('\n');
                    foreach (string s in parts)
                        sw.WriteLine(s);
                    sw.WriteLine("~");
                    trigger.Modifyed = false;
                }
            }
            //if (triggersCollection.Count > 0)
            //{
            //    triggersCollection.Sort(new BaseDataObjectComparer());
            //    CTrigger Trigger = triggersCollection.GetFirst();
            //    int LastVnum = triggersCollection.GetLastVNum();
            //    bool finished = false;
            //    while (!finished)
            //    {
            //        sw.WriteLine("#" + Trigger.VNum);
            //        sw.WriteLine(Trigger.Name + "~");
            //        sw.WriteLine(Trigger.Class + " " + Trigger.Type + " " + Trigger.NumArg);
            //        sw.WriteLine(Trigger.Arg + "~");
            //        string[] parts = Trigger.Body.Replace("\r", "").TrimEnd('\n').Split('\n');
            //        foreach (string s in parts)
            //            sw.WriteLine(s);
            //        sw.WriteLine("~");

            //        if (Trigger.VNum < LastVnum)
            //            Trigger = triggersCollection.GetNext(Trigger.VNum);
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