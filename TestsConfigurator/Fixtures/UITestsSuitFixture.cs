﻿using AutomationCore.Managers;
using NUnit.Framework;
using TestsConfigurator.Controllers;
using TestsConfigurator.Models.POM.HomePage;

namespace TestsConfigurator
{
    [TestFixture, Category("UI")]
    public class UITestsSuitFixture : TestsSuitsFixture
    {
        protected Home HomePage => HomePages[TestContext.CurrentContext.Test.Name];

        [SetUp]
        public void SetUp()
        {
            TestsManagers.TryAdd(TestContext.CurrentContext.Test.Name, new ManagersContainer());
            TestsControllers.TryAdd(TestContext.CurrentContext.Test.Name, new ControllersContainer(API));
            HomePages.TryAdd(TestContext.CurrentContext.Test.Name, new Home(TestsManagers[TestContext.CurrentContext.Test.Name]));
            HomePage.Open();
        }

        [TearDown]
        public void TearDown()
        {
            HomePage.WebDriver.Quit();
        }
    }
}
