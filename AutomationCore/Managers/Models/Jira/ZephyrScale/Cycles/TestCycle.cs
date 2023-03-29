using Newtonsoft.Json;

namespace AutomationCore.Managers.Models.Jira.ZephyrScale.Cycles
{
    public class TestCycle
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("parentId")]
        public object ParentId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("folderType")]
        public string FolderType { get; set; }

        [JsonProperty("project")]
        public Project Project { get; set; }
    }
}
