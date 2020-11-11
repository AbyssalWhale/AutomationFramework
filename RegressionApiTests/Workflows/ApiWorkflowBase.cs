using AutomationFramework;
using AutomationFramework.Entities;

namespace RegressionApiTests.Workflows
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
