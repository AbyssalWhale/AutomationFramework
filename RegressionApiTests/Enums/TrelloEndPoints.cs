

using System.Runtime.Serialization;

namespace RegressionApiTests.Enums
{
    public enum TrelloEndPoints
    {
        [EnumMember(Value = "members/me/boards")]
        MyAllBoards,
        [EnumMember(Value = "boards/")]
        PostBoard,
        [EnumMember(Value = "boards/")]
        RemoveBoard
    }
}
