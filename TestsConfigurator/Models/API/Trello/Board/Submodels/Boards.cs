using Newtonsoft.Json;

namespace TestsConfigurator.Models.API.Trello.Board.Submodels
{
    public class Boards
    {
        [JsonProperty("totalMembersPerBoard")]
        public TotalMembersPerBoard TotalMembersPerBoard { get; set; }

        [JsonProperty("totalAccessRequestsPerBoard")]
        public TotalAccessRequestsPerBoard TotalAccessRequestsPerBoard { get; set; }
    }
}
