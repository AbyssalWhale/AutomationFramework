using AutomationCore.Managers;
using Microsoft.Playwright;
using System.Text.RegularExpressions;

namespace TestsConfigurator_PW.Models.POM
{
    public class HomePage : PageBase
    {
        public HomePage(IPage page) : base(page)
        {
        }

        public async Task<HomePage> Navigate()
        {
            var url = RunSettings.InstanceUrl;
            if (url is not null)
            {
                await Page.GotoAsync(url);
                await Assertions.Expect(Page).ToHaveTitleAsync(new Regex("Головна сторінка"));
            }

            return this;
        }
    }
}
