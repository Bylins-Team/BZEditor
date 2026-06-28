using System;
using System.Collections.Generic;
using DataUtils.YamlModels;
using SystemFrameworks;

namespace DataUtils.YamlMappers
{
    /// <summary>
    /// Mapper for Mob (NPC) - matches reference format
    /// </summary>
    public static class YamlMobMapper
    {
        private static readonly string[] DirectionNames = { "north", "east", "south", "west", "up", "down" };

        public static YamlMob ToYaml(Mob mob)
        {
            if (mob == null) return null;

            var yaml = new YamlMob
            {
                VNum = mob.VNum
            };

            // Names: aliases + the six Russian grammatical cases, using the engine's key names.
            // TrimEnd: trailing whitespace in a name is junk (some worlds carry a stray tab)
            // and YAML strips it on the next read, so trim on write to keep re-saves stable.
            yaml.Names["aliases"] = (mob.Alias ?? "").TrimEnd();
            yaml.Names["nominative"] = (mob.Cases.Imen ?? "").TrimEnd();
            yaml.Names["genitive"] = (mob.Cases.Rod ?? "").TrimEnd();
            yaml.Names["dative"] = (mob.Cases.Dat ?? "").TrimEnd();
            yaml.Names["accusative"] = (mob.Cases.Vin ?? "").TrimEnd();
            yaml.Names["instrumental"] = (mob.Cases.Tvor ?? "").TrimEnd();
            yaml.Names["prepositional"] = (mob.Cases.Pred ?? "").TrimEnd();

            // Descriptions: engine short_desc = room one-liner, long_desc = look text
            yaml.Descriptions = new YamlMobDescriptions
            {
                ShortDesc = (mob.Desc ?? "").TrimEnd('\r', '\n'),
                LongDesc = (mob.DetailDescr ?? "").TrimEnd('\r', '\n')
            };

            // Action / affect flags as engine symbolic names
            foreach (var name in EngineCodec.FlagsToNames(mob.Flags, EngineDictionaries.ActionFlags))
                yaml.ActionFlags.Add(name);
            foreach (var name in EngineCodec.FlagsToNames(mob.Affects, EngineDictionaries.AffectFlags))
                yaml.AffectFlags.Add(name);

            yaml.Alignment = mob.Align;
            yaml.MobType = "E"; // Extended mob type

            // Stats with dice objects
            yaml.Stats = new YamlMobStats
            {
                Level = mob.Level,
                HitrollPenalty = mob.Hitroll,
                Armor = mob.Ac,
                Hp = YamlDice.Parse(mob.Hits),
                Damage = YamlDice.Parse(mob.Damage)
            };

            // Gold as dice
            yaml.Gold = YamlDice.Parse(mob.Money);

            yaml.Experience = mob.Exp;

            // Position (engine names)
            yaml.Position = new YamlMobPosition
            {
                Default = EngineCodec.EnumName(mob.PosDefault, EngineDictionaries.Positions),
                Start = EngineCodec.EnumName(mob.PosLoad, EngineDictionaries.Positions)
            };

            yaml.Sex = EngineCodec.EnumName(mob.Sex, EngineDictionaries.Genders);

            // Movement speed (-1 = default cadence, omitted)
            if (mob.Speed != -1)
                yaml.Speed = mob.Speed;

            // Attributes from MobStats (engine key names)
            yaml.Attributes["strength"] = mob.Stats.Str;
            yaml.Attributes["dexterity"] = mob.Stats.Dex;
            yaml.Attributes["intelligence"] = mob.Stats.Int;
            yaml.Attributes["wisdom"] = mob.Stats.Wis;
            yaml.Attributes["constitution"] = mob.Stats.Con;
            yaml.Attributes["charisma"] = mob.Stats.Cha;

            yaml.Size = mob.Stats.Size;
            yaml.Height = mob.Stats.Height;
            yaml.Weight = mob.Stats.Weight;
            yaml.MobClass = mob.Class;
            yaml.Race = mob.Race;
            yaml.MaxInWorld = mob.MaxInWorld;

            // Skills
            foreach (MobSkill skill in mob.Skills)
            {
                yaml.Skills.Add(new YamlMobSkill(skill.VNum, skill.Percent));
            }

            // Triggers
            foreach (var trig in mob.TriggersList)
            {
                if (trig is int t)
                    yaml.Triggers.Add(t);
            }

            // Enhanced E-spec fields
            var enhanced = new YamlMobEnhanced();
            bool hasEnhanced = false;

            if (mob.HPreg != 0) { enhanced.HpRegen = mob.HPreg; hasEnhanced = true; }
            if (mob.Armour != 0) { enhanced.ArmourBonus = mob.Armour; hasEnhanced = true; }
            if (mob.PlusMem != 0) { enhanced.ManaRegen = mob.PlusMem; hasEnhanced = true; }
            if (mob.CastSuccess != 0) { enhanced.CastSuccess = mob.CastSuccess; hasEnhanced = true; }
            if (mob.Luck != 0) { enhanced.Morale = mob.Luck; hasEnhanced = true; }
            if (mob.Initiative != 0) { enhanced.InitiativeAdd = mob.Initiative; hasEnhanced = true; }
            if (mob.Absorbe != 0) { enhanced.Absorb = mob.Absorbe; hasEnhanced = true; }
            if (mob.AResist != 0) { enhanced.Aresist = mob.AResist; hasEnhanced = true; }
            if (mob.MResist != 0) { enhanced.Mresist = mob.MResist; hasEnhanced = true; }
            if (mob.PResist != 0) { enhanced.Presist = mob.PResist; hasEnhanced = true; }
            if (mob.BareHandAttack != 0) { enhanced.BareHandAttack = mob.BareHandAttack; hasEnhanced = true; }
            if (mob.LikeWork != 0) { enhanced.LikeWork = mob.LikeWork; hasEnhanced = true; }
            if (mob.MaxFactor != 0) { enhanced.MaxFactor = mob.MaxFactor; hasEnhanced = true; }
            if (mob.ExtraAttack != 0) { enhanced.ExtraAttack = mob.ExtraAttack; hasEnhanced = true; }
            if (!string.IsNullOrEmpty(mob.SpecialBitvector)) { enhanced.SpecialBitvector = mob.SpecialBitvector; hasEnhanced = true; }

            // Role bitvector as a 9-char binary string (same encoding as the legacy "Role:" line)
            int roleBits = 0;
            foreach (int r in mob.Roles) roleBits += Convert.ToInt32(Math.Pow(2, r - 1));
            if (roleBits > 0) { enhanced.Role = BinaryUtils.NumberToBinary(roleBits, 9); hasEnhanced = true; }

            // Resistances: engine-named map, non-zero entries only.
            var resistances = new YamlResistMap();
            if (mob.ResistFromFire != 0) resistances["kFire"] = mob.ResistFromFire;
            if (mob.ResistFromAir != 0) resistances["kAir"] = mob.ResistFromAir;
            if (mob.ResistFromWater != 0) resistances["kWater"] = mob.ResistFromWater;
            if (mob.ResistFromEarth != 0) resistances["kEarth"] = mob.ResistFromEarth;
            if (mob.ResistDark != 0) resistances["kDark"] = mob.ResistDark;
            if (mob.Vitality != 0) resistances["kVitality"] = mob.Vitality;
            if (mob.Mind != 0) resistances["kMind"] = mob.Mind;
            if (mob.Immunitet != 0) resistances["kImmunity"] = mob.Immunitet;
            if (resistances.Count > 0)
            {
                enhanced.Resistances = resistances;
                hasEnhanced = true;
            }

            // Saves: engine-named map, non-zero entries only.
            var saves = new YamlSaveMap();
            if (mob.SaveParalyzeCast != 0) saves["kWill"] = mob.SaveParalyzeCast;
            if (mob.SaveMagBreathes != 0) saves["kCritical"] = mob.SaveMagBreathes;
            if (mob.SaveMagDamages != 0) saves["kStability"] = mob.SaveMagDamages;
            if (mob.SaveFightSkills != 0) saves["kReflex"] = mob.SaveFightSkills;
            if (saves.Count > 0)
            {
                enhanced.Saves = saves;
                hasEnhanced = true;
            }

            // Feats
            if (mob.Feats.Count > 0)
            {
                enhanced.Feats = new List<int>();
                foreach (var feat in mob.Feats)
                {
                    if (feat is int f)
                        enhanced.Feats.Add(f);
                }
                hasEnhanced = true;
            }

            // Spells: emit a flat int list, repeating each id `count` times
            // (the engine reads bare ints and counts occurrences as memorized slots).
            if (mob.Spells.Count > 0)
            {
                enhanced.Spells = new List<int>();
                foreach (MobSpell spell in mob.Spells)
                {
                    int count = spell.Count < 1 ? 1 : spell.Count;
                    for (int i = 0; i < count; i++)
                        enhanced.Spells.Add(spell.VNum);
                }
                hasEnhanced = true;
            }

            // Helpers
            if (mob.Helpers.Count > 0)
            {
                enhanced.Helpers = new List<int>();
                foreach (var helper in mob.Helpers)
                {
                    if (helper is int h)
                        enhanced.Helpers.Add(h);
                }
                hasEnhanced = true;
            }

            // Destinations
            if (mob.Destination.Count > 0)
            {
                enhanced.Destinations = new List<int>();
                foreach (var dest in mob.Destination)
                {
                    if (dest is int d)
                        enhanced.Destinations.Add(d);
                }
                hasEnhanced = true;
            }

            if (hasEnhanced)
                yaml.Enhanced = enhanced;

            // Ingredients
            foreach (Ingredient ingr in mob.Ingredients)
            {
                yaml.Ingredients.Add(new YamlIngredient
                {
                    TypeName = ingr.TypeName,
                    Power = ingr.Power,
                    Probability = ingr.Probability,
                    PowerAuto = ingr.PowerAuto
                });
            }

            // Loaded objects after death
            foreach (LoadedObjAfterDeath obj in mob.LoadedObjectAfterDeath)
            {
                yaml.LoadedObjectAfterDeath.Add(new YamlLoadedObjAfterDeath
                {
                    ObjVNum = obj.VNum,
                    Probability = obj.LoadProb
                });
            }

            return yaml;
        }

        public static Mob FromYaml(YamlMob yaml)
        {
            if (yaml == null) return null;

            var mob = new Mob(yaml.VNum);

            // Names
            if (yaml.Names != null)
            {
                string val;
                if (yaml.Names.TryGetValue("aliases", out val)) mob.Alias = val ?? "";
                if (yaml.Names.TryGetValue("nominative", out val)) mob.Cases.Imen = val;
                if (yaml.Names.TryGetValue("genitive", out val)) mob.Cases.Rod = val;
                if (yaml.Names.TryGetValue("dative", out val)) mob.Cases.Dat = val;
                if (yaml.Names.TryGetValue("accusative", out val)) mob.Cases.Vin = val;
                if (yaml.Names.TryGetValue("instrumental", out val)) mob.Cases.Tvor = val;
                if (yaml.Names.TryGetValue("prepositional", out val)) mob.Cases.Pred = val;
            }

            // Descriptions
            if (yaml.Descriptions != null)
            {
                mob.Desc = yaml.Descriptions.ShortDesc ?? "";
                mob.DetailDescr = yaml.Descriptions.LongDesc ?? "";
            }

            // Action / affect flags from engine names back to asciiflag strings
            mob.Flags = EngineCodec.NamesToFlags(yaml.ActionFlags, EngineDictionaries.ActionFlags);
            mob.Affects = EngineCodec.NamesToFlags(yaml.AffectFlags, EngineDictionaries.AffectFlags);

            mob.Align = yaml.Alignment;

            // Stats
            if (yaml.Stats != null)
            {
                mob.Level = yaml.Stats.Level;
                mob.Hitroll = yaml.Stats.HitrollPenalty;
                mob.Ac = yaml.Stats.Armor;
                mob.Hits = yaml.Stats.Hp?.ToString() ?? "0d0+0";
                mob.Damage = yaml.Stats.Damage?.ToString() ?? "0d0+0";
            }

            // Gold
            mob.Money = yaml.Gold?.ToString() ?? "0d0+0";

            mob.Exp = yaml.Experience;

            // Position
            if (yaml.Position != null)
            {
                mob.PosDefault = EngineCodec.EnumValue(yaml.Position.Default, EngineDictionaries.Positions, 8);
                mob.PosLoad = EngineCodec.EnumValue(yaml.Position.Start, EngineDictionaries.Positions, 8);
            }

            mob.Sex = EngineCodec.EnumValue(yaml.Sex, EngineDictionaries.Genders);
            if (yaml.Speed.HasValue) mob.Speed = yaml.Speed.Value;

            // Attributes
            if (yaml.Attributes != null)
            {
                int attrVal;
                if (yaml.Attributes.TryGetValue("strength", out attrVal)) mob.Stats.Str = attrVal;
                if (yaml.Attributes.TryGetValue("dexterity", out attrVal)) mob.Stats.Dex = attrVal;
                if (yaml.Attributes.TryGetValue("intelligence", out attrVal)) mob.Stats.Int = attrVal;
                if (yaml.Attributes.TryGetValue("wisdom", out attrVal)) mob.Stats.Wis = attrVal;
                if (yaml.Attributes.TryGetValue("constitution", out attrVal)) mob.Stats.Con = attrVal;
                if (yaml.Attributes.TryGetValue("charisma", out attrVal)) mob.Stats.Cha = attrVal;
            }

            mob.Stats.Size = yaml.Size;
            mob.Stats.Height = yaml.Height;
            mob.Stats.Weight = yaml.Weight;
            mob.Class = yaml.MobClass;
            mob.Race = yaml.Race;
            mob.MaxInWorld = yaml.MaxInWorld;

            // Skills
            if (yaml.Skills != null)
            {
                foreach (var skill in yaml.Skills)
                    mob.Skills.Add(new MobSkill(skill.SkillId, skill.Value));
            }

            // Triggers
            if (yaml.Triggers != null)
            {
                foreach (var trig in yaml.Triggers)
                    mob.TriggersList.Add(trig);
            }

            // Enhanced E-spec fields
            if (yaml.Enhanced != null)
            {
                var enh = yaml.Enhanced;

                if (enh.HpRegen.HasValue) mob.HPreg = enh.HpRegen.Value;
                if (enh.ArmourBonus.HasValue) mob.Armour = enh.ArmourBonus.Value;
                if (enh.ManaRegen.HasValue) mob.PlusMem = enh.ManaRegen.Value;
                if (enh.CastSuccess.HasValue) mob.CastSuccess = enh.CastSuccess.Value;
                if (enh.Morale.HasValue) mob.Luck = enh.Morale.Value;
                if (enh.InitiativeAdd.HasValue) mob.Initiative = enh.InitiativeAdd.Value;
                if (enh.Absorb.HasValue) mob.Absorbe = enh.Absorb.Value;
                if (enh.Aresist.HasValue) mob.AResist = enh.Aresist.Value;
                if (enh.Mresist.HasValue) mob.MResist = enh.Mresist.Value;
                if (enh.Presist.HasValue) mob.PResist = enh.Presist.Value;
                if (enh.BareHandAttack.HasValue) mob.BareHandAttack = enh.BareHandAttack.Value;
                if (enh.LikeWork.HasValue) mob.LikeWork = enh.LikeWork.Value;
                if (enh.MaxFactor.HasValue) mob.MaxFactor = enh.MaxFactor.Value;
                if (enh.ExtraAttack.HasValue) mob.ExtraAttack = enh.ExtraAttack.Value;
                if (!string.IsNullOrEmpty(enh.SpecialBitvector)) mob.SpecialBitvector = enh.SpecialBitvector;

                // Role bitvector: positions of set bits (mirrors MobsFileManager "Role:" parsing)
                if (!string.IsNullOrEmpty(enh.Role))
                {
                    int cntr = enh.Role.Length;
                    foreach (char bit in enh.Role)
                    {
                        if (bit == '1') mob.Roles.Add(cntr);
                        cntr--;
                    }
                }

                // Resistances (named map; the converter also accepts a positional list)
                if (enh.Resistances != null)
                {
                    int v;
                    if (enh.Resistances.TryGetValue("kFire", out v)) mob.ResistFromFire = v;
                    if (enh.Resistances.TryGetValue("kAir", out v)) mob.ResistFromAir = v;
                    if (enh.Resistances.TryGetValue("kWater", out v)) mob.ResistFromWater = v;
                    if (enh.Resistances.TryGetValue("kEarth", out v)) mob.ResistFromEarth = v;
                    if (enh.Resistances.TryGetValue("kDark", out v)) mob.ResistDark = v;
                    if (enh.Resistances.TryGetValue("kVitality", out v)) mob.Vitality = v;
                    if (enh.Resistances.TryGetValue("kMind", out v)) mob.Mind = v;
                    if (enh.Resistances.TryGetValue("kImmunity", out v)) mob.Immunitet = v;
                }

                // Saves (named map; the converter also accepts a positional list)
                if (enh.Saves != null)
                {
                    int v;
                    if (enh.Saves.TryGetValue("kWill", out v)) mob.SaveParalyzeCast = v;
                    if (enh.Saves.TryGetValue("kCritical", out v)) mob.SaveMagBreathes = v;
                    if (enh.Saves.TryGetValue("kStability", out v)) mob.SaveMagDamages = v;
                    if (enh.Saves.TryGetValue("kReflex", out v)) mob.SaveFightSkills = v;
                }

                // Feats
                if (enh.Feats != null)
                {
                    foreach (var feat in enh.Feats)
                        mob.Feats.Add(feat);
                }

                // Spells: the flat int list repeats each id once per memorized slot;
                // group consecutive-or-not occurrences back into (id, count) entries.
                if (enh.Spells != null)
                {
                    var counts = new Dictionary<int, int>();
                    var order = new List<int>();
                    foreach (int spellId in enh.Spells)
                    {
                        if (!counts.ContainsKey(spellId)) { counts[spellId] = 0; order.Add(spellId); }
                        counts[spellId]++;
                    }
                    foreach (int spellId in order)
                        mob.Spells.Add(new MobSpell(spellId, counts[spellId]));
                }

                // Helpers
                if (enh.Helpers != null)
                {
                    foreach (var helper in enh.Helpers)
                        mob.Helpers.Add(helper);
                }

                // Destinations
                if (enh.Destinations != null)
                {
                    foreach (var dest in enh.Destinations)
                        mob.Destination.Add(dest);
                }
            }

            // Ingredients
            if (yaml.Ingredients != null)
            {
                foreach (var ingr in yaml.Ingredients)
                {
                    var ingredient = ingr.PowerAuto
                        ? new Ingredient(ingr.TypeName, ingr.Probability)
                        : new Ingredient(ingr.TypeName, ingr.Power, ingr.Probability);
                    mob.Ingredients.Add(ingredient);
                }
            }

            // Loaded objects after death
            if (yaml.LoadedObjectAfterDeath != null)
            {
                foreach (var obj in yaml.LoadedObjectAfterDeath)
                {
                    mob.LoadedObjectAfterDeath.Add(new LoadedObjAfterDeath(obj.ObjVNum)
                    {
                        LoadProb = obj.Probability
                    });
                }
            }

            return mob;
        }
    }
}
