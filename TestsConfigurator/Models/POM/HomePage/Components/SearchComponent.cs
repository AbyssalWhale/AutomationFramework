using OpenQA.Selenium;

namespace TestsConfigurator.Models.POM.HomePage.Components
{
    public class SearchComponent : ComponentBase
    {
        public SearchComponent(AutomationCore.Managers.WebDriver webdriver) : base(webdriver)
        {
        }

        protected override string Title => "Search games...";

        private By Input_SearchGame => By.XPath($"//input[@placeholder='{Title}']");

        public override bool IsLoaded() => WebDriver.FindElement(Input_SearchGame).Displayed;

        public SearchComponent Input_Search_Game(string gameName)
        {
            WebDriver.SendKeys(Input_SearchGame, gameName);
            WebDriver.FindElement(Input_SearchGame).SendKeys(Keys.Enter);
            return this;
        }
    }
}
