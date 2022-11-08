using AutomationFramework;
using NUnit.Framework;

namespace TestConfigurator.TestFixtures
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture]
    public class RegressionUiTestsBase : UIParallelTestBase
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
            _webDriverManager.GoToUrl(_runSettingsSettings.InstanceUrl);
        }

        [TearDown]
        public override void TearDown()
        {
            base.TearDown();
        }
    }
}