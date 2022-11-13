using Newtonsoft.Json;

namespace TestsConfigurator.Models.API.Trello.Board.Submodels
{
    public class Labels
    {
        [JsonProperty("perBoard")]
        public PerBoard PerBoard { get; set; }
    }
}
