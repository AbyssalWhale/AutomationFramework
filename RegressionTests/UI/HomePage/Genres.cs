using AutomationCore.AssertAndErrorMsgs.UI;
using NUnit.Framework;
using TestsConfigurator;

namespace RegressionTests.UI.HomePage
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class Genre : UITestsSuitFixture
    {
        [Test]
        public void TheUser_CanSelect_Game_Genre_T4()
        {
            //Arrange
            var genreName = "Card";
            Assert.IsTrue(HomePage.IsLoaded(), UIAMessages.PageNotLoaded(HomePage.Title));
            var genreUnderTest = Controllers.Genres.Get_Genre(genreName).Result;

            //Act
            HomePage.Genres.Click_Genre_Link(genreName);
            HomePage.GamesGrid.IsLoaded();
            var card_Titles_UI = HomePage.GamesGrid.Get_Cards_Titles();

            //Assert
            Parallel.ForEach(card_Titles_UI, gameTitle =>
            {
                Assert.IsNotNull(genreUnderTest.games.Where(g => g.name.ToLower().Equals(gameTitle.ToLower())), "Expected game is found on UI after filtering by genre");
            });
        }
    }
}