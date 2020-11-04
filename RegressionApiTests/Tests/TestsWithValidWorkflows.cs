using NUnit.Framework;
using RestSharp;
using Tests;

namespace RegressionApiTests.Tests
{
    class TestsWithValidWorkflows : RegressionUiTestsBase
    {
        [Test]
        public void GetAllBoards()
        {
            var allboards = _utilsManager.API.RestResponse<object>("boards", Method.GET);
        }
    }
}
