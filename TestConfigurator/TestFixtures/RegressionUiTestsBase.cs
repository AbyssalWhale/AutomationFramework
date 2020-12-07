using AutomationFramework;
using NUnit.Framework;
using TestConfigurator.Models.UI;
using TestConfigurator.Workflows.API;

namespace TestConfigurator.TestFixtures
{
    [TestFixture]
    public class RegressionUiTestsBase : TestBase
    {
        protected HomePage _homePage;
        protected BoardWorkflow _boardWorkflow;

        [OneTimeSetUp]
        public override void OneTimeSetUp()
        {
            base.OneTimeSetUp();
        }

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            _homePage = new HomePage(_webDriverManager, _runSettingsSettings, _logManager, _folderManager, _utilsManager);
            _boardWorkflow = new BoardWorkflow(_utilsManager);
        }

        [TearDown]
        public override void TearDown()
        {
            base.TearDown();
        }

        [OneTimeTearDown]
        public override void OneTimeTearDown()
        {
            _boardWorkflow.RemoveAllBaordsAsync();
            base.OneTimeTearDown();
        }
    }
}