using AutomationFramework.Entities;
using NUnit.Framework;
using TestsBaseConfigurator.POM;

namespace RegressionUiTests.POM
{
    public class HomePage : BasePagePOM
    {
        public override string Title => "Trello";

        public HomePage(
            WebDriverManager webDriverManager,
            RunSettingManager runSettingsManager,
            LogManager logManager,
            FolderManager folderManager,
            UtilsManager utilsManager) : 
            base(webDriverManager, runSettingsManager, logManager, folderManager, utilsManager)
        {
            _webDriverManager.GoToUrl(_runSettingsSettings.InstanceUrl);
            Assert.IsTrue(IsAt(), $"It's expected page be: {Title} but was: {_webDriverManager.GetPageTitle()}");
        }

        protected override bool IsAt()
        {
            _webDriverManager.IsPageLoaded();
            return _webDriverManager.GetPageTitle().Equals(Title);
        }
    }
}
