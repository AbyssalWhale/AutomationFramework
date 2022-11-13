using AutomationCore.Managers;

namespace TestsConfigurator.Models.POM
{
    public abstract class PagesBase
    {
        protected ManagersContainer ManagersContainer;

        public PagesBase(ManagersContainer managersContainer)
        {
            ManagersContainer = managersContainer;
        }
    }
}
