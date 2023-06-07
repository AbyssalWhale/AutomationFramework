using AutomationCore.Managers;
using RestSharp;
using System.Collections.Concurrent;
using TestsConfigurator.Models.API.Games;
using TestsConfigurator.Models.API.Platforms;

namespace TestsConfigurator.Controllers
{
    public class GamesController : ControllersBase
    {
        public GamesController(RestApiManager apiManager) : base(apiManager)
        {
        }

        protected override string _routeMainUrl => "games";

        public async Task<RestResponse<AllGames>> Get_Games(ParentPlatform? parentPlatform = null)
        {
            RestResponse<AllGames> response;
            if (parentPlatform == null)
            {
                response = await _apiManager.ExecuteAsync<AllGames>(endPoint: _routeMainUrl, method: Method.Get);
            } else
            {
                var parameters = new ConcurrentDictionary<string, string>();
                parameters.TryAdd("parent_platforms", parentPlatform.id.ToString());
                response = await _apiManager.ExecuteAsync<AllGames>(endPoint: _routeMainUrl, method: Method.Get, parameters);
            }

            return response;
        }
    }
}