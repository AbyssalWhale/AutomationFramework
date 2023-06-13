using AutomationCore.Managers.Models.Jira.ZephyrScale.Cycles;
using Newtonsoft.Json;

namespace AutomationCore.Managers
{
    public class ZephyrScaleManager
    {
        public readonly TestsLoggerManager TestsLoggerManager;
        private readonly RestApiManager _restApiManager;
        private RunSettings RunSettings => RunSettings.Instance;

        private static readonly Lazy<ZephyrScaleManager> lazy = new Lazy<ZephyrScaleManager>(() => new ZephyrScaleManager());

        public static ZephyrScaleManager Instance { get { return lazy.Value; } }

        private ZephyrScaleManager()
        {
            TestsLoggerManager = new TestsLoggerManager(nameof(ZephyrScaleManager));
            _restApiManager = new RestApiManager(TestsLoggerManager);
        }

        public void CreateTestCycleConfigFile()
        {
            var agentConfigPath = $"{RunSettings.AgentTestsResultsFolder}jiraTestCycle.json";

            try
            {
                if (!File.Exists(agentConfigPath))
                {
                    var zephyrTestCycles = _restApiManager.GetZephyrFolders();
                    var runTestCycle = zephyrTestCycles.Values is null ?
                        null :
                        zephyrTestCycles.Values.FirstOrDefault(c => c.Name.ToLower().Equals(RunSettings.Instance.Branch));

                    var configToWrite = GetConfigurationObject(runTestCycle);

                    Directory.CreateDirectory(RunSettings.AgentTestsResultsFolder);

                    using (StreamWriter writer = new StreamWriter(agentConfigPath))
                    {
                        writer.Write(JsonConvert.SerializeObject(configToWrite));
                        writer.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                TestsLoggerManager.LogError($"'{nameof(CreateTestCycleConfigFile)}' throws exception: {ex.Message}");
            }
        }

        private object GetConfigurationObject(TestCycle runTestCycle)
        {
           return new
            {
                name = $"{DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss")} Build ID: {RunSettings.Instance.BuildId}",
                description = "Desc",
                jiraProjectVersion = 0,
                folderId = runTestCycle is null ? "null" : runTestCycle.Id.ToString()
            };
        }
    }
}
