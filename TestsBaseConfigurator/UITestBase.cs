using AutomationFramework;
using AutomationFramework.Entities;
using NUnit.Framework;
using OpenQA.Selenium;
using System.IO;

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

        [TearDown]
        public override void TearDown()
        {
            base.TearDown();
        }
    }

    public class RegressionTest : UITestBase
    {
        [Test]
        public void Test()
        {
            driver.Navigate().GoToUrl(runSettings.InstanceUrl);
        }
    }
}