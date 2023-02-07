using Microsoft.Playwright;

namespace TestsConfigurator_PW.Models.POM
{
    public class AboutUsPage : PageBase
    {
        public AboutUsPage(IPage page) : base(page)
        {
        }

        public override string Title => "Наша Історія";
    }
}
