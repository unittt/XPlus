using System;
using System.Collections.Generic;
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
    public sealed partial class YooAssetResourceHelper : IResourceHelper
    {
        private ResourceManager _module;
        private bool _isLoading;    //单线下载中
        private WaitUntil _loadWait;    //单线下载等待;
        
        private readonly Dictionary<Object, IDisposable> _obj_2_handles = new();
        
        /// <summary>
        /// 已加载的所有场景【场景名称、场景】
        /// </summary>
        public Dictionary<string, Scene> Scenes { get; private set; } = new Dictionary<string, Scene>();

        /// <summary>
        /// 是否完成初始化完成
        /// </summary>
        public bool IsInitialized { get; private set; }
        /// <summary>
        /// 默认包名
        /// </summary>
        public string PackageName => _module.PackageName;
        /// <summary>
        /// 当前版本
        /// </summary>
        public string PackageVersion { get; private set; }
        /// <summary>
        /// 模块
        /// </summary>
        public IModuleManager Module { get; set; }

        public event HTFAction<bool> InitializationCompleted;
        
        /// <summary>
        /// 初始化助手
        /// </summary>
        public void OnInit()
        {
            _module = Module as ResourceManager;
            _loadWait = new WaitUntil(() => !_isLoading);
        }
        
        /// <summary>
        /// 助手准备工作
        /// </summary>
        public void OnReady()
        {
            if (_module.IsManually) return;
            var updateHandler = new DefaultUpdateHandler
            {
                PackageName = PackageName,
                Mode = _module.Mode,
                DefaultHostServer = _module.DefaultHostServer,
                FallbackHostServer = _module.FallbackHostServer,
                IsDefaultPackage = true
            };
            Initialize(updateHandler);
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


        public void Initialize(IUpdateHandler handler)
        {
            if (IsInitialized)return;
            // 初始化资源系统
            YooAssets.Initialize();
            BeginUpdatePackage(handler).ContinueWith((result) =>
            {
                IsInitialized = result;
                InitializationCompleted?.Invoke(result);
            }).Forget();
        }

        public void UpdatePackage(IUpdateHandler handler)
        {
            BeginUpdatePackage(handler).Forget();
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
        /// 打印加载结束时的信息
        /// </summary>
        /// <param name="isValid"></param>
        /// <param name="location"></param>
        /// <param name="beginTime"></param>
        /// <param name="waitTime"></param>
        private void LogLoadedInfo(bool isValid, string location,float beginTime,float waitTime)
        {
            var endTime = Time.realtimeSinceStartup;
            string.Format("异步加载资源{0}[{1}模式]：\r\n{2}\r\n等待耗时：{3}秒  加载耗时：{4}秒"
                , isValid ? "成功" : "失败"
                , _module.PlayMode.ToString()
                , location
                , (waitTime - beginTime).ToString()
                , (endTime - waitTime).ToString()).Info();
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
            
            var package = YooAssets.GetPackage(PackageName);
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
                , Main.m_Resource.PlayMode.ToString()
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
                ,Main.m_Resource.PlayMode.ToString()
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
                ,Main.m_Resource.PlayMode.ToString()
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
        public async UniTask<byte[]> LoadRawFileDataAsync(YooAsset.AssetInfo info)
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
            
            var package = YooAssets.GetPackage(PackageName);
            var handle = package.LoadRawFileAsync(info);
            await handle.ToUniTask();
            
            var endTime = Time.realtimeSinceStartup;
            string.Format("异步加载资源{0}[{1}模式]：\r\n{2}\r\n等待耗时：{3}秒  加载耗时：{4}秒"
                , handle.IsValid ? "成功" : "失败"
                , Main.m_Resource.PlayMode.ToString()
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
        public async UniTask<string> LoadRawFileTextAsync(YooAsset.AssetInfo info)
        {
            var beginTime = Time.realtimeSinceStartup;
            //单线加载，如果其他地方在加载资源，则等待
            if (_isLoading )
            {
                await _loadWait;
            }
            //轮到本线路加载资源
            _isLoading = true;
            var waitTime = Time.realtimeSinceStartup;
            
            var package = YooAssets.GetPackage(PackageName);
            var handle = package.LoadRawFileAsync(info);
            await handle.ToUniTask();
            
            var endTime = Time.realtimeSinceStartup;
            string.Format("异步加载资源{0}[{1}模式]：\r\n{2}\r\n等待耗时：{3}秒  加载耗时：{4}秒"
                , handle.IsValid ? "成功" : "失败"
                , Main.m_Resource.PlayMode.ToString()
                , info.AssetPath
                , (waitTime - beginTime).ToString()
                , (endTime - waitTime).ToString()).Info();
            
            //本线路加载资源结束
            _isLoading = false;
            
            return handle.GetRawFileText();
        }

        public YooAsset.AssetInfo[] GetAssetInfos(string tag)
        {
            var package = YooAssets.GetPackage(PackageName);
            return package.GetAssetInfos(tag);
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


        #region 资源信息
        /// <summary>
        /// 获取资源信息列表
        /// </summary>
        /// <param name="tag">资源标签</param>
        public AssetInfo[] GetAssetInfos1(string tag)
        {
            return YooAssets.GetAssetInfos(tag);
        }
        
        /// <summary>
        /// 获取资源信息列表
        /// </summary>
        /// <param name="tags">资源标签列表</param>
        public AssetInfo[] GetAssetInfos(string[] tags)
        {
            return YooAssets.GetAssetInfos(tags);
        }

        /// <summary>
        /// 获取资源信息
        /// </summary>
        /// <param name="location">资源的定位地址</param>
        public AssetInfo GetAssetInfo(string location)
        {
            return YooAssets.GetAssetInfo(location);
        }
        /// <summary>
        /// 检查资源定位地址是否有效
        /// </summary>
        /// <param name="location">资源的定位地址</param>
        public bool CheckLocationValid(string location)
        {
            return YooAssets.CheckLocationValid(location);
        }
        #endregion
        
        #region 原生文件
        /// <summary>
        /// 异步加载原生文件
        /// </summary>
        /// <param name="location">资源的定位地址</param>
        public async UniTask<RawFileOperationHandle> LoadRawFileAsync(string location)
        {
            var beginTime = Time.realtimeSinceStartup;
            //单线加载，如果其他地方在加载资源，则等待
            if (_isLoading )
            {
                await _loadWait;
            }
            //轮到本线路加载资源
            _isLoading = true;
            var waitTime = Time.realtimeSinceStartup;
            
            var handle = YooAssets.LoadRawFileAsync(location);
            await handle.ToUniTask();
            
            LogLoadedInfo(handle.IsValid, location, beginTime, waitTime);
            
            //本线路加载资源结束
            _isLoading = false;
            return handle;
        }
        #endregion
        
        #region 场景加载
        /// <summary>
        /// 异步加载场景
        /// </summary>
        /// <param name="location">场景的定位地址</param>
        /// <param name="sceneMode">场景加载模式</param>
        /// <param name="suspendLoad">场景加载到90%自动挂起</param>
        /// <param name="priority">优先级</param>
        public async UniTask<SceneOperationHandle> LoadSceneAsync(string location,HTFAction<float> onLoading = null, LoadSceneMode sceneMode = LoadSceneMode.Single, bool suspendLoad = false, int priority = 100)
        {
            var beginTime = Time.realtimeSinceStartup;
            //单线加载，如果其他地方在加载资源，则等待
            if (_isLoading )
            {
                await _loadWait;
            }
            //轮到本线路加载资源
            _isLoading = true;
            var waitTime = Time.realtimeSinceStartup;
            
            var handle = YooAssets.LoadSceneAsync(location, sceneMode, suspendLoad,priority);
            
            var progress = Progress.Create<float>(p =>
            {
                onLoading?.Invoke(p);
            });
            await handle.ToUniTask(progress);
            
            LogLoadedInfo(handle.IsValid, location, beginTime, waitTime);
            
            //本线路加载资源结束
            _isLoading = false;
      
            return handle;
        }
        #endregion
        
        #region 资源加载
        /// <summary>
        /// 异步加载资源对象
        /// </summary>
        /// <typeparam name="TObject">资源类型</typeparam>
        /// <param name="location">资源的定位地址</param>
        public async UniTask<AssetOperationHandle> LoadAssetAsync<TObject>(string location) where TObject : UnityEngine.Object
        {
            var beginTime = Time.realtimeSinceStartup;
            //单线加载，如果其他地方在加载资源，则等待
            if (_isLoading )
            {
                await _loadWait;
            }
            //轮到本线路加载资源
            _isLoading = true;
            var waitTime = Time.realtimeSinceStartup;
            
            var handle = YooAssets.LoadAssetAsync<TObject>(location);
            await handle.ToUniTask();
           
            LogLoadedInfo(handle.IsValid, location, beginTime, waitTime);
            //本线路加载资源结束
            _isLoading = false;
            return handle;
        }
        #endregion
        
        #region 资源加载
        /// <summary>
        /// 异步加载子资源对象
        /// </summary>
        /// <typeparam name="TObject">资源类型</typeparam>
        /// <param name="location">资源的定位地址</param>
        public async UniTask<SubAssetsOperationHandle> LoadSubAssetsAsync<TObject>(string location) where TObject : UnityEngine.Object
        {
            var beginTime = Time.realtimeSinceStartup;
            //单线加载，如果其他地方在加载资源，则等待
            if (_isLoading )
            {
                await _loadWait;
            }
            //轮到本线路加载资源
            _isLoading = true;
            var waitTime = Time.realtimeSinceStartup;
            
            var handle = YooAssets.LoadSubAssetsAsync<TObject>(location);
            await handle.ToUniTask();
            
            LogLoadedInfo(handle.IsValid, location, beginTime, waitTime);
            //本线路加载资源结束
            _isLoading = false;
          
            return handle;
        }
        #endregion
        
        #region 资源加载
        /// <summary>
        /// 异步加载资源包内所有资源对象
        /// </summary>
        /// <typeparam name="TObject">资源类型</typeparam>
        /// <param name="location">资源的定位地址</param>
        public async UniTask<AllAssetsOperationHandle> LoadAllAssetsAsync<TObject>(string location) where TObject : UnityEngine.Object
        {
            var beginTime = Time.realtimeSinceStartup;
            //单线加载，如果其他地方在加载资源，则等待
            if (_isLoading )
            {
                await _loadWait;
            }
            //轮到本线路加载资源
            _isLoading = true;
            var waitTime = Time.realtimeSinceStartup;
            
            var handle = YooAssets.LoadAllAssetsAsync<TObject>(location);
            await handle.ToUniTask();
            
            LogLoadedInfo(handle.IsValid, location, beginTime, waitTime);
            
            return handle;
        }
        #endregion

        #region 资源释放
        /// <summary>
        /// 异步卸载子场景
        /// </summary>
        /// <param name="handle"></param>
        public async UniTask UnloadAsync(SceneOperationHandle handle)
        { 
            var unloadSceneOperation = handle.UnloadAsync();
            await unloadSceneOperation.ToUniTask();
        }
        
        /// <summary>
        /// 释放资源句柄(单纯只减少引用计数 当引用计数为0)
        /// </summary>
        /// <param name="handle"></param>
        /// <typeparam name="T"></typeparam>
        public void Release<T>(T handle) where T: OperationHandleBase,IDisposable
        {
            handle.Dispose();
        }
        
        /// <summary>
        /// 一旦调用了Package.UnloadUnusedAssets()，是会把引用计数为0的资源的Provider都给销毁了
        /// </summary>
        /// <param name="packageName"></param>
        public void UnloadUnusedAssets(string packageName)
        {
            // 有2个 东西会 引用计数 一个是provider 一个是 Loader
            var package = YooAssets.TryGetPackage(packageName);
            package?.UnloadUnusedAssets();
        }
        #endregion
    }
}