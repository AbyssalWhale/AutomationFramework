using AutomationFramework.Managers;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace AutomationFramework{
    public class APITestBase
    {
        protected RunSettingManager _runSettingsSettings;
        protected LogManager _logManager;
        protected ToolsManager _toolsManager;

        public virtual void OneTimeSetUp()
        {
            _runSettingsSettings = new RunSettingManager();

            Directory.CreateDirectory(_runSettingsSettings.TestsReportDirectory);
            Directory.CreateDirectory(_runSettingsSettings.TestsAssetDirectory);

            _logManager = LogManager.GetLogManager(_runSettingsSettings);

            _logManager.CreateGlobalLog();

            _toolsManager = new ToolsManager(_runSettingsSettings);

            if (_runSettingsSettings.PublishToZephyr) PrepareZephyrTestCycle(); 

            _logManager.LogGlobalTestExecutionAction("One Time Set Up was successfully executed for tests;");
        }

        public virtual void SetUp()
        {
            _logManager.CreateTestFolderAndLog(TestContext.CurrentContext);
        }

        private void PrepareZephyrTestCycle()
        {
            var agentTempFolder = @"D:\\a\\_temp\\TestResults\\";
            var agentConfigPath = $"{agentTempFolder}jiraTestCycle.json";

            try
            {
                if (!File.Exists(agentConfigPath))
                {
                    var zephyrTestCycles = _toolsManager._api.GetZephyrFolders();
                    var runTestCycle = zephyrTestCycles.Values.FirstOrDefault(c => c.Name.ToLower().Equals(_runSettingsSettings.Branch));

                    var configToWrite = new
                    {
                        name = $"{DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss")} Build ID: {_runSettingsSettings.BuildId}",
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
