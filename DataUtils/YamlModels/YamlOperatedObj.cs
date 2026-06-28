using System.Collections.Generic;

namespace DataUtils.YamlModels
{
    /// <summary>
    /// YAML model for operated object (object placed in a room)
    /// </summary>
    public class YamlOperatedObj
    {
        public int VNum { get; set; }
        public int Probability { get; set; } = 100;
        public int LoadType { get; set; }
        public List<YamlOperatedObj> ObjectsInObject { get; set; } = new List<YamlOperatedObj>();
    }
}
