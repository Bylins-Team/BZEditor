using System.Collections.Generic;
using DataUtils.YamlMappers;
using NUnit.Framework;

namespace DataUtils.Tests
{
    /// <summary>
    /// The asciiflag codec underlies every flag field (action/affect/extra/wear/...).
    /// A bug here silently corrupts flags on save, so it is worth pinning down.
    /// </summary>
    [TestFixture]
    public class EngineCodecTests
    {
        [Test]
        public void Decode_KnownBits()
        {
            // bit = letterValue + 30 * planeDigit. a=0, b=1, A=26; plane is the digit.
            Assert.That(EngineCodec.DecodeAsciiFlags("a0"), Is.EqualTo(new List<int> { 0 }));
            Assert.That(EngineCodec.DecodeAsciiFlags("b0"), Is.EqualTo(new List<int> { 1 }));
            Assert.That(EngineCodec.DecodeAsciiFlags("a1"), Is.EqualTo(new List<int> { 30 }));
            Assert.That(EngineCodec.DecodeAsciiFlags("A0"), Is.EqualTo(new List<int> { 26 }));
        }

        [Test]
        public void Encode_IsAscendingByBit()
        {
            // sorted -> 0,1,30 -> a0 b0 a1
            Assert.That(EngineCodec.EncodeAsciiFlags(new[] { 30, 0, 1 }), Is.EqualTo("a0b0a1"));
        }

        [TestCase("a0")]
        [TestCase("a0b0c0")]
        [TestCase("a1")]
        [TestCase("A0")]
        [TestCase("z9")]
        [TestCase("a0c2D5")]
        public void AsciiFlags_RoundTrip(string ascii)
        {
            List<int> bits = EngineCodec.DecodeAsciiFlags(ascii);
            string reencoded = EngineCodec.EncodeAsciiFlags(bits);
            // Decoding is canonical (sorted, unique), so decode(encode(decode(x))) == decode(x).
            Assert.That(EngineCodec.DecodeAsciiFlags(reencoded), Is.EqualTo(bits));
        }

        [Test]
        public void Decode_Empty_IsEmpty()
        {
            Assert.That(EngineCodec.DecodeAsciiFlags(""), Is.Empty);
            Assert.That(EngineCodec.DecodeAsciiFlags(null), Is.Empty);
        }
    }
}
