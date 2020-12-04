using AutomationFramework.Entities;
using NUnit.Framework;
using OpenQA.Selenium;

namespace TestConfigurator.Models.UI
{
    public class BoardDetailsPage : BasePagePOM
    {
        public override string Title => "| Trello";

        private By BoardTitle => By.XPath("//div[@class='board-header-btn mod-board-name inline-rename-board js-rename-board']/h1");

        public BoardDetailsPage(
            WebDriverManager webDriverManager,
            RunSettingManager runSettingsManager,
            LogManager logManager,
            FolderManager folderManager,
            UtilsManager utilsManager) :
            base(webDriverManager, runSettingsManager, logManager, folderManager, utilsManager)
        {
            _webDriverManager.IsPageLoaded();
            Assert.IsTrue(IsAt(), $"It's expected page be: {Title} but was: {_webDriverManager.GetPageTitle()}");
        }

        protected override bool IsAt()
        {
            _webDriverManager.IsPageLoaded();
            return _webDriverManager.GetPageTitle().Contains(Title);
        }

        public string GetCurrentBoardTitle()
        {
            var element = _webDriverManager.FindElement(BoardTitle);

            return element.Text;
        }
    }
}
