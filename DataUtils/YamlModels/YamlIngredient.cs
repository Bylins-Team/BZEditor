namespace DataUtils.YamlModels
{
    /// <summary>
    /// YAML model for ingredient
    /// </summary>
    public class YamlIngredient
    {
        public string TypeName { get; set; }
        public int Power { get; set; }
        public int Probability { get; set; }
        public bool PowerAuto { get; set; }
    }
}
