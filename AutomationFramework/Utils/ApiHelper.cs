﻿using AutomationFramework.Managers;
using AutomationFramework.Models;
using AutomationFramework.Models.Jira.Zephyr;
using RestSharp;
using System.Collections.Concurrent;
using System.Net;
using System.Threading.Tasks;

namespace AutomationFramework.Utils
{
    public class ApiHelper
    {
        internal RestClient _client;
        private RunSettingManager _runSettingsManger;
        private LogManager _logManager { get { return LogManager.GetLogManager(_runSettingsManger); } }
        private StringHelper _stringHelper;
        public ApiHelper(RunSettingManager runSettingsManger, StringHelper stringHelper)
        {
            _runSettingsManger = runSettingsManger;
            _stringHelper = stringHelper;
            _client = new RestClient(runSettingsManger.ApiInstanceUrl);
        }

        public async Task<RestResponse<T>> RestResponseAsync<T>(
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

        public RestResponse<T> RestResponse<T>(
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
    
        public TestCyclesResponse GetZephyrFolders()
        {
            var localCliend = new RestClient("https://api.zephyrscale.smartbear.com");
            var newRequest = new RestRequest("/v2/folders", Method.Get);
            newRequest.AddHeader("Authorization", $"{_runSettingsManger.ZephyrToken}");

            var response = localCliend.Execute<TestCyclesResponse>(newRequest);
            if (!response.StatusCode.Equals(HttpStatusCode.OK))
            {
                throw new System.Exception($"Unable to get zephyr test cycle folders. https://api.zephyrscale.smartbear.com/v2/folders returns {response.StatusCode} for GET request");
            }

            return response.Data;
        }
    }
}
