using Newtonsoft.Json;

namespace AutomationFramework.Models.Jira.Zephyr
{
    public class Project
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("self")]
        public string Self { get; set; }
    }
}
