using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using YooAsset;
using Object = UnityEngine.Object;

namespace HT.Framework
{
    /// <summary>
    ///YooAsset资源管理器助手
    /// </summary>
    public sealed class YooAssetResourceHelper : IResourceHelper
    {
        public bool EnableForix;
        public long Milliseconds;
        public EVerifyLevel VerifyLevel;
   
        
        private bool _isLoading = false;    //单线下载中
        private WaitUntil _loadWait;    //单线下载等待;
        private readonly Dictionary<Object, IDisposable> _obj_2_handles = new();
        public EPlayMode PlayMode { get; private set; }
        public string PackageName { get; private set; }
        public string PackageVersion { get; private set; }


        private int downloadingMaxNumber;
        private int failedTryAgain;

        /// <summary>
        /// 已加载的所有场景【场景名称、场景】
        /// </summary>
        public Dictionary<string, Scene> Scenes { get; private set; } = new Dictionary<string, Scene>();

        public IModuleManager Module { get; set; }

        /// <summary>
        /// 初始化助手
        /// </summary>
        public void OnInit()
        {
            // 初始化资源系统
            YooAssets.Initialize();
             YooAssets.SetOperationSystemMaxTimeSlice(Milliseconds);
            YooAssets.SetCacheSystemCachedFileVerifyLevel(VerifyLevel);
            
            // 创建默认的资源包
            var package = YooAssets.TryGetPackage(PackageName) ?? YooAssets.CreatePackage(PackageName);
            // 设置该资源包为默认的资源包，可以使用YooAssets相关加载接口加载该资源包内容。
            YooAssets.SetDefaultPackage(package);
            
            _loadWait = new WaitUntil(() => !_isLoading);
        }
        
        
        /// <summary>
        /// 助手准备工作
        /// </summary>
        public void OnReady()
        {

        }
        /// <summary>
        /// 刷新助手
        /// </summary>
        public void OnUpdate()
        {

        }
        /// <summary>
        /// 终结助手
        /// </summary>
        public void OnTerminate()
        {
            ClearMemory();
        }
        /// <summary>
        /// 暂停助手
        /// </summary>
        public void OnPause()
        {

        }
        /// <summary>
        /// 恢复助手
        /// </summary>
        public void OnResume()
        {

        }
        
        public InitializationOperation InitPackage(string hostServerURL, string fallbackHostServerURL)
        {

            if (string.IsNullOrEmpty(hostServerURL))
            {
               //设置默认的url
            }

            if (string.IsNullOrEmpty(fallbackHostServerURL))
            {
                //设置默认的url
            }
            
            // 创建默认的资源包
            var package = YooAssets.TryGetPackage(PackageName) ?? YooAssets.CreatePackage(PackageName);
            // 设置该资源包为默认的资源包，可以使用YooAssets相关加载接口加载该资源包内容。
            YooAssets.SetDefaultPackage(package);
            
            InitializationOperation initializationOperation = null;
            if (PlayMode == EPlayMode.EditorSimulateMode)
            {
                var createParameters = new EditorSimulateModeParameters();
                createParameters.SimulateManifestFilePath = EditorSimulateModeHelper.SimulateBuild(PackageName);
                initializationOperation = package.InitializeAsync(createParameters);
            }
            
            // 单机运行模式
            if (PlayMode == EPlayMode.OfflinePlayMode)
            {
                var createParameters = new OfflinePlayModeParameters();
                createParameters.DecryptionServices = new GameDecryptionServices();
                initializationOperation = package.InitializeAsync(createParameters);
            }
            
            // 联机运行模式
            if (PlayMode == EPlayMode.HostPlayMode)
            {
                var createParameters = new HostPlayModeParameters();
                createParameters.DecryptionServices = new GameDecryptionServices();
                createParameters.QueryServices = new GameQueryServices();
                createParameters.RemoteServices = new RemoteServices(hostServerURL, fallbackHostServerURL);
                initializationOperation = package.InitializeAsync(createParameters);
            }
            
            // WebGL运行模式
            if(PlayMode == EPlayMode.WebPlayMode)
            {
                var createParameters = new WebPlayModeParameters();
                createParameters.DecryptionServices = new GameDecryptionServices();
                createParameters.QueryServices = new GameQueryServices();
                createParameters.RemoteServices = new RemoteServices(hostServerURL, fallbackHostServerURL);
                initializationOperation = package.InitializeAsync(createParameters);
            }

            return initializationOperation;
        }

        /// <summary>
        /// 异步更新最新包的版本。
        /// </summary>
        /// <param name="appendTimeTicks">请求URL是否需要带时间戳。</param>
        /// <param name="timeout">超时时间。</param>
        /// <returns>请求远端包裹的最新版本操作句柄。</returns>
        public async UniTask<UpdatePackageVersionOperation> UpdatePackageVersionAsync(bool appendTimeTicks, int timeout)
        {
            var package = YooAssets.GetPackage(PackageName);
            var updatePackageVersionOperation = package.UpdatePackageVersionAsync(appendTimeTicks, timeout);
            await updatePackageVersionOperation.ToUniTask();
            if (updatePackageVersionOperation.Status == EOperationStatus.Succeed)
            {
                PackageVersion = updatePackageVersionOperation.PackageVersion;
            }
            return updatePackageVersionOperation;
        }

        /// <summary>
        /// 向网络端请求并更新清单
        /// </summary>
        /// <param name="packageVersion">更新的包裹版本</param>
        /// <param name="autoSaveVersion">更新成功后自动保存版本号，作为下次初始化的版本。</param>
        /// <param name="timeout">超时时间（默认值：60秒）</param>
        public async UniTask<UpdatePackageManifestOperation> UpdatePackageManifestAsync(bool autoSaveVersion, int timeout)
        {
            var package = YooAssets.GetPackage(PackageName);
            var updatePackageManifest =  package.UpdatePackageManifestAsync(PackageVersion,autoSaveVersion, timeout);
            await updatePackageManifest.ToUniTask();
            if (updatePackageManifest.Status != EOperationStatus.Succeed) return updatePackageManifest;
            if (autoSaveVersion)
            {
                updatePackageManifest.SavePackageVersion();
            }

            return updatePackageManifest;
        }

        
        public ResourceDownloaderOperation CreateResourceDownloader()
        {
            var package = YooAssets.GetPackage(PackageName);
            var downloader = package.CreateResourceDownloader(downloadingMaxNumber, failedTryAgain);
            return downloader;
        }
        
        /// <summary>
        /// 获取Pakage
        /// </summary>
        /// <param name="packageName"></param>
        /// <returns></returns>
        public ResourcePackage TryGetPackage(string packageName)
        {
            return YooAssets.TryGetPackage(packageName);
        }
        
        private void AddAssetOperationHandle(Object obj,IDisposable handle)
        {
            _obj_2_handles.TryAdd(obj, handle);
        }


        /// <summary>
        /// 加载资源（异步）
        /// </summary>
        /// <typeparam name="T">资源类型</typeparam>
        /// <param name="info">资源信息标记</param>
        /// <param name="onLoading">加载中事件</param>
        /// <param name="isPrefab">是否是加载预制体</param>
        /// <param name="parent">预制体加载完成后的父级</param>
        /// <param name="isUI">是否是加载UI</param>
        /// <returns>加载协程迭代器</returns>
        public async UniTask<T> LoadAssetAsync<T> (ResourceInfoBase info, HTFAction<float> onLoading,bool isPrefab, Transform parent,bool isUI) where T : Object
        {
            var beginTime = Time.realtimeSinceStartup;
            //单线加载，如果其他地方在加载资源，则等待
            if (_isLoading)
            {
               await _loadWait;
            }
            //轮到本线路加载资源
            _isLoading = true;
            var waitTime = Time.realtimeSinceStartup;

            if (string.IsNullOrEmpty(info.AssetBundleName))
            {
                info.AssetBundleName = PackageName;
            }
          
            var package = YooAssets.GetPackage(info.AssetBundleName);
            var handle = package.LoadAssetAsync<T>(info.AssetPath);
            var progress = Progress.Create<float>(p =>
            {
                onLoading?.Invoke(p);
            });
            await handle.ToUniTask(progress);
            var obj =  handle.AssetObject;
            if (isPrefab)
            {
                var prefabTem = obj.Cast<GameObject>();
                var operation =  handle.InstantiateAsync(parent);
                await operation.ToUniTask();
                var instance = operation.Result;
                
                if (isUI)
                {
                    instance.rectTransform().anchoredPosition3D = prefabTem.rectTransform().anchoredPosition3D;
                    instance.rectTransform().sizeDelta = prefabTem.rectTransform().sizeDelta;
                    instance.rectTransform().anchorMin = prefabTem.rectTransform().anchorMin;
                    instance.rectTransform().anchorMax = prefabTem.rectTransform().anchorMax;
                    instance.transform.localRotation = Quaternion.identity;
                    instance.transform.localScale = Vector3.one;
                }
                else
                {
                    instance.transform.localPosition = prefabTem.transform.localPosition;
                    instance.transform.localRotation = Quaternion.identity;
                    instance.transform.localScale = Vector3.one;
                }
                obj = instance;
            }
            
            AddAssetOperationHandle(obj, handle);
            
            var endTime = Time.realtimeSinceStartup;
            string.Format("异步加载资源{0}[{1}模式]：\r\n{2}\r\n等待耗时：{3}秒  加载耗时：{4}秒"
                , obj ? "成功" : "失败"
                , PlayMode.ToString()
                , info.AssetPath
                , (waitTime - beginTime).ToString()
                , (endTime - waitTime).ToString()).Info();
            
            //本线路加载资源结束
            _isLoading = false;
            return obj.Cast<T>();
        }
        
        /// <summary>
        /// 加载场景（异步）
        /// </summary>
        /// <param name="info">资源信息标记</param>
        /// <param name="sceneMode">场景加载模式</param>
        /// <param name="activateOnLoad">加载完毕时是否主动激活</param>
        /// <param name="onLoading">加载中事件</param>
        /// <returns>加载协程迭代器</returns>
        public async UniTask<Scene> LoadSceneAsync(SceneInfo info, LoadSceneMode sceneMode,bool activateOnLoad, HTFAction<float> onLoading)
        {
            var beginTime = Time.realtimeSinceStartup;
            //单线加载，如果其他地方在加载资源，则等待
            if (_isLoading)
            {
                await _loadWait;
            }
            //轮到本线路加载资源
            _isLoading = true;
            var waitTime = Time.realtimeSinceStartup;
            
            if (string.IsNullOrEmpty(info.AssetBundleName))
            {
                info.AssetBundleName = PackageName;
            }
            
            var package = YooAssets.GetPackage(info.AssetBundleName);
            var handle = package.LoadSceneAsync(info.AssetPath, sceneMode, activateOnLoad);
            
            var progress = Progress.Create<float>(p =>
            {
                onLoading?.Invoke(p);
            });
            
            await handle.ToUniTask(progress);
            Scenes.Add(info.AssetPath, handle.SceneObject);
            var endTime = Time.realtimeSinceStartup;
            string.Format("异步加载场景完成[{0}模式]：{1}\r\n等待耗时：{2}秒  加载耗时：{3}秒"
                ,PlayMode.ToString()
                , info.AssetPath
                , (waitTime - beginTime).ToString()
                , (endTime - waitTime).ToString()).Info();
            
            //本线路加载资源结束
            _isLoading = false;
            return handle.SceneObject;
        }
        
        
        /// <summary>
        /// 异步加载子资源对象
        /// </summary>
        /// <param name="info"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async UniTask<T> LoadSubAssetsAsync<T>(SubAssetInfo info)where T : Object
        {
            var beginTime = Time.realtimeSinceStartup;
            //单线加载，如果其他地方在加载资源，则等待
            if (_isLoading)
            {
                await _loadWait;
            }
            //轮到本线路加载资源
            _isLoading = true;
            var waitTime = Time.realtimeSinceStartup;
            
            if (string.IsNullOrEmpty(info.AssetBundleName))
            {
                info.AssetBundleName = PackageName;
            }
            
            var package = YooAssets.GetPackage(info.AssetBundleName);
            var handle = package.LoadSubAssetsAsync<Sprite>(info.AssetPath);
            await handle.ToUniTask();
            var obj = handle.GetSubAssetObject<T>(info.Location);
            AddAssetOperationHandle(obj, handle);
            
            var endTime = Time.realtimeSinceStartup;
            string.Format("异步加载资源{0}[{1}模式]：\r\n{2}{3}\r\n等待耗时：{4}秒  加载耗时：{5}秒"
                , obj ? "成功" : "失败"
                ,PlayMode.ToString()
                , string.Format("{location}/{name}")
                , (waitTime - beginTime).ToString()
                , (endTime - waitTime).ToString()).Info();
            
            //本线路加载资源结束
            _isLoading = false;
            
            return obj;
        }

        /// <summary>
        /// 异步获取原生文件二进制数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async UniTask<byte[]> LoadRawFileDataAsync(ResourceInfoBase info)
        {
            var beginTime = Time.realtimeSinceStartup;
            //单线加载，如果其他地方在加载资源，则等待
            if (_isLoading)
            {
                await _loadWait;
            }
            //轮到本线路加载资源
            _isLoading = true;
            var waitTime = Time.realtimeSinceStartup;
            
            if (string.IsNullOrEmpty(info.AssetBundleName))
            {
                info.AssetBundleName = PackageName;
            }
            
            var package = YooAssets.GetPackage(info.AssetBundleName);
            var handle = package.LoadRawFileAsync(info.AssetPath);
            await handle.ToUniTask();
            
            var endTime = Time.realtimeSinceStartup;
            string.Format("异步加载资源{0}[{1}模式]：\r\n{2}\r\n等待耗时：{3}秒  加载耗时：{4}秒"
                , handle.IsValid ? "成功" : "失败"
                , PlayMode.ToString()
                , info.AssetPath
                , (waitTime - beginTime).ToString()
                , (endTime - waitTime).ToString()).Info();
            
            //本线路加载资源结束
            _isLoading = false;
            
            return handle.GetRawFileData();
        }

        /// <summary>
        /// 异步获取原生文件文本
        /// </summary>
        /// <param name="packageName"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public async UniTask<string> LoadRawFileTextAsync(ResourceInfoBase info)
        {
            var beginTime = Time.realtimeSinceStartup;
            //单线加载，如果其他地方在加载资源，则等待
            if (_isLoading)
            {
                await _loadWait;
            }
            //轮到本线路加载资源
            _isLoading = true;
            var waitTime = Time.realtimeSinceStartup;
            
            if (string.IsNullOrEmpty(info.AssetBundleName))
            {
                info.AssetBundleName = PackageName;
            }
            
            var package = YooAssets.GetPackage(info.AssetBundleName);
            var handle = package.LoadRawFileAsync(info.AssetPath);
            await handle.ToUniTask();
            
            var endTime = Time.realtimeSinceStartup;
            string.Format("异步加载资源{0}[{1}模式]：\r\n{2}\r\n等待耗时：{3}秒  加载耗时：{4}秒"
                , handle.IsValid ? "成功" : "失败"
                , PlayMode.ToString()
                , info.AssetPath
                , (waitTime - beginTime).ToString()
                , (endTime - waitTime).ToString()).Info();
            
            //本线路加载资源结束
            _isLoading = false;
            
            return handle.GetRawFileText();
        }
        
        public async UniTask UnLoadScene(SceneInfo info)
        {
            if (!Scenes.ContainsKey(info.ResourcePath))
            {
                $"卸载场景失败：名为 {info.ResourcePath} 的场景还未加载！".Warning();
                return;
            }

            Scenes.Remove(info.ResourcePath);
            await SceneManager.UnloadSceneAsync(info.ResourcePath).ToUniTask();
        }

        public async UniTask UnLoadAllScene()
        {
            foreach (var scene in Scenes)
            {
              await SceneManager.UnloadSceneAsync(scene.Key).ToUniTask();
            }
            Scenes.Clear();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="obj"></param>
        public void UnLoadAsset(Object obj)
        {
            if (obj is null)
            {
                return;
            }
            if (_obj_2_handles.Remove(obj, out var handle))
            {
                handle.Dispose();
            }
        }
        
        /// <summary>
        /// 释放package资源
        /// </summary>
        /// <param name="packageName"></param>
        public void UnLoadAsset(string assetBundleName)
        {
            var package = YooAssets.GetPackage(assetBundleName);
            package.UnloadUnusedAssets();
        }
        
        /// <summary>
        /// 清理内存，释放空闲内存（异步）
        /// </summary>
        /// <returns>协程迭代器</returns>
        public async UniTask ClearMemory()
        {
            await Resources.UnloadUnusedAssets();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}