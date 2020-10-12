﻿using System;
using System.Linq;
using System.Runtime.Serialization;

namespace AutomationFramework.Utils
{
    public static class EnumExtension
    {
        public static string GetEnumStringValue(Type enumType, object enumVal)
        {
            var memInfo = enumType.GetMember(enumVal.ToString());
            var attr = memInfo[0].GetCustomAttributes(false).OfType<EnumMemberAttribute>().FirstOrDefault();

            if (attr != null) {
                return attr.Value;
            }

            return null;
        }
    }
}
