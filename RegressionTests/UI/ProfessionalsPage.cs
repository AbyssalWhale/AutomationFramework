using AutomationCore.AssertAndErrorMsgs.UI;
using NUnit.Framework;
using TestsConfigurator;

namespace UI
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class ProfessionalsPage : UITestsSuitFixture
    {
        [SetUp]
        public new void SetUp()
        {
            Assert.IsTrue(HomePage.IsLoaded(), UIAMessages.PageNotLoaded(HomePage.Title));
        }

        [Test]
        public void TheUserCanNavigateToProfessionalsPage_TES_T1()
        {
            //Arrange
            var professionsPage = HomePage
                .ScrollTo_Professions_Container()
                .Click_AllProfessions_Button();

            //Act
            Assert.IsTrue(professionsPage.IsLoaded(), UIAMessages.PageNotLoaded(professionsPage.Title));
        }
    }
}