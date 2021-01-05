using AutomationFramework.Entities;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading.Tasks;

namespace AutomationFramework
{
    //[Parallelizable(ParallelScope.All)]
    //[TestFixture]
    //class TestConfigurationBase
    //{
    //    public void OneTimeSetup(
    //        out RunSettingManager runSettingsSettings,
    //        out LogManager logManager,
    //        out FolderManager folderManager,
    //        out UtilsManager utilsManager,
    //        out WebDriverManager webDriverManager
    //        )
    //    {
    //        runSettingsSettings = new RunSettingManager();
    //        logManager = new LogManager();
    //        folderManager = new FolderManager(runSettingsSettings, logManager);
    //        utilsManager = new UtilsManager(runSettingsSettings, logManager);

    //        webDriverManager = new WebDriverManager(runSettingsSettings, logManager);
    //        logManager._driver = webDriverManager._driver;
    //        //folderManager.CreateTestDataFolders(TestContext.CurrentContext);
    //    }

    //    [Test]
    //    public void Test1()
    //    {
    //        OneTimeSetup(
    //        out RunSettingManager runSettingsSettings,
    //        out LogManager logManager,
    //        out FolderManager folderManager,
    //        out UtilsManager utilsManager,
    //        out WebDriverManager webDriverManager);

    //        webDriverManager.GoToUrl("https://api.trello.com/");
    //        webDriverManager.FindElement(By.XPath("//a[@class='btn btn-sm btn-link text-white']")).Click();
    //        Task.Delay(5000).Wait();
    //        webDriverManager._driver.Quit();
    //    }

    //    [Test]
    //    public void Test2()
    //    {
    //        OneTimeSetup(
    //        out RunSettingManager runSettingsSettings,
    //        out LogManager logManager,
    //        out FolderManager folderManager,
    //        out UtilsManager utilsManager,
    //        out WebDriverManager webDriverManager);

    //        webDriverManager.GoToUrl("https://api.trello.com/");
    //        webDriverManager.FindElement(By.XPath("//a[@class='btn btn-sm btn-link text-white']")).Click();
    //        Task.Delay(5000).Wait();
    //        webDriverManager._driver.Quit();
    //    }
    //}
}
