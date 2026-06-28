using DataUtils.YamlModels;

namespace DataUtils.YamlMappers
{
    /// <summary>
    /// Mapper for OperatedObj (object placed in room)
    /// </summary>
    public static class YamlOperatedObjMapper
    {
        public static YamlOperatedObj ToYaml(OperatedObj opObj)
        {
            if (opObj == null) return null;

            var yaml = new YamlOperatedObj
            {
                VNum = opObj.VNum,
                Probability = opObj.Probability,
                LoadType = opObj.LoadType
            };

            // Nested objects (objects inside this object)
            foreach (OperatedObj nested in opObj.ObjectsInObject)
            {
                var nestedYaml = ToYaml(nested);
                if (nestedYaml != null)
                    yaml.ObjectsInObject.Add(nestedYaml);
            }

            return yaml;
        }

        public static OperatedObj FromYaml(YamlOperatedObj yaml)
        {
            if (yaml == null) return null;

            var opObj = new OperatedObj(yaml.VNum)
            {
                Probability = yaml.Probability,
                LoadType = yaml.LoadType
            };

            // Nested objects
            if (yaml.ObjectsInObject != null)
            {
                foreach (var nested in yaml.ObjectsInObject)
                {
                    var nestedObj = FromYaml(nested);
                    if (nestedObj != null)
                        opObj.ObjectsInObject.Add(nestedObj);
                }
            }

            return opObj;
        }
    }
}
