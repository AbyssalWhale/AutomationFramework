using NUnit.Framework;
using TestsConfigurator_PW.Fixtures;

namespace RegressionTests_PW.UI
{
    public class AboutUsPage : UITestsSuitFixture
    {
        [Test]
        public async Task TheUser_CanNavigateTo_AboutUs_Page()
        {
            await HomePage.Click_AboutUs_Button()
                .Result.IsAtPage();
        }
    }
}
