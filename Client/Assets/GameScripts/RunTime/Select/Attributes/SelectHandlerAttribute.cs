using System;

namespace GameScripts.RunTime.Select.Attributes
{
    /// <summary>
    /// 选择器属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class SelectHandlerAttribute: Attribute
    {
        /// <summary>
        /// 选择器类型
        /// </summary>
        public Type SelectHandler { get; private set; }
        
        public SelectHandlerAttribute(Type selectHandlerType)
        {
            SelectHandler = selectHandlerType;
        }
    }
}