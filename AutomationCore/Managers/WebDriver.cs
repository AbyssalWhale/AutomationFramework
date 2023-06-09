using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using AutomationCore.Enums;
using System.Diagnostics;
using OpenQA.Selenium.Interactions;
using AutomationCore.Utils;
using AutomationCore.AssertAndErrorMsgs.UI;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using OpenQA.Selenium.Remote;

namespace AutomationCore.Managers
{
    public class WebDriver
    {
        private TestsLoggerManager _logger;
        private RunSettings _runSettings;
        internal IWebDriver _seleniumDriver;

        private static Dictionary<Browsers, List<string>> BrowsersProcessesNames = new Dictionary<Browsers, List<string>>
        {
            { Browsers.chrome, new List<string>() { "chrome", "chromedriver", "Google Chrome" } },
            { Browsers.firefox, new List<string>() { "Firefox", "geckodriver" } }
        };

        public WebDriver(TestsLoggerManager logger)
        {
            _logger = logger;
            _runSettings = RunSettings.Instance;
            _seleniumDriver = InitNewCopyOfWebDriver();
            _seleniumDriver.Manage().Window.Maximize();
            _seleniumDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_runSettings.ImplicitWait);
            _seleniumDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
        }

        private IWebDriver InitNewCopyOfWebDriver()
        {
            var browser = _runSettings.Browser.ToLower();

            if (browser.Equals(Browsers.chrome.ToString()))
            {
                //ChromeDriverService service_Local = ChromeDriverService.CreateDefaultService();
                //service_Local.WhitelistedIPAddresses = " ";
                //service_Local.Port = 9515;
                var uri = new Uri("http://localhost:4444");
                //var result = new RemoteWebDriver(remoteAddress: uri, options: SetChrome());
                var result = new RemoteWebDriver(remoteAddress: uri, options: SetChrome());
                result.Manage().Window.Size = new System.Drawing.Size(1920, 1080);
                return result;
            }
            else if (browser.Equals(Browsers.firefox.ToString()))
            {
                return new FirefoxDriver(SetFirefox());
            }
            else
            {
                var msg = $"Unknown browser is tried to be initialized: {browser}";
                _logger.LogError(LogMessages.MethodExecution($"Method throws exception: {msg}"));
                throw UIAMessages.GetException(msg);
            }
        }

        private ChromeOptions SetChrome()
        {
            ChromeOptions options = new ChromeOptions();
            //options.AddArgument("--start-maximized");
            options.AddArgument("--window-size=1920,1080");

            if (_runSettings.Headless)
            {
                //options.AddArgument("--headless=new");
            };

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

        public WebDriver WaitFor_ElementBe_Visible(Func<IWebDriver, IWebElement> func, int secondsToWait)
        {
            var wait = new WebDriverWait(_seleniumDriver, TimeSpan.FromSeconds(secondsToWait));
            try
            {
                wait.Until(func);
                //wait.Until(ExpectedConditions.ElementIsVisible(locator));
            }
            catch (Exception ex)
            {
                _logger.LogError(LogMessages.MethodExecution($"{WaitFor_ElementBe_Visible} has exception: {ex.Message}"));
            }
            return this;
        }

        public WebDriver WaitFor(Func<IWebDriver, IWebElement> condition, int secondsToWait = 5)
        {
            var wait = new WebDriverWait(_seleniumDriver, TimeSpan.FromSeconds(secondsToWait));
            try
            {
                wait.Until(condition);
            }
            catch (Exception ex)
            {
                _logger.LogError(LogMessages.MethodExecution($"{WaitFor_ElementBe_Visible} has exception: {ex.Message}"));
            }
            return this;
        }

        ///<summary>
        ///Go to the provided URL address and wait until page is fully loaded
        ///</summary>
        public bool GoToUrl(string url)
        {
            _logger.LogTestAction(LogMessages.MethodExecution($"URL: {url}"));
            _seleniumDriver.Navigate().GoToUrl(url);
            return IsPageLoaded();
        }

        ///<summary>
        ///Navigates back to a previous page and wait until page is fully loaded
        ///</summary>
        public void NavigateBack()
        {
            _logger.LogTestAction(LogMessages.MethodExecution());
            _seleniumDriver.Navigate().Back();
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
                    throw UIAMessages.GetException(msg);
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
            return ((IJavaScriptExecutor)_seleniumDriver).ExecuteScript(script);
        }

        ///<summary>
        ///Allows to execute JS for elements
        ///</summary>
        public object ExecuteJSScript(string script, List<IWebElement> elements)
        {
            _logger.LogTestAction(LogMessages.MethodExecution($"for element script: {script}"));
            return ((IJavaScriptExecutor)_seleniumDriver).ExecuteScript(script, elements);
        }

        ///<summary>
        ///Allows to quit browser
        ///</summary>
        public void Quit()
        {
            _logger.LogTestAction(LogMessages.MethodExecution());
            _seleniumDriver.Quit();
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
            _seleniumDriver.SwitchTo().Window(_seleniumDriver.WindowHandles[_seleniumDriver.WindowHandles.Count - 1]);
            return GoToUrl(url);
        }

        ///<summary>
        ///Return the name of current page
        ///</summary>
        public string GetPageTitle()
        {
            _logger.LogTestAction(LogMessages.MethodExecution());
            return _seleniumDriver.Title;
        }

        ///<summary>
        ///Wait and search element for set time. Default is 60 seconds
        ///</summary>
        public IWebElement FindElement(By elementLocator, int mSecondsToWait = 5000, int newImplicitWait = 2)
        {
            var message = string.Empty;
            _logger.LogTestAction(LogMessages.MethodExecution(additionalData: $"Locator: {elementLocator.Criteria} MSeconds to wait: {mSecondsToWait}"));

            _seleniumDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(newImplicitWait);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (true)
            {
                if (stopwatch.ElapsedMilliseconds > mSecondsToWait)
                {
                    var msg = $"It is impossible to find element by locator [{elementLocator}]. Latest exception message: {message}";
                    _logger.LogError(LogMessages.MethodExecution($"Method throws exception: {msg}"));
                    throw UIAMessages.GetException(msg);
                }
                    
                try
                {
                    var element = _seleniumDriver.FindElement(elementLocator);
                    _logger.MakeLogScreenshoot(_seleniumDriver, element);
                    _seleniumDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_runSettings.ImplicitWait);
                    return element;
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
        }

        ///<summary>
        ///Wait and search for element. Default is 60 seconds
        ///</summary>
        public ReadOnlyCollection<IWebElement> FindElements(By elementLocator, int mSecondsToWait = 5000, int newImplicitWait = 2)
        {
            var message = string.Empty;
            _logger.LogTestAction(LogMessages.MethodExecution(additionalData: $"Locator: {elementLocator.Criteria} MSeconds to wait: {mSecondsToWait}"));

            _seleniumDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(newImplicitWait);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (true)
            {
                if (stopwatch.ElapsedMilliseconds > mSecondsToWait)
                {
                    var msg = $"It is impossible to find element by locator [{elementLocator}]. Latest exception message: {message}";
                    _logger.LogError(LogMessages.MethodExecution($"Method throws exception: {msg}"));
                    throw UIAMessages.GetException(msg);
                }

                try
                {
                    var element = _seleniumDriver.FindElements(elementLocator);
                    _logger.MakeLogScreenshoot(_seleniumDriver);
                    _seleniumDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_runSettings.ImplicitWait);
                    return element;
                }
                catch (Exception e)
                {
                    message = e.Message;
                }
            }
        }

        ///<summary>
        ///Search and send text into element
        ///</summary>
        public void SendKeys(By elementLocator, string textToSend, int mSecondsToWait = 5000)
        {
            _logger.LogTestAction(LogMessages.MethodExecution(additionalData: $"Locator: {elementLocator.Criteria}"));
            var element = FindElement(elementLocator, mSecondsToWait);
            element.Clear();
            element.SendKeys(textToSend);
        }

        ///<summary>
        ///Search and click on element
        ///</summary>
        public void ClickOnElement(By elementLocator, int mSecondsToWait = 5000)
        {
            _logger.LogTestAction(LogMessages.MethodExecution(additionalData: $"Locator: {elementLocator.Criteria} MSeconds to wait: {mSecondsToWait}"));
            IWebElement element = FindElement(elementLocator, mSecondsToWait);
            _logger.LogTestAction(LogMessages.MethodExecution(additionalData: $"Element found"), element: element);
            element.Click();
        }

        ///<summary>
        ///Returns TRUE if element exists in DOM otherwise FALSE.
        ///</summary>
        public bool IsElementExistInDOM(By elementLocator, int mSecondsToWait = 5000)
        {
            _logger.LogTestAction(LogMessages.MethodExecution(additionalData: $"Locator: {elementLocator.Criteria}"));
            var result = false;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (true)
            {
                if (stopwatch.ElapsedMilliseconds > mSecondsToWait)
                {
                    return result;
                }

                try
                {
                    var element = _seleniumDriver.FindElement(elementLocator);
                    result = true;
                    return result;
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        ///<summary>
        ///Scrool the browser to elements coordinats
        ///</summary>
        public IWebElement ScrollToElement(By locator)
        {
            _logger.LogTestAction(LogMessages.MethodExecution(additionalData: $"Locator: {locator.Criteria}"));
            if (IsElementExistInDOM(locator))
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)_seleniumDriver;
                var el = FindElement(locator);
                js.ExecuteScript(JSCommands.MoveToElement, el);

                return el;
            }
            else
            {
                var msg = $"Can not scroll to element. It doesn't exist in DOM. Locator: {locator}";
                _logger.LogError(LogMessages.MethodExecution(additionalData: $"Method throws exception: {msg}"));
                throw UIAMessages.GetException(msg);
            }
        }

        ///<summary>
        ///Scrool the browser until element is visible
        ///</summary>
        public IWebElement ScrollToElement(IWebElement element)
        {
            _logger.LogTestAction(LogMessages.MethodExecution(additionalData: $"IWebElement"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)_seleniumDriver;
            js.ExecuteScript(JSCommands.MoveToElement, element);

            return element;
        }

        ///<summary>
        ///Hover the cursor over an element
        ///</summary>
        public void HoverElement(By locator)
        {
            _logger.LogTestAction(LogMessages.MethodExecution(additionalData: $"Locator: {locator.Criteria}"));
            if (IsElementExistInDOM(locator))
            {
                var actions = new Actions(_seleniumDriver);
                actions.MoveToElement(FindElement(locator)).Perform();
            }
            else
            {
                var msg = $"Element doesn't exist in DOM. Locator: {locator}";
                _logger.LogError(LogMessages.MethodExecution(additionalData: $"Method throws exception: {msg}"));
                throw UIAMessages.GetException(msg);
            }

        }

        ///<summary>
        ///Returns TRUE if iframe exists in DOM and driver was switched - otherwise FALSE.
        ///</summary>
        public bool SwitchToIFrame(By frameLocator)
        {
            _logger.LogTestAction(LogMessages.MethodExecution(additionalData: $"Locator: {frameLocator.Criteria}"));
            var result = false;

            if (IsElementExistInDOM(frameLocator))
            {
                _seleniumDriver.SwitchTo().Frame(FindElement(frameLocator));
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
            _seleniumDriver.SwitchTo().DefaultContent();
            _seleniumDriver.SwitchTo().ActiveElement();
        }
    }
}
