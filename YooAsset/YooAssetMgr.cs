using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using YooAsset;

public class YooAssetMgr
{
    static YooAssetMgr()
    {
        // 初始化资源系统
        YooAssets.Initialize();
    }

    /// <summary>
    /// 更新package
    /// </summary>
    public static async UniTask BeginUpdatePackage(IUpdateHandler handler)
    {
        await InitPackage(handler);
        var version = await UpdateVersion(handler);
        await UpdateManifest(handler, version);
        await DownloadFiles(handler);
        await ClearCache(handler);
        handler.OnDone();
    }

    #region 1.初始化package
    /// <summary>
    /// 初始化资源包
    /// </summary>
    private static async UniTask InitPackage(IUpdateHandler handler)
    {
        handler.OnInitPackage();
        // 创建默认的资源包
        var package = YooAssets.TryGetPackage(handler.PackageName) ?? YooAssets.CreatePackage(handler.PackageName);
        if (handler.IsDefaultPackage)
        {
            YooAssets.SetDefaultPackage(package);
        }

        if (package.InitializeStatus == EOperationStatus.Succeed) return;

        // 初始化
        InitializeParameters initializeParameters = null;
        switch (handler.Mode)
        {
            case EPlayMode.EditorSimulateMode:
            {
                var createParameters = new EditorSimulateModeParameters
                {
                    SimulateManifestFilePath = EditorSimulateModeHelper.SimulateBuild(handler.PackageName)
                };
                initializeParameters = createParameters;
                break;
            }
            case EPlayMode.OfflinePlayMode:
            {
                var createParameters = new OfflinePlayModeParameters();
                createParameters.DecryptionServices = new GameDecryptionServices();
                initializeParameters = createParameters;
                break;
            }
            case EPlayMode.HostPlayMode:
            {
                var createParameters = new HostPlayModeParameters
                {
                    DecryptionServices = new GameDecryptionServices(),
                    BuildinQueryServices = new GameQueryServices(),
                    DeliveryQueryServices = new DefaultDeliveryQueryServices(),
                    RemoteServices = new RemoteServices(handler.DefaultHostServer, handler.FallbackHostServer)
                };
                initializeParameters = createParameters;
                break;
            }
            case EPlayMode.WebPlayMode:
            {
                var createParameters = new WebPlayModeParameters
                {
                    DecryptionServices = new GameDecryptionServices(),
                    BuildinQueryServices = new GameQueryServices(),
                    RemoteServices = new RemoteServices(handler.DefaultHostServer, handler.FallbackHostServer)
                };
                initializeParameters = createParameters;
                break;
            }
        }

        await package.InitializeAsync(initializeParameters).Task;
    }

    #endregion

    #region 2.更新资源版本号
    /// <summary>
    /// 更新资源版本号
    /// </summary>
    /// <param name="packageName"></param>
    private static async UniTask<string> UpdateVersion(IUpdateHandler handler)
    {
        handler.OnUpdateVersion();
        var errorNum = 3;
        for (var i = 0; i < errorNum; i++)
        {
            var package = YooAssets.GetPackage(handler.PackageName);
            var operation = package.UpdatePackageVersionAsync();
            await operation.ToUniTask();
            if (operation.Status == EOperationStatus.Succeed)
            {
                return operation.PackageVersion;
            }

            var taskCompletionSource = new TaskCompletionSource<bool>();
            handler.OnVersionUpdateFailed(operation.Error, taskCompletionSource);
            var isContinue = await taskCompletionSource.Task;
            //如果为ture 继续
            if (!isContinue)
            {
                break;
            }
        }

        //退出游戏
        Application.Quit();
        return "";
    }

    #endregion

    #region 3.更新Manifest

    /// <summary>
    /// 更新Manifest
    /// </summary>
    /// <param name="packageName"></param>
    /// <param name="packageVersion"></param>
    private static async UniTask UpdateManifest(IUpdateHandler handler,string packageVersion)
    {
        handler.OnUpdateManifest();
        var errorNum = 3;
        for (int i = 0; i < errorNum; i++)
        {
            var package = YooAssets.GetPackage(handler.PackageName);
            bool savePackageVersion = true;
            var operation = package.UpdatePackageManifestAsync(packageVersion, savePackageVersion);
            await operation.ToUniTask();

            if (operation.Status == EOperationStatus.Succeed)
            {
                return;
            }

            var taskCompletionSource = new TaskCompletionSource<bool>();
            handler.OnManifestUpdateFailed(operation.Error, taskCompletionSource);
            var isContinue = await taskCompletionSource.Task;
            //如果为ture 继续
            if (!isContinue)
            {
                break;
            }
        }
        //退出游戏
        Application.Quit();
    }

    #endregion

    #region 4.下载
    /// <summary>
    /// 
    /// </summary>
    /// <param name="handler"></param>
    private static async UniTask DownloadFiles(IUpdateHandler handler)
    {
        handler.OnDownloadFiles();
        var downloadingMaxNum = 10;
        var failedTryAgain = 3;
        var errorNum = 3;
        for (var i = 0; i < errorNum; i++)
        {
            var downloader = YooAssets.CreateResourceDownloader(downloadingMaxNum, failedTryAgain);
            if (downloader.TotalDownloadCount == 0) return;
            
            // downloader.OnDownloadErrorCallback = PatchEventDefine.WebFileDownloadFailed.SendEventMessage;
            downloader.OnDownloadProgressCallback = handler.OnProgress;
            downloader.BeginDownload();
            await downloader.ToUniTask();

            if (downloader.Status == EOperationStatus.Succeed) return;

            //等待玩家确认
            var taskCompletionSource = new TaskCompletionSource<bool>();
            handler.OnDownloadFilesFailed(downloader.Error, taskCompletionSource);
            var isContinue = await taskCompletionSource.Task;
            //如果为ture 继续
            if (!isContinue)
            {
                break;
            }
        }

        //退出游戏
        Application.Quit();
    }
    #endregion

    #region 5.清理缓存
    /// <summary>
    /// 清理未使用的缓存
    /// </summary>
    /// <param name="packageName"></param>
    private static async UniTask ClearCache(IUpdateHandler handler)
    {
        handler.OnClearCache();
        var package = YooAssets.GetPackage(handler.PackageName);
        var operation = package.ClearUnusedCacheFilesAsync();
        await operation.ToUniTask();
    }
    #endregion
}
