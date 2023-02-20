using AutomationCore.Managers;
using Microsoft.Playwright;
using System.Text.RegularExpressions;

namespace TestsConfigurator_PW.Models.POM
{
    public class HomePage : PageBase
    {
        private ILocator Button_AboutUs => Page.GetByRole(AriaRole.Link, new() { Name = "Більше о нас" });
        private ILocator Button_Teachers => Page.GetByRole(AriaRole.Link, new() { Name = "Усі викладачі" });

        public override string Title => "Головна сторінка";

        public HomePage(IPage page) : base(page)
        {

        }

        public async Task<HomePage> Navigate()
        {
            var url = RunSettings.InstanceUrl;
            if (url is not null)
            {
                await Page.GotoAsync(url);
                await IsAtPage();
            }

            return this;
        }

        public async Task<AboutUsPage> Click_AboutUs_Button()
        {
            await Assertions.Expect(Button_AboutUs).ToHaveAttributeAsync("href", "pages/aboutus.html");
            await Button_AboutUs.ClickAsync();
            return new AboutUsPage(Page);
        }

        public async Task<TeachersPage> Click_Tachers_Button()
        {
            await Assertions.Expect(Button_Teachers).ToHaveAttributeAsync("href", "pages/teachers.html");
            await Button_Teachers.ClickAsync();
            return new TeachersPage(Page);
        }
    }
}
