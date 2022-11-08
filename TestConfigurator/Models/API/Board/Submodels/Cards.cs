using Newtonsoft.Json;

namespace TestConfigurator.Models.API.Board.Submodels
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
