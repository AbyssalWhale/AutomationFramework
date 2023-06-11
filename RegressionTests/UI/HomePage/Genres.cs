using AutomationCore.AssertAndErrorMsgs.UI;
using NUnit.Framework;
using TestsConfigurator;

namespace RegressionTests.UI.HomePage
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class Genre : UITestsSuitFixture
    {
        [SetUp]
        public new void SetUp()
        {
            Assert.IsTrue(HomePage.IsLoaded(), UIAMessages.PageNotLoaded(HomePage.Title));
        }

        [Test]
        public void TheUser_CanSelect_Game_Genre_T4([Values("Card")] string genreName)
        {
            //Arrange
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