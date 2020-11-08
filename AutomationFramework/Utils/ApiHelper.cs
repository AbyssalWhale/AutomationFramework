using AutomationFramework.Entities;
using AutomationFramework.Models;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<IRestResponse<T>> RestResponseAsync<T>(string endPoint, Method method, ICollection<KeyValuePair<string, string>> headers = null, IRestObject restObject = null) where T : new()
        {
            var request = new RestRequest($"{endPoint}?key={_runSettingsManger.ApiKey}&token={_runSettingsManger.ApiToken}", method);

            var response = await _client.ExecuteAsync<T>(request);

            if (headers != null)
            {
                Parallel.ForEach(headers, header => { request.AddParameter(header.Key, header.Value, ParameterType.UrlSegment); });
            }

            if (restObject != null)
            {
                request.AddJsonBody(restObject);
            }

            return response;
        }
    }
}
