using AutomationCore.Managers;

namespace TestsConfigurator.Models.POM
{
    public class Home : PagesBase
    {
        public Home(ManagersContainer managersContainer) : base(managersContainer)
        {
        }

        public void Open()
        {
            ManagersContainer.WebDriver.GoToUrl(ManagersContainer.RunSettings.InstanceUrl);
        }
    }
}
