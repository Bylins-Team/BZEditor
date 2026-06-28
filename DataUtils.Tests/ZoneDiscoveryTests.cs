using System;
using System.IO;
using System.Text;
using DataUtils;
using NUnit.Framework;

namespace DataUtils.Tests
{
    /// <summary>
    /// Zone discovery must follow the selected world format. Regression guard for the
    /// "switched to YAML, deleted legacy, editor still demands the legacy world" bug.
    /// </summary>
    [TestFixture]
    public class ZoneDiscoveryTests
    {
        private string _tmp;

        [SetUp]
        public void Setup()
        {
            _tmp = Path.Combine(Path.GetTempPath(), "bzed_disc_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(_tmp);
            StaticData.CurrentEncoding = Encoding.GetEncoding("koi8-r");
            StaticData.WorldFolderPath = _tmp;
        }

        [TearDown]
        public void Teardown()
        {
            try { Directory.Delete(_tmp, true); } catch { /* best effort */ }
        }

        [Test]
        public void DiscoversYamlZones_FromZonesDir()
        {
            StaticData.WorldDataFormat = "yaml";
            string zoneDir = Path.Combine(_tmp, "zones", "42");
            Directory.CreateDirectory(zoneDir);
            File.WriteAllText(Path.Combine(zoneDir, "zone.yaml"),
                "name: Test Zone\nvnum: 42\n", StaticData.CurrentEncoding);

            var m = new FileListsDataManager();
            m.LoadAvailZones();

            ZoneData z = m.ZonesDataList["42"];
            Assert.That(z, Is.Not.Null, "zone 42 should be discovered from zones/42/zone.yaml");
            Assert.That(z.Name, Is.EqualTo("Test Zone"));
        }

        [Test]
        public void DiscoversLegacyZones_FromZonDir()
        {
            StaticData.WorldDataFormat = "circlemud";
            string zonDir = Path.Combine(_tmp, "ZON");
            Directory.CreateDirectory(zonDir);
            File.WriteAllText(Path.Combine(zonDir, "42.zon"),
                "#42\nLegacy Name~\nS\n$\n", StaticData.CurrentEncoding);

            var m = new FileListsDataManager();
            m.LoadAvailZones();

            ZoneData z = m.ZonesDataList["42"];
            Assert.That(z, Is.Not.Null, "zone 42 should be discovered from ZON/42.zon");
            Assert.That(z.Name, Is.EqualTo("Legacy Name"));
        }

        [Test]
        public void YamlFormat_IgnoresLegacyZonDir()
        {
            // With YAML selected, a stray ZON/ must not be required or scanned.
            StaticData.WorldDataFormat = "yaml";
            Directory.CreateDirectory(Path.Combine(_tmp, "ZON"));
            File.WriteAllText(Path.Combine(_tmp, "ZON", "9.zon"),
                "#9\nLegacy Only~\nS\n$\n", StaticData.CurrentEncoding);

            var m = new FileListsDataManager();
            m.LoadAvailZones();

            Assert.That(m.ZonesDataList["9"], Is.Null, "legacy zone must not be discovered in YAML mode");
        }
    }
}
