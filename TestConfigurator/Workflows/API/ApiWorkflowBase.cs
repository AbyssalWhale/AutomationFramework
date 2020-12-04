using AutomationFramework.Entities;

namespace TestConfigurator.Workflows.API
{
    public class ApiWorkflowBase
    {
        internal UtilsManager _utilsManager;
        public ApiWorkflowBase(UtilsManager utilsManager)
        {
            _utilsManager = utilsManager;
        }
    }
}
