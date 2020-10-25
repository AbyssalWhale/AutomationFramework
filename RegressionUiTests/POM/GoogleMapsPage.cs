using AutomationFramework.Entities;
using AutomationFramework.Enums;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestsBaseConfigurator.POM
{
    public class GoogleMapsPage : BasePagePOM
    {
        public override string Title => "Google Maps";

        public enum GoogleMapDemographicInfo { Address, PlusCodes, HeaderPhoto }

        private By CookieIframe => By.XPath("//iframe[@class='widget-consent-frame']");
        private By AcceptCookieButton => By.XPath("//div[@id='introAgreeButton']//span[@class='RveJvd snByac']");
        private By SearchInput => By.XPath("//form[@id='searchbox_form']//input");

        public GoogleMapsPage(WebDriverManager webDriverManager, RunSettingManager runSettingsManager, LogManager logManager, FolderManager folderManager) :
            base(webDriverManager, runSettingsManager, logManager, folderManager)
        {
            AcceptCookie();
            Assert.IsTrue(IsAt(), $"It's expected page be: {Title} but was: {_webDriverManager.GetPageTitle()}");
        }

        #region Internal Methods

        protected override bool IsAt()
        {
            _webDriverManager.IsPageLoaded();
            return _webDriverManager.GetPageTitle().Equals(Title);
        }

        protected void AcceptCookie()
        {
            _logManager.LogAction(LogLevels.local, $"Trying to accept cookies on the '{Title}' page...");

            if (_webDriverManager.SwitchToIFrame(CookieIframe))
            {
                _webDriverManager.ClickOnElement(AcceptCookieButton);
                _webDriverManager.SwitchToDefaultContent();
            }
        }

        #endregion

        #region Enxernal Methods

        public void Search(string valueToSearch)
        {
            _logManager.LogAction(LogLevels.local, $"Trying to search in the '{Title}' page. Data to search: {valueToSearch}...");

            _webDriverManager.SendKeys(SearchInput, valueToSearch);
            _webDriverManager.ClickOnElement(By.XPath($"//span[text()='{valueToSearch}']"));
            IsAt();
        }

        public bool IsSearchResultHasExpectedDemographicInfo(GoogleMapDemographicInfo demographicInfo, string expectedValue)
        {
            _logManager.LogAction(LogLevels.local, $"Checking a search result in the '{Title}' page. Check if $'{demographicInfo}' information equals {expectedValue}");

            var result = false;

            switch (demographicInfo)
            {
                case GoogleMapDemographicInfo.Address:
                case GoogleMapDemographicInfo.PlusCodes:
                    result = _webDriverManager.IsElementExistInDOM(By.CssSelector($"div[aria-label *='{expectedValue}']"));
                    break;
                case GoogleMapDemographicInfo.HeaderPhoto:
                    result = _webDriverManager.IsElementExistInDOM(By.CssSelector($"img[src = '{expectedValue}']"));
                    break;
                default:
                    break;
            }

            return result;
        }
        #endregion
    }
}
