using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace GameScripts.RunTime.Utility.Selector
{
    /// <summary>
    /// 枚举选择器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SelectorHandler_Enum<T> :SelectorHandler where T: struct,Enum
    {
        private Dictionary<string, T> _s2eDic;

        public SelectorHandler_Enum()
        {
            _s2eDic = new Dictionary<string, T>();
            var enumType = typeof(T);
            foreach (var eObj in Enum.GetValues(enumType))
            {
                var fieldInfo = enumType.GetField(eObj.ToString()); // 修改此处
                if (fieldInfo == null) continue;
                var eValue = (T)fieldInfo.GetValue(null);
                var attribute = (InspectorNameAttribute)fieldInfo.GetCustomAttribute(typeof(InspectorNameAttribute), false);
                var inspectorName = attribute != null ? attribute.displayName : eObj.ToString(); // 添加空值判断
                Terms.Add(inspectorName);
                _s2eDic.Add(inspectorName, eValue);
            }
        }
        
        internal override object GetTermValue(string value)
        {
            return _s2eDic[value];
        }
    }
}