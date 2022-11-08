using Newtonsoft.Json;
namespace TestConfigurator.Models.API.Board.Submodels
{
    public class CustomFields
    {
        [JsonProperty("perBoard")]
        public PerBoard PerBoard { get; set; }
    }
}
