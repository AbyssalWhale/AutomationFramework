using AutomationFramework.Entities;
using NUnit.Framework;
using OpenQA.Selenium;

namespace TestConfigurator.Models.UI
{
    public class LoginPage : BasePagePOM
    {
        public override string Title => "Log in to Trello";

        private By _loginField => By.CssSelector("input#user");
        private By _passwordField => By.CssSelector("input#password");
        private By _loginButton => By.CssSelector("input#login");

        public LoginPage(
            WebDriverManager webDriverManager,
            RunSettingManager runSettingsManager,
            LogManager logManager,
            FolderManager folderManager,
            UtilsManager utilsManager) :
            base(webDriverManager, runSettingsManager, logManager, folderManager, utilsManager)
        {
            Assert.IsTrue(IsAt(), $"It's expected page be: {Title} but was: {_webDriverManager.GetPageTitle()}");
        }

        protected override bool IsAt()
        {
            _webDriverManager.IsPageLoaded();
            return _webDriverManager.GetPageTitle().Equals(Title);
        }

        public BoardsPage Login()
        {
            _webDriverManager.SendKeys(_loginField, _runSettingsSettings.Email);
            _webDriverManager.SendKeys(_passwordField, _runSettingsSettings.Password);
            _webDriverManager.ClickOnElement(_loginButton);

            _webDriverManager.IsPageLoaded();

            return new BoardsPage(_webDriverManager, _runSettingsSettings, _logManager, _folderManager, _utilsManager);
        }
    }
}
