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
        public void GamesCanBiFilteredByPlatform_TES_T4([Values("PlayStation")] string platform)
        {
            //Arrange
            Assert.IsTrue(HomePage.IsLoaded(), UIAMessages.PageNotLoaded(HomePage.Title));

            ////Act
            //HomePage.ScrollTo_Professions_Container();

            ////Assert
            //Assert.IsTrue(HomePage.Is_Cook_ContainerSection_Displayed, UIAMessages.ElementIsNotDisplayed(nameof(HomePage.Is_Cook_ContainerSection_Displayed), HomePage.Title));
            //Assert.IsTrue(HomePage.Is_Seamtress_ContainerSection_Displayed, UIAMessages.ElementIsNotDisplayed(nameof(HomePage.Is_Seamtress_ContainerSection_Displayed), HomePage.Title));
            //Assert.IsTrue(HomePage.Is_Seller_ContainerSection_Displayed, UIAMessages.ElementIsNotDisplayed(nameof(HomePage.Is_Seller_ContainerSection_Displayed), HomePage.Title));
        }
    }
}
