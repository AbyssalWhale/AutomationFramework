using AutomationCore.AssertAndErrorMsgs.API;
using NUnit.Framework;
using System.Net;
using TestsConfigurator.Fixtures;

namespace API.Boards
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class Post : APITestsSuitFixture
    {
        [Test]
        public void NewBoardCanBeCreated_TES_T3()
        {
            var response = Controllers.Board.Post(name: FakeDataGenerator.Random.String2(10));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created), ApiAEMessages.NotExepctedResponseCode(response));
        }
    }
}
