namespace TestsConfigurator.Models.API.Genres
{
    public class Genres
    {
        public int count { get; set; }
        public object next { get; set; }
        public object previous { get; set; }
        public List<Genre> results { get; set; }
    }
}
