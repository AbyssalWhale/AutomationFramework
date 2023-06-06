namespace TestsConfigurator.Models.API.Platforms
{
    public class GamePlatforms
    {
        public int count { get; set; }
        public string next { get; set; }
        public object previous { get; set; }
        public List<Platform> results { get; set; }
    }
}
