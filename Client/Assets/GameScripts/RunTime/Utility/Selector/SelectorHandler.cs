using System.Collections.Generic;
using System.Reflection;

namespace GameScripts.RunTime.Utility.Selector
{
    /// <summary>
    /// 选择器基类
    /// </summary>
    public abstract class SelectorHandler
    {

        protected List<string> Elements;
        private object _target;
        private FieldInfo _targetFieldInfo;

        protected SelectorHandler()
        {
            Elements = new List<string>();
        }

        public virtual void GetElementCollection(List<string> eList)
        {
            eList.Clear();
            eList.AddRange(Elements);
        }

        public virtual void OnSelect(string value)
        {
            if (Elements.Contains(value) && (_target is not null && _targetFieldInfo is not null))
            {
                _targetFieldInfo.SetValue(_target,GetObjValue(value));
            }
            _target = null;
            _targetFieldInfo = null;
        }
        
        protected abstract object GetObjValue(string value);
        
        
        /// <summary>
        /// 设置作用目标对象
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fieldInfo"></param>
        public virtual void SetTarget(object obj,FieldInfo fieldInfo)
        {
            _target = obj;
            _targetFieldInfo = fieldInfo;
        }
    }
}