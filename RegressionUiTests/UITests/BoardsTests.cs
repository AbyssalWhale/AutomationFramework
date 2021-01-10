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
        );

            _webDriverManager.GoToUrl(_runSettingsSettings.InstanceUrl);
            Assert.IsTrue(_webDriverManager.GetPageTitle().Equals("Головна сторінка"));
            _webDriverManager.ClickOnElement(By.XPath("//a[text()='Більше о нас']"));

            UITestTearDownParallelExec(_webDriverManager);
        }
    }
}
