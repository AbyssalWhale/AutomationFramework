using NUnit.Framework;
using TestsConfigurator;

namespace UI
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class TestsSuit1 : UITestsSuitFixture
    {
        [Test]
        public void Test1()
        {
            Logger.LogTestAction($"Loggin Step from test {nameof(Test1)}");
        }

        [Test]
        public void Test1_2()
        {
            Thread.Sleep(1000);
        }

        [Test]
        public void Test1_3()
        {
            Thread.Sleep(1000);
        }

        [Test]
        public void Test1_4()
        {
            Thread.Sleep(10000);
        }

        [Test]
        public void Test1_5()
        {
            Thread.Sleep(10000);
        }
    }
}
