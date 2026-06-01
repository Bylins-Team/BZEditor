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
                AttachType = trigger.Class,
                Narg = trigger.NumArg,
                Arglist = trigger.Arg ?? "",
                Script = trigger.Body ?? ""
            };

            // Trigger types (split space-separated string to list)
            if (!string.IsNullOrEmpty(trigger.Type))
            {
                foreach (var t in trigger.Type.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                    yaml.TriggerTypes.Add(t);
            }

            return yaml;
        }

        public static Trigger FromYaml(YamlTrigger yaml)
        {
            if (yaml == null) return null;

            var trigger = new Trigger(yaml.VNum)
            {
                Name = yaml.Name ?? "",
                Class = yaml.AttachType,
                NumArg = yaml.Narg,
                Arg = yaml.Arglist ?? "",
                Body = yaml.Script ?? ""
            };

            // Trigger types (join list to space-separated string)
            if (yaml.TriggerTypes != null && yaml.TriggerTypes.Count > 0)
                trigger.Type = string.Join(" ", yaml.TriggerTypes.ToArray());

            return trigger;
        }
    }
}
