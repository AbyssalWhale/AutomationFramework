using NUnit.Framework;
using TestsConfigurator;

namespace UI
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class TestsSuit2 : UITestsSuitFixture
    {
        [Test]
        public void Test2_1_TES_T1()
        {
            Thread.Sleep(10000);
        }
    }
}
