using RestSharp;

namespace AutomationCore.AssertAndErrorMsgs.API
{
    public static class ApiAEMessages
    {
        public static string NotExepctedResponseCode(RestResponse response) => $"\n Response code is not matched with expected. \nURL: {response.ResponseUri} \nMethod: {response.Request.Method} \nContent: {response.Content}";
    }
}
