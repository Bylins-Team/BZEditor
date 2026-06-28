using DataUtils.YamlModels;

namespace DataUtils.YamlMappers
{
    /// <summary>
    /// Mapper for Cases (Russian grammatical cases)
    /// </summary>
    public static class YamlCasesMapper
    {
        public static YamlCases ToYaml(Cases cases)
        {
            if (cases == null) return new YamlCases();
            return new YamlCases
            {
                Imen = cases.Imen ?? "",
                Rod = cases.Rod ?? "",
                Dat = cases.Dat ?? "",
                Vin = cases.Vin ?? "",
                Tvor = cases.Tvor ?? "",
                Pred = cases.Pred ?? ""
            };
        }

        public static void FromYaml(YamlCases yaml, Cases target)
        {
            if (yaml == null || target == null) return;
            target.Imen = yaml.Imen ?? "";
            target.Rod = yaml.Rod ?? "";
            target.Dat = yaml.Dat ?? "";
            target.Vin = yaml.Vin ?? "";
            target.Tvor = yaml.Tvor ?? "";
            target.Pred = yaml.Pred ?? "";
        }
    }
}
