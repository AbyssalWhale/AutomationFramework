using AutomationCore.Managers;
using NUnit.Framework;
using TestsConfigurator.Models.Controllers;
using TestsConfigurator.Models.POM;

namespace TestsConfigurator
{
    [TestFixture]
    [Category("UI")]
    public class UITestsSuitFixture : TestsSuitsFixture
    {
        protected Home HomePage => HomePages[TestContext.CurrentContext.Test.Name];

        protected WebDriver Driver => TestsManagers[TestContext.CurrentContext.Test.Name].WebDriver;

        [SetUp]
        public void SetUp()
        {
            TestsManagers.TryAdd(TestContext.CurrentContext.Test.Name, new ManagersContainer());
            TestsControllers.TryAdd(TestContext.CurrentContext.Test.Name, new ControllersContainer(API));
            HomePages.TryAdd(TestContext.CurrentContext.Test.Name, new Home(TestsManagers[TestContext.CurrentContext.Test.Name]));
            HomePage.Open();
        }

        [TearDown]
        public void TearDown()
        {
            Driver.Quit();
        }
    }
}
