using NUnit.Framework;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace AutomationCore.Utils
{
    public static class UString
    {
        public static string GetClassNameAndCurrentLine(string? message = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "")
        {
            string className = filePath.Remove(0, filePath.LastIndexOf(@"\") + 1);

            if (string.IsNullOrEmpty(message))
                return "The '" + className + "' class " + " into " + lineNumber + " line.";
            else
                return "The '" + className + "' class " + " into " + lineNumber + " line: " + message;
        }

        public static ConcurrentDictionary<string, string> GetAllClassPropertiesWithValuesAsStrings(object objectForApiCall, bool returnProperties = false)
        {
            Assert.IsNotNull(objectForApiCall, $"Object for api can't not be null. Type: {objectForApiCall.GetType()}");

            ConcurrentDictionary<string, string> result = new ConcurrentDictionary<string, string>();
            var allProperties = objectForApiCall.GetType().GetProperties();

            Parallel.ForEach(allProperties, property => {
                if (property.GetValue(objectForApiCall, null) != null | returnProperties)
                {
                    var value = property.GetValue(objectForApiCall, null) as string;
                    value = value is null ? string.Empty : value.ToString().ToLower();
                    result.TryAdd(property.Name, value);
                }
            });

            return result;
        }
    }
}
