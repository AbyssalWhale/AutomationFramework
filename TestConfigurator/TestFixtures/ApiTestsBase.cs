using AutomationFramework;
using NUnit.Framework;
using TestConfigurator.Workflows.API;

namespace TestConfigurator.TestFixtures
{
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

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _boardWorkflow.RemoveAllBaordsAsync();
        }
    }
}