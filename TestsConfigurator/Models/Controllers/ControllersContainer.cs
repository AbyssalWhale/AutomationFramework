using AutomationCore.Managers;

namespace TestsConfigurator.Models.Controllers
{
    public class ControllersContainer
    {
        private RestApiManager _apiManager;

        public ControllersContainer(RestApiManager apiManager)
        {
            _apiManager = apiManager;
        }

        public PlatformsController Platforms => new PlatformsController(_apiManager);
    }
}
