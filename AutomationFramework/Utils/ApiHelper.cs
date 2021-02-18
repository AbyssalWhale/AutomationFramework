using AutomationFramework.Managers;
using AutomationFramework.Models;
using RestSharp;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace AutomationFramework.Utils
{
    public class ApiHelper
    {
        internal IRestClient _client;
        private RunSettingManager _runSettingsManger;
        private LogManager _logManager { get { return LogManager.GetLogManager(_runSettingsManger); } }
        private StringHelper _stringHelper;
        public ApiHelper(RunSettingManager runSettingsManger, StringHelper stringHelper)
        {
            _runSettingsManger = runSettingsManger;
            _stringHelper = stringHelper;
            _client = new RestClient(runSettingsManger.ApiInstanceUrl);
        }

        public async Task<IRestResponse<T>> RestResponseAsync<T>(
            string endPoint, Method method,
            ConcurrentDictionary<string, string> headers = null,
            ConcurrentDictionary<string, string> parameters = null,
            IRestObject restObject = null) where T : new()
        {
            var request = CreateRequest(endPoint, method, headers, parameters, restObject);

            var result = await _client.ExecuteAsync<T>(request);

            _logManager.LogTestAction($"Status response: {result.StatusCode}");

            return result;
        }

        public IRestResponse<T> RestResponse<T>(
            string endPoint, Method method,
            ConcurrentDictionary<string, string> headers = null,
            ConcurrentDictionary<string, string> parameters = null,
            IRestObject restObject = null) where T : new()
        {
            var request = CreateRequest(endPoint, method, headers, parameters, restObject);
            var result = _client.Execute<T>(request);

            _logManager.LogTestAction($"Status response: {result.StatusCode}");

            return result;
        }

        private RestRequest CreateRequest(
            string endPoint, 
            Method method,
            ConcurrentDictionary<string, string> headers = null,
            ConcurrentDictionary<string, string> parameters = null,
            IRestObject restObject = null)
        {
            var request = new RestRequest(endPoint, method);
            request.AddParameter("key", _runSettingsManger.ApiKey);
            request.AddParameter("token", _runSettingsManger.ApiToken);

            if (headers != null)
            {
                Parallel.ForEach(headers, header => { request.AddParameter(header.Key, header.Value, ParameterType.UrlSegment); });
            }

            if (parameters != null)
            {
                Parallel.ForEach(parameters, parameter => { request.AddParameter(parameter.Key, parameter.Value); });
            }

            if (restObject != null)
            {
                Parallel.ForEach(_stringHelper.GetAllClassPropertiesWithValuesAsStrings(restObject), property => { request.AddParameter(property.Key, property.Value); });
            }

            _logManager.LogTestAction($"{method} call will be made for the following url: {_runSettingsManger.ApiInstanceUrl}{endPoint};");

            return request;
        }
    }
}
