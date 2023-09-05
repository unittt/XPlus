using System.Threading.Tasks;
using YooAsset;

namespace HT.Framework
{
    /// <summary>
    /// 更新处理接口
    /// </summary>
    public interface IUpdateHandler
    {
        public string PackageName { get; }
        public EPlayMode Mode { get; }
        public string DefaultHostServer { get; }
        public string FallbackHostServer { get; }
        public bool IsDefaultPackage { get; }

        /// <summary>
        /// 初始化Package
        /// </summary>
        public void OnInitPackage();

        /// <summary>
        /// 更新资源版本号
        /// </summary>
        public void OnUpdateVersion();

        /// <summary>
        /// 更新Manifest
        /// </summary>
        public void OnUpdateManifest();

        /// <summary>
        /// 下载文件
        /// </summary>
        public void OnDownloadFiles();

        /// <summary>
        /// 更新进度
        /// </summary>
        /// <param name="value"></param>
        public void OnProgress(int currentDownloadCount, int totalDownloadCount, long currentDownloadBytes,
            long totalDownloadBytes);

        /// <summary>
        /// 当清理缓存
        /// </summary>
        public void OnClearCache();

        /// <summary>
        /// 当结束
        /// </summary>
        public void OnDone();

        /// <summary>
        /// 更新资源版本号失败
        /// </summary>
        /// <returns></returns>
        public void OnVersionUpdateFailed(string error, TaskCompletionSource<bool> source);

        /// <summary>
        /// 补丁清单更新失败
        /// </summary>
        /// <returns></returns>
        public void OnManifestUpdateFailed(string error, TaskCompletionSource<bool> source);

        /// <summary>
        /// 下载文件失败
        /// </summary>
        /// <param name="error"></param>
        /// <param name="taskCompletionSource"></param>
        void OnDownloadFilesFailed(string error, TaskCompletionSource<bool> source);
    }
}
