using Newtonsoft.Json;

namespace TestConfigurator.Models.API.Board.Submodels
{
    public class Limits
    {
        [JsonProperty("attachments")]
        public Attachments Attachments { get; set; }

        [JsonProperty("boards")]
        public Boards Boards { get; set; }

        [JsonProperty("cards")]
        public Cards Cards { get; set; }

        [JsonProperty("checklists")]
        public Checklists Checklists { get; set; }

        [JsonProperty("checkItems")]
        public CheckItems CheckItems { get; set; }

        [JsonProperty("customFields")]
        public CustomFields CustomFields { get; set; }

        [JsonProperty("customFieldOptions")]
        public CustomFieldOptions CustomFieldOptions { get; set; }

        [JsonProperty("labels")]
        public Labels Labels { get; set; }

        [JsonProperty("lists")]
        public Lists Lists { get; set; }

        [JsonProperty("stickers")]
        public Stickers Stickers { get; set; }

        [JsonProperty("reactions")]
        public Reactions Reactions { get; set; }
    }
}
