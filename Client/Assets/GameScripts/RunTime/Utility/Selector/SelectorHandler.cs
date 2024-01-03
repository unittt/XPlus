using System.Collections.Generic;

namespace GameScripts.RunTime.Utility.Selector
{
    /// <summary>
    /// 选择器基类
    /// </summary>
    public abstract class SelectorHandler
    {
        
        protected readonly List<string> Terms;
        protected SelectorHandler()
        {
            Terms = new List<string>();
        }

        internal virtual void GetTerms(List<string> terms)
        {
            terms.Clear();
            terms.AddRange(Terms);
        }
        
        internal abstract object GetTermValue(string value);
    }
}