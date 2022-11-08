using Newtonsoft.Json;

namespace TestConfigurator.Models.API.Board.Submodels
{
    public class Reactions
    {
        [JsonProperty("perAction")]
        public PerAction PerAction { get; set; }

        [JsonProperty("uniquePerAction")]
        public UniquePerAction UniquePerAction { get; set; }
    }
}
