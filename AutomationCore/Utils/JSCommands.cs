namespace AutomationCore.Utils
{
    public static class JSCommands
    {
        public static string MoveToElement => "arguments[0].scrollIntoView({block: 'nearest'});";
        public static string PageState => "return document.readyState";
        public static string OpenNewTab => "window.open();";
    }
}
