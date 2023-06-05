using AutomationCore.Managers;

namespace TestsConfigurator.Models.POM
{
    public abstract class ComponentBase
    {
        public ComponentBase(WebDriver webdriver)
        {
            WebDriver = webdriver;
        }

        protected abstract string Title { get; }

        protected WebDriver WebDriver;

        public abstract bool IsLoaded();
    }
}
