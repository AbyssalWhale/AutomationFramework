using System.Runtime.Serialization;

namespace AutomationCore.Utils
{
    public static class EnumHelper
    {
        public static string GetEnumStringValue(Type enumType, object enumVal)
        {
            var memInfo = enumType.GetMember(enumVal.ToString());
            var attr = memInfo
                .FirstOrDefault()
                .GetCustomAttributes(false)
                .OfType<EnumMemberAttribute>().FirstOrDefault();

            return attr is null ? "" : attr.Value;
        }
    }
}
