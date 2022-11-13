using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using AutomationCore.Enums;
using System.Diagnostics;
using NUnit.Framework;
using OpenQA.Selenium.Interactions;
using AutomationCore.Utils;

namespace AutomationCore.Managers
{
    public class WebDriver
    {
        private Logger _logger;
        private RunSettings _runSettings;
        private IWebDriver _driver;

        private static Dictionary<Browsers, List<string>> BrowsersProcessesNames = new Dictionary<Browsers, List<string>>
        {
            { Browsers.chrome, new List<string>() { "chrome", "chromedriver", "Google Chrome" } },
            { Browsers.firefox, new List<string>() { "Firefox", "geckodriver" } }
        };

        public WebDriver(Logger logger)
        {
            _logger = logger;
            _runSettings = RunSettings.GetRunSettings;
            _driver = InitNewCopyOfWebDriver();
        }

        private IWebDriver InitNewCopyOfWebDriver()
        {
            var browser = _runSettings.Browser.ToLower();

            if (browser.Equals(Browsers.chrome.ToString()))
            {
                return new ChromeDriver(SetChrome());
            }
            else if (browser.Equals(Browsers.firefox.ToString()))
            {
                return new FirefoxDriver(SetFirefox());
            }
            else
            {
                throw new Exception($"Unknown browser is tried to be initialized: {browser}");
            }
        }

        private ChromeOptions SetChrome()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            if (_runSettings.Headless) options.AddArguments("headless");

            return options;
        }

        private FirefoxOptions SetFirefox()
        {
            var fireFoxOptions = new FirefoxOptions();

            fireFoxOptions.SetPreference("browser.download.folderList", 2);
            fireFoxOptions.SetPreference("browser.download.manager.showWhenStarting", false);
            fireFoxOptions.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet,text/plain");
            fireFoxOptions.AddArgument("--disable-popup-blocking");
            fireFoxOptions.AddArgument("--width=1920");
            fireFoxOptions.AddArgument("--height=1000");
            if (_runSettings.Headless) fireFoxOptions.AddArguments("--headless"); ;
            fireFoxOptions.SetPreference("browser.download.dir", "provide folder");

            return fireFoxOptions;
        }

        ///<summary>
        ///Go to the provided URL address and wait until page is fully loaded
        ///</summary>
        public bool GoToUrl(string url)
        {
            _logger.LogTestAction(LogMessages.MethodExecution($"URL: {url}"));
            _driver.Navigate().GoToUrl(url);
            return IsPageLoaded();
        }

        ///<summary>
        ///Allows to previous page from current page
        ///</summary>
        public void NavigateBack()
        {
            _logger.LogTestAction(LogMessages.MethodExecution());
            _driver.Navigate().Back();
            IsPageLoaded();
        }

        ///<summary>
        ///Check page if page fully loaded via JS script. 
        ///</summary>
        public bool IsPageLoaded()
        {
            var millisecondsToWait = 10000;
            var stopwatch = new Stopwatch();
            _logger.LogTestAction(LogMessages.MethodExecution($"MSeconds to wait: {millisecondsToWait}"));

            stopwatch.Start();

            string pageState = (string)ExecuteJSScript(JSCommands.PageState);

            while (pageState != "complete")
            {
                if (stopwatch.ElapsedMilliseconds > millisecondsToWait)
                {
                    var msg = $"Unable to load page in {millisecondsToWait} MSeconds";
                    _logger.LogError(LogMessages.MethodExecution($"Method throws exception: {msg}"));
                    throw new Exception(msg);
                }

                pageState = (string)ExecuteJSScript(JSCommands.PageState);

                Thread.Sleep(500);
            }
            return true;
        }

        ///<summary>
        ///Allows to execute JS 
        ///</summary>
        public object ExecuteJSScript(string script)
        {
            _logger.LogTestAction(LogMessages.MethodExecution($"Script: {script}"));
            return ((IJavaScriptExecutor)_driver).ExecuteScript(script);
        }

        ///<summary>
        ///Allows to execute JS for elements
        ///</summary>
        public object ExecuteJSScript(string script, IWebElement[] elements)
        {
            _logger.LogTestAction(LogMessages.MethodExecution($"for element script: {script}"));
            return ((IJavaScriptExecutor)_driver).ExecuteScript(script, elements);
        }

        ///<summary>
        ///Allows to quit browser and kill all processors
        ///</summary>
        public void Quit()
        {
            _logger.LogTestAction(LogMessages.MethodExecution());
            _driver.Quit();
        }


        ///<summary>
        ///Close browser through killing all processess on machine 
        ///</summary>
        internal void CloseWebDriverProcesses(string browser)
        {
            _logger.LogTestAction(LogMessages.MethodExecution());
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

        ///<summary>
        ///Open new tab in the current browser. Go to the provided URL address and wait until page is fully loaded
        ///</summary>
        public bool GoToUrlInNewTab(string url)
        {
            _logger.LogTestAction(LogMessages.MethodExecution());
            ExecuteJSScript(JSCommands.OpenNewTab);
            _driver.SwitchTo().Window(_driver.WindowHandles[_driver.WindowHandles.Count - 1]);
            return GoToUrl(url);
        }

        ///<summary>
        ///Return the name of current page
        ///</summary>
        public string GetPageTitle()
        {
            _logger.LogTestAction(LogMessages.MethodExecution());
            return _driver.Title;
        }

        ///<summary>
        ///Wait and search element for set time. Default is 60 seconds
        ///</summary>
        public IWebElement FindElement(By elementLocator, IWebElement parent = null, int mSecondsToWait = 10000)
        {
            var message = string.Empty;
            _logger.LogTestAction(LogMessages.MethodExecution($"Locator: {elementLocator.Criteria} MSeconds to wait: {mSecondsToWait}"));

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (true)
            {
                if (stopwatch.ElapsedMilliseconds > mSecondsToWait)
                {
                    var msg = $"It is impossible to find element by locator [{elementLocator}]. Latest exception message: {message}";
                    _logger.LogError(LogMessages.MethodExecution($"Method throws exception: {msg}"));
                    throw new Exception(msg);
                }
                    
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
            }
        }

        ///<summary>
        ///Send text into elements
        ///</summary>
        public void SendKeys(By elementLocator, string textToSend, IWebElement parent = null, int secondsToWait = 60)
        {
            _logger.LogTestAction(LogMessages.MethodExecution($"Locator: {elementLocator.Criteria}"));
            var element = FindElement(elementLocator, parent, secondsToWait);
            element.Clear();
            element.SendKeys(textToSend);
        }

        ///<summary>
        ///Click on element
        ///</summary>
        public void ClickOnElement(By elementLocator, IWebElement parent = null, int mSecondsToWait = 10000)
        {
            _logger.LogTestAction(LogMessages.MethodExecution($"Locator: {elementLocator.Criteria} MSeconds to wait: {mSecondsToWait}"));
            IWebElement element = FindElement(elementLocator, parent, mSecondsToWait);
            var message = string.Empty;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (true)
            {
                if (stopwatch.ElapsedMilliseconds > mSecondsToWait)
                {
                    var msg = $"It is impossible to click element by locator [{elementLocator}]. Message: {message}";
                    _logger.LogError(LogMessages.MethodExecution($"Method throws exception: {msg}"));
                    throw new Exception(msg);
                }

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
                    element = FindElement(elementLocator, parent, mSecondsToWait);
                }
            }
        }

        ///<summary>
        ///Returns TRUE if element exists in DOM otherwise FALSE.
        ///</summary>
        public bool IsElementExistInDOM(By elementLocator, IWebElement parent = null, int secondsToWait = 60)
        {
            _logger.LogTestAction(LogMessages.MethodExecution($"Locator: {elementLocator.Criteria}"));
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
            _logger.LogTestAction(LogMessages.MethodExecution($"Locator: {locator.Criteria}"));
            if (IsElementExistInDOM(locator))
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
                js.ExecuteScript(JSCommands.MoveToElement, FindElement(locator));
            }
            else
            {
                var msg = $"Element doesn't exist in DOM. Locator: {locator}";
                _logger.LogError(LogMessages.MethodExecution($"Method throws exception: {msg}"));
                throw new Exception(msg);
            }
        }

        ///<summary>
        ///Hover the cursor over an element
        ///</summary>
        public void HoverElement(By locator)
        {
            _logger.LogTestAction(LogMessages.MethodExecution($"Locator: {locator.Criteria}"));
            if (IsElementExistInDOM(locator))
            {
                var actions = new Actions(_driver);
                actions.MoveToElement(FindElement(locator)).Perform();
            }
            else
            {
                var msg = $"Element doesn't exist in DOM. Locator: {locator}";
                _logger.LogError(LogMessages.MethodExecution($"Method throws exception: {msg}"));
                throw new Exception(msg);
            }

        }

        ///<summary>
        ///Returns TRUE if iframe exists in DOM and driver was switched - otherwise FALSE.
        ///</summary>
        public bool SwitchToIFrame(By frameLocator)
        {
            _logger.LogTestAction(LogMessages.MethodExecution($"Locator: {frameLocator.Criteria}"));
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
            _logger.LogTestAction(LogMessages.MethodExecution());
            _driver.SwitchTo().DefaultContent();
            _driver.SwitchTo().ActiveElement();
        }
    }
}
