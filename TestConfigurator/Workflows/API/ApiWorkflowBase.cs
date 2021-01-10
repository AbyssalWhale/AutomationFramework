using AutomationFramework.Entities;

namespace TestConfigurator.Workflows.API
{
    public class ApiWorkflowBase
    {
        internal ToolsManager _toolsManager;
        public ApiWorkflowBase(ToolsManager toolsManager)
        {
            _toolsManager = toolsManager;
        }
    }
}
