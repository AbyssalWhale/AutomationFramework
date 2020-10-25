using AutomationFramework.Entities;
using AutomationFramework.Enums;
using AutomationFramework.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using TestsBaseConfigurator.Enums;

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

        #region External Methods

        public GigaBerlinPage OpenGigaBerlinArticle(WikiArticles articleToOpen)
        {
            FindAndOpenArticle(articleToOpen);
            return new GigaBerlinPage(_webDriverManager, _runSettingsSettings, _logManager, _folderManager);
        }

        #endregion 

        #region Internal method

        private void FindAndOpenArticle(WikiArticles articleToOpen)
        {
            var articleName = EnumExtension.GetEnumStringValue(typeof(WikiArticles), articleToOpen);

            _logManager.LogAction(LogLevels.local, $"Seaching for the '{articleName}' article on wiki...");

            _webDriverManager.SendKeys(searchInput, articleName);
            _webDriverManager.ClickOnElement(By.XPath($"//div[@id='typeahead-suggestions']//*[text()='{articleName}']"));
        }

        #endregion
    }
}
