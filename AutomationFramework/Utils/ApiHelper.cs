using AutomationFramework.Entities;
using AutomationFramework.Models;
using RestSharp;
using System.Collections.Generic;

namespace AutomationFramework.Utils
{
    public class ApiHelper
    {
        internal IRestClient _client;
        private RunSettingManager _runSettingsManger;
        public ApiHelper(RunSettingManager runSettingsManger)
        {
            _runSettingsManger = runSettingsManger;
            _client = new RestClient(runSettingsManger.ApiInstanceUrl);
        }

        public IRestResponse<T> RestResponse<T>(string endPoint, Method method, ICollection<KeyValuePair<string, string>> headers = null, IRestObject restObject = null) where T : new()
        {
            var request = new RestRequest($"{endPoint}?key={_runSettingsManger.ApiKey}&token={_runSettingsManger.ApiToken}", method);

            if (headers != null)
            {
                request.AddHeaders(headers);
            }

            if (restObject != null)
            {
                request.AddJsonBody(restObject);
            }

            return _client.Execute<T>(request);
        }
    }
}
