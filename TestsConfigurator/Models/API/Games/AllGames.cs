namespace TestsConfigurator.Models.API.Games
{
    public class AllGames
    {
        public int count { get; set; }
        public string next { get; set; }
        public object previous { get; set; }
        public List<GameDetails> results { get; set; }
        public bool user_platforms { get; set; }
    }
}