using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Configuration;
using System.IO;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace AutomationFramework.Entities
{
    public class WebDriverProvider
    {
        public enum Browsers { Chrome, Firefox, IE}
        private static Dictionary<Browsers, List<string>> BrowsersProcessesNames = new Dictionary<Browsers, List<string>>
        {
            { Browsers.Chrome, new List<string>() { "Google Chrome", "chrome" } },
            { Browsers.Firefox, new List<string>() { "Firefox", "geckodriver" } }
        };
        public static IWebDriver GetWebDriverInstance(string browser)
        {
            return SetUpDriver(browser);
        }
        internal static IWebDriver SetUpDriver(string browser)
        {
            IWebDriver driver = null;
            string filePath = Path.GetDirectoryName($"{System.AppDomain.CurrentDomain.BaseDirectory}");

            if (browser.ToLower().Equals(Browsers.Chrome.ToString().ToLower()))
            {
                CloseWebDriverInstances(Browsers.Chrome);
                driver = new ChromeDriver($"{filePath}/drivers");
            } else if (browser.ToLower().Equals(Browsers.Firefox.ToString().ToLower()))
            {
                CloseWebDriverInstances(Browsers.Firefox);
                driver = new FirefoxDriver($"{filePath}/drivers", SetFirefox());


            } else if (browser.ToLower().Equals(Browsers.IE.ToString().ToLower()))
            {

            }

            //todo: Finish Here
            CreateReportDirectory();

            return driver;
        }
        public static void CloseWebDriverInstances(Browsers browser)
        {
            foreach (var processName in BrowsersProcessesNames[browser])
            {
                var processes = Process.GetProcessesByName(processName);
                foreach (var process in processes)
                {
                    process.Kill();
                }
            }
        }

        #region Browser SetUp
        private static FirefoxOptions SetFirefox()
        {
            var fireFoxOptions = new FirefoxOptions();

            fireFoxOptions.SetPreference("browser.download.folderList", 2);
            fireFoxOptions.SetPreference("browser.download.manager.showWhenStarting", false);
            fireFoxOptions.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet,text/plain");
            fireFoxOptions.AddArgument("--disable-popup-blocking");
            fireFoxOptions.AddArgument("--width=1920");
            fireFoxOptions.AddArgument("--height=1080");
            fireFoxOptions.SetPreference("browser.download.dir", "provide folder");

            return fireFoxOptions;
        }
        private static void CreateReportDirectory()
        {
            
        }
        #endregion
    }
}
