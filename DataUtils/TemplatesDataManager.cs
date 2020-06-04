using System;
using System.IO;

namespace DataUtils
{
    public class TemplatesDataManager
    {
        #region Delegates

        public delegate void MobClipboardStateChangeEvent(Mob mob);

        public delegate void TrgClipboardStateChangeEvent(Trigger trg);

        public delegate void ObjClipboardStateChangeEvent(Obj obj);

        public delegate void RoomClipboardStateChangeEvent(Room room);

        public delegate void TemplatesStateChangeEvent(TemplatesType type);

        #endregion

        #region TemplatesType enum

        public enum TemplatesType
        {
            Both,
            Object,
            Mob
        }

        #endregion

        private readonly string path;

        public MobsCollection ConstMobsTemplates = new MobsCollection();
        public ObjsCollection ConstObjectsTemplates = new ObjsCollection();
        //private string[] FourParamsFiles = new string[] {};
        public Mob MobClip;
        public Trigger TrgClip;
        public Obj ObjClip;
        public Room RoomClip;
        //private string[] ThreeParamsFiles = new string[] {};
        public MobsCollection UserMobsTemplates = new MobsCollection();
        public ObjsCollection UserObjectsTemplates = new ObjsCollection();

        public TemplatesDataManager(string path)
        {
            this.path = path;
        }

        #region Get/Set

        #endregion

        public event TemplatesStateChangeEvent TemplatesStateChanged;
        public event MobClipboardStateChangeEvent MobClipboardStateChanged;
        public event TrgClipboardStateChangeEvent TrgClipboardStateChanged;
        public event ObjClipboardStateChangeEvent ObjClipboardStateChanged;
        public event RoomClipboardStateChangeEvent RoomClipboardStateChanged;

        public void LoadData()
        {
            LoadMobTemplates(path + @"\Mob.ct", false);
            LoadMobTemplates(path + @"\Mob.ut", true);
            LoadObjTemplates(path + @"\Obj.ct", false);
            LoadObjTemplates(path + @"\Obj.ut", true);
        }

        public void SaveData()
        {
            SaveMobTemplates();
            SaveObjTemplates();
        }

        #region Mob

        public void LoadMobTemplates(string name, bool userTemplates)
        {
            var fi = new FileInfo(name);
            if (!fi.Exists) return;
            var sr = new StreamReader(fi.OpenRead(), StaticData.CurrentEncoding);
            string input = sr.ReadLine();
            Mob mt = null;
            while (input != null)
            {
                string[] parts = input.Split('\t');
                switch (parts[0])
                {
                    case "GUID":
                        if (mt != null)
                        {
                            if (userTemplates)
                                UserMobsTemplates.Add(mt);
                            else
                                ConstMobsTemplates.Add(mt);
                        }
                        mt = new Mob(-1) {Guid = new Guid(parts[1])};
                        break;
                    case "Name":
                        if (mt != null) mt.Cases.Imen = parts[1];
                        break;
                    case "Absorbe":
                        if (mt != null) mt.Absorbe = Convert.ToInt32(parts[1]);
                        break;
                    case "Affects":
                        if (mt != null) mt.Affects = parts[1];
                        break;
                    case "Align":
                        if (mt != null) mt.Align = Convert.ToInt32(parts[1]);
                        break;
                    case "AResist":
                        if (mt != null) mt.AResist = Convert.ToInt32(parts[1]);
                        break;
                    case "Armour":
                        if (mt != null) mt.Armour = Convert.ToInt32(parts[1]);
                        break;
                    case "BareHandAttack":
                        if (mt != null) mt.BareHandAttack = Convert.ToInt32(parts[1]);
                        break;
                    case "CastSuccess":
                        if (mt != null) mt.CastSuccess = Convert.ToInt32(parts[1]);
                        break;
                    case "Class":
                        if (mt != null) mt.Class = Convert.ToInt32(parts[1]);
                        break;
                    case "Damage":
                        if (mt != null) mt.Damage = parts[1];
                        break;
                    case "Exp":
                        if (mt != null) mt.Exp = Convert.ToInt32(parts[1]);
                        break;
                    case "ExtraAttack":
                        if (mt != null) mt.ExtraAttack = Convert.ToInt32(parts[1]);
                        break;
                    case "Feat":
                        if (mt != null) mt.Feats.Add(Convert.ToInt32(parts[1]));
                        break;
                    case "Hitroll":
                        if (mt != null) mt.Hitroll = Convert.ToInt32(parts[1]);
                        break;
                    case "Hits":
                        if (mt != null) mt.Hits = parts[1];
                        break;
                    case "HPreg":
                        if (mt != null) mt.HPreg = Convert.ToInt32(parts[1]);
                        break;
                    case "Immunitet":
                        if (mt != null) mt.Immunitet = Convert.ToInt32(parts[1]);
                        break;
                    case "Initiative":
                        if (mt != null) mt.Initiative = Convert.ToInt32(parts[1]);
                        break;
                    case "Level":
                        if (mt != null) mt.Level = Convert.ToInt32(parts[1]);
                        break;
                    case "LikeWork":
                        if (mt != null) mt.LikeWork = Convert.ToInt32(parts[1]);
                        break;
                    case "MaxFactor":
                        if (mt != null) mt.MaxFactor = Convert.ToInt32(parts[1]);
                        break;
                    case "Mind":
                        if (mt != null) mt.Mind = Convert.ToInt32(parts[1]);
                        break;
                    case "Money":
                        if (mt != null) mt.Money = parts[1];
                        break;
                    case "MResist":
                        if (mt != null) mt.MResist = Convert.ToInt32(parts[1]);
                        break;
                    case "PlusMem":
                        if (mt != null) mt.PlusMem = Convert.ToInt32(parts[1]);
                        break;
                    case "PosDefault":
                        if (mt != null) mt.PosDefault = Convert.ToInt32(parts[1]);
                        break;
                    case "PosLoad":
                        if (mt != null) mt.PosLoad = Convert.ToInt32(parts[1]);
                        break;
                    case "ResistFromAir":
                        if (mt != null) mt.ResistFromAir = Convert.ToInt32(parts[1]);
                        break;
                    case "ResistFromEarth":
                        if (mt != null) mt.ResistFromEarth = Convert.ToInt32(parts[1]);
                        break;
                    case "ResistFromFire":
                        if (mt != null) mt.ResistFromFire = Convert.ToInt32(parts[1]);
                        break;
                    case "ResistFromWater":
                        if (mt != null) mt.ResistFromWater = Convert.ToInt32(parts[1]);
                        break;
                    case "SaveFightSkills":
                        if (mt != null) mt.SaveFightSkills = Convert.ToInt32(parts[1]);
                        break;
                    case "SaveMagBreathes":
                        if (mt != null) mt.SaveMagBreathes = Convert.ToInt32(parts[1]);
                        break;
                    case "SaveMagDamages":
                        if (mt != null) mt.SaveMagDamages = Convert.ToInt32(parts[1]);
                        break;
                    case "SaveParalyzeCast":
                        if (mt != null) mt.SaveParalyzeCast = Convert.ToInt32(parts[1]);
                        break;
                    case "Sex":
                        if (mt != null) mt.Sex = Convert.ToInt32(parts[1]);
                        break;
                    case "Skill":
                        if (mt != null) mt.Skills.Add(Convert.ToInt32(parts[1]), Convert.ToInt32(parts[2]));
                        break;
                    case "Spell":
                        if (mt != null) mt.Spells.Add(Convert.ToInt32(parts[1]), Convert.ToInt32(parts[2]));
                        break;
                    case "Cha":
                        if (mt != null) mt.Stats.Cha = Convert.ToInt32(parts[1]);
                        break;
                    case "Con":
                        if (mt != null) mt.Stats.Con = Convert.ToInt32(parts[1]);
                        break;
                    case "Dex":
                        if (mt != null) mt.Stats.Dex = Convert.ToInt32(parts[1]);
                        break;
                    case "Int":
                        if (mt != null) mt.Stats.Int = Convert.ToInt32(parts[1]);
                        break;
                    case "Str":
                        if (mt != null) mt.Stats.Str = Convert.ToInt32(parts[1]);
                        break;
                    case "Wis":
                        if (mt != null) mt.Stats.Wis = Convert.ToInt32(parts[1]);
                        break;
                    case "Height":
                        if (mt != null) mt.Stats.Height = Convert.ToInt32(parts[1]);
                        break;
                    case "Size":
                        if (mt != null) mt.Stats.Size = Convert.ToInt32(parts[1]);
                        break;
                    case "Weight":
                        if (mt != null) mt.Stats.Weight = Convert.ToInt32(parts[1]);
                        break;
                    case "Success":
                        if (mt != null) mt.Luck = Convert.ToInt32(parts[1]);
                        break;
                    case "Vitality":
                        if (mt != null) mt.Vitality = Convert.ToInt32(parts[1]);
                        break;
                }
                input = sr.ReadLine();
                if (mt != null && input == null)
                {
                    if (userTemplates)
                        UserMobsTemplates.Add(mt);
                    else
                        ConstMobsTemplates.Add(mt);
                }
            }
            sr.Close();
            TemplatesStateChanged?.Invoke(TemplatesType.Mob);
        }

        private void SaveMobTemplates()
        {
            var fs = new FileStream(path + @"\Mob.ut", FileMode.Create, FileAccess.Write);
            var sw = new StreamWriter(fs, StaticData.CurrentEncoding);
            foreach (Mob m in UserMobsTemplates)
            {
                sw.WriteLine("GUID	" + m.Guid);
                sw.WriteLine("Name	" + m.Cases.Imen);
                sw.WriteLine("Absorbe	" + m.Absorbe);
                sw.WriteLine("Affects	" + m.Affects);
                sw.WriteLine("Align	" + m.Align);
                sw.WriteLine("AResist	" + m.AResist);
                sw.WriteLine("Armour	" + m.Armour);
                sw.WriteLine("BareHandAttack	" + m.BareHandAttack);
                sw.WriteLine("CastSuccess	" + m.CastSuccess);
                sw.WriteLine("Class	" + m.Class);
                sw.WriteLine("Damage	" + m.Damage);
                sw.WriteLine("Exp	" + m.Exp);
                sw.WriteLine("ExtraAttack	" + m.ExtraAttack);
                foreach (int feat in m.Feats)
                    sw.WriteLine("Feat	" + feat);
                sw.WriteLine("Flags	" + m.Flags);
                sw.WriteLine("Hitroll	" + m.Hitroll);
                sw.WriteLine("Hits	" + m.Hits);
                sw.WriteLine("HPreg	" + m.HPreg);
                sw.WriteLine("Immunitet	" + m.Immunitet);
                sw.WriteLine("Initiative	" + m.Initiative);
                sw.WriteLine("Level	" + m.Level);
                sw.WriteLine("LikeWork	" + m.LikeWork);
                sw.WriteLine("MaxFactor	" + m.MaxFactor);
                sw.WriteLine("Mind	" + m.Mind);
                sw.WriteLine("Money	" + m.Money);
                sw.WriteLine("MResist	" + m.MResist);
                sw.WriteLine("PlusMem	" + m.PlusMem);
                sw.WriteLine("PosDefault	" + m.PosDefault);
                sw.WriteLine("PosLoad	" + m.PosLoad);
                sw.WriteLine("ResistFromAir	" + m.ResistFromAir);
                sw.WriteLine("ResistFromEarth	" + m.ResistFromEarth);
                sw.WriteLine("ResistFromFire	" + m.ResistFromFire);
                sw.WriteLine("ResistFromWater	" + m.ResistFromWater);
                sw.WriteLine("SaveFightSkills	" + m.SaveFightSkills);
                sw.WriteLine("SaveMagBreathes	" + m.SaveMagBreathes);
                sw.WriteLine("SaveMagDamages	" + m.SaveMagDamages);
                sw.WriteLine("SaveParalyzeCast	" + m.SaveParalyzeCast);
                sw.WriteLine("Sex	" + m.Sex);
                foreach (MobSkill ms in m.Skills)
                    sw.WriteLine("Skill	" + ms.VNum + "	" + ms.Percent);
                foreach (MobSpell ms in m.Spells)
                    sw.WriteLine("Spell	" + ms.VNum + "	" + ms.Count);
                sw.WriteLine("Cha	" + m.Stats.Cha);
                sw.WriteLine("Con	" + m.Stats.Con);
                sw.WriteLine("Dex	" + m.Stats.Dex);
                sw.WriteLine("Int	" + m.Stats.Int);
                sw.WriteLine("Str	" + m.Stats.Str);
                sw.WriteLine("Wis	" + m.Stats.Wis);
                sw.WriteLine("Height	" + m.Stats.Height);
                sw.WriteLine("Size	" + m.Stats.Size);
                sw.WriteLine("Weight	" + m.Stats.Weight);
                sw.WriteLine("Success	" + m.Luck);
                sw.WriteLine("Vitality	" + m.Vitality);
            }
            sw.Close();
        }

        public Mob GetUserMobTemplate(string name)
        {
            foreach (Mob m in UserMobsTemplates)
            {
                if (m.Cases.Imen.ToLower() == name.ToLower())
                    return m;
            }
            return null;
        }

        public Mob GetUserMobTemplate(Guid guid)
        {
            foreach (Mob m in UserMobsTemplates)
            {
                if (m.Guid == guid)
                    return m;
            }
            return null;
        }

        private Mob GetMobTemplate(Guid guid)
        {
            foreach (Mob m in UserMobsTemplates)
            {
                if (m.Guid == guid)
                    return m;
            }
            foreach (Mob m in ConstMobsTemplates)
            {
                if (m.Guid == guid)
                    return m;
            }
            return null;
        }

        public void AddTemplate(Mob m, string templateName)
        {
            if (m == null) return;
            Mob mt = GetUserMobTemplate(templateName) ?? new Mob(-1);
            mt.Cases.Imen = templateName;
            ApplyTemplate(ref mt, m);
            UserMobsTemplates.Add(mt);
            SaveMobTemplates();
            TemplatesStateChanged?.Invoke(TemplatesType.Mob);
        }

        public void ApplyTemplate(ref Mob mt, Guid g)
        {
            Mob m = GetMobTemplate(g);
            if (mt != null && m != null)
                ApplyTemplate(ref mt, m);
        }

        public void ApplyClipAsTemplate(ref Mob mt, bool fullCopy)
        {
            Mob m = MobClip;
            if (mt == null || m == null) return;
            ApplyTemplate(ref mt, m);
            if (fullCopy) //Точная копия
            {
                mt.Alias = m.Alias;
                mt.Cases = m.Cases.Clone();
                mt.Desc = m.Desc;
                mt.DetailDescr = m.DetailDescr;
                mt.Feats = (BaseDataArrayList) (m.Feats.Clone());
                mt.TriggersList = (BaseDataArrayList) (m.TriggersList.Clone());
            }
            mt.Reactivate();
        }

        public void ApplyTemplate(ref Mob mt, Mob m)
        {
            mt.Absorbe = m.Absorbe;
            mt.Ac = m.Ac;
            mt.Affects = m.Affects;
            mt.Align = m.Align;
            mt.AResist = m.AResist;
            mt.Armour = m.Armour;
            mt.BareHandAttack = m.BareHandAttack;
            mt.CastSuccess = m.CastSuccess;
            mt.Class = m.Class;
            mt.Damage = m.Damage;
            mt.Exp = m.Exp;
            mt.ExtraAttack = m.ExtraAttack;
            mt.Feats = (BaseDataArrayList) (m.Feats.Clone());
            mt.Flags = m.Flags;
            mt.Hitroll = m.Hitroll;
            mt.Hits = m.Hits;
            mt.HPreg = m.HPreg;
            mt.Immunitet = m.Immunitet;
            mt.Initiative = m.Initiative;
            mt.Level = m.Level;
            mt.LikeWork = m.LikeWork;
            mt.MaxFactor = m.MaxFactor;
            mt.Mind = m.Mind;
            mt.Money = m.Money;
            mt.MResist = m.MResist;
            mt.PlusMem = m.PlusMem;
            mt.PosDefault = m.PosDefault;
            mt.PosLoad = m.PosLoad;
            mt.Race = m.Race;
            mt.ResistFromAir = m.ResistFromAir;
            mt.ResistFromEarth = m.ResistFromEarth;
            mt.ResistFromFire = m.ResistFromFire;
            mt.ResistFromWater = m.ResistFromWater;
            mt.SaveFightSkills = m.SaveFightSkills;
            mt.SaveMagBreathes = m.SaveMagBreathes;
            mt.SaveMagDamages = m.SaveMagDamages;
            mt.SaveParalyzeCast = m.SaveParalyzeCast;
            mt.Sex = m.Sex;
            mt.Skills = m.Skills.Clone();
            mt.SpecialBitvector = m.SpecialBitvector;
            mt.Spells = m.Spells.Clone();
            mt.Stats = m.Stats.Clone();
            mt.Luck = m.Luck;
            mt.Vitality = m.Vitality;
            mt.Reactivate();
        }

        public void RemoveMobTemplate(Guid[] guids)
        {
            bool changed = false;
            foreach (Guid g in guids)
            {
                Mob mt = GetUserMobTemplate(g);
                if (mt == null) continue;
                UserMobsTemplates.Remove(mt);
                changed = true;
            }
            if (TemplatesStateChanged != null && changed)
                TemplatesStateChanged(TemplatesType.Mob);
        }

        public void AddToClipboard(Mob mob)
        {
            MobClip = mob;
            MobClipboardStateChanged?.Invoke(MobClip);
        }

        #endregion

        #region Obj

        public void LoadObjTemplates(string name, bool userTemplates)
        {
            var fi = new FileInfo(name);
            if (!fi.Exists) return;
            var sr = new StreamReader(fi.OpenRead(), StaticData.CurrentEncoding);
            string input = sr.ReadLine();
            Obj ot = null;
            while (input != null)
            {
                string[] parts = input.Split('\t');
                switch (parts[0])
                {
                    case "GUID":
                        if (ot != null)
                        {
                            if (userTemplates)
                                UserObjectsTemplates.Add(ot);
                            else
                                ConstObjectsTemplates.Add(ot);
                        }
                        ot = new Obj(-1) {Guid = new Guid(parts[1])};
                        break;
                    case "Name":
                        if (ot != null) ot.Cases.Imen = parts[1];
                        break;
                    case "ActionDesc":
                        if (ot != null) ot.ActionDesc = parts[1];
                        break;
                    case "Alias":
                        if (ot != null) ot.Alias = parts[1];
                        break;
                    case "Bonus":
                        if (ot != null)
                            ot.BonusesCollection.Add(Convert.ToInt32(parts[1]), Convert.ToInt32(parts[2]));
                        break;
                    case "CantTouch":
                        if (ot != null) ot.CantTouch = parts[1];
                        break;
                    case "CantUse":
                        if (ot != null) ot.CantUse = parts[1];
                        break;
                    case "CurrDurab":
                        if (ot != null) ot.CurrDurab = Convert.ToInt32(parts[1]);
                        break;
                    case "Effects":
                        if (ot != null) ot.ExctraEffects = parts[1];
                        break;
                    case "Flags":
                        if (ot != null) ot.Affects = parts[1];
                        break;
                    case "Material":
                        if (ot != null) ot.Material = Convert.ToInt32(parts[1]);
                        break;
                    case "MaxDurab":
                        if (ot != null) ot.MaxDurab = Convert.ToInt32(parts[1]);
                        break;
                    case "Param1":
                        if (ot != null) ot.Param1 = parts[1];
                        break;
                    case "Param2":
                        if (ot != null) ot.Param2 = parts[1];
                        break;
                    case "Param3":
                        if (ot != null) ot.Param3 = parts[1];
                        break;
                    case "Param4":
                        if (ot != null) ot.Param4 = parts[1];
                        break;
                    case "Price":
                        if (ot != null) ot.Price = Convert.ToInt32(parts[1]);
                        break;
                    case "RentInv":
                        if (ot != null) ot.RentInv = Convert.ToInt32(parts[1]);
                        break;
                    case "Sex":
                        if (ot != null) ot.Sex = Convert.ToInt32(parts[1]);
                        break;
                    case "Spell":
                        if (ot != null) ot.Spell = Convert.ToInt32(parts[1]);
                        break;
                    case "SpellLevel":
                        if (ot != null) ot.SpellLevel = Convert.ToInt32(parts[1]);
                        break;
                    case "Timer":
                        if (ot != null) ot.Timer = Convert.ToInt32(parts[1]);
                        break;
                    case "TrenSkill":
                        if (ot != null) ot.TrenSkill = Convert.ToInt32(parts[1]);
                        break;
                    case "Type":
                        if (ot != null) ot.Type = Convert.ToInt32(parts[1]);
                        break;
                    case "WearFlags":
                        if (ot != null) ot.WearFlags = parts[1];
                        break;
                    case "Weight":
                        if (ot != null) ot.Weight = Convert.ToInt32(parts[1]);
                        break;
                }
                input = sr.ReadLine();
                if (ot != null && input == null)
                {
                    if (userTemplates)
                        UserObjectsTemplates.Add(ot);
                    else
                        ConstObjectsTemplates.Add(ot);
                }
            }
            sr.Close();
            TemplatesStateChanged?.Invoke(TemplatesType.Object);
        }

        private void SaveObjTemplates()
        {
            var fs = new FileStream(path + @"\Obj.ut", FileMode.Create, FileAccess.Write);
            var sw = new StreamWriter(fs, StaticData.CurrentEncoding);
            foreach (Obj o in UserObjectsTemplates)
            {
                sw.WriteLine("GUID	" + o.Guid);
                sw.WriteLine("Name	" + o.Cases.Imen);
                sw.WriteLine("ActionDesc	" + o.ActionDesc);
                sw.WriteLine("Alias	" + o.Alias);
                foreach (Bonus bonus in o.BonusesCollection)
                    sw.WriteLine("Bonus	" + bonus.VNum + "	" + bonus.Value);
                sw.WriteLine("CantTouch	" + o.CantTouch);
                sw.WriteLine("CantUse	" + o.CantUse);
                sw.WriteLine("CurrDurab	" + o.CurrDurab);
                //sw.WriteLine("Desc	" + o.Desc);
                sw.WriteLine("Effects	" + o.ExctraEffects);
                /*foreach (CExtraDesc ed in o.ExtraDescriptions)
                    sw.WriteLine("ExtraDescription	" + ed.Effects + "	" + ed.Description);*/
                sw.WriteLine("Flags	" + o.Affects);
                sw.WriteLine("MagicFlags	" + o.MagicFlags);
                sw.WriteLine("Material	" + o.Material);
                sw.WriteLine("MaxDurab	" + o.MaxDurab);
                sw.WriteLine("Param1	" + o.Param1);
                sw.WriteLine("Param2	" + o.Param2);
                sw.WriteLine("Param3	" + o.Param3);
                sw.WriteLine("Param4	" + o.Param4);
                sw.WriteLine("Price	" + o.Price);
                sw.WriteLine("RentInv	" + o.RentInv);
                sw.WriteLine("RentWear	" + o.RentWear);
                sw.WriteLine("Sex	" + o.Sex);
                sw.WriteLine("Spell	" + o.Spell);
                sw.WriteLine("SpellLevel	" + o.SpellLevel);
                sw.WriteLine("Timer	" + o.Timer);
                sw.WriteLine("TrenSkill	" + o.TrenSkill);
                sw.WriteLine("Type	" + o.Type);
                sw.WriteLine("WearFlags	" + o.WearFlags);
                sw.WriteLine("Weight	" + o.Weight);
            }
            sw.Close();
            fs.Close();
            sw.Dispose();
            fs.Dispose();
        }

        public Obj GetUserObjTemplate(string name)
        {
            foreach (Obj o in UserObjectsTemplates)
            {
                if (o.Cases.Imen.ToLower() == name.ToLower())
                    return o;
            }
            return null;
        }

        public Obj GetUserObjTemplate(Guid guid)
        {
            foreach (Obj o in UserObjectsTemplates)
            {
                if (o.Guid == guid)
                    return o;
            }
            return null;
        }

        private Obj GetObjTemplate(Guid guid)
        {
            foreach (Obj o in UserObjectsTemplates)
            {
                if (o.Guid == guid)
                    return o;
            }
            foreach (Obj o in ConstObjectsTemplates)
            {
                if (o.Guid == guid)
                    return o;
            }
            return null;
        }

        public void AddTemplate(Obj obj, string templateName)
        {
            if (obj == null) return;
            Obj ot = GetUserObjTemplate(templateName) ?? new Obj(-1);
            ot.Cases.Imen = templateName;
            ApplyTemplate(ref ot, obj);
            UserObjectsTemplates.Add(ot);
            SaveObjTemplates();
            TemplatesStateChanged?.Invoke(TemplatesType.Object);
        }

        public void ApplyTemplate(ref Obj ot, Guid g)
        {
            Obj os = GetObjTemplate(g);
            if (ot != null && os != null)
                ApplyTemplate(ref ot, os);
        }

        /// <summary>
        /// Применить к переданному объекту объект из буфера как шаблон
        /// </summary>
        public void ApplyClipAsTemplate(ref Obj ot, bool isFullCopy)
        {
            Obj os = ObjClip;
            if (ot != null && os != null)
            {
                ApplyTemplate(ref ot, os);
                if (isFullCopy)
                {
                    ot.Cases = os.Cases.Clone();
                    ot.Alias = os.Alias;
                    ot.ExtraDescriptions = (ExtraDescCollection) (os.ExtraDescriptions.Clone());
                    ot.TriggersList = (BaseDataArrayList) (os.TriggersList.Clone());
                }
                ot.Reactivate();
            }
        }

        public void ApplyTemplate(ref Obj ot, Obj os)
        {
            ot.ActionDesc = os.ActionDesc;
            ot.BonusesCollection.Clear();
            ot.BonusesCollection = os.BonusesCollection.Clone();
            ot.CantTouch = os.CantTouch;
            ot.CantUse = os.CantUse;
            ot.CurrDurab = os.CurrDurab;
            ot.Desc = os.Desc;
            ot.ExctraEffects = os.ExctraEffects;
            ot.Affects = os.Affects;
            ot.MagicFlags = os.MagicFlags;
            ot.Material = os.Material;
            ot.MaxDurab = os.MaxDurab;
            ot.Param1 = os.Param1;
            ot.Param2 = os.Param2;
            ot.Param3 = os.Param3;
            ot.Param4 = os.Param4;
            ot.Price = os.Price;
            ot.RentInv = os.RentInv;
            ot.RentWear = os.RentWear;
            ot.Sex = os.Sex;
            ot.Spell = os.Spell;
            ot.SpellLevel = os.SpellLevel;
            ot.Timer = os.Timer;
            ot.TrenSkill = os.TrenSkill;
            ot.Type = os.Type;
            ot.WearFlags = os.WearFlags;
            ot.Weight = os.Weight;
            ot.Reactivate();
        }

        public void RemoveObjTemplate(Guid[] guids)
        {
            bool changed = false;
            foreach (Guid g in guids)
            {
                Obj ot = GetUserObjTemplate(g);
                if (ot == null) continue;
                UserObjectsTemplates.Remove(ot);
                changed = true;
            }
            if (TemplatesStateChanged != null && changed)
                TemplatesStateChanged(TemplatesType.Object);
        }

        public void AddToClipboard(Obj obj)
        {
            ObjClip = obj;
            ObjClipboardStateChanged?.Invoke(ObjClip);
        }

        #endregion

        #region Room

        public void AddToClipboard(Room room)
        {
            RoomClip = room;
            RoomClipboardStateChanged?.Invoke(RoomClip);
        }

        public void ApplyClipAsTemplate(ref Room tr)
        {
            if (tr == null) return;
            tr.Description = RoomClip.Description.Clone();
            tr.ExitColors = RoomClip.ExitColors.Clone();
            tr.ExitDown = RoomClip.ExitDown.Clone();
            tr.ExitEast = RoomClip.ExitEast.Clone();
            tr.ExitNorth = RoomClip.ExitNorth.Clone();
            tr.ExitSouth = RoomClip.ExitSouth.Clone();
            tr.ExitUp = RoomClip.ExitUp.Clone();
            tr.ExitWest = RoomClip.ExitWest.Clone();
            tr.ExtraDescriptions = (ExtraDescCollection) (RoomClip.ExtraDescriptions.Clone());
            tr.Flags = RoomClip.Flags;
            tr.Name = RoomClip.Name;
            tr.SectorType = RoomClip.SectorType;
            tr.ZoneNum = RoomClip.ZoneNum;
            tr.Reactivate();
        }

        #endregion  
        
        #region Trigger

        public void AddToClipboard(Trigger trg)
        {
            TrgClip = trg;
            TrgClipboardStateChanged?.Invoke(TrgClip);
        }

        public void ApplyClipAsTemplate(ref Trigger trg)
        {
            if (trg == null) return;
            trg.Arg = TrgClip.Arg;        
            trg.Body = TrgClip.Body;
            trg.Class = TrgClip.Class;
            trg.NumArg = TrgClip.NumArg;
            trg.Type = TrgClip.Type;
            trg.Name = "Копия триггера \"" + TrgClip.Name + "\"";
        }

        #endregion
    }
}