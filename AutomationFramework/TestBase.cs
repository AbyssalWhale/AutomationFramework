using AutomationFramework.Entities;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.IO;
using static AutomationFramework.Entities.WebDriverProvider;

namespace AutomationFramework
{
    public class TestBase
    {
        FolderManager folderManager;
        protected RunSettingManager runSettings;
        protected IWebDriver driver;

        ///<summary>
        ///Initializes base objects for tests including IWebDriver, RunSettingManager etc... Use 1 time for all project tests in [OneTimeSetUp]  
        ///</summary>
        public virtual void OneTimeSetUp()
        {
            folderManager = new FolderManager();
            runSettings = new RunSettingManager();
            driver = DriverProvider.Driver(runSettings.Browser);

            folderManager.CreateTestsDataMainFolders(runSettings);
        }

        ///<summary>
        ///Perform actions that are required for each test before run. Use in [SetUp]  
        ///</summary>
        public virtual void SetUp()
        {
            folderManager.CreateTestDataFolders(runSettings, TestContext.CurrentContext);
        }

        ///<summary>
        ///Perform actions that are required after each test run and prepare for the next test. Use in [TearDown]  
        ///</summary>
        public virtual void TearDown()
        {
            runSettings.ResetTestReportAndAssetDirectoriesPathes();
        }

        ///<summary>
        ///Perform actions that are required after all tests were run. Use 1 time for all project tests in [OneTimeTearDown]  
        ///</summary>
        public virtual void OneTimeTearDown()
        {
            CloseWebDriverPrecesses(runSettings.Browser);
        }
    }
}
