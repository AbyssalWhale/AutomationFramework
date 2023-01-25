using Microsoft.Playwright;
using PlaywrightCore;
using System.Text.RegularExpressions;

namespace RegressionTestsPW
{
    public class Tests : UITestsSuitFixture
    {
        [Test]
        public async Task Test1()
        {
            //var getStartedButton = Page.GetByRole(AriaRole.Link, new() { Name = "Get started" });
            //await Assertions.Expect(getStartedButton).ToHaveAttributeAsync("href", "/dotnet/docs/intro");
            //await getStartedButton.ClickAsync();
            //await Assertions.Expect(Page).ToHaveURLAsync(new Regex(".*intro"));
        }

        [Test]
        public async Task Test2()
        {
            //var getStartedButton = Page.GetByRole(AriaRole.Link, new() { Name = "Get started" });
            //await Assertions.Expect(getStartedButton).ToHaveAttributeAsync("href", "/dotnet/docs/intro");
            //await getStartedButton.ClickAsync();
            //await Assertions.Expect(Page).ToHaveURLAsync(new Regex(".*intro"));
        }
    }
}