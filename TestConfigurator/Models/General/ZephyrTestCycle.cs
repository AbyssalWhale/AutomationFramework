using Newtonsoft.Json;

namespace TestConfigurator.Models.General
{
    internal class ZephyrTestCycle
    {
        [JsonProperty("name")]
        public string Name;

        [JsonProperty("description")]
        public string Description;

        [JsonProperty("jiraProjectVersion")]
        public int JiraProjectVersion;

        [JsonProperty("folderId")]
        public int FolderId;
    }
}
