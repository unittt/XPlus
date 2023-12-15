using System.Threading.Tasks;
using YooAsset;

namespace HT.Framework
{
    /// <summary>
    /// 默认更新助手
    /// </summary>
    public sealed class DefaultUpdateHandler : IUpdateHandler
    {
        public string PackageName { get; set; }
        public EPlayMode Mode { get; set; }
        public string DefaultHostServer { get; set; }
        public string FallbackHostServer { get; set; }
        public bool IsDefaultPackage { get; set; }
        public void OnInitPackage()
        {
            
        }

        public void OnUpdateVersion()
        {
           
        }

        public void OnUpdateManifest()
        {
            
        }

        public void OnDownloadFiles()
        {
           
        }

        public void OnProgress(int currentDownloadCount, int totalDownloadCount, long currentDownloadBytes, long totalDownloadBytes)
        {
           
        }

        public void OnClearCache()
        {
            
        }

        public void OnDone()
        {
           
        }

        public void OnVersionUpdateFailed(string error, TaskCompletionSource<bool> source)
        {
            source.SetResult(true);
        }

        public void OnManifestUpdateFailed(string error, TaskCompletionSource<bool> source)
        {
            source.SetResult(true);
        }

        public void OnDownloadFilesFailed(string error, TaskCompletionSource<bool> source)
        {
            source.SetResult(true);
        }
    }
}


