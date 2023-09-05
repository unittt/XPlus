using YooAsset;

namespace Game.Yoo
{
    /// <summary>
    /// 资源文件查询服务类
    /// </summary>
    public class GameQueryServices : IBuildinQueryServices
    {
        public bool QueryStreamingAssets(string packageName, string fileName)
        {
            // 注意：fileName包含文件格式
            return StreamingAssetsHelper.FileExists(packageName, fileName);
        }
    }
}