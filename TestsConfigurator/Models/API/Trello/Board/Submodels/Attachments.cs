﻿using Newtonsoft.Json;

namespace TestsConfigurator.Models.API.Trello.Board.Submodels
{
    public class Attachments
    {
        [JsonProperty("perBoard")]
        public PerBoard PerBoard { get; set; }

        [JsonProperty("perCard")]
        public PerCard PerCard { get; set; }
    }
}