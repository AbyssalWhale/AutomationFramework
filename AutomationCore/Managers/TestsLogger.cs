using AutomationCore.AssertAndErrorMsgs.UI;
using NUnit.Framework;
using OpenQA.Selenium;
using Serilog;
using Serilog.Core;
using Serilog.Formatting.Json;

namespace AutomationCore.Managers
{
    public class TestsLogger
    {
        private const string TestLogFileSuffixAndExtension = "_Log.json";
        private const string TestScreenshootFormat = ".png";

        private string _screenshootsPath;
        private int _testsCountersForScreshoots;
        private RunSettings _settingsManager;
        private Logger _logger;

        public TestsLogger()
        {
            _settingsManager = RunSettings.GetRunSettings;
            _screenshootsPath = string.Empty;
            _testsCountersForScreshoots = 0;
            _logger = CreateTestFolderAndLog();
            LogTestAction($"Folder and Log for the '{TestContext.CurrentContext.Test.Name}' test were created;");
        }

        private Logger CreateTestFolderAndLog()
        {
            string localLogFileName = $"{_settingsManager.TestsReportDirectory}/{TestContext.CurrentContext.Test.Name}/{TestContext.CurrentContext.Test.Name}{TestLogFileSuffixAndExtension}";
            _screenshootsPath = $"{_settingsManager.TestsReportDirectory}/{ TestContext.CurrentContext.Test.Name}";
            Directory.CreateDirectory($"{_settingsManager.TestsReportDirectory}/{TestContext.CurrentContext.Test.Name}");

            var result = new LoggerConfiguration().WriteTo.File(new JsonFormatter(), $"{localLogFileName}").CreateLogger();
            TestContext.AddTestAttachment(localLogFileName);

            return result;
        }

        public void LogTestAction(string message, bool makeScreenshoot = false, WebDriver? driver = null, IWebElement? element = null)
        {
            _logger.Information($"{message}");

            if (makeScreenshoot)
            {
                LogScreenShoot(driver, element);
            }
        }

        public void LogError(string message, bool makeScreenshoot = false, WebDriver? driver = null, IWebElement? element = null)
        {
            _logger.Error($"{message}");

            if (makeScreenshoot)
            {
                LogScreenShoot(driver, element);
            }
        }

        public void MakeLogScreenshoot(IWebDriver driver)
        {
            if (driver is null)
            {
                throw UIAMessages.GetException("Screenshoot can not be made with null IWebDriver");
            }

            var path = $"{_screenshootsPath}/{_testsCountersForScreshoots}{TestScreenshootFormat}";
            var screenShoot = ((ITakesScreenshot)driver).GetScreenshot();
            screenShoot.SaveAsFile(path);
            TestContext.AddTestAttachment(path);
            _testsCountersForScreshoots++;
        }

        public void MakeLogScreenshoot(IWebDriver driver, IWebElement element)
        {
            if (element is null)
            {
                throw UIAMessages.GetException("Screenshoot can not be made with null IWebElement"); ;
            }

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", element, " border: 3px solid red;");
            MakeLogScreenshoot(driver);
            js.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", element, "");
        }
    
        private void LogScreenShoot(WebDriver? driver = null, IWebElement? element = null)
        {
            if (driver is null)
            {
                throw UIAMessages.GetException($"WebDriver can not be null for invoking the 'LogScreenShoot' method. \n Test name: {TestContext.CurrentContext.Test.Name}");
            }

            if (element == null)
            {
                MakeLogScreenshoot(driver._seleniumDriver);
            } else
            {
                MakeLogScreenshoot(driver._seleniumDriver, element);
            }
        }
    }
}
