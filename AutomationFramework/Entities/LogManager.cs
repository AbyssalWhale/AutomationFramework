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

namespace AutomationFramework.Entities
{
    /// <summary>Class <c>LogManager</c> allows to log test actions 
    /// </summary>
    public class LogManager
    {
        internal Logger _globalLog { get; private set; }
        internal Logger _testExecutionLocalLogger { get; private set; }
        internal IWebDriver _driver { get; set; }
        RunSettingManager _settingsManager { get; set; }
        int screenshootCounter { get; set; }

        #region CSV
        //private string _csvTestLogPath { get; set; }
        //private string _csvGlobalLogPath { get; set; }
        //private List<string> _allGlobalLogs { get; set; }
        //private List<string> _allTestLogs { get; set; }
        #endregion


        public LogManager(RunSettingManager settingsManager, TestContext testContext)
        {
            _settingsManager = settingsManager;
            CreateTestFoldersAndLog(_settingsManager, testContext);
        }

        ///<summary>
        ///Create the 'TestsExecutionGlobalLog' file for loggin of high level steps of tests execution. Folder where the file is saved can be found in path: .runSettings.TestsReportDirectory
        ///</summary>
        internal void CreateGlobalLog(RunSettingManager settingsManager)
        {
            string globalLogFileName = "TestsExecutionGlobalLog.json";
            

            _globalLog = new LoggerConfiguration().WriteTo.File(new JsonFormatter(), $"{settingsManager.TestsReportDirectory}/{globalLogFileName}").CreateLogger();

            //LogAction(LogLevels.global, $"GlobalTestLog was initialized and {globalLogFileName} was created;");
        }

        ///<summary>
        ///Create the 'TestExecutionLocalLog' file for loggin of steps of a current tests. Folder where the file is saved can be found in path: .runSettings.TestReportDirectory
        ///</summary>
        internal void CreateTestFoldersAndLog(RunSettingManager settingsManager, TestContext testContext)
        {
            _settingsManager = settingsManager;

            screenshootCounter = 0;

            string localLogFileName = $"{testContext.Test.Name}_Log.json";

            Directory.CreateDirectory($"{_settingsManager.TestsReportDirectory}/{testContext.Test.Name}");
            Directory.CreateDirectory($"{_settingsManager.TestsAssetDirectory}/{testContext.Test.Name}");

            _testExecutionLocalLogger = new LoggerConfiguration().WriteTo.File(new JsonFormatter(), $"{_settingsManager.TestsReportDirectory}/{testContext.Test.Name}/{localLogFileName}").CreateLogger();

            //LogAction(LogLevels.local, $"Start execution. TestLog was initialized and {localLogFileName} was created;");
        }

        ///<summary>
        ///Allows to log action and write it into global or test local log file. Test log files can be found in path: .runSettings.TestReportDirectory
        ///</summary>
        public void LogAction(LogLevels logLevel, string message, bool makeScreenshoot = false, IWebElement element = null)
        {
            Assert.IsNotNull(message, $"Empty message can not be written into the '{logLevel}' log");

            Log.Logger = logLevel.Equals(LogLevels.global) ? _globalLog : _testExecutionLocalLogger;
            Log.Information(message);

            if (makeScreenshoot)
            {
                if (element == null) MakeLogScreenshoot(); else MakeLogScreenshoot(element);
            }
        }

        public void MakeLogScreenshoot()
        {
            var path = $"{_settingsManager.TestReportDirectory}/{screenshootCounter}.jpg";
            ((ITakesScreenshot)_driver).GetScreenshot().SaveAsFile(path);
            screenshootCounter++;
        }
        public void MakeLogScreenshoot(IWebElement element)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", element, " border: 3px solid red;");            
            MakeLogScreenshoot();
            js.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", element, "");

            screenshootCounter++;
        }

        ///<summary>
        ///Allows to create log CSV file
        ///</summary>
        internal async void CreateFinalCSVLog(LogLevels logLevel)
        {
            //string path = logLevel.Equals(LogLevels.global) ? _csvGlobalLogPath : _csvTestLogPath;
            //var allLogs = logLevel.Equals(LogLevels.global) ? _allGlobalLogs : _allTestLogs;

            //File.Create(path).Close();

            //var file = new StreamWriter(path, true);

            //foreach (var log in allLogs)
            //{
            //    await file.WriteLineAsync(log);
            //}

            //file.Flush();
            //file.Close();

            //_allTestLogs = new List<string>();
        }
    }
}
