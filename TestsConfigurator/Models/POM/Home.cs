using AutomationCore.Managers;
using AutomationCore.Utils;
using OpenQA.Selenium;

namespace TestsConfigurator.Models.POM
{
    public class Home : PagesBase
    {
        private By Header_Parent => By.XPath("//div[@class='jumbotron mainJumbotron']");
        private By Header_Logo => By.XPath(Header_Parent.Criteria + "/img");
        private By Header_Title => By.XPath(Header_Parent.Criteria + "/h1");
        private By Header_Signature => By.XPath(Header_Parent.Criteria + "/p");

        public Home(ManagersContainer managersContainer) : base(managersContainer)
        {

        }

        public override bool IsLoaded()
        {
            return webDriver.FindElement(Header_Logo).Displayed &
                webDriver.FindElement(Header_Title).Displayed &
                webDriver.FindElement(Header_Signature).Displayed;
        }

        public void Open()
        {
            var path = OSHelper.TryGetSolutionDirectoryInfo().FullName + "/StaticSite/index.html";
            if (!File.Exists(path))
            {
                throw new Exception("\nUnable to find static web site for UI tests");
            }
            webDriver.GoToUrl(path);
        } 
    }
}
