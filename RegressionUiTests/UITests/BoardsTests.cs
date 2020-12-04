using NUnit.Framework;
using TestConfigurator.Enums.UI;
using TestConfigurator.TestFixtures;

namespace RegressionTests.UITests
{
    class BoardsTests : RegressionUiTestsBase
    {
        [Test]
        public void CheckBoardCreation()
        {
            var boardsPage = _homePage.GoToLogin().Login();
            boardsPage.CloaseAtlassinWindow();
            var newBoardDetailsPage = boardsPage.CreateNewBoard(BoardsTypes.Personal, out string newBoardName);

            Assert.AreEqual(newBoardName, newBoardDetailsPage.GetCurrentBoardTitle(), $"It's expected that new board had the name that was provided in the process of creation. Expected name: {newBoardName}");
        }
    }
}
