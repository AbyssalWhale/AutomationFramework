using NUnit.Framework;
using RegressionApiTests.Enums;
using RegressionApiTests.Models.Board;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Tests;

namespace RegressionApiTests.Tests
{
    class TestsWithValidWorkflows : RegressionApiTestsBase
    {
        [Test]
        public void GetAllBoards()
        {
            var allboardsGetResponse = _utilsManager.API.RestResponseAsync<List<ResponseBoardModel>>(_utilsManager.Enum.GetEnumStringValue(typeof(TrelloEndPoints), TrelloEndPoints.MyAllBoards), Method.GET);
            Assert.AreEqual(HttpStatusCode.OK, allboardsGetResponse.Result.StatusCode, $"It's expected response code is: {HttpStatusCode.OK}");
            Assert.NotZero(allboardsGetResponse.Result.Data.Count, "There is more than 1 board while it was returned 0");
        }
    }
}
