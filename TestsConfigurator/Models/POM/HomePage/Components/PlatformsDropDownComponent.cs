using OpenQA.Selenium;

namespace TestsConfigurator.Models.POM.HomePage.Components
{
    public class PlatformsDropDownComponent : ComponentBase
    {
        public PlatformsDropDownComponent(AutomationCore.Managers.WebDriver webdriver) : base(webdriver)
        {
        }

        protected override string Title => "Platforms";

        private By AllElements_Root => By.XPath($"{Option_Title.Criteria}//ancestor::div[@class='css-1g3r3q7']");

        private By Option_Title => By.XPath($"//span[text()='{Title}']");

        private By Option_Platform(string name) => By.XPath($"{AllElements_Root.Criteria}//button[text()='{name}']");

        public override bool IsLoaded() => WebDriver.FindElement(Option_Title).Displayed;

        public PlatformsDropDownComponent Click_Platform_DropDown()
        {
            WebDriver.ClickOnElement(AllElements_Root);
            return this;
        }

        public PlatformsDropDownComponent Click_Platform_Option(string name)
        {
            WebDriver.ClickOnElement(Option_Platform(name));
            return this;
        }
    }
}