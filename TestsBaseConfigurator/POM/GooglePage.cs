using AutomationFramework;
using AutomationFramework.Entities;
using AutomationFramework.Enums;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using TestsBaseConfigurator.Interfaces;

namespace TestsBaseConfigurator.POM
{
    public class GooglePage : BasePagePOM
    {
        public override string Title => "Google";

        public GooglePage(WebDriverManager webDriverManager, RunSettingManager runSettingsManager, LogManager logManager, FolderManager folderManager) : 
            base(webDriverManager, runSettingsManager, logManager, folderManager)
        {
            _webDriverManager.GoToUrl(_runSettingsSettings.InstanceUrl);
            Assert.IsTrue(IsAt(), $"It's expected page be: {Title} but was: {_webDriverManager.GetPageTitle()}");
        }

        protected override bool IsAt()
        {
            _webDriverManager.IsPageLoaded();
            return _webDriverManager.GetPageTitle().Equals(Title);
        }

        public WikipediaPage GoToWikipedia()
        {
            _webDriverManager.GoToUrl("https://www.wikipedia.org/");
            return new WikipediaPage(_webDriverManager, _runSettingsSettings, _logManager, _folderManager);
        }

        public GoogleMapsPage GoToGoogleMaps()
        {
            _webDriverManager.GoToUrl("https://www.google.com/maps/");
            return new GoogleMapsPage(_webDriverManager, _runSettingsSettings, _logManager, _folderManager);
        }
    }
}
