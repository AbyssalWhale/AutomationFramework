using Bogus;
using NUnit.Framework;
using RegressionApiTests.Models.Board;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using TestConfigurator.Enums.API;
using TestConfigurator.TestFixtures;

namespace RegressionApiTests.Tests.Boards
{
    [TestFixture]
    class TestsWithValidWorkflows : ApiTestsBase
    {
        [Test]
        public void AllAccountBoardsCanBeGet_TES_T2()
        {
            //Act
            var allboardsGetResponse = _toolsManager._api.RestResponseAsync<List<ResponseBoardModel>>(_toolsManager._enum.GetEnumStringValue(typeof(TrelloEndPoints), TrelloEndPoints.MyAllBoards), Method.Get);
            
            //Assert
            Assert.AreEqual(HttpStatusCode.OK, allboardsGetResponse.Result.StatusCode, $"It's expected response code is: {HttpStatusCode.OK}");
            Assert.NotZero(allboardsGetResponse.Result.Data.Count, "There is more than 1 board while it was returned 0");
        }
    }
}
