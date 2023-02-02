using Microsoft.Playwright;

namespace TestsConfigurator_PW.Models.POM
{
    public abstract class PageBase
    {
        protected IPage Page;

        public PageBase(IPage page)
        {
            this.Page = page;
        }
    }
}
