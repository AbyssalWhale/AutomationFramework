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
        }

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            _boardWorkflow = new BoardWorkflow(_toolsManager);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _boardWorkflow.RemoveAllBaordsAsync();
        }
    }
}