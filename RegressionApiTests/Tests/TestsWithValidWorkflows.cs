using NUnit.Framework;
using RegressionApiTests.Enums;
using RegressionApiTests.Models.Board;
using RegressionApiTests.Models.Board.Enums;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Tests;

namespace RegressionApiTests.Tests
{
    [Parallelizable(ParallelScope.All)]
    class TestsWithValidWorkflows : RegressionApiTestsBaseNoUI
    {
        [Test]
        public void CheckGetAllBoards()
        {
            var boardModelToPost = _boardWorkflow.GenerateSimpleBoardForPOST();
            var actualBoardResponse = _boardWorkflow.CreateBoard(boardModelToPost);
            Assert.AreEqual(HttpStatusCode.OK, actualBoardResponse.Result.StatusCode, $"It's expected response code is: {HttpStatusCode.OK}");

            var allboardsGetResponse = _utilsManager._api.RestResponseAsync<List<ResponseBoardModel>>(_utilsManager._enum.GetEnumStringValue(typeof(TrelloEndPoints), TrelloEndPoints.MyAllBoards), Method.GET);
            Assert.AreEqual(HttpStatusCode.OK, allboardsGetResponse.Result.StatusCode, $"It's expected response code is: {HttpStatusCode.OK}");
            Assert.NotZero(allboardsGetResponse.Result.Data.Count, "There is more than 1 board while it was returned 0");

            Assert.AreEqual(HttpStatusCode.OK, _boardWorkflow.RemoveBoardAsync(actualBoardResponse.Result.Data.id).Result.StatusCode);
        }


        [Test]
        public void CheckBoardCreation()
        {
            var boardModelToPost = _boardWorkflow.GenerateSimpleBoardForPOST();
            var actualBoardResponse = _boardWorkflow.CreateBoard(boardModelToPost);
            Assert.AreEqual(HttpStatusCode.OK, actualBoardResponse.Result.StatusCode, $"It's expected response code is: {HttpStatusCode.OK}");
            Assert.AreEqual(boardModelToPost.name, actualBoardResponse.Result.Data.name);

            Assert.AreEqual(HttpStatusCode.OK, _boardWorkflow.RemoveBoardAsync(actualBoardResponse.Result.Data.id).Result.StatusCode);
        }
    }
}
