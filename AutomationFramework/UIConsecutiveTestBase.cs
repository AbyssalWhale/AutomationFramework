using AutomationFramework.Managers;
using NUnit.Framework;
using System.IO;

namespace AutomationFramework
{
    public class UIConsecutiveTestBase
    {
        protected RunSettingManager _runSettingsSettings;
        protected LogManager _logManager;
        protected ToolsManager _toolsManager;
        protected WebDriverManager _webDriverManager;


        ///<summary>
        ///Once Before UI Tests 
        ///</summary>
        public virtual void OneTimeSetUp()
        {
            _runSettingsSettings = new RunSettingManager();

            Directory.CreateDirectory(_runSettingsSettings.TestsReportDirectory);
            Directory.CreateDirectory(_runSettingsSettings.TestsAssetDirectory);
        }

        ///<summary>
        ///Before Each UI Test 
        ///</summary>
        public virtual void SetUp()
        {
            _logManager = LogManager.GetLogManager(_runSettingsSettings);
            _logManager.CreateTestFolderAndLog(TestContext.CurrentContext);

            _toolsManager = ToolsManager.GetToolsManager(_runSettingsSettings);
            _webDriverManager = WebDriverManager.GetWebDriverManager(_runSettingsSettings);
        }

        ///<summary>
        ///After Each UI Test 
        ///</summary>
        public virtual void OneTearDown()
        {
            _webDriverManager.Quit();
        }
    }
}
