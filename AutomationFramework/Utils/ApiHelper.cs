using AutomationFramework.Entities;
using AutomationFramework.Enums;
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
        private LogManager _logManager;
        public ApiHelper(RunSettingManager runSettingsManger, LogManager logManager)
        {
            _runSettingsManger = runSettingsManger;
            _logManager = logManager;
            _client = new RestClient(runSettingsManger.ApiInstanceUrl);
        }

        public async Task<IRestResponse<T>> RestResponseAsync<T>(string endPoint, Method method, ICollection<KeyValuePair<string, string>> headers = null, IRestObject restObject = null) where T : new()
        {
            string url = $"{endPoint}?key={_runSettingsManger.ApiKey}&token={_runSettingsManger.ApiToken}";

            var request = new RestRequest(url, method);

            _logManager.LogAction(LogLevels.local, $"{method} call will be made for the following url: {_runSettingsManger.ApiInstanceUrl}{endPoint}");

            if (headers != null)
            {
                Parallel.ForEach(headers, header => { request.AddParameter(header.Key, header.Value, ParameterType.UrlSegment); });
            }

            if (restObject != null)
            {
                request.AddJsonBody(restObject);
            }

            return await _client.ExecuteAsync<T>(request);
        }
    }
}
