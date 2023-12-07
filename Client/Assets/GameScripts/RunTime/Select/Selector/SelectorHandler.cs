using System.Collections.Generic;
using System.Reflection;
using HT.Framework;

namespace GameScripts.RunTime.Select.Selector
{
    
    public abstract class SelectorHandler
    {

        protected List<string> Elements;
        private object _target;
        private FieldInfo _targetFieldInfo;

        public SelectorHandler()
        {
            Elements = new List<string>();
        }

        public virtual void GetElementCollection(List<string> eList)
        {
            eList.Clear();
            eList.AddRange(Elements);
        }

        public virtual void Select(string value)
        {
            if (Elements.Contains(value) && (_target is not null && _targetFieldInfo is not null))
            {
                _targetFieldInfo.SetValue(_target,GetObjValue(value));
            }
            _target = null;
            _targetFieldInfo = null;
        }
        
        protected abstract object GetObjValue(string value);
        
        
        public virtual void SetTarget(object obj,FieldInfo fieldInfo)
        {
            _target = obj;
            _targetFieldInfo = fieldInfo;
        }
        
        // public static void SetPropertyValueWithAttribute<T>(object target, string attributeName, object value)
        //     where T : Attribute
        // {
        //     // 获取 target 对象的类型
        //     var type = target.GetType();
        //
        //     // 遍历所有属性
        //      foreach (var property in type.GetProperties())
        //     {
        //         // 获取属性上的特定 Attribute
        //         var attribute = property.GetCustomAttribute<T>();
        //
        //         // 如果找到了带有指定 Attribute 的属性
        //         // if (attribute != null && ((MyCustomAttribute)(object)attribute).Name == attributeName)
        //         // {
        //         //     // 设置属性值
        //         //     property.SetValue(target, value);
        //         //     break;
        //         // }
        //     }
        // }
        
        //1.选择器类型,当值发生改变时 是否刷新界面
        //2.new 一个选择器
        //3.选择器赋值
    }
    
}