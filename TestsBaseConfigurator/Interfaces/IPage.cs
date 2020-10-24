namespace TestsBaseConfigurator.Interfaces
{
    interface IPage
    {
        string Title { get; }

        ///<summary>
        ///Check the load status of page and verifies page title. Return false if titles are not match
        ///</summary>
        bool IsAt();
    }
}
