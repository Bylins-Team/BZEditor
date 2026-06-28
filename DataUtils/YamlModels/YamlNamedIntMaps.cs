using System;
using System.Collections.Generic;
using System.Globalization;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace DataUtils.YamlModels
{
    /// <summary>Mob resistances as engine-named entries (kFire, kVitality, ...).</summary>
    public class YamlResistMap : Dictionary<string, int> { }

    /// <summary>Mob saving throws as engine-named entries (kWill, kCritical, ...).</summary>
    public class YamlSaveMap : Dictionary<string, int> { }

    /// <summary>
    /// (De)serializes mob resistances/saves. The current engine emits them as a
    /// named map (e.g. "kFire: 7"); worlds generated before that used a positional
    /// int list. This reads BOTH forms (a list is mapped onto the canonical name
    /// order) and always writes a named map with the non-zero entries only, in
    /// canonical order -- matching yaml_world_data_source.cpp.
    /// </summary>
    public class NamedIntMapConverter : IYamlTypeConverter
    {
        // Canonical order = the engine EResist / ESaving enum order, which is also
        // the positional order used by pre-named-map worlds.
        private static readonly string[] ResistNames =
            { "kFire", "kAir", "kWater", "kEarth", "kDark", "kVitality", "kMind", "kImmunity" };
        private static readonly string[] SaveNames =
            { "kWill", "kCritical", "kStability", "kReflex" };

        public bool Accepts(Type type)
        {
            return type == typeof(YamlResistMap) || type == typeof(YamlSaveMap);
        }

        private static string[] NamesFor(Type type)
        {
            return type == typeof(YamlResistMap) ? ResistNames : SaveNames;
        }

        public object ReadYaml(IParser parser, Type type)
        {
            string[] names = NamesFor(type);
            IDictionary<string, int> map = type == typeof(YamlResistMap)
                ? (IDictionary<string, int>)new YamlResistMap()
                : new YamlSaveMap();

            if (parser.TryConsume<MappingStart>(out _))
            {
                while (!parser.TryConsume<MappingEnd>(out _))
                {
                    string key = parser.Consume<Scalar>().Value;
                    string val = parser.Consume<Scalar>().Value;
                    int iv;
                    if (int.TryParse(val, NumberStyles.Integer, CultureInfo.InvariantCulture, out iv))
                        map[key] = iv;
                }
            }
            else if (parser.TryConsume<SequenceStart>(out _))
            {
                int i = 0;
                while (!parser.TryConsume<SequenceEnd>(out _))
                {
                    string val = parser.Consume<Scalar>().Value;
                    int iv;
                    if (i < names.Length && int.TryParse(val, NumberStyles.Integer, CultureInfo.InvariantCulture, out iv))
                        map[names[i]] = iv;
                    i++;
                }
            }
            return map;
        }

        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            string[] names = NamesFor(type);
            var map = value as IDictionary<string, int>;
            emitter.Emit(new MappingStart());
            if (map != null)
            {
                foreach (string name in names)
                {
                    int v;
                    if (map.TryGetValue(name, out v))
                    {
                        emitter.Emit(new Scalar(name));
                        emitter.Emit(new Scalar(v.ToString(CultureInfo.InvariantCulture)));
                    }
                }
            }
            emitter.Emit(new MappingEnd());
        }
    }
}
