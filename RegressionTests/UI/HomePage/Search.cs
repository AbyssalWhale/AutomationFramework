using AutomationCore.AssertAndErrorMsgs.UI;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using TestsConfigurator;

namespace RegressionTests.UI.HomePage
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class Search : UITestsSuitFixture
    {
        [SetUp]
        public new void SetUp()
        {
            ClassicAssert.IsTrue(HomePage.IsLoaded(), UIAMessages.PageNotLoaded(HomePage.Title));
        }

        [Test]
        public void TheUser_CanSearch_Game_TES_T1([Values("Grand Theft Auto")] string gameName)
        {
            //Act
            HomePage.Search.Input_Search_Game(gameName);

            //Assert
            ClassicAssert.IsTrue(HomePage.GamesGrid.IsLoaded(), "Expected game grid is succefully loaded after entering game for search");
            var card_Titles_UI = HomePage.GamesGrid.Get_Cards_Titles();
            ClassicAssert.Contains(gameName, card_Titles_UI, $"Expected that all test titles contains game {gameName}");
        }
    }
}