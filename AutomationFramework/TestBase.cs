using AutomationFramework.Entities;
using OpenQA.Selenium;
using System.IO;
using static AutomationFramework.Entities.WebDriverProvider;

namespace AutomationFramework
{
    public class TestBase
    {
        protected RunSettingManager runSettings;
        protected IWebDriver driver;

        public virtual void OneTimeSetUp()
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            System.Console.WriteLine(filePath);
            runSettings = new RunSettingManager();
            driver = DriverProvider.Driver(runSettings.Browser);
        }

        public virtual void TearDown()
        {
            CloseWebDriverPrecesses(runSettings.Browser);
        }
    }
}
