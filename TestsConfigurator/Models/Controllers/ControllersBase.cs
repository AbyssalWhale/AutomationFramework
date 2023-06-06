using AutomationCore.Managers;

namespace TestsConfigurator.Models.Controllers
{
    public abstract class ControllersBase
    {
        protected RestApiManager _apiManager;
        protected abstract string _routeMainUrl { get; }
        
        public ControllersBase(RestApiManager apiManager)
        {
            _apiManager = apiManager;
        }
    }
}
