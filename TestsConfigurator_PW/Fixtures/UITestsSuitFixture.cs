using AutomationCore.AssertAndErrorMsgs.UI;
using AutomationCore.Enums;
using AutomationCore.Managers;
using Microsoft.Playwright;
using NUnit.Framework;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using TestsConfigurator_PW.Models.POM;

namespace TestsConfigurator_PW.Fixtures
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture]
    public class UITestsSuitFixture
    {
        private ConcurrentDictionary<string, HomePage>? homePages;

        protected RunSettings RunSettings;
        protected IPlaywright Playwright;
        protected IBrowser Browser;
        protected HomePage? HomePage => homePages[TestContext.CurrentContext.Test.Name];
        

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            RunSettings = RunSettings.GetRunSettings;
            homePages = new ConcurrentDictionary<string, HomePage>();
            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        }

        [SetUp]
        public async Task Setup()
        {
            Browser = await InitBrowser();

            var newHomePage = new HomePage(await Browser.NewPageAsync());
            lock (this)
            {
                homePages.TryAdd(TestContext.CurrentContext.Test.Name, newHomePage);
            }

            await HomePage.Navigate();
        }

        private async Task<IBrowser> InitBrowser()
        {
            var browser = RunSettings.Browser.ToLower();

            if (browser.Equals(Browsers.chrome.ToString()))
            {
                return await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions()
                {
                    Headless = false,
                });
            }
            else if (browser.Equals(Browsers.firefox.ToString()))
            {
                return await Playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions()
                {
                    Headless = false,
                });
            }
            else
            {
                var msg = $"Unknown browser is tried to be initialized: {browser}";
                throw AutomationCore.AssertAndErrorMsgs.AEMessagesBase.GetException(msg);
            }
        }
    }
}