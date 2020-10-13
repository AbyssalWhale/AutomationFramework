using NUnit.Framework;
using System.Configuration;

namespace AutomationFramework.Entities
{
    public class RunSettingManager
    {
        public string InstanceUrl { get; set; }
        public string Browser { get; set; }
        public string StepRecordingEnabled { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string TestReportDirectory { get; set; }
        public string TestAssetDirectory { get; set; }
        public string ApiKey { get; set; }
        public string DBServer { get; set; }
        public string DBName { get; set; }
        public string DBUserId { get; set; }
        public string DBUserPass { get; set; }

        public RunSettingManager()
        {
            InstanceUrl = TryToParseTestContext(nameof(InstanceUrl));
            Browser = TryToParseTestContext(nameof(Browser));
            StepRecordingEnabled = TryToParseTestContext(nameof(StepRecordingEnabled));
            Username = TryToParseTestContext(nameof(Username));
            Password = TryToParseTestContext(nameof(Password));
            TestReportDirectory = TryToParseTestContext(nameof(TestReportDirectory));
            TestAssetDirectory = TryToParseTestContext(nameof(TestAssetDirectory));
            ApiKey = TryToParseTestContext(nameof(ApiKey));
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

        ///<summary>
        ///Reset TestReportDirectory and TestAssetDirectory into of the current test to default into RunSettingManager. Use it in [TearDown] 
        ///</summary>
        internal void ResetTestReportAndAssetDirectoriesPathes()
        {
            TestReportDirectory = TestReportDirectory.Replace(TestContext.CurrentContext.Test.Name, string.Empty);
            TestAssetDirectory = TestAssetDirectory.Replace(TestContext.CurrentContext.Test.Name, string.Empty);
        }
    }
}
