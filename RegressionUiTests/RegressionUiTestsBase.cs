using AutomationFramework;
using NUnit.Framework;
using RegressionUiTests.POM;
using TestsBaseConfigurator.POM;

namespace Tests
{
    [TestFixture]
    public class RegressionUiTestsBase : TestBase
    {
        public FactorialPage _factorialPage;

        [OneTimeSetUp]
        public override void OneTimeSetUp()
        {
            base.OneTimeSetUp();
        }

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            _factorialPage = new FactorialPage(_webDriverManager, _runSettingsSettings, _logManager, _folderManager, _utilsManager);
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