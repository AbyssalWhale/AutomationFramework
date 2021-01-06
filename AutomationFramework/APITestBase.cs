
using AutomationFramework.Entities;
using NUnit.Framework;
using System.IO;

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
        }

        public virtual void SetUp()
        {

            _logManager = LogManager.GetLogManager(_runSettingsSettings);
            _logManager.CreateTestFoldersAndLog(TestContext.CurrentContext);

            _toolsManager = ToolsManager.GetToolsManager(_runSettingsSettings, _logManager);
        }
    }
}
