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
                VNum = obj.VNum,
                Alias = obj.Alias ?? ""
            };

            // Names (Russian grammatical cases)
            yaml.Names["imen"] = obj.Cases.Imen ?? "";
            yaml.Names["rod"] = obj.Cases.Rod ?? "";
            yaml.Names["dat"] = obj.Cases.Dat ?? "";
            yaml.Names["vin"] = obj.Cases.Vin ?? "";
            yaml.Names["tvor"] = obj.Cases.Tvor ?? "";
            yaml.Names["pred"] = obj.Cases.Pred ?? "";

            yaml.ShortDesc = obj.Desc ?? "";
            yaml.ActionDesc = obj.ActionDesc ?? "";
            yaml.Type = obj.Type;

            // Extra flags (split space-separated string to list)
            if (!string.IsNullOrEmpty(obj.Affects))
            {
                foreach (var flag in obj.Affects.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                    yaml.ExtraFlags.Add(flag);
            }

            // Wear flags
            if (!string.IsNullOrEmpty(obj.WearFlags))
            {
                foreach (var flag in obj.WearFlags.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                    yaml.WearFlags.Add(flag);
            }

            // No-touch flags
            if (!string.IsNullOrEmpty(obj.CantTouch))
            {
                foreach (var flag in obj.CantTouch.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                    yaml.NoFlags.Add(flag);
            }

            // Anti-use flags
            if (!string.IsNullOrEmpty(obj.CantUse))
            {
                foreach (var flag in obj.CantUse.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                    yaml.AntiFlags.Add(flag);
            }

            // Affect flags (magic flags)
            if (!string.IsNullOrEmpty(obj.MagicFlags))
            {
                foreach (var flag in obj.MagicFlags.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                    yaml.AffectFlags.Add(flag);
            }

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
            yaml.Sex = obj.Sex;
            yaml.MaxInWorld = obj.MaxInWorld;
            yaml.MinimumRemorts = obj.MinimumRemorts;

            // Applies (bonuses)
            foreach (Bonus bonus in obj.BonusesCollection)
            {
                yaml.Applies.Add(new YamlObjApply(bonus.VNum, bonus.Value));
            }

            // Extra descriptions
            foreach (ExtraDesc ed in obj.ExtraDescriptions)
            {
                yaml.ExtraDescriptions.Add(new YamlExtraDesc
                {
                    Keywords = ed.Aliases ?? "",
                    Description = ed.Description ?? ""
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

            var obj = new Obj(yaml.VNum)
            {
                Alias = yaml.Alias ?? ""
            };

            // Names
            if (yaml.Names != null)
            {
                string val;
                if (yaml.Names.TryGetValue("imen", out val)) obj.Cases.Imen = val;
                if (yaml.Names.TryGetValue("rod", out val)) obj.Cases.Rod = val;
                if (yaml.Names.TryGetValue("dat", out val)) obj.Cases.Dat = val;
                if (yaml.Names.TryGetValue("vin", out val)) obj.Cases.Vin = val;
                if (yaml.Names.TryGetValue("tvor", out val)) obj.Cases.Tvor = val;
                if (yaml.Names.TryGetValue("pred", out val)) obj.Cases.Pred = val;
            }

            obj.Desc = yaml.ShortDesc ?? "";
            obj.ActionDesc = yaml.ActionDesc ?? "";
            obj.Type = yaml.Type;

            // Extra flags (join list to space-separated string)
            if (yaml.ExtraFlags != null && yaml.ExtraFlags.Count > 0)
                obj.Affects = string.Join(" ", yaml.ExtraFlags.ToArray());

            // Wear flags
            if (yaml.WearFlags != null && yaml.WearFlags.Count > 0)
                obj.WearFlags = string.Join(" ", yaml.WearFlags.ToArray());

            // No-touch flags
            if (yaml.NoFlags != null && yaml.NoFlags.Count > 0)
                obj.CantTouch = string.Join(" ", yaml.NoFlags.ToArray());

            // Anti-use flags
            if (yaml.AntiFlags != null && yaml.AntiFlags.Count > 0)
                obj.CantUse = string.Join(" ", yaml.AntiFlags.ToArray());

            // Affect flags (magic flags)
            if (yaml.AffectFlags != null && yaml.AffectFlags.Count > 0)
                obj.MagicFlags = string.Join(" ", yaml.AffectFlags.ToArray());

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
            obj.Sex = yaml.Sex;
            obj.MaxInWorld = yaml.MaxInWorld;
            obj.MinimumRemorts = yaml.MinimumRemorts;

            // Applies (bonuses)
            if (yaml.Applies != null)
            {
                foreach (var apply in yaml.Applies)
                    obj.BonusesCollection.Add(new Bonus(apply.Location, apply.Modifier));
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
