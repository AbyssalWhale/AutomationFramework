using AutomationFramework.Managers;
using NUnit.Framework;
using OpenQA.Selenium;
using TestConfigurator.TestFixtures;

namespace RegressionTests.UITests
{
    class BoardsTests : RegressionUiTestsBase
    {
        [Test]
        public void CheckBoardCreation_TES_T4()
        {
            _webDriverManager.GoToUrl(_runSettingsSettings.InstanceUrl);
            Assert.IsTrue(_webDriverManager.GetPageTitle().Equals("Головна сторінка"));
            _webDriverManager.ClickOnElement(By.XPath("//a[text()='Усі викладачі']"));
        }

        [Test]
        public void CheckBoardCreation_TES_T1()
        {
            _webDriverManager.GoToUrl(_runSettingsSettings.InstanceUrl);
            Assert.IsTrue(_webDriverManager.GetPageTitle().Equals("Головна сторінка"));
            _webDriverManager.ClickOnElement(By.XPath("//a[text()='Усі викладачі']"));
        }
    }
}
