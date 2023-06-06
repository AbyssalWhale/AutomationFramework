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
        public void GamesCanBiFilteredByPlatform_TES_T4()
        {
            //Arrange
            var platform = "PlayStation";
            Assert.IsTrue(HomePage.IsLoaded(), UIAMessages.PageNotLoaded(HomePage.Title));
            var platform_API = Controllers.Platforms.Get_VideoGamesPlatform(platform, strictEqual: false).Result;
            //todo: add Get details of the platform. - https://api.rawg.io/docs/#operation/platforms_lists_parents_list

            //Act
            HomePage.GamesGrid.Platforms
                .Click_Platform_DropDown()
                .Click_Platform_Option(platform);
            HomePage.GamesGrid.IsLoaded();
            var card_Titles_UI = HomePage.GamesGrid.Get_Cards_Titles();

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