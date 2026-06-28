using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace DataUtils
{
    public class ShopsFileManager : BaseFileManager
    {
        private int FilePos;
        private string LastString;

        private string ReadLine(TextReader sr)
        {
            LastString = sr.ReadLine();
            FilePos++;
            return LastString;
        }

        public bool Load(ShopsCollection shopsCollection, string zoneNumber, Encoding encoding)
        {
            FilePos = 0;
            string additionalInfo = "";
            string filePath = StaticData.WorldFolderPath + @"\SHP\" + zoneNumber + ".shp";
            if (!File.Exists(filePath))
                return true;

            var tnumold = new Regex("#\\d+~");
            var tnum = new Regex("#(?<Num>\\d+)");
            using (var sr = new StreamReader(filePath, encoding))
            {
                string input;
                //int CurShpNum = -1;
                try
                {
                    while ((input = ReadLine(sr)) != null)
                    {
                        additionalInfo = "отсутствует...";
                        var shop = new Shop(-1);
                        while (input.IndexOf("#") == -1) //Смещаемся на начало описания магазина
                        {
                            input = ReadLine(sr);
                            if (input == null) break; //если конец файла, то прекращаем искать начало след.шопа
                        }
                        if (input == null) break; //если конец файла, прекращаем обработку файла
                        Match mold = tnumold.Match(input);
                        bool newFormat = !mold.Success;

                        Match m = tnum.Match(input);
                        if (m.Success)
                        {
                            shop = new Shop(Convert.ToInt32(m.Groups["Num"].ToString()));
                            additionalInfo = "магазин номер [" + m.Groups["Num"] + "]";
                        }
                        input = ReadLine(sr);
                        while (input != "-1") //Читаем список товаров, которые постоянно есть в магазине	(до -1)
                        {
                            shop.AddPermanentlySellingObject(Convert.ToInt32(input));
                            input = ReadLine(sr);
                        }
                        input = ReadLine(sr);
                        shop.SellCoeff = Convert.ToDecimal(input.Replace(".", ","));
                        //число - коэффициент стоимости при продаже
                        input = ReadLine(sr);
                        shop.BuyCoeff = Convert.ToDecimal(input.Replace(".", ","));
                        //число - коэффициент стоимости при покупке
                        if (newFormat)
                        {
                            input = ReadLine(sr);
                            shop.ChangeCoeff = Convert.ToDecimal(input.Replace(".", ","));
                            //число - коэффициент стоимости при обмене
                        }
                        input = ReadLine(sr);
                        while (input != "-1") //тип предмета для покупки или строка.покупаются предметы, 
                            //имеющие это слово(а) в имени или флагах предмета. (до -1)
                        {
                            shop.AddBuyingObject(input.Replace("~", ""));
                            input = ReadLine(sr);
                        }
                        if (newFormat)
                        {
                            input = ReadLine(sr);
                            while (input != "-1") //тип предмета для обмена (аналогично) (до -1)
                            {
                                shop.AddChangingObject(input.Replace("~", ""));
                                input = ReadLine(sr);
                            }
                        }

                        shop.Msg1 = ReadLine(sr).Replace("~", ""); //Этого нет в списке товаров!
                        shop.Msg2 = ReadLine(sr).Replace("~", ""); //Таким барахлом я не торгую!
                        shop.Msg3 = ReadLine(sr).Replace("~", ""); //Таким барахлом я не торгую!
                        shop.Msg4 = ReadLine(sr).Replace("~", ""); //Извините, но сейчас у меня нет денег!
                        shop.Msg5 = ReadLine(sr).Replace("~", ""); //У вас нет столько денег!
                        shop.Msg6 = ReadLine(sr).Replace("~", ""); //За это с тебя будет %d.
                        shop.Msg7 = ReadLine(sr).Replace("~", ""); //За это я дам не больше %d!

                        input = ReadLine(sr);
                        shop.Emotion = Convert.ToInt32(input);

                        input = ReadLine(sr);
                        shop.Bitvector = input == "0" ? "" : input;

                        input = ReadLine(sr);
                        shop.ShopkeeperVNum = Convert.ToInt32(input); //число  - номер продавца

                        input = ReadLine(sr);
                        shop.NotTradeWithBitvector = input == "0" ? "" : input;

                        input = ReadLine(sr);
                        while (input != "-1") //виртуальные номера комнат магазина(до -1)
                        {
                            shop.AddShopLocation(Convert.ToInt32(input));
                            input = ReadLine(sr);
                        }

                        shop.OpeningTime1 = Convert.ToInt32(ReadLine(sr).Trim()); //число - время открытия 1
                        shop.ClosingTime1 = Convert.ToInt32(ReadLine(sr).Trim()); //число - время закрытия 1
                        shop.OpeningTime2 = Convert.ToInt32(ReadLine(sr).Trim()); //число - время открытия 2
                        shop.ClosingTime2 = Convert.ToInt32(ReadLine(sr).Trim()); //число - время закрытия 2	

                        shopsCollection.Add(shop);
                    }
                }
                catch (Exception ex)
                {
                    FireExceptionEvent("Ошибка при загрузке магазинов:\nФайл: \"" + filePath + "\"\nСтрока #" + FilePos + ": " +
                            LastString + "\nДополнительная информация: " + additionalInfo, ex, EventLogEntryType.Warning);
                    sr.Close();
                    return false;
                }
                sr.Close();
                return true;
            }
        }

        public void Save(ShopsCollection shopsCollection, string zoneNumber)
        {
            var t = new Regex("^\\d+$");
            var fs =
                new FileStream(StaticData.WorldFolderPath + @"\SHP\" + zoneNumber + ".shp", FileMode.Create,
                               FileAccess.Write);
            var sw = new StreamWriter(fs, StaticData.CurrentEncoding) {NewLine = "\n"};
            sw.WriteLine("CircleMUD v3.0 Shop File~");
            /*sw.WriteLine("* Сгенерировано BZEditor");
            sw.WriteLine("* Количество магазинов : " + shopsCollection.Count);
            sw.WriteLine("* Сохранено " + DateTime.Now.ToString());*/
            if (shopsCollection.Count > 0)
            {
                shopsCollection.Sort(new BaseDataObjectComparer());
                foreach (Shop shop in shopsCollection)
                {
                    sw.WriteLine("#" + shop.VNum + " E~"); //буквой обозначается новый формат магазина
                    foreach (int i in shop.PermanentlySellingList)
                        //список товаров, которые постоянно есть в магазине	(до -1)
                        sw.WriteLine(i);
                    sw.WriteLine("-1");
                    sw.WriteLine(shop.SellCoeff.ToString().Replace(",", "."));
                    sw.WriteLine(shop.BuyCoeff.ToString().Replace(",", "."));
                    sw.WriteLine(shop.ChangeCoeff.ToString().Replace(",", "."));
                    foreach (string s in shop.BuyingObjectsList)
                    //тип предмета для покупки или строка.покупаются предметы
                    //имеющие это слово(а) в имени или флагах предмета. (до -1)
                    {
                        Match m = t.Match(s);
                        if (m.Success)
                            sw.WriteLine(s);
                        else
                            sw.WriteLine(s + "~");
                    }
                    sw.WriteLine("-1");
                    foreach (string s in shop.ChangingObjectsList) //тип предмета для обмена (аналогично) (до -1)
                    {
                        Match m = t.Match(s);
                        if (m.Success)
                            sw.WriteLine(s);
                        else
                            sw.WriteLine(s + "~");
                    }
                    sw.WriteLine("-1");
                    sw.WriteLine(shop.Msg1 + "~"); //Этого нет в списке товаров!
                    sw.WriteLine(shop.Msg2 + "~"); //Таким барахлом я не торгую!
                    sw.WriteLine(shop.Msg3 + "~"); //Таким барахлом я не торгую!
                    sw.WriteLine(shop.Msg4 + "~"); //Извините, но сейчас у меня нет денег!
                    sw.WriteLine(shop.Msg5 + "~"); //У вас нет столько денег!
                    sw.WriteLine(shop.Msg6 + "~"); //За это с тебя будет %d.
                    sw.WriteLine(shop.Msg7 + "~"); //За это я дам не больше %d!
                    sw.WriteLine(shop.Emotion);
                    string tmpparam1 = (shop.Bitvector == "") ? "0" : shop.Bitvector;
                    sw.WriteLine(tmpparam1);
                    sw.WriteLine(shop.ShopkeeperVNum); //число  - номер продавца
                    tmpparam1 = (shop.NotTradeWithBitvector == "") ? "0" : shop.NotTradeWithBitvector;
                    //строка - битвектор профессий с которыми НЕ ТОРГУЕТ
                    sw.WriteLine(tmpparam1);
                    foreach (int i in shop.ShopLocationsList)
                        sw.WriteLine(i);
                    sw.WriteLine("-1");

                    sw.WriteLine(shop.OpeningTime1); //число - время открытия 1
                    sw.WriteLine(shop.ClosingTime1); //число - время закрытия 1
                    sw.WriteLine(shop.OpeningTime2); //число - время открытия 2
                    sw.WriteLine(shop.ClosingTime2); //число - время закрытия 2

                    shop.Modifyed = false;
                }
            }

            //if (shopsCollection.Count > 0)
            //{
            //    shopsCollection.Sort(new BaseDataObjectComparer());
            //    CShop Shop = shopsCollection.GetFirst();
            //    int LastVnum = shopsCollection.GetLastVNum();
            //    bool finished = false;
            //    while (!finished)
            //    {
            //        sw.WriteLine("#" + Shop.VNum + " E~"); //буквой обозначается новый формат магазина
            //        foreach (int i in Shop.PermanentlySellingList)
            //            //список товаров, которые постоянно есть в магазине	(до -1)
            //            sw.WriteLine(i);
            //        sw.WriteLine("-1");
            //        sw.WriteLine(Shop.SellCoeff.ToString().Replace(",", "."));
            //        sw.WriteLine(Shop.BuyCoeff.ToString().Replace(",", "."));
            //        sw.WriteLine(Shop.ChangeCoeff.ToString().Replace(",", "."));
            //        foreach (string s in Shop.BuyingObjectsList)
            //            //тип предмета для покупки или строка.покупаются предметы
            //            //имеющие это слово(а) в имени или флагах предмета. (до -1)
            //        {
            //            Match m = t.Match(s);
            //            if (m.Success)
            //                sw.WriteLine(s);
            //            else
            //                sw.WriteLine(s + "~");
            //        }
            //        sw.WriteLine("-1");
            //        foreach (string s in Shop.ChangingObjectsList) //тип предмета для обмена (аналогично) (до -1)
            //        {
            //            Match m = t.Match(s);
            //            if (m.Success)
            //                sw.WriteLine(s);
            //            else
            //                sw.WriteLine(s + "~");
            //        }
            //        sw.WriteLine("-1");
            //        sw.WriteLine(Shop.Msg1 + "~"); //Этого нет в списке товаров!
            //        sw.WriteLine(Shop.Msg2 + "~"); //Таким барахлом я не торгую!
            //        sw.WriteLine(Shop.Msg3 + "~"); //Таким барахлом я не торгую!
            //        sw.WriteLine(Shop.Msg4 + "~"); //Извините, но сейчас у меня нет денег!
            //        sw.WriteLine(Shop.Msg5 + "~"); //У вас нет столько денег!
            //        sw.WriteLine(Shop.Msg6 + "~"); //За это с тебя будет %d.
            //        sw.WriteLine(Shop.Msg7 + "~"); //За это я дам не больше %d!
            //        sw.WriteLine(Shop.Emotion);
            //        string tmpparam1 = (Shop.Bitvector == "") ? "0" : Shop.Bitvector;
            //        sw.WriteLine(tmpparam1);
            //        sw.WriteLine(Shop.ShopkeeperVNum); //число  - номер продавца
            //        tmpparam1 = (Shop.NotTradeWithBitvector == "") ? "0" : Shop.NotTradeWithBitvector;
            //        //строка - битвектор профессий с которыми НЕ ТОРГУЕТ
            //        sw.WriteLine(tmpparam1);
            //        foreach (int i in Shop.ShopLocationsList)
            //            sw.WriteLine(i);
            //        sw.WriteLine("-1");

            //        sw.WriteLine(Shop.OpeningTime1); //число - время открытия 1
            //        sw.WriteLine(Shop.CloingTime1); //число - время закрытия 1
            //        sw.WriteLine(Shop.OpeningTime2); //число - время открытия 2
            //        sw.WriteLine(Shop.CloingTime2); //число - время закрытия 2
            //        if (Shop.VNum < LastVnum)
            //            Shop = shopsCollection.GetNext(Shop.VNum);
            //        else
            //            finished = true;
            //    }
            //}

            sw.WriteLine("$");
            sw.WriteLine("$");
            sw.WriteLine("~");
            sw.Close();
            fs.Close();
            sw.Dispose();
            fs.Dispose();
        }
    }
}