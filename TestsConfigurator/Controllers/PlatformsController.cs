using AutomationCore.Managers;
using Bogus.DataSets;
using RestSharp;
using TestsConfigurator.Models.API.Platforms;

namespace TestsConfigurator.Controllers
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

        public async Task<RestResponse<GamePlatformsParents>> Get_ParentPlatforms()
        {
            return await _apiManager.ExecuteAsync<GamePlatformsParents>(endPoint: $"{_routeMainUrl}/lists/parents", method: Method.Get);
        }

        public async Task<ParentPlatform> Get_ParentPlatform(string name)
        {
            var allPlatforms = await Get_ParentPlatforms();
            if (allPlatforms.Data is null)
            {
                throw new Exception($"Data is null from {nameof(Get_ParentPlatform)}");
            }


            var result = allPlatforms.Data.results.Where(r => r.slug.Equals(name.ToLower())).FirstOrDefault();

            if (result is null)
            {
                var message = $"Unable to find parent platform {name} via api. Route: {allPlatforms.ResponseUri}";
                throw new Exception(message);
            }

            return result;
        }
    }
}