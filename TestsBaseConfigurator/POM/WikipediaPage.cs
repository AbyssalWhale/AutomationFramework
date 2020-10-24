using AutomationFramework.Entities;
using NUnit.Framework;
using OpenQA.Selenium;

namespace TestsBaseConfigurator.POM
{
    public class WikipediaPage : BasePagePOM
    {
        public override string Title => "Wikipedia";

        private By searchInput => By.XPath("//input[@id='searchInput']");

        public WikipediaPage(WebDriverManager webDriverManager, RunSettingManager runSettingsManager, LogManager logManager, FolderManager folderManager) 
            : base(webDriverManager, runSettingsManager, logManager, folderManager)
        {
            Assert.IsTrue(IsAt(), $"It's expected page be: {Title} but was: {_webDriverManager.GetPageTitle()}");
        }

        protected override bool IsAt()
        {
            _webDriverManager.IsPageLoaded();
            return _webDriverManager.GetPageTitle().Equals(Title);
        }

        public void FindAndOpenArticle(string articleName)
        {
            _webDriverManager.SendKeys(searchInput, articleName);
            _webDriverManager.IsPageLoaded();
            _webDriverManager.ClickOnElement(By.XPath($"//div[@id='typeahead-suggestions']//*[text()='{articleName}']"));
        }
    }
}
