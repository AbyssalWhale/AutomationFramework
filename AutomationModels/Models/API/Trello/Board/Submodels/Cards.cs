using Newtonsoft.Json;

namespace TestsConfigurator.Models.API.Trello.Board.Submodels
{
    public class Cards
    {
        [JsonProperty("openPerBoard")]
        public OpenPerBoard OpenPerBoard { get; set; }

        [JsonProperty("openPerList")]
        public OpenPerList OpenPerList { get; set; }

        [JsonProperty("totalPerBoard")]
        public TotalPerBoard TotalPerBoard { get; set; }

        [JsonProperty("totalPerList")]
        public TotalPerList TotalPerList { get; set; }
    }
}
