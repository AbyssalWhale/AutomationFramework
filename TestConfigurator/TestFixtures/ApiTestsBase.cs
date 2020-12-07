using AutomationFramework;
using NUnit.Framework;
using TestConfigurator.Workflows.API;

namespace TestConfigurator.TestFixtures
{
    [TestFixture]
    public class ApiTestsBase : TestBase
    {
        protected BoardWorkflow _boardWorkflow;

        [OneTimeSetUp]
        public override void OneTimeSetUp()
        {
            OneTimeSetUpApiWithOutUi();
            _boardWorkflow = new BoardWorkflow(_utilsManager);
        }

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
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
            base.OneTimeTearDownApiWithOutUi();
        }
    }
}