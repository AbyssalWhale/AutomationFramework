using AutomationCore.Managers;
using AutomationCore.Managers.LogManagers;
using Bogus;
using NUnit.Framework;
using System.Collections.Concurrent;
using TestsConfigurator.Controllers;
using TestsConfigurator.Models.POM.HomePage;

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

        public RunSettingsManager RunSettings => RunSettingsManager.Instance;

        public JsonLogManager Logger => TestsManagers[TestContext.CurrentContext.Test.Name].LogManager;

        public ControllersContainer Controllers => TestsControllers[TestContext.CurrentContext.Test.Name];

        public Faker FakeDataGenerator => TestsManagers[TestContext.CurrentContext.Test.Name].FakeDataGenerator;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            TestsManagers = new ConcurrentDictionary<string, ManagersContainer>();
            HomePages = new ConcurrentDictionary<string, Home>();
            TestsControllers = new ConcurrentDictionary<string, ControllersContainer>();

            if (RunSettingsManager.Instance.PublishToZephyr) ZephyrScale.CreateTestCycleConfigFile();
        }

        [SetUp]
        public void GlobalSetUp()
        {
            TestContext.AddTestAttachment(ZephyrScale.TestsLoggerManager.LoggerFilePath);
        }
    }
}
