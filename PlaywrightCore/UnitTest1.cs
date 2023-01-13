using Microsoft.Playwright;
using System.Collections.Concurrent;

namespace PlaywrightCore
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture]
    public class Tests
    {
        private IPlaywright playwright;
        private IBrowser browser;

        private ConcurrentDictionary<string, IPage> testsPages;

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            testsPages = new ConcurrentDictionary<string, IPage>();
            playwright = await Playwright.CreateAsync();
        }

        [SetUp]
        public async Task Setup()
        {
            browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions()
            {
                Headless = false,
            });

            var page = await browser.NewPageAsync();
            lock (this)
            {
                testsPages.TryAdd(TestContext.CurrentContext.Test.Name, page);
            }
        }

        [Test]
        public async Task Test1()
        {
            var page = testsPages[TestContext.CurrentContext.Test.Name];
            await page.GotoAsync("https://playwright.dev/dotnet");
            Thread.Sleep(2000);
        }

        [Test]
        public async Task Test2()
        {
            var page = testsPages[TestContext.CurrentContext.Test.Name];
            await page.GotoAsync("https://playwright.dev/dotnet");
            Thread.Sleep(2000);
        }
    }
}