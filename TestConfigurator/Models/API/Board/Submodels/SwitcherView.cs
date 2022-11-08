using Newtonsoft.Json;

namespace TestConfigurator.Models.API.Board.Submodels
{
    public class SwitcherView
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("viewType")]
        public string ViewType { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("typeName")]
        public string TypeName { get; set; }

        [JsonProperty("id")]
        public string Id2 { get; set; }
    }
}
