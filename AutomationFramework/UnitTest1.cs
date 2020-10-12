using AutomationFramework.Entities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using static AutomationFramework.Entities.WebDriverProvider;

namespace Tests
{
    public class Tests
    {
        IWebDriver driver;
        RunSettingManager RunSettings;

        [SetUp]
        public void Setup()
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);

            RunSettings = new RunSettingManager();
            driver = DriverProvider.GetDriver(RunSettings.Browser);
        }

        [Test]
        public void CheckIDS()
        {
            driver.Navigate().GoToUrl(RunSettings.InstanceUrl);
        }

        [TearDown]
        public void FinishTest()
        {
            driver.Close();
        }
    }
}