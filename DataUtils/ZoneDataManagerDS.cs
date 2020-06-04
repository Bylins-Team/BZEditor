using System;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace DataUtils
{
	public class ZoneDataManagerDS				
	{
		public DataSet DS;
		private int number;//Номер зоны
		private string name;//Название зоны
		private int lastRoomNum;//Номер последнец комнаты < 98 принудительно
		private int repopType;
		private int repopTimer;
		private string Path;
		private string zoneName;
		private string zonFileBody;
        private bool iRemovingMobsListUpdated = false;
        private Encoding CurrentEncoding = Encoding.Default;

        public ZoneDataManagerDS(string Path, string ZoneName, Encoding Enc)
		{
			this.Path = Path;
			this.zoneName = ZoneName;
            CurrentEncoding = Enc;
		}

		public string ZoneName
		{
			get
			{
				return this.zoneName;
			}
		}

		#region Get/Set Zon

		public int LastRoomNum
		{
			get
			{
				return this.lastRoomNum;
			}
			set
			{
				this.lastRoomNum = value;
			}
		}

		public int RepopType
		{
			get
			{
				return this.repopType;
			}
			set
			{
				this.repopType = value;
			}
		}

		public int Number
		{
			get
			{
				return this.number;
			}
			set
			{
				//Тут нужна хитрая обработка (логика смены номера зоны, исправление всех номеров)
                throw new NotImplementedException();
				//this.number = value;
			}
		}

		public string ZonFileBody
		{
			get
			{
				return this.zonFileBody;
			}
			set
			{
				this.zonFileBody = value;
			}
		}

		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		public int RepopTimer
		{
			get
			{
				return this.repopTimer;
			}
			set
			{
				this.repopTimer = value;
			}
		}

		public DataTable RemovingMobsData
		{
			get
			{
				return this.DS.Tables["RemovingMobsData"];
			}
		}

		public DataTable LoadingMobsData
		{
			get
			{
				return this.DS.Tables["LoadingMobsData"];
			}
		}

		public DataTable LoadingObjectsData
		{
			get
			{
				return this.DS.Tables["LoadingObjectsData"];
			}
		}
		
		public DataTable MobsFollowingData
		{
			get
			{
				return this.DS.Tables["MobsFollowingData"];
			}
		}

		public DataTable PlaceObjInObjData
		{
			get
			{
				return this.DS.Tables["PlaceObjInObjData"];
			}
		}

		public DataTable EquipMobByObjData
		{
			get
			{
				return this.DS.Tables["EquipMobByObjData"];
			}
		}
		
		public DataTable PutObjToMobInvData
		{
			get
			{
				return this.DS.Tables["PutObjToMobInvData"];
			}
		}
		
		public DataTable RemoveObjFromRoomData
		{
			get
			{
				return this.DS.Tables["RemoveObjFromRoomData"];
			}
		}

		public DataTable SetExitStateData
		{
			get
			{
				return this.DS.Tables["SetExitStateData"];
			}
		}

		#endregion

		#region Get/Set Trg
		
		public int TotalTriggersCount
		{
			get
			{
				return this.DS.Tables["TriggersData"].Rows.Count;
			}
		}

		public DataTable TriggersData
		{
			get
			{
				return this.DS.Tables["TriggersData"];
			}
		}

		#endregion

		#region Get/Set Shp
		
		public DataTable ShopsData
		{
			get
			{
				return this.DS.Tables["ShopsData"];
			}
		}

		public DataTable PermanentlySellingData
		{
			get
			{
				return this.DS.Tables["PermanentlySellingData"];
			}
		}
		
		public DataTable BuyingObjectsData
		{
			get
			{
				return this.DS.Tables["BuyingObjectsData"];
			}
		}
		
		public DataTable ChangingObjectsData
		{
			get
			{
				return this.DS.Tables["ChangingObjectsData"];
			}
		}

		public DataTable ShopRoomsData
		{
			get
			{
				return this.DS.Tables["ShopRoomsData"];
			}
		}
		
		#endregion
	
		#region Get/Set Obj
		
		public DataTable ObjectsData
		{
			get
			{
				return this.DS.Tables["ObjectsData"];
			}
		}

		public DataTable ObjExtraDesc
		{
			get
			{
				return this.DS.Tables["ObjExtraDesc"];
			}
		}
		
		public DataTable ObjAddParam
		{
			get
			{
				return this.DS.Tables["ObjAddParam"];
			}
		}
		
		public DataTable ObjTriggers
		{
			get
			{
				return this.DS.Tables["ObjTriggers"];
			}
		}

		#endregion

		#region Get/Set Wld
		
		public DataTable WldData
		
		{
			get
			{
				return this.DS.Tables["WldData"];
			}
		}
		
		public DataTable MapData
		
		{
			get
			{
				return this.DS.Tables["MapData"];
			}
		}
		
		public DataTable RoomExtraDesc
		
		{
			get
			{
				return this.DS.Tables["RoomExtraDesc"];
			}
		}

		public DataTable RoomTriggers
		
		{
			get
			{
				return this.DS.Tables["RoomTriggers"];
			}
		}
		
		#endregion

		#region Get/Set Mob
		
		public DataTable MobData
		
		{
			get
			{
				return this.DS.Tables["MobData"];
			}
		}
		
		public DataTable MobSkills
		
		{
			get
			{
				return this.DS.Tables["MobSkills"];
			}
		}

		public DataTable MobSpells
		
		{
			get
			{
				return this.DS.Tables["MobSpells"];
			}
		}
		
		#endregion
		
		public void LoadData()
		{
			CreateDataSchema();
			LoadDataZon();
			LoadDataTrg();
			LoadDataShp();
			LoadDataObj();
			LoadDataWld();
			LoadDataMob();
			this.DS.AcceptChanges();
		}

		#region LoadDataZon

		public void LoadDataZon()
		{	
			zonFileBody = "";
			Regex tnum = new Regex("#(?<Num>\\d+)");
			Regex tname = new Regex("(?<Name>.+)~");
			Regex tparam = new Regex("^(?<Patram1>\\d+) (?<Patram2>\\d+) (?<Patram3>\\d+)");
            Regex tq = new Regex("Q (?<ifflag>.+) (?<mob_vnum>.+) -1 -1 -1");
			Regex tm = new Regex("M (?<ifflag>.+) (?<mob_vnum>.+) (?<max_in_room>.+) (?<room_vnum>.+) (?<max_in_world>.+)\\t\\((?<name>.+)\\)");
			Regex to = new Regex("O (?<ifflag>.+) (?<obj_vnum>.+) (?<max_in_world>.+) (?<room_vnum>.+) (?<probability>.+)\\t\\((?<name>.+)\\)");
			Regex tf = new Regex("F (?<ifflag>.+) (?<room_vnum>.+) (?<mob_vnum_leader>.+) (?<mob_vnum_follower>.+) (?<reserved>.+)\\t\\((?<name>.+)\\)");
			Regex tp = new Regex("P (?<ifflag>.+) (?<obj_vnum>.+) (?<max_in_world>.+) (?<objto_vnum>.+) (?<probability>.+)\\t\\((?<name>.+)\\)");
			Regex tg = new Regex("G (?<ifflag>.+) (?<obj_vnum>.+) (?<max_in_world>.+) (?<probability>.+) (?<reserved>.+)\\t\\((?<name>.+)\\)");
			Regex te = new Regex("E (?<ifflag>.+) (?<obj_vnum>.+) (?<max_in_world>.+) (?<obj_pos>.+) (?<probability>.+)\\t\\((?<name>.+)\\)");
			Regex tr = new Regex("R (?<ifflag>.+) (?<room_vnum>.+) (?<obj_vnum>.+) (?<reserved1>.+) (?<reserved2>.+)\\t\\((?<name>.+)\\)");
			Regex td = new Regex("D (?<ifflag>.+) (?<room_vnum>.+) (?<dir>.+) (?<door_flag>.+) (?<reserved>.+)\\t\\((?<name>.+)\\)");
            StreamReader sr = new StreamReader(Path + @"\ZON\" + zoneName + ".zon", CurrentEncoding);
			string input;
			string LastLoadedMob = "";
			while ((input=sr.ReadLine())!=null) 
			{
				if (zonFileBody != "") zonFileBody += "\r\n";
				zonFileBody += input;
				if (input.IndexOf("*") != 0)
				{
					Match m = tnum.Match(input);
					if (m.Success)
					{
						GroupCollection gcoll = m.Groups;								
						this.number = Convert.ToInt32(gcoll["Num"].ToString());
					}
					m = tname.Match(input);
					if (m.Success)
					{
						GroupCollection gcoll = m.Groups;								
						this.name = gcoll["Name"].ToString();
					}
					m = tparam.Match(input);
					if (m.Success)
					{
						GroupCollection gcoll = m.Groups;								
						this.lastRoomNum = Convert.ToInt32(gcoll["Patram1"].ToString());
						this.repopTimer = Convert.ToInt32(gcoll["Patram2"].ToString());
						this.repopType = Convert.ToInt32(gcoll["Patram3"].ToString());
					}
					//Мобы, удаляемые при перезапуске
					m = tq.Match(input);
					if (m.Success)
					{
						GroupCollection gcoll = m.Groups;								
						DataRow drq = RemovingMobsData.NewRow();
						drq["ifflag"] = (gcoll["ifflag"].ToString() == "1");
                        drq["mob_vnum"] = gcoll["mob_vnum"].ToString();
                        RemovingMobsData.Rows.Add(drq);
					}
					//Мобы, заргужаемые в комнаты
					m = tm.Match(input);
					if (m.Success)
					{
						GroupCollection gcoll = m.Groups;								
						DataRow drm = LoadingMobsData.NewRow();
                        drm["ifflag"] = (gcoll["ifflag"].ToString() == "1");
						drm["mob_vnum"] = gcoll["mob_vnum"].ToString();
						LastLoadedMob = gcoll["mob_vnum"].ToString();
						drm["max_in_room"] = gcoll["max_in_room"].ToString();
						drm["room_vnum"] = gcoll["room_vnum"].ToString();
						drm["max_in_world"] = gcoll["max_in_world"].ToString();
						drm["name"] = gcoll["name"].ToString();
						LoadingMobsData.Rows.Add(drm);
					}
					//Объекты, заргужаемые в комнаты
					m = to.Match(input);
					if (m.Success)
					{
						GroupCollection gcoll = m.Groups;								
						DataRow dro = LoadingObjectsData.NewRow();
                        dro["ifflag"] = (gcoll["ifflag"].ToString() == "1");
						dro["obj_vnum"] = gcoll["obj_vnum"].ToString();
						dro["max_in_world"] = gcoll["max_in_world"].ToString();
						dro["room_vnum"] = gcoll["room_vnum"].ToString();
						dro["probability"] = gcoll["probability"].ToString();
						dro["name"] = gcoll["name"].ToString();
						LoadingObjectsData.Rows.Add(dro);
					}
					//Мобы следуют в группе
					m = tf.Match(input);
					if (m.Success)
					{
						GroupCollection gcoll = m.Groups;								
						DataRow drf = MobsFollowingData.NewRow();
                        drf["ifflag"] = (gcoll["ifflag"].ToString() == "1");
						drf["room_vnum"] = gcoll["room_vnum"].ToString();
						drf["mob_vnum_leader"] = gcoll["mob_vnum_leader"].ToString();
						drf["mob_vnum_follower"] = gcoll["mob_vnum_follower"].ToString();
						//drf["reserved"] = gcoll["reserved"].ToString();
						drf["name"] = gcoll["name"].ToString();
						MobsFollowingData.Rows.Add(drf);
					}
					//Поместить предмет в предмет
					m = tp.Match(input);
					if (m.Success)
					{
						GroupCollection gcoll = m.Groups;								
						DataRow drf = PlaceObjInObjData.NewRow();
                        drf["ifflag"] = (gcoll["ifflag"].ToString() == "1");
						drf["obj_vnum"] = gcoll["obj_vnum"].ToString();
						drf["max_in_world"] = gcoll["max_in_world"].ToString();
						drf["objto_vnum"] = gcoll["objto_vnum"].ToString();
						drf["probability"] = gcoll["probability"].ToString();
						drf["name"] = gcoll["name"].ToString();
						PlaceObjInObjData.Rows.Add(drf);
					}
					//Дать предмет мобу (который загружен предыдущей командой)
					m = tg.Match(input);
					if (m.Success)
					{
						GroupCollection gcoll = m.Groups;								
						DataRow drf = PutObjToMobInvData.NewRow();
                        drf["ifflag"] = (gcoll["ifflag"].ToString() == "1");
						drf["obj_vnum"] = gcoll["obj_vnum"].ToString();
						drf["mobto_vnum"] = LastLoadedMob;
						drf["max_in_world"] = gcoll["max_in_world"].ToString();
						drf["probability"] = gcoll["probability"].ToString();
						//drf["reserved"] = gcoll["reserved"].ToString();
						drf["name"] = gcoll["name"].ToString();
						PutObjToMobInvData.Rows.Add(drf);
					}
					//Экипировать предмет мобу (который загружен предыдущей командой)
					m = te.Match(input);
					if (m.Success)
					{
						GroupCollection gcoll = m.Groups;								
						DataRow drf = EquipMobByObjData.NewRow();
                        drf["ifflag"] = (gcoll["ifflag"].ToString() == "1");
						drf["obj_vnum"] = gcoll["obj_vnum"].ToString();
						drf["mobto_vnum"] = LastLoadedMob;
						drf["max_in_world"] = gcoll["max_in_world"].ToString();
						drf["probability"] = gcoll["probability"].ToString();
						drf["name"] = gcoll["name"].ToString();
						EquipMobByObjData.Rows.Add(drf);
					}
					//Удалить предмет из комнаты
					m = tr.Match(input);
					if (m.Success)
					{
						GroupCollection gcoll = m.Groups;								
						DataRow drf = RemoveObjFromRoomData.NewRow();
                        drf["ifflag"] = (gcoll["ifflag"].ToString() == "1");
						drf["room_vnum"] = gcoll["room_vnum"].ToString();
						drf["obj_vnum"] = gcoll["obj_vnum"].ToString();
						//drf["probability"] = gcoll["probability"].ToString();
						//drf["reserved"] = gcoll["reserved"].ToString();
						drf["name"] = gcoll["name"].ToString();
						RemoveObjFromRoomData.Rows.Add(drf);
					}
					//Установить состояние существующего выхода
					m = td.Match(input);
					if (m.Success)
					{
						GroupCollection gcoll = m.Groups;								
						DataRow drf = SetExitStateData.NewRow();
                        drf["ifflag"] = (gcoll["ifflag"].ToString() == "1");
						drf["room_vnum"] = gcoll["room_vnum"].ToString();
						drf["dir"] = gcoll["dir"].ToString();
						drf["door_flag"] = gcoll["door_flag"].ToString();
						//drf["reserved"] = gcoll["reserved"].ToString();
						drf["name"] = gcoll["name"].ToString();
						SetExitStateData.Rows.Add(drf);
					}
				}
			}
			sr.Close();			
		}

		#endregion

		#region LoadDataTrg

		public void LoadDataTrg()
		{
			Regex tnum = new Regex("#(?<Num>\\d+)");
			Regex tname = new Regex("(?<Name>.+)~");
			Regex tparam = new Regex("^(?<trig_class>\\d+) (?<trig_type>.+) (?<num_arg>\\d+)");
            StreamReader sr = new StreamReader(Path + @"\TRG\" + zoneName + ".trg", CurrentEncoding);
			string input;
			int TrigPos = -1;
			int CurTrigNum = -1;
			string TrigBody = "";
			string TrigName = "";
			int TrigClass = -1;
			string TrigType = "";
			int TrigNumArg = -1;
			string TrigArg = "";
			while ((input=sr.ReadLine())!=null) 
			{
				if (input.IndexOf("*") != 0)
				{
					Match m = tnum.Match(input);
					Match m1 = tname.Match(input);
					Match m2 = tparam.Match(input);
					if (m.Success)
					{
						//Запись предыдущего триггера trignumber если таковой был прочитан
						if (CurTrigNum != -1)
						{
							DataRow dr = TriggersData.NewRow();
							dr["vnum"] = CurTrigNum;
							dr["name"] = TrigName;
							dr["class"] = TrigClass;
							dr["type"] = TrigType;
							dr["num_arg"] = TrigNumArg;
							dr["arg"] = TrigArg;
							dr["body"] = TrigBody;
							TriggersData.Rows.Add(dr);
						}

						//Сброс позиции в читаемом триггере
						TrigPos = 0;							
						CurTrigNum = Convert.ToInt32(m.Groups["Num"].ToString());
						TrigBody = "";
					}
						//распознавание всех строк завершающихся ~ с учетом позиции в триггере
					else if (m1.Success)
					{						
						string text = m1.Groups["Name"].ToString();
						if (TrigPos == 0)//Название триггера					
						{
							TrigPos = 1;
							TrigName = text;

						}
						else if (TrigPos == 2)//аргумент
						{
							TrigPos = 3;
							TrigArg = text;
						}
					}
					else if (m2.Success)
					{
						TrigPos = 2;
						GroupCollection gcoll = m2.Groups;								
						TrigClass = Convert.ToInt32(gcoll["trig_class"].ToString());
						TrigType = gcoll["trig_type"].ToString();
						TrigNumArg = Convert.ToInt32(gcoll["num_arg"].ToString());
					}
					else //Тело триггера
					{
						TrigPos++;
						if (input != "~")
						{
							if (TrigBody != "") TrigBody += "\r\n";
							TrigBody += input;
						}
					}
				}
			}
			sr.Close();			
		}

		#endregion

		#region LoadDataShp

		public void LoadDataShp()
		{
			bool NewFormat = false;
			Regex tnumold = new Regex("#\\d+~");
			Regex tnum = new Regex("#(?<Num>\\d+)");
			StreamReader sr = new StreamReader(Path+@"\SHP\"+zoneName+".shp", CurrentEncoding);
			string input;
			int CurShpNum = -1;

            while ((input=sr.ReadLine())!=null) 
			{
                double SellCoeff = 0;
                double BuyCoeff = 0;
                double ChangeCoeff = 0;
                string Msg1 = "";
                string Msg2 = "";
                string Msg3 = "";
                string Msg4 = "";
                string Msg5 = "";
                string Msg6 = "";
                string Msg7 = "";
                int Emotion = 0;
                string Bitvector = "";
                string NotTradeWithBitvector = "";
                int ShopkeeperVNum = -1;
                string OpeningTime1 = "";
                string CloingTime1 = "";
                string OpeningTime2 = "";
                string CloingTime2 = "";

				while (input.IndexOf("#") == -1)//Смещаемся на начало описания магазина
				{
					input=sr.ReadLine();
					if (input == null) break; //если конец файла, то прекращаем искать начало след.шопа
				}
				if (input == null) break;//если конец файла, прекращаем обработку файла
				
				Match mold = tnumold.Match(input);
				if (mold.Success)
					NewFormat = false;
				else
					NewFormat = true;

				Match m = tnum.Match(input);
				if (m.Success)
				{							
					CurShpNum = Convert.ToInt32(m.Groups["Num"].ToString());
				}
				input=sr.ReadLine();
				while (input != "-1") //Читаем список товаров, которые постоянно есть в магазине	(до -1)
				{
					DataRow dr = PermanentlySellingData.NewRow();
					dr["shop_vnum"] = CurShpNum;
					dr["obj_vnum"] = input;
					PermanentlySellingData.Rows.Add(dr);
					input=sr.ReadLine();
				}
				input=sr.ReadLine();
				SellCoeff = Convert.ToDouble(input.Replace(".",",")); //число - коэффициент стоимости при продаже
				input=sr.ReadLine();
				BuyCoeff = Convert.ToDouble(input.Replace(".",",")); //число - коэффициент стоимости при покупке
				if (NewFormat)
				{
					input=sr.ReadLine();
					ChangeCoeff = Convert.ToDouble(input.Replace(".",",")); //число - коэффициент стоимости при обмене
				}
				input=sr.ReadLine();
				while (input != "-1") //тип предмета для покупки или строка.покупаются предметы, 
					//имеющие это слово(а) в имени или флагах предмета. (до -1)
				{
					DataRow dr = BuyingObjectsData.NewRow();
					dr["shop_vnum"] = CurShpNum;
					dr["obj_vnum"] = input.Replace("~",""); //Чистить строку от ~ если пришла строка
					BuyingObjectsData.Rows.Add(dr);
					input=sr.ReadLine();
				}
				if (NewFormat)
				{
					input=sr.ReadLine();
					while (input != "-1") //тип предмета для обмена (аналогично) (до -1)
					{
						DataRow dr = ChangingObjectsData.NewRow();
						dr["shop_vnum"] = CurShpNum;
						dr["obj_vnum"] = input.Replace("~","");
						ChangingObjectsData.Rows.Add(dr);
						input=sr.ReadLine();
					}
				}

				Msg1 = sr.ReadLine().Replace("~",""); //Этого нет в списке товаров!
				Msg2 = sr.ReadLine().Replace("~",""); //Таким барахлом я не торгую!
				Msg3 = sr.ReadLine().Replace("~",""); //Таким барахлом я не торгую!
				Msg4 = sr.ReadLine().Replace("~",""); //Извините, но сейчас у меня нет денег!
				Msg5 = sr.ReadLine().Replace("~",""); //У вас нет столько денег!
				Msg6 = sr.ReadLine().Replace("~",""); //За это с тебя будет %d.
				Msg7 = sr.ReadLine().Replace("~",""); //За это я дам не больше %d!

				input=sr.ReadLine();
				Emotion = Convert.ToInt32(input);

				input=sr.ReadLine(); 
				if (input == "0") 
				{
					Bitvector = "";
				}
				else Bitvector = input; 
				
				input=sr.ReadLine();
				ShopkeeperVNum = Convert.ToInt32(input); //число  - номер продавца
				
				input=sr.ReadLine(); 
				if (input == "0") 
				{
					NotTradeWithBitvector = "";
				}
				else NotTradeWithBitvector = input; //строка - битвектор профессий с которыми НЕ ТОРГУЕТ

				input=sr.ReadLine();
				while (input != "-1") //виртуальные номера комнат магазина(до -1)
				{
					DataRow dr = ShopRoomsData.NewRow();
					dr["shop_vnum"] = CurShpNum;
					dr["room_vnum"] = input;
					ShopRoomsData.Rows.Add(dr);
					input=sr.ReadLine();
				}

				OpeningTime1 = sr.ReadLine(); //число - время открытия 1
				CloingTime1 = sr.ReadLine(); //число - время закрытия 1
				OpeningTime2 = sr.ReadLine(); //число - время открытия 2
				CloingTime2 = sr.ReadLine(); //число - время закрытия 2	

                if (CurShpNum != -1)//Запись предыдущего магазина если таковой был прочитан
                {
                    DataRow dr = ShopsData.NewRow();
                    dr["vnum"] = CurShpNum;
                    dr["sell_coeff"] = SellCoeff;
                    dr["buy_coeff"] = BuyCoeff;
                    dr["change_coeff"] = ChangeCoeff;
                    dr["msg1"] = Msg1;
                    dr["msg2"] = Msg2;
                    dr["msg3"] = Msg3;
                    dr["msg4"] = Msg4;
                    dr["msg5"] = Msg5;
                    dr["msg6"] = Msg6;
                    dr["msg7"] = Msg7;
                    dr["emotion"] = Emotion;
                    dr["bitvector"] = Bitvector;
                    dr["not_trade_with"] = NotTradeWithBitvector;
                    dr["shopkeeper_vnum"] = ShopkeeperVNum;
                    dr["opening_time_1"] = OpeningTime1;
                    dr["closing_time_1"] = CloingTime1;
                    dr["opening_time_2"] = OpeningTime2;
                    dr["closing_time_2"] = CloingTime2;
                    ShopsData.Rows.Add(dr);
                }
			}
			sr.Close();
		}

		#endregion

		#region LoadDataObj

		public void LoadDataObj()
		{
			Regex tnum = new Regex("#(?<Num>\\d+)");
			Regex tdigets = new Regex("\\d+");
			StreamReader sr = new StreamReader(Path+@"\OBJ\"+zoneName+".obj", CurrentEncoding);
			string input;
			int CurObjNum = -1;

			while ((input=sr.ReadLine())!=null) 
			{
                string Alias = "";
                string Imen = "";
                string Rod = "";
                string Dat = "";
                string Vin = "";
                string Tvor = "";
                string Pred = "";
                string Desc = "";
                string Action = "";
                string MagicVector = "";
                string TrenSkill = "0";
                string MaxDurab = "";
                string CurrDurab = "";
                string Material = "";

                string Sex = "";
                string Timer = "";
                string Spell = "";
                string SpellLevel = "";

                string Effects = "";
                string Discomfort = "";
                string Bans = "";

                string Kind = "";	//Тип предмета				
                string Flags = ""; //экстрафлаги
                string WearFlags = ""; //флаги, куда можно одеть

                string Param1 = "";
                string Param2 = "";
                string Param3 = "";
                string Param4 = "";

                string Weight = "";
                string Price = "";
                string RentInv = "";
                string RentWear = "";

				while (input.IndexOf("#") == -1)//Смещаемся на начало описания объекта
				{
					input=sr.ReadLine();
					if (input == null) break; //если конец файла, то прекращаем искать начало след.объекта
				}
				if (input == null) break;//если конец файла, прекращаем обработку файла

				Match m = tnum.Match(input);
				if (m.Success)//строка - виртуальный номер предмета = Номер зоны * 100 + номер предмета в зоне - начинается с #
				{							
					CurObjNum = Convert.ToInt32(m.Groups["Num"].ToString());
				}

				Alias = sr.ReadLine().Replace("~","");//синонимы предмета - на какие названия он будет откликаться - заканчивается ~
				Imen = sr.ReadLine().Replace("~","");//имя предмета в именительном падеже - заканчивается ~
				Rod = sr.ReadLine().Replace("~","");//имя предмета в родительном падеже - заканчивается ~
				Dat = sr.ReadLine().Replace("~","");//имя предмета в дательном падеже - заканчивается ~
				Vin = sr.ReadLine().Replace("~","");//имя предмета в винительном падеже - заканчивается ~
				Tvor = sr.ReadLine().Replace("~","");//имя предмета в творительном падеже - заканчивается ~
				Pred = sr.ReadLine().Replace("~","");//имя предмета в предложном падеже - заканчивается ~
				Desc = sr.ReadLine().Replace("~","");//описание предмета, если он лежит в комнате - заканчивается ~
				//Action = sr.ReadLine().Replace("~","");//описание при действии палочки или посоха - заканчивается ~
				Action = "";
				input=sr.ReadLine();
				while (input != "~")
				{
					if (input.IndexOf("~") >=0)
					{
						Action += input.Replace("~","");
						input = "~";
					}
					else
					{
						if (Action.Length > 0) Action += "\r\n";
						Action += input;
						input=sr.ReadLine();
					}
				}
				input = sr.ReadLine();
				string[] Parts = input.Split(' ');
				m = tdigets.Match(Parts[0]);
				if (Parts[0] == "0")
				{
					TrenSkill = "0";
					MagicVector = "";
				}
				else if (m.Success) //Значит передено число
				{
					TrenSkill = Parts[0];
					MagicVector = "";
				}
				else //Передана строка
				{
					TrenSkill = "0";
					MagicVector = Parts[0];					
				}
				
				MaxDurab = Parts[1]; //Максимальная прочность предмета
				CurrDurab = Parts[2]; //Текущая прочность предмета
				Material = Parts[3]; //Материал, из которого сделан предмет

				input = sr.ReadLine();
				Parts = input.Split(' ');
				Sex = Parts[0];	//пол предмета				
				Timer = Parts[1]; //время жизни предмета в тиках
				Spell = Parts[2]; //спел, кастуемый предметом
				SpellLevel = Parts[3]; //уровень кастуемого спела

				input = sr.ReadLine();
				Parts = input.Split(' ');
				Effects = Parts[0]=="0" ? "": Parts[0];	//аффекты, накладываемые при надевании предмета				
				Discomfort = Parts[1]=="0" ? "": Parts[1]; //флаги неудобств (не может одеть на себя)
				Bans = Parts[2]=="0" ? "": Parts[2]; //флаги запретов (не может даже взять в руки)
				
				input = sr.ReadLine();
				Parts = input.Split(' ');
				Kind = Parts[0];	//Тип предмета	
				/*if (Kind == "25" && TrenSkill != "0") //Для маг.ингр.
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
				Flags = Parts[1]; //экстрафлаги
				WearFlags = Parts[2]; //флаги, куда можно одеть

				input = sr.ReadLine();
				Parts = input.Split(' ');
				Param1 = Parts[0];
				/*Есть в коде старого редактора такая обработка на случай если флаги не числом а строкой
				* Val(Param1, i, j);
				if j <> 0 then
				begin
					j := 0;
					for i := 1 to (Length(Param1) div 2) do 
					j := j + (1 shl (Ord(Param1[i*2-1]) - Ord('a')));
					Param1 := IntToStr(j);
				end;*/
				Param2 = Parts[1];
				Param3 = Parts[2];
				Param4 = Parts[3];

				input = sr.ReadLine();
				Parts = input.Split(' ');
				Weight = Parts[0];
				Price = Parts[1];
				RentInv = Parts[2];
				RentWear = Parts[3];

				input = sr.ReadLine();
				while (input.IndexOf("E") != -1)
				{
					DataRow dr = ObjExtraDesc.NewRow();
					dr["obj_vnum"] = CurObjNum;
					dr["aliases"] = sr.ReadLine().Replace("~","");// ключевое слово - строка заканчиваемая ~
					string ExtraDescTmp = ""; 
					input=sr.ReadLine();
					while (input != "~")
					{
						if (input[0] == 'E')
						{
							ExtraDescTmp += input.Replace("~","");
							input = "~";
						}
						else
						{
							if (ExtraDescTmp.Length > 0) ExtraDescTmp += "\r\n";
							ExtraDescTmp += input;
							input=sr.ReadLine();
						}
					}
					dr["extra_desc"] = ExtraDescTmp;
					ObjExtraDesc.Rows.Add(dr);
					input=sr.ReadLine();
				}
				while (input[0] == 'A')
				{
					input = sr.ReadLine();
					Parts = input.Split(' ');
					DataRow dr = ObjAddParam.NewRow();
					dr["obj_vnum"] = CurObjNum;
					dr["obj_param"] = Parts[0];
					dr["param_val"] = Parts[1];
					ObjAddParam.Rows.Add(dr);
					input=sr.ReadLine();
				}
				while (input[0] == 'T')
				{
					input = sr.ReadLine();
					DataRow dr = ObjTriggers.NewRow();
					dr["obj_vnum"] = CurObjNum;
					dr["trg_vnum"] = input;
					ObjTriggers.Rows.Add(dr);
					input=sr.ReadLine();
				}

                if (CurObjNum != -1)//Запись объекта если таковой был прочитан
                {
                    DataRow dr = ObjectsData.NewRow();
                    dr["vnum"] = CurObjNum;
                    dr["alias"] = Alias;
                    dr["imen"] = Imen;
                    dr["rod"] = Rod;
                    dr["dat"] = Dat;
                    dr["vin"] = Vin;
                    dr["tvor"] = Tvor;
                    dr["pred"] = Pred;
                    dr["desc"] = Desc;
                    dr["action"] = Action;
                    dr["magic_vector"] = MagicVector;
                    dr["tren_skill"] = TrenSkill;
                    dr["max_durab"] = MaxDurab;
                    dr["curr_durab"] = CurrDurab;
                    dr["material"] = Material;
                    dr["sex"] = Sex;
                    dr["timer"] = Timer;
                    dr["spell"] = Spell;
                    dr["spell_level"] = SpellLevel;
                    dr["effects"] = Effects;
                    dr["discomfort"] = Discomfort;
                    dr["bans"] = Bans;
                    dr["kind"] = Kind;
                    dr["flags"] = Flags;
                    dr["wear_flags"] = WearFlags;
                    dr["param1"] = Param1;
                    dr["param2"] = Param2;
                    dr["param3"] = Param3;
                    dr["param4"] = Param4;
                    dr["weight"] = Weight;
                    dr["price"] = Price;
                    dr["rent_inv"] = RentInv;
                    dr["rent_wear"] = RentWear;
                    ObjectsData.Rows.Add(dr);
                }
			}
            sr.Close();
		}

		#endregion

		#region LoadDataWld

		public void LoadDataWld()
		{
			Regex tnum = new Regex("#(?<Num>\\d+)");
			Regex tdescday = new Regex("<day>(?<data>.+)<day>", RegexOptions.Singleline);
			Regex tdescnight = new Regex("<night>(?<data>.+)<night>", RegexOptions.Singleline);
			Regex tdescwinternight = new Regex("<winternight>(?<data>)<winternight>", RegexOptions.Singleline);
			Regex tdescwinterday = new Regex("<winterday>(?<data>)<winterday>", RegexOptions.Singleline);
			Regex tdescspringnight = new Regex("<springnight>(?<data>)<springnight>", RegexOptions.Singleline);
			Regex tdescspringday = new Regex("<springday>(?<data>)<springday>", RegexOptions.Singleline);
			Regex tdescsummernight = new Regex("<summernight>(?<data>)<summernight>", RegexOptions.Singleline);
			Regex tdescsummerday = new Regex("<summerday>(?<data>)<summerday>", RegexOptions.Singleline);
			Regex tdescautumnnight = new Regex("<autumnnight>(?<data>)<autumnnight>", RegexOptions.Singleline);
			Regex tdescautumnday = new Regex("<autumnday>(?<data>)<autumnday>", RegexOptions.Singleline);
			StreamReader sr = new StreamReader(Path+@"\WLD\"+zoneName+".wld", CurrentEncoding);
			int CurRoomNum = -1;
            string input = "";
            while ((input = sr.ReadLine()) != null) 
			{
                string RoomName = "";
                string RoomDesc = "";
                string DescDay = "";
                string DescNight = "";
                string DescWinterNight = "";
                string DescWinterDay = "";
                string DescSpringNight = "";
                string DescSpringDay = "";
                string DescSummerNight = "";
                string DescSummerDay = "";
                string DescAutumnNight = "";
                string DescAutumnDay = "";
                string DescDayR = "false";
                string DescNightR = "false";
                string DescWinterNightR = "false";
                string DescWinterDayR = "false";
                string DescSpringNightR = "false";
                string DescSpringDayR = "false";
                string DescSummerNightR = "false";
                string DescSummerDayR = "false";
                string DescAutumnNightR = "false";
                string DescAutumnDayR = "false";

                string ZoneNum = "";
                string Flags = "";
                string Sector = "";

                string[] Desc = new string[6] { "", "", "", "", "", "" };
                string[] Names = new string[6] { "", "", "", "", "", "" };
                string[] ExitFlags = new string[6] { "", "", "", "", "", "" };
                string[] Keys = new string[6] { "", "", "", "", "", "" };
                string[] ExitRooms = new string[6] { "", "", "", "", "", "" };

                while (input.IndexOf("#") == -1)//Смещаемся на начало описания объекта
				{
					input=sr.ReadLine();
					if (input == null) break; //если конец файла, то прекращаем искать начало след.объекта
				}
				if (input == null) break;//если конец файла, прекращаем обработку файла
				
				Match m = tnum.Match(input);
				if (m.Success)
				{							
					CurRoomNum = Convert.ToInt32(m.Groups["Num"].ToString());
				}
				RoomName = sr.ReadLine().Replace("~","");
				string FullDesc = "";
				input=sr.ReadLine();
				while (input != "~")//Читаем все описание зоны до завершающей тильды
				{
					if (input.IndexOf("~") >=0)
					{
						FullDesc += input.Replace("~","");
						input = "~";
					}
					else
					{
						if (FullDesc.Length > 0) FullDesc += "\r\n";
						FullDesc += input;
						input=sr.ReadLine();
					}
				}
                if (FullDesc.IndexOf("<") >= 0)
                {
                    RoomDesc = FullDesc.Substring(0, FullDesc.IndexOf("<") - 1);
                    m = tdescday.Match(FullDesc);
                    DescDay = m.Groups["data"].ToString();
                    if (DescDay.Length > 0)
                        if (DescDay[0] == 'R')
                        {
                            DescDayR = "true";
                            DescDay = DescDay.Remove(0, 1);
                        }
                    m = tdescnight.Match(FullDesc);
                    DescNight = m.Groups["data"].ToString();
                    if (DescNight.Length > 0)
                        if (DescNight[0] == 'R')
                        {
                            DescNightR = "true";
                            DescNight = DescNight.Remove(0, 1);
                        }
                    m = tdescwinternight.Match(FullDesc);
                    DescWinterNight = m.Groups["data"].ToString();
                    if (DescWinterNight.Length > 0)
                        if (DescWinterNight[0] == 'R')
                        {
                            DescWinterNightR = "true";
                            DescWinterNight = DescWinterNight.Remove(0, 1);
                        }
                    m = tdescwinterday.Match(FullDesc);
                    DescWinterDay = m.Groups["data"].ToString();
                    if (DescWinterDay.Length > 0)
                        if (DescWinterDay[0] == 'R')
                        {
                            DescWinterDayR = "true";
                            DescWinterDay = DescWinterDay.Remove(0, 1);
                        }
                    m = tdescspringnight.Match(FullDesc);
                    DescSpringNight = m.Groups["data"].ToString();
                    if (DescSpringNight.Length > 0)
                        if (DescSpringNight[0] == 'R')
                        {
                            DescSpringNightR = "true";
                            DescSpringNight = DescSpringNight.Remove(0, 1);
                        }
                    m = tdescspringday.Match(FullDesc);
                    DescSpringDay = m.Groups["data"].ToString();
                    if (DescSpringDay.Length > 0)
                        if (DescSpringDay[0] == 'R')
                        {
                            DescSpringDayR = "true";
                            DescSpringDay = DescSpringDay.Remove(0, 1);
                        }
                    m = tdescsummernight.Match(FullDesc);
                    DescSummerNight = m.Groups["data"].ToString();
                    if (DescSummerNight.Length > 0)
                        if (DescSummerNight[0] == 'R')
                        {
                            DescSummerNightR = "true";
                            DescSummerNight = DescSummerNight.Remove(0, 1);
                        }
                    m = tdescsummerday.Match(FullDesc);
                    DescSummerDay = m.Groups["data"].ToString();
                    if (DescSummerDay.Length > 0)
                        if (DescSummerDay[0] == 'R')
                        {
                            DescSummerDayR = "true";
                            DescSummerDay = DescSummerDay.Remove(0, 1);
                        }
                    m = tdescautumnnight.Match(FullDesc);
                    DescAutumnNight = m.Groups["data"].ToString();
                    if (DescAutumnNight.Length > 0)
                        if (DescAutumnNight[0] == 'R')
                        {
                            DescAutumnNightR = "true";
                            DescAutumnNight = DescAutumnNight.Remove(0, 1);
                        }
                    m = tdescautumnday.Match(FullDesc);
                    DescAutumnDay = m.Groups["data"].ToString();
                    if (DescAutumnDay.Length > 0)
                        if (DescAutumnDay[0] == 'R')
                        {
                            DescAutumnDayR = "true";
                            DescAutumnDay = DescAutumnDay.Remove(0, 1);
                        }
                }
                else
                {
                    RoomDesc = FullDesc;
                    DescDay = "";
                    DescNight = "";
                    DescWinterNight = "";
                    DescWinterDay = "";
                    DescSpringNight = "";
                    DescSpringDay = "";
                    DescSummerNight = "";
                    DescSummerDay = "";
                    DescAutumnNight = "";
                    DescAutumnDay = "";
                }

				input = sr.ReadLine();
				string[] Parts = input.Split(' ');
				ZoneNum = Parts[0];	//Номер зоны
				Flags = Parts[1]=="0" ? "": Parts[1]; //флаги комнаты
				Sector = Parts[2]; //тип сектора

				input=sr.ReadLine();
				while (input[0].ToString() == "D")//Читаем выходы
				{
					int exitIndex = Convert.ToInt32(input[1].ToString());//Полоучаем индекс направления c 0 до 5 (север восток юг запад вверх вниз)
					Desc[exitIndex] = sr.ReadLine().Replace("~","");
					Names[exitIndex] = sr.ReadLine().Replace("~","");
					input = sr.ReadLine();//Последняя строка описания выхода
					Parts = input.Split(' ');
					ExitFlags[exitIndex] = Parts[0];
					Keys[exitIndex] = Parts[1];
					ExitRooms[exitIndex] = Parts[2];
					input=sr.ReadLine();
				}
				while (input[0].ToString() == "E")//Читаем дополнения (Extra)
				{	
					DataRow dr = RoomExtraDesc.NewRow();
					dr["room_vnum"] = CurRoomNum;
					dr["alias"] = sr.ReadLine().Replace("~","");
					string ExtraDesc = "";
					input=sr.ReadLine();
					while (input != "~")//Читаем все до завершающей тильды
					{
						if (input.IndexOf("~") >=0)
						{
							FullDesc += input.Replace("~","");
							input = "~";
						}
						else
						{
							if (FullDesc.Length > 0) FullDesc += "\r\n";
							FullDesc += input;
							input=sr.ReadLine();
						}
					}
					dr["desc"] = ExtraDesc;
					RoomExtraDesc.Rows.Add(dr);
					input=sr.ReadLine();
				}
				while (input[0].ToString() != "S")//Смещаемся
				{
					input=sr.ReadLine();
				}
				while (input[0].ToString() == "T")//Читаем триггеры комнаты
				{
					DataRow dr		= RoomTriggers.NewRow();
					dr["room_vnum"] = CurRoomNum;
					dr["trg_vnum"]	= input.Split(' ')[1];
					input=sr.ReadLine();
				}

                if (CurRoomNum != -1)//Запись комнаты если таковой был прочитан
                {
                    DataRow dr = WldData.NewRow();
                    dr["vnum"] = CurRoomNum;
                    dr["room_name"] = RoomName;
                    dr["room_desc"] = RoomDesc;
                    dr["desc_day"] = DescDay;
                    dr["desc_day_r"] = DescDayR;
                    dr["desc_night"] = DescNight;
                    dr["desc_night_r"] = DescNightR;
                    dr["desc_winterday"] = DescWinterDay;
                    dr["desc_winterday_r"] = DescWinterDayR;
                    dr["desc_winternight"] = DescWinterNight;
                    dr["desc_winternight_r"] = DescWinterNightR;
                    dr["desc_springday"] = DescSpringDay;
                    dr["desc_springday_r"] = DescSpringDayR;
                    dr["desc_springnight"] = DescSpringNight;
                    dr["desc_springnight_r"] = DescSpringNightR;
                    dr["desc_summerday"] = DescSummerDay;
                    dr["desc_summerday_r"] = DescSummerDayR;
                    dr["desc_summernight"] = DescSummerNight;
                    dr["desc_summernight_r"] = DescSummerNightR;
                    dr["desc_autumnday"] = DescAutumnDay;
                    dr["desc_autumnday_r"] = DescAutumnDayR;
                    dr["desc_autumnnight"] = DescAutumnNight;
                    dr["desc_autumnnight_r"] = DescAutumnNightR;
                    dr["zone_num"] = ZoneNum;
                    dr["room_flags"] = Flags;
                    dr["sector"] = Sector;

                    dr["exit_n_desc"] = Desc[0];
                    dr["exit_e_desc"] = Desc[1];
                    dr["exit_s_desc"] = Desc[2];
                    dr["exit_w_desc"] = Desc[3];
                    dr["exit_u_desc"] = Desc[4];
                    dr["exit_d_desc"] = Desc[5];

                    dr["exit_n_name"] = Names[0];
                    dr["exit_e_name"] = Names[1];
                    dr["exit_s_name"] = Names[2];
                    dr["exit_w_name"] = Names[3];
                    dr["exit_u_name"] = Names[4];
                    dr["exit_d_name"] = Names[5];

                    dr["exit_flags_n"] = ExitFlags[0];
                    dr["exit_flags_e"] = ExitFlags[1];
                    dr["exit_flags_s"] = ExitFlags[2];
                    dr["exit_flags_w"] = ExitFlags[3];
                    dr["exit_flags_u"] = ExitFlags[4];
                    dr["exit_flags_d"] = ExitFlags[5];

                    dr["exit_key_n"] = Keys[0];
                    dr["exit_key_e"] = Keys[1];
                    dr["exit_key_s"] = Keys[2];
                    dr["exit_key_w"] = Keys[3];
                    dr["exit_key_u"] = Keys[4];
                    dr["exit_key_d"] = Keys[5];

                    dr["exit_room_n"] = ExitRooms[0];
                    dr["exit_room_e"] = ExitRooms[1];
                    dr["exit_room_s"] = ExitRooms[2];
                    dr["exit_room_w"] = ExitRooms[3];
                    dr["exit_room_u"] = ExitRooms[4];
                    dr["exit_room_d"] = ExitRooms[5];

                    WldData.Rows.Add(dr);
                }
			}

			//Загрузка map-файла
			Regex t;
			t = new Regex("(?<vnum>\\d+) (?<x>\\d+) (?<y>\\d+) (?<z>\\d+)");
			sr = new StreamReader(Path+@"\WLD\"+zoneName+".map", CurrentEncoding);
			while ((input=sr.ReadLine())!=null) 
			{
				Match mval = t.Match(input);
				if (mval.Success)
				{
					DataRow dr	= MapData.NewRow();
					dr["vnum"]	=mval.Groups["vnum"].ToString().Trim();
					dr["x"]		=mval.Groups["x"].ToString().Trim();
					dr["y"]		=mval.Groups["y"].ToString().Trim();
					dr["z"]		=mval.Groups["z"].ToString().Trim();
					MapData.Rows.Add(dr);
				}
			}
			sr.Close();
		}

		#endregion

		#region LoadDataMob

		public void LoadDataMob()
		{
			Regex tnum = new Regex("#(?<Num>\\d+)");
			StreamReader sr = new StreamReader(Path+@"\MOB\"+zoneName+".mob", CurrentEncoding);
			string input;
			int CurMobNum = -1;

			while ((input=sr.ReadLine())!=null) 
			{
                string Alias = "";
                string Imen = "";
                string Rod = "";
                string Dat = "";
                string Vin = "";
                string Tvor = "";
                string Pred = "";
                string Desc = "";
                string Look = "";

                string Flags = "";
                string Affects = "";
                string Align = "";

                string Level = "";
                string Hitroll = "";
                string AC = "";
                string Hits = "";
                string Damage = "";

                string Money = "";
                string Exp = "";

                string PosLoad = "";
                string PosDefault = "";
                string Pol = "";

                string BareHandAttack = "";
                string Destination = "";
                string Str = "";
                string Int = "";
                string Wis = "";
                string Dex = "";
                string Con = "";
                string Cha = "";
                string LikeWork = "";
                string MaxFactor = "";
                string ExtraAttack = "";
                string Class = "";
                string Size = "";
                string Height = "";
                string Weight = "";
                string SpecialBitvector = "";

				while (input.IndexOf("#") == -1)//Смещаемся на начало описания объекта
				{
					input=sr.ReadLine();
					if (input == null) break; //если конец файла, то прекращаем искать начало след.объекта
				}
				if (input == null) break;//если конец файла, прекращаем обработку файла
				
				Match m = tnum.Match(input);
				if (m.Success)
				{							
					CurMobNum = Convert.ToInt32(m.Groups["Num"].ToString());
				}
				Alias = sr.ReadLine().Replace("~","");
				Imen = sr.ReadLine().Replace("~","");
				Rod = sr.ReadLine().Replace("~","");
				Dat = sr.ReadLine().Replace("~","");
				Vin = sr.ReadLine().Replace("~","");
				Tvor = sr.ReadLine().Replace("~","");
				Pred = sr.ReadLine().Replace("~","");
				input=sr.ReadLine();
				while (input != "~")//Читаем все описание моба до завершающей тильды
				{
					if (input.IndexOf("~") >= 0)
					{
						Desc += input.Replace("~","");
						input = "~";
					}
					else
					{
						if (Desc.Length > 0) Desc += "\r\n";
						Desc += input;
						input=sr.ReadLine();
					}
				}
				input=sr.ReadLine();
				while (input != "~")//Читаем все описание моба до завершающей тильды
				{
					if (input.IndexOf("~") >= 0)
					{
						Look += input.Replace("~","");
						input = "~";
					}
					else
					{
						if (Desc.Length > 0) Look += "\r\n";
						Look += input;
						input=sr.ReadLine();
					}
				}

				input = sr.ReadLine();
				string[] Parts = input.Split(' ');
				Flags = Parts[0]=="0" ? "": Parts[1]; //флаги
				Affects = Parts[1]=="0" ? "": Parts[1]; //аффекты
				Align = Parts[2];

				input = sr.ReadLine();
				Parts = input.Split(' ');
				Level = Parts[0];
				Hitroll = Parts[1];
				AC = Parts[2];
				Hits = Parts[3];
				Damage = Parts[4];

				input = sr.ReadLine();
				Parts = input.Split(' ');
				Money = Parts[0];
				Exp = Parts[1];

				input = sr.ReadLine();
				Parts = input.Split(' ');
				PosLoad = Parts[0];
				PosDefault = Parts[1];
				Pol = Parts[2];
				
				input=sr.ReadLine();
				while (input != "E")//Читаем все параметры моба до E
				{
					Parts = input.Split(' ');
					switch (input.Split(':')[0])
					{
						case "BareHandAttack":
							BareHandAttack = Parts[1];
							break;
						case "Destination":
							Destination = (Destination == "")?Parts[1]:Destination + "/" + Parts[1];
							break;
						case "Str":
							Str = Parts[1];
							break;
						case "Int":
							Int = Parts[1];
							break;
						case "Wis":
							Wis = Parts[1];
							break;
						case "Dex":
							Dex = Parts[1];
							break;
						case "Con":
							Con = Parts[1];
							break;
						case "Cha":
							Cha = Parts[1];
							break;
						case "LikeWork":
							LikeWork = Parts[1];
							break;
						case "MaxFactor":
							MaxFactor = Parts[1];
							break;
						case "ExtraAttack":
							ExtraAttack = Parts[1];
							break;
						case "Class":
							Class = Parts[1];
							break;
						case "Size":
							Size = Parts[1];
							break;
						case "Height":
							Height = Parts[1];
							break;
						case "Weight":
							Weight = Parts[1];
							break;
						case "Special_Bitvector":
							SpecialBitvector = Parts[1];
							break;
						case "Spell":
							DataRow[] drs = MobSpells.Select("mob_vnum = "+CurMobNum+" and spell_num = " + Parts[1]);
							if (drs.Length > 0)//Инкризим количество спелов
								drs[0]["spell_count"] = Convert.ToInt32(drs[0]["spell_count"])+1;
							else
							{
								DataRow drspell = MobSpells.NewRow();
								drspell["mob_vnum"] = CurMobNum;
								drspell["spell_num"] = Parts[1];
								drspell["spell_count"] = 1;
								MobSpells.Rows.Add(drspell);
							}
							break;
						case "Skill":
							DataRow drskill = MobSkills.NewRow();
							drskill["mob_vnum"] = CurMobNum;
							drskill["skill_num"] = Parts[1];
							drskill["skill_percent"] = Parts[2];
							MobSkills.Rows.Add(drskill);
							break;
					}
					input=sr.ReadLine();
				}

                if (CurMobNum != -1)//Запись моба если таковой был прочитан
                {
                    DataRow dr = MobData.NewRow();
                    dr["vnum"] = CurMobNum;
                    dr["alias"] = Alias;
                    dr["imen"] = Imen;
                    dr["rod"] = Rod;
                    dr["dat"] = Dat;
                    dr["vin"] = Vin;
                    dr["tvor"] = Tvor;
                    dr["pred"] = Pred;
                    dr["desc"] = Desc;
                    dr["look"] = Look;

                    dr["flags"] = Flags;
                    dr["affects"] = Affects;
                    dr["align"] = Align;

                    dr["level"] = Level;
                    dr["hitroll"] = Hitroll;
                    dr["ac"] = AC;
                    dr["hits"] = Hits;
                    dr["damage"] = Damage;

                    dr["money"] = Money;
                    dr["exp"] = Exp;

                    dr["pos_load"] = PosLoad;
                    dr["pos_default"] = PosDefault;
                    dr["pol"] = Pol;

                    dr["bare_hand_attack"] = BareHandAttack;
                    dr["destination"] = Destination;
                    dr["str"] = Str;
                    dr["int"] = Int;
                    dr["wis"] = Wis;
                    dr["dex"] = Dex;
                    dr["con"] = Con;
                    dr["cha"] = Cha;
                    dr["like_work"] = LikeWork;
                    dr["max_factor"] = MaxFactor;
                    dr["extra_attack"] = ExtraAttack;
                    dr["class"] = Class;
                    dr["size"] = Size;
                    dr["height"] = Height;
                    dr["weight"] = Weight;
                    dr["special_bitvector"] = SpecialBitvector;

                    MobData.Rows.Add(dr);
                }
			}
            sr.Close();
		}

		#endregion

        private void CreateDataSchema()
		{
			DS = new DataSet();
			CreateDataSchemaZon();
			CreateDataSchemaTrg();
			CreateDataSchemaShp();
			CreateDataSchemaObj();
			CreateDataSchemaWld();
			CreateDataSchemaMob();
		}


		#region CreateDataSchemaZon

		private void CreateDataSchemaZon()
		{

			DS.Tables.Add("RemovingMobsData");
			RemovingMobsData.Columns.Add("ifflag");
            RemovingMobsData.Columns.Add("mob_vnum");
			
			DS.Tables.Add("LoadingMobsData");
			LoadingMobsData.Columns.Add("ifflag");
			LoadingMobsData.Columns.Add("mob_vnum");
			LoadingMobsData.Columns.Add("max_in_room");
			LoadingMobsData.Columns.Add("room_vnum");
			LoadingMobsData.Columns.Add("max_in_world");
			LoadingMobsData.Columns.Add("name");

			DS.Tables.Add("LoadingObjectsData");
			LoadingObjectsData.Columns.Add("ifflag");
			LoadingObjectsData.Columns.Add("obj_vnum");
			LoadingObjectsData.Columns.Add("max_in_world");
			LoadingObjectsData.Columns.Add("room_vnum");
			LoadingObjectsData.Columns.Add("probability");
			LoadingObjectsData.Columns.Add("name");

			DS.Tables.Add("MobsFollowingData");
			MobsFollowingData.Columns.Add("ifflag");
			MobsFollowingData.Columns.Add("room_vnum");
			MobsFollowingData.Columns.Add("mob_vnum_leader");
			MobsFollowingData.Columns.Add("mob_vnum_follower");
			//MobsFollowingData.Columns.Add("reserved");
			MobsFollowingData.Columns.Add("name");
			
			DS.Tables.Add("PlaceObjInObjData");
			PlaceObjInObjData.Columns.Add("ifflag");
			PlaceObjInObjData.Columns.Add("obj_vnum");
			PlaceObjInObjData.Columns.Add("max_in_world");
			PlaceObjInObjData.Columns.Add("objto_vnum");
			PlaceObjInObjData.Columns.Add("probability");
			PlaceObjInObjData.Columns.Add("name");

			DS.Tables.Add("PutObjToMobInvData");
			PutObjToMobInvData.Columns.Add("ifflag");
			PutObjToMobInvData.Columns.Add("obj_vnum");
			PutObjToMobInvData.Columns.Add("mobto_vnum");
			PutObjToMobInvData.Columns.Add("max_in_world");
			PutObjToMobInvData.Columns.Add("probability");
			//EquipMobByObjData.Columns.Add("reserved");
			PutObjToMobInvData.Columns.Add("name");

			DS.Tables.Add("EquipMobByObjData");
			EquipMobByObjData.Columns.Add("ifflag");
			EquipMobByObjData.Columns.Add("obj_vnum");
			EquipMobByObjData.Columns.Add("mobto_vnum");
			EquipMobByObjData.Columns.Add("max_in_world");
			EquipMobByObjData.Columns.Add("obj_pos");
			EquipMobByObjData.Columns.Add("probability");
			EquipMobByObjData.Columns.Add("name");
			
			DS.Tables.Add("RemoveObjFromRoomData");
			RemoveObjFromRoomData.Columns.Add("ifflag");
			RemoveObjFromRoomData.Columns.Add("room_vnum");
			RemoveObjFromRoomData.Columns.Add("obj_vnum");
			//RemoveObjFromRoomData.Columns.Add("reserved1");
			//RemoveObjFromRoomData.Columns.Add("reserved2");
			RemoveObjFromRoomData.Columns.Add("name");

			DS.Tables.Add("SetExitStateData");
			SetExitStateData.Columns.Add("ifflag");
			SetExitStateData.Columns.Add("room_vnum");
			SetExitStateData.Columns.Add("dir");
			SetExitStateData.Columns.Add("door_flag");
			//SetExitStateData.Columns.Add("reserved");
			SetExitStateData.Columns.Add("name");
		}

		#endregion

		#region CreateDataSchemaTrg

        private void CreateDataSchemaTrg()
		{
			DS.Tables.Add("TriggersData");
			TriggersData.Columns.Add("vnum");
			TriggersData.Columns.Add("name");
			TriggersData.Columns.Add("class");
			TriggersData.Columns.Add("type");
			TriggersData.Columns.Add("num_arg");
			TriggersData.Columns.Add("arg");
			TriggersData.Columns.Add("body");
		}

		#endregion

		#region CreateDataSchemaShp

        private void CreateDataSchemaShp()
		{
			DS.Tables.Add("ShopsData");
			ShopsData.Columns.Add("vnum");
			ShopsData.Columns.Add("sell_coeff");
			ShopsData.Columns.Add("buy_coeff");
			ShopsData.Columns.Add("change_coeff");
			ShopsData.Columns.Add("msg1");
			ShopsData.Columns.Add("msg2");
			ShopsData.Columns.Add("msg3");
			ShopsData.Columns.Add("msg4");
			ShopsData.Columns.Add("msg5");
			ShopsData.Columns.Add("msg6");
			ShopsData.Columns.Add("msg7");
			ShopsData.Columns.Add("emotion");
			ShopsData.Columns.Add("bitvector");
			ShopsData.Columns.Add("not_trade_with");
			ShopsData.Columns.Add("shopkeeper_vnum");
			ShopsData.Columns.Add("opening_time_1");
			ShopsData.Columns.Add("closing_time_1");
			ShopsData.Columns.Add("opening_time_2");
			ShopsData.Columns.Add("closing_time_2");

			//Предметы постоянно в продаже
			DS.Tables.Add("PermanentlySellingData");
			PermanentlySellingData.Columns.Add("shop_vnum");
			PermanentlySellingData.Columns.Add("obj_vnum");

			//Покупаемые предметы
			DS.Tables.Add("BuyingObjectsData");
			BuyingObjectsData.Columns.Add("shop_vnum");
			BuyingObjectsData.Columns.Add("obj_vnum");

			//Предметы на обмен
			DS.Tables.Add("ChangingObjectsData");
			ChangingObjectsData.Columns.Add("shop_vnum");
			ChangingObjectsData.Columns.Add("obj_vnum");

			//Список комнат магазина
			DS.Tables.Add("ShopRoomsData");
			ShopRoomsData.Columns.Add("shop_vnum");
			ShopRoomsData.Columns.Add("room_vnum");

		}

		#endregion

		#region CreateDataSchemaObj

        private void CreateDataSchemaObj()
		{
			DS.Tables.Add("ObjectsData");
			ObjectsData.Columns.Add("vnum");
			ObjectsData.Columns.Add("alias");
			ObjectsData.Columns.Add("imen");
			ObjectsData.Columns.Add("rod");
			ObjectsData.Columns.Add("dat");
			ObjectsData.Columns.Add("vin");
			ObjectsData.Columns.Add("tvor");
			ObjectsData.Columns.Add("pred");
			ObjectsData.Columns.Add("desc");
			ObjectsData.Columns.Add("action");
			ObjectsData.Columns.Add("magic_vector");
			ObjectsData.Columns.Add("tren_skill");
			ObjectsData.Columns.Add("max_durab");
			ObjectsData.Columns.Add("curr_durab");
			ObjectsData.Columns.Add("material");
			ObjectsData.Columns.Add("sex");
			ObjectsData.Columns.Add("timer");
			ObjectsData.Columns.Add("spell");
			ObjectsData.Columns.Add("spell_level");
			ObjectsData.Columns.Add("effects");
			ObjectsData.Columns.Add("discomfort");
			ObjectsData.Columns.Add("bans");
			ObjectsData.Columns.Add("kind");
			ObjectsData.Columns.Add("flags");
			ObjectsData.Columns.Add("wear_flags");
			ObjectsData.Columns.Add("param1");
			ObjectsData.Columns.Add("param2");
			ObjectsData.Columns.Add("param3");
			ObjectsData.Columns.Add("param4");
			ObjectsData.Columns.Add("weight");
			ObjectsData.Columns.Add("price");
			ObjectsData.Columns.Add("rent_inv");
			ObjectsData.Columns.Add("rent_wear");

			//
			DS.Tables.Add("ObjExtraDesc");
			ObjExtraDesc.Columns.Add("obj_vnum");
			ObjExtraDesc.Columns.Add("aliases");
			ObjExtraDesc.Columns.Add("extra_desc");

			//
			DS.Tables.Add("ObjAddParam");
			ObjAddParam.Columns.Add("obj_vnum");
			ObjAddParam.Columns.Add("obj_param");
			ObjAddParam.Columns.Add("param_val");

			//
			DS.Tables.Add("ObjTriggers");
			ObjTriggers.Columns.Add("obj_vnum");
			ObjTriggers.Columns.Add("trg_vnum");

		}

		#endregion

		#region CreateDataSchemaWld

        private void CreateDataSchemaWld()
		{
            DS.Tables.Add("MapData");
            MapData.Columns.Add("vnum");
            MapData.Columns.Add("x");
            MapData.Columns.Add("y");
            MapData.Columns.Add("z");

			DS.Tables.Add("WldData");
			WldData.Columns.Add("vnum");
			WldData.Columns.Add("room_name");
			WldData.Columns.Add("room_desc");
            WldData.Columns.Add("desc_day");
            WldData.Columns.Add("desc_day_r");
            WldData.Columns.Add("desc_night");
            WldData.Columns.Add("desc_night_r");
            WldData.Columns.Add("desc_winterday");
            WldData.Columns.Add("desc_winterday_r");
            WldData.Columns.Add("desc_winternight");
            WldData.Columns.Add("desc_winternight_r");
            WldData.Columns.Add("desc_springday");
            WldData.Columns.Add("desc_springday_r");
            WldData.Columns.Add("desc_springnight");
            WldData.Columns.Add("desc_springnight_r");
            WldData.Columns.Add("desc_summerday");
            WldData.Columns.Add("desc_summerday_r");
            WldData.Columns.Add("desc_summernight");
            WldData.Columns.Add("desc_summernight_r");
            WldData.Columns.Add("desc_autumnday");
            WldData.Columns.Add("desc_autumnday_r");
            WldData.Columns.Add("desc_autumnnight");
            WldData.Columns.Add("desc_autumnnight_r");
            WldData.Columns.Add("zone_num");
            WldData.Columns.Add("room_flags");
			WldData.Columns.Add("sector");

			WldData.Columns.Add("exit_n_desc");
			WldData.Columns.Add("exit_e_desc");
			WldData.Columns.Add("exit_s_desc");
			WldData.Columns.Add("exit_w_desc");
			WldData.Columns.Add("exit_u_desc");
			WldData.Columns.Add("exit_d_desc");

			WldData.Columns.Add("exit_n_name");
			WldData.Columns.Add("exit_e_name");
			WldData.Columns.Add("exit_s_name");
			WldData.Columns.Add("exit_w_name");
			WldData.Columns.Add("exit_u_name");
			WldData.Columns.Add("exit_d_name");

			WldData.Columns.Add("exit_flags_n");
			WldData.Columns.Add("exit_flags_e");
			WldData.Columns.Add("exit_flags_s");
			WldData.Columns.Add("exit_flags_w");
			WldData.Columns.Add("exit_flags_u");
			WldData.Columns.Add("exit_flags_d");
			
			WldData.Columns.Add("exit_key_n");
			WldData.Columns.Add("exit_key_e");
			WldData.Columns.Add("exit_key_s");
			WldData.Columns.Add("exit_key_w");
			WldData.Columns.Add("exit_key_u");
			WldData.Columns.Add("exit_key_d");

			WldData.Columns.Add("exit_room_n");
			WldData.Columns.Add("exit_room_e");
			WldData.Columns.Add("exit_room_s");
			WldData.Columns.Add("exit_room_w");
			WldData.Columns.Add("exit_room_u");
			WldData.Columns.Add("exit_room_d");

			DS.Tables.Add("RoomExtraDesc");
			RoomExtraDesc.Columns.Add("room_vnum");
			RoomExtraDesc.Columns.Add("alias");
			RoomExtraDesc.Columns.Add("desc");

			DS.Tables.Add("RoomTriggers");
			RoomTriggers.Columns.Add("room_vnum");
			RoomTriggers.Columns.Add("trg_vnum");

		}

		#endregion

		#region CreateDataSchemaMob

        private void CreateDataSchemaMob()
		{
			DS.Tables.Add("MobData");
			MobData.Columns.Add("vnum");
			MobData.Columns.Add("alias");
			MobData.Columns.Add("imen");
			MobData.Columns.Add("rod");
			MobData.Columns.Add("dat");
			MobData.Columns.Add("vin");
			MobData.Columns.Add("tvor");
			MobData.Columns.Add("pred");
			MobData.Columns.Add("desc");
			MobData.Columns.Add("look");

			MobData.Columns.Add("flags");
			MobData.Columns.Add("affects");
			MobData.Columns.Add("align");

			MobData.Columns.Add("level");
			MobData.Columns.Add("hitroll");
			MobData.Columns.Add("ac");
			MobData.Columns.Add("hits");
			MobData.Columns.Add("damage");

			MobData.Columns.Add("money");
			MobData.Columns.Add("exp");

			MobData.Columns.Add("pos_load");
			MobData.Columns.Add("pos_default");
			MobData.Columns.Add("pol");

			MobData.Columns.Add("bare_hand_attack");
			MobData.Columns.Add("destination");
			MobData.Columns.Add("str");
			MobData.Columns.Add("int");
			MobData.Columns.Add("wis");
			MobData.Columns.Add("dex");
			MobData.Columns.Add("con");
			MobData.Columns.Add("cha");
			MobData.Columns.Add("like_work");
			MobData.Columns.Add("max_factor");
			MobData.Columns.Add("extra_attack");
			MobData.Columns.Add("class");
			MobData.Columns.Add("size");
			MobData.Columns.Add("height");
			MobData.Columns.Add("weight");
			MobData.Columns.Add("special_bitvector");

			DS.Tables.Add("MobSpells");
			MobSpells.Columns.Add("mob_vnum");
			MobSpells.Columns.Add("spell_num");
			MobSpells.Columns.Add("spell_count");

			DS.Tables.Add("MobSkills");
			MobSkills.Columns.Add("mob_vnum");
			MobSkills.Columns.Add("skill_num");
			MobSkills.Columns.Add("skill_percent");
		}

		#endregion

		public void SaveData()
		{	
		}

		public void Commit()
		{
			this.DS.AcceptChanges();
		}

        #region Zon

        #endregion

        #region Wld
        /// <summary>
        /// Метод для получения списка комнат
        /// </summary>
        /// <param name="ReturnAllRooms"></param>
        /// <returns></returns>
        public DataRow[] GetRoomsList(bool ReturnAllRooms)
        {
            if (ReturnAllRooms)
            {
                return WldData.Select("");
            }
            else
            {
                DataRow[] resdrs = new DataRow[WldData.Rows.Count - MapData.Rows.Count];
                int cntr = 0;
                foreach (DataRow dr in WldData.Rows)
                {
                    if (MapData.Select("vnum = " + dr["vnum"].ToString()).Length == 0)
                    {
                        resdrs[cntr] = dr;
                        cntr++;
                    }
                }
                return resdrs;
            }
        }

        public DataRow GetRoomData(string vnum)
        {
            DataRow[] drs = WldData.Select("vnum = " + vnum);
            if (drs.Length > 0)
                return drs[0];
            else
                return null;
        }

       /* public DataRow GetRoomTriggers(string vnum)
        {
            DataRow[] drs = RoomTriggers.Select("room_vnum = " + vnum);
            DataRow[] resdrs = new DataRow[drs.Length];
            int cntr = 0;
            foreach (DataRow dr in WldData.Rows)
            {
                if (MapData.Select("vnum = " + dr["vnum"].ToString()).Length == 0)
                {
                    resdrs[cntr] = dr;
                    cntr++;
                }
            }
            return resdrs;
        }*/

        #endregion

        #region Mob
        /// <summary>
        /// Метод возвращает данные моба для добавления в листвью
        /// </summary>
        /// <param name="vnum"></param>
        /// <returns></returns>
        public string GetMobNameByVnum(string vnum)
        {
            DataRow[] drsmobs = MobData.Select("vnum = "+vnum);
            if (drsmobs.Length > 0)
                return drsmobs[0]["imen"].ToString();
            return "";
        }

        #endregion
    }
}
