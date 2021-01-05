using AutomationFramework.Entities;
using NUnit.Framework;
using OpenQA.Selenium;
using TestConfigurator.Enums.UI;
using TestConfigurator.Models.UI;
using TestConfigurator.TestFixtures;
using TestConfigurator.Workflows.API;

namespace RegressionTests.UITests
{
    class BoardsTests : RegressionUiTestsBase
    {
        [Test]
        public void CheckBoardCreation()
        {
            UITestSetup(
            out LogManager _logManager,
            out UtilsManager _utilsManager,
            out WebDriverManager _webDriverManager
                );

            _webDriverManager.GoToUrl(_runSettingsSettings.InstanceUrl);
            Assert.IsTrue(_webDriverManager.GetPageTitle().Equals("Головна сторінка"));
            _webDriverManager.ClickOnElement(By.XPath("//a[text()='Більше о нас']"));

            UITestTearDown(
                _runSettingsSettings,
                _logManager,
                _utilsManager,
                _webDriverManager
                );
        }

        [Test]
        public void CheckBoardCreation_1()
        {
            UITestSetup(
                out LogManager _logManager,
                out UtilsManager _utilsManager,
                out WebDriverManager _webDriverManager
        );

            _webDriverManager.GoToUrl(_runSettingsSettings.InstanceUrl);
            Assert.IsTrue(_webDriverManager.GetPageTitle().Equals("Головна сторінка"));
            _webDriverManager.ClickOnElement(By.XPath("//a[text()='Більше о нас']"));

            UITestTearDown(
                _runSettingsSettings,
                _logManager,
                _utilsManager,
                _webDriverManager
                );
        }
    }
}
