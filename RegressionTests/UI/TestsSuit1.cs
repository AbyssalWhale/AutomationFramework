using NUnit.Framework;
using TestsConfigurator;

namespace UI
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class TestsSuit1 : UITestsSuitFixture
    {
        [Test]
        public void Test1_TES_T4()
        {
            Logger.LogTestAction($"Loggin Step from test {nameof(Test1_TES_T4)}");
            Assert.IsTrue(HomePage.IsLoaded(), "");
        }
    }
}
