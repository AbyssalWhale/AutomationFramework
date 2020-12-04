using System.Runtime.Serialization;

namespace TestConfigurator.Enums.UI
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
