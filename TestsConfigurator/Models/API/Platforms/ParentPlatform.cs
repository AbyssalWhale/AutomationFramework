namespace TestsConfigurator.Models.API.Platforms
{
    public class ParentPlatform
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? slug { get; set; }
        public List<Platform>? platforms { get; set; }
    }
}
