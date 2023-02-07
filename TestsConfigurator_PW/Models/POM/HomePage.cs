using AutomationCore.Managers;
using Microsoft.Playwright;
using System.Text.RegularExpressions;

namespace TestsConfigurator_PW.Models.POM
{
    public class HomePage : PageBase
    {
        private ILocator Button_AboutUs => Page.GetByRole(AriaRole.Link, new() { Name = "Більше о нас" });

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
            await Button_AboutUs.ClickAsync();
            return new AboutUsPage(Page);
        }
    }
}
