using AutomationCore.Managers;
using RestSharp;
using TestsConfigurator.Models.API.Genres;

namespace TestsConfigurator.Controllers
{
    public class GenresController : ControllersBase
    {
        public GenresController(RestApiManager apiManager) : base(apiManager)
        {

        }

        protected override string _routeMainUrl => "genres";

        public async Task<RestResponse<Genres>> Get_AllGenres()
        {
            var response = await _apiManager.ExecuteAsync<Genres>(_routeMainUrl, Method.Get);
            return response;
        }

        public async Task<Genre> Get_Genre(string name)
        {
            var allGenres = Get_AllGenres().Result.Data.results;
            var result = allGenres.Where(g => g.name.ToLower().Equals(name.ToLower())).FirstOrDefault();

            return result;
        }
    }
}
