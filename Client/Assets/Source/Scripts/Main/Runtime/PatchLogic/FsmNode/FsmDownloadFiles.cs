using YooAsset;
using ZEngine.Utility.State;


/// <summary>
    /// 下载更新文件
    /// </summary>
    internal sealed class FsmDownloadFiles : StateBase
    {

        public void OnInit()
        {
            throw new System.NotImplementedException();
        }

        public  override  void OnEnter()
        {
            PatchManager.Listener.OnPatchStatesChange("开始下载补丁文件！");
            BeginDownload().Forget();
        }
        
        private async UniTaskVoid BeginDownload()
        {
            var downloader = PatchManager.Downloader;
            PatchManager.Listener.OnPatchStatesChange("开始下载补丁文件！");
            // 注册下载回调
            downloader.OnDownloadErrorCallback =  PatchManager.Listener.OnWebFileDownloadFailed;
            downloader.OnDownloadProgressCallback =  PatchManager.Listener.OnDownloadProgressUpdate;
            downloader.BeginDownload();
            await downloader.ToUniTask();

            // 检测下载结果
            if (downloader.Status != EOperationStatus.Succeed)
            {
                return;
            }
            Machine.SwitchState<FsmPatchDone>();
        }
    }