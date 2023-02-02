using Microsoft.Playwright;
using System.Text.RegularExpressions;
using TestsConfigurator_PW;

namespace RegressionTestsPW
{
    public class Tests : UITestsSuitFixture
    {
        [Test]
        public async Task TheUser_CanNavigateTo_AboutUs_Page()
        {
            var getStartedButton = Page.GetByRole(AriaRole.Link, new() { Name = "Більше о нас" });
            await Assertions.Expect(getStartedButton).ToHaveAttributeAsync("href", "pages/aboutus.html");
            await getStartedButton.ClickAsync();
            await Assertions.Expect(Page).ToHaveURLAsync(new Regex(".*aboutus"));
        }

        [Test]
        public async Task TheUser_CanNavigateTo_Teachers_Page()
        {
            var getStartedButton = Page.GetByRole(AriaRole.Link, new() { Name = "Усі викладачі" });
            await Assertions.Expect(getStartedButton).ToHaveAttributeAsync("href", "pages/teachers.html");
            await getStartedButton.ClickAsync();
            await Assertions.Expect(Page).ToHaveURLAsync(new Regex(".*teachers"));
        }
    }
}