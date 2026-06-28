using System;
using DataUtils.YamlModels;

namespace DataUtils.YamlMappers
{
    /// <summary>
    /// Mapper for Obj (item) - matches reference format
    /// </summary>
    public static class YamlObjMapper
    {
        public static YamlObj ToYaml(Obj obj)
        {
            if (obj == null) return null;

            var yaml = new YamlObj
            {
                VNum = obj.VNum
            };

            // Names: aliases + the six Russian grammatical cases, using the engine's key names.
            yaml.Names["aliases"] = obj.Alias ?? "";
            yaml.Names["nominative"] = obj.Cases.Imen ?? "";
            yaml.Names["genitive"] = obj.Cases.Rod ?? "";
            yaml.Names["dative"] = obj.Cases.Dat ?? "";
            yaml.Names["accusative"] = obj.Cases.Vin ?? "";
            yaml.Names["instrumental"] = obj.Cases.Tvor ?? "";
            yaml.Names["prepositional"] = obj.Cases.Pred ?? "";

            yaml.ShortDesc = obj.Desc ?? "";
            yaml.ActionDesc = string.IsNullOrEmpty(obj.ActionDesc) ? null : obj.ActionDesc;
            yaml.Type = EngineCodec.EnumName(obj.Type, EngineDictionaries.ObjTypes);

            // Flag bitvectors as engine symbolic names
            foreach (var name in EngineCodec.FlagsToNames(obj.Affects, EngineDictionaries.ExtraFlags))
                yaml.ExtraFlags.Add(name);
            foreach (var name in EngineCodec.FlagsToNames(obj.WearFlags, EngineDictionaries.WearFlags))
                yaml.WearFlags.Add(name);
            foreach (var name in EngineCodec.FlagsToNames(obj.CantTouch, EngineDictionaries.NoFlags))
                yaml.NoFlags.Add(name);
            foreach (var name in EngineCodec.FlagsToNames(obj.CantUse, EngineDictionaries.AntiFlags))
                yaml.AntiFlags.Add(name);
            foreach (var name in EngineCodec.FlagsToNames(obj.MagicFlags, EngineDictionaries.AffectFlags))
                yaml.AffectFlags.Add(name);

            yaml.Material = obj.Material;

            // Values (param1-4 as array)
            int val1, val2, val3, val4;
            int.TryParse(obj.Param1, out val1);
            int.TryParse(obj.Param2, out val2);
            int.TryParse(obj.Param3, out val3);
            int.TryParse(obj.Param4, out val4);
            yaml.Values.Add(val1);
            yaml.Values.Add(val2);
            yaml.Values.Add(val3);
            yaml.Values.Add(val4);

            yaml.Weight = obj.Weight;
            yaml.Cost = obj.Price;
            yaml.RentOff = obj.RentInv;
            yaml.RentOn = obj.RentWear;
            yaml.SpecParam = 0; // Not in current model
            yaml.MaxDurability = obj.MaxDurab;
            yaml.CurDurability = obj.CurrDurab;
            yaml.Timer = obj.Timer;
            yaml.Spell = obj.Spell;
            yaml.Level = obj.SpellLevel;
            yaml.Sex = EngineCodec.EnumName(obj.Sex, EngineDictionaries.Genders);
            yaml.MaxInWorld = obj.MaxInWorld;
            yaml.MinimumRemorts = obj.MinimumRemorts;

            // Applies (bonuses)
            foreach (Bonus bonus in obj.BonusesCollection)
            {
                yaml.Applies.Add(new YamlObjApply(bonus.VNum, bonus.Value));
            }

            // Skills granted by the object (legacy 'S' lines) - engine key: skills
            foreach (Bonus skill in obj.SkillBonusesCollection)
            {
                yaml.Skills.Add(new YamlMobSkill(skill.VNum, skill.Value));
            }

            // Extra descriptions
            foreach (ExtraDesc ed in obj.ExtraDescriptions)
            {
                yaml.ExtraDescriptions.Add(new YamlExtraDesc
                {
                    Keywords = ed.Aliases ?? "",
                    Description = (ed.Description ?? "").TrimEnd('\r', '\n')
                });
            }

            // Triggers
            foreach (var trig in obj.TriggersList)
            {
                if (trig is int t)
                    yaml.Triggers.Add(t);
            }

            return yaml;
        }

        public static Obj FromYaml(YamlObj yaml)
        {
            if (yaml == null) return null;

            var obj = new Obj(yaml.VNum);

            // Names
            if (yaml.Names != null)
            {
                string val;
                if (yaml.Names.TryGetValue("aliases", out val)) obj.Alias = val ?? "";
                if (yaml.Names.TryGetValue("nominative", out val)) obj.Cases.Imen = val;
                if (yaml.Names.TryGetValue("genitive", out val)) obj.Cases.Rod = val;
                if (yaml.Names.TryGetValue("dative", out val)) obj.Cases.Dat = val;
                if (yaml.Names.TryGetValue("accusative", out val)) obj.Cases.Vin = val;
                if (yaml.Names.TryGetValue("instrumental", out val)) obj.Cases.Tvor = val;
                if (yaml.Names.TryGetValue("prepositional", out val)) obj.Cases.Pred = val;
            }

            obj.Desc = yaml.ShortDesc ?? "";
            obj.ActionDesc = yaml.ActionDesc ?? "";
            obj.Type = EngineCodec.EnumValue(yaml.Type, EngineDictionaries.ObjTypes, 12);

            // Flag bitvectors from engine names back to asciiflag strings
            obj.Affects = EngineCodec.NamesToFlags(yaml.ExtraFlags, EngineDictionaries.ExtraFlags);
            obj.WearFlags = EngineCodec.NamesToFlags(yaml.WearFlags, EngineDictionaries.WearFlags);
            obj.CantTouch = EngineCodec.NamesToFlags(yaml.NoFlags, EngineDictionaries.NoFlags);
            obj.CantUse = EngineCodec.NamesToFlags(yaml.AntiFlags, EngineDictionaries.AntiFlags);
            obj.MagicFlags = EngineCodec.NamesToFlags(yaml.AffectFlags, EngineDictionaries.AffectFlags);

            obj.Material = yaml.Material;

            // Values to params
            if (yaml.Values != null && yaml.Values.Count >= 4)
            {
                obj.Param1 = yaml.Values[0].ToString();
                obj.Param2 = yaml.Values[1].ToString();
                obj.Param3 = yaml.Values[2].ToString();
                obj.Param4 = yaml.Values[3].ToString();
            }

            obj.Weight = yaml.Weight;
            obj.Price = yaml.Cost;
            obj.RentInv = yaml.RentOff;
            obj.RentWear = yaml.RentOn;
            obj.MaxDurab = yaml.MaxDurability;
            obj.CurrDurab = yaml.CurDurability;
            obj.Timer = yaml.Timer;
            obj.Spell = yaml.Spell;
            obj.SpellLevel = yaml.Level;
            obj.Sex = EngineCodec.EnumValue(yaml.Sex, EngineDictionaries.Genders);
            obj.MaxInWorld = yaml.MaxInWorld;
            obj.MinimumRemorts = yaml.MinimumRemorts;

            // Applies (bonuses)
            if (yaml.Applies != null)
            {
                foreach (var apply in yaml.Applies)
                    obj.BonusesCollection.Add(new Bonus(apply.Location, apply.Modifier));
            }

            // Skills granted by the object (legacy 'S' lines)
            if (yaml.Skills != null)
            {
                foreach (var skill in yaml.Skills)
                    obj.AddSkillBonus(skill.SkillId, skill.Value);
            }

            // Extra descriptions
            if (yaml.ExtraDescriptions != null)
            {
                foreach (var ed in yaml.ExtraDescriptions)
                    obj.ExtraDescriptions.Add(new ExtraDesc(ed.Keywords ?? "", ed.Description ?? ""));
            }

            // Triggers
            if (yaml.Triggers != null)
            {
                foreach (var trig in yaml.Triggers)
                    obj.TriggersList.Add(trig);
            }

            return obj;
        }
    }
}
