using NUnit.Framework;
using OpenQA.Selenium;

namespace AutomationCore.Managers.LogManagers
{
    public class ScreenShootManager
    {
        private const string TestScreenshootFormat = ".png";

        private IWebDriver _driver;
        private string _screenshootsPath;
        private RunSettingsManager _settingsManager;
        private int _testsCountersForScreshoots;

        public ScreenShootManager(string loggerFileFullPath, IWebDriver driver)
        {
            _testsCountersForScreshoots = 0;
            _driver = driver;
            _settingsManager = RunSettingsManager.Instance;
            _screenshootsPath = loggerFileFullPath;
        }

        public Screenshot MakeScreenshoot(IWebElement? element = null)
        {
            if (element is null)
            {
                return MakeAndSaveScreenshoot();
            }

            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", element, " border: 3px solid red;");
            return MakeAndSaveScreenshoot();
        }

        private Screenshot MakeAndSaveScreenshoot()
        {
            var path = $"{_screenshootsPath}/{_testsCountersForScreshoots}{TestScreenshootFormat}";
            var screenShoot = ((ITakesScreenshot)_driver).GetScreenshot();
            screenShoot.SaveAsFile(path);
            TestContext.AddTestAttachment(path);
            _testsCountersForScreshoots++;

            return screenShoot;
        }
    }
}
