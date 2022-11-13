using Newtonsoft.Json;

namespace TestsConfigurator.Models.API.Trello.Board.Submodels
{
    public class OpenPerList
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("disableAt")]
        public int DisableAt { get; set; }

        [JsonProperty("warnAt")]
        public int WarnAt { get; set; }
    }
}
