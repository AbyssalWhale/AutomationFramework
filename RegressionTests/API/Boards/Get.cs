using AutomationCore.AssertAndErrorMsgs.API;
using NUnit.Framework;
using System.Net;
using TestsConfigurator.Fixtures;

namespace API.Boards
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class Get : APITestsSuitFixture
    {
        [Test]
        public void AllBoardsCanBeRetrieved_TES_T2()
        {
        }
    }
}