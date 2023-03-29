using AutomationCore.Managers;

namespace TestsConfigurator.Models.Controllers
{
    public class ControllersContainer
    {
        private RestApiManager _apiManager;

        public Board Board => new Board(_apiManager);

        public ControllersContainer(RestApiManager apiManager)
        {
            _apiManager = apiManager;
        }
    }
}
