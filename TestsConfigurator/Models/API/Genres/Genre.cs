namespace TestsConfigurator.Models.API.Genres
{
    public class Genre
    {
        public int id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public int games_count { get; set; }
        public string image_background { get; set; }
        public List<Game> games { get; set; }
    }
}