using AutomationFramework;
using NUnit.Framework;
using NUnit.Framework.Internal;
using SeleniumExtras.PageObjects;
using TestsBaseConfigurator.POM;

namespace Tests
{
    [TestFixture]
    public class UITestBase : TestBase
    {
        public GooglePage googlePage;

        [OneTimeSetUp]
        public override void OneTimeSetUp()
        {
            base.OneTimeSetUp();
        }

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            googlePage = new GooglePage(_webDriverManager, _runSettingsSettings, _logManager, _folderManager);
        }

        [TearDown]
        public override void TearDown()
        {
            base.TearDown();
        }

        [OneTimeTearDown]
        public override void OneTimeTearDown()
        {
            base.OneTimeTearDown();
        }
    }

    public class RegressionTest : UITestBase
    {
         

        [Test]
        public void InitTest()
        {
            var wikipediaPage = googlePage.GoToWikipedia();
            wikipediaPage.FindAndOpenArticle("Giga Berlin");
        }
        [Test]
        public void InitTest2()
        {
            Assert.IsTrue(_webDriverManager.GoToUrl("https://www.google.com/"));
        }
    }
}