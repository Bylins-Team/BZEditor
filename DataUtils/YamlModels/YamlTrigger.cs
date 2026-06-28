using System.Collections.Generic;
using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace DataUtils.YamlModels
{
    /// <summary>
    /// YAML model for a trigger (DG Script) - matches reference format
    /// </summary>
    public class YamlTrigger
    {
        [YamlMember(Alias = "vnum")]
        public int VNum { get; set; }

        /// <summary>
        /// Trigger name
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// Attach type: 0=mob, 1=obj, 2=room
        /// </summary>
        public string AttachType { get; set; }

        /// <summary>
        /// Trigger types as list of strings
        /// </summary>
        public List<string> TriggerTypes { get; set; } = new List<string>();

        /// <summary>
        /// Numeric argument
        /// </summary>
        public int Narg { get; set; }

        /// <summary>
        /// Argument list (keywords, etc.)
        /// </summary>
        public string Arglist { get; set; } = "";

        /// <summary>
        /// Script body
        /// </summary>
        [YamlMember(ScalarStyle = ScalarStyle.Literal)]
        public string Script { get; set; } = "";
    }
}
