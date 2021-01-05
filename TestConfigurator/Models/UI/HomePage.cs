using AutomationFramework.Entities;
using NUnit.Framework;
using OpenQA.Selenium;

namespace TestConfigurator.Models.UI
{
    public class HomePage : BasePagePOM
    {
        public override string Title => "Trello";

        private By _loginButton => By.XPath("//a[@class='btn btn-sm btn-link text-white']");

        public HomePage(
            WebDriverManager webDriverManager,
            RunSettingManager runSettingsManager,
            LogManager logManager,
            UtilsManager utilsManager) : 
            base(webDriverManager, runSettingsManager, logManager, utilsManager)
        {
            _webDriverManager.GoToUrl(_runSettingsSettings.InstanceUrl);
            Assert.IsTrue(IsAt(), $"It's expected page be: {Title} but was: {_webDriverManager.GetPageTitle()}");
        }

        protected override bool IsAt()
        {
            _webDriverManager.IsPageLoaded();
            return _webDriverManager.GetPageTitle().Equals(Title);
        }

        public LoginPage GoToLogin()
        {
            _webDriverManager.ClickOnElement(_loginButton);
            _webDriverManager.IsPageLoaded();
            return new LoginPage(_webDriverManager, _runSettingsSettings, _logManager, _utilsManager);
        }
    }
}
