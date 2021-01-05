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
        private RunSettingManager _localRunSettingManager;

        public FolderManager(RunSettingManager settingsManager)
        {
            _localRunSettingManager = settingsManager;

            CreateTestsDataMainFolders();
        }

        ///<summary>
        ///Creates initial test's reports and assets folders into test project. If folders are existed - folders will be deleted. Use in [OneTimeSetUp]  
        ///</summary>
        private void CreateTestsDataMainFolders()
        {
            Directory.CreateDirectory(_localRunSettingManager.TestsReportDirectory);
            Directory.CreateDirectory(_localRunSettingManager.TestsAssetDirectory);
        }
    }
}
