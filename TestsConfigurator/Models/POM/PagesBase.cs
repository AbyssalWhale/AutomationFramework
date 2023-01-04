using AutomationCore.Managers;

namespace TestsConfigurator.Models.POM
{
    public abstract class PagesBase
    {
        public string Title => WebDriver.GetPageTitle();

        protected ManagersContainer ManagersContainer;

        public WebDriver WebDriver => ManagersContainer.WebDriver ?? new WebDriver(ManagersContainer.LogManager);

        public PagesBase(ManagersContainer managersContainer)
        {
            ManagersContainer = managersContainer;
        }

        public abstract bool IsLoaded();
    }
}
