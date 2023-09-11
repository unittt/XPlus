using System.Collections.Generic;
using UnityEngine;

namespace YooAssetPlus
{
    /// <summary>
    /// obj 和 LoadHandle关联
    /// </summary>
    internal static partial class LoadHelper
    {
        private static Dictionary<Object, LoadHandle> m_ObjLoadHandle = new();

        internal static bool AddLoadHandle(Object obj, LoadHandle handle)
        {
            if (m_ObjLoadHandle.ContainsKey(obj))
            {
                Debug.LogError($"此obj {obj.name} Handle 已存在 请检查 请勿创建多个");
                return false;
            }

            m_ObjLoadHandle.Add(obj, handle);
            return true;
        }

        private static bool RemoveLoadHandle(LoadHandle handle)
        {
            var obj = handle.Object;
            return obj != null && RemoveLoadHandle(obj);
        }

        private static bool RemoveLoadHandle(Object obj)
        {
            if (m_ObjLoadHandle.ContainsKey(obj)) return m_ObjLoadHandle.Remove(obj);
            Debug.LogError($"此obj {obj.name} Handle 不存在 请检查 请先创建设置");
            return false;

        }

        internal static LoadHandle GetLoadHandle(Object obj)
        {
            if (m_ObjLoadHandle.ContainsKey(obj)) return m_ObjLoadHandle[obj];
            Debug.LogError($"此obj {obj.name} Handle 不存在 请检查 请先创建设置");
            return null;

        }
    }
}
