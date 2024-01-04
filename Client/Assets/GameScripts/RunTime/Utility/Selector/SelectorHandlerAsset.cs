using UnityEditor;

namespace GameScripts.RunTime.Utility.Selector
{
    /// <summary>
    /// 资源筛选
    /// </summary>
    public abstract class SelectorHandlerAsset : SelectorHandler
    {
        /// <summary>
        /// 路径
        /// </summary>
        protected abstract string Path { get; }
        /// <summary>
        /// 过滤
        /// </summary>
        protected abstract string Filter { get; }

#if UNITY_EDITOR
        public SelectorHandlerAsset()
        {
            var guids = AssetDatabase.FindAssets(Filter, new[] { Path });
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