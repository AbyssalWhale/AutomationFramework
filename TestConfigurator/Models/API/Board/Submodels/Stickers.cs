using Newtonsoft.Json;

namespace TestConfigurator.Models.API.Board.Submodels
{
    public class Stickers
    {
        [JsonProperty("perCard")]
        public PerCard PerCard { get; set; }
    }
}
