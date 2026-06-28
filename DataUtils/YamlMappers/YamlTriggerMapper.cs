using System;
using DataUtils.YamlModels;

namespace DataUtils.YamlMappers
{
    /// <summary>
    /// Mapper for Trigger (DG Script) - matches reference format
    /// </summary>
    public static class YamlTriggerMapper
    {
        public static YamlTrigger ToYaml(Trigger trigger)
        {
            if (trigger == null) return null;

            var yaml = new YamlTrigger
            {
                VNum = trigger.VNum,
                Name = trigger.Name ?? "",
                AttachType = EngineCodec.EnumName(trigger.Class, EngineDictionaries.AttachTypes),
                Narg = trigger.NumArg,
                Arglist = trigger.Arg ?? "",
                Script = (trigger.Body ?? "").TrimEnd('\r', '\n')
            };

            // Trigger types as engine symbolic names (single-plane letter flags)
            foreach (var name in EngineCodec.LetterFlagsToNames(trigger.Type, EngineDictionaries.TriggerTypes))
                yaml.TriggerTypes.Add(name);

            return yaml;
        }

        public static Trigger FromYaml(YamlTrigger yaml)
        {
            if (yaml == null) return null;

            var trigger = new Trigger(yaml.VNum)
            {
                Name = yaml.Name ?? "",
                Class = EngineCodec.EnumValue(yaml.AttachType, EngineDictionaries.AttachTypes),
                NumArg = yaml.Narg,
                Arg = yaml.Arglist ?? "",
                Body = yaml.Script ?? ""
            };

            // Trigger types from engine names back to single-plane letter flags
            trigger.Type = EngineCodec.NamesToLetterFlags(yaml.TriggerTypes, EngineDictionaries.TriggerTypes);

            return trigger;
        }
    }
}
