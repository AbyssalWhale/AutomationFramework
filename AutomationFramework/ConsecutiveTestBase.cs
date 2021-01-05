using AutomationFramework.Entities;
using System.IO;

namespace AutomationFramework
{
    public class ConsecutiveTestBase
    {
        protected RunSettingManager _runSettingsSettings;

        public virtual void OneTimeSetUp()
        {
            _runSettingsSettings = new RunSettingManager();

            Directory.CreateDirectory(_runSettingsSettings.TestsReportDirectory);
            Directory.CreateDirectory(_runSettingsSettings.TestsAssetDirectory);


        }
    }
}
