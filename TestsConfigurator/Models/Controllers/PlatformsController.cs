using AutomationCore.Managers;
using RestSharp;
using TestsConfigurator.Models.API.Platforms;

namespace TestsConfigurator.Models.Controllers
{
    public class PlatformsController : ControllersBase
    {
        public PlatformsController(RestApiManager apiManager) : base(apiManager)
        {
        }

        protected override string _routeMainUrl => "platforms";

        public async Task<RestResponse<GamePlatforms>> Get_VideoGamesPlatforms()
        {
            return await _apiManager.ExecuteAsync<GamePlatforms>(endPoint: _routeMainUrl, method: Method.Get);
        }

        public async Task<List<Platform>> Get_VideoGamesPlatform(string name, bool strictEqual = true)
        {
            var allPlatforms = await Get_VideoGamesPlatforms();
            if (allPlatforms.Data is null)
            {
                throw new Exception($"Data is null from {nameof(Get_VideoGamesPlatforms)}");
            }

            var result = strictEqual ?
                allPlatforms.Data.results.Where(p => p.name.ToLower().Equals(name.ToLower())).ToList() :
                allPlatforms.Data.results.Where(p => p.name.ToLower().Contains(name.ToLower())).ToList();

            if (result is null)
            {
                var message = $"Unable to find platform {name} via api";
                throw new Exception(message);
            }


            return result;
        }

        public async Task<RestResponse<GamePlatforms>> Get_ParentPlatforms()
        {
            return await _apiManager.ExecuteAsync<GamePlatforms>(endPoint:  $"{_routeMainUrl}/lists/parents", method: Method.Get);
        }
    }
}