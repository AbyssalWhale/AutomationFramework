using System.Runtime.Serialization;

namespace RegressionUiTests.Enums
{
    public enum BoardsTypes
    {
        [EnumMember(Value = "Personal Boards")]
        Personal,
        [EnumMember(Value = "Value should be dynamic")]
        Team,
        [EnumMember(Value = "Trello workspace")]
        Workspace
    }
}
