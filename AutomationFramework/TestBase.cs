using AutomationFramework.Entities;
using AutomationFramework.Enums;
using NUnit.Framework;
using OpenQA.Selenium;
using System.IO;
using static AutomationFramework.Entities.WebDriverManager;

namespace AutomationFramework
{
    public class TestBase
    {
        protected RunSettingManager _runSettingsSettings;
        //protected FolderManager _folderManager;
        //protected LogManager _logManager;
        //protected WebDriverManager _webDriverManager;
        //public UtilsManager _utilsManager;

        /////<summary>
        /////Initializes base objects for tests including IWebDriver, RunSettingManager etc... Use 1 time for all project tests in [OneTimeSetUp]  
        /////</summary>
        //public virtual void OneTimeSetUp()
        //{
        //    GeneralOneTimeSetup();

        //    _webDriverManager = new WebDriverManager(_runSettingsSettings, _logManager);
        //    _logManager._driver = _webDriverManager._driver;

        //}

        /////<summary>
        /////Initializes base objects for tests without IWebDriver. Use 1 time for all project tests in [OneTimeSetUp]  
        /////</summary>
        //public void OneTimeSetUpApiWithOutUi()
        //{
        //    GeneralOneTimeSetup();
        //}

        //private void GeneralOneTimeSetup()
        //{
        //    _runSettingsSettings = new RunSettingManager();
        //    _logManager = new LogManager();
        //    _folderManager = new FolderManager(_runSettingsSettings, _logManager);
        //    _utilsManager = new UtilsManager(_runSettingsSettings, _logManager);
        //}


        /////<summary>
        /////Perform actions that are required for each test before run. Use in [SetUp]  
        /////</summary>
        //public virtual void SetUp()
        //{
        //    _folderManager.CreateTestDataFolders(TestContext.CurrentContext);
        //}

        /////<summary>
        /////Perform actions that are required after each test run and do steps for the next test. Use in [TearDown]  
        /////</summary>
        //public virtual void TearDown()
        //{
        //    _runSettingsSettings.ResetTestReportAndAssetDirectoriesPathes(_logManager);
        //    _logManager.CreateFinalCSVLog(LogLevels.local);
        //}

        /////<summary>
        /////Perform actions that are required after all tests were run. Use 1 time for all project tests in [OneTimeTearDown]  
        /////</summary>
        //public virtual void OneTimeTearDown()
        //{
        //    _webDriverManager.Quit(_runSettingsSettings.Browser);
        //    GeneralOneTimeTearDown();
        //}

        /////<summary>
        /////Perform actions that are required after all API tests without UI were run. Use 1 time for all project tests in [OneTimeTearDown]  
        /////</summary>
        //public virtual void OneTimeTearDownApiWithOutUi()
        //{
        //    GeneralOneTimeTearDown();
        //}

        //private void GeneralOneTimeTearDown()
        //{
        //    _logManager.LogAction(LogLevels.global, $"Tests finished execution");
        //    _logManager.CreateFinalCSVLog(LogLevels.global);
        //}

        public virtual void OneTimeSetUp()
        {
            _runSettingsSettings = new RunSettingManager();

            Directory.CreateDirectory(_runSettingsSettings.TestsReportDirectory);
            Directory.CreateDirectory(_runSettingsSettings.TestsAssetDirectory);
        }

        ///<summary>
        ///Before Each Test
        ///</summary>
        public void UITestSetup(
            out LogManager logManager,
            out UtilsManager utilsManager,
            out WebDriverManager webDriverManager
            )
        {
            logManager = new LogManager(_runSettingsSettings, TestContext.CurrentContext);
            
            utilsManager = new UtilsManager(_runSettingsSettings, logManager);

            webDriverManager = new WebDriverManager(_runSettingsSettings, logManager);
            logManager._driver = webDriverManager._driver;
            
        }

        ///<summary>
        ///After Each Test
        ///</summary>
        public void UITestTearDown(
            RunSettingManager runSettingsSettings,
            LogManager logManager,
            UtilsManager utilsManager,
            WebDriverManager webDriverManager
            )
        {
            webDriverManager.Quit(runSettingsSettings.Browser);
        }
    }
}
