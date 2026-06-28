using DataUtils.YamlModels;
using NUnit.Framework;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace DataUtils.Tests
{
    /// <summary>
    /// Mob resistances/saves: the current engine emits named maps, older worlds use
    /// positional lists. The converter must read both and always write a named map.
    /// (This is what made ~half the production world unreadable before the fix.)
    /// </summary>
    [TestFixture]
    public class NamedIntMapTests
    {
        private static IDeserializer De()
        {
            return new DeserializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .WithTypeConverter(new NamedIntMapConverter())
                .IgnoreUnmatchedProperties()
                .Build();
        }

        private static ISerializer Ser()
        {
            return new SerializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .WithTypeConverter(new NamedIntMapConverter())
                .Build();
        }

        [Test]
        public void Reads_NamedMap()
        {
            var m = De().Deserialize<YamlResistMap>("kFire: 7\nkVitality: 50\nkImmunity: 95\n");
            Assert.That(m["kFire"], Is.EqualTo(7));
            Assert.That(m["kVitality"], Is.EqualTo(50));
            Assert.That(m["kImmunity"], Is.EqualTo(95));
        }

        [Test]
        public void Reads_PositionalList_MappedToCanonicalNames()
        {
            // order: kFire,kAir,kWater,kEarth,kDark,kVitality,kMind,kImmunity
            var m = De().Deserialize<YamlResistMap>("- 1\n- 0\n- 0\n- 0\n- 0\n- 5\n- 0\n- 9\n");
            Assert.That(m["kFire"], Is.EqualTo(1));
            Assert.That(m["kVitality"], Is.EqualTo(5));
            Assert.That(m["kImmunity"], Is.EqualTo(9));
        }

        [Test]
        public void Reads_PositionalSaves()
        {
            // order: kWill,kCritical,kStability,kReflex
            var m = De().Deserialize<YamlSaveMap>("- 80\n- 0\n- 0\n- 5\n");
            Assert.That(m["kWill"], Is.EqualTo(80));
            Assert.That(m["kReflex"], Is.EqualTo(5));
        }

        [Test]
        public void Writes_NamedMap_InCanonicalOrder()
        {
            var m = new YamlSaveMap { { "kReflex", 3 }, { "kWill", 1 } };
            string y = Ser().Serialize(m);
            StringAssert.Contains("kWill", y);
            StringAssert.Contains("kReflex", y);
            // canonical ESaving order puts kWill before kReflex regardless of insert order
            Assert.That(y.IndexOf("kWill"), Is.LessThan(y.IndexOf("kReflex")));
        }
    }
}
