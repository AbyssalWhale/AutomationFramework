using TestsConfigurator_PW.Fixtures;

namespace RegressionTestsPW
{
    public class Tests : UITestsSuitFixture
    {
        [Test]
        public async Task TheUser_CanNavigateTo_AboutUs_Page()
        {
            await HomePage.Click_AboutUs_Button()
                .Result.IsAtPage();
        }

        [Test]
        public async Task TheUser_CanNavigateTo_Teachers_Page()
        {
            await HomePage.Click_Tachers_Button()
                .Result.IsAtPage();
        }
    }
}