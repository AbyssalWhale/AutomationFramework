using Newtonsoft.Json;

namespace TestConfigurator.Models.API.Board.Submodels
{
    public class CheckItems
    {
        [JsonProperty("perChecklist")]
        public PerChecklist PerChecklist { get; set; }
    }
}
