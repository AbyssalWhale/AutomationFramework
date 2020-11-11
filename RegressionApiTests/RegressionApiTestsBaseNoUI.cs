using AutomationFramework;
using NUnit.Framework;
using RegressionApiTests.Workflows;

namespace Tests
{
    [TestFixture]
    public class RegressionApiTestsBaseNoUI : TestBase
    {
        internal BoardWorkflow _boardWorkflow;

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
            base.OneTimeTearDownApiWithOutUi();
        }
    }
}