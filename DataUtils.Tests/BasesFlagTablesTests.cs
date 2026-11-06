using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DataUtils.YamlMappers;
using NUnit.Framework;

namespace DataUtils.Tests
{
    /// <summary>
    /// The UI's flag checklists come from <c>redist/Bases/*.bb</c>, but what gets written
    /// to disk comes from <see cref="EngineDictionaries"/>. A checklist row whose bit has no
    /// name there is offered to builders and then silently dropped on save
    /// (<see cref="EngineCodec.FlagsToNames"/> skips unnamed bits). These tests keep the
    /// two sources of truth from drifting apart.
    /// </summary>
    [TestFixture]
    public class BasesFlagTablesTests
    {
        [Test]
        public void ActionBb_EveryRow_HasAnEngineName()
        {
            var named = new HashSet<int>(EngineDictionaries.ActionFlags.Values);

            var unnamed = ReadRows("Action.bb")
                .Where(r => !EngineCodec.DecodeAsciiFlags(r.Key).All(named.Contains))
                .Select(r => r.Key + " " + r.Value)
                .ToList();

            Assert.That(unnamed, Is.Empty,
                "Action.bb rows whose bit is missing from EngineDictionaries.ActionFlags would be lost on save");
        }

        [Test]
        public void ActionBb_EveryRow_IsASingleBit()
        {
            var malformed = ReadRows("Action.bb")
                .Where(r => r.Key.Length != 2 || EngineCodec.DecodeAsciiFlags(r.Key).Count != 1)
                .Select(r => r.Key)
                .ToList();

            Assert.That(malformed, Is.Empty);
        }

        /// <summary>
        /// The rows <c>CBasesDataManager</c> turns into checklist items: <c>key\tdescription</c>,
        /// skipping lines that start with <c>*</c> (hidden) and <c>g</c> group headers.
        /// </summary>
        private static List<KeyValuePair<string, string>> ReadRows(string fileName)
        {
            var rows = new List<KeyValuePair<string, string>>();
            foreach (string line in File.ReadAllLines(FindBasesFile(fileName), Encoding.UTF8))
            {
                if (line.StartsWith("*")) continue;
                int tab = line.IndexOf('\t');
                if (tab <= 0) continue;
                string key = line.Substring(0, tab).Trim();
                if (key.ToLowerInvariant() == "g") continue;
                rows.Add(new KeyValuePair<string, string>(key, line.Substring(tab + 1).Trim()));
            }
            Assert.That(rows, Is.Not.Empty, fileName + " has no rows");
            return rows;
        }

        /// <summary>Walks up from the test assembly folder to the checkout's <c>redist/Bases</c>.</summary>
        private static string FindBasesFile(string fileName)
        {
            for (var dir = new DirectoryInfo(TestContext.CurrentContext.TestDirectory); dir != null; dir = dir.Parent)
            {
                string candidate = Path.Combine(dir.FullName, "redist", "Bases", fileName);
                if (File.Exists(candidate))
                    return candidate;
            }
            Assert.Fail("redist/Bases/" + fileName + " not found above " + TestContext.CurrentContext.TestDirectory);
            return null;
        }
    }
}
