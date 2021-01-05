using AutomationFramework.Entities;

namespace TestConfigurator.Workflows.API
{
    public class ApiWorkflowBase
    {
        internal ToolsManager _utilsManager;
        public ApiWorkflowBase(ToolsManager utilsManager)
        {
            _utilsManager = utilsManager;
        }
    }
}
