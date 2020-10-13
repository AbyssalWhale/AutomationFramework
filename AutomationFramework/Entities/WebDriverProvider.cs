using OpenQA.Selenium;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Configuration;
using System.IO;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using AutomationFramework.Enums;
using NUnit.Framework;

namespace AutomationFramework.Entities
{
    public class WebDriverProvider
    {
        private static Dictionary<Browsers, List<string>> BrowsersProcessesNames = new Dictionary<Browsers, List<string>>
        {
            { Browsers.chrome, new List<string>() { "chrome", "Google Chrome" } },
            { Browsers.firefox, new List<string>() { "geckodriver", "Firefox" } }
        };
        public static IWebDriver GetWebDriverInstance(string browser)
        {
            return SetUpDriver(browser);
        }

        ///<summary>
        ///Kill all processes for selected browser on local machine. Create new copy of selected IWebDriver with default settings and return it.
        ///</summary>
        internal static IWebDriver SetUpDriver(string browser)
        {
            browser = browser.ToLower();

            IWebDriver driver = null;

            CloseWebDriverPrecesses(browser);
            driver = InitNewCopyOfWebDriver(browser);

            return driver;
        }

        private static IWebDriver InitNewCopyOfWebDriver(string browser)
        {
            IWebDriver driver = null;

            string debugPath = Path.GetDirectoryName($"{System.AppDomain.CurrentDomain.BaseDirectory}");
            string browsersDriversFolder = "/drivers";

            if (browser.Equals(Browsers.chrome.ToString()))
            {
                driver = new ChromeDriver($"{debugPath}{browsersDriversFolder}");
            }
            else if (browser.Equals(Browsers.firefox.ToString()))
            {
                driver = new FirefoxDriver($"{debugPath}{browsersDriversFolder}", SetFirefox());
            }
            else if (browser.Equals(Browsers.ie.ToString()))
            {

            }
            else
            {
                Assert.IsNull($"Unknown browser is tried to be initialized: {browser}");
            }

            return driver;
        }

        ///<summary>
        ///Close browser through killing all processess on machine 
        ///</summary>
        public static void CloseWebDriverPrecesses(string browser)
        {
            foreach (var BrowserProcessNames in BrowsersProcessesNames)
            {
                if (BrowserProcessNames.Key.ToString().Equals(browser.ToLower()))
                {
                    foreach (var processName in BrowserProcessNames.Value)
                    {
                        var processes = Process.GetProcessesByName(processName.ToLower());

                        foreach (var process in processes) process.Kill();

                        break;
                    }

                    break;
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
        #endregion
    }
}
