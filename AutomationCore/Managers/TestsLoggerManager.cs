using AutomationCore.AssertAndErrorMsgs.UI;
using NUnit.Framework;
using OpenQA.Selenium;
using Serilog;
using Serilog.Core;
using Serilog.Formatting.Json;

namespace AutomationCore.Managers
{
    public class TestsLoggerManager
    {
        private const string TestLogFileSuffixAndExtension = "_Log.json";
        private const string TestScreenshootFormat = ".png";
        public readonly string LoggerFullPath;

        private string _screenshootsPath;
        private int _testsCountersForScreshoots;
        private RunSettings _settingsManager;
        private Logger _logger;

        public TestsLoggerManager()
        {
            _settingsManager = RunSettings.Instance;
            _screenshootsPath = $"{_settingsManager.TestsReportDirectory}/{TestContext.CurrentContext.Test.Name}";
            _testsCountersForScreshoots = 0;
            _logger = CreateTestFolderAndLog(out LoggerFullPath);
        }

        public TestsLoggerManager(string managerName)
        {
            _settingsManager = RunSettings.Instance;
            _screenshootsPath = $"{_settingsManager.TestsReportDirectory}/{managerName}";
            _testsCountersForScreshoots = 0;
            _logger = CreateTestFolderAndLog(out LoggerFullPath, managerName);
        }

        private Logger CreateTestFolderAndLog(out string loggerFullPath, string? managerName = null)
        {
            string directory = managerName is null ?
                $"{_settingsManager.TestsReportDirectory}/{TestContext.CurrentContext.Test.Name}" :
                _settingsManager.TestsReportDirectory;
            loggerFullPath = managerName is null ? 
                $"{directory}/{TestContext.CurrentContext.Test.Name}{TestLogFileSuffixAndExtension}" :
                $"{directory}/{managerName}{TestLogFileSuffixAndExtension}";
            Directory.CreateDirectory(directory);
            var result = new LoggerConfiguration().WriteTo.File(new JsonFormatter(), $"{loggerFullPath}").CreateLogger();
            result.Information($"Logger for '{managerName ?? TestContext.CurrentContext.Test.Name}' has been created. Path: {loggerFullPath}");
            TestContext.AddTestAttachment(loggerFullPath);

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
