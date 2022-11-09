using Bogus;
using NUnit.Framework;
using System.Net;
using TestConfigurator.Models.API.Board.Submodels;
using TestConfigurator.TestFixtures;

namespace RegressionApiTests.Tests.Boards
{
    [TestFixture]
    public class Post : ApiTestsBase
    {
        private string boardIdToDelete;

        [Test]
        public void BoardCanBeCreatedWithReqPropertiesOnly_TES_T3()
        {
            //Arrange
            var boardName = new Faker().Random.String2(5);

            //Act
            var actualBoardResponse = _boardWorkflow.CreateBoard(boardName);
            
            //Assert
            Assert.AreEqual(HttpStatusCode.OK, actualBoardResponse.Result.StatusCode, $"It's expected response code is: {HttpStatusCode.OK} \n Content: {actualBoardResponse.Result.Content}");
            Assert.AreEqual(boardName, actualBoardResponse.Result.Data.Name);
            var boardId = actualBoardResponse.Result.Data.Id;
        }

        [TearDown]
        public void TearDown()
        {
            if (!(boardIdToDelete is null))
            {
                var response = _boardWorkflow.RemoveBoardAsync(boardIdToDelete);
                Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
            }
        }
    }
}
