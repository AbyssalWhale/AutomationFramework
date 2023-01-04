using System.Diagnostics;

namespace AutomationCore.Utils
{
    public static class LogMessages
    {
        public static string MethodExecution(string? methodName = null, string? additionalData = null)
        {
            StackTrace stackTrace = new StackTrace();
            var frame = stackTrace.GetFrame(1);
            if (frame is null)
            {
                return string.Empty;
            }

            var methodBase = frame.GetMethod();
            if (methodBase is null)
            {
                return string.Empty;
            }

            var declaringType = methodBase.DeclaringType;
            if (declaringType is null)
            {
                return string.Empty;
            }

            var classToLog = declaringType.FullName;
            var methodToLog = methodName is null ? methodBase.Name : methodName;

            return $"{classToLog} is executing method '{methodToLog}' {additionalData}";
        }
    }
}
