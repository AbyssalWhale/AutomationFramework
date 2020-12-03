using System.Runtime.Serialization;

namespace RegressionUiTests.Enums
{
    public enum BoardsMainPageMenu
    {
        [EnumMember(Value = "//a/span[text()='Boards']")]
        Boards,
        [EnumMember(Value = "//a/span[text()='Templates']")]
        Tamplates,
        [EnumMember(Value = "//a/span[text()='Home']")]
        Home
    }
}
