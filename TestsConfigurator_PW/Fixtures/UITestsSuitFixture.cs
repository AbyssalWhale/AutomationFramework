using AutomationCore.AssertAndErrorMsgs.UI;
using AutomationCore.Managers;
using AutomationCore_PW.Managers;
using Microsoft.Playwright;
using NUnit.Framework;
using OpenQA.Selenium.DevTools.V107.Page;
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
            RunSettings = RunSettings.Instance;
            pwManager = new PlaywrightManager(RunSettings);
            homePages = new ConcurrentDictionary<string, HomePage>();
        }

        [SetUp]
        public async Task Setup()
        {
            Directory.CreateDirectory(RunSettings.TestsReportDirectory);

            if (pwManager is null)
            {
                throw UIAMessages.GetExceptionForNullObject(nameof(pwManager), nameof(Setup));
            }
            var newHomePage = new HomePage(await pwManager.GetTestContextPW().Result.NewPageAsync());

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

        [TearDown] 
        public async Task TearDown()
        {
            pwManager.ReleaseTestExecution();
        }
    }
}