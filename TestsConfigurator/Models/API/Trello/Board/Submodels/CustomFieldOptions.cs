using Newtonsoft.Json;

namespace TestsConfigurator.Models.API.Trello.Board.Submodels
{
    public class CustomFieldOptions
    {
        [JsonProperty("perField")]
        public PerField PerField { get; set; }
    }
}
