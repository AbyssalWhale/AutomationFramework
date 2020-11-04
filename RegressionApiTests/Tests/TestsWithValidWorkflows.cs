using NUnit.Framework;
using RegressionApiTests.Models.Board;
using RestSharp;
using System.Collections.Generic;
using Tests;

namespace RegressionApiTests.Tests
{
    class TestsWithValidWorkflows : RegressionUiTestsBase
    {
        [Test]
        public void GetAllBoards()
        {
            var allboards = _utilsManager.API.RestResponse<List<ResponseBoardModel>>("boards", Method.GET);
        }
    }
}
