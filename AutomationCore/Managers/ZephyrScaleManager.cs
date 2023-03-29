using AutomationCore.Managers.Models.ZephyrScale.Cycles;
using Newtonsoft.Json;

namespace AutomationCore.Managers
{
    public class ZephyrScaleManager
    {
        private RunSettings RunSettings => RunSettings.Instance;

        private static readonly Lazy<ZephyrScaleManager> lazy = new Lazy<ZephyrScaleManager>(() => new ZephyrScaleManager());

        public static ZephyrScaleManager Instance { get { return lazy.Value; } }

        private ZephyrScaleManager()
        {
        }

        public void CreateTestCycleConfigFile()
        {
            var agentConfigPath = $"{RunSettings.AgentTestsResultsFolder}jiraTestCycle.json";

            try
            {
                if (!File.Exists(agentConfigPath))
                {
                    var api = new ApiM(new TestsLogger());
                    var zephyrTestCycles = api.GetZephyrFolders<TestCyclesResponse>();
                    var runTestCycle = zephyrTestCycles.Values is null ?
                        null :
                        zephyrTestCycles.Values.FirstOrDefault(c => c.Name.ToLower().Equals(RunSettings.Instance.Branch));

                    var configToWrite = new
                    {
                        name = $"{DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss")} Build ID: {RunSettings.Instance.BuildId}",
                        description = "Desc",
                        jiraProjectVersion = 0,
                        folderId = runTestCycle is null ? "null" : runTestCycle.Id.ToString()
                    };

                    Directory.CreateDirectory(RunSettings.AgentTestsResultsFolder);

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
