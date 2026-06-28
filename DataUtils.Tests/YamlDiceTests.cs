using DataUtils.YamlModels;
using NUnit.Framework;

namespace DataUtils.Tests
{
    /// <summary>
    /// Dice strings carry hp/damage/gold. The "+-N" negative-bonus form (e.g. 0d0+-56)
    /// is a known footgun the engine/converter use, so guard the parser against it.
    /// </summary>
    [TestFixture]
    public class YamlDiceTests
    {
        [Test]
        public void Parse_PositiveBonus()
        {
            var d = YamlDice.Parse("3d6+2");
            Assert.That(d.DiceCount, Is.EqualTo(3));
            Assert.That(d.DiceSize, Is.EqualTo(6));
            Assert.That(d.Bonus, Is.EqualTo(2));
        }

        [Test]
        public void Parse_NegativeBonus_PlusMinusForm()
        {
            var d = YamlDice.Parse("0d0+-56");
            Assert.That(d.DiceCount, Is.EqualTo(0));
            Assert.That(d.DiceSize, Is.EqualTo(0));
            Assert.That(d.Bonus, Is.EqualTo(-56));
        }

        [TestCase("3d6+2")]
        [TestCase("0d0+-56")]
        [TestCase("20d10+2500")]
        [TestCase("0d0+0")]
        public void ToString_RoundTripsCanonicalForm(string s)
        {
            Assert.That(YamlDice.Parse(s).ToString(), Is.EqualTo(s));
        }
    }
}
