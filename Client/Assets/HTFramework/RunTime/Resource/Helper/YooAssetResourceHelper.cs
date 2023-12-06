using System;
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
        
        #region 加载资源
        /// <summary>
        /// 加载资源（异步）
        /// </summary>
        /// <param name="location"></param>
        /// <param name="isPrefab">是否是加载预制体</param>
        /// <param name="parent">预制体加载完成后的父级</param>
        /// <param name="isUI">是否是加载UI</param>
        /// <typeparam name="T">资源类型</typeparam>
        /// <returns></returns>
        public async UniTask<T> LoadAssetAsync<T> (string location,bool isPrefab, Transform parent,bool isUI) where T : Object
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

            //开始加载
            Object result;
            
            //从定位中获取到LoadHandle,如果没有则创建一个新的
            var loadHandle = TryGetLoadHandleByLocation(location,true);
            //设置LoadHandle 为正在加载中
            loadHandle.SetWaitAsync(true);
            
            if (loadHandle.Object != null)
            {
                result = loadHandle.Object;
            }
            else
            {
                //使用YooAssets 加载 资源句柄
                var assetOperationHandle = YooAssets.LoadAssetAsync<T>(location);
                await assetOperationHandle.ToUniTask();
                
                //记录资源句柄
                AddAssetOperationHandle(assetOperationHandle);
                
                result = assetOperationHandle.AssetObject;
           
                //将result 和 loadHandle 关联
                if (AddLoadHandle(result, loadHandle))
                {
                    loadHandle.ResetHandle(result, assetOperationHandle.GetHashCode());
                }
            }

            if (result != null)
            {
                // 只在成功加载资源后增加引用计数
                loadHandle.AddRefCount(); 
                
                //如果当前为预制件 则实例化对象
                if (isPrefab)
                {
                    var prefabTem = result.Cast<GameObject>();
                    result = InstanceGameObject(prefabTem, parent, isUI);
                    //关联clone 与 prefab
                    AddClone(result, prefabTem,loadHandle);
                }
            }
            
            loadHandle.SetWaitAsync(false);
            LogLoadedInfo(result != null, location,beginTime,waitTime);
            //本线路加载资源结束
            _isLoading = false;
            return result.Cast<T>();
        }
        #endregion
        
        #region 原生文件
        /// <summary>
        /// 获取原生文件文本
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public async UniTask<string> LoadRawFileTextAsync(string location)
        {
            var rawFileOperationHandle = await LoadRawFileAsync(location);
            var rawFileText = rawFileOperationHandle.GetRawFileText();
            rawFileOperationHandle.Release();
            return rawFileText;
        }
        
        /// <summary>
        /// 获取原生文件二进制数据
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public async UniTask<byte[]> LoadRawFileDataAsync(string location)
        {
            var rawFileOperationHandle = await LoadRawFileAsync(location);
            var rawFileData = rawFileOperationHandle.GetRawFileData();
            rawFileOperationHandle.Release();
            return rawFileData;
        }
        
        /// <summary>
        /// 异步加载原生文件
        /// </summary>
        /// <param name="location">资源的定位地址</param>
        private async UniTask<RawFileOperationHandle> LoadRawFileAsync(string location)
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
        /// 加载场景
        /// </summary>
        /// <param name="location">场景的定位地址</param>
        /// <param name="onLoading">加载进度</param>
        /// <param name="sceneMode">场景加载模式</param>
        /// <param name="suspendLoad">景加载到90%自动挂起</param>
        /// <param name="priority">优先级</param>
        /// <returns></returns>
        public async UniTask<Scene> LoadSceneAsync(string location,HTFAction<float> onLoading = null, LoadSceneMode sceneMode = LoadSceneMode.Single, bool suspendLoad = false, int priority = 100)
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
            
            var scene = handle.SceneObject;
            AddSceneOperationHandle(scene.name,handle);
            return scene;
        }

        /// <summary>
        /// 获取资源信息的路径列表
        /// </summary>
        /// <param name="tag">标签</param>
        /// <param name="isAddress">是否为可寻址路径</param>
        /// <returns></returns>
        public string[] GetAssetPath(string tag, bool isAddress)
        {
            var assetInfos =  YooAssets.GetAssetInfos(tag);
            var pathAry = new string[assetInfos.Length];
            if (isAddress)
            {  
                for (var i = 0; i < assetInfos.Length; i++)
                {
                    pathAry[i] = assetInfos[i].Address;
                }
            }
            else
            {
                for (var i = 0; i < assetInfos.Length; i++)
                {
                    pathAry[i] = assetInfos[i].AssetPath;
                }
            }
            return pathAry;
        }

        #endregion
        
        #region 释放
        /// <summary>
        ///  卸载资源
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="isClone"></param>
        public void UnLoadAsset(Object obj)
        {
            if (obj is null)
            {
                return;
            }

            //是否为克隆体
            var isClone = false;
            //作为clone的实例化编号
            var cloneInstanceID = 0;
            //如果为gameObject 判断是否为克隆体
            if (obj.GetType() == typeof(GameObject))
            {
                //判断是否关联源obj
                var sourceObj = TryGetObjectByClone(obj, true);
                
                if (sourceObj != null)
                {
                    cloneInstanceID = obj.GetInstanceID();
                    obj = sourceObj;
                    isClone = true;
                }
            }

            //获得obj handle
            if (!TryGetLoadHandleByObj(obj, out var handle))
            {
                return;
            }
            
            //如果为克隆先移除其InstanceID
            if (isClone)
            {
                handle.RemoveInstanceID(cloneInstanceID);
            }
            
            //减少引用
            handle.RemoveRefCount();
            
            //如果引用为0 则释放
            if (handle.RefCount == 0)
            {
                ReleaseHandle(handle);
            }
        }
        
        /// <summary>
        /// 卸载场景
        /// </summary>
        /// <param name="sceneName">场景名称</param>
        public async UniTask UnLoadScene(string sceneName)
        {
            if (TryGetSceneOperationHandle(sceneName, out var sceneOperationHandle))
            {
                await sceneOperationHandle.UnloadAsync().ToUniTask();
            }
            RemoveSceneOperationHandle(sceneName);
        }
        
        /// <summary>
        ///  清理内存，释放空闲内存（异步）
        /// </summary>
        public async UniTask ClearMemory()
        {
            ReleaseAllHandle();
            var package = YooAssets.GetPackage(PackageName);
            package.UnloadUnusedAssets();
            
            await Resources.UnloadUnusedAssets();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        #endregion
        
        #region 工具
        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="prefabTem">预制体</param>
        /// <param name="parent">父物体</param>
        /// <param name="isUI">是否是ui</param>
        /// <returns></returns>
        private GameObject InstanceGameObject(GameObject prefabTem, Transform parent,bool isUI)
        {
            var clone = Main.Instantiate(prefabTem, parent);
            if (isUI)
            {
                clone.rectTransform().anchoredPosition3D = prefabTem.rectTransform().anchoredPosition3D;
                clone.rectTransform().sizeDelta = prefabTem.rectTransform().sizeDelta;
                clone.rectTransform().anchorMin = prefabTem.rectTransform().anchorMin;
                clone.rectTransform().anchorMax = prefabTem.rectTransform().anchorMax;
                clone.transform.localRotation = Quaternion.identity;
                clone.transform.localScale = Vector3.one;
            }
            else
            {
                clone.transform.localPosition = prefabTem.transform.localPosition;
                clone.transform.localRotation = Quaternion.identity;
                clone.transform.localScale = Vector3.one;
            }
            return clone;
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
        #endregion
    }
}