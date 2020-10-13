using NUnit.Framework;
using System.IO;

namespace AutomationFramework.Entities
{
    internal class FolderManager
    {
        ///<summary>
        ///Creates initial test's reports and assets folders into test project. If folders are existed - folders will be deleted. Use in [OneTimeSetUp]  
        ///</summary>
        public void CreateTestsDataMainFolders(RunSettingManager settingsManager)
        {
            if (Directory.Exists(settingsManager.TestReportDirectory)) Directory.Delete(settingsManager.TestReportDirectory, true);
            if (Directory.Exists(settingsManager.TestAssetDirectory)) Directory.Delete(settingsManager.TestAssetDirectory, true);
        }

        ///<summary>
        ///Creates report and assets folders for current test cases by using RunSettingManager and TestContext. If folders are existed - folders will be deleted. Use in [SetUp] 
        ///</summary>
        public void CreateTestDataFolders(RunSettingManager settingsManager, TestContext testContext)
        {
            var testReportPath = $"{settingsManager.TestReportDirectory}/{testContext.Test.Name}";
            var testAssetPath = $"{settingsManager.TestAssetDirectory}/{testContext.Test.Name}";

            if (Directory.Exists(testReportPath)) Directory.Delete(testReportPath, true);
            Directory.CreateDirectory(testReportPath);

            if (Directory.Exists(testAssetPath)) Directory.Delete(testAssetPath, true);
            Directory.CreateDirectory(testAssetPath);
        }
    }
}
