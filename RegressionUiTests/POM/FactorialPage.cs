using AutomationFramework.Entities;
using AutomationFramework.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using TestsBaseConfigurator.POM;

namespace RegressionUiTests.POM
{
    public class FactorialPage : BasePagePOM
    {

        public override string Title => "Factoriall";

        #region Locators

        private By HeaderField = By.CssSelector("h1[class='margin-base-vertical text-center']");
        private By EnterIntegerField = By.XPath("//input[@id='number']");
        private By CalculateButton = By.CssSelector("button[id='getFactorial']");
        private By ResultField = By.CssSelector("p[id='resultDiv']");

        public enum AllFactorialLinks {
            [EnumMember(Value = "//a[text()='Terms and Conditions']")]
            TermsAndConditions,
            [EnumMember(Value = "//a[text()='Privacy']")]
            Privacy,
            [EnumMember(Value = "//a[contains(@href, 'qxf2')]")]
            Qxf2Services
        }

        private Dictionary<AllFactorialLinks, By> AllFactorialLinksVerificationElements = new Dictionary<AllFactorialLinks, By>()
        {
            { AllFactorialLinks.TermsAndConditions, By.XPath("//*[text()='This is the privacy document. We are not yet ready with it. Stay tuned!']") },
            { AllFactorialLinks.Privacy, By.XPath("//*[text()='This is the terms and conditions document. We are not yet ready with it. Stay tuned!']") },
            { AllFactorialLinks.Qxf2Services, By.XPath("//*[text()='QA for startups']") },
        };

        #endregion

        #region External Methods

        public FactorialPage(
            WebDriverManager webDriverManager, 
            RunSettingManager runSettingsManager, 
            LogManager logManager, 
            FolderManager folderManager,
            UtilsManager utilsManager) :
            base(webDriverManager, runSettingsManager, logManager, folderManager, utilsManager)
        {
            _webDriverManager.GoToUrl(_runSettingsSettings.InstanceUrl);
            Assert.IsTrue(IsAt(), $"It's expected page be: {Title} but was: {_webDriverManager.GetPageTitle()}");
        }

        protected override bool IsAt()
        {
            _webDriverManager.IsPageLoaded();
            return _webDriverManager.GetPageTitle().Equals(Title);
        }

        public bool IsHeaderHasExpectedDesign()
        {
            var result = false;

            var element = _webDriverManager.FindElement(HeaderField);

            var color = element.GetCssValue("color");
            var font = element.GetCssValue("font-family");
            var fontSize = element.GetCssValue("font-size");

            if (color.Equals("rgba(92, 184, 92, 1)") & font.Contains("Abel, Arial, sans-serif") & fontSize.Equals("40px")) result = true;

            return result;
        }

        public bool IsEnterIntegerFiledHasExpectedDesign()
        {
            var result = false;

            var element = _webDriverManager.FindElement(EnterIntegerField);

            var placeHolder = element.GetAttribute("placeholder");

            if (placeHolder.Equals("Enter an integer")) result = true;

            return result;
        }

        public bool IsCalculateButtonHasExpectedDesign()
        {
            var result = false;

            var element = _webDriverManager.FindElement(CalculateButton);

            var text = element.Text;
            var color = element.GetCssValue("background-color");

            if (text.Equals("Calculate!") && color.Equals("rgba(92, 184, 92, 1)")) result = true;

            return result;
        }

        public bool IsLinkBroken(AllFactorialLinks link)
        {
            var result = false;

            _webDriverManager.ClickOnElement(By.XPath(_utilsManager._enum.GetEnumStringValue(typeof(AllFactorialLinks), link)));

            IWebElement verificationElement = _webDriverManager.FindElement(AllFactorialLinksVerificationElements[link]);
       
            if (verificationElement.Displayed && _webDriverManager.IsElementExistInDOM(AllFactorialLinksVerificationElements[link])) result = true;

            _webDriverManager.NavigateBack();

            IsAt();

            return result;
        }


        public string CalculateFactorial(int number)
        {
            var resultText = EnterValueCalculateGeResponse(number.ToString());

            resultText = resultText.Remove(0, resultText.IndexOf(": ") + 2);

            return resultText;
        }

        public bool IsInifinityNumberNotCalculated(int number)
        {
            var resultText = EnterValueCalculateGeResponse(number.ToString());

            if (resultText.Equals($"The factorial of {number} is: Infinity")) return true; else return false;
        }

        public bool IsErrorDisplayedWithInvalidInput(string input)
        {
            var resultText = EnterValueCalculateGeResponse(input);

            if (resultText.Equals($"Please enter an integer")) return true; else return false;
        }
        #endregion

        #region Internal Methods
        private string EnterValueCalculateGeResponse(string value)
        {
            _webDriverManager.SendKeys(EnterIntegerField, value);
            _webDriverManager.ClickOnElement(CalculateButton);

            return _webDriverManager.FindElement(ResultField).Text; 
        }
        #endregion
    }
}
