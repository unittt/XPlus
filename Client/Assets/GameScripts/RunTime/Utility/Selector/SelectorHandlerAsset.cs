using UnityEditor;
using UnityEngine;

namespace GameScripts.RunTime.Utility.Selector
{
    /// <summary>
    /// 资源筛选
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SelectorHandlerAsset<T> : SelectorHandler where T: Object
    {
        /// <summary>
        /// 路径
        /// </summary>
        protected abstract string Path { get; }
        
#if UNITY_EDITOR
        public SelectorHandlerAsset()
        {
            var guids = AssetDatabase.FindAssets($"t:{typeof(T)}", new[] { Path });
            foreach (var guid in guids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                var assetName = System.IO.Path.GetFileNameWithoutExtension(assetPath);
                Terms.Add(assetName);
            }
        }
#endif
        
        internal override object GetTermValue(string value)
        {
            return value;
        }
    }
}