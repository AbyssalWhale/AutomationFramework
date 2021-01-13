using AutomationFramework.Entities;
using AutomationFramework.Enums;
using NUnit.Framework;
using OpenQA.Selenium;
using System.IO;

namespace AutomationFramework
{
    public class UIParallelTestBase
    {
        protected RunSettingManager _runSettingsSettings;
        protected LogManager _logManager;
        protected ToolsManager _toolsManager;
        protected WebDriverManager _webDriverManager;

        ///<summary>
        ///Run once for all UI Test for parallel execution
        ///</summary>
        public virtual void OneTimeSetUp()
        {
            _runSettingsSettings = new RunSettingManager();

            Directory.CreateDirectory(_runSettingsSettings.TestsReportDirectory);
            Directory.CreateDirectory(_runSettingsSettings.TestsAssetDirectory);

            _logManager = LogManager.GetLogManager(_runSettingsSettings);

            _logManager.CreateGlobalLog();

            _toolsManager = ToolsManager.GetToolsManager(_runSettingsSettings, _logManager);

            _webDriverManager = WebDriverManager.GetWebDriverManager(_runSettingsSettings);

            _logManager.LogGlobalTestExecutionAction("One Time Set Up was successfully executed for tests;");
        }

        ///<summary>
        ///Before Each UI Test for parallel execution
        ///</summary>
        public virtual void SetUp()
        {
            _logManager.CreateTestFolderAndLog(TestContext.CurrentContext);
            _webDriverManager.AddWebDriverForTest();
        }

        ///<summary>
        ///After Each UI Test for parallel execution
        ///</summary>
        public virtual void TearDown()
        {
            _webDriverManager.Quit();
        }
    }
}
