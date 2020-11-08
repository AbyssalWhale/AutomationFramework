

using System.Runtime.Serialization;

namespace RegressionApiTests.Enums
{
    public enum TrelloEndPoints
    {
        [EnumMember(Value = "members/me/boards")]
        MyAllBoards
    }
}
