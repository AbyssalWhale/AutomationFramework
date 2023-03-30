using AutomationCore.Managers;
using Bogus;
using NUnit.Framework;
using System.Collections.Concurrent;
using TestsConfigurator.Models.Controllers;
using TestsConfigurator.Models.POM;

namespace TestsConfigurator
{
    [SetUpFixture]
    public class TestsSuitsFixture
    {
        internal ConcurrentDictionary<string, ManagersContainer> TestsManagers;
        internal ConcurrentDictionary<string, Home> HomePages;
        internal ConcurrentDictionary<string, ControllersContainer> TestsControllers;

        public ZephyrScaleManager ZephyrScale => ZephyrScaleManager.Instance;

        public RestApiManager API => TestsManagers[TestContext.CurrentContext.Test.Name].API;

        public RunSettings RunSettings => RunSettings.Instance;

        public TestsLoggerManager Logger => TestsManagers[TestContext.CurrentContext.Test.Name].LogManager;

        public ControllersContainer Controllers => TestsControllers[TestContext.CurrentContext.Test.Name];

        public Faker FakeDataGenerator => TestsManagers[TestContext.CurrentContext.Test.Name].FakeDataGenerator;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            TestsManagers = new ConcurrentDictionary<string, ManagersContainer>();
            HomePages = new ConcurrentDictionary<string, Home>();
            TestsControllers = new ConcurrentDictionary<string, ControllersContainer>();

            if (RunSettings.Instance.PublishToZephyr) ZephyrScale.CreateTestCycleConfigFile();
        }

        [SetUp]
        public void GlobalSetUp()
        {
            TestContext.AddTestAttachment(ZephyrScale.TestsLoggerManager.LoggerFullPath);
        }
    }
}
