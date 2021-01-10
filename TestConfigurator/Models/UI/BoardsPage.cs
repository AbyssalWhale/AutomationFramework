using AutomationFramework.Entities;
using NUnit.Framework;
using OpenQA.Selenium;
using TestConfigurator.Enums.UI;

namespace TestConfigurator.Models.UI
{
    public class BoardsPage : BasePagePOM
    {
        public override string Title => "Trello";

        private By _closeAtlassingWindow => By.XPath("//*[@id='layer-manager-overlay']//span[@aria-label='CloseIcon']");

        public BoardsPage(
            WebDriverManager webDriverManager,
            RunSettingManager runSettingsManager,
            LogManager logManager,
            ToolsManager utilsManager) :
            base(webDriverManager, runSettingsManager, logManager, utilsManager)
        {
            _webDriverManager.IsPageLoaded();
            Assert.IsTrue(IsAt(), $"It's expected page be: {Title} but was: {_webDriverManager.GetPageTitle()}");
        }

        protected override bool IsAt()
        {
            _webDriverManager.IsPageLoaded();
            return _webDriverManager.GetPageTitle().Contains(Title);
        }

        public void CloaseAtlassinWindow()
        {
            if (_webDriverManager.IsElementExistInDOM(_closeAtlassingWindow, secondsToWait: 15))
            {
                _webDriverManager.ClickOnElement(_closeAtlassingWindow);
            }
        }

        public void SelectMenuOption(BoardsMainPageMenu option)
        {
           var locator = _utilsManager._enum.GetEnumStringValue(typeof(BoardsMainPageMenu), option);
           _webDriverManager.ClickOnElement(By.XPath(locator));
           IsAt();
        }

        public BoardDetailsPage CreateNewBoard(BoardsTypes boardType, out string newBoardName)
        {
            newBoardName = _utilsManager._getFakeData.Random.String2(10);

            SelectMenuOption(BoardsMainPageMenu.Boards);

            _webDriverManager.ClickOnElement(By.XPath($"//h3[text()='{_utilsManager._enum.GetEnumStringValue(typeof(BoardsTypes), boardType)}']//parent::div//parent::div//span[text()='Create new board']"));
            _webDriverManager.SendKeys(By.XPath("//input[@placeholder='Add board title']"), newBoardName);
            _webDriverManager.ClickOnElement(By.XPath("//button[text()='Create Board']"));
            _webDriverManager.IsPageLoaded();

            return new BoardDetailsPage(_webDriverManager, _runSettingsSettings, _logManager, _utilsManager);
        }
    }
}
