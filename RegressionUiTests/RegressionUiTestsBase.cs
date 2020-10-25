using AutomationFramework;
using NUnit.Framework;
using TestsBaseConfigurator.POM;

namespace Tests
{
    [TestFixture]
    public class RegressionUiTestsBase : TestBase
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
}