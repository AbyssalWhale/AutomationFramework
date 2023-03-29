using AutomationCore.Managers;

namespace TestsConfigurator.Models.Controllers
{
    public abstract class Base
    {
        protected RestApiManager _apiManager;
        
        public Base(RestApiManager apiManager)
        {
            _apiManager = apiManager;
        }
    }
}
