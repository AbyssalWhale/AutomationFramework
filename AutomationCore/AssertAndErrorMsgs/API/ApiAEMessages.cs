using RestSharp;
using System.Net;

namespace AutomationCore.AssertAndErrorMsgs.API
{
    public class ApiAEMessages : AEMessagesBase
    {
        public static string NotExepctedResponseCode(RestResponse response) => $"\n Response code is not matched with expected. \nURL: {response.ResponseUri} \nContent: {response.Content}";
    }
}
