using AutomationCore.Managers;
using OpenQA.Selenium;

namespace TestsConfigurator.Models.POM.HomePage.Components
{
    public class GenresListComponent : ComponentBase
    {
        public GenresListComponent(AutomationCore.Managers.WebDriver webdriver) : base(webdriver)
        {
        }

        protected override string Title => "Genres";

        private By Label_Title => By.XPath($"//h2[text()='{Title}']");
        private By Link_Genre(string genreName) => By.XPath($"//button[text()='{genreName}']");

        public override bool IsLoaded() => WebDriver.FindElement(Label_Title).Displayed;

        public GenresListComponent Click_Genre_Link(string genre)
        {
            WebDriver.ClickOnElement(Link_Genre(genre));
            return this;
        }
    }
}