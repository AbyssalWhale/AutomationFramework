using NUnit.Framework;
using OpenQA.Selenium;
using Serilog;
using Serilog.Core;
using Serilog.Formatting.Json;

namespace AutomationCore.Managers
{
    /// <summary>Class <c>LogManager</c> allows to log test actions 
    /// </summary>
    public class Logger
    {
        private const string TestLogFileSuffixAndExtension = "_Log.json";
        private const string TestScreenshootFormat = ".jpg";

        private Serilog.Core.Logger _logger;

        private string _screenshootsPath;
        private int _testsCountersForScreshoots;
        private RunSettings _settingsManager;

        public Logger()
        {
            _settingsManager = RunSettings.GetRunSettings;
            _screenshootsPath = string.Empty;
            _testsCountersForScreshoots = 0;
            _logger = CreateTestFolderAndLog();

            LogTestAction($"Folder and Log for the '{TestContext.CurrentContext.Test.Name}' test were created;");
        }

        ///<summary>
        ///Create a log file for loggin of steps of a current test execution. Folder where the file is saved can be found in path: .runSettings.TestReportDirectory
        ///</summary>
        private Serilog.Core.Logger CreateTestFolderAndLog()
        {
            string localLogFileName = $"{_settingsManager.TestsReportDirectory}/{TestContext.CurrentContext.Test.Name}/{TestContext.CurrentContext.Test.Name}{TestLogFileSuffixAndExtension}";
            _screenshootsPath = $"{_settingsManager.TestsReportDirectory}/{ TestContext.CurrentContext.Test.Name}";

            Directory.CreateDirectory($"{_settingsManager.TestsReportDirectory}/{TestContext.CurrentContext.Test.Name}");
            //todo: Move to WebDriver
            //Directory.CreateDirectory($"{_settingsManager.TestsAssetDirectory}/{TestContext.CurrentContext.Test.Name}");
            var result = new LoggerConfiguration().WriteTo.File(new JsonFormatter(), $"{localLogFileName}").CreateLogger();
            TestContext.AddTestAttachment(localLogFileName);

            return result;
        }

        ///<summary>
        ///Allows to log action and write it into test local log file. Test log files can be found in path: .runSettings.TestReportDirectory
        ///</summary>
        public void LogTestAction(string message,bool makeScreenshoot = false, WebDriver? driver = null, IWebElement? element = null)
        {
            Assert.IsNotNull(message, $"Empty message can not be written into the log");

            _logger.Information($"{message}");

            if (makeScreenshoot)
            {
                if (driver is null)
                {
                    throw new Exception("Screenshoot can not be made with null WebDriver");
                }

                if (element == null) MakeLogScreenshoot(driver._seleniumDriver); else MakeLogScreenshoot(driver._seleniumDriver, element);
            }
        }

        ///<summary>
        ///Allows to log action and write it into test local log file. Test log files can be found in path: .runSettings.TestReportDirectory
        ///</summary>
        public void LogError(string message, bool makeScreenshoot = false, IWebElement? element = null)
        {
            Assert.IsNotNull(message, $"Empty message can not be written into the log");

            _logger.Error($"{message}");

            if (makeScreenshoot)
            {
                //if (element == null) MakeLogScreenshoot(); else MakeLogScreenshoot(element);
            }
        }

        ///<summary>
        ///Allows to make and save screenshoot in a test report directory. Pass IWebElement to make screenshoot with highlighted element. 
        ///</summary>
        public void MakeLogScreenshoot(IWebDriver driver)
        {
            var path = $"{_screenshootsPath}/{_testsCountersForScreshoots}{TestScreenshootFormat}";
            var screenShoot = ((ITakesScreenshot)driver).GetScreenshot();
            screenShoot.SaveAsFile(path);
            TestContext.AddTestAttachment(path);
            _testsCountersForScreshoots++;
        }

        /////<summary>
        /////Allows to highlight WebElement, make and save screenshoot in a test report directory. Pass IWebElement to make screenshoot with highlighted element. 
        /////</summary>
        public void MakeLogScreenshoot(IWebDriver driver, IWebElement element)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", element, " border: 3px solid red;");
            MakeLogScreenshoot(driver);
            js.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", element, "");
        }
    }
}
