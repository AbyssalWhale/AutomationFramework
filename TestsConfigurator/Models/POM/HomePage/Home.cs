using AutomationCore.Managers;
using OpenQA.Selenium;
using TestsConfigurator.Models.POM.HomePage.Components;
using AutomationCore.AssertAndErrorMsgs;

namespace TestsConfigurator.Models.POM.HomePage
{
    public class Home : PagesBase
    {
        private By Icon_Logo => By.XPath("//img[@src='/assets/logo-ff4914e6.webp']");

        public GenresListComponent Genres => new GenresListComponent(WebDriver);
        public GamesGridComponent GamesGrid => new GamesGridComponent(WebDriver);
        public SearchComponent Search => new SearchComponent(WebDriver);

        public Home(ManagersContainer managersContainer) : base(managersContainer)
        {

        }

        public void Open()
        {
            var url = RunSettingsManager.InstanceUrl;
            if (url is null)
            {
                throw AEMessagesBase.GetException("Settings url is empty. Can not procceed.");
            }

            WebDriver.GoToUrl(url);
        }

        public override bool IsLoaded()
        {
            return WebDriver.FindElement(Icon_Logo).Displayed & GamesGrid.IsLoaded() & Search.IsLoaded();
        }
    }
}
