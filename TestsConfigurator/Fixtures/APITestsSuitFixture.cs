using AutomationCore.Managers;
using NUnit.Framework;
using TestsConfigurator.Controllers;

namespace TestsConfigurator.Fixtures
{
    [TestFixture, Category("API")]
    public class APITestsSuitFixture : TestsSuitsFixture
    {
        [SetUp]
        public void SetUp()
        {
            TestsManagers.TryAdd(TestContext.CurrentContext.Test.Name, new ManagersContainer(initWebDriver: false));
            TestsControllers.TryAdd(TestContext.CurrentContext.Test.Name, new ControllersContainer(API));
        }

        [TearDown]
        public void TearDown()
        {
        }
    }
}