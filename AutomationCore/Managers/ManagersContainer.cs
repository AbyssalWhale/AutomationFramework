﻿using Bogus;

namespace AutomationCore.Managers
{
    public class ManagersContainer
    {
        public RunSettings RunSettings { get; }

        public TestsLogger LogManager { get; }

        public WebDriver? WebDriver { get; }

        public ApiM API { get; }

        public Faker FakeDataGenerator { get; }

        public ManagersContainer(bool initWebDriver = true)
        {
            RunSettings = RunSettings.Instance;
            LogManager = new TestsLogger();
            if (initWebDriver) WebDriver = new WebDriver(LogManager);
            API = new ApiM(LogManager);
            FakeDataGenerator = new Faker();
        }
    }
}
