using Newtonsoft.Json;

namespace TestsConfigurator.Models.API.Trello.Board.Submodels
{
    public class CheckItems
    {
        [JsonProperty("perChecklist")]
        public PerChecklist PerChecklist { get; set; }
    }
}
