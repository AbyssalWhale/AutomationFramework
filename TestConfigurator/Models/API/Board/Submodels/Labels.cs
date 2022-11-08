using Newtonsoft.Json;

namespace TestConfigurator.Models.API.Board.Submodels
{
    public class Labels
    {
        [JsonProperty("perBoard")]
        public PerBoard PerBoard { get; set; }
    }
}
