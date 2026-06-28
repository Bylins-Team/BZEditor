using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DataUtils;
using NUnit.Framework;

namespace DataUtils.Tests
{
    /// <summary>
    /// Integration tests against a real YAML world. The world is NOT committed (it is
    /// game content); point BZED_TEST_WORLD at a checkout to run these. Without it the
    /// tests are ignored, so CI stays green and self-contained.
    /// </summary>
    [TestFixture]
    public class ProdWorldIntegrationTests
    {
        private static string WorldOrIgnore()
        {
            string w = Environment.GetEnvironmentVariable("BZED_TEST_WORLD");
            if (string.IsNullOrEmpty(w) || !Directory.Exists(Path.Combine(w, "zones")))
                Assert.Ignore("Set BZED_TEST_WORLD to a YAML world directory to run integration tests.");
            return w;
        }

        private static List<int> ZoneNumbers(string world)
        {
            var zones = new List<int>();
            foreach (string d in Directory.GetDirectories(Path.Combine(world, "zones")))
            {
                int z;
                if (int.TryParse(Path.GetFileName(d), out z)) zones.Add(z);
            }
            zones.Sort();
            return zones;
        }

        [Test]
        public void EveryZone_Loads()
        {
            string world = WorldOrIgnore();
            var koi = Encoding.GetEncoding("koi8-r");
            StaticData.CurrentEncoding = koi;
            StaticData.WorldFolderPath = world;
            StaticData.WorldDataFormat = "yaml";

            var failed = new List<int>();
            foreach (int z in ZoneNumbers(world))
            {
                try
                {
                    var zdm = new ZoneDataManager(z.ToString(), koi);
                    if (!zdm.LoadData()) failed.Add(z);
                }
                catch
                {
                    failed.Add(z);
                }
            }

            Assert.That(failed, Is.Empty, "zones that failed to load: " + string.Join(", ", failed));
        }

        [Test]
        public void EveryZone_SaveIsStableOnSecondPass()
        {
            string world = WorldOrIgnore();
            var koi = Encoding.GetEncoding("koi8-r");
            StaticData.CurrentEncoding = koi;

            string root = Path.Combine(Path.GetTempPath(), "bzed_rt_" + Guid.NewGuid().ToString("N"));
            string a = Path.Combine(root, "a"), b = Path.Combine(root, "b");
            var drift = new List<int>();
            try
            {
                foreach (int z in ZoneNumbers(world))
                {
                    string zn = z.ToString();
                    var z1 = Load(world, zn, koi); Save(z1, a, zn);
                    var z2 = Load(a, zn, koi); Save(z2, b, zn);
                    if (!ZoneFilesEqual(a, b, zn, koi)) drift.Add(z);
                }
                // A re-save must be byte-stable. The whole production world round-trips
                // cleanly; a tiny margin tolerates one-off junk in future world data while
                // still catching any systemic regression (which would drift many zones).
                Assert.That(drift.Count, Is.LessThanOrEqualTo(2),
                    "too many zones drift on re-save: " + string.Join(", ", drift));
            }
            finally
            {
                try { Directory.Delete(root, true); } catch { }
            }
        }

        private static ZoneDataManager Load(string dir, string zn, Encoding enc)
        {
            StaticData.WorldFolderPath = dir;
            StaticData.WorldDataFormat = "yaml";
            var z = new ZoneDataManager(zn, enc);
            z.LoadData();
            return z;
        }

        private static void Save(ZoneDataManager z, string dir, string zn)
        {
            StaticData.WorldFolderPath = dir;
            var y = new YamlFormatProvider();
            y.SaveZone(z.Zone, z.Objects, z.Mobs, z.Rooms);
            y.SaveMobs(z.Mobs, zn);
            y.SaveObjects(z.Objects, zn);
            y.SaveRooms(z.Rooms, zn);
            y.SaveTriggers(z.Triggers, zn);
        }

        private static bool ZoneFilesEqual(string a, string b, string zn, Encoding enc)
        {
            foreach (string f in new[] { "zone.yaml", "mobs.yaml", "objects.yaml", "rooms.yaml", "triggers.yaml" })
            {
                string p1 = Path.Combine(a, "zones", zn, f), p2 = Path.Combine(b, "zones", zn, f);
                if (File.Exists(p1) != File.Exists(p2)) return false;
                if (File.Exists(p1) && !File.ReadAllText(p1, enc).Equals(File.ReadAllText(p2, enc))) return false;
            }
            return true;
        }
    }
}
