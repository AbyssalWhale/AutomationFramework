using AutomationFramework.Enums;
using AutomationFramework.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using Serilog;
using Serilog.Core;
using Serilog.Formatting.Json;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace AutomationFramework.Entities
{
    /// <summary>Class <c>LogManager</c> allows to log test actions 
    /// </summary>
    public class LogManager
    {
        private static LogManager _logManager;
        internal TestContext _currentTestContext;
        internal Logger _testExecutionLocalLogger { get; private set; }
        internal IWebDriver _driver { get; set; }
        RunSettingManager _settingsManager { get; set; }
        int screenshootCounter { get; set; }

        #region CSV
        private string _csvTestLogPath { get; set; }
        private string _csvGlobalLogPath { get; set; }
        private List<string> _allGlobalLogs { get; set; }
        private List<string> _allTestLogs { get; set; }
        #endregion

        protected LogManager(RunSettingManager settingsManager, TestContext testContext)
        {
            _settingsManager = settingsManager;
            _currentTestContext = testContext;
            CreateTestFoldersAndLog(_settingsManager);
        }

        internal static LogManager GetLogManager(RunSettingManager settingsManager, TestContext testContext)
        {
            if (_logManager == null)
            {
                _logManager = new LogManager(settingsManager, testContext);
            }

            return _logManager;
        }

        ///<summary>
        ///Create the 'TestExecutionLocalLog' file for loggin of steps of a current tests. Folder where the file is saved can be found in path: .runSettings.TestReportDirectory
        ///</summary>
        internal async void CreateTestFoldersAndLog(RunSettingManager settingsManager)
        {
            _settingsManager = settingsManager;

            screenshootCounter = 0;

            string localLogFileName = $"{_currentTestContext.Test.Name}_Log.json";

            Directory.CreateDirectory($"{_settingsManager.TestsReportDirectory}/{_currentTestContext.Test.Name}");
            Directory.CreateDirectory($"{_settingsManager.TestsAssetDirectory}/{_currentTestContext.Test.Name}");

            _testExecutionLocalLogger =  new LoggerConfiguration().WriteTo.File(new JsonFormatter(), $"{_settingsManager.TestsReportDirectory}/{_currentTestContext.Test.Name}/{localLogFileName}").CreateLogger();

            await Task.Run(() => LogAction($"Start execution. Folder and Log for the '{_currentTestContext.Test.Name}' test were created;"));
        }

        ///<summary>
        ///Allows to log action and write it into global or test local log file. Test log files can be found in path: .runSettings.TestReportDirectory
        ///</summary>
        public async void LogAction(string message, bool makeScreenshoot = false, IWebElement element = null)
        {
            Assert.IsNotNull(message, $"Empty message can not be written into the log");

            await Task.Run(() => _testExecutionLocalLogger.Information(message));

            if (makeScreenshoot)
            {
                if (element == null) MakeLogScreenshoot(); else MakeLogScreenshoot(element);
            }
        }

        public async void MakeLogScreenshoot()
        {
            var path = $"{_settingsManager.TestsReportDirectory}/{_currentTestContext.Test.Name}/{screenshootCounter}.jpg";
            var screenShoot = ((ITakesScreenshot)_driver).GetScreenshot();
            await Task.Run(() => screenShoot.SaveAsFile(path));
            screenshootCounter++;
        }

        public void MakeLogScreenshoot(IWebElement element)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", element, " border: 3px solid red;");            
            MakeLogScreenshoot();
            js.ExecuteAsyncScript("arguments[0].setAttribute('style', arguments[1]);", element, "");
            screenshootCounter++;
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
