using NUnit.Framework;
using OpenQA.Selenium;
using Serilog;
using Serilog.Core;
using Serilog.Formatting.Json;

namespace AutomationCore.Managers.LogManagers
{
    public class JsonLogManager
    {
        private const string TestExecutionLogName = "TestExecutionLog.json";
        public readonly string LoggerFullPath;

        private string _managerName;
        private RunSettingsManager _settingsManager;
        private Logger _logger;
        private WebDriver? _driver;
        private ScreenShootManager? _screenShootManager;

        public JsonLogManager(string? managerName = null, WebDriver? driver = null)
        {
            _settingsManager = RunSettingsManager.Instance;
            _managerName = managerName ?? string.Empty;
            _logger = CreateTestFolderAndLog(out LoggerFullPath, _managerName);

            if (driver is not null)
            {
                _driver = driver;
                _screenShootManager = new ScreenShootManager(_driver._seleniumDriver, _managerName);
            }
        }

        public void SetWebDriver(WebDriver driver)
        {
            if (_driver is not null)
            {
                throw AssertAndErrorMsgs.AEMessagesBase.GetException($"WebDriver has been already initialized in {nameof(JsonLogManager)}");
            }

            _driver = driver;
            _screenShootManager = new ScreenShootManager(_driver._seleniumDriver, _managerName);
        }

        public void LogInfo(string message, bool makeScreenshoot = false, IWebElement? element = null)
        {
            _logger.Information($"{message}");

            if (makeScreenshoot) MakeScreenShoot(element);
        }

        public void LogError(string message, bool makeScreenshoot = false, IWebElement? element = null)
        {
            _logger.Error($"{message}");

            if (makeScreenshoot) MakeScreenShoot(element);
        }

        public Screenshot MakeScreenShoot(IWebElement? element = null)
        {
            if (_driver is null)
            {
                throw AssertAndErrorMsgs.AEMessagesBase.GetException($"Screenshoot can not be made with null WebDriver Make sure WebDriver has been pased during init {nameof(JsonLogManager)}");
            }
            return _screenShootManager.MakeScreenshoot(element);
        }

        private Logger CreateTestFolderAndLog(out string loggerFullPath, string? managerName = null)
        {
            string directory = string.IsNullOrEmpty(managerName) ?
                $"{_settingsManager.Get_TestContent_Name()}" :
                _settingsManager.TestsReportDirectory;
            loggerFullPath = string.IsNullOrEmpty(managerName) ?
                $"{directory}/{TestExecutionLogName}" :
                $"{directory}/{managerName}{TestExecutionLogName}";

            Directory.CreateDirectory(directory);
            var result = new LoggerConfiguration().WriteTo.File(new JsonFormatter(), $"{loggerFullPath}").CreateLogger();
            result.Information($"Logger for '{managerName ?? TestContext.CurrentContext.Test.Name}' has been created. Path: {loggerFullPath}");
            TestContext.AddTestAttachment(loggerFullPath);

            return result;
        }
    }
}
