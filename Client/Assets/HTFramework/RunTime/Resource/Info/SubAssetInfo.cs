namespace HT.Framework
{
    /// <summary>
    /// 子资源信息
    /// </summary>
    public sealed class SubAssetInfo : ResourceInfoBase
    {
        /// <summary>
        /// 子资源名称
        /// </summary>
        public string Location;
        
        public SubAssetInfo(string assetBundleName, string assetPath, string resourcePath,string location) : base(assetBundleName, assetPath, resourcePath)
        {
            Location = location;
        }
    }
}