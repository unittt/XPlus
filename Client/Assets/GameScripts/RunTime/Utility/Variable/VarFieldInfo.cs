using System;
using System.Reflection;

namespace GameScripts.RunTime.Utility.Variable
{
    public sealed class VarFieldInfo : IVariable
    {
        public object Target { get; }
        public FieldInfo Info { get; }

        public event Action<object> ValueChange;

        public VarFieldInfo(object target, FieldInfo fieldInfo)
        {
            Target = target;
            Info = fieldInfo;
        }

        public object Value
        {
            get => Info.GetValue(Target);
            set
            {
                Info.SetValue(Target, value);
                ValueChange?.Invoke(value);
            }
        }

        public Type FieldType => Info.FieldType;
       

        public T GetCustomAttribute<T>() where T: Attribute
        {
            return Info.GetCustomAttribute<T>();
        }
    }
}