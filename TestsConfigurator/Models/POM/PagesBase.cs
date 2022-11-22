using AutomationCore.Managers;

namespace TestsConfigurator.Models.POM
{
    public abstract class PagesBase
    {
        public string Title => webDriver.GetPageTitle();

        protected ManagersContainer ManagersContainer;

        protected WebDriver webDriver => ManagersContainer.WebDriver;

        public PagesBase(ManagersContainer managersContainer)
        {
            ManagersContainer = managersContainer;
        }

        public abstract bool IsLoaded();
    }
}
