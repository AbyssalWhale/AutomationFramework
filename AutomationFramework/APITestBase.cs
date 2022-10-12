using AutomationFramework.Managers;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

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

            _toolsManager = ToolsManager.GetToolsManager(_runSettingsSettings);

            PrepareZephyrTestCycle();

            _logManager.LogGlobalTestExecutionAction("One Time Set Up was successfully executed for tests;");
        }

        public virtual void SetUp()
        {
            _logManager.CreateTestFolderAndLog(TestContext.CurrentContext);
        }

        private void PrepareZephyrTestCycle()
        {
            var agentTempFolder = @"D:\\a\\_temp\\";
            var agentConfigPath = $"{agentTempFolder}jiraTestCycle.json";

            Console.WriteLine($"!!!!!!!!!!!{_runSettingsSettings.Branch}");

            if (!File.Exists(agentConfigPath))
            {
                Directory.CreateDirectory(agentTempFolder);
                var configToWrite = new
                {
                    name = $"{DateTime.UtcNow.ToString("dddd, dd MMMM yyyy HH:mm:ss")} Build ID: {_runSettingsSettings.BuildId}",
                    description = "Desc",
                    jiraProjectVersion = 0,
                    folderId = 4245422
                };
                Console.WriteLine($"!!!!!!!!!!!{_runSettingsSettings.Branch}");

                File.WriteAllText(agentConfigPath, JsonConvert.SerializeObject(configToWrite));
            }
        }
    }
}
