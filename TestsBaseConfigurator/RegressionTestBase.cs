using AutomationFramework;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TestsBaseConfigurator.Enums;
using TestsBaseConfigurator.POM;
using static TestsBaseConfigurator.POM.GoogleMapsPage;

namespace Tests
{
    [TestFixture]
    public class RegressionTestBase : TestBase
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