using Newtonsoft.Json;

namespace TestsConfigurator.Models.API.Jira.Zephyr
{
    public class Project
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("self")]
        public string? Self { get; set; }
    }
}
