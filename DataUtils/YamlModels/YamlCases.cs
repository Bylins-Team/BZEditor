namespace DataUtils.YamlModels
{
    /// <summary>
    /// YAML model for Russian grammatical cases (6 cases)
    /// </summary>
    public class YamlCases
    {
        /// <summary>
        /// Nominative case (Imenitel'nyj padezh)
        /// </summary>
        public string Imen { get; set; } = "";

        /// <summary>
        /// Genitive case (Roditel'nyj padezh)
        /// </summary>
        public string Rod { get; set; } = "";

        /// <summary>
        /// Dative case (Datel'nyj padezh)
        /// </summary>
        public string Dat { get; set; } = "";

        /// <summary>
        /// Accusative case (Vinitel'nyj padezh)
        /// </summary>
        public string Vin { get; set; } = "";

        /// <summary>
        /// Instrumental case (Tvoritel'nyj padezh)
        /// </summary>
        public string Tvor { get; set; } = "";

        /// <summary>
        /// Prepositional case (Predlozhnyj padezh)
        /// </summary>
        public string Pred { get; set; } = "";
    }
}
