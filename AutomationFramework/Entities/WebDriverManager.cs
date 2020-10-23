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
using System.Threading;

namespace AutomationFramework.Entities
{
    public class WebDriverManager
    {
        private static WebDriverManager webDriverManager;
        internal IWebDriver _driver;

        protected WebDriverManager(string driverName){
            _driver = SetUpDriver(driverName);
        }

        public static WebDriverManager GetWebDriverManager(string driverName)
        {
            if (webDriverManager == null) {
                webDriverManager = new WebDriverManager(driverName);
            }
            
            return webDriverManager;
        }


        private static Dictionary<Browsers, List<string>> BrowsersProcessesNames = new Dictionary<Browsers, List<string>>
        {
            { Browsers.chrome, new List<string>() { "chrome", "chromedriver", "Google Chrome" } },
            { Browsers.firefox, new List<string>() { "Firefox", "geckodriver" } }
        };


        #region All Internal Methods

        ///<summary>
        ///Kill all processes for selected browser on local machine. Create new copy of selected IWebDriver with default settings and return it.
        ///</summary>
        internal IWebDriver SetUpDriver(string browser)
        {
            browser = browser.ToLower();

            IWebDriver driver = null;

            CloseWebDriverProcesses(browser);
            driver = InitNewCopyOfWebDriver(browser);

            return driver;
        }

        private IWebDriver InitNewCopyOfWebDriver(string browser)
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
            else
            {
                Assert.IsNull($"Unknown browser is tried to be initialized: {browser}");
            }

            return driver;
        }

        #region Browser SetUp
        private FirefoxOptions SetFirefox()
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

        ///<summary>
        ///Close browser through killing all processess on machine 
        ///</summary>
        internal void CloseWebDriverProcesses(string browser)
        {
            foreach (var BrowserProcessNames in BrowsersProcessesNames)
            {
                if (BrowserProcessNames.Key.ToString().Equals(browser.ToLower()))
                {
                    foreach (var processName in BrowserProcessNames.Value)
                    {
                        var processes = Process.GetProcessesByName(processName);

                        foreach (var process in processes) process.Kill();

                        break;
                    }

                    break;
                }
            }
        }
        #endregion

        #region All External Methods

        ///<summary>
        ///Go to thr provided URL address and wait until page is fully loaded
        ///</summary>
        public bool GoToUrl(string url)
        {
            _driver.Navigate().GoToUrl(url);
            return IsPageLoaded();
        }

        ///<summary>
        ///Check page if page fully loaded via JS script. 
        ///</summary>
        public bool IsPageLoaded()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            string loadingScript = "return document.readyState";
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            string pageState = (string)js.ExecuteScript(loadingScript);

            while (pageState != "complete")
            {
                if (stopwatch.ElapsedMilliseconds > 60000)
                    Assert.Fail($"It is impossible to load page.");

                pageState = (string)js.ExecuteScript(loadingScript);

                Thread.Sleep(500);
            }

            return true;
        }

        ///<summary>
        ///Allows to quit browser and kill all processors
        ///</summary>
        public void Quit(string browser)
        {
            _driver.Quit();
            CloseWebDriverProcesses(browser);
        }

        ///<summary>
        ///Allows to execute JS script
        ///</summary>
        public object ExecuteJSScript(string script)
        {
            return ((IJavaScriptExecutor)_driver).ExecuteScript(script);
        }

        ///<summary>
        ///Allows to switch tabs by name
        ///</summary>
        public void SwitchTabByTitle(string tabTitle, int tabsExpected, bool isHardCheck = false)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            while (_driver.WindowHandles.Count != tabsExpected)
            {
                if (stopwatch.ElapsedMilliseconds > 60000) Assert.Fail($"The amount of excted tab: {tabsExpected} is not match with amount of current browser tabs: {_driver.WindowHandles.Count} after 1 minute");
            }

            stopwatch.Stop();

            var tabs = _driver.WindowHandles;

            foreach (var tab in tabs)
            {
                _driver.SwitchTo().Window(tab);

                if (isHardCheck)
                {
                    if (_driver.Title == tabTitle)
                        break;
                }
                else
                {
                    if (_driver.Title.Contains(tabTitle))
                        break;
                }
            }
        }
        #endregion
    }
}
