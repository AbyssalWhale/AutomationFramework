using Newtonsoft.Json;

namespace TestsConfigurator.Models.API.Trello.Board.Submodels
{
    public class Lists
    {
        [JsonProperty("openPerBoard")]
        public OpenPerBoard OpenPerBoard { get; set; }

        [JsonProperty("totalPerBoard")]
        public TotalPerBoard TotalPerBoard { get; set; }
    }
}
