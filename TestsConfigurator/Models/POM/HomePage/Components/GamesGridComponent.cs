using AutomationCore.Managers;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace TestsConfigurator.Models.POM.HomePage.Components
{
    public class GamesGridComponent : ComponentBase
    {
        public GamesGridComponent(AutomationCore.Managers.WebDriver webdriver) : base(webdriver)
        {
        }

        protected override string Title => "Games";

        public PlatformsDropDownComponent Platforms => new PlatformsDropDownComponent(WebDriver);

        private By Label_Title => By.XPath($"//h1[contains(text(), '{Title}')]");
        private By Cards_Skeleton => By.XPath("//div[@class='chakra-skeleton css-1uzecpb']");

        public override bool IsLoaded()
        {
            WebDriver.WaitFor(ExpectedConditions.ElementIsVisible(Cards_Skeleton));
            return Platforms.IsLoaded() && WebDriver.FindElement(Label_Title).Displayed;
        }
    }
}
