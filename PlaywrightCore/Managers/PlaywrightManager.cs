using AutomationCore.Enums;
using AutomationCore.Managers;
using Bogus.DataSets;
using Microsoft.Playwright;
using Microsoft.VisualStudio.TestPlatform.Common;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using NUnit.Framework;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationCore_PW.Managers
{
    public class PlaywrightManager
    {
        private AutomationCore.Managers.RunSettings runSettings;
        private IPlaywright? playwright;
        private ConcurrentDictionary<string, IBrowser>? testsBrowsers;

        public PlaywrightManager(AutomationCore.Managers.RunSettings RunSettings)
        {
            runSettings = RunSettings;
            playwright = Playwright.CreateAsync().Result;
            testsBrowsers = new ConcurrentDictionary<string, IBrowser>();
        }

        public async Task<IPlaywright> GetPlaywright()
        {
            if (playwright is null)
            {
                playwright = await Playwright.CreateAsync();
            }
            return playwright;
        }

        public async Task<IBrowser> GetTestBrowser()
        {   
            if (testsBrowsers.ContainsKey(TestContext.CurrentContext.Test.Name))
            {
                return testsBrowsers[TestContext.CurrentContext.Test.Name];
            }

            var browser = await InitBrowser();
            testsBrowsers.TryAdd(TestContext.CurrentContext.Test.Name, browser);
            return browser;
        }

        private async Task<IBrowser> InitBrowser()
        {
            var browser = runSettings.Browser.ToLower();

            if (browser.Equals(Browsers.chrome.ToString()))
            {
                return await GetPlaywright().Result.Chromium.LaunchAsync(new BrowserTypeLaunchOptions()
                {
                    Headless = false,
                });
            }
            else if (browser.Equals(Browsers.firefox.ToString()))
            {
                return await GetPlaywright().Result.Firefox.LaunchAsync(new BrowserTypeLaunchOptions()
                {
                    Headless = false,
                });
            }
            else
            {
                var msg = $"Unknown browser is tried to be initialized: {browser}";
                throw AutomationCore.AssertAndErrorMsgs.AEMessagesBase.GetException(msg);
            }
        }
    }
}
