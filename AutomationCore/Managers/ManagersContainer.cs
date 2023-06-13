using AutomationCore.Managers.LogManagers;
using Bogus;

namespace AutomationCore.Managers
{
    public class ManagersContainer
    {
        public RunSettingsManager RunSettings { get; }

        public JsonLogManager LogManager { get; }

        public WebDriver? WebDriver { get; }

        public RestApiManager API { get; }

        public Faker FakeDataGenerator { get; }

        public ManagersContainer(bool initWebDriver = true)
        {
            RunSettings = RunSettingsManager.Instance;
            LogManager = new JsonLogManager();
            if (initWebDriver)
            {
                WebDriver = new WebDriver(LogManager);
                LogManager.SetWebDriver(WebDriver);
            } 
            API = new RestApiManager(LogManager);
            FakeDataGenerator = new Faker();
        }
    }
}
