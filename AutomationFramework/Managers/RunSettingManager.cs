using NUnit.Framework;
using System;
using System.Collections.Concurrent;
using System.Configuration;

namespace AutomationFramework.Managers
{
    /// <summary>
    /// Class <c>RunSettingManager</c> read all properties from current .runsettings file and provide access to them.
    /// </summary>
    public class RunSettingManager
    {
        public string InstanceUrl { get; set; }
        public string ApiInstanceUrl { get; set; }
        public string BrowserDriversPath { get; set; }
        public string Browser { get; set; }
        public string StepRecordingEnabled { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string RunId { get; set; }
        public string TestsReportDirectory { get; set; }
        public string TestReportDirectory { get; set; }
        public string TestsAssetDirectory { get; set; }
        public string ApiKey { get; set; }
        public string ApiToken { get; set; }
        public ConcurrentDictionary<string, string> APIHeaders { get; set; }
        public string DBServer { get; set; }
        public string DBName { get; set; }
        public string DBUserId { get; set; }
        public string DBUserPass { get; set; }

        public RunSettingManager()
        {
            InstanceUrl = TryToParseTestContext(nameof(InstanceUrl));
            ApiInstanceUrl = TryToParseTestContext(nameof(ApiInstanceUrl));
            BrowserDriversPath = TryToParseTestContext(nameof(BrowserDriversPath));
            Browser = TryToParseTestContext(nameof(Browser));
            StepRecordingEnabled = TryToParseTestContext(nameof(StepRecordingEnabled));
            Username = TryToParseTestContext(nameof(Username));
            Password = TryToParseTestContext(nameof(Password));
            Email = TryToParseTestContext(nameof(Email));
            RunId = DateTime.UtcNow.ToString("MM-dd-yyyy, hh-mm-ss").Replace("-", "_").Replace(",", "").Replace(" ", "_");
            TestsReportDirectory = $"../../../TestsData/{RunId}/TestsReports";
            TestReportDirectory = string.Empty;
            TestsAssetDirectory = $"../../../TestsData/{RunId}/TestsAssets";
            ApiKey = TryToParseTestContext(nameof(ApiKey));
            ApiToken = TryToParseTestContext(nameof(ApiToken));
            APIHeaders = new ConcurrentDictionary<string, string>();
            DBServer = TryToParseTestContext(nameof(DBServer));
            DBName = TryToParseTestContext(nameof(DBName));
            DBUserId = TryToParseTestContext(nameof(DBUserId));
            DBUserPass = TryToParseTestContext(nameof(DBUserPass));
        }

        private string TryToParseTestContext(string settingName)
        {
            var value = TestContext.Parameters[settingName];

            if (value is null) value = ConfigurationManager.AppSettings[settingName];
            if (value is null) Assert.IsNull($"'{settingName}' setting is not found");

            return value;
        }
    }
}
