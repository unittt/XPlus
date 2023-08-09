using System;
using UnityEngine;
using YooAsset;
using ZEngine.Utility.State;


/// <summary>
    /// 创建文件下载器
    /// </summary>
    internal sealed class FsmCreateDownloader : StateBase
    {
        public override void OnEnter()
        {
            PatchManager.Listener.OnPatchStatesChange("创建补丁下载器！");
            CreateDownloader().Forget();
        }
        
        private async UniTaskVoid CreateDownloader()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
    
            const int downloadingMaxNum = 10;
            const int failedTryAgain = 3;
            var downloader = YooAssets.CreatePatchDownloader(downloadingMaxNum, failedTryAgain);
            PatchManager.Downloader = downloader;
    
            if (downloader.TotalDownloadCount == 0)
            {
                Debug.Log("Not found any download files !");
                _machine.ChangeState<FsmDownloadOver>();
            }
            else
            {
                //A total of 10 files were found that need to be downloaded
                Debug.Log($"Found total {downloader.TotalDownloadCount.ToString()} files that need download ！");
    
                // 发现新更新文件后，挂起流程系统
                // 注意：开发者需要在下载前检测磁盘空间不足
                var totalDownloadCount = downloader.TotalDownloadCount;
                var totalDownloadBytes = downloader.TotalDownloadBytes;
                PatchManager.Listener.OnFoundUpdateFiles(totalDownloadCount, totalDownloadBytes);
            }
        }
    }
