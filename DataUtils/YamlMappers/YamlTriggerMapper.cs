using System;
using System.Collections.Generic;
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
                AddFlag = trigger.AddFlag,
                Arglist = trigger.Arg ?? "",
                Script = (trigger.Body ?? "").TrimEnd('\r', '\n')
            };

            // Trigger types as engine symbolic names; the name of a bit depends on the attach type
            yaml.TriggerTypes.AddRange(TriggerTypeCodec.ToNames(trigger.Type, trigger.Class));

            return yaml;
        }

        /// <param name="problems">Receives builder errors in trigger_types (a foreign-prefix or unknown name).</param>
        public static Trigger FromYaml(YamlTrigger yaml, ICollection<string> problems = null)
        {
            if (yaml == null) return null;

            var trigger = new Trigger(yaml.VNum)
            {
                Name = yaml.Name ?? "",
                Class = EngineCodec.EnumValue(yaml.AttachType, EngineDictionaries.AttachTypes),
                NumArg = yaml.Narg,
                AddFlag = yaml.AddFlag,
                Arg = yaml.Arglist ?? "",
                Body = yaml.Script ?? ""
            };

            // Trigger types from engine names back to single-plane letter flags
            trigger.Type = TriggerTypeCodec.FromNames(yaml.TriggerTypes, trigger.Class, problems);

            return trigger;
        }
    }
}
