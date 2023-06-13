using AutomationCore.Managers.Models;
using AutomationCore.Managers.Models.Jira.ZephyrScale.Cycles;
using AutomationCore.Utils;
using RestSharp;
using System.Collections.Concurrent;
using System.Net;

namespace AutomationCore.Managers
{
    public class RestApiManager
    {
        private RestClient _client;
        private TestsLoggerManager _logger;
        private RunSettingsManager _runSettings;

        public RestApiManager(TestsLoggerManager logger)
        {
            _logger = logger;
            _runSettings = RunSettingsManager.Instance;
            _client = new RestClient(_runSettings.ApiInstanceUrl);
            _client.AddDefaultParameter("key", _runSettings.ApiKey);
        }

        public async Task<RestResponse<T>> ExecuteAsync<T>(
            string endPoint, Method method,
            ConcurrentDictionary<string, string>? headers = null,
            ConcurrentDictionary<string, string>? parameters = null,
            IRestObject? restObject = null) where T : new()
        {
            _logger.LogTestAction(LogMessages.MethodExecution(methodName: nameof(ExecuteAsync), $"End point: {endPoint} Method: {method}"));
            var request = CreateRequest(endPoint, method, headers, parameters, restObject);

            var result = await _client.ExecuteAsync<T>(request);

            _logger.LogTestAction(LogMessages.MethodExecution(methodName: nameof(ExecuteAsync), $"End point: {endPoint} Method: {method} Response code: {result.StatusCode}"));

            return result;
        }

        private RestRequest CreateRequest(
            string endPoint,
            Method method,
            ConcurrentDictionary<string, string>? headers = null,
            ConcurrentDictionary<string, string>? parameters = null,
            IRestObject? restObject = null)
        {
            var request = new RestRequest(endPoint, method);
            request.AddParameter("key", _runSettings.ApiKey);
            request.AddParameter("token", _runSettings.ApiToken);

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            }

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    request.AddParameter(parameter.Key, parameter.Value, ParameterType.UrlSegment);
                }
            }

            if (restObject != null)
            {
                Parallel.ForEach(UString.GetAllClassPropertiesWithValuesAsStrings(restObject), property => { request.AddParameter(property.Key, property.Value); });
            }

            return request;
        }

        public TestCyclesResponse GetZephyrFolders()
        {
            var zephyrUrl = "https://api.zephyrscale.smartbear.com";
            var requestUrl = "/v2/folders";

            _logger.LogTestAction(LogMessages.MethodExecution(methodName: nameof(ExecuteAsync), $"End point: {zephyrUrl}{requestUrl} Method: {Method.Get}"));
            var localCliend = new RestClient(zephyrUrl);
            var newRequest = new RestRequest(requestUrl, Method.Get);
            newRequest.AddHeader("Authorization", $"{_runSettings.ZephyrToken}");

            var response = localCliend.Execute<TestCyclesResponse>(newRequest);
            if (!response.StatusCode.Equals(HttpStatusCode.OK))
            {
                var msg = $"Unable to get zephyr test cycle folders. https://api.zephyrscale.smartbear.com/v2/folders returns {response.StatusCode} for GET request";
                _logger.LogError(LogMessages.MethodExecution($"Method throws exception: {msg}"));
                throw new HttpRequestException(msg);
            }

            _logger.LogTestAction(LogMessages.MethodExecution(methodName: nameof(ExecuteAsync), $"End point: {zephyrUrl}{requestUrl} Method: {Method.Get} Response Code: {response.StatusCode}"));


            return response.Data;
        }
    }
}
