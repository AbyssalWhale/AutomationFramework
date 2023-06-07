namespace TestsConfigurator.Models.API.Platforms
{
    public class GamePlatformsParents
    {
        public int count { get; set; }
        public object? next { get; set; }
        public object? previous { get; set; }
        public List<GamePlatforms>? results { get; set; }
    }
}
