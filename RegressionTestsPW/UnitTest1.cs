using PlaywrightCore;

namespace RegressionTestsPW
{
    public class Tests : UITestsSuitFixture
    {
        [Test]
        public async Task Test1()
        {
            await Page.GotoAsync("https://playwright.dev/dotnet");
            Thread.Sleep(2000);
        }

        [Test]
        public async Task Test2()
        {
            await Page.GotoAsync("https://playwright.dev/dotnet");
            Thread.Sleep(2000);
        }
    }
}