using AutomationFramework.Entities;
using AutomationFramework.Enums;
using AutomationFramework.Models;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace AutomationFramework.Utils
{
    public class ApiHelper
    {
        internal IRestClient _client;
        private RunSettingManager _runSettingsManger;
        private LogManager _logManager;
        private StringHelper _stringHelper;
        public ApiHelper(RunSettingManager runSettingsManger, LogManager logManager, StringHelper stringHelper)
        {
            _runSettingsManger = runSettingsManger;
            _logManager = logManager;
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

            return await _client.ExecuteAsync<T>(request);
        }

        public IRestResponse<T> RestResponse<T>(
            string endPoint, Method method,
            ConcurrentDictionary<string, string> headers = null,
            ConcurrentDictionary<string, string> parameters = null,
            IRestObject restObject = null) where T : new()
        {
            var request = CreateRequest(endPoint, method, headers, parameters, restObject);
            return _client.Execute<T>(request);
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

            //_logManager.LogAction(LogLevels.local, $"{method} call will be made for the following url: {_runSettingsManger.ApiInstanceUrl}{endPoint}");

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

            return request;
        }
    }
}
