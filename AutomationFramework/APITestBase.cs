using AutomationFramework.Managers;
using NUnit.Framework;
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
            var testCycleConfig = "jiraTestCycle.json";
            var agentConfigPath = $"{agentTempFolder}{testCycleConfig}";

            //Get Root Directory
            var directory = new DirectoryInfo(Directory.GetCurrentDirectory());
            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }

            var solutionConfigPath = directory.FullName + @$"\\{testCycleConfig}";
            
            if (!File.Exists(solutionConfigPath)) throw new FileNotFoundException($"Expected pass: {solutionConfigPath}");
            Directory.CreateDirectory(agentTempFolder);
            File.Copy(solutionConfigPath, agentConfigPath);

            //
        }
    }
}
