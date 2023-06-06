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
        private By Titles_Cards => By.XPath("//h2[@class='chakra-heading css-1xix1js']");

        public override bool IsLoaded()
        {
            WebDriver.WaitFor(ExpectedConditions.ElementIsVisible(Cards_Skeleton));
            return Platforms.IsLoaded() && WebDriver.FindElement(Label_Title).Displayed;
        }

        public List<string> Get_Cards_Titles()
        {
            var result = new List<string>();
            var elements = WebDriver.FindElements(Titles_Cards);
            foreach (var elemen in elements)
            {
                result.Add(elemen.Text);
            }

            return result;
        }
    }
}
