using NUnit.Framework;
using OpenQA.Selenium;
using Serilog;
using Serilog.Core;
using Serilog.Formatting.Json;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AutomationFramework.Entities
{
    /// <summary>Class <c>LogManager</c> allows to log test actions 
    /// </summary>
    public class LogManager
    {
        const string GlobalLogName = "GlobalLog.json";
        const string TestLogFileSuffixAndExtension = "_Log.json";

        ConcurrentDictionary<string, Logger> _allTestsLoger;
        ConcurrentDictionary<string, int> _testsCountersForScreshoots;
        static LogManager _logManager;
        IWebDriver _driver { get { return WebDriverManager.GetWebDriverManager(_settingsManager)._driver; } }
        RunSettingManager _settingsManager { get; set; }

        #region CSV
        private string _csvTestLogPath { get; set; }
        private string _csvGlobalLogPath { get; set; }
        private List<string> _allGlobalLogs { get; set; }
        private List<string> _allTestLogs { get; set; }
        #endregion

        protected LogManager(RunSettingManager settingsManager)
        {
            _settingsManager = settingsManager;
            _allTestsLoger = new ConcurrentDictionary<string, Logger>();
            _testsCountersForScreshoots = new ConcurrentDictionary<string, int>();
        }

        internal static LogManager GetLogManager(RunSettingManager settingsManager)
        {
            if (_logManager == null)
            {
                _logManager = new LogManager(settingsManager);
            }

            return _logManager;
        }

        ///<summary>
        ///Create global log for tests run
        ///</summary>
        internal void CreateGlobalLog()
        {
            string logFileNameWithPath = $"{_settingsManager.TestsReportDirectory}/{GlobalLogName}";
            Directory.CreateDirectory($"{_settingsManager.TestsReportDirectory}");

            _allTestsLoger.TryAdd(GlobalLogName, new LoggerConfiguration().WriteTo.File(new JsonFormatter(), $"{logFileNameWithPath}").CreateLogger());

            LogGlobalTestExecutionAction("Global log was created;");
        }

        ///<summary>
        ///Create a log file for loggin of steps of a current test execution. Folder where the file is saved can be found in path: .runSettings.TestReportDirectory
        ///</summary>
        internal void CreateTestFolderAndLog(TestContext testContext)
        {
            string localLogFileName = $"{_settingsManager.TestsReportDirectory}/{testContext.Test.Name}/{testContext.Test.Name}{TestLogFileSuffixAndExtension}";

            Directory.CreateDirectory($"{_settingsManager.TestsReportDirectory}/{testContext.Test.Name}");
            Directory.CreateDirectory($"{_settingsManager.TestsAssetDirectory}/{testContext.Test.Name}");

            _allTestsLoger.TryAdd(TestContext.CurrentContext.Test.Name, new LoggerConfiguration().WriteTo.File(new JsonFormatter(), $"{localLogFileName}").CreateLogger());
            _testsCountersForScreshoots.TryAdd(TestContext.CurrentContext.Test.Name, 0);

            TestContext.AddTestAttachment(localLogFileName);

            LogTestAction($"Folder and Log for the '{testContext.Test.Name}' test were created;");
            LogGlobalTestExecutionAction($"The {testContext.Test.Name} test started execution;");
        }

        ///<summary>
        ///Allows to log action and write it into test local log file. Test log files can be found in path: .runSettings.TestReportDirectory
        ///</summary>
        public void LogTestAction(string message, bool makeScreenshoot = false, IWebElement element = null)
        {
            Assert.IsNotNull(message, $"Empty message can not be written into the log");

            var record = new { Test = $"{TestContext.CurrentContext.Test.Name}", Message = $"{message}" };

            _allTestsLoger[TestContext.CurrentContext.Test.Name].Information($"{record}");

            if (makeScreenshoot)
            {
                if (element == null) MakeLogScreenshoot(); else MakeLogScreenshoot(element);
            }
        }

        ///<summary>
        ///Allows to log action and write it into test global test run log file. Test log files can be found in path: .runSettings.TestReportDirectory
        ///</summary>
        public async void LogGlobalTestExecutionAction(string message)
        {
            Assert.IsNotNull(message, $"Empty message can not be written into the log");
            await Task.Run(() => { _allTestsLoger[GlobalLogName].Information($"{message}"); });
        }

        ///<summary>
        ///Allows to make and save screenshoot in a test report directory. Pass IWebElement to make screenshoot with highlighted element. 
        ///</summary>
        public void MakeLogScreenshoot()
        {
            var path = $"{_settingsManager.TestsReportDirectory}/{TestContext.CurrentContext.Test.Name}/{_testsCountersForScreshoots[TestContext.CurrentContext.Test.Name]}.jpg";
            var screenShoot = ((ITakesScreenshot)_driver).GetScreenshot();
            screenShoot.SaveAsFile(path);
            TestContext.AddTestAttachment(path, $"Screenshoot {_testsCountersForScreshoots[TestContext.CurrentContext.Test.Name]}");

            _testsCountersForScreshoots[TestContext.CurrentContext.Test.Name]++;
        }

        ///<summary>
        ///Allows to highlight WebElement, make and save screenshoot in a test report directory. Pass IWebElement to make screenshoot with highlighted element. 
        ///</summary>
        public void MakeLogScreenshoot(IWebElement element)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            WebDriverManager.GetWebDriverManager(_settingsManager).MoveToElement(element);
            js.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", element, " border: 3px solid red;");            
            MakeLogScreenshoot();
            js.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", element, "");
        }

        ///<summary>
        ///Allows to create log CSV file
        ///</summary>
        
        internal async void CreateFinalCSVLog()
        {
            File.Create(_csvTestLogPath).Close();

            await Task.Run(() => {

                var file = new StreamWriter(_csvTestLogPath, true);

                foreach (var log in _allTestLogs)
                {
                    file.WriteLineAsync(log);
                }

                file.Flush();
                file.Close();

                _allTestLogs = new List<string>();
            });
        }
    }
}
