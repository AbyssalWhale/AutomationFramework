using NUnit.Framework;
using TestsConfigurator;

namespace UI
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class TestsSuit2 : UITestsSuitFixture
    {
        [Test]
        public void Test2_1()
        {
            Thread.Sleep(10000);
        }

        [Test]
        public void Test2_2()
        {
            Thread.Sleep(10000);
        }

        [Test]
        public void Test2_3()
        {
            Thread.Sleep(10000);
        }

        [Test]
        public void Test2_4()
        {
            Thread.Sleep(10000);
        }

        [Test]
        public void Test2_5()
        {
            Thread.Sleep(10000);
        }

        [Test]
        public void Test2_6()
        {
            Thread.Sleep(10000);
        }
    }
}
