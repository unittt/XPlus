using System;

namespace GameScripts.RunTime.Magic.Command
{
    /// <summary>
    /// 指令属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class CommandAttribute : Attribute
    {
        /// <summary>
        /// 指令名称
        /// </summary>
        public string WrapName { get; private set; }
        
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; }

        public CommandAttribute(string wrapName, int sort = 0)
        {
            WrapName = wrapName;
            Sort = sort;
        }
    }
}