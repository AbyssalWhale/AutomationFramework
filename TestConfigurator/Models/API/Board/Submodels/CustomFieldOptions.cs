using Newtonsoft.Json;

namespace TestConfigurator.Models.API.Board.Submodels
{
    public class CustomFieldOptions
    {
        [JsonProperty("perField")]
        public PerField PerField { get; set; }
    }
}
