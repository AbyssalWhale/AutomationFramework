using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using static AutomationFramework.Entities.WebDriverProvider;

namespace AutomationFramework.Entities
{
    public class DriverProvider
    {
        static IWebDriver _driver;
        private Browsers browser;
        protected DriverProvider(Browsers browser)
        {
            this.browser = browser;
        }
        public static IWebDriver GetDriver(string browser)
        {
            if (_driver == null)
            {
                _driver = WebDriverProvider.GetWebDriverInstance(browser);
            }
            return _driver;
        }
    }
}
