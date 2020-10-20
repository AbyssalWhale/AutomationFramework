using AutomationFramework.Enums;
using NUnit.Framework;
using Serilog;
using Serilog.Formatting.Json;
using System.IO;

namespace AutomationFramework.Entities
{
    /// <summary>Class <c>FolderManager</c> allows to create folders for all tests or current test
    /// </summary>
    public class FolderManager
    {
        private LogManager _localLogManager;
        private RunSettingManager _localRunSettingManager;

        public FolderManager(RunSettingManager settingsManager, LogManager logManager)
        {
            _localRunSettingManager = settingsManager;
            _localLogManager = logManager;

            CreateTestsDataMainFolders();
        }

        ///<summary>
        ///Creates initial test's reports and assets folders into test project. If folders are existed - folders will be deleted. Use in [OneTimeSetUp]  
        ///</summary>
        private void CreateTestsDataMainFolders()
        {
            if (Directory.Exists(_localRunSettingManager.TestsReportDirectory)) Directory.Delete(_localRunSettingManager.TestsReportDirectory, true);
            Directory.CreateDirectory(_localRunSettingManager.TestsReportDirectory);

            if (Directory.Exists(_localRunSettingManager.TestsAssetDirectory)) Directory.Delete(_localRunSettingManager.TestsAssetDirectory, true);
            Directory.CreateDirectory(_localRunSettingManager.TestsAssetDirectory);

            _localLogManager.CreateGlobalLog(_localRunSettingManager);
            _localLogManager.LogAction(LogLevels.global, "Test's assets and reports folders with global log were created;");
        }

        ///<summary>
        ///Creates report and assets folders for current test cases by using RunSettingManager and TestContext. If folders are existed - folders will be deleted. Use in [SetUp] 
        ///</summary>
        internal void CreateTestDataFolders(TestContext testContext)
        {
            _localLogManager.LogAction(LogLevels.global, $"Start executing of the '{TestContext.CurrentContext.Test.Name}' test...;");

            _localRunSettingManager.TestReportDirectory = $"{_localRunSettingManager.TestsReportDirectory}/{testContext.Test.Name}";
            _localRunSettingManager.TestAssetDirectory = $"{_localRunSettingManager.TestsAssetDirectory}/{testContext.Test.Name}";

            if (Directory.Exists(_localRunSettingManager.TestReportDirectory)) Directory.Delete(_localRunSettingManager.TestReportDirectory, true);
            Directory.CreateDirectory(_localRunSettingManager.TestReportDirectory);

            if (Directory.Exists(_localRunSettingManager.TestAssetDirectory)) Directory.Delete(_localRunSettingManager.TestAssetDirectory, true);
            Directory.CreateDirectory(_localRunSettingManager.TestAssetDirectory);

            _localLogManager.CreateLogForTest(_localRunSettingManager, testContext);
        }

        ///<summary>
        ///Create new global folder for all tests in path: RunSettingManager.TestsReportDirectory. Returns new folder path in the end.
        ///</summary>
        public string CreateFolderForAllTests(string folderName)
        {
            return string.Empty;
        }

        ///<summary>
        ///Create new folder for current text in path: RunSettingManager.TestsAssetDirectory. Returns new folder path in the end.
        ///</summary>
        public string CreateFolderInTestDirectory()
        {
            return string.Empty;
        }
    }
}
