using System;

namespace HT.Framework
{
    /// <summary>
    /// 实体资源标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class EntityResourceAttribute : Attribute
    {
        public string Location { get; private set; }
        public bool IsUseObjectPool { get; private set; }

        public EntityResourceAttribute(string location, bool useObjectPool = false)
        {
            Location = location;
            IsUseObjectPool = useObjectPool;
        }
    }
}
