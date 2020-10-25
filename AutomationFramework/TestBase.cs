using AutomationFramework.Entities;
using AutomationFramework.Enums;
using NUnit.Framework;
using OpenQA.Selenium;
using static AutomationFramework.Entities.WebDriverManager;

namespace AutomationFramework
{
    public class TestBase
    {
        protected RunSettingManager _runSettingsSettings;
        protected FolderManager _folderManager;
        protected LogManager _logManager;
        protected WebDriverManager _webDriverManager;

        ///<summary>
        ///Initializes base objects for tests including IWebDriver, RunSettingManager etc... Use 1 time for all project tests in [OneTimeSetUp]  
        ///</summary>
        public virtual void OneTimeSetUp()
        {
            _runSettingsSettings = new RunSettingManager();
            _logManager = new LogManager();
            _folderManager = new FolderManager(_runSettingsSettings, _logManager);
            _webDriverManager = GetWebDriverManager(_runSettingsSettings.Browser, _logManager);

            _logManager._driver = _webDriverManager._driver;
        }

        ///<summary>
        ///Perform actions that are required for each test before run. Use in [SetUp]  
        ///</summary>
        public virtual void SetUp()
        {
            _folderManager.CreateTestDataFolders(TestContext.CurrentContext);
        }

        ///<summary>
        ///Perform actions that are required after each test run and do steps for the next test. Use in [TearDown]  
        ///</summary>
        public virtual void TearDown()
        {
            _runSettingsSettings.ResetTestReportAndAssetDirectoriesPathes(_logManager);
        }

        ///<summary>
        ///Perform actions that are required after all tests were run. Use 1 time for all project tests in [OneTimeTearDown]  
        ///</summary>
        public virtual void OneTimeTearDown()
        {
            _webDriverManager.Quit(_runSettingsSettings.Browser);
            _logManager.LogAction(LogLevels.global, $"Tests finished execution");
        }
    }
}
