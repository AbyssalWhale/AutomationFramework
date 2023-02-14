namespace AutomationCore.AssertAndErrorMsgs
{
    public class AEMessagesBase
    {
        public static Exception GetException(string message, Exception? innerEx = null)
        {
            var header = "\n------Framework Exception------\n";
            var ex = innerEx is null ? new Exception(header) : new Exception(header, innerEx);
            ex.Data.Add("Error", message);

            return ex;
        }

        public static Exception GetExceptionForNullObject(string objectName, string method)
        {
            var msg = $"{objectName} is null. Can not go on with execution of method: {method}";
            return GetException(msg);
        }
    }
}
