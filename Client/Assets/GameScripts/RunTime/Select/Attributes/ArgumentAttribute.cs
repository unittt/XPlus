using System;
using UnityEngine;

namespace GameScripts.RunTime.Select.Attributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class ArgumentAttribute: InspectorNameAttribute
    {
        /// <summary>
        /// 显示前提
        /// </summary>
        public string ShowMethod { get; private set; }
        /// <summary>
        /// 是否为容器
        /// </summary>
        public bool IsContainer { get; private set; }

        public ArgumentAttribute(string displayName,string showMethod = "", bool isContainer = false) : base(displayName)
        {
            ShowMethod = showMethod;
            IsContainer = isContainer;
        }
    }
}