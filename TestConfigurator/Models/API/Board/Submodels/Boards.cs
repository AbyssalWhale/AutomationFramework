using Newtonsoft.Json;

namespace TestConfigurator.Models.API.Board.Submodels
{
    public class Boards
    {
        [JsonProperty("totalMembersPerBoard")]
        public TotalMembersPerBoard TotalMembersPerBoard { get; set; }

        [JsonProperty("totalAccessRequestsPerBoard")]
        public TotalAccessRequestsPerBoard TotalAccessRequestsPerBoard { get; set; }
    }
}
