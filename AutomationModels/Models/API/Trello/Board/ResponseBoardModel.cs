using AutomationCore.Managers.Models;
using Newtonsoft.Json;
using TestsConfigurator.Models.API.Trello.Board.Submodels;

namespace TestsConfigurator.Models.API.Trello.Board
{
    public class ResponseBoardModel : IRestObject
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }

        [JsonProperty("descData")]
        public object DescData { get; set; }

        [JsonProperty("closed")]
        public bool Closed { get; set; }

        [JsonProperty("dateClosed")]
        public object DateClosed { get; set; }

        [JsonProperty("idOrganization")]
        public string IdOrganization { get; set; }

        [JsonProperty("idEnterprise")]
        public object IdEnterprise { get; set; }

        [JsonProperty("limits")]
        public Limits Limits { get; set; }

        [JsonProperty("pinned")]
        public bool Pinned { get; set; }

        [JsonProperty("starred")]
        public bool Starred { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("prefs")]
        public Prefs Prefs { get; set; }

        [JsonProperty("shortLink")]
        public string ShortLink { get; set; }

        [JsonProperty("subscribed")]
        public bool Subscribed { get; set; }

        [JsonProperty("labelNames")]
        public LabelNames LabelNames { get; set; }

        [JsonProperty("powerUps")]
        public List<object> PowerUps { get; set; }

        [JsonProperty("dateLastActivity")]
        public object DateLastActivity { get; set; }

        [JsonProperty("dateLastView")]
        public object DateLastView { get; set; }

        [JsonProperty("shortUrl")]
        public string ShortUrl { get; set; }

        [JsonProperty("idTags")]
        public List<object> IdTags { get; set; }

        [JsonProperty("datePluginDisable")]
        public object DatePluginDisable { get; set; }

        [JsonProperty("creationMethod")]
        public string CreationMethod { get; set; }

        [JsonProperty("ixUpdate")]
        public string IxUpdate { get; set; }

        [JsonProperty("templateGallery")]
        public object TemplateGallery { get; set; }

        [JsonProperty("enterpriseOwned")]
        public bool EnterpriseOwned { get; set; }

        [JsonProperty("idBoardSource")]
        public object IdBoardSource { get; set; }

        [JsonProperty("premiumFeatures")]
        public List<string> PremiumFeatures { get; set; }

        [JsonProperty("idMemberCreator")]
        public string IdMemberCreator { get; set; }

        [JsonProperty("memberships")]
        public List<Membership> Memberships { get; set; }
    }
}
