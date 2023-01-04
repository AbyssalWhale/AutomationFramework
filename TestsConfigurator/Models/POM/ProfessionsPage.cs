using AutomationCore.Managers;
using OpenQA.Selenium;

namespace TestsConfigurator.Models.POM
{
    public class ProfessionsPage : PagesBase
    {
        private By Header_Title => By.CssSelector("h1.pageHeader");

        public ProfessionsPage(ManagersContainer managersContainer) : base(managersContainer)
        {
        }

        public override bool IsLoaded()
        {
            return WebDriver.IsPageLoaded() && WebDriver.FindElement(Header_Title).Displayed;
        }
    }
}
