using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using YooAsset;

namespace HT.Framework
{

    internal partial class YooAssetResourceHelper
    {
        private string _version;
        private bool _isUpdate = false;
        
        /// <summary>
        /// 开始更新package
        /// </summary>
        private async UniTask<bool> BeginUpdatePackage(IUpdateHandler handler)
        {
            //1.初始化Package
            await InitPackage(handler);

            //2.更新版本号
            var result = await UpdateVersion(handler);
            if (!result) return result;

            //3.更新manifest
            result = await UpdateManifest(handler);
            if (!result) return result;

            //4.下载文件
            result = await DownloadFiles(handler);
            if (!result) return result;

            //5.清理缓存
            await ClearCache(handler);
            handler.OnDone();
            return true;
        }

        #region 1.初始化package

        /// <summary>
        /// 初始化资源包
        /// </summary>
        private async UniTask InitPackage(IUpdateHandler handler)
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
                    var buildPipeline = EDefaultBuildPipeline.BuiltinBuildPipeline;
                    var createParameters = new EditorSimulateModeParameters
                    {
                       
                        SimulateManifestFilePath = EditorSimulateModeHelper.SimulateBuild( _module.BuildPipeline.ToString(),handler.PackageName)
                    };
                    initializeParameters = createParameters;
                    break;
                }
                case EPlayMode.OfflinePlayMode:
                {
                    var createParameters = new OfflinePlayModeParameters
                    {
                        DecryptionServices = new FileStreamDecryption()
                    };
                    initializeParameters = createParameters;
                    break;
                }
                case EPlayMode.HostPlayMode:
                {
                    var createParameters = new HostPlayModeParameters
                    {
                        DecryptionServices = new FileStreamDecryption(),
                        BuildinQueryServices = new GameQueryServices(),
                        RemoteServices = new RemoteServices(handler.DefaultHostServer, handler.FallbackHostServer)
                    };
                    initializeParameters = createParameters;
                    break;
                }
                case EPlayMode.WebPlayMode:
                {
                    var createParameters = new WebPlayModeParameters
                    {
                        DecryptionServices = new FileStreamDecryption(),
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
        private async UniTask<bool> UpdateVersion(IUpdateHandler handler)
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
                    _version = operation.PackageVersion;
                    return true;
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

            return false;
        }

        #endregion

        #region 3.更新Manifest

        /// <summary>
        /// 更新Manifest
        /// </summary>
        /// <param name="packageName"></param>
        /// <param name="packageVersion"></param>
        private async UniTask<bool> UpdateManifest(IUpdateHandler handler)
        {
            handler.OnUpdateManifest();
            var errorNum = 3;
            for (int i = 0; i < errorNum; i++)
            {
                var package = YooAssets.GetPackage(handler.PackageName);
                bool savePackageVersion = true;
                var operation = package.UpdatePackageManifestAsync(_version, savePackageVersion);
                await operation.ToUniTask();

                if (operation.Status == EOperationStatus.Succeed)
                {
                    return true;
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

            return false;
        }

        #endregion

        #region 4.下载

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handler"></param>
        private async UniTask<bool> DownloadFiles(IUpdateHandler handler)
        {
            handler.OnDownloadFiles();
            var downloadingMaxNum = 10;
            var failedTryAgain = 3;
            var errorNum = 3;
            for (var i = 0; i < errorNum; i++)
            {
                var downloader = YooAssets.CreateResourceDownloader(downloadingMaxNum, failedTryAgain);
                if (downloader.TotalDownloadCount == 0) return true;

                // downloader.OnDownloadErrorCallback = PatchEventDefine.WebFileDownloadFailed.SendEventMessage;
                downloader.OnDownloadProgressCallback = handler.OnProgress;
                downloader.BeginDownload();
                await downloader.ToUniTask();

                if (downloader.Status == EOperationStatus.Succeed) return true;

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

            return false;
        }

        #endregion

        #region 5.清理缓存

        /// <summary>
        /// 清理未使用的缓存
        /// </summary>
        /// <param name="packageName"></param>
        private async UniTask<bool> ClearCache(IUpdateHandler handler)
        {
            handler.OnClearCache();
            var package = YooAssets.GetPackage(handler.PackageName);
            var operation = package.ClearUnusedCacheFilesAsync();
            await operation.ToUniTask();
            return true;
        }

        #endregion
    }
}
