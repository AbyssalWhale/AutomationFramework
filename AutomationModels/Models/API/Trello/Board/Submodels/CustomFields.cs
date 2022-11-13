using Newtonsoft.Json;
namespace TestsConfigurator.Models.API.Trello.Board.Submodels
{
    public class CustomFields
    {
        [JsonProperty("perBoard")]
        public PerBoard PerBoard { get; set; }
    }
}
