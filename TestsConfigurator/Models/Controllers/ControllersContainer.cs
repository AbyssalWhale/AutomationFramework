using AutomationCore.Managers;

namespace TestsConfigurator.Models.Controllers
{
    public class ControllersContainer
    {
        private ApiM _apiManager;

        public Board Board => new Board(_apiManager);

        public ControllersContainer(ApiM apiManager)
        {
            _apiManager = apiManager;
        }
    }
}
