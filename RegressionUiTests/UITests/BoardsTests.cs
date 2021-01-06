using AutomationFramework.Entities;
using NUnit.Framework;
using OpenQA.Selenium;
using TestConfigurator.TestFixtures;

namespace RegressionTests.UITests
{
    class BoardsTests : RegressionUiTestsBase
    {
        [Test]
        public void CheckBoardCreation()
        {
            UITestSetUpParallelExec(
            out LogManager _logManager,
            out ToolsManager _utilsManager,
            out WebDriverManager _webDriverManager
                );

            _webDriverManager.GoToUrl(_runSettingsSettings.InstanceUrl);
            Assert.IsTrue(_webDriverManager.GetPageTitle().Equals("Головна сторінка"));
            _webDriverManager.ClickOnElement(By.XPath("//a[text()='Більше о нас']"));

            UITestTearDownParallelExec(_webDriverManager);
        }

        [Test]
        public void CheckBoardCreation_1()
        {
            UITestSetUpParallelExec(
                out LogManager _logManager,
                out ToolsManager _utilsManager,
                out WebDriverManager _webDriverManager
        );

            _webDriverManager.GoToUrl(_runSettingsSettings.InstanceUrl);
            Assert.IsTrue(_webDriverManager.GetPageTitle().Equals("Головна сторінка"));
            _webDriverManager.ClickOnElement(By.XPath("//a[text()='Більше о нас']"));

            UITestTearDownParallelExec(_webDriverManager);
        }
    }
}
