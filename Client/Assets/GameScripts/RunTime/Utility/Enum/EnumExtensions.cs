using System;
using UnityEngine;

namespace GameScripts.RunTime.Utility
{
    public static class EnumExtensions
    {
        /// <summary>
        /// 获得枚举InspectorName
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetInspectorName(this Enum value)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            if (name == null) return name;
            var field = type.GetField(name);
            if (field == null) return name;
            if (Attribute.GetCustomAttribute(field,  typeof(InspectorNameAttribute)) is InspectorNameAttribute attr)
            {
                return attr.displayName;
            }
            return name;
        }
    }
}