using Microsoft.Playwright;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace PlaywrightCore
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture]
    public class UITestsSuitFixture
    {
        protected IPage Page => testsPages[TestContext.CurrentContext.Test.Name];

        protected IPlaywright Playwright;
        protected IBrowser Browser;
        private ConcurrentDictionary<string, IPage> testsPages;


        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            testsPages = new ConcurrentDictionary<string, IPage>();
            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        }

        [SetUp]
        public async Task Setup()
        {
            Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions()
            {
                Headless = false,
            });

            var page = await Browser.NewPageAsync();
            lock (this)
            {
                testsPages.TryAdd(TestContext.CurrentContext.Test.Name, page);
            }

            await Page.GotoAsync("https://playwright.dev/dotnet");
            await Assertions.Expect(Page).ToHaveTitleAsync(new Regex("Playwright"));
        }
    }
}