using AutomationCore.AssertAndErrorMsgs.UI;
using NUnit.Framework;
using TestsConfigurator;

namespace RegressionTests.UI.HomePage
{
    [TestFixture]
    //[Parallelizable(ParallelScope.All)]
    public class Search : UITestsSuitFixture
    {
        [SetUp]
        public new void SetUp()
        {
            Assert.IsTrue(HomePage.IsLoaded(), UIAMessages.PageNotLoaded(HomePage.Title));
        }

        [Test]
        public void TheUser_CanSearch_Game_TES_T1()
        {

        }
    }
}