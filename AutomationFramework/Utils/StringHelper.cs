using NUnit.Framework;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AutomationFramework.Utils
{
    public class StringHelper
    {
        public string GetClassNameAndCurrentLine(string message = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = "")
        {
            string className = filePath.Remove(0, filePath.LastIndexOf(@"\") + 1);

            if (string.IsNullOrEmpty(message))
                return "The '" + className + "' class " + " into " + lineNumber + " line.";
            else
                return "The '" + className + "' class " + " into " + lineNumber + " line: " + message;
        }

        public ConcurrentDictionary<string, string> GetAllClassPropertiesWithValuesAsStrings(object objectForApiCall, bool returnProperties = false)
        {
            Assert.IsNotNull(objectForApiCall, $"Object for api can't not be null. Type: {objectForApiCall.GetType()}");

            ConcurrentDictionary<string, string> result = new ConcurrentDictionary<string, string>();
            var allProperties = objectForApiCall.GetType().GetProperties();

            Parallel.ForEach(allProperties, property => {
                if (property.GetValue(objectForApiCall, null) != null | returnProperties)
                {
                    result.TryAdd(property.Name, property.GetValue(objectForApiCall, null).ToString().ToLower());
                }
            });

            return result;
        }
    }
}
