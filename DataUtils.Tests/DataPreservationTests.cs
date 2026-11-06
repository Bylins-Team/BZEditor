using System.Linq;
using DataUtils;
using DataUtils.YamlMappers;
using NUnit.Framework;

namespace DataUtils.Tests
{
    /// <summary>
    /// Guards the fields that used to be silently dropped on save (found via the
    /// production-world field scan): mob spells/remort/resistances/saves/dead_load
    /// and object extra_values. They round-trip through the YAML mappers in memory,
    /// so this runs in CI with no world data.
    /// </summary>
    [TestFixture]
    public class DataPreservationTests
    {
        [Test]
        public void Mob_EnhancedAndDeadLoad_SurviveRoundTrip()
        {
            var mob = new Mob(7305);
            mob.Spells.Add(16, 3);          // spell id 16, memorized x3
            mob.MobRemort = 7;
            mob.ResistFromFire = 22;        // kFire
            mob.Immunitet = 95;             // kImmunity
            mob.SaveParalyzeCast = 80;      // kWill
            mob.LoadedObjectAfterDeath.Add(new LoadedObjAfterDeath(2600)
            {
                LoadProb = 100,
                LoadType = 1,
                SpecParam = 5
            });

            var mob2 = YamlMobMapper.FromYaml(YamlMobMapper.ToYaml(mob));

            Assert.That(mob2.MobRemort, Is.EqualTo(7));
            Assert.That(mob2.ResistFromFire, Is.EqualTo(22));
            Assert.That(mob2.Immunitet, Is.EqualTo(95));
            Assert.That(mob2.SaveParalyzeCast, Is.EqualTo(80));

            var spell = mob2.Spells.Cast<MobSpell>().FirstOrDefault(s => s.VNum == 16);
            Assert.That(spell, Is.Not.Null, "spell 16 should round-trip");
            Assert.That(spell.Count, Is.EqualTo(3), "spell memorized count");

            var dead = mob2.LoadedObjectAfterDeath.Cast<LoadedObjAfterDeath>().First();
            Assert.That(dead.LoadType, Is.EqualTo(1));
            Assert.That(dead.SpecParam, Is.EqualTo(5));
        }

        [Test]
        public void Mob_ActionFlags_IncludingPlaneThree_SurviveRoundTrip()
        {
            // m0 = kHelper (12), w2 = kIgnoresFormation (82), a3 = kGreatHelper (90) -- the
            // first flag in the third plane. A bit with no name in EngineDictionaries is
            // silently dropped by FlagsToNames, so this pins both newest names.
            var mob = new Mob(7305) { Flags = "m0w2a3" };

            var yaml = YamlMobMapper.ToYaml(mob);
            Assert.That(yaml.ActionFlags, Is.EquivalentTo(new[] { "kHelper", "kIgnoresFormation", "kGreatHelper" }));

            var mob2 = YamlMobMapper.FromYaml(yaml);
            Assert.That(EngineCodec.DecodeAsciiFlags(mob2.Flags), Is.EquivalentTo(new[] { 12, 82, 90 }));
        }

        [Test]
        public void Mob_Undead_IsWrittenAsKUndead_AndReadFromEngineUnusedName()
        {
            // B1 = bit 57. The engine's world dictionary still calls it UNUSED_57, which is what
            // engine-saved worlds contain; the editor reads that and writes the enum's name.
            var yaml = YamlMobMapper.ToYaml(new Mob(7305) { Flags = "B1" });
            Assert.That(yaml.ActionFlags, Is.EqualTo(new[] { "kUndead" }));

            yaml.ActionFlags.Clear();
            yaml.ActionFlags.Add("UNUSED_57");
            var mob = YamlMobMapper.FromYaml(yaml);
            Assert.That(EngineCodec.DecodeAsciiFlags(mob.Flags), Is.EqualTo(new[] { 57 }));
            Assert.That(YamlMobMapper.ToYaml(mob).ActionFlags, Is.EqualTo(new[] { "kUndead" }));
        }

        [TestCase(0, "kMobAct")]
        [TestCase(1, "kObjFight")]
        [TestCase(2, "kWldEnterPc")]
        public void Trigger_Types_SurviveRoundTrip_WithAttachTypeNames(int attachType, string bit4Name)
        {
            var trigger = new Trigger(2600) { Class = attachType, Type = "bez" };

            var yaml = YamlTriggerMapper.ToYaml(trigger);
            Assert.That(yaml.TriggerTypes, Is.EqualTo(new[] { "kRandom", bit4Name, "kAuto" }));

            var trigger2 = YamlTriggerMapper.FromYaml(yaml);
            Assert.That(trigger2.Class, Is.EqualTo(attachType));
            Assert.That(trigger2.Type, Is.EqualTo("bez"));
        }

        [Test]
        public void Obj_ExtraValues_SurviveRoundTrip()
        {
            var obj = new Obj(15050);
            obj.ExtraValues["POTION_SPELL1_NUM"] = 16;
            obj.ExtraValues["POTION_SPELL1_LVL"] = 0;

            var obj2 = YamlObjMapper.FromYaml(YamlObjMapper.ToYaml(obj));

            Assert.That(obj2.ExtraValues["POTION_SPELL1_NUM"], Is.EqualTo(16));
            Assert.That(obj2.ExtraValues.ContainsKey("POTION_SPELL1_LVL"), Is.True);
        }
    }
}
