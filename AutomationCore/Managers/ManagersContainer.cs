using Bogus;

namespace AutomationCore.Managers
{
    public class ManagersContainer
    {
        public RunSettingsManager RunSettings { get; }

        public TestsLoggerManager LogManager { get; }

        public WebDriver? WebDriver { get; }

        public RestApiManager API { get; }

        public Faker FakeDataGenerator { get; }

        public ManagersContainer(bool initWebDriver = true)
        {
            RunSettings = RunSettingsManager.Instance;
            LogManager = new TestsLoggerManager();
            if (initWebDriver) WebDriver = new WebDriver(LogManager);
            API = new RestApiManager(LogManager);
            FakeDataGenerator = new Faker();
        }
    }
}
