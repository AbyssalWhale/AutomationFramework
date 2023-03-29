using Newtonsoft.Json;

namespace AutomationCore.Managers.Models.Jira.ZephyrScale.Cycles
{
    public class Project
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("self")]
        public string? Self { get; set; }
    }
}
