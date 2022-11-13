using Newtonsoft.Json;

namespace TestsConfigurator.Models.API.Trello.Board.Submodels
{
    public class Reactions
    {
        [JsonProperty("perAction")]
        public PerAction PerAction { get; set; }

        [JsonProperty("uniquePerAction")]
        public UniquePerAction UniquePerAction { get; set; }
    }
}
