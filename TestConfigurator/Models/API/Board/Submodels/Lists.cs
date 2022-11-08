using Newtonsoft.Json;

namespace TestConfigurator.Models.API.Board.Submodels
{
    public class Lists
    {
        [JsonProperty("openPerBoard")]
        public OpenPerBoard OpenPerBoard { get; set; }

        [JsonProperty("totalPerBoard")]
        public TotalPerBoard TotalPerBoard { get; set; }
    }
}
