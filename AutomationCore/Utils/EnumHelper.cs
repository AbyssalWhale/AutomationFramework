using AutomationCore.AssertAndErrorMsgs.UI;
using System.Runtime.Serialization;

namespace AutomationCore.Utils
{
    public static class EnumHelper
    {
        public static string GetEnumStringValue(Type enumType, object enumVal)
        {
            var enumValStr = enumVal as string;
            if (enumValStr is null)
            {
                throw UIAMessages.GetException($"enumVal can not be null to retrieve value from enum: {enumType.GetType()}");
            }

            var memInfo = enumType.GetMember(enumValStr);
            var firstMemberInfo = memInfo.FirstOrDefault();
            if (firstMemberInfo is null)
            {
                return string.Empty;
            }

            var attr = firstMemberInfo.GetCustomAttributes(false)
                .OfType<EnumMemberAttribute>().FirstOrDefault();

            if (attr is null)
            {
                return string.Empty;
            }

            if (attr.Value is null)
            {
                return string.Empty;
            }

            return attr.Value;
        }
    }
}
