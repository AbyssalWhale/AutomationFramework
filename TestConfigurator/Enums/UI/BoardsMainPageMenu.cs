using System.Runtime.Serialization;

namespace TestConfigurator.Enums.UI
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
