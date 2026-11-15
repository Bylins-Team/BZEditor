using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DataUtils.YamlMappers;
using DataUtils.YamlModels;
using NUnit.Framework;

namespace DataUtils.Tests
{
    /// <summary>
    /// Loads a world written the way the engine writes it (yaml_world_data_source.cpp) and checks that
    /// the editor puts every field where the engine means it and saves it back with the same meaning.
    /// The round-trip tests elsewhere start from the editor's own output, so they cannot see a field
    /// that is misread on the first load -- which is how item affects, spec_param and every load% of
    /// engine-written reset commands were being lost.
    /// </summary>
    [TestFixture]
    public class EngineWorldFidelityTests
    {
        private const string Zone = "777";

        private const string ZoneYaml =
            "vnum: 777\nname: Тест\ntop_room: 77799\nlifespan: 10\nreset_mode: 2\nentrance: 77701\ntypeA_zones:\n  - 778\ntypeB_zones:\n  - 779\ncommands:\n" +
            "  - MOB 0 77700 1 77700 1  # max_in_world 1 here...\n" +
            "  - TRIGGER 1 0 77700  # mob trigger: binds to the mob loaded just before\n" +
            "  - GIVE 1 77700 5\n" +
            "  - EQUIP 1 77701 5 16\n" +
            "  - MOB 0 77700 3 77701 1  # ...and 3 here\n" +
            "  - GIVE 1 77700 5 30\n" +
            "  - OBJECT 0 77702 1 77700 1\n" +
            "  - PUT 1 77703 0 77702 25\n" +
            "  - DOOR 0 77700 1 2\n" +
            "  - EXTRACT 0 77700\n" +
            "  - TRIGGER 0 2 77700 77700\n" +
            "  - MOB 2 77700 2 77799 1  # zone virtual room, persist bit\n" +
            "  - EQUIP 3 77701 5 16 50\n" +
            "  - FOLLOW 1 77700 77700 77700  # a same-vnum group the room model cannot hold\n" +
            "  - MOB 0 77701 1 77799 1  # virtual room again: the follower, loaded before its leader\n" +
            "  - GIVE 1 77703 5  # if_flag 1: the command before it must stay this mob's own MOB\n" +
            "  - FOLLOW 1 77799 77702 77701\n" +
            "  - MOB 0 77702 1 77799 1  # the leader, loaded after the follower\n";

        private const string RoomsYaml =
            "0:\n  name: Комната 0\n  sector: kInside\n  exits:\n    - direction: east\n      to_room: 77701\n      exit_flags: 1\n" +
            "    - direction: north\n      description: Глухая стена.\n      keywords: стена\n      to_room: -1\n" +
            "1:\n  name: Комната 1\n  sector: kInside\n";

        private const string Names =
            "{aliases: x, nominative: x, genitive: x, dative: x, accusative: x, instrumental: x, prepositional: x}";

        private const string MobsYaml =
            "0:\n  names: " + Names + "\n  stats: {level: 1}\n  action_flags: [kNpc, UNUSED_57, UNUSED_59]\n" +
            "1:\n  names: " + Names + "\n  stats: {level: 1}\n  action_flags: [kNpc]\n" +
            "2:\n  names: " + Names + "\n  stats: {level: 1}\n  action_flags: [kNpc]\n";

        private const string ObjectsYaml =
            "0:\n  names: " + Names + "\n  type: kWeapon\n  spec_param: 143\n" +
            "  extra_flags: [kNodisarm, kHasTwoSlots, UNUSED_55]\n" +
            "  affect_flags: [kDetectInvisibility, kInfravision, kNoFlee, kCloudly]\n" +
            "1:\n  names: " + Names + "\n  type: kArmor\n  max_in_world: 2\n" +
            "2:\n  names: " + Names + "\n  type: kContainer\n" +
            "3:\n  names: " + Names + "\n  type: kOther\n";

        private string root;
        private string savedWorld;

        [SetUp]
        public void SetUp()
        {
            savedWorld = StaticData.WorldFolderPath;
            root = Path.Combine(Path.GetTempPath(), "bzed_fid_" + Guid.NewGuid().ToString("N"));
            string dir = Path.Combine(root, "src", "zones", Zone);
            Directory.CreateDirectory(dir);
            var utf8 = new UTF8Encoding(false);
            File.WriteAllText(Path.Combine(dir, "zone.yaml"), ZoneYaml, utf8);
            File.WriteAllText(Path.Combine(dir, "rooms.yaml"), RoomsYaml, utf8);
            File.WriteAllText(Path.Combine(dir, "mobs.yaml"), MobsYaml, utf8);
            File.WriteAllText(Path.Combine(dir, "objects.yaml"), ObjectsYaml, utf8);
            File.WriteAllText(Path.Combine(dir, "triggers.yaml"),
                "0:\n  name: lua\n  attach_type: kMobTrigger\n  trigger_types: [kMobRandom]\n  narg: 100\n  language: lua\n  script: |-\n    return 1\n", utf8);
        }

        [TearDown]
        public void TearDown()
        {
            StaticData.WorldFolderPath = savedWorld;
            try { Directory.Delete(root, true); } catch { }
        }

        private ZoneDataManager Load(string world)
        {
            StaticData.WorldFolderPath = world;
            var z = new ZoneDataManager(Zone, Encoding.UTF8);
            Assert.That(z.LoadData(), Is.True, "zone loads");
            return z;
        }

        private string SaveAndRead(ZoneDataManager z, string file)
        {
            string dst = Path.Combine(root, "out");
            StaticData.WorldFolderPath = dst;
            var y = new YamlFormatProvider();
            y.SaveZone(z.Zone, z.Objects, z.Mobs, z.Rooms);
            y.SaveMobs(z.Mobs, Zone);
            y.SaveObjects(z.Objects, Zone);
            y.SaveRooms(z.Rooms, Zone);
            y.SaveTriggers(z.Triggers, Zone);
            return File.ReadAllText(Path.Combine(dst, "zones", Zone, file), Encoding.UTF8);
        }

        private static List<string> Commands(string zoneYaml) =>
            zoneYaml.Replace("\r", "").Split('\n')
                .Where(l => l.StartsWith("- ") || l.StartsWith("  - "))
                .Select(l => l.Trim().Substring(2).Split('#')[0].Trim())
                .Where(c => c.Length > 0 && char.IsLetter(c[0]))
                .ToList();

        [Test]
        public void ItemFlags_LandInTheModelFieldsTheUiShows()
        {
            var obj = Load(Path.Combine(root, "src")).Objects[77700, 0];

            // "Эффекты" tab = ExctraEffects (Bases/ExtraEffect.bb): m0 = !обезоружить, e1 = 2 slots.
            Assert.That(EngineCodec.DecodeAsciiFlags(obj.ExctraEffects), Is.EquivalentTo(new[] { 12, 34, 55 }));
            // "Аффекты" tab = Affects (Bases/Affect.bb, EEquipmentAffect numbering): d0, j0, x0, q1.
            Assert.That(EngineCodec.DecodeAsciiFlags(obj.Affects), Is.EquivalentTo(new[] { 3, 9, 23, 46 }));
            Assert.That(obj.MagicFlags, Is.Empty, "affect_flags must not leak into the ingredient checklist");
            Assert.That(obj.TrenSkill, Is.EqualTo(143), "spec_param");
        }

        [Test]
        public void ItemFields_SurviveSave_WithEngineNames()
        {
            string text = SaveAndRead(Load(Path.Combine(root, "src")), "objects.yaml");
            var objs = new YamlFormatProviderProbe().Objects(text);

            Assert.That(objs[0].ExtraFlags, Is.EquivalentTo(new[] { "kNodisarm", "kHasTwoSlots", "UNUSED_55" }));
            Assert.That(objs[0].AffectFlags,
                Is.EquivalentTo(new[] { "kDetectInvisibility", "kInfravision", "kNoFlee", "kCloudly" }));
            Assert.That(objs[0].SpecParam, Is.EqualTo(143));
            Assert.That(objs[0].MaxInWorld, Is.EqualTo(-1), "absent max_in_world means unlimited, not 0");
            Assert.That(objs[1].MaxInWorld, Is.EqualTo(2));
        }

        [Test]
        public void MobUnnamedActionBits_SurviveSave()
        {
            string text = SaveAndRead(Load(Path.Combine(root, "src")), "mobs.yaml");
            Assert.That(text, Does.Contain("UNUSED_57"));
            Assert.That(text, Does.Contain("UNUSED_59"));
            Assert.That(text, Does.Not.Contain("kUndead"));
        }

        [Test]
        public void ZoneHeader_KeepsEntranceAndZoneLinks()
        {
            string text = SaveAndRead(Load(Path.Combine(root, "src")), "zone.yaml").Replace("\r", "");
            Assert.That(text, Does.Contain("entrance: 77701"));
            Assert.That(text, Does.Contain("typeA_zones:\n- 778"));
            Assert.That(text, Does.Contain("typeB_zones:\n- 779"));
        }

        [Test]
        public void ExitToNowhere_WithText_AndTriggerLanguage_Survive()
        {
            var z = Load(Path.Combine(root, "src"));
            string rooms = SaveAndRead(z, "rooms.yaml");
            Assert.That(rooms, Does.Contain("Глухая стена."));
            Assert.That(rooms, Does.Contain("to_room: -1"));
            Assert.That(SaveAndRead(z, "triggers.yaml"), Does.Contain("language: lua"));
        }

        [Test]
        public void ResetCommands_KeepEngineMeaning()
        {
            var cmds = Commands(SaveAndRead(Load(Path.Combine(root, "src")), "zone.yaml"));

            Assert.That(cmds, Does.Contain("MOB 0 77700 1 77700 1"), "per-command max_in_world, not the zone max");
            Assert.That(cmds, Does.Contain("MOB 0 77700 3 77701 1"));
            Assert.That(cmds, Does.Contain("GIVE 1 77700 -1"), "GIVE without load% is loaded, not crashed on");
            Assert.That(cmds, Does.Contain("GIVE 1 77700 -1 30"), "GIVE load% in the engine's 4th field");
            Assert.That(cmds.Any(c => c.StartsWith("EQUIP 1 77701 ") && c.EndsWith(" 16")), Is.True, string.Join("\n", cmds));
            Assert.That(cmds, Does.Contain("OBJECT 0 77702 -1 77700 1"), "1% stays 1%");
            Assert.That(cmds.Any(c => c.StartsWith("PUT 1 77703 ") && c.EndsWith(" 77702 25")), Is.True, string.Join("\n", cmds));
            Assert.That(cmds, Does.Contain("DOOR 0 77700 1 2"));
            Assert.That(cmds, Does.Contain("EXTRACT 0 77700"));
            Assert.That(cmds, Does.Contain("TRIGGER 0 2 77700 77700"), "unmodelled commands are kept verbatim");
            Assert.That(cmds, Does.Contain("MOB 2 77700 2 77799 1"), "virtual-room spawn kept, with its persist bit");
            // (the EQUIP/GIVE max field is the prototype's limit; the engine does not read it)
            Assert.That(cmds.Any(c => c.StartsWith("EQUIP 3 77701 ") && c.EndsWith(" 16 50")), Is.True,
                "if_flag 3 (check + persist) kept");
            Assert.That(cmds, Does.Contain("FOLLOW 1 77700 77700 77700"), "an unmodellable FOLLOW is kept verbatim");
            int m = cmds.IndexOf("MOB 0 77700 1 77700 1");
            Assert.That(m >= 0 && m + 1 < cmds.Count && cmds[m + 1] == "TRIGGER 1 0 77700", Is.True,
                "a mob-type TRIGGER stays right after its MOB:\n" + string.Join("\n", cmds));
            Assert.That(cmds.Count, Is.EqualTo(18), string.Join("\n", cmds));
        }

        /// <summary>
        /// Следование в виртуальной комнате зоны (NN99). Её мобы лежат в отдельной коллекции зоны,
        /// поэтому FOLLOW там не разбирался и уходил в дословные команды, а запись ставила F сразу
        /// за M последователя -- то есть раньше M лидера (движок такое следование теряет) и между
        /// мобом и его вещью, у которой бит 1 означает «только если предыдущая команда удалась».
        /// </summary>
        [Test]
        public void VirtualRoomFollow_KeepsItsPlaceAfterTheSpawnsAndItems()
        {
            var cmds = Commands(SaveAndRead(Load(Path.Combine(root, "src")), "zone.yaml"));
            string all = string.Join("\n", cmds);

            int follower = cmds.IndexOf("MOB 0 77701 1 77799 1");
            int leader = cmds.IndexOf("MOB 0 77702 1 77799 1");
            int give = cmds.IndexOf("GIVE 1 77703 -1");
            int follow = cmds.IndexOf("FOLLOW 1 77799 77702 77701");

            Assert.That(follower, Is.GreaterThanOrEqualTo(0), all);
            Assert.That(leader, Is.GreaterThanOrEqualTo(0), all);
            Assert.That(follow, Is.GreaterThanOrEqualTo(0), "the follow is modelled, not dropped:\n" + all);
            Assert.That(cmds.Count(c => c == "FOLLOW 1 77799 77702 77701"), Is.EqualTo(1),
                "and it is written once:\n" + all);
            Assert.That(give, Is.EqualTo(follower + 1), "the item follows its own MOB:\n" + all);
            Assert.That(follow, Is.GreaterThan(leader), "F comes after the leader's MOB:\n" + all);
        }

        [Test]
        public void MobMaxInWorld_EditedInUi_AppliesToEveryMobCommand()
        {
            var z = Load(Path.Combine(root, "src"));
            z.Mobs[77700, 0].MaxInWorld = 7;
            var cmds = Commands(SaveAndRead(z, "zone.yaml"));
            Assert.That(cmds, Does.Contain("MOB 0 77700 7 77700 1"));
            Assert.That(cmds, Does.Contain("MOB 0 77700 7 77701 1"));
        }

        [Test]
        public void SecondSave_IsByteStable_AndHasNoBom()
        {
            var first = Load(Path.Combine(root, "src"));
            string a = SaveAndRead(first, "zone.yaml") + SaveAndRead(first, "objects.yaml");
            byte[] head = File.ReadAllBytes(Path.Combine(root, "out", "zones", Zone, "zone.yaml")).Take(3).ToArray();
            Assert.That(head, Is.Not.EqualTo(new byte[] { 0xEF, 0xBB, 0xBF }), "engine-style files carry no BOM");

            var second = Load(Path.Combine(root, "out"));
            string b = SaveAndRead(second, "zone.yaml") + SaveAndRead(second, "objects.yaml");
            Assert.That(b, Is.EqualTo(a));
        }

        /// <summary>Deserializes saved objects.yaml the way the provider does (flat map rel -> body).</summary>
        private class YamlFormatProviderProbe
        {
            public Dictionary<int, YamlObj> Objects(string text) =>
                new YamlDotNet.Serialization.DeserializerBuilder()
                    .WithNamingConvention(YamlDotNet.Serialization.NamingConventions.UnderscoredNamingConvention.Instance)
                    .IgnoreUnmatchedProperties()
                    .Build()
                    .Deserialize<Dictionary<int, YamlObj>>(text);
        }
    }
}
