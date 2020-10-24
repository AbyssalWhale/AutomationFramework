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
using System;

namespace AutomationFramework.Entities
{
    public class WebDriverManager
    {
        private static WebDriverManager _webDriverManager;
        private LogManager _logManager;
        internal IWebDriver _driver;

        protected WebDriverManager(string driverName, LogManager logManager)
        {
            _driver = SetUpDriver(driverName);
            _logManager = logManager;

            _logManager.LogAction(LogLevels.global, $"Initializing the '{driverName}' browser");
        }

        internal static WebDriverManager GetWebDriverManager(string driverName, LogManager logManager)
        {
            if (_webDriverManager == null) {
                _webDriverManager = new WebDriverManager(driverName, logManager);
            }

            return _webDriverManager;
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
            _logManager.LogAction(LogLevels.local, $"Going to the url: {url}");
            _driver.Navigate().GoToUrl(url);
            return IsPageLoaded();
        }

        ///<summary>
        ///Check page if page fully loaded via JS script. 
        ///</summary>
        public bool IsPageLoaded()
        {
            _logManager.LogAction(LogLevels.local, $"Checking if a page is fully loaded...");

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
            _logManager.LogAction(LogLevels.local, $"Quit browser");
            _driver.Quit();
            CloseWebDriverProcesses(browser);
        }

        ///<summary>
        ///Allows to execute JS script
        ///</summary>
        public object ExecuteJSScript(string script)
        {
            _logManager.LogAction(LogLevels.local, $"Running JS script: {script}...");
            return ((IJavaScriptExecutor)_driver).ExecuteScript(script);
        }

        ///<summary>
        ///Allows to switch tabs by name
        ///</summary>
        public void SwitchTabByTitle(string tabTitle, int tabsExpected, bool isHardCheck = false)
        {
            _logManager.LogAction(LogLevels.local, $"Trying to switch tab with title: {tabTitle}...");

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

        ///<summary>
        ///Return the name of current page
        ///</summary>
        public string GetPageTitle()
        {
            return _driver.Title;
        }

        #region IteractionWithWebElements
        ///<summary>
        ///Wait and search element for set time. Default is 60 seconds
        ///</summary>
        public IWebElement FindElement(By elementLocator, IWebElement parent = null, int secondsToWait = 60)
        {
            _logManager.LogAction(LogLevels.local, $"Searching for elemnt with locator: {elementLocator.ToString()}...");

            var message = string.Empty;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (true)
            {
                if (stopwatch.ElapsedMilliseconds > secondsToWait * 1000)
                    Assert.Fail($"It is impossible to find element by locator [{elementLocator}]. Exception message: {message}");

                try
                {
                    var element = parent == null ? _driver.FindElement(elementLocator) : parent.FindElement(elementLocator);
                    return element;
                }
                catch (NoSuchElementException e)
                {
                    message = e.Message;
                }
                catch (StaleElementReferenceException e)
                {
                    message = e.Message;
                }
                catch (AssertionException e)
                {
                    message = e.Message;
                }
                catch (Exception e)
                {
                    message = e.Message;
                }

                Thread.Sleep(500);
            }
        }

        ///<summary>
        ///Send text into elements
        ///</summary>
        public void SendKeys(By elementLocator, string textToSend, IWebElement parent = null, int secondsToWait = 60)
        {
            _logManager.LogAction(LogLevels.local, $"Trying to send text: '{textToSend}' into element with locator: {elementLocator.ToString()}");
            var element = FindElement(elementLocator, parent, secondsToWait);
            element.Clear();
            element.SendKeys(textToSend);
        }

        ///<summary>
        ///Click on element
        ///</summary>
        public void ClickOnElement(By elementLocator, IWebElement parent = null, int secondsToWait = 60)
        {
            _logManager.LogAction(LogLevels.local, $"Trying click on the element with locator: {elementLocator.ToString()}");


            IWebElement element = FindElement(elementLocator, parent, secondsToWait);
            var message = string.Empty;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (true)
            {
                if (stopwatch.ElapsedMilliseconds > secondsToWait * 1000)
                    Assert.Fail($"It is impossible to click element by locator [{elementLocator}]. Message: {message}");

                try
                {
                    element.Click();
                    break;
                }
                catch (ElementClickInterceptedException e)
                {
                    message = e.GetType().FullName + " - " + e.Message;
                }
                catch (ElementNotInteractableException e)
                {
                    message = e.GetType().FullName + " - " + e.Message;
                }
                catch (StaleElementReferenceException)
                {
                    element = FindElement(elementLocator, parent, secondsToWait);
                }

                Thread.Sleep(250);
            }
        }
        #endregion
        #endregion
    }
}
