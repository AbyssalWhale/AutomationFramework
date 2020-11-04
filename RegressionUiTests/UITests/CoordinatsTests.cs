using AutomationFramework.Utils;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using Tests;
using static RegressionUiTests.POM.FactorialPage;

namespace RegressionTests.UITests
{
    class Tests : RegressionUiTestsBase
    {
        private Dictionary<int, string> NonInfinityNumbersWithResults = new Dictionary<int, string>()
        {
            { 0, "1" },
            { 1, "1" },
            { 21 , "51090942171709440000" },
            { 22 , "1.1240007277776077e+21" },
            { 169, "4.269068009004705e+304" },
            { 170, "7.257415615307999e+306" },
        };

        private List<string> InvalidIputs = new List<string>() { " ", "!", "1a", "q", "null" };

        [Test]
        public void LayoutTest()
        {
            WriteHelloWorld();
            Assert.IsTrue(_factorialPage.IsHeaderHasExpectedDesign(), "Header title style is not match with expected");
            Assert.IsTrue(_factorialPage.IsEnterIntegerFiledHasExpectedDesign(), "the 'Enter Integer' field style is not match with expected");
            Assert.IsTrue(_factorialPage.IsCalculateButtonHasExpectedDesign(), "the 'Calculate' button style is not match with expected");
            Assert.IsTrue(_factorialPage.IsLinkBroken(AllFactorialLinks.TermsAndConditions), "Verification element is not found for the 'Terms and Conditions' link broken");
            Assert.IsTrue(_factorialPage.IsLinkBroken(AllFactorialLinks.Privacy), "Verification element is not found for the 'Privacy' link broken");
            Assert.IsTrue(_factorialPage.IsLinkBroken(AllFactorialLinks.Qxf2Services), "Verification element is not found for the 'Qxf2Services' link broken");
        }

        [Test]
        public void CheckNonInfinityFactorials([Values(0, 1, 21, 22, 169, 170)] int factorialNumberToCalculate)
        {
            WriteHelloWorld();
            var actualResult = _factorialPage.CalculateFactorial(factorialNumberToCalculate);
            Assert.AreEqual(NonInfinityNumbersWithResults[factorialNumberToCalculate], actualResult, "Actual result of factorial calculation is not match with expected");
        }

        [Test]
        public void CheckInfinityFactorial()
        {
            WriteHelloWorld();
            var randomNumber = new Random().Next(171, 990);
            Assert.IsTrue(_factorialPage.IsInifinityNumberNotCalculated(randomNumber), "Error is not displayed: 'The factorial of {randomNumber} is: Infinity'");
        }

        [Test]
        public void InvalidScenarious()
        {
            WriteHelloWorld();
            foreach (var input in InvalidIputs)
            {
                Assert.IsTrue(_factorialPage.IsErrorDisplayedWithInvalidInput(input), $"Error is not displayed for value: {input}");
            }
        }

        public void WriteHelloWorld()
        {
            Console.WriteLine("Hello World");
        }
    }
}
