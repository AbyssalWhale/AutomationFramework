using AutomationCore.AssertAndErrorMsgs.UI;
using NUnit.Framework;
using System.Configuration;

namespace AutomationCore.Managers
{
    public class RunSettingsManager
    {
        private static readonly Lazy<RunSettingsManager> instance = new Lazy<RunSettingsManager>(() => new RunSettingsManager());
        public static RunSettingsManager Instance => instance.Value;

        public bool PublishToZephyr { get; set; }
        public string ZephyrToken { get; set; }
        public string AgentTestsResultsFolder { get; set; }
        public string ReleaseUrl { get; set; }
        public string BuildId { get; set; }
        public string Branch { get; set; }
        public static string InstanceUrl => GetRunSetting(nameof(InstanceUrl));
        public string ApiInstanceUrl { get; set; }
        public string Browser { get; set; }
        public bool IsRemoteWebDriver { get; }
        public bool Headless { get; set; }
        public int ImplicitWait { get; set; }
        public string RunId { get; set; }
        public string TestsReportDirectory { get; set; }
        public string ApiKey { get; set; }
        public string ApiToken { get; set; }
        public string DBServer { get; set; }
        public string DBName { get; set; }
        public string DBUserId { get; set; }
        public string DBUserPass { get; set; }

        private RunSettingsManager()
        {
            PublishToZephyr = TryParse_Bool_RunSetting(nameof(PublishToZephyr));
            ZephyrToken = GetRunSetting(nameof(ZephyrToken));
            AgentTestsResultsFolder = GetRunSetting(nameof(AgentTestsResultsFolder));
            ReleaseUrl = GetRunSetting(nameof(ReleaseUrl));
            BuildId = GetRunSetting(nameof(BuildId));
            Branch = GetRunSetting(nameof(Branch));
            ApiInstanceUrl = GetRunSetting(nameof(ApiInstanceUrl));
            Browser = GetRunSetting(nameof(Browser));
            IsRemoteWebDriver = TryParse_Bool_RunSetting(nameof(IsRemoteWebDriver));
            Headless = TryParse_Bool_RunSetting(nameof(Headless));
            ImplicitWait = TryParse_Int_RunSetting(nameof(ImplicitWait));
            RunId = DateTime.UtcNow.ToString("MM-dd-yyyy, hh-mm-ss").Replace("-", "_").Replace(",", "").Replace(" ", "_");
            TestsReportDirectory = $"../../../TestsResults/{RunId}/";
            ApiKey = GetRunSetting(nameof(ApiKey));
            ApiToken = GetRunSetting(nameof(ApiToken));
            DBServer = GetRunSetting(nameof(DBServer));
            DBName = GetRunSetting(nameof(DBName));
            DBUserId = GetRunSetting(nameof(DBUserId));
            DBUserPass = GetRunSetting(nameof(DBUserPass));
        }

        private int TryParse_Int_RunSetting(string settingName)
        {
            var value = GetRunSetting(settingName);
            return int.TryParse(value, out int result) ? result : 0;
        }

        private bool TryParse_Bool_RunSetting(string settingName)
        {
            var value = GetRunSetting(settingName);
            return bool.TryParse(value, out bool result) ? result : false;
        }

        private static string GetRunSetting(string settingName)
        {
            var value = TestContext.Parameters[settingName];

            if (value is null) value = ConfigurationManager.AppSettings[settingName];
            if (value is null) throw UIAMessages.GetException($"'{settingName}' setting is not found"); 

            return value;
        }

        /// <summary>
        /// Usage of NUnit.Framework.ValuesAttribute can cause issues with creating a folder where a name is test case name. This method provides work around. 
        /// </summary>
        /// <returns>the path to directory with test content for current execution</returns>
        public string Get_TestContent_Name()
        {
            var testDir = $"{TestsReportDirectory}{TestContext.CurrentContext.Test.Name}";
            return testDir.Replace(@"""", "_");
        }
    }
}
