using AutomationCore.AssertAndErrorMsgs.UI;
using NUnit.Framework;
using TestsConfigurator;

namespace UI
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class HomePage : UITestsSuitFixture
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

            //Assert


            //Assert.Multiple(() =>
            //{
            //    foreach (var game_UI in card_Titles_UI)
            //    {
            //        var isFound = platform_API.Any(p => p.games.Any(g => g.name.ToLower().Equals(game_UI.ToLower())));
            //        Assert.IsTrue(isFound, $"Expected that UI game is found in API platform data after filtering UI by that platform. \n Game: {game_UI}");
            //    }
            //});

            //Assert
            //Assert.IsTrue(HomePage.Is_Seller_ContainerSection_Displayed, UIAMessages.ElementIsNotDisplayed(nameof(HomePage.Is_Seller_ContainerSection_Displayed), HomePage.Title));
        }
    }
}