using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace YooAssetPlus
{
    /// <summary>
    /// 扩展 GameObject快捷方法 需成对使用
    /// </summary>
    internal static partial class LoadHelper
    {
        private static readonly Dictionary<Object, Object> o_ObjectMap = new();
        
        /// <summary>
        /// 释放由 实例化出来的GameObject
        /// </summary>
        internal static void ReleaseInstantiate(Object gameObject)
        {
            if (!o_ObjectMap.TryGetValue(gameObject, out var asset)) return;
            o_ObjectMap.Remove(gameObject);
            Release(asset);
        }

        internal static void AddToMap(GameObject obj, GameObject source)
        {
            if (!o_ObjectMap.TryAdd(obj, source))
            {
                
            }
        }

        public static Object RemoveInMap(Object obj)
        {
             o_ObjectMap.TryGetValue(obj, out var source);
             return source;
        }
        
        internal static bool TryGetxxxx(Object obj, out Object source)
        {
            return o_ObjectMap.TryGetValue(obj, out source);
        }
    }
}
