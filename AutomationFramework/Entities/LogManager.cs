using AutomationFramework.Enums;
using NUnit.Framework;
using Serilog;
using Serilog.Core;
using Serilog.Formatting.Json;

namespace AutomationFramework.Entities
{
    /// <summary>Class <c>LogManager</c> allows to log test actions 
    /// </summary>
    public class LogManager
    {
        internal Logger _globalLog { get; set; }
        internal Logger _testExecutionLocalLogger { get; set; }

        ///<summary>
        ///Create the 'TestsExecutionGlobalLog' file for loggin of high level steps of tests execution. Folder where the file is saved can be found in path: .runSettings.TestsReportDirectory
        ///</summary>
        internal void CreateGlobalLog(RunSettingManager settingsManager)
        {
            string globalLogFileName = "TestsExecutionGlobalLog.json";

            _globalLog = new LoggerConfiguration().WriteTo.File(new JsonFormatter(), $"{settingsManager.TestsReportDirectory}/{globalLogFileName}").CreateLogger();
            LogAction(LogLevels.global, $"GlobalTestLog was initialized and {globalLogFileName} was created;");
        }

        ///<summary>
        ///Create the 'TestExecutionLocalLog' file for loggin of steps of a current tests. Folder where the file is saved can be found in path: .runSettings.TestReportDirectory
        ///</summary>
        internal void CreateLogForTest(RunSettingManager settingsManager, TestContext testContext)
        {
            string localLogFileName = "TestExecutionLocalLog.json";

            _testExecutionLocalLogger = new LoggerConfiguration().WriteTo.File(new JsonFormatter(), $"{settingsManager.TestReportDirectory}/{localLogFileName}").CreateLogger();
            LogAction(LogLevels.local, $"Start execution. TestLog was initialized and {localLogFileName} was created;");
        }

        ///<summary>
        ///Allows to log action and write it into global or test local log file. Test log files can be found in path: .runSettings.TestReportDirectory
        ///</summary>
        public void LogAction(LogLevels logLevel, string message)
        {
            Assert.IsNotNull(message, $"Empty message can not be written into the '{logLevel}' log");

            Log.Logger = logLevel.Equals(LogLevels.global) ? _globalLog : _testExecutionLocalLogger;
            Log.Information(message);
        }
    }
}
