using System.Collections.Generic;

namespace GameScripts.RunTime.Utility.Selector
{
    /// <summary>
    /// 选择器基类
    /// </summary>
    public abstract class SelectorHandler
    {

        protected List<string> Elements;
        protected SelectorHandler()
        {
            Elements = new List<string>();
        }

        public virtual void GetElementCollection(List<string> eList)
        {
            eList.Clear();
            eList.AddRange(Elements);
        }
        
        public abstract object GetValue(string value);
    }
}