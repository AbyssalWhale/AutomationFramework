using AutomationFramework;
using NUnit.Framework;
using TestConfigurator.Workflows.API;

namespace TestConfigurator.TestFixtures
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture]
    public class ApiTestsBase : APITestBase
    {
        protected BoardWorkflow _boardWorkflow;

        [OneTimeSetUp]
        public override void OneTimeSetUp()
        {
            base.OneTimeSetUp();

            _boardWorkflow = new BoardWorkflow(_toolsManager);
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
        public void OneTimeTearDown()
        {
            _boardWorkflow.RemoveAllBaordsAsync();
        }
    }
}