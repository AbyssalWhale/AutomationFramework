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
            var platform = "PlayStation";
            //Arrange
            Assert.IsTrue(HomePage.IsLoaded(), UIAMessages.PageNotLoaded(HomePage.Title));

            //Act
            HomePage.GamesGrid.Platforms
                .Click_Platform_DropDown()
                .Click_Platform_Option(platform);
            HomePage.GamesGrid.IsLoaded();


            //Assert
            //Assert.IsTrue(HomePage.Is_Seller_ContainerSection_Displayed, UIAMessages.ElementIsNotDisplayed(nameof(HomePage.Is_Seller_ContainerSection_Displayed), HomePage.Title));
        }
    }
}
