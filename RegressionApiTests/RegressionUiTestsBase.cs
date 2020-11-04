using AutomationFramework;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class RegressionUiTestsBase : TestBase
    {
        [OneTimeSetUp]
        public override void OneTimeSetUp()
        {
            base.OneTimeSetUpApi();
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
            base.OneTimeSetUpApi();
        }
    }
}