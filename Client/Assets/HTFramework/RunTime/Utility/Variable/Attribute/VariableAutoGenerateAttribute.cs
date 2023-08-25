using System;
using System.Diagnostics;

namespace HT.Framework
{
    /// <summary>
    /// 变量自动生成特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    [Conditional("UNITY_EDITOR")]
    public sealed class VariableAutoGenerateAttribute : Attribute
    {

    }
}