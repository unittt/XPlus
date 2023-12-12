using System;
using UnityEngine;

namespace GameScripts.RunTime.Magic.Command
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class ArgumentAttribute : InspectorNameAttribute
    {
        /// <summary>
        /// 显示前提
        /// </summary>
        public string ShowMethod { get; }

        /// <summary>
        /// 默认值
        /// </summary>
        public object DefaultValue { get; }

        public ArgumentAttribute(string displayName) : base(displayName)
        {

        }

        public ArgumentAttribute(string displayName, object defaultValue) : base(displayName)
        {
            DefaultValue = defaultValue;
        }

        public ArgumentAttribute(string displayName, object defaultValue, string showMethod) : base(displayName)
        {
            DefaultValue = defaultValue;
            ShowMethod = showMethod;
        }

        public ArgumentAttribute(string displayName, string showMethod) : base(displayName)
        {
            ShowMethod = showMethod;
        }
    }
}