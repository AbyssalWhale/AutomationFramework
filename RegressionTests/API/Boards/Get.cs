using AutomationCore.AssertAndErrorMsgs.API;
using NUnit.Framework;
using System.Net;
using TestsConfigurator.Fixtures;

namespace API.Boards
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class Get : APITestsSuitFixture
    {
        [Test]
        public void AllBoardsCanBeRetrieved_TES_T2()
        {
            var response = Controllers.Board.Get();
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), ApiAEMessages.NotExepctedResponseCode(response));
        }
    }
}