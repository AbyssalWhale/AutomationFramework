namespace AutomationCore.AssertAndErrorMsgs.UI
{
    public static class UIAMessages
    {
        public static string PageNotLoaded(string pageTitle) => $"\n the '{pageTitle}' page can not be loaded";
        public static string ElementIsNotDisplayed(string elementName, string pageTitle) => $"\nthe '{elementName}' Element is not displayed on the '{pageTitle}' page";
    }
}