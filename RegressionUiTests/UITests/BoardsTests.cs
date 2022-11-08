using NUnit.Framework;
using OpenQA.Selenium;
using TestConfigurator.TestFixtures;

namespace RegressionTests.UITests
{
    [TestFixture]
    class BoardsTests : RegressionUiTestsBase
    {
        [Test]
        public void CheckBoardCreation_TES_T4()
        {
            Assert.IsTrue(_webDriverManager.GetPageTitle().Equals("Головна сторінка"));
            _webDriverManager.ClickOnElement(By.XPath("//a[text()='Усі викладачі']"));
        }

        [Test]
        public void CheckBoardCreation_TES_T1()
        {
            Assert.IsTrue(_webDriverManager.GetPageTitle().Equals("Головна сторінка"));
            _webDriverManager.ClickOnElement(By.XPath("//a[text()='Усі викладачі']"));
        }
    }
}
