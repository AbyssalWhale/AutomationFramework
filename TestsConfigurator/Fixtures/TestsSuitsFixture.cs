using AutomationCore.Managers;
using Bogus;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Concurrent;
using TestsConfigurator.Models.API.Jira.Zephyr;
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

        public ApiM API => TestsManagers[TestContext.CurrentContext.Test.Name].API;

        public RunSettings RunSettings => RunSettings.GetRunSettings;

        public TestsLogger Logger => TestsManagers[TestContext.CurrentContext.Test.Name].LogManager;

        public ControllersContainer Controllers => TestsControllers[TestContext.CurrentContext.Test.Name];

        public Faker FakeDataGenerator => TestsManagers[TestContext.CurrentContext.Test.Name].FakeDataGenerator;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            TestsManagers = new ConcurrentDictionary<string, ManagersContainer>();
            HomePages = new ConcurrentDictionary<string, Home>();
            TestsControllers = new ConcurrentDictionary<string, ControllersContainer>();

            if (RunSettings.GetRunSettings.PublishToZephyr) PrepareZephyrTestCycle();
        }

        private void PrepareZephyrTestCycle()
        {
            var agentTempFolder = @"D:\\a\\_temp\\TestResults\\";
            var agentConfigPath = $"{agentTempFolder}jiraTestCycle.json";

            try
            {
                if (!File.Exists(agentConfigPath))
                {
                    var api = new ApiM(new TestsLogger());
                    var zephyrTestCycles = api.GetZephyrFolders<TestCyclesResponse>();
                    var runTestCycle = zephyrTestCycles.Values.FirstOrDefault(c => c.Name.ToLower().Equals(RunSettings.GetRunSettings.Branch));

                    var configToWrite = new
                    {
                        name = $"{DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss")} Build ID: {RunSettings.GetRunSettings.BuildId}",
                        description = "Desc",
                        jiraProjectVersion = 0,
                        folderId = runTestCycle.Id
                    };

                    Directory.CreateDirectory(agentTempFolder);

                    using (StreamWriter writer = new StreamWriter(agentConfigPath))
                    {
                        writer.Write(JsonConvert.SerializeObject(configToWrite));
                        writer.Close();
                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
