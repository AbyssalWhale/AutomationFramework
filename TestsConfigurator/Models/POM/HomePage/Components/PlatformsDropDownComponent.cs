using OpenQA.Selenium;

namespace TestsConfigurator.Models.POM.HomePage.Components
{
    public class PlatformsDropDownComponent : ComponentBase
    {
        public PlatformsDropDownComponent(AutomationCore.Managers.WebDriver webdriver) : base(webdriver)
        {
        }

        protected override string Title => "Platforms";

        private By Option_Title => By.XPath($"//span[text()='{Title}']");

        public override bool IsLoaded() => WebDriver.FindElement(Option_Title).Displayed;
    }
}