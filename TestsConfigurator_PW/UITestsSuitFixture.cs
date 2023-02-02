using AutomationCore.AssertAndErrorMsgs.UI;
using AutomationCore.Enums;
using AutomationCore.Managers;
using Microsoft.Playwright;
using NUnit.Framework;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace TestsConfigurator_PW
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture]
    public class UITestsSuitFixture
    {
        protected RunSettings RunSettings;
        protected IPage Page => testsPages[TestContext.CurrentContext.Test.Name];
        protected IPlaywright Playwright;
        protected IBrowser Browser;
        private ConcurrentDictionary<string, IPage> testsPages;


        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            RunSettings = RunSettings.GetRunSettings;
            testsPages = new ConcurrentDictionary<string, IPage>();
            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        }

        [SetUp]
        public async Task Setup()
        {
            Browser = await InitBrowser();
            var page = await Browser.NewPageAsync();
            lock (this)
            {
                testsPages.TryAdd(TestContext.CurrentContext.Test.Name, page);
            }

            await Page.GotoAsync(RunSettings.InstanceUrl);
            await Assertions.Expect(Page).ToHaveTitleAsync(new Regex("Головна сторінка"));
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
                throw UIAMessages.GetException(msg);
            }
        }
    }
}