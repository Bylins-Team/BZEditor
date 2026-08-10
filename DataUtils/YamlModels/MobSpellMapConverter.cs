using System;
using System.Collections.Generic;
using System.Globalization;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace DataUtils.YamlModels
{
    public sealed class YamlMobSpellMap : Dictionary<int, int>
    {
        public YamlMobSpellMap()
        {
            // For deserializator
        }

        public YamlMobSpellMap(IReadOnlyDictionary<int, int> dictionary)
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            foreach (var item in dictionary)
            {
                this[item.Key] = item.Value;
            }
        }
    };

    public sealed class MobSpellMapConverter : IYamlTypeConverter
    {

        public bool Accepts(Type type)
        {
            return type == typeof(YamlMobSpellMap);
        }

        public object ReadYaml(IParser parser, Type type)
        {
            var map = new YamlMobSpellMap();

            if (parser.TryConsume<MappingStart>(out var xx))
            {
                // New map format - spell type: spell amount
                while (!parser.TryConsume<MappingEnd>(out var yy))
                {
                    string stringKey = parser.Consume<Scalar>().Value;
                    string stringValue = parser.Consume<Scalar>().Value;

                    if (!int.TryParse(stringKey, NumberStyles.Integer, CultureInfo.InvariantCulture, out var spellId))
                    {
                        continue;
                    }

                    if (!int.TryParse(stringValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out var value))
                    {
                        continue;
                    }

                    if (map.TryGetValue(spellId, out var currentValue))
                    {
                        value = currentValue + value;
                    }

                    map[spellId] = value;
                }
            }
            else if (parser.TryConsume<SequenceStart>(out _))
            {
                while (!parser.TryConsume<SequenceEnd>(out _))
                {
                    string stringValue = parser.Consume<Scalar>().Value;

                    if (!int.TryParse(stringValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out var spellId))
                    {
                        continue;
                    }

                    var value = map.TryGetValue(spellId, out var currentValue) ? (currentValue + 1) : 1;
                    map[spellId] = value;
                }
            }
            return map;
        }

        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            emitter.Emit(new MappingStart());

            // Write only as map.
            if (value is YamlMobSpellMap map)
            {
                foreach (var kv in map)
                {
                    emitter.Emit(new Scalar(kv.Key.ToString(CultureInfo.InvariantCulture)));
                    emitter.Emit(new Scalar(kv.Value.ToString(CultureInfo.InvariantCulture)));
                }
            }
            emitter.Emit(new MappingEnd());
        }
    }
}
