using System.Collections.Generic;
using System.Globalization;

namespace DataUtils.YamlMappers
{
    /// <summary>
    /// Trigger types (the trigger_types list) between the editor's single-plane letter flags and
    /// the engine's names. Bits are reused with a different meaning per attach type (bit 4 is Act
    /// for a mob, Fighting round for an object, Enter PC for a room), so the name depends on the
    /// trigger's attach type, not on the bit alone.
    /// </summary>
    public static class TriggerTypeCodec
    {
        // Indexed by attach type (kMobTrigger = 0, kObjTrigger = 1, kRoomTrigger = 2).
        private static readonly string[] Prefixes = { "kMob", "kObj", "kWld" };
        private static readonly string[] AttachNames = { "mob", "object", "room" };

        /// <summary>
        /// Attach type a current dictionary name belongs to, or -1 for a name shared by all types.
        /// </summary>
        public static int OwnerOf(string name)
        {
            for (int i = 0; i < Prefixes.Length; i++)
                if (name.StartsWith(Prefixes[i], System.StringComparison.Ordinal))
                    return i;
            return -1;
        }

        /// <summary>
        /// Name to write for a bit: the lexicographically smallest name with the trigger's own
        /// prefix, else the smallest unprefixed one, else the bit number. Legacy names are never
        /// candidates.
        /// </summary>
        public static string NameFor(int bit, int attachType)
        {
            string own = null, common = null;
            foreach (var kv in EngineDictionaries.TriggerTypes)
            {
                if (kv.Value != bit) continue;
                int owner = OwnerOf(kv.Key);
                if (owner < 0)
                    common = Min(common, kv.Key);
                else if (owner == attachType)
                    own = Min(own, kv.Key);
            }
            return own ?? common ?? bit.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>Letter flags -> names for a trigger of the given attach type (ascending by bit).</summary>
        public static List<string> ToNames(string letterFlags, int attachType)
        {
            var bits = EngineCodec.DecodeLetterFlags(letterFlags);
            bits.Sort();
            var names = new List<string>();
            foreach (int bit in bits)
                names.Add(NameFor(bit, attachType));
            return names;
        }

        /// <summary>
        /// Names -> letter flags. Accepts current names, legacy unprefixed names and plain bit
        /// numbers (the engine writes the number when a bit has no name). A name carrying another
        /// attach type's prefix still sets its bit, but is reported to <paramref name="problems"/>
        /// as a builder error, as is anything unrecognised (which is dropped).
        /// </summary>
        public static string FromNames(IEnumerable<string> names, int attachType, ICollection<string> problems = null)
        {
            var bits = new List<int>();
            if (names == null) return EngineCodec.EncodeLetterFlags(bits);
            foreach (string raw in names)
            {
                if (raw == null) continue;
                string name = raw.Trim();
                if (name.Length == 0) continue;

                int bit;
                if (EngineDictionaries.TriggerTypes.TryGetValue(name, out bit))
                {
                    int owner = OwnerOf(name);
                    if (owner >= 0 && owner != attachType)
                        problems?.Add($"trigger type '{name}' belongs to {AttachNames[owner]} triggers, "
                            + $"not {AttachName(attachType)} triggers (bit {bit} is read as '{NameFor(bit, attachType)}')");
                }
                else if (!EngineDictionaries.LegacyTriggerTypes.TryGetValue(name, out bit)
                         && !(int.TryParse(name, NumberStyles.None, CultureInfo.InvariantCulture, out bit) && bit < 52))
                {
                    problems?.Add($"unknown trigger type '{name}' is dropped");
                    continue;
                }
                if (!bits.Contains(bit)) bits.Add(bit);
            }
            return EngineCodec.EncodeLetterFlags(bits);
        }

        private static string AttachName(int attachType) =>
            attachType >= 0 && attachType < AttachNames.Length
                ? AttachNames[attachType]
                : "attach type " + attachType.ToString(CultureInfo.InvariantCulture);

        private static string Min(string current, string candidate) =>
            current == null || string.CompareOrdinal(candidate, current) < 0 ? candidate : current;
    }
}
