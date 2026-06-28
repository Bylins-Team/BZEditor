using System.Collections.Generic;
using System.Text;

namespace DataUtils.YamlMappers
{
    /// <summary>
    /// Converts between the editor's internal flag/enum representations and the
    /// engine's symbolic dictionary names (kXxx) used in the YAML world format.
    ///
    /// Flags are stored in legacy files as an "asciiflag" string: a sequence of
    /// (letter, plane-digit) pairs, where each pair encodes one set bit as
    ///   bit = letterValue + 30 * plane
    /// with letterValue: 'a'..'z' -> 0..25, 'A'..'Z' -> 26..51 (each plane holds 30 bits).
    /// E.g. "b0d0" -> bits {1, 3}; "C0n1" -> bits {28, 43}.
    /// </summary>
    public static class EngineCodec
    {
        private static readonly Dictionary<Dictionary<string, int>, Dictionary<int, string>> ReverseCache =
            new Dictionary<Dictionary<string, int>, Dictionary<int, string>>();

        private static Dictionary<int, string> Reverse(Dictionary<string, int> map)
        {
            Dictionary<int, string> rev;
            lock (ReverseCache)
            {
                if (!ReverseCache.TryGetValue(map, out rev))
                {
                    rev = new Dictionary<int, string>();
                    foreach (var kv in map)
                        if (!rev.ContainsKey(kv.Value))
                            rev[kv.Value] = kv.Key; // first name wins
                    ReverseCache[map] = rev;
                }
            }
            return rev;
        }

        private static int LetterValue(char c)
        {
            if (c >= 'a' && c <= 'z') return c - 'a';
            if (c >= 'A' && c <= 'Z') return 26 + (c - 'A');
            return -1;
        }

        /// <summary>Decode an asciiflag string into the set bit indices.</summary>
        public static List<int> DecodeAsciiFlags(string ascii)
        {
            var bits = new List<int>();
            if (string.IsNullOrEmpty(ascii)) return bits;
            for (int i = 0; i + 1 < ascii.Length; i += 2)
            {
                int lv = LetterValue(ascii[i]);
                char p = ascii[i + 1];
                if (lv < 0 || p < '0' || p > '9') continue;
                int bit = lv + 30 * (p - '0');
                if (!bits.Contains(bit)) bits.Add(bit);
            }
            return bits;
        }

        /// <summary>Encode bit indices back into an asciiflag string (ascending).</summary>
        public static string EncodeAsciiFlags(IEnumerable<int> bits)
        {
            var list = new List<int>(bits);
            list.Sort();
            var sb = new StringBuilder();
            foreach (int bit in list)
            {
                if (bit < 0) continue;
                int lv = bit % 30;
                int plane = bit / 30;
                char letter = lv < 26 ? (char)('a' + lv) : (char)('A' + (lv - 26));
                sb.Append(letter);
                sb.Append((char)('0' + plane));
            }
            return sb.ToString();
        }

        /// <summary>
        /// Decode single-plane letter flags (no plane digits, used by trigger types):
        /// each letter is one bit (a-z -> 0-25, A-Z -> 26-51). A pure-integer string is
        /// treated as a numeric bitvector instead.
        /// </summary>
        public static List<int> DecodeLetterFlags(string s)
        {
            var bits = new List<int>();
            if (string.IsNullOrEmpty(s)) return bits;
            int num;
            if (int.TryParse(s, out num))
            {
                for (int i = 0; i < 31; i++)
                    if ((num & (1 << i)) != 0) bits.Add(i);
                return bits;
            }
            foreach (char c in s)
            {
                int v = LetterValue(c);
                if (v >= 0 && !bits.Contains(v)) bits.Add(v);
            }
            return bits;
        }

        /// <summary>Encode bit indices into single-plane letter flags (ascending).</summary>
        public static string EncodeLetterFlags(IEnumerable<int> bits)
        {
            var list = new List<int>(bits);
            list.Sort();
            var sb = new StringBuilder();
            foreach (int b in list)
            {
                if (b < 0) continue;
                if (b < 26) sb.Append((char)('a' + b));
                else if (b < 52) sb.Append((char)('A' + (b - 26)));
            }
            return sb.ToString();
        }

        public static List<string> LetterFlagsToNames(string s, Dictionary<string, int> nameToBit)
        {
            var rev = Reverse(nameToBit);
            var names = new List<string>();
            foreach (int bit in DecodeLetterFlags(s))
            {
                string name;
                if (rev.TryGetValue(bit, out name)) names.Add(name);
            }
            return names;
        }

        public static string NamesToLetterFlags(IEnumerable<string> names, Dictionary<string, int> nameToBit)
        {
            var bits = new List<int>();
            if (names != null)
            {
                foreach (string name in names)
                {
                    int bit;
                    if (name != null && nameToBit.TryGetValue(name, out bit)) bits.Add(bit);
                }
            }
            return EncodeLetterFlags(bits);
        }

        /// <summary>Asciiflag string -> list of symbolic flag names.</summary>
        public static List<string> FlagsToNames(string ascii, Dictionary<string, int> nameToBit)
        {
            var rev = Reverse(nameToBit);
            var names = new List<string>();
            foreach (int bit in DecodeAsciiFlags(ascii))
            {
                string name;
                if (rev.TryGetValue(bit, out name))
                    names.Add(name);
            }
            return names;
        }

        /// <summary>List of symbolic flag names -> asciiflag string.</summary>
        public static string NamesToFlags(IEnumerable<string> names, Dictionary<string, int> nameToBit)
        {
            var bits = new List<int>();
            if (names != null)
            {
                foreach (string name in names)
                {
                    int bit;
                    if (name != null && nameToBit.TryGetValue(name, out bit))
                        bits.Add(bit);
                }
            }
            return EncodeAsciiFlags(bits);
        }

        /// <summary>Enum integer -> symbolic name (falls back to the integer as a string).</summary>
        public static string EnumName(int value, Dictionary<string, int> nameToValue)
        {
            string name;
            if (Reverse(nameToValue).TryGetValue(value, out name))
                return name;
            return value.ToString();
        }

        /// <summary>Symbolic name -> enum integer (accepts a raw integer string as a fallback).</summary>
        public static int EnumValue(string name, Dictionary<string, int> nameToValue, int fallback = 0)
        {
            if (string.IsNullOrEmpty(name)) return fallback;
            int value;
            if (nameToValue.TryGetValue(name, out value)) return value;
            if (int.TryParse(name, out value)) return value;
            return fallback;
        }
    }
}
