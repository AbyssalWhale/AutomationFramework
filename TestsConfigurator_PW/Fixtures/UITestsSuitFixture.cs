using AutomationCore.AssertAndErrorMsgs.UI;
using AutomationCore.Managers;
using AutomationCore_PW.Managers;
using NUnit.Framework;
using System.Collections.Concurrent;
using TestsConfigurator_PW.Models.POM;

namespace TestsConfigurator_PW.Fixtures
{
    [Parallelizable(ParallelScope.All)]
    [TestFixture]
    public class UITestsSuitFixture
    {
        private PlaywrightManager? pwManager;
        private ConcurrentDictionary<string, HomePage>? homePages;

        protected RunSettings? RunSettings;
        protected HomePage? HomePage => homePages[TestContext.CurrentContext.Test.Name];
        

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            RunSettings = RunSettings.GetRunSettings;
            pwManager = new PlaywrightManager(RunSettings);
            homePages = new ConcurrentDictionary<string, HomePage>();
        }

        [SetUp]
        public async Task Setup()
        {
            if (pwManager is null)
            {
                throw UIAMessages.GetExceptionForNullObject(nameof(pwManager), nameof(Setup));
            }

            var newHomePage = new HomePage(await pwManager.GetTestBrowser().Result.NewPageAsync());

            lock (this)
            {
                if (homePages is null)
                {
                    throw UIAMessages.GetExceptionForNullObject(nameof(homePages), nameof(Setup));
                }

                homePages.TryAdd(TestContext.CurrentContext.Test.Name, newHomePage);
            }

            if (HomePage is null)
            {
                throw UIAMessages.GetExceptionForNullObject(nameof(HomePage), nameof(Setup));
            }

            await HomePage.Navigate();
        }
    }
}