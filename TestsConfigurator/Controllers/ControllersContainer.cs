using AutomationCore.Managers;

namespace TestsConfigurator.Controllers
{
    public class ControllersContainer
    {
        private RestApiManager _apiManager;

        public ControllersContainer(RestApiManager apiManager)
        {
            _apiManager = apiManager;
        }

        public PlatformsController Platforms => new PlatformsController(_apiManager);
        public GamesController Games => new GamesController(_apiManager);
    }
}
