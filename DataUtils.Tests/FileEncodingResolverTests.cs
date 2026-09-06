using System;
using System.IO;
using System.Linq;
using System.Text;
using DataUtils;
using NUnit.Framework;

namespace DataUtils.Tests
{
    /// <summary>
    /// World files carry no BOM, so the encoding has to be guessed from the bytes:
    /// valid UTF-8 means UTF-8, anything else is a legacy koi8-r file.
    /// </summary>
    [TestFixture]
    public class FileEncodingResolverTests
    {
        private const string Russian = "Крохотная комнатка, освещённая свечой.";

        private string _tmp;

        [SetUp]
        public void Setup()
        {
            _tmp = Path.Combine(Path.GetTempPath(), "bzed_enc_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(_tmp);
        }

        [TearDown]
        public void Teardown()
        {
            try { Directory.Delete(_tmp, true); } catch { /* best effort */ }
        }

        private string Write(string name, byte[] bytes)
        {
            string path = Path.Combine(_tmp, name);
            File.WriteAllBytes(path, bytes);
            return path;
        }

        [Test]
        public void Utf8File_ResolvedAsUtf8_AndReadsBack()
        {
            string path = Write("utf8.yaml", new UTF8Encoding(false).GetBytes(Russian));

            Encoding enc = FileEncodingResolver.Resolve(path);

            Assert.That(enc.WebName, Is.EqualTo("utf-8"));
            Assert.That(File.ReadAllText(path, enc), Is.EqualTo(Russian));
        }

        [Test]
        public void Utf8Encoding_DoesNotEmitBom()
        {
            Assert.That(FileEncodingResolver.Utf8NoBom.GetPreamble(), Is.Empty);
        }

        [Test]
        public void Koi8File_FallsBackToKoi8_AndReadsBack()
        {
            Encoding koi = Encoding.GetEncoding("koi8-r");
            string path = Write("koi8.yaml", koi.GetBytes(Russian));

            Encoding enc = FileEncodingResolver.Resolve(path);

            Assert.That(enc.WebName, Is.EqualTo(koi.WebName));
            Assert.That(File.ReadAllText(path, enc), Is.EqualTo(Russian));
        }

        [Test]
        public void AsciiOnlyFile_ResolvedAsUtf8()
        {
            string path = Write("ascii.yaml", Encoding.ASCII.GetBytes("vnum: 4200\nname: Test Zone\n"));

            Assert.That(FileEncodingResolver.Resolve(path).WebName, Is.EqualTo("utf-8"));
        }

        [Test]
        public void EmptyFile_ResolvedAsUtf8()
        {
            string path = Write("empty.yaml", new byte[0]);

            Assert.That(FileEncodingResolver.Resolve(path).WebName, Is.EqualTo("utf-8"));
        }

        [Test]
        public void TruncatedUtf8Sequence_FallsBackToKoi8()
        {
            byte[] utf8 = new UTF8Encoding(false).GetBytes("Комната");
            byte[] cut = new byte[utf8.Length - 1]; // drops the trailing continuation byte
            Array.Copy(utf8, cut, cut.Length);

            Assert.That(FileEncodingResolver.Resolve(cut).WebName, Is.EqualTo("koi8-r"));
        }

        [Test]
        public void OverlongAndSurrogateForms_FallBackToKoi8()
        {
            // C0 80 - overlong encoding of U+0000; ED A0 80 - surrogate U+D800
            Assert.That(FileEncodingResolver.Resolve(new byte[] { 0xC0, 0x80 }).WebName, Is.EqualTo("koi8-r"));
            Assert.That(FileEncodingResolver.Resolve(new byte[] { 0xED, 0xA0, 0x80 }).WebName, Is.EqualTo("koi8-r"));
        }

        [Test]
        public void MultiByteSequence_SplitAcrossBuffers_StaysUtf8()
        {
            // Long enough to cross the internal 64K read buffer at an arbitrary offset.
            var text = new StringBuilder();
            while (text.Length < 200000)
                text.Append(Russian);
            string path = Write("big.yaml", new UTF8Encoding(false).GetBytes(text.ToString()));

            Assert.That(FileEncodingResolver.Resolve(path).WebName, Is.EqualTo("utf-8"));
        }

        [Test]
        public void EmptyFileName_Throws()
        {
            Assert.Throws<ArgumentException>(() => FileEncodingResolver.Resolve(""));
        }

        [Test]
        public void ReadAllText_DecodesWithTheResolvedEncoding()
        {
            Encoding koi = Encoding.GetEncoding("koi8-r");
            string utf8Path = Write("utf8.yaml", new UTF8Encoding(false).GetBytes(Russian));
            string koi8Path = Write("koi8.yaml", koi.GetBytes(Russian));

            Encoding utf8Used, koi8Used;
            Assert.That(FileEncodingResolver.ReadAllText(utf8Path, out utf8Used), Is.EqualTo(Russian));
            Assert.That(FileEncodingResolver.ReadAllText(koi8Path, out koi8Used), Is.EqualTo(Russian));

            Assert.That(utf8Used.WebName, Is.EqualTo("utf-8"));
            Assert.That(koi8Used.WebName, Is.EqualTo(koi.WebName));
        }

        [Test]
        public void ReadAllText_StripsUtf8Bom()
        {
            string path = Write("bom.yaml", new UTF8Encoding(true).GetPreamble()
                .Concat(new UTF8Encoding(false).GetBytes(Russian)).ToArray());

            Assert.That(FileEncodingResolver.ReadAllText(path), Is.EqualTo(Russian));
        }
    }
}
