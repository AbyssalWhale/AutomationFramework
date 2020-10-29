using System.Runtime.CompilerServices;

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
    }
}
