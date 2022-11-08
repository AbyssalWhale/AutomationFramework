using Newtonsoft.Json;

namespace TestConfigurator.Models.API.Board.Submodels
{
    public class Membership
    {
        [JsonProperty("idMember")]
        public string IdMember { get; set; }

        [JsonProperty("memberType")]
        public string MemberType { get; set; }

        [JsonProperty("unconfirmed")]
        public bool Unconfirmed { get; set; }

        [JsonProperty("deactivated")]
        public bool Deactivated { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}