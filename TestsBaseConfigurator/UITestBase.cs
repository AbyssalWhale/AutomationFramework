using AutomationFramework;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Tests
{
    [TestFixture]
    public class UITestBase : TestBase
    {
        [OneTimeSetUp]
        public override void OneTimeSetUp()
        {
            base.OneTimeSetUp();
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
            base.OneTimeTearDown();
        }
    }

    public class RegressionTest : UITestBase
    {
        [Test]
        public void InitTest()
        {
            Assert.IsTrue(_webDriverManager.GoToUrl(_runSettingsSettings.InstanceUrl));
        }
        [Test]
        public void InitTest2()
        {
            Assert.IsTrue(_webDriverManager.GoToUrl("https://www.google.com/"));
        }
    }
}