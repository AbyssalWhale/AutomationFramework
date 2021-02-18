﻿using OpenQA.Selenium;
using System.Collections.Generic;
using System.Diagnostics;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using AutomationFramework.Enums;
using NUnit.Framework;
using System.Threading;
using System;
using OpenQA.Selenium.Interactions;
using System.Collections.Concurrent;

namespace AutomationFramework.Managers
{
    public class WebDriverManager
    {
        private static WebDriverManager _webDriverManager;
        private LogManager _logManager { get { return LogManager.GetLogManager(_runSettingManager); } }
        private RunSettingManager _runSettingManager;
        internal IWebDriver _driver { get { return _allTestsWebDrivers[TestContext.CurrentContext.Test.Name]; } }
        ConcurrentDictionary<string, IWebDriver> _allTestsWebDrivers;

        #region CONST
        private const string MoveToElementJSCommand = "arguments[0].scrollIntoView({block: 'nearest'});";
        #endregion

        protected WebDriverManager(RunSettingManager runSettingManager)
        {
            _runSettingManager = runSettingManager;
            _allTestsWebDrivers = new ConcurrentDictionary<string, IWebDriver>();
        }

        internal static WebDriverManager GetWebDriverManager(RunSettingManager runSettingManager)
        {
            if (_webDriverManager == null)
            {
                _webDriverManager = new WebDriverManager(runSettingManager);
            }

            return _webDriverManager;
        }

        private static Dictionary<Browsers, List<string>> BrowsersProcessesNames = new Dictionary<Browsers, List<string>>
        {
            { Browsers.chrome, new List<string>() { "chrome", "chromedriver", "Google Chrome" } },
            { Browsers.firefox, new List<string>() { "Firefox", "geckodriver" } }
        };

        #region Browser SetUp
        private FirefoxOptions SetFirefox()
        {
            var fireFoxOptions = new FirefoxOptions();

            fireFoxOptions.SetPreference("browser.download.folderList", 2);
            fireFoxOptions.SetPreference("browser.download.manager.showWhenStarting", false);
            fireFoxOptions.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet,text/plain");
            fireFoxOptions.AddArgument("--disable-popup-blocking");
            fireFoxOptions.AddArgument("--width=1920");
            fireFoxOptions.AddArgument("--height=1000");
            if (_runSettingManager.Headless) fireFoxOptions.AddArguments("--headless"); ;
            fireFoxOptions.SetPreference("browser.download.dir", "provide folder");

            return fireFoxOptions;
        }

        private ChromeOptions SetChrome()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            if (_runSettingManager.Headless) options.AddArguments("headless");

            return options;
        }
        #endregion

        #region All Internal Methods

        ///<summary>
        ///Add new copy of IWebDriver in case if test run in parallel
        ///</summary>
        internal void AddWebDriverForTest()
        {
            if (_allTestsWebDrivers.TryAdd(TestContext.CurrentContext.Test.Name, SetUpDriver(_runSettingManager.Browser)))
            {
                _logManager.LogTestAction($"the '{_runSettingManager.Browser}' browser was initialized;");
            }
        }

        ///<summary>
        ///Create new copy of selected IWebDriver with default settings and return it.
        ///</summary>
        internal IWebDriver SetUpDriver(string browser)
        {
            browser = browser.ToLower();

            IWebDriver driver = null;

            driver = InitNewCopyOfWebDriver(browser);

            return driver;
        }

        ///<summary>
        ///Returns new copu of IWebDriver
        ///</summary>
        private IWebDriver InitNewCopyOfWebDriver(string browser)
        {
            IWebDriver driver = null;

            if (browser.Equals(Browsers.chrome.ToString()))
            {
                driver = new ChromeDriver(SetChrome());
            }
            else if (browser.Equals(Browsers.firefox.ToString()))
            {
                driver = new FirefoxDriver(SetFirefox());
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
        ///Go to the provided URL address and wait until page is fully loaded
        ///</summary>
        public bool GoToUrl(string url)
        {
            _driver.Navigate().GoToUrl(url);
            _logManager.LogTestAction($"the '{_runSettingManager.Browser}' browser was navigated to: {url}. Page was successfully loaded;", makeScreenshoot: true);
            return IsPageLoaded();
        }

        ///<summary>
        ///Open new tab in the current browser. Go to the provided URL address and wait until page is fully loaded
        ///</summary>
        public bool GoToUrlInNewTab(string url)
        {
            ExecuteJSScript("window.open();");
            _driver.SwitchTo().Window(_driver.WindowHandles[_driver.WindowHandles.Count - 1]);
            return GoToUrl(url);
        }

        ///<summary>
        ///Check page if page fully loaded via JS script. 
        ///</summary>
        public bool IsPageLoaded()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            string loadingScript = "return document.readyState";

            string pageState = (string)ExecuteJSScript(loadingScript);

            while (pageState != "complete")
            {
                if (stopwatch.ElapsedMilliseconds > 60000)
                    Assert.Fail($"It is impossible to load page.");

                pageState = (string)ExecuteJSScript(loadingScript);

                Thread.Sleep(500);
            }

            return true;
        }

        #region Browser

        ///<summary>
        ///Allows to quit browser and kill all processors
        ///</summary>
        public void Quit()
        {
            _driver.Quit();
        }

        ///<summary>
        ///Allows to execute JS 
        ///</summary>
        public object ExecuteJSScript(string script)
        {
            return ((IJavaScriptExecutor)_driver).ExecuteScript(script);
        }

        ///<summary>
        ///Allows to execute JS for elements
        ///</summary>
        public object ExecuteJSScript(string script, IWebElement[] elements)
        {
            return ((IJavaScriptExecutor)_driver).ExecuteScript(script, elements);
        }

        ///<summary>
        ///Close tabs by title and switch to another one
        ///</summary>
        public void CloseTab(string tabTitleToClose, string tabTitleToSwitch)
        {
            _driver.SwitchTo().Window(tabTitleToClose);
            _driver.Close();
            _driver.SwitchTo().Window(tabTitleToSwitch);
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

        ///<summary>
        ///Allows to previous page from current page
        ///</summary>
        public void NavigateBack()
        {
            _driver.Navigate().Back();
            IsPageLoaded();
        }

        ///<summary>
        ///Delete all current cookies
        ///</summary>
        public void DeleteAllCookies()
        {
            _driver.Manage().Cookies.DeleteAllCookies();
        }

        ///<summary>
        ///Accepts page allert and if the page is loaded after accepting
        ///</summary>
        public void AcceptAlert()
        {
            _driver.SwitchTo().Alert().Accept();
            IsPageLoaded();
        }

        ///<summary>
        ///Return the name of current page
        ///</summary>
        public string GetPageTitle()
        {
            return _driver.Title;
        }

        #endregion

        #region IteractionWithWebElements

        ///<summary>
        ///Wait and search element for set time. Default is 60 seconds
        ///</summary>
        public IWebElement FindElement(By elementLocator, IWebElement parent = null, int secondsToWait = 60)
        {
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
                    _logManager.LogTestAction($"WebElement with locator: {elementLocator} was found;", makeScreenshoot: true, element);
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
            }
        }

        ///<summary>
        ///Send text into elements
        ///</summary>
        public void SendKeys(By elementLocator, string textToSend, IWebElement parent = null, int secondsToWait = 60)
        {
            var element = FindElement(elementLocator, parent, secondsToWait);
            element.Clear();
            element.SendKeys(textToSend);
        }

        ///<summary>
        ///Click on element
        ///</summary>
        public void ClickOnElement(By elementLocator, IWebElement parent = null, int secondsToWait = 60)
        {
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

        ///<summary>
        ///Returns TRUE if element exists in DOM otherwise FALSE.
        ///</summary>
        public bool IsElementExistInDOM(By elementLocator, IWebElement parent = null, int secondsToWait = 60)
        {
            var result = false;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (true)
            {
                if (stopwatch.ElapsedMilliseconds > secondsToWait * 1000)
                {
                    return result;
                }

                try
                {
                    var element = parent == null ? _driver.FindElement(elementLocator) : parent.FindElement(elementLocator);
                    result = true;
                    return result;
                }
                catch (NoSuchElementException)
                {
                    continue;
                }
                catch (StaleElementReferenceException)
                {

                }
                catch (AssertionException)
                {
                    
                }
                catch (Exception)
                {
                    
                }
            }
        }

        ///<summary>
        ///Scrool the browser until element is visible
        ///</summary>
        public void MoveToElement(By locator)
        {
            if(IsElementExistInDOM(locator)){
                IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
                js.ExecuteScript(MoveToElementJSCommand, FindElement(locator));
            } else
            {
                Assert.IsNull($"Element doesn't exist in DOM. Locator: {locator}");
            }
        }

        ///<summary>
        ///Scrool the browser until element is visible
        ///</summary>
        public void MoveToElement(IWebElement element)
        {
            if (element == null)
            {
                Assert.IsNull($"Element can't be NULL for scrolling");
            }
            else
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
                js.ExecuteScript(MoveToElementJSCommand, element);
            }
        }

        ///<summary>
        ///Hover the cursor over an element
        ///</summary>
        public void HoverElement(By locator)
        {
            if (IsElementExistInDOM(locator))
            {
                var actions = new Actions(_driver);
                actions.MoveToElement(FindElement(locator)).Perform();
            }
            else
            {
                Assert.IsNull($"Element doesn't exist in DOM. Locator: {locator}");
            }

        }

        ///<summary>
        ///Returns TRUE if iframe exists in DOM and driver was switched - otherwise FALSE.
        ///</summary>
        public bool SwitchToIFrame(By frameLocator)
        {
            var result = false;

            if (IsElementExistInDOM(frameLocator))
            {
                _driver.SwitchTo().Frame(FindElement(frameLocator));
                result = true;
            }

            return result;
        }

        ///<summary>
        ///Switches driver back to default content if driver was switched on iframe.
        ///</summary>
        public void SwitchToDefaultContent()
        {
            _driver.SwitchTo().DefaultContent();
            _driver.SwitchTo().ActiveElement();
        }
        
        #endregion

        #endregion
    }
}