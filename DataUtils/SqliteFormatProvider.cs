using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace DataUtils
{
    /// <summary>
    /// Format provider for SQLite database format
    /// Based on schema from sqlite-world-schema.md
    /// </summary>
    public class SqliteFormatProvider : BaseFormatProvider
    {
        public override string FormatName => "sqlite";
        public override string FormatDescription => "SQLite Database Format";
        public override Encoding DefaultEncoding => Encoding.UTF8;

        private string GetDatabasePath(string zoneNumber = null)
        {
            return Path.Combine(StaticData.WorldFolderPath, "world.db");
        }

        private SQLiteConnection CreateConnection(string dbPath)
        {
            var connectionString = $"Data Source={dbPath};Version=3;";
            return new SQLiteConnection(connectionString);
        }

        #region Schema Creation

        private void EnsureSchema(SQLiteConnection conn)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    -- Main entity tables
                    CREATE TABLE IF NOT EXISTS zones (
                        vnum INTEGER PRIMARY KEY,
                        name TEXT NOT NULL,
                        comment TEXT,
                        location TEXT,
                        author TEXT,
                        description TEXT,
                        first_room INTEGER,
                        top_room INTEGER,
                        mode INTEGER DEFAULT 0,
                        zone_type INTEGER DEFAULT 0,
                        zone_group INTEGER DEFAULT 1,
                        entrance INTEGER,
                        lifespan INTEGER DEFAULT 10,
                        reset_mode INTEGER DEFAULT 2,
                        reset_idle INTEGER DEFAULT 0,
                        under_construction INTEGER DEFAULT 0
                    );

                    CREATE TABLE IF NOT EXISTS mobs (
                        vnum INTEGER PRIMARY KEY,
                        aliases TEXT,
                        name_nom TEXT,
                        name_gen TEXT,
                        name_dat TEXT,
                        name_acc TEXT,
                        name_ins TEXT,
                        name_pre TEXT,
                        short_desc TEXT,
                        long_desc TEXT,
                        alignment INTEGER DEFAULT 0,
                        mob_type TEXT DEFAULT 'E',
                        level INTEGER DEFAULT 1,
                        hitroll_penalty INTEGER DEFAULT 0,
                        armor INTEGER DEFAULT 100,
                        hp_dice_count INTEGER DEFAULT 1,
                        hp_dice_size INTEGER DEFAULT 1,
                        hp_bonus INTEGER DEFAULT 0,
                        dam_dice_count INTEGER DEFAULT 1,
                        dam_dice_size INTEGER DEFAULT 1,
                        dam_bonus INTEGER DEFAULT 0,
                        gold_dice_count INTEGER DEFAULT 0,
                        gold_dice_size INTEGER DEFAULT 0,
                        gold_bonus INTEGER DEFAULT 0,
                        experience INTEGER DEFAULT 0,
                        default_pos INTEGER DEFAULT 8,
                        start_pos INTEGER DEFAULT 8,
                        sex INTEGER DEFAULT 1,
                        size INTEGER DEFAULT 50,
                        height INTEGER DEFAULT 170,
                        weight INTEGER DEFAULT 70,
                        mob_class INTEGER DEFAULT 100,
                        race INTEGER DEFAULT 100,
                        max_in_world INTEGER DEFAULT -1,
                        attr_str INTEGER DEFAULT 11,
                        attr_dex INTEGER DEFAULT 11,
                        attr_int INTEGER DEFAULT 11,
                        attr_wis INTEGER DEFAULT 11,
                        attr_con INTEGER DEFAULT 11,
                        attr_cha INTEGER DEFAULT 11,
                        hp_regen INTEGER DEFAULT 0,
                        armour_bonus INTEGER DEFAULT 0,
                        mana_regen INTEGER DEFAULT 0,
                        cast_success INTEGER DEFAULT 0,
                        morale INTEGER DEFAULT 0,
                        initiative_add INTEGER DEFAULT 0,
                        absorb INTEGER DEFAULT 0,
                        aresist INTEGER DEFAULT 0,
                        mresist INTEGER DEFAULT 0,
                        presist INTEGER DEFAULT 0,
                        bare_hand_attack INTEGER DEFAULT 0,
                        like_work INTEGER DEFAULT 0,
                        max_factor INTEGER DEFAULT 0,
                        extra_attack INTEGER DEFAULT 0,
                        special_bitvector TEXT,
                        resist_fire INTEGER DEFAULT 0,
                        resist_air INTEGER DEFAULT 0,
                        resist_water INTEGER DEFAULT 0,
                        resist_earth INTEGER DEFAULT 0,
                        resist_dark INTEGER DEFAULT 0,
                        resist_vitality INTEGER DEFAULT 0,
                        resist_mind INTEGER DEFAULT 0,
                        resist_immunity INTEGER DEFAULT 0,
                        save_paralyze INTEGER DEFAULT 0,
                        save_breath INTEGER DEFAULT 0,
                        save_magic INTEGER DEFAULT 0,
                        save_skills INTEGER DEFAULT 0
                    );

                    CREATE TABLE IF NOT EXISTS objects (
                        vnum INTEGER PRIMARY KEY,
                        aliases TEXT,
                        name_nom TEXT,
                        name_gen TEXT,
                        name_dat TEXT,
                        name_acc TEXT,
                        name_ins TEXT,
                        name_pre TEXT,
                        short_desc TEXT,
                        action_desc TEXT,
                        obj_type INTEGER DEFAULT 12,
                        material INTEGER DEFAULT 0,
                        value0 TEXT,
                        value1 TEXT,
                        value2 TEXT,
                        value3 TEXT,
                        weight INTEGER DEFAULT 0,
                        cost INTEGER DEFAULT 0,
                        rent_off INTEGER DEFAULT 0,
                        rent_on INTEGER DEFAULT 0,
                        spec_param INTEGER DEFAULT 0,
                        max_durability INTEGER DEFAULT 100,
                        cur_durability INTEGER DEFAULT 100,
                        timer INTEGER DEFAULT -1,
                        spell INTEGER DEFAULT -1,
                        level INTEGER DEFAULT 0,
                        sex INTEGER DEFAULT 0,
                        max_in_world INTEGER DEFAULT -1,
                        min_remorts INTEGER DEFAULT 0
                    );

                    CREATE TABLE IF NOT EXISTS rooms (
                        vnum INTEGER PRIMARY KEY,
                        zone_vnum INTEGER,
                        name TEXT,
                        description TEXT,
                        sector_id INTEGER DEFAULT 0,
                        x INTEGER,
                        y INTEGER,
                        z INTEGER,
                        placed_on_map INTEGER DEFAULT 0
                    );

                    CREATE TABLE IF NOT EXISTS triggers (
                        vnum INTEGER PRIMARY KEY,
                        name TEXT,
                        attach_type INTEGER DEFAULT 0,
                        narg INTEGER DEFAULT 0,
                        arglist TEXT,
                        script TEXT
                    );

                    -- Linking tables
                    CREATE TABLE IF NOT EXISTS zone_groups (
                        zone_vnum INTEGER,
                        linked_zone_vnum INTEGER,
                        group_type TEXT,
                        PRIMARY KEY (zone_vnum, linked_zone_vnum, group_type)
                    );

                    CREATE TABLE IF NOT EXISTS mob_flags (
                        mob_vnum INTEGER,
                        flag_category TEXT,
                        flag_name TEXT,
                        PRIMARY KEY (mob_vnum, flag_category, flag_name)
                    );

                    CREATE TABLE IF NOT EXISTS mob_skills (
                        mob_vnum INTEGER,
                        skill_id INTEGER,
                        value INTEGER,
                        PRIMARY KEY (mob_vnum, skill_id)
                    );

                    CREATE TABLE IF NOT EXISTS mob_spells (
                        mob_vnum INTEGER,
                        spell_id INTEGER,
                        count INTEGER,
                        PRIMARY KEY (mob_vnum, spell_id)
                    );

                    CREATE TABLE IF NOT EXISTS mob_feats (
                        mob_vnum INTEGER,
                        feat_id INTEGER,
                        PRIMARY KEY (mob_vnum, feat_id)
                    );

                    CREATE TABLE IF NOT EXISTS mob_helpers (
                        mob_vnum INTEGER,
                        helper_vnum INTEGER,
                        PRIMARY KEY (mob_vnum, helper_vnum)
                    );

                    CREATE TABLE IF NOT EXISTS mob_destinations (
                        mob_vnum INTEGER,
                        destination_vnum INTEGER,
                        PRIMARY KEY (mob_vnum, destination_vnum)
                    );

                    CREATE TABLE IF NOT EXISTS mob_ingredients (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        mob_vnum INTEGER,
                        type_name TEXT,
                        power INTEGER,
                        probability INTEGER,
                        power_auto INTEGER DEFAULT 0
                    );

                    CREATE TABLE IF NOT EXISTS mob_loaded_objects (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        mob_vnum INTEGER,
                        obj_vnum INTEGER,
                        probability INTEGER
                    );

                    CREATE TABLE IF NOT EXISTS trigger_types (
                        trigger_vnum INTEGER,
                        type_name TEXT,
                        PRIMARY KEY (trigger_vnum, type_name)
                    );

                    CREATE TABLE IF NOT EXISTS obj_flags (
                        obj_vnum INTEGER,
                        flag_category TEXT,
                        flag_name TEXT,
                        PRIMARY KEY (obj_vnum, flag_category, flag_name)
                    );

                    CREATE TABLE IF NOT EXISTS obj_applies (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        obj_vnum INTEGER,
                        location INTEGER,
                        modifier INTEGER
                    );

                    CREATE TABLE IF NOT EXISTS room_flags (
                        room_vnum INTEGER,
                        flag_name TEXT,
                        PRIMARY KEY (room_vnum, flag_name)
                    );

                    CREATE TABLE IF NOT EXISTS room_exits (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        room_vnum INTEGER,
                        direction INTEGER,
                        description TEXT,
                        keywords TEXT,
                        exit_flags INTEGER DEFAULT 0,
                        key_vnum INTEGER DEFAULT -1,
                        to_room INTEGER DEFAULT -1,
                        lock_complexity INTEGER DEFAULT 0,
                        UNIQUE (room_vnum, direction)
                    );

                    CREATE TABLE IF NOT EXISTS room_ingredients (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        room_vnum INTEGER,
                        type_name TEXT,
                        power INTEGER,
                        probability INTEGER,
                        power_auto INTEGER DEFAULT 0
                    );

                    CREATE TABLE IF NOT EXISTS extra_descriptions (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        entity_type TEXT,
                        entity_vnum INTEGER,
                        keywords TEXT,
                        description TEXT
                    );

                    CREATE TABLE IF NOT EXISTS entity_triggers (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        entity_type TEXT,
                        entity_vnum INTEGER,
                        trigger_vnum INTEGER,
                        UNIQUE (entity_type, entity_vnum, trigger_vnum)
                    );

                    CREATE TABLE IF NOT EXISTS room_descriptions (
                        room_vnum INTEGER PRIMARY KEY,
                        main TEXT,
                        day TEXT,
                        night TEXT
                    );

                    -- Indexes
                    CREATE INDEX IF NOT EXISTS idx_mob_flags_vnum ON mob_flags(mob_vnum);
                    CREATE INDEX IF NOT EXISTS idx_mob_skills_vnum ON mob_skills(mob_vnum);
                    CREATE INDEX IF NOT EXISTS idx_obj_flags_vnum ON obj_flags(obj_vnum);
                    CREATE INDEX IF NOT EXISTS idx_obj_applies_vnum ON obj_applies(obj_vnum);
                    CREATE INDEX IF NOT EXISTS idx_room_flags_vnum ON room_flags(room_vnum);
                    CREATE INDEX IF NOT EXISTS idx_room_exits_vnum ON room_exits(room_vnum);
                    CREATE INDEX IF NOT EXISTS idx_extra_desc_entity ON extra_descriptions(entity_type, entity_vnum);
                    CREATE INDEX IF NOT EXISTS idx_entity_triggers ON entity_triggers(entity_type, entity_vnum);
                    CREATE INDEX IF NOT EXISTS idx_rooms_zone ON rooms(zone_vnum);
                ";
                cmd.ExecuteNonQuery();
            }
        }

        #endregion

        #region Load Operations

        public override bool LoadZone(Zone zone, MobsCollection mobs, RoomsCollection rooms, string zoneNumber, Encoding encoding)
        {
            try
            {
                string dbPath = GetDatabasePath();
                if (!File.Exists(dbPath))
                {
                    FireExceptionEvent($"Database not found: {dbPath}", null, EventLogEntryType.Warning);
                    return false;
                }

                using (var conn = CreateConnection(dbPath))
                {
                    conn.Open();
                    int vnum = int.Parse(zoneNumber);

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM zones WHERE vnum = @vnum";
                        cmd.Parameters.AddWithValue("@vnum", vnum);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                FireExceptionEvent($"Zone {zoneNumber} not found in database", null, EventLogEntryType.Warning);
                                return false;
                            }

                            zone.Number = reader.GetInt32(reader.GetOrdinal("vnum"));
                            zone.Name = GetStringOrEmpty(reader, "name");
                            zone.Comment = GetStringOrEmpty(reader, "comment");
                            zone.Location = GetStringOrEmpty(reader, "location");
                            zone.Author = GetStringOrEmpty(reader, "author");
                            zone.Description = GetStringOrEmpty(reader, "description");
                            zone.LastRoomNumber = reader.GetInt32(reader.GetOrdinal("top_room"));
                            zone.Level = reader.GetInt32(reader.GetOrdinal("mode"));
                            zone.Type = reader.GetInt32(reader.GetOrdinal("zone_type"));
                            zone.OptimalCharsInGroup = reader.GetInt32(reader.GetOrdinal("zone_group"));
                            zone.RepopTimer = reader.GetInt32(reader.GetOrdinal("lifespan"));
                            zone.RepopType = reader.GetInt32(reader.GetOrdinal("reset_mode"));
                            zone.ResetIdle = reader.GetInt32(reader.GetOrdinal("reset_idle"));
                            zone.Test = reader.GetInt32(reader.GetOrdinal("under_construction")) != 0;
                        }
                    }

                    // Load zone groups
                    LoadZoneGroups(conn, zone);
                }
                return true;
            }
            catch (Exception ex)
            {
                FireExceptionEvent($"Error loading SQLite zone {zoneNumber}", ex, EventLogEntryType.Error);
                return false;
            }
        }

        private void LoadZoneGroups(SQLiteConnection conn, Zone zone)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT linked_zone_vnum, group_type FROM zone_groups WHERE zone_vnum = @vnum";
                cmd.Parameters.AddWithValue("@vnum", zone.Number);

                zone.ResetA.Clear();
                zone.ResetB.Clear();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int linkedVnum = reader.GetInt32(0);
                        string groupType = reader.GetString(1);
                        if (groupType == "A")
                            zone.ResetA.Add(linkedVnum);
                        else if (groupType == "B")
                            zone.ResetB.Add(linkedVnum);
                    }
                }
            }
        }

        public override bool LoadRooms(RoomsCollection rooms, string zoneNumber, Encoding encoding)
        {
            try
            {
                string dbPath = GetDatabasePath();
                if (!File.Exists(dbPath))
                    return true; // No database yet

                using (var conn = CreateConnection(dbPath))
                {
                    conn.Open();
                    int zoneVnum = int.Parse(zoneNumber);

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM rooms WHERE zone_vnum = @zone_vnum";
                        cmd.Parameters.AddWithValue("@zone_vnum", zoneVnum);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var room = ReadRoom(reader);
                                rooms.Add(room);
                            }
                        }
                    }

                    // Load related data for each room
                    foreach (Room room in rooms)
                    {
                        LoadRoomFlags(conn, room);
                        LoadRoomExits(conn, room);
                        LoadRoomExtraDescs(conn, room);
                        LoadRoomTriggers(conn, room);
                        LoadRoomIngredients(conn, room);
                        LoadRoomDescriptions(conn, room);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                FireExceptionEvent($"Error loading SQLite rooms for zone {zoneNumber}", ex, EventLogEntryType.Error);
                return false;
            }
        }

        private Room ReadRoom(SQLiteDataReader reader)
        {
            var room = new Room(reader.GetInt32(reader.GetOrdinal("vnum")))
            {
                ZoneNum = reader.GetInt32(reader.GetOrdinal("zone_vnum")),
                Name = GetStringOrEmpty(reader, "name"),
                SectorType = reader.GetInt32(reader.GetOrdinal("sector_id"))
            };
            room.Description.Main = GetStringOrEmpty(reader, "description");

            if (!reader.IsDBNull(reader.GetOrdinal("x")))
                room.X = reader.GetInt32(reader.GetOrdinal("x"));
            if (!reader.IsDBNull(reader.GetOrdinal("y")))
                room.Y = reader.GetInt32(reader.GetOrdinal("y"));
            if (!reader.IsDBNull(reader.GetOrdinal("z")))
                room.Z = reader.GetInt32(reader.GetOrdinal("z"));
            room.PlacedOnMap = reader.GetInt32(reader.GetOrdinal("placed_on_map")) != 0;

            return room;
        }

        private void LoadRoomFlags(SQLiteConnection conn, Room room)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT flag_name FROM room_flags WHERE room_vnum = @vnum";
                cmd.Parameters.AddWithValue("@vnum", room.VNum);

                var flags = new List<string>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        flags.Add(reader.GetString(0));
                }
                if (flags.Count > 0)
                    room.Flags = string.Join(" ", flags.ToArray());
            }
        }

        private void LoadRoomExits(SQLiteConnection conn, Room room)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM room_exits WHERE room_vnum = @vnum";
                cmd.Parameters.AddWithValue("@vnum", room.VNum);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int direction = reader.GetInt32(reader.GetOrdinal("direction"));
                        Exit exit = GetExitByDirection(room, direction);
                        if (exit != null)
                        {
                            exit.Description = GetStringOrEmpty(reader, "description");
                            exit.Aliases = GetStringOrEmpty(reader, "keywords");
                            exit.ExitFlag = reader.GetInt32(reader.GetOrdinal("exit_flags"));
                            exit.Key = reader.GetInt32(reader.GetOrdinal("key_vnum"));
                            exit.RoomVNum = reader.GetInt32(reader.GetOrdinal("to_room"));
                            exit.LockLevel = reader.GetInt32(reader.GetOrdinal("lock_complexity"));
                        }
                    }
                }
            }
        }

        private void LoadRoomExtraDescs(SQLiteConnection conn, Room room)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT keywords, description FROM extra_descriptions WHERE entity_type = 'room' AND entity_vnum = @vnum";
                cmd.Parameters.AddWithValue("@vnum", room.VNum);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        room.ExtraDescriptions.Add(new ExtraDesc(
                            GetStringOrEmpty(reader, "keywords"),
                            GetStringOrEmpty(reader, "description")
                        ));
                    }
                }
            }
        }

        private void LoadRoomTriggers(SQLiteConnection conn, Room room)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT trigger_vnum FROM entity_triggers WHERE entity_type = 'room' AND entity_vnum = @vnum";
                cmd.Parameters.AddWithValue("@vnum", room.VNum);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        room.TriggersList.Add(reader.GetInt32(0));
                }
            }
        }

        private void LoadRoomIngredients(SQLiteConnection conn, Room room)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT type_name, power, probability, power_auto FROM room_ingredients WHERE room_vnum = @vnum";
                cmd.Parameters.AddWithValue("@vnum", room.VNum);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bool powerAuto = reader.GetInt32(3) != 0;
                        var ingredient = powerAuto
                            ? new Ingredient(reader.GetString(0), reader.GetInt32(2))
                            : new Ingredient(reader.GetString(0), reader.GetInt32(1), reader.GetInt32(2));
                        room.Ingredients.Add(ingredient);
                    }
                }
            }
        }

        private void LoadRoomDescriptions(SQLiteConnection conn, Room room)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT main, day, night FROM room_descriptions WHERE room_vnum = @vnum";
                cmd.Parameters.AddWithValue("@vnum", room.VNum);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        room.Description.Main = GetStringOrEmpty(reader, "main");
                        room.Description.Day = GetStringOrEmpty(reader, "day");
                        room.Description.Night = GetStringOrEmpty(reader, "night");
                    }
                }
            }
        }

        private Exit GetExitByDirection(Room room, int direction)
        {
            switch (direction)
            {
                case 0: return room.ExitNorth;
                case 1: return room.ExitEast;
                case 2: return room.ExitSouth;
                case 3: return room.ExitWest;
                case 4: return room.ExitUp;
                case 5: return room.ExitDown;
                default: return null;
            }
        }

        public override bool LoadMobs(MobsCollection mobs, string zoneNumber, Encoding encoding)
        {
            try
            {
                string dbPath = GetDatabasePath();
                if (!File.Exists(dbPath))
                    return true;

                using (var conn = CreateConnection(dbPath))
                {
                    conn.Open();
                    int zoneVnum = int.Parse(zoneNumber);
                    int minVnum = zoneVnum * 100;
                    int maxVnum = minVnum + 99;

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM mobs WHERE vnum >= @min AND vnum <= @max";
                        cmd.Parameters.AddWithValue("@min", minVnum);
                        cmd.Parameters.AddWithValue("@max", maxVnum);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var mob = ReadMob(reader);
                                mobs.Add(mob);
                            }
                        }
                    }

                    // Load related data
                    foreach (Mob mob in mobs)
                    {
                        LoadMobFlags(conn, mob);
                        LoadMobSkills(conn, mob);
                        LoadMobSpells(conn, mob);
                        LoadMobFeats(conn, mob);
                        LoadMobHelpers(conn, mob);
                        LoadMobDestinations(conn, mob);
                        LoadMobIngredients(conn, mob);
                        LoadMobLoadedObjects(conn, mob);
                        LoadMobTriggers(conn, mob);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                FireExceptionEvent($"Error loading SQLite mobs for zone {zoneNumber}", ex, EventLogEntryType.Error);
                return false;
            }
        }

        private Mob ReadMob(SQLiteDataReader reader)
        {
            var mob = new Mob(reader.GetInt32(reader.GetOrdinal("vnum")))
            {
                Alias = GetStringOrEmpty(reader, "aliases"),
                Desc = GetStringOrEmpty(reader, "short_desc"),
                DetailDescr = GetStringOrEmpty(reader, "long_desc"),
                Align = reader.GetInt32(reader.GetOrdinal("alignment")),
                Level = reader.GetInt32(reader.GetOrdinal("level")),
                Hitroll = reader.GetInt32(reader.GetOrdinal("hitroll_penalty")),
                Ac = reader.GetInt32(reader.GetOrdinal("armor")),
                Exp = reader.GetInt32(reader.GetOrdinal("experience")),
                PosDefault = reader.GetInt32(reader.GetOrdinal("default_pos")),
                PosLoad = reader.GetInt32(reader.GetOrdinal("start_pos")),
                Sex = reader.GetInt32(reader.GetOrdinal("sex")),
                Class = reader.GetInt32(reader.GetOrdinal("mob_class")),
                Race = reader.GetInt32(reader.GetOrdinal("race")),
                MaxInWorld = reader.GetInt32(reader.GetOrdinal("max_in_world"))
            };

            // Names
            mob.Cases.Imen = GetStringOrEmpty(reader, "name_nom");
            mob.Cases.Rod = GetStringOrEmpty(reader, "name_gen");
            mob.Cases.Dat = GetStringOrEmpty(reader, "name_dat");
            mob.Cases.Vin = GetStringOrEmpty(reader, "name_acc");
            mob.Cases.Tvor = GetStringOrEmpty(reader, "name_ins");
            mob.Cases.Pred = GetStringOrEmpty(reader, "name_pre");

            // Dice
            mob.Hits = FormatDice(
                reader.GetInt32(reader.GetOrdinal("hp_dice_count")),
                reader.GetInt32(reader.GetOrdinal("hp_dice_size")),
                reader.GetInt32(reader.GetOrdinal("hp_bonus")));
            mob.Damage = FormatDice(
                reader.GetInt32(reader.GetOrdinal("dam_dice_count")),
                reader.GetInt32(reader.GetOrdinal("dam_dice_size")),
                reader.GetInt32(reader.GetOrdinal("dam_bonus")));
            mob.Money = FormatDice(
                reader.GetInt32(reader.GetOrdinal("gold_dice_count")),
                reader.GetInt32(reader.GetOrdinal("gold_dice_size")),
                reader.GetInt32(reader.GetOrdinal("gold_bonus")));

            // Stats
            mob.Stats.Size = reader.GetInt32(reader.GetOrdinal("size"));
            mob.Stats.Height = reader.GetInt32(reader.GetOrdinal("height"));
            mob.Stats.Weight = reader.GetInt32(reader.GetOrdinal("weight"));
            mob.Stats.Str = reader.GetInt32(reader.GetOrdinal("attr_str"));
            mob.Stats.Dex = reader.GetInt32(reader.GetOrdinal("attr_dex"));
            mob.Stats.Int = reader.GetInt32(reader.GetOrdinal("attr_int"));
            mob.Stats.Wis = reader.GetInt32(reader.GetOrdinal("attr_wis"));
            mob.Stats.Con = reader.GetInt32(reader.GetOrdinal("attr_con"));
            mob.Stats.Cha = reader.GetInt32(reader.GetOrdinal("attr_cha"));

            // Enhanced fields
            mob.HPreg = reader.GetInt32(reader.GetOrdinal("hp_regen"));
            mob.Armour = reader.GetInt32(reader.GetOrdinal("armour_bonus"));
            mob.PlusMem = reader.GetInt32(reader.GetOrdinal("mana_regen"));
            mob.CastSuccess = reader.GetInt32(reader.GetOrdinal("cast_success"));
            mob.Luck = reader.GetInt32(reader.GetOrdinal("morale"));
            mob.Initiative = reader.GetInt32(reader.GetOrdinal("initiative_add"));
            mob.Absorbe = reader.GetInt32(reader.GetOrdinal("absorb"));
            mob.AResist = reader.GetInt32(reader.GetOrdinal("aresist"));
            mob.MResist = reader.GetInt32(reader.GetOrdinal("mresist"));
            mob.PResist = reader.GetInt32(reader.GetOrdinal("presist"));
            mob.BareHandAttack = reader.GetInt32(reader.GetOrdinal("bare_hand_attack"));
            mob.LikeWork = reader.GetInt32(reader.GetOrdinal("like_work"));
            mob.MaxFactor = reader.GetInt32(reader.GetOrdinal("max_factor"));
            mob.ExtraAttack = reader.GetInt32(reader.GetOrdinal("extra_attack"));
            mob.SpecialBitvector = GetStringOrEmpty(reader, "special_bitvector");

            // Resistances
            mob.ResistFromFire = reader.GetInt32(reader.GetOrdinal("resist_fire"));
            mob.ResistFromAir = reader.GetInt32(reader.GetOrdinal("resist_air"));
            mob.ResistFromWater = reader.GetInt32(reader.GetOrdinal("resist_water"));
            mob.ResistFromEarth = reader.GetInt32(reader.GetOrdinal("resist_earth"));
            mob.ResistDark = reader.GetInt32(reader.GetOrdinal("resist_dark"));
            mob.Vitality = reader.GetInt32(reader.GetOrdinal("resist_vitality"));
            mob.Mind = reader.GetInt32(reader.GetOrdinal("resist_mind"));
            mob.Immunitet = reader.GetInt32(reader.GetOrdinal("resist_immunity"));

            // Saves
            mob.SaveParalyzeCast = reader.GetInt32(reader.GetOrdinal("save_paralyze"));
            mob.SaveMagBreathes = reader.GetInt32(reader.GetOrdinal("save_breath"));
            mob.SaveMagDamages = reader.GetInt32(reader.GetOrdinal("save_magic"));
            mob.SaveFightSkills = reader.GetInt32(reader.GetOrdinal("save_skills"));

            return mob;
        }

        private void LoadMobFlags(SQLiteConnection conn, Mob mob)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT flag_category, flag_name FROM mob_flags WHERE mob_vnum = @vnum";
                cmd.Parameters.AddWithValue("@vnum", mob.VNum);

                var actionFlags = new List<string>();
                var affectFlags = new List<string>();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string category = reader.GetString(0);
                        string flagName = reader.GetString(1);
                        if (category == "action")
                            actionFlags.Add(flagName);
                        else if (category == "affect")
                            affectFlags.Add(flagName);
                    }
                }

                if (actionFlags.Count > 0)
                    mob.Flags = string.Join(" ", actionFlags.ToArray());
                if (affectFlags.Count > 0)
                    mob.Affects = string.Join(" ", affectFlags.ToArray());
            }
        }

        private void LoadMobSkills(SQLiteConnection conn, Mob mob)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT skill_id, value FROM mob_skills WHERE mob_vnum = @vnum";
                cmd.Parameters.AddWithValue("@vnum", mob.VNum);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        mob.Skills.Add(new MobSkill(reader.GetInt32(0), reader.GetInt32(1)));
                }
            }
        }

        private void LoadMobSpells(SQLiteConnection conn, Mob mob)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT spell_id, count FROM mob_spells WHERE mob_vnum = @vnum";
                cmd.Parameters.AddWithValue("@vnum", mob.VNum);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        mob.Spells.Add(new MobSpell(reader.GetInt32(0), reader.GetInt32(1)));
                }
            }
        }

        private void LoadMobFeats(SQLiteConnection conn, Mob mob)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT feat_id FROM mob_feats WHERE mob_vnum = @vnum";
                cmd.Parameters.AddWithValue("@vnum", mob.VNum);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        mob.Feats.Add(reader.GetInt32(0));
                }
            }
        }

        private void LoadMobHelpers(SQLiteConnection conn, Mob mob)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT helper_vnum FROM mob_helpers WHERE mob_vnum = @vnum";
                cmd.Parameters.AddWithValue("@vnum", mob.VNum);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        mob.Helpers.Add(reader.GetInt32(0));
                }
            }
        }

        private void LoadMobDestinations(SQLiteConnection conn, Mob mob)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT destination_vnum FROM mob_destinations WHERE mob_vnum = @vnum";
                cmd.Parameters.AddWithValue("@vnum", mob.VNum);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        mob.Destination.Add(reader.GetInt32(0));
                }
            }
        }

        private void LoadMobIngredients(SQLiteConnection conn, Mob mob)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT type_name, power, probability, power_auto FROM mob_ingredients WHERE mob_vnum = @vnum";
                cmd.Parameters.AddWithValue("@vnum", mob.VNum);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bool powerAuto = reader.GetInt32(3) != 0;
                        var ingredient = powerAuto
                            ? new Ingredient(reader.GetString(0), reader.GetInt32(2))
                            : new Ingredient(reader.GetString(0), reader.GetInt32(1), reader.GetInt32(2));
                        mob.Ingredients.Add(ingredient);
                    }
                }
            }
        }

        private void LoadMobLoadedObjects(SQLiteConnection conn, Mob mob)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT obj_vnum, probability FROM mob_loaded_objects WHERE mob_vnum = @vnum";
                cmd.Parameters.AddWithValue("@vnum", mob.VNum);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        mob.LoadedObjectAfterDeath.Add(new LoadedObjAfterDeath(reader.GetInt32(0))
                        {
                            LoadProb = reader.GetInt32(1)
                        });
                    }
                }
            }
        }

        private void LoadMobTriggers(SQLiteConnection conn, Mob mob)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT trigger_vnum FROM entity_triggers WHERE entity_type = 'mob' AND entity_vnum = @vnum";
                cmd.Parameters.AddWithValue("@vnum", mob.VNum);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        mob.TriggersList.Add(reader.GetInt32(0));
                }
            }
        }

        public override bool LoadObjects(ObjsCollection objects, string zoneNumber, Encoding encoding)
        {
            try
            {
                string dbPath = GetDatabasePath();
                if (!File.Exists(dbPath))
                    return true;

                using (var conn = CreateConnection(dbPath))
                {
                    conn.Open();
                    int zoneVnum = int.Parse(zoneNumber);
                    int minVnum = zoneVnum * 100;
                    int maxVnum = minVnum + 99;

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM objects WHERE vnum >= @min AND vnum <= @max";
                        cmd.Parameters.AddWithValue("@min", minVnum);
                        cmd.Parameters.AddWithValue("@max", maxVnum);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var obj = ReadObject(reader);
                                objects.Add(obj);
                            }
                        }
                    }

                    foreach (Obj obj in objects)
                    {
                        LoadObjFlags(conn, obj);
                        LoadObjApplies(conn, obj);
                        LoadObjExtraDescs(conn, obj);
                        LoadObjTriggers(conn, obj);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                FireExceptionEvent($"Error loading SQLite objects for zone {zoneNumber}", ex, EventLogEntryType.Error);
                return false;
            }
        }

        private Obj ReadObject(SQLiteDataReader reader)
        {
            var obj = new Obj(reader.GetInt32(reader.GetOrdinal("vnum")))
            {
                Alias = GetStringOrEmpty(reader, "aliases"),
                Desc = GetStringOrEmpty(reader, "short_desc"),
                ActionDesc = GetStringOrEmpty(reader, "action_desc"),
                Type = reader.GetInt32(reader.GetOrdinal("obj_type")),
                Material = reader.GetInt32(reader.GetOrdinal("material")),
                Param1 = GetStringOrEmpty(reader, "value0"),
                Param2 = GetStringOrEmpty(reader, "value1"),
                Param3 = GetStringOrEmpty(reader, "value2"),
                Param4 = GetStringOrEmpty(reader, "value3"),
                Weight = reader.GetInt32(reader.GetOrdinal("weight")),
                Price = reader.GetInt32(reader.GetOrdinal("cost")),
                RentInv = reader.GetInt32(reader.GetOrdinal("rent_off")),
                RentWear = reader.GetInt32(reader.GetOrdinal("rent_on")),
                MaxDurab = reader.GetInt32(reader.GetOrdinal("max_durability")),
                CurrDurab = reader.GetInt32(reader.GetOrdinal("cur_durability")),
                Timer = reader.GetInt32(reader.GetOrdinal("timer")),
                Spell = reader.GetInt32(reader.GetOrdinal("spell")),
                SpellLevel = reader.GetInt32(reader.GetOrdinal("level")),
                Sex = reader.GetInt32(reader.GetOrdinal("sex")),
                MaxInWorld = reader.GetInt32(reader.GetOrdinal("max_in_world")),
                MinimumRemorts = reader.GetInt32(reader.GetOrdinal("min_remorts"))
            };

            // Names
            obj.Cases.Imen = GetStringOrEmpty(reader, "name_nom");
            obj.Cases.Rod = GetStringOrEmpty(reader, "name_gen");
            obj.Cases.Dat = GetStringOrEmpty(reader, "name_dat");
            obj.Cases.Vin = GetStringOrEmpty(reader, "name_acc");
            obj.Cases.Tvor = GetStringOrEmpty(reader, "name_ins");
            obj.Cases.Pred = GetStringOrEmpty(reader, "name_pre");

            return obj;
        }

        private void LoadObjFlags(SQLiteConnection conn, Obj obj)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT flag_category, flag_name FROM obj_flags WHERE obj_vnum = @vnum";
                cmd.Parameters.AddWithValue("@vnum", obj.VNum);

                var extraFlags = new List<string>();
                var wearFlags = new List<string>();
                var noFlags = new List<string>();
                var antiFlags = new List<string>();
                var affectFlags = new List<string>();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string category = reader.GetString(0);
                        string flagName = reader.GetString(1);
                        switch (category)
                        {
                            case "extra": extraFlags.Add(flagName); break;
                            case "wear": wearFlags.Add(flagName); break;
                            case "no": noFlags.Add(flagName); break;
                            case "anti": antiFlags.Add(flagName); break;
                            case "affect": affectFlags.Add(flagName); break;
                        }
                    }
                }

                if (extraFlags.Count > 0) obj.Affects = string.Join(" ", extraFlags.ToArray());
                if (wearFlags.Count > 0) obj.WearFlags = string.Join(" ", wearFlags.ToArray());
                if (noFlags.Count > 0) obj.CantTouch = string.Join(" ", noFlags.ToArray());
                if (antiFlags.Count > 0) obj.CantUse = string.Join(" ", antiFlags.ToArray());
                if (affectFlags.Count > 0) obj.MagicFlags = string.Join(" ", affectFlags.ToArray());
            }
        }

        private void LoadObjApplies(SQLiteConnection conn, Obj obj)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT location, modifier FROM obj_applies WHERE obj_vnum = @vnum";
                cmd.Parameters.AddWithValue("@vnum", obj.VNum);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        obj.BonusesCollection.Add(new Bonus(reader.GetInt32(0), reader.GetInt32(1)));
                }
            }
        }

        private void LoadObjExtraDescs(SQLiteConnection conn, Obj obj)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT keywords, description FROM extra_descriptions WHERE entity_type = 'obj' AND entity_vnum = @vnum";
                cmd.Parameters.AddWithValue("@vnum", obj.VNum);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        obj.ExtraDescriptions.Add(new ExtraDesc(
                            GetStringOrEmpty(reader, "keywords"),
                            GetStringOrEmpty(reader, "description")
                        ));
                    }
                }
            }
        }

        private void LoadObjTriggers(SQLiteConnection conn, Obj obj)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT trigger_vnum FROM entity_triggers WHERE entity_type = 'obj' AND entity_vnum = @vnum";
                cmd.Parameters.AddWithValue("@vnum", obj.VNum);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        obj.TriggersList.Add(reader.GetInt32(0));
                }
            }
        }

        public override bool LoadTriggers(TriggersCollection triggers, string zoneNumber, Encoding encoding)
        {
            try
            {
                string dbPath = GetDatabasePath();
                if (!File.Exists(dbPath))
                    return true;

                using (var conn = CreateConnection(dbPath))
                {
                    conn.Open();
                    int zoneVnum = int.Parse(zoneNumber);
                    int minVnum = zoneVnum * 100;
                    int maxVnum = minVnum + 99;

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM triggers WHERE vnum >= @min AND vnum <= @max";
                        cmd.Parameters.AddWithValue("@min", minVnum);
                        cmd.Parameters.AddWithValue("@max", maxVnum);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var trigger = new Trigger(reader.GetInt32(reader.GetOrdinal("vnum")))
                                {
                                    Name = GetStringOrEmpty(reader, "name"),
                                    Class = reader.GetInt32(reader.GetOrdinal("attach_type")),
                                    NumArg = reader.GetInt32(reader.GetOrdinal("narg")),
                                    Arg = GetStringOrEmpty(reader, "arglist"),
                                    Body = GetStringOrEmpty(reader, "script")
                                };
                                triggers.Add(trigger);
                            }
                        }
                    }

                    // Load trigger types
                    foreach (Trigger trigger in triggers)
                        LoadTriggerTypes(conn, trigger);
                }
                return true;
            }
            catch (Exception ex)
            {
                FireExceptionEvent($"Error loading SQLite triggers for zone {zoneNumber}", ex, EventLogEntryType.Error);
                return false;
            }
        }

        private void LoadTriggerTypes(SQLiteConnection conn, Trigger trigger)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT type_name FROM trigger_types WHERE trigger_vnum = @vnum";
                cmd.Parameters.AddWithValue("@vnum", trigger.VNum);

                var types = new List<string>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        types.Add(reader.GetString(0));
                }
                if (types.Count > 0)
                    trigger.Type = string.Join(" ", types.ToArray());
            }
        }

        public override bool LoadSketches(SketchRoomsCollection sketches, string zoneNumber, Encoding encoding)
        {
            // Sketches are stored as room coordinates in SQLite
            return true;
        }

        #endregion

        #region Save Operations

        public override void SaveZone(Zone zone, ObjsCollection objects, MobsCollection mobs, RoomsCollection rooms)
        {
            try
            {
                string dbPath = GetDatabasePath();
                string dbDir = Path.GetDirectoryName(dbPath);
                if (!Directory.Exists(dbDir))
                    Directory.CreateDirectory(dbDir);

                using (var conn = CreateConnection(dbPath))
                {
                    conn.Open();
                    EnsureSchema(conn);

                    using (var transaction = conn.BeginTransaction())
                    {
                        // Delete existing zone data
                        DeleteZoneData(conn, zone.Number);

                        // Insert zone
                        InsertZone(conn, zone);
                        InsertZoneGroups(conn, zone);

                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                FireExceptionEvent($"Error saving SQLite zone {zone.Number}", ex, EventLogEntryType.Error);
            }
        }

        private void DeleteZoneData(SQLiteConnection conn, int zoneVnum)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM zones WHERE vnum = @vnum";
                cmd.Parameters.AddWithValue("@vnum", zoneVnum);
                cmd.ExecuteNonQuery();

                cmd.CommandText = "DELETE FROM zone_groups WHERE zone_vnum = @vnum";
                cmd.ExecuteNonQuery();
            }
        }

        private void InsertZone(SQLiteConnection conn, Zone zone)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    INSERT INTO zones (vnum, name, comment, location, author, description,
                        first_room, top_room, mode, zone_type, zone_group, entrance,
                        lifespan, reset_mode, reset_idle, under_construction)
                    VALUES (@vnum, @name, @comment, @location, @author, @description,
                        @first_room, @top_room, @mode, @zone_type, @zone_group, @entrance,
                        @lifespan, @reset_mode, @reset_idle, @under_construction)";
                cmd.Parameters.AddWithValue("@vnum", zone.Number);
                cmd.Parameters.AddWithValue("@name", zone.Name ?? "");
                cmd.Parameters.AddWithValue("@comment", zone.Comment ?? "");
                cmd.Parameters.AddWithValue("@location", zone.Location ?? "");
                cmd.Parameters.AddWithValue("@author", zone.Author ?? "");
                cmd.Parameters.AddWithValue("@description", zone.Description ?? "");
                cmd.Parameters.AddWithValue("@first_room", zone.Number * 100);
                cmd.Parameters.AddWithValue("@top_room", zone.LastRoomNumber);
                cmd.Parameters.AddWithValue("@mode", zone.Level);
                cmd.Parameters.AddWithValue("@zone_type", zone.Type);
                cmd.Parameters.AddWithValue("@zone_group", zone.OptimalCharsInGroup);
                cmd.Parameters.AddWithValue("@entrance", zone.Number * 100);
                cmd.Parameters.AddWithValue("@lifespan", zone.RepopTimer);
                cmd.Parameters.AddWithValue("@reset_mode", zone.RepopType);
                cmd.Parameters.AddWithValue("@reset_idle", zone.ResetIdle);
                cmd.Parameters.AddWithValue("@under_construction", zone.Test ? 1 : 0);
                cmd.ExecuteNonQuery();
            }
        }

        private void InsertZoneGroups(SQLiteConnection conn, Zone zone)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    INSERT INTO zone_groups (zone_vnum, linked_zone_vnum, group_type)
                    VALUES (@zone_vnum, @linked_zone_vnum, @group_type)";

                foreach (var z in zone.ResetA)
                {
                    if (z is int znum)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@zone_vnum", zone.Number);
                        cmd.Parameters.AddWithValue("@linked_zone_vnum", znum);
                        cmd.Parameters.AddWithValue("@group_type", "A");
                        cmd.ExecuteNonQuery();
                    }
                }

                foreach (var z in zone.ResetB)
                {
                    if (z is int znum)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@zone_vnum", zone.Number);
                        cmd.Parameters.AddWithValue("@linked_zone_vnum", znum);
                        cmd.Parameters.AddWithValue("@group_type", "B");
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public override void SaveRooms(RoomsCollection rooms, string zoneNumber)
        {
            try
            {
                string dbPath = GetDatabasePath();
                using (var conn = CreateConnection(dbPath))
                {
                    conn.Open();
                    EnsureSchema(conn);

                    using (var transaction = conn.BeginTransaction())
                    {
                        int zoneVnum = int.Parse(zoneNumber);

                        // Delete existing rooms for this zone
                        DeleteRoomsForZone(conn, zoneVnum);

                        foreach (Room room in rooms)
                        {
                            InsertRoom(conn, room, zoneVnum);
                            InsertRoomFlags(conn, room);
                            InsertRoomExits(conn, room);
                            InsertRoomExtraDescs(conn, room);
                            InsertRoomTriggers(conn, room);
                            InsertRoomIngredients(conn, room);
                            InsertRoomDescriptions(conn, room);
                        }

                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                FireExceptionEvent($"Error saving SQLite rooms for zone {zoneNumber}", ex, EventLogEntryType.Error);
            }
        }

        private void DeleteRoomsForZone(SQLiteConnection conn, int zoneVnum)
        {
            using (var cmd = conn.CreateCommand())
            {
                // Get room vnums first
                cmd.CommandText = "SELECT vnum FROM rooms WHERE zone_vnum = @zone";
                cmd.Parameters.AddWithValue("@zone", zoneVnum);

                var roomVnums = new List<int>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        roomVnums.Add(reader.GetInt32(0));
                }

                // Delete related data
                foreach (int vnum in roomVnums)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@vnum", vnum);

                    cmd.CommandText = "DELETE FROM room_flags WHERE room_vnum = @vnum";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "DELETE FROM room_exits WHERE room_vnum = @vnum";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "DELETE FROM room_ingredients WHERE room_vnum = @vnum";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "DELETE FROM room_descriptions WHERE room_vnum = @vnum";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "DELETE FROM extra_descriptions WHERE entity_type = 'room' AND entity_vnum = @vnum";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "DELETE FROM entity_triggers WHERE entity_type = 'room' AND entity_vnum = @vnum";
                    cmd.ExecuteNonQuery();
                }

                // Delete rooms
                cmd.CommandText = "DELETE FROM rooms WHERE zone_vnum = @zone";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@zone", zoneVnum);
                cmd.ExecuteNonQuery();
            }
        }

        private void InsertRoom(SQLiteConnection conn, Room room, int zoneVnum)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    INSERT INTO rooms (vnum, zone_vnum, name, description, sector_id, x, y, z, placed_on_map)
                    VALUES (@vnum, @zone_vnum, @name, @description, @sector_id, @x, @y, @z, @placed_on_map)";
                cmd.Parameters.AddWithValue("@vnum", room.VNum);
                cmd.Parameters.AddWithValue("@zone_vnum", zoneVnum);
                cmd.Parameters.AddWithValue("@name", room.Name ?? "");
                cmd.Parameters.AddWithValue("@description", room.Description.Main ?? "");
                cmd.Parameters.AddWithValue("@sector_id", room.SectorType);
                cmd.Parameters.AddWithValue("@x", room.X);
                cmd.Parameters.AddWithValue("@y", room.Y);
                cmd.Parameters.AddWithValue("@z", room.Z);
                cmd.Parameters.AddWithValue("@placed_on_map", room.PlacedOnMap ? 1 : 0);
                cmd.ExecuteNonQuery();
            }
        }

        private void InsertRoomFlags(SQLiteConnection conn, Room room)
        {
            if (string.IsNullOrEmpty(room.Flags)) return;

            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO room_flags (room_vnum, flag_name) VALUES (@vnum, @flag)";
                foreach (var flag in room.Flags.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@vnum", room.VNum);
                    cmd.Parameters.AddWithValue("@flag", flag);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void InsertRoomExits(SQLiteConnection conn, Room room)
        {
            InsertExit(conn, room.VNum, 0, room.ExitNorth);
            InsertExit(conn, room.VNum, 1, room.ExitEast);
            InsertExit(conn, room.VNum, 2, room.ExitSouth);
            InsertExit(conn, room.VNum, 3, room.ExitWest);
            InsertExit(conn, room.VNum, 4, room.ExitUp);
            InsertExit(conn, room.VNum, 5, room.ExitDown);
        }

        private void InsertExit(SQLiteConnection conn, int roomVnum, int direction, Exit exit)
        {
            if (exit == null || exit.RoomVNum < 0) return;

            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    INSERT INTO room_exits (room_vnum, direction, description, keywords, exit_flags, key_vnum, to_room, lock_complexity)
                    VALUES (@room_vnum, @direction, @description, @keywords, @exit_flags, @key_vnum, @to_room, @lock_complexity)";
                cmd.Parameters.AddWithValue("@room_vnum", roomVnum);
                cmd.Parameters.AddWithValue("@direction", direction);
                cmd.Parameters.AddWithValue("@description", exit.Description ?? "");
                cmd.Parameters.AddWithValue("@keywords", exit.Aliases ?? "");
                cmd.Parameters.AddWithValue("@exit_flags", exit.ExitFlag);
                cmd.Parameters.AddWithValue("@key_vnum", exit.Key);
                cmd.Parameters.AddWithValue("@to_room", exit.RoomVNum);
                cmd.Parameters.AddWithValue("@lock_complexity", exit.LockLevel);
                cmd.ExecuteNonQuery();
            }
        }

        private void InsertRoomExtraDescs(SQLiteConnection conn, Room room)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    INSERT INTO extra_descriptions (entity_type, entity_vnum, keywords, description)
                    VALUES ('room', @vnum, @keywords, @description)";
                foreach (ExtraDesc ed in room.ExtraDescriptions)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@vnum", room.VNum);
                    cmd.Parameters.AddWithValue("@keywords", ed.Aliases ?? "");
                    cmd.Parameters.AddWithValue("@description", ed.Description ?? "");
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void InsertRoomTriggers(SQLiteConnection conn, Room room)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    INSERT INTO entity_triggers (entity_type, entity_vnum, trigger_vnum)
                    VALUES ('room', @vnum, @trigger_vnum)";
                foreach (var trig in room.TriggersList)
                {
                    if (trig is int trigVnum)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@vnum", room.VNum);
                        cmd.Parameters.AddWithValue("@trigger_vnum", trigVnum);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private void InsertRoomIngredients(SQLiteConnection conn, Room room)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    INSERT INTO room_ingredients (room_vnum, type_name, power, probability, power_auto)
                    VALUES (@vnum, @type_name, @power, @probability, @power_auto)";
                foreach (Ingredient ingr in room.Ingredients)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@vnum", room.VNum);
                    cmd.Parameters.AddWithValue("@type_name", ingr.TypeName);
                    cmd.Parameters.AddWithValue("@power", ingr.Power);
                    cmd.Parameters.AddWithValue("@probability", ingr.Probability);
                    cmd.Parameters.AddWithValue("@power_auto", ingr.PowerAuto ? 1 : 0);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void InsertRoomDescriptions(SQLiteConnection conn, Room room)
        {
            if (string.IsNullOrEmpty(room.Description.Day) && string.IsNullOrEmpty(room.Description.Night))
                return;

            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    INSERT INTO room_descriptions (room_vnum, main, day, night)
                    VALUES (@vnum, @main, @day, @night)";
                cmd.Parameters.AddWithValue("@vnum", room.VNum);
                cmd.Parameters.AddWithValue("@main", room.Description.Main ?? "");
                cmd.Parameters.AddWithValue("@day", room.Description.Day ?? "");
                cmd.Parameters.AddWithValue("@night", room.Description.Night ?? "");
                cmd.ExecuteNonQuery();
            }
        }

        public override void SaveMobs(MobsCollection mobs, string zoneNumber)
        {
            try
            {
                string dbPath = GetDatabasePath();
                using (var conn = CreateConnection(dbPath))
                {
                    conn.Open();
                    EnsureSchema(conn);

                    using (var transaction = conn.BeginTransaction())
                    {
                        foreach (Mob mob in mobs)
                        {
                            DeleteMobData(conn, mob.VNum);
                            InsertMob(conn, mob);
                            InsertMobFlags(conn, mob);
                            InsertMobSkills(conn, mob);
                            InsertMobSpells(conn, mob);
                            InsertMobFeats(conn, mob);
                            InsertMobHelpers(conn, mob);
                            InsertMobDestinations(conn, mob);
                            InsertMobIngredients(conn, mob);
                            InsertMobLoadedObjects(conn, mob);
                            InsertMobTriggers(conn, mob);
                        }
                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                FireExceptionEvent($"Error saving SQLite mobs for zone {zoneNumber}", ex, EventLogEntryType.Error);
            }
        }

        private void DeleteMobData(SQLiteConnection conn, int vnum)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.Parameters.AddWithValue("@vnum", vnum);
                cmd.CommandText = "DELETE FROM mobs WHERE vnum = @vnum";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "DELETE FROM mob_flags WHERE mob_vnum = @vnum";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "DELETE FROM mob_skills WHERE mob_vnum = @vnum";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "DELETE FROM mob_spells WHERE mob_vnum = @vnum";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "DELETE FROM mob_feats WHERE mob_vnum = @vnum";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "DELETE FROM mob_helpers WHERE mob_vnum = @vnum";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "DELETE FROM mob_destinations WHERE mob_vnum = @vnum";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "DELETE FROM mob_ingredients WHERE mob_vnum = @vnum";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "DELETE FROM mob_loaded_objects WHERE mob_vnum = @vnum";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "DELETE FROM entity_triggers WHERE entity_type = 'mob' AND entity_vnum = @vnum";
                cmd.ExecuteNonQuery();
            }
        }

        private void InsertMob(SQLiteConnection conn, Mob mob)
        {
            var dice = YamlModels.YamlDice.Parse(mob.Hits);
            var damDice = YamlModels.YamlDice.Parse(mob.Damage);
            var goldDice = YamlModels.YamlDice.Parse(mob.Money);

            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    INSERT INTO mobs (
                        vnum, aliases, name_nom, name_gen, name_dat, name_acc, name_ins, name_pre,
                        short_desc, long_desc, alignment, mob_type, level, hitroll_penalty, armor,
                        hp_dice_count, hp_dice_size, hp_bonus, dam_dice_count, dam_dice_size, dam_bonus,
                        gold_dice_count, gold_dice_size, gold_bonus, experience, default_pos, start_pos,
                        sex, size, height, weight, mob_class, race, max_in_world,
                        attr_str, attr_dex, attr_int, attr_wis, attr_con, attr_cha,
                        hp_regen, armour_bonus, mana_regen, cast_success, morale, initiative_add,
                        absorb, aresist, mresist, presist, bare_hand_attack, like_work, max_factor,
                        extra_attack, special_bitvector,
                        resist_fire, resist_air, resist_water, resist_earth, resist_dark,
                        resist_vitality, resist_mind, resist_immunity,
                        save_paralyze, save_breath, save_magic, save_skills
                    ) VALUES (
                        @vnum, @aliases, @name_nom, @name_gen, @name_dat, @name_acc, @name_ins, @name_pre,
                        @short_desc, @long_desc, @alignment, @mob_type, @level, @hitroll_penalty, @armor,
                        @hp_dice_count, @hp_dice_size, @hp_bonus, @dam_dice_count, @dam_dice_size, @dam_bonus,
                        @gold_dice_count, @gold_dice_size, @gold_bonus, @experience, @default_pos, @start_pos,
                        @sex, @size, @height, @weight, @mob_class, @race, @max_in_world,
                        @attr_str, @attr_dex, @attr_int, @attr_wis, @attr_con, @attr_cha,
                        @hp_regen, @armour_bonus, @mana_regen, @cast_success, @morale, @initiative_add,
                        @absorb, @aresist, @mresist, @presist, @bare_hand_attack, @like_work, @max_factor,
                        @extra_attack, @special_bitvector,
                        @resist_fire, @resist_air, @resist_water, @resist_earth, @resist_dark,
                        @resist_vitality, @resist_mind, @resist_immunity,
                        @save_paralyze, @save_breath, @save_magic, @save_skills
                    )";
                cmd.Parameters.AddWithValue("@vnum", mob.VNum);
                cmd.Parameters.AddWithValue("@aliases", mob.Alias ?? "");
                cmd.Parameters.AddWithValue("@name_nom", mob.Cases.Imen ?? "");
                cmd.Parameters.AddWithValue("@name_gen", mob.Cases.Rod ?? "");
                cmd.Parameters.AddWithValue("@name_dat", mob.Cases.Dat ?? "");
                cmd.Parameters.AddWithValue("@name_acc", mob.Cases.Vin ?? "");
                cmd.Parameters.AddWithValue("@name_ins", mob.Cases.Tvor ?? "");
                cmd.Parameters.AddWithValue("@name_pre", mob.Cases.Pred ?? "");
                cmd.Parameters.AddWithValue("@short_desc", mob.Desc ?? "");
                cmd.Parameters.AddWithValue("@long_desc", mob.DetailDescr ?? "");
                cmd.Parameters.AddWithValue("@alignment", mob.Align);
                cmd.Parameters.AddWithValue("@mob_type", "E");
                cmd.Parameters.AddWithValue("@level", mob.Level);
                cmd.Parameters.AddWithValue("@hitroll_penalty", mob.Hitroll);
                cmd.Parameters.AddWithValue("@armor", mob.Ac);
                cmd.Parameters.AddWithValue("@hp_dice_count", dice.DiceCount);
                cmd.Parameters.AddWithValue("@hp_dice_size", dice.DiceSize);
                cmd.Parameters.AddWithValue("@hp_bonus", dice.Bonus);
                cmd.Parameters.AddWithValue("@dam_dice_count", damDice.DiceCount);
                cmd.Parameters.AddWithValue("@dam_dice_size", damDice.DiceSize);
                cmd.Parameters.AddWithValue("@dam_bonus", damDice.Bonus);
                cmd.Parameters.AddWithValue("@gold_dice_count", goldDice.DiceCount);
                cmd.Parameters.AddWithValue("@gold_dice_size", goldDice.DiceSize);
                cmd.Parameters.AddWithValue("@gold_bonus", goldDice.Bonus);
                cmd.Parameters.AddWithValue("@experience", mob.Exp);
                cmd.Parameters.AddWithValue("@default_pos", mob.PosDefault);
                cmd.Parameters.AddWithValue("@start_pos", mob.PosLoad);
                cmd.Parameters.AddWithValue("@sex", mob.Sex);
                cmd.Parameters.AddWithValue("@size", mob.Stats.Size);
                cmd.Parameters.AddWithValue("@height", mob.Stats.Height);
                cmd.Parameters.AddWithValue("@weight", mob.Stats.Weight);
                cmd.Parameters.AddWithValue("@mob_class", mob.Class);
                cmd.Parameters.AddWithValue("@race", mob.Race);
                cmd.Parameters.AddWithValue("@max_in_world", mob.MaxInWorld);
                cmd.Parameters.AddWithValue("@attr_str", mob.Stats.Str);
                cmd.Parameters.AddWithValue("@attr_dex", mob.Stats.Dex);
                cmd.Parameters.AddWithValue("@attr_int", mob.Stats.Int);
                cmd.Parameters.AddWithValue("@attr_wis", mob.Stats.Wis);
                cmd.Parameters.AddWithValue("@attr_con", mob.Stats.Con);
                cmd.Parameters.AddWithValue("@attr_cha", mob.Stats.Cha);
                cmd.Parameters.AddWithValue("@hp_regen", mob.HPreg);
                cmd.Parameters.AddWithValue("@armour_bonus", mob.Armour);
                cmd.Parameters.AddWithValue("@mana_regen", mob.PlusMem);
                cmd.Parameters.AddWithValue("@cast_success", mob.CastSuccess);
                cmd.Parameters.AddWithValue("@morale", mob.Luck);
                cmd.Parameters.AddWithValue("@initiative_add", mob.Initiative);
                cmd.Parameters.AddWithValue("@absorb", mob.Absorbe);
                cmd.Parameters.AddWithValue("@aresist", mob.AResist);
                cmd.Parameters.AddWithValue("@mresist", mob.MResist);
                cmd.Parameters.AddWithValue("@presist", mob.PResist);
                cmd.Parameters.AddWithValue("@bare_hand_attack", mob.BareHandAttack);
                cmd.Parameters.AddWithValue("@like_work", mob.LikeWork);
                cmd.Parameters.AddWithValue("@max_factor", mob.MaxFactor);
                cmd.Parameters.AddWithValue("@extra_attack", mob.ExtraAttack);
                cmd.Parameters.AddWithValue("@special_bitvector", mob.SpecialBitvector ?? "");
                cmd.Parameters.AddWithValue("@resist_fire", mob.ResistFromFire);
                cmd.Parameters.AddWithValue("@resist_air", mob.ResistFromAir);
                cmd.Parameters.AddWithValue("@resist_water", mob.ResistFromWater);
                cmd.Parameters.AddWithValue("@resist_earth", mob.ResistFromEarth);
                cmd.Parameters.AddWithValue("@resist_dark", mob.ResistDark);
                cmd.Parameters.AddWithValue("@resist_vitality", mob.Vitality);
                cmd.Parameters.AddWithValue("@resist_mind", mob.Mind);
                cmd.Parameters.AddWithValue("@resist_immunity", mob.Immunitet);
                cmd.Parameters.AddWithValue("@save_paralyze", mob.SaveParalyzeCast);
                cmd.Parameters.AddWithValue("@save_breath", mob.SaveMagBreathes);
                cmd.Parameters.AddWithValue("@save_magic", mob.SaveMagDamages);
                cmd.Parameters.AddWithValue("@save_skills", mob.SaveFightSkills);
                cmd.ExecuteNonQuery();
            }
        }

        private void InsertMobFlags(SQLiteConnection conn, Mob mob)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    INSERT INTO mob_flags (mob_vnum, flag_category, flag_name)
                    VALUES (@vnum, @category, @flag)";

                if (!string.IsNullOrEmpty(mob.Flags))
                {
                    foreach (var flag in mob.Flags.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@vnum", mob.VNum);
                        cmd.Parameters.AddWithValue("@category", "action");
                        cmd.Parameters.AddWithValue("@flag", flag);
                        cmd.ExecuteNonQuery();
                    }
                }

                if (!string.IsNullOrEmpty(mob.Affects))
                {
                    foreach (var flag in mob.Affects.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@vnum", mob.VNum);
                        cmd.Parameters.AddWithValue("@category", "affect");
                        cmd.Parameters.AddWithValue("@flag", flag);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private void InsertMobSkills(SQLiteConnection conn, Mob mob)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO mob_skills (mob_vnum, skill_id, value) VALUES (@vnum, @skill_id, @value)";
                foreach (MobSkill skill in mob.Skills)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@vnum", mob.VNum);
                    cmd.Parameters.AddWithValue("@skill_id", skill.VNum);
                    cmd.Parameters.AddWithValue("@value", skill.Percent);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void InsertMobSpells(SQLiteConnection conn, Mob mob)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO mob_spells (mob_vnum, spell_id, count) VALUES (@vnum, @spell_id, @count)";
                foreach (MobSpell spell in mob.Spells)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@vnum", mob.VNum);
                    cmd.Parameters.AddWithValue("@spell_id", spell.VNum);
                    cmd.Parameters.AddWithValue("@count", spell.Count);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void InsertMobFeats(SQLiteConnection conn, Mob mob)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO mob_feats (mob_vnum, feat_id) VALUES (@vnum, @feat_id)";
                foreach (var feat in mob.Feats)
                {
                    if (feat is int f)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@vnum", mob.VNum);
                        cmd.Parameters.AddWithValue("@feat_id", f);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private void InsertMobHelpers(SQLiteConnection conn, Mob mob)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO mob_helpers (mob_vnum, helper_vnum) VALUES (@vnum, @helper_vnum)";
                foreach (var helper in mob.Helpers)
                {
                    if (helper is int h)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@vnum", mob.VNum);
                        cmd.Parameters.AddWithValue("@helper_vnum", h);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private void InsertMobDestinations(SQLiteConnection conn, Mob mob)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO mob_destinations (mob_vnum, destination_vnum) VALUES (@vnum, @destination_vnum)";
                foreach (var dest in mob.Destination)
                {
                    if (dest is int d)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@vnum", mob.VNum);
                        cmd.Parameters.AddWithValue("@destination_vnum", d);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private void InsertMobIngredients(SQLiteConnection conn, Mob mob)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    INSERT INTO mob_ingredients (mob_vnum, type_name, power, probability, power_auto)
                    VALUES (@vnum, @type_name, @power, @probability, @power_auto)";
                foreach (Ingredient ingr in mob.Ingredients)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@vnum", mob.VNum);
                    cmd.Parameters.AddWithValue("@type_name", ingr.TypeName);
                    cmd.Parameters.AddWithValue("@power", ingr.Power);
                    cmd.Parameters.AddWithValue("@probability", ingr.Probability);
                    cmd.Parameters.AddWithValue("@power_auto", ingr.PowerAuto ? 1 : 0);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void InsertMobLoadedObjects(SQLiteConnection conn, Mob mob)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    INSERT INTO mob_loaded_objects (mob_vnum, obj_vnum, probability)
                    VALUES (@vnum, @obj_vnum, @probability)";
                foreach (LoadedObjAfterDeath obj in mob.LoadedObjectAfterDeath)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@vnum", mob.VNum);
                    cmd.Parameters.AddWithValue("@obj_vnum", obj.VNum);
                    cmd.Parameters.AddWithValue("@probability", obj.LoadProb);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void InsertMobTriggers(SQLiteConnection conn, Mob mob)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    INSERT INTO entity_triggers (entity_type, entity_vnum, trigger_vnum)
                    VALUES ('mob', @vnum, @trigger_vnum)";
                foreach (var trig in mob.TriggersList)
                {
                    if (trig is int trigVnum)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@vnum", mob.VNum);
                        cmd.Parameters.AddWithValue("@trigger_vnum", trigVnum);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public override void SaveObjects(ObjsCollection objects, string zoneNumber)
        {
            try
            {
                string dbPath = GetDatabasePath();
                using (var conn = CreateConnection(dbPath))
                {
                    conn.Open();
                    EnsureSchema(conn);

                    using (var transaction = conn.BeginTransaction())
                    {
                        foreach (Obj obj in objects)
                        {
                            DeleteObjData(conn, obj.VNum);
                            InsertObj(conn, obj);
                            InsertObjFlags(conn, obj);
                            InsertObjApplies(conn, obj);
                            InsertObjExtraDescs(conn, obj);
                            InsertObjTriggers(conn, obj);
                        }
                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                FireExceptionEvent($"Error saving SQLite objects for zone {zoneNumber}", ex, EventLogEntryType.Error);
            }
        }

        private void DeleteObjData(SQLiteConnection conn, int vnum)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.Parameters.AddWithValue("@vnum", vnum);
                cmd.CommandText = "DELETE FROM objects WHERE vnum = @vnum";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "DELETE FROM obj_flags WHERE obj_vnum = @vnum";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "DELETE FROM obj_applies WHERE obj_vnum = @vnum";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "DELETE FROM extra_descriptions WHERE entity_type = 'obj' AND entity_vnum = @vnum";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "DELETE FROM entity_triggers WHERE entity_type = 'obj' AND entity_vnum = @vnum";
                cmd.ExecuteNonQuery();
            }
        }

        private void InsertObj(SQLiteConnection conn, Obj obj)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    INSERT INTO objects (
                        vnum, aliases, name_nom, name_gen, name_dat, name_acc, name_ins, name_pre,
                        short_desc, action_desc, obj_type, material,
                        value0, value1, value2, value3,
                        weight, cost, rent_off, rent_on, spec_param,
                        max_durability, cur_durability, timer, spell, level, sex, max_in_world, min_remorts
                    ) VALUES (
                        @vnum, @aliases, @name_nom, @name_gen, @name_dat, @name_acc, @name_ins, @name_pre,
                        @short_desc, @action_desc, @obj_type, @material,
                        @value0, @value1, @value2, @value3,
                        @weight, @cost, @rent_off, @rent_on, @spec_param,
                        @max_durability, @cur_durability, @timer, @spell, @level, @sex, @max_in_world, @min_remorts
                    )";
                cmd.Parameters.AddWithValue("@vnum", obj.VNum);
                cmd.Parameters.AddWithValue("@aliases", obj.Alias ?? "");
                cmd.Parameters.AddWithValue("@name_nom", obj.Cases.Imen ?? "");
                cmd.Parameters.AddWithValue("@name_gen", obj.Cases.Rod ?? "");
                cmd.Parameters.AddWithValue("@name_dat", obj.Cases.Dat ?? "");
                cmd.Parameters.AddWithValue("@name_acc", obj.Cases.Vin ?? "");
                cmd.Parameters.AddWithValue("@name_ins", obj.Cases.Tvor ?? "");
                cmd.Parameters.AddWithValue("@name_pre", obj.Cases.Pred ?? "");
                cmd.Parameters.AddWithValue("@short_desc", obj.Desc ?? "");
                cmd.Parameters.AddWithValue("@action_desc", obj.ActionDesc ?? "");
                cmd.Parameters.AddWithValue("@obj_type", obj.Type);
                cmd.Parameters.AddWithValue("@material", obj.Material);
                cmd.Parameters.AddWithValue("@value0", obj.Param1 ?? "");
                cmd.Parameters.AddWithValue("@value1", obj.Param2 ?? "");
                cmd.Parameters.AddWithValue("@value2", obj.Param3 ?? "");
                cmd.Parameters.AddWithValue("@value3", obj.Param4 ?? "");
                cmd.Parameters.AddWithValue("@weight", obj.Weight);
                cmd.Parameters.AddWithValue("@cost", obj.Price);
                cmd.Parameters.AddWithValue("@rent_off", obj.RentInv);
                cmd.Parameters.AddWithValue("@rent_on", obj.RentWear);
                cmd.Parameters.AddWithValue("@spec_param", 0);
                cmd.Parameters.AddWithValue("@max_durability", obj.MaxDurab);
                cmd.Parameters.AddWithValue("@cur_durability", obj.CurrDurab);
                cmd.Parameters.AddWithValue("@timer", obj.Timer);
                cmd.Parameters.AddWithValue("@spell", obj.Spell);
                cmd.Parameters.AddWithValue("@level", obj.SpellLevel);
                cmd.Parameters.AddWithValue("@sex", obj.Sex);
                cmd.Parameters.AddWithValue("@max_in_world", obj.MaxInWorld);
                cmd.Parameters.AddWithValue("@min_remorts", obj.MinimumRemorts);
                cmd.ExecuteNonQuery();
            }
        }

        private void InsertObjFlags(SQLiteConnection conn, Obj obj)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    INSERT INTO obj_flags (obj_vnum, flag_category, flag_name)
                    VALUES (@vnum, @category, @flag)";

                InsertFlagsForCategory(cmd, obj.VNum, "extra", obj.Affects);
                InsertFlagsForCategory(cmd, obj.VNum, "wear", obj.WearFlags);
                InsertFlagsForCategory(cmd, obj.VNum, "no", obj.CantTouch);
                InsertFlagsForCategory(cmd, obj.VNum, "anti", obj.CantUse);
                InsertFlagsForCategory(cmd, obj.VNum, "affect", obj.MagicFlags);
            }
        }

        private void InsertFlagsForCategory(SQLiteCommand cmd, int vnum, string category, string flagsString)
        {
            if (string.IsNullOrEmpty(flagsString)) return;

            foreach (var flag in flagsString.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@vnum", vnum);
                cmd.Parameters.AddWithValue("@category", category);
                cmd.Parameters.AddWithValue("@flag", flag);
                cmd.ExecuteNonQuery();
            }
        }

        private void InsertObjApplies(SQLiteConnection conn, Obj obj)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO obj_applies (obj_vnum, location, modifier) VALUES (@vnum, @location, @modifier)";
                foreach (Bonus bonus in obj.BonusesCollection)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@vnum", obj.VNum);
                    cmd.Parameters.AddWithValue("@location", bonus.VNum);
                    cmd.Parameters.AddWithValue("@modifier", bonus.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void InsertObjExtraDescs(SQLiteConnection conn, Obj obj)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    INSERT INTO extra_descriptions (entity_type, entity_vnum, keywords, description)
                    VALUES ('obj', @vnum, @keywords, @description)";
                foreach (ExtraDesc ed in obj.ExtraDescriptions)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@vnum", obj.VNum);
                    cmd.Parameters.AddWithValue("@keywords", ed.Aliases ?? "");
                    cmd.Parameters.AddWithValue("@description", ed.Description ?? "");
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void InsertObjTriggers(SQLiteConnection conn, Obj obj)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    INSERT INTO entity_triggers (entity_type, entity_vnum, trigger_vnum)
                    VALUES ('obj', @vnum, @trigger_vnum)";
                foreach (var trig in obj.TriggersList)
                {
                    if (trig is int trigVnum)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@vnum", obj.VNum);
                        cmd.Parameters.AddWithValue("@trigger_vnum", trigVnum);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public override void SaveTriggers(TriggersCollection triggers, string zoneNumber)
        {
            try
            {
                string dbPath = GetDatabasePath();
                using (var conn = CreateConnection(dbPath))
                {
                    conn.Open();
                    EnsureSchema(conn);

                    using (var transaction = conn.BeginTransaction())
                    {
                        foreach (Trigger trigger in triggers)
                        {
                            DeleteTriggerData(conn, trigger.VNum);
                            InsertTrigger(conn, trigger);
                            InsertTriggerTypes(conn, trigger);
                        }
                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                FireExceptionEvent($"Error saving SQLite triggers for zone {zoneNumber}", ex, EventLogEntryType.Error);
            }
        }

        private void DeleteTriggerData(SQLiteConnection conn, int vnum)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.Parameters.AddWithValue("@vnum", vnum);
                cmd.CommandText = "DELETE FROM triggers WHERE vnum = @vnum";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "DELETE FROM trigger_types WHERE trigger_vnum = @vnum";
                cmd.ExecuteNonQuery();
            }
        }

        private void InsertTrigger(SQLiteConnection conn, Trigger trigger)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    INSERT INTO triggers (vnum, name, attach_type, narg, arglist, script)
                    VALUES (@vnum, @name, @attach_type, @narg, @arglist, @script)";
                cmd.Parameters.AddWithValue("@vnum", trigger.VNum);
                cmd.Parameters.AddWithValue("@name", trigger.Name ?? "");
                cmd.Parameters.AddWithValue("@attach_type", trigger.Class);
                cmd.Parameters.AddWithValue("@narg", trigger.NumArg);
                cmd.Parameters.AddWithValue("@arglist", trigger.Arg ?? "");
                cmd.Parameters.AddWithValue("@script", trigger.Body ?? "");
                cmd.ExecuteNonQuery();
            }
        }

        private void InsertTriggerTypes(SQLiteConnection conn, Trigger trigger)
        {
            if (string.IsNullOrEmpty(trigger.Type)) return;

            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO trigger_types (trigger_vnum, type_name) VALUES (@vnum, @type_name)";
                foreach (var typeName in trigger.Type.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@vnum", trigger.VNum);
                    cmd.Parameters.AddWithValue("@type_name", typeName);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public override void SaveSketches(SketchRoomsCollection sketches, string zoneNumber)
        {
            // Sketches are stored as room coordinates in SQLite
        }

        #endregion

        #region Helper Methods

        public override bool CanLoadZone(string zoneNumber)
        {
            string dbPath = GetDatabasePath();
            if (!File.Exists(dbPath))
                return false;

            try
            {
                using (var conn = CreateConnection(dbPath))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT COUNT(*) FROM zones WHERE vnum = @vnum";
                        cmd.Parameters.AddWithValue("@vnum", int.Parse(zoneNumber));
                        return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        private string GetStringOrEmpty(SQLiteDataReader reader, string columnName)
        {
            int ordinal = reader.GetOrdinal(columnName);
            return reader.IsDBNull(ordinal) ? "" : reader.GetString(ordinal);
        }

        private string FormatDice(int count, int size, int bonus)
        {
            if (bonus >= 0)
                return $"{count}d{size}+{bonus}";
            return $"{count}d{size}{bonus}";
        }

        #endregion
    }
}
