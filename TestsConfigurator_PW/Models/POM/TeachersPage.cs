using Microsoft.Playwright;

namespace TestsConfigurator_PW.Models.POM
{
    public class TeachersPage : PageBase
    {
        public TeachersPage(IPage page) : base(page)
        {
        }

        public override string Title => "Наші Викладачі";
    }
}
