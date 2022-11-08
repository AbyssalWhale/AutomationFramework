using Newtonsoft.Json;

namespace TestConfigurator.Models.API.Board.Submodels
{
    public class TotalPerList
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("disableAt")]
        public int DisableAt { get; set; }

        [JsonProperty("warnAt")]
        public int WarnAt { get; set; }
    }
}
