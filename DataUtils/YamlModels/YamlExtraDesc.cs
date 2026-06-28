using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace DataUtils.YamlModels
{
    /// <summary>
    /// YAML model for extra description - matches reference format
    /// </summary>
    public class YamlExtraDesc
    {
        public string Keywords { get; set; } = "";

        [YamlMember(ScalarStyle = ScalarStyle.Literal)]
        public string Description { get; set; } = "";
    }
}
