using System.Collections.Generic;
using DataUtils.YamlMappers;
using NUnit.Framework;

namespace DataUtils.Tests
{
    /// <summary>
    /// Trigger type names depend on the attach type: bit 4 is kMobAct / kObjFight / kWldEnterPc.
    /// Writing the mob name for every trigger is what made room triggers read back as kAct.
    /// </summary>
    [TestFixture]
    public class TriggerTypeCodecTests
    {
        private const int Mob = 0, Obj = 1, Wld = 2;

        [TestCase(Mob, "kMobAct")]
        [TestCase(Obj, "kObjFight")]
        [TestCase(Wld, "kWldEnterPc")]
        public void SameBit_IsNamedByAttachType(int attachType, string expected)
        {
            Assert.That(TriggerTypeCodec.NameFor(4, attachType), Is.EqualTo(expected));
        }

        [TestCase(Mob)]
        [TestCase(Obj)]
        [TestCase(Wld)]
        public void CommonBits_StayUnprefixed(int attachType)
        {
            Assert.That(TriggerTypeCodec.ToNames("abcz", attachType),
                Is.EqualTo(new[] { "kRandomGlobal", "kRandom", "kCommand", "kAuto" }));
        }

        [Test]
        public void BitWithoutName_IsWrittenAsNumber()
        {
            // Bit 10 has a mob and a room name but no object one; bit 24 only a legacy name.
            Assert.That(TriggerTypeCodec.NameFor(10, Obj), Is.EqualTo("10"));
            Assert.That(TriggerTypeCodec.NameFor(24, Mob), Is.EqualTo("24"));
        }

        [Test]
        public void LegacyNames_AreNeverWritten()
        {
            for (int type = Mob; type <= Wld; type++)
                for (int bit = 0; bit < 26; bit++)
                    Assert.That(EngineDictionaries.LegacyTriggerTypes.ContainsKey(TriggerTypeCodec.NameFor(bit, type)),
                        Is.False, $"bit {bit}, attach type {type}");
        }

        [Test]
        public void Reading_AcceptsCurrentLegacyAndNumericNames()
        {
            var problems = new List<string>();
            string flags = TriggerTypeCodec.FromNames(
                new[] { "kWldEnterPc", "kGreet", "24", "kRandom" }, Wld, problems);

            Assert.That(flags, Is.EqualTo("begy")); // bits 1, 4, 6, 24
            Assert.That(problems, Is.Empty);
        }

        [Test]
        public void ForeignPrefix_IsReportedButKeepsItsBit()
        {
            var problems = new List<string>();
            string flags = TriggerTypeCodec.FromNames(new[] { "kObjFight" }, Mob, problems);

            Assert.That(flags, Is.EqualTo("e"));
            Assert.That(problems, Has.Count.EqualTo(1));
            Assert.That(problems[0], Does.Contain("kObjFight"));
        }

        [Test]
        public void UnknownName_IsReportedAndDropped()
        {
            var problems = new List<string>();
            Assert.That(TriggerTypeCodec.FromNames(new[] { "kNoSuchType", "kMobAct" }, Mob, problems), Is.EqualTo("e"));
            Assert.That(problems, Has.Count.EqualTo(1));
        }

        [TestCase(Mob)]
        [TestCase(Obj)]
        [TestCase(Wld)]
        public void EveryBit_RoundTrips(int attachType)
        {
            const string all = "abcdefghijklmnopqrstuvwxyz";
            var problems = new List<string>();
            string back = TriggerTypeCodec.FromNames(TriggerTypeCodec.ToNames(all, attachType), attachType, problems);

            Assert.That(back, Is.EqualTo(all));
            Assert.That(problems, Is.Empty);
        }
    }
}
