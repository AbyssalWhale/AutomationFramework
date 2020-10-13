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
            driver.Navigate().GoToUrl(runSettings.InstanceUrl);
        }
        [Test]
        public void InitTest2()
        {
            driver.Navigate().GoToUrl("https://www.google.com/");
        }
    }
}