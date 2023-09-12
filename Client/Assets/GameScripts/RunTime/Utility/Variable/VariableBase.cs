using System;

namespace GameScripts.RunTime.Utility.Variable
{
    public abstract class VariableBase
    {
        /// <summary>
        /// 获取变量类型
        /// </summary>
        public abstract Type Type
        {
            get;
        }
    }
}