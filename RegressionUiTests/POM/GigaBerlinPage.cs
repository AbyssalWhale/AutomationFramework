using AutomationFramework.Entities;
using AutomationFramework.Enums;
using NUnit.Framework;
using OpenQA.Selenium;

namespace TestsBaseConfigurator.POM
{
    public class GigaBerlinPage : BasePagePOM
    {
        public override string Title => "Giga Berlin - Wikipedia";
        private By CoordinatsLink = By.XPath("//span[@class='geo-default']//span[@title='Maps, aerial photos, and other data for this location']");

        public GigaBerlinPage(WebDriverManager webDriverManager, RunSettingManager runSettingsManager, LogManager logManager, FolderManager folderManager) :
            base(webDriverManager, runSettingsManager, logManager, folderManager)
        {
            Assert.IsTrue(IsAt(), $"It's expected page be: {Title} but was: {_webDriverManager.GetPageTitle()}");
        }

        protected override bool IsAt()
        {
            _webDriverManager.IsPageLoaded();
            return _webDriverManager.GetPageTitle().Equals(Title);
        }

        public string GetCoordinates()
        {
            var element = _webDriverManager.FindElement(CoordinatsLink);
            _logManager.LogAction(LogLevels.local, $"Getting geographical coordinats of the '{Title}' page on wiki page.", true, element);
            return element.Text;
        }
    }
}
