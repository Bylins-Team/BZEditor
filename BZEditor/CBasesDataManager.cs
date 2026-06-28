using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace BZEditor
{
    public class CBasesDataManager
    {
        private readonly Encoding currentEncoding = Encoding.GetEncoding("windows-1251");
        //private Font fnt = new Font("Courier New", 9f, FontStyle.Bold);
        private readonly string[] fourParamsFiles = new string[] {};

        private readonly string[] grouppedFiveParamsFiles =
            new[]
                {
                    "roomvector"
                };

        private readonly string[] grouppedTwoParamsFiles =
            new[]
                {
                    "action", "extraeffect", "wear", "mob_affect", "mob_special_bitvector", "affect", "feats", "bonus",
                    "notake", "nouse", "char_skills", "mob_role"
                };

        private readonly string path;
        private readonly string[] threeParamsFiles = new string[] {};

        private readonly string[] twoParamsFiles = new[]
                                                       {
                                                           "ac", "align", "class", "class_mob",
                                                           "mob_type", "doordefault", "drinktypes",
                                                           "extraattack",
                                                           "level", "likework", "magic_flags", "material", "maxfactor",
                                                           "objects_decaytimer",
                                                           "objects_structurehits", "pol", "size", "skills", "stats",
                                                           "thac0", "type", "weapons",
                                                           "zon_equipped", "zon_repop", "ZON_Type",
                                                           "ZON_ObjLoadType", "ZON_ObjLoadSpecParam",
                                                           "container",
                                                           "position",
                                                           "Room_Ingredients", "Mob_Ingredients",
                                                           "Money_Currency",
                                                           "sectortype", "shop_bitvector", "shop_race_bitvector",
                                                           "trigger_bitvector_mob",
                                                           "trigger_bitvector_obj", "trigger_bitvector_wld",
                                                           "trigger_type", "spells_mob", "skills_mob", "spells"
                                                       };

        public DataSet Ds;
        public ArrayList ListItemsArray;

        public CBasesDataManager(string path)
        {
            this.path = path;
        }

        #region Get/Set

        public DataTable RoomIngredients => Ds.Tables["Room_Ingredients"];

        public DataTable MobIngredients => Ds.Tables["Mob_Ingredients"];

        public DataTable ZonType => Ds.Tables["ZON_Type"];

        public DataTable Ac => Ds.Tables["ac"];

        public DataTable MobFlags => Ds.Tables["action"];

        public DataTable Affect => Ds.Tables["affect"];

        public DataTable Align => Ds.Tables["align"];

        public DataTable Bonus => Ds.Tables["bonus"];

        public DataTable Class => Ds.Tables["class"];

        public DataTable MobClass => Ds.Tables["class_mob"];

        public DataTable MobType => Ds.Tables["mob_type"];

        public DataTable Container => Ds.Tables["container"];

        public DataTable DoorDefault => Ds.Tables["doordefault"];

        public DataTable DrinkTypes => Ds.Tables["drinktypes"];

        public DataTable ExtraAttack => Ds.Tables["extraattack"];

        public DataTable ExtraEffect => Ds.Tables["extraeffect"];

        public DataTable Level => Ds.Tables["level"];

        public DataTable Likework => Ds.Tables["likework"];

        public DataTable MagicFlags => Ds.Tables["magic_flags"];

        public DataTable Material => Ds.Tables["material"];

        public DataTable MoneyCurrency => Ds.Tables["money_currency"];

        public DataTable MaxFactor => Ds.Tables["maxfactor"];

        public DataTable MobAffects => Ds.Tables["mob_affect"];

        public DataTable MobRoles => Ds.Tables["mob_role"];

        public DataTable MobSpecBitvector => Ds.Tables["mob_special_bitvector"];

        public DataTable NoTake => Ds.Tables["notake"];

        public DataTable NoUse => Ds.Tables["nouse"];

        public DataTable ObjectsDecaytimer => Ds.Tables["objects_decaytimer"];

        public DataTable ObjectsStructurehits => Ds.Tables["objects_structurehits"];

        public DataTable Sex => Ds.Tables["pol"];

        public DataTable Position => Ds.Tables["position"];

        public DataTable RoomVector => Ds.Tables["roomvector"];

        public DataTable SectorType => Ds.Tables["sectortype"];

        public DataTable ShopBitvector => Ds.Tables["shop_bitvector"];

        public DataTable ShopHours => Ds.Tables["shop_hours"];

        public DataTable ShopRaceBitvector => Ds.Tables["shop_race_bitvector"];

        public DataTable Size => Ds.Tables["size"];

        public DataTable Skills => Ds.Tables["skills"];

        public DataTable CharSkills => Ds.Tables["char_skills"];

        public DataTable MobSkills => Ds.Tables["skills_mob"];

        public DataTable Spells => Ds.Tables["spells"];

        public DataTable MobSpells => Ds.Tables["spells_mob"];

        public DataTable Stats => Ds.Tables["stats"];

        public DataTable Thac0 => Ds.Tables["thac0"];

        public DataTable MobTriggerBitvector => Ds.Tables["trigger_bitvector_mob"];

        public DataTable ObjTriggerBitvector => Ds.Tables["trigger_bitvector_obj"];

        public DataTable WldTriggerBitvector => Ds.Tables["trigger_bitvector_wld"];

        public DataTable TriggerType => Ds.Tables["trigger_type"];

        public DataTable Type => Ds.Tables["type"];

        public DataTable Weapons => Ds.Tables["weapons"];

        public DataTable Wear => Ds.Tables["wear"];

        public DataTable ZonEquipped => Ds.Tables["zon_equipped"];

        public DataTable ZonObjLoadType => Ds.Tables["zon_objloadtype"];

        public DataTable ZonObjLoadSpecParam => Ds.Tables["zon_objloadspecparam"];

        public DataTable ZonRepop => Ds.Tables["zon_repop"];

        public DataTable MobFeatures => Ds.Tables["feats"];

        #endregion

        public void LoadData()
        {
            ListItemsArray = new ArrayList();
            LoadDgsSintax();
            LoadBases();
        }

        private void LoadDgsSintax()
        {
            var temlate = new Regex("^(?<token>.+)\t(?<color>.+)\t(?<descr>.+)\t(?<autocomlete>.+)");
            var sr1 = new StreamReader(path + @"\Bases\DGSAutocompl.bb", currentEncoding);
            string input;
            while ((input = sr1.ReadLine()) != null)
            {
                if (input.IndexOf("*") != 0)
                {
                    Match m = temlate.Match(input);
                    if (m.Success)
                    {
                        ListItemsArray.Add(
                            m.Groups["token"].ToString());
                    }
                }
            }

            temlate =
                new Regex(
                    "^(?<token>.+)\t(?<insert>.+)\t(?<color>.+)\t(?<type>.+)\t(?<descr>.+)\t(?<result>.+)\t(?<autocomlete>.+)");
            sr1 = new StreamReader(path + @"\Bases\DGSAutocomplCmds.bb", currentEncoding);
            while ((input = sr1.ReadLine()) != null)
            {
                if (input.IndexOf("*") != 0)
                {
                    Match m = temlate.Match(input);
                    if (m.Success)
                    {
                        ListItemsArray.Add(
                            m.Groups["token"].ToString());
                    }
                }
            }
            sr1 = new StreamReader(path + @"\Bases\DGSAutocomplVars.bb", currentEncoding);
            while ((input = sr1.ReadLine()) != null)
            {
                if (input.IndexOf("*") != 0)
                {
                    Match m = temlate.Match(input);
                    if (m.Success)
                    {
                        ListItemsArray.Add(
                            m.Groups["token"].ToString());
                    }
                }
            }
        }

        private void LoadBases()
        {
            CreateDataSchema();
            string input;
            var t = new Regex("(?<val>.+)\t(?<desc>.+)");
            var tR = new Regex("FROM (?<valfrom>.+) TO (?<valto>.+) STEP (?<step>.+)");
            foreach (string fname in twoParamsFiles)
            {
                var sr = new StreamReader(path + @"\Bases\" + fname + ".bb", currentEncoding);
                while ((input = sr.ReadLine()) != null)
                {
                    Match mval = t.Match(input);
                    Match mvalR = tR.Match(input);
                    if (mvalR.Success)
                    {
                        int from = Convert.ToInt32(mvalR.Groups["valfrom"].ToString().Trim());
                        int to = Convert.ToInt32(mvalR.Groups["valto"].ToString().Trim());
                        int step = Convert.ToInt32(mvalR.Groups["step"].ToString().Trim());
                        if (step > 0)
                        {
                            for (int i = from; i <= to; i += step)
                            {
                                DataRow dr = Ds.Tables[fname].NewRow();
                                dr["val"] = i.ToString();
                                dr["desc"] = i.ToString();
                                Ds.Tables[fname].Rows.Add(dr);
                            }
                        }
                        else
                        {
                            for (int i = from; i <= to; i += step)
                            {
                                DataRow dr = Ds.Tables[fname].NewRow();
                                dr["val"] = i.ToString();
                                dr["desc"] = i.ToString();
                                Ds.Tables[fname].Rows.Add(dr);
                            }
                        }
                    }
                    else if (mval.Success && input.IndexOf("*", StringComparison.Ordinal) != 0)
                    {
                        DataRow dr = Ds.Tables[fname].NewRow();
                        dr["val"] = mval.Groups["val"].ToString().Trim();
                        //dr["desc"] = "[" + mval.Groups["val"].ToString().Trim() + "] " + mval.Groups["desc"].ToString().Trim();
                        dr["desc"] = mval.Groups["desc"].ToString().Trim();
                        Ds.Tables[fname].Rows.Add(dr);
                    }
                }
                sr.Close();
            }

            t = new Regex("(?<val>.+)\t(?<par>.+)\t(?<desc>.+)");
            foreach (string fname in threeParamsFiles)
            {
                var sr = new StreamReader(path + @"\Bases\" + fname + ".bb", currentEncoding);
                while ((input = sr.ReadLine()) != null)
                {
                    Match mval = t.Match(input);
                    if (mval.Success && input.IndexOf("*", StringComparison.Ordinal) != 0)
                    {
                        DataRow dr = Ds.Tables[fname].NewRow();
                        dr["val"] = mval.Groups["val"].ToString().Trim();
                        dr["par"] = mval.Groups["par"].ToString().Trim();
                        //dr["desc"] = "[" + mval.Groups["val"].ToString().Trim() + "] " + mval.Groups["desc"].ToString().Trim();
                        dr["desc"] = mval.Groups["desc"].ToString().Trim();
                        Ds.Tables[fname].Rows.Add(dr);
                    }
                }
                sr.Close();
            }

            t = new Regex("(?<val>.+)\t(?<par1>\\d+)\t(?<par2>\\d+)\t(?<desc>.+)");
            foreach (string fname in fourParamsFiles)
            {
                var sr = new StreamReader(path + @"\Bases\" + fname + ".bb", currentEncoding);
                while ((input = sr.ReadLine()) != null)
                {
                    Match mval = t.Match(input);
                    if (mval.Success && input.IndexOf("*", StringComparison.Ordinal) != 0)
                    {
                        DataRow dr = Ds.Tables[fname].NewRow();
                        dr["val"] = mval.Groups["val"].ToString().Trim();
                        dr["par1"] = mval.Groups["par1"].ToString().Trim();
                        dr["par2"] = mval.Groups["par2"].ToString().Trim();
                        //dr["desc"] = "[" + mval.Groups["val"].ToString().Trim() + "] " + mval.Groups["desc"].ToString().Trim();
                        dr["desc"] = mval.Groups["desc"].ToString().Trim();
                        Ds.Tables[fname].Rows.Add(dr);
                    }
                }
                sr.Close();
            }

            string group = "Прочее";
            t = new Regex("(?<val>.+)\t(?<desc>.+)");
            foreach (string fname in grouppedTwoParamsFiles)
            {
                var sr = new StreamReader(path + @"\Bases\" + fname + ".bb", currentEncoding);
                while ((input = sr.ReadLine()) != null)
                {
                    Match mval = t.Match(input);
                    if (mval.Success && input.IndexOf("*", StringComparison.Ordinal) != 0)
                    {
                        if (mval.Groups["val"].ToString().Trim().ToLower() == "g")
                            group = mval.Groups["desc"].ToString().Trim();
                        else
                        {
                            DataRow dr = Ds.Tables[fname].NewRow();
                            dr["val"] = mval.Groups["val"].ToString().Trim();
                            //dr["desc"] = "[" + mval.Groups["val"].ToString().Trim() + "] " + mval.Groups["desc"].ToString().Trim();
                            dr["desc"] = mval.Groups["desc"].ToString().Trim();
                            dr["group"] = group;
                            Ds.Tables[fname].Rows.Add(dr);
                        }
                    }
                }
                sr.Close();
            }

            group = "Прочее";
            t = new Regex("(?<val>.+)\t(?<desc>.+)\t(?<forecolor>.+)\t(?<backcolor>.+)\t(?<order>.+)");
            var tgr = new Regex("[G|g]\t(?<desc>.+)");
            foreach (string fname in grouppedFiveParamsFiles)
            {
                var sr = new StreamReader(path + @"\Bases\" + fname + ".bb", currentEncoding);
                while ((input = sr.ReadLine()) != null)
                {
                    Match mvalgr = tgr.Match(input);
                    if (mvalgr.Success && input.IndexOf("*", StringComparison.Ordinal) != 0)
                        group = mvalgr.Groups["desc"].ToString().Trim();
                    else
                    {
                        Match mval = t.Match(input);
                        if (mval.Success && input.IndexOf("*", StringComparison.Ordinal) != 0)
                        {
                            DataRow dr = Ds.Tables[fname].NewRow();
                            dr["val"] = mval.Groups["val"].ToString().Trim();
                            //dr["desc"] = "[" + mval.Groups["val"].ToString().Trim() + "] " + mval.Groups["desc"].ToString().Trim();
                            dr["desc"] = mval.Groups["desc"].ToString().Trim();
                            dr["group"] = group;
                            dr["forecolor"] = mval.Groups["forecolor"].ToString().Trim();
                            dr["backcolor"] = mval.Groups["backcolor"].ToString().Trim();
                            dr["order"] = mval.Groups["order"].ToString().Trim();
                            Ds.Tables[fname].Rows.Add(dr);
                        }
                    }
                }
                sr.Close();
            }

            Commit();
        }

        public void CreateDataSchema()
        {
            Ds = new DataSet();

            foreach (string tname in twoParamsFiles)
            {
                Ds.Tables.Add(tname);
                Ds.Tables[tname].Columns.Add("val");
                Ds.Tables[tname].Columns.Add("desc");
            }

            foreach (string tname in threeParamsFiles)
            {
                Ds.Tables.Add(tname);
                Ds.Tables[tname].Columns.Add("val");
                Ds.Tables[tname].Columns.Add("par");
                Ds.Tables[tname].Columns.Add("desc");
            }

            foreach (string tname in fourParamsFiles)
            {
                Ds.Tables.Add(tname);
                Ds.Tables[tname].Columns.Add("val");
                Ds.Tables[tname].Columns.Add("par1");
                Ds.Tables[tname].Columns.Add("par2");
                Ds.Tables[tname].Columns.Add("desc");
            }

            foreach (string tname in grouppedTwoParamsFiles)
            {
                Ds.Tables.Add(tname);
                Ds.Tables[tname].Columns.Add("val");
                Ds.Tables[tname].Columns.Add("desc");
                Ds.Tables[tname].Columns.Add("group");
            }

            foreach (string tname in grouppedFiveParamsFiles)
            {
                Ds.Tables.Add(tname);
                Ds.Tables[tname].Columns.Add("val");
                Ds.Tables[tname].Columns.Add("desc");
                Ds.Tables[tname].Columns.Add("group");
                Ds.Tables[tname].Columns.Add("forecolor");
                Ds.Tables[tname].Columns.Add("backcolor");
                Ds.Tables[tname].Columns.Add("order");
            }
        }

        public void SaveData()
        {
        }

        public void Commit()
        {
            Ds.AcceptChanges();
        }

        public string GetObjectType(string num)
        {
            DataRow[] drs = Type.Select("val = '" + num + "'");
            return drs.Length > 0 ? drs[0][1].ToString() : "";
        }

        public string GetSkillNameByNum(int num)
        {
            DataRow[] drs = MobSkills.Select("val = '" + num + "'");
            return drs.Length > 0 ? drs[0][1].ToString() : "";
        }

        public string GetSpellNameByNum(int num)
        {
            DataRow[] drs = MobSpells.Select("val = '" + num + "'");
            return drs.Length > 0 ? drs[0][1].ToString() : "";
        }

        public string GetBonusNameByNum(int num)
        {
            DataRow[] drs = Bonus.Select("val = '" + num + "'");
            return drs.Length > 0 ? drs[0][1].ToString() : "";
        }

        public string GetSkillBonusNameByNum(int num)
        {
            DataRow[] drs = CharSkills.Select("val = '" + num + "'");
            return drs.Length > 0 ? drs[0][1].ToString() : "";
        }

        public string GetObjPosNameByNum(int num)
        {
            DataRow[] drs = ZonEquipped.Select("val = '" + num + "'");
            return drs.Length > 0 ? drs[0][1].ToString() : "";
        }

        public int GetObjPosNumByName(string name)
        {
            DataRow[] drs = ZonEquipped.Select("desc = '" + name + "'");
            return drs.Length > 0 ? Convert.ToInt32(drs[0]["val"].ToString()) : -1;
        }

        public string GetObjLoadTypeNameByNum(int num)
        {
            DataRow[] drs = ZonObjLoadType.Select("val = '" + num + "'");
            return drs.Length > 0 ? drs[0][1].ToString() : "";
        }

        public int GetObjLoadTypeNumByName(string name)
        {
            DataRow[] drs = ZonObjLoadType.Select("desc = '" + name + "'");
            return drs.Length > 0 ? Convert.ToInt32(drs[0]["val"].ToString()) : -1;
        }
        public string GetObjLoadSpecParamNameByNum(int num)
        {
            DataRow[] drs = ZonObjLoadSpecParam.Select("val = '" + num + "'");
            return drs.Length > 0 ? drs[0][1].ToString() : "";
        }

        public int GetObjLoadSpecParamNumByName(string name)
        {
            DataRow[] drs = ZonObjLoadSpecParam.Select("desc = '" + name + "'");
            return drs.Length > 0 ? Convert.ToInt32(drs[0]["val"].ToString()) : -1;
        }
    }
}