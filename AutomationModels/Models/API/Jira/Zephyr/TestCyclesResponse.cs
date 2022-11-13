using Newtonsoft.Json;

namespace TestsConfigurator.Models.API.Jira.Zephyr
{
    public class TestCyclesResponse
    {
        [JsonProperty("next")]
        public object Next { get; set; }

        [JsonProperty("startAt")]
        public int StartAt { get; set; }

        [JsonProperty("maxResults")]
        public int MaxResults { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("isLast")]
        public bool IsLast { get; set; }

        [JsonProperty("values")]
        public List<TestCycle>? Values { get; set; }
    }
}
