using AutomationCore.Enums;
using Microsoft.Playwright;
using NUnit.Framework;
using System.Collections.Concurrent;

namespace AutomationCore_PW.Managers
{
    public class PlaywrightManager
    {
        private AutomationCore.Managers.RunSettings runSettings;
        private IPlaywright? playwright;
        private ConcurrentDictionary<string, IBrowser>? testsBrowsers;
        private ConcurrentDictionary<string, IBrowserContext>? testsContexts;

        public PlaywrightManager(AutomationCore.Managers.RunSettings RunSettings)
        {
            runSettings = RunSettings;
            playwright = Playwright.CreateAsync().Result;
            testsBrowsers = new ConcurrentDictionary<string, IBrowser>();
            testsContexts = new ConcurrentDictionary<string, IBrowserContext>();
        }

        public async Task<IPlaywright> GetPlaywright()
        {
            if (playwright is null)
            {
                playwright = await Playwright.CreateAsync();
            }
            return playwright;
        }

        public async Task<IBrowserContext> GetTestContextPW()
        {   
            if (testsBrowsers.ContainsKey(TestContext.CurrentContext.Test.Name))
            {
                return testsContexts[TestContext.CurrentContext.Test.Name];
            }

            var browser = await InitTestBrowser();
            testsBrowsers.TryAdd(TestContext.CurrentContext.Test.Name, browser);
            var context = await InitTestContext(browser);
            testsContexts.TryAdd(TestContext.CurrentContext.Test.Name, context);
            return context;
        }

        public async void ReleaseTestExecution()
        {
            if (Directory.Exists(runSettings.TestReportDirectory))
            {
                var allFiles = Directory.GetFiles(runSettings.TestReportDirectory);
                Parallel.ForEach(allFiles, file => {
                    TestContext.AddTestAttachment(file);
                });
            }
            
            await testsContexts[TestContext.CurrentContext.Test.Name].CloseAsync();
            await testsBrowsers[TestContext.CurrentContext.Test.Name].CloseAsync();
        }

        private async Task<IBrowser> InitTestBrowser()
        {
            var browser = runSettings.Browser.ToLower();

            if (browser.Equals(Browsers.chrome.ToString()))
            {
                var chrome = await GetPlaywright().Result.Chromium.LaunchAsync(new BrowserTypeLaunchOptions()
                {
                    Args = new[] { "--start-maximized" },
                    Headless = runSettings.Headless,
                }); ;

                return chrome;
            }
            else if (browser.Equals(Browsers.firefox.ToString()))
            {
                var fireFox = await GetPlaywright().Result.Firefox.LaunchAsync(new BrowserTypeLaunchOptions()
                {
                    Headless = runSettings.Headless,
                });

                return fireFox;
            }
            else
            {
                var msg = $"Unknown browser is tried to be initialized: {browser}";
                throw AutomationCore.AssertAndErrorMsgs.AEMessagesBase.GetException(msg);
            }
        }

        private async Task<IBrowserContext> InitTestContext(IBrowser browser)
        {
            var context = await browser.NewContextAsync(new()
            {
                RecordVideoDir = runSettings.TestReportDirectory,
                RecordVideoSize = new RecordVideoSize() { Width = 1200, Height = 700 },
                IsMobile = false,
            });

            return context;
        }
    }
}
