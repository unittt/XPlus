using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using YooAsset;

namespace HT.Framework
{
    /// <summary>
    /// 资源管理器
    /// </summary>
    [InternalModule(HTFrameworkModule.Resource)]
    public sealed class ResourceManager : InternalModuleBase<IResourceHelper>
    {
        /// <summary>
        /// 资源加载模式【请勿在代码中修改】
        /// </summary>
        [SerializeField] internal EPlayMode Mode = EPlayMode.EditorSimulateMode;
        /// <summary>
        /// 所有AssetBundle资源包清单的名称【请勿在代码中修改】
        /// </summary>
        [SerializeField] internal string PackageName;
       
        
        [SerializeField] internal string DefaultHostServer = "http://127.0.0.1:8081";
        [SerializeField] internal string FallbackHostServer = "http://127.0.0.1:8081";
        /// <summary>
        /// 是否手动初始化【请勿在代码中修改】
        /// </summary>
        [SerializeField] internal bool Manually;
        
        /// <summary>
        /// 当前的资源加载模式
        /// </summary>
        public EPlayMode PlayMode => Mode;
        /// <summary>
        /// 是否手动初始化
        /// </summary>
        public bool IsManually => Manually;
        public string PackageVersion => _helper.PackageVersion;

        /// <summary>
        /// 是否初始化完成
        /// </summary>
        public bool IsInitialized => _helper.IsInitialized;
        /// <summary>
        /// 初始化完成
        /// </summary>
        public event HTFAction<bool> InitializationCompleted;

        public override void OnInit()
        {
            base.OnInit();
            _helper.InitializationCompleted += (result) =>
            {
                InitializationCompleted?.Invoke(result);
            };
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="updateHandler"></param>
        public void Initialize(IUpdateHandler updateHandler)
        {
            _helper.Initialize(updateHandler);
        }

        /// <summary>
        /// 更新package
        /// </summary>
        /// <param name="handler"></param>
        public void UpdatePackage(IUpdateHandler handler)
        {
            _helper.UpdatePackage(handler);
        }
        
        /// <summary>
        /// 加载资源（异步）
        /// </summary>
        /// <param name="location"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async UniTask<T> LoadAsset<T>(string location) where T : Object
        {
            return await _helper.LoadAssetAsync<T>(location, false, null, false);
        }
        
        /// <summary>
        /// 加载预制体（异步）
        /// </summary>
        /// <param name="location"></param>
        /// <param name="parent">预制体的预设父物体</param>
        /// <param name="isUI">预制体是否是U</param>
        /// <returns></returns>
        public async UniTask<GameObject> LoadPrefab(string location, Transform parent, bool isUI = false)
        {
            return await _helper.LoadAssetAsync<GameObject>(location, true, parent, isUI);
        }
        
        /// <summary>
        /// 加载原生文件文本（异步）
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public async UniTask<string> LoadRawFileText(string location)
        {
            return await _helper.LoadRawFileTextAsync(location);
        }
        
        /// <summary>
        /// 获取原生文件二进制数据
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public async UniTask<byte[]> LoadRawFileData(string location)
        {
            return await _helper.LoadRawFileDataAsync(location);
        }

        /// <summary>
        /// 加载资源 （异步）
        /// </summary>
        /// <param name="tag">标签</param>
        /// <param name="isAddress">是否为可寻址路径</param>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public async UniTask<Dictionary<string, T>> LoadAssetByTag<T>(string tag,bool isAddress) where T : Object
        {
            var entities = new Dictionary<string, T>();
            var paths = _helper.GetAssetPath(tag, isAddress);
            foreach (var location in paths)
            {
                var obj = await _helper.LoadAssetAsync<T>(location, false, null, false);
                entities.Add(location,obj);
            }
            return entities;
        }

        /// <summary>
        ///  加载原生文件文本（异步）
        /// </summary>
        /// <param name="tag">标签</param>
        /// <param name="isAddress">是否为可寻址路径</param>
        /// <returns></returns>
        public async UniTask<Dictionary<string, string>> LoadRawFileTextByTag<T>(string tag, bool isAddress)
        {
            var entities = new Dictionary<string, string>();
            var paths = _helper.GetAssetPath(tag, isAddress);
            foreach (var location in paths)
            {
                var rawFileText = await _helper.LoadRawFileTextAsync(location);
                entities.Add(location,rawFileText);
            }
            return entities;
        }
        
        /// <summary>
        /// 获取原生文件二进制数据（异步）
        /// </summary>
        /// <param name="tag">标签</param>
        /// <param name="isAddress">是否为可寻址路径</param>
        /// <returns></returns>
        public async UniTask<Dictionary<string, byte[]>> LoadRawFileDataByTag(string tag, bool isAddress)
        {
            var entities = new Dictionary<string, byte[]>();
            var paths = _helper.GetAssetPath(tag, isAddress);
            foreach (var location in paths)
            {
                var rawFileData = await _helper.LoadRawFileDataAsync(location);
                entities.Add(location, rawFileData);
            }

            return entities;
        }

        /// <summary>
        /// 加载场景
        /// </summary>
        /// <param name="location">场景的定位地址</param>
        /// <param name="onLoading">加载进度</param>
        /// <param name="sceneMode">场景加载模式</param>
        /// <param name="suspendLoad">景加载到90%自动挂起</param>
        /// <param name="priority">优先级</param>
        /// <returns></returns>
        public async UniTask<Scene> LoadSceneAsync(string location, HTFAction<float> onLoading = null,
            LoadSceneMode sceneMode = LoadSceneMode.Single, bool suspendLoad = false, int priority = 100)
        {
            return await _helper.LoadSceneAsync(location, onLoading,sceneMode, suspendLoad, priority);
        }

        /// <summary>
        /// 卸载场景
        /// </summary>
        /// <param name="sceneName">场景名称</param>
        public async UniTask UnLoadScene(string sceneName)
        {
             await _helper.UnLoadScene(sceneName);
        }
        
        /// <summary>
        ///  卸载资源
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="isClone"></param>
        public void UnLoadAsset(Object obj)
        {
            _helper.UnLoadAsset(obj);
        }

        /// <summary>
        ///  清理内存，释放空闲内存（异步）
        /// </summary>
        public async UniTask ClearMemory()
        {
            await _helper.ClearMemory();
        }
    }
}