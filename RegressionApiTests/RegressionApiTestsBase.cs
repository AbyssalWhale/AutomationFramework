using AutomationFramework;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class RegressionApiTestsBase : TestBase
    {
        [OneTimeSetUp]
        public override void OneTimeSetUp()
        {
            base.OneTimeSetUpApiWithOutUi();
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