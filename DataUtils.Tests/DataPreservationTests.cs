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
