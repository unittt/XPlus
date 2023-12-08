using System;
using UnityEngine;

namespace GameScripts.RunTime.Magic.Command
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class ArgumentAttribute: InspectorNameAttribute
    {
        /// <summary>
        /// 显示前提
        /// </summary>
        public string ShowMethod { get; private set; }
        
        public ArgumentAttribute(string displayName) : base(displayName)
        {
            
        }
        
        public ArgumentAttribute(string displayName,string showMethod) : base(displayName)
        {
            ShowMethod = showMethod;
        }
    }
}