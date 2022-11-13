using System.Diagnostics;

namespace AutomationCore.Utils
{
    public static class LogMessages
    {
        public static string MethodExecution(string? methodName = null, string? additionalData = null)
        {
            StackTrace stackTrace = new StackTrace();
            var methodBase = stackTrace.GetFrame(1).GetMethod();
            var classToLog = methodBase.DeclaringType.FullName;
            var methodToLog = methodName is null ? methodBase.Name : methodName;

            return $"{classToLog} is executing method '{methodToLog}' {additionalData}";
        }
    }
}
