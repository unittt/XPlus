using System;

namespace GameScripts.RunTime.Utility.Variable
{
    /// <summary>
    /// 变量泛型基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Variable<T> : VariableBase
    {
        /// <summary>
        /// 当前存储的真实值
        /// </summary>
        private T _value;
        /// <summary>
        /// 允许触发事件
        /// </summary>
        public bool IsAllowThrow;
        
        public event Action<T> ValueChange; 

        public virtual T Value
        {
            get => _value;
            set
            {
                if (IsEquals(_value, value)) return;
                _value = value;
                if (IsAllowThrow)
                {
                    ValueChange?.Invoke(_value);
                }
            }
        }
        
        /// <summary>
        /// 变量类型
        /// </summary>
        public override Type Type => typeof(T);

        protected virtual bool IsEquals(T oldValue, T newValue)
        {
            return oldValue.Equals(newValue);
        }
    }
}