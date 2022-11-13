using AutomationCore.Managers;

namespace TestsConfigurator.Models.Controllers
{
    public abstract class Base
    {
        protected ApiM _apiManager;
        
        public Base(ApiM apiManager)
        {
            _apiManager = apiManager;
        }
    }
}
