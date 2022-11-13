using Newtonsoft.Json;

namespace TestsConfigurator.Models.API.Trello.Board.Submodels
{
    public class Stickers
    {
        [JsonProperty("perCard")]
        public PerCard PerCard { get; set; }
    }
}
