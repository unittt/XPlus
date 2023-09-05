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
            if (IsManually) return;
            var updateHandler = new DefaultUpdateHandler
            {
                PackageName = PackageName,
                Mode = Mode,
                DefaultHostServer = DefaultHostServer,
                FallbackHostServer = FallbackHostServer,
                IsDefaultPackage = true
            };
            Initialize(updateHandler);
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


        public async UniTask<T> LoadAssetAsync<T>(ResourceInfoBase info, HTFAction<float> onLoading) where T : Object
        {
            return await _helper.LoadAssetAsync<T>(info, onLoading, false, null,false);
        }
        
        /// <summary>
        /// 加载数据集（异步）
        /// </summary>
        /// <typeparam name="T">数据集类型</typeparam>
        /// <param name="info">数据集配置信息</param>
        /// <param name="onLoading">数据集加载中回调</param>
        /// <param name="onLoadDone">数据集加载完成回调</param>
        /// <returns>加载协程</returns>
        public async UniTask<T> LoadDataSet<T>(DataSetInfo info, HTFAction<float> onLoading = null, HTFAction<T> onLoadDone = null) where T : DataSetBase
        {
            return await _helper.LoadAssetAsync<T>(info, onLoading, false, null,false);
        }
        /// <summary>
        /// 加载预制体（异步）
        /// </summary>
        /// <param name="info">预制体配置信息</param>
        /// <param name="parent">预制体的预设父物体</param>
        /// <param name="onLoading">预制体加载中回调</param>
        /// <param name="isUI">预制体是否是UI</param>
        /// <returns>加载协程</returns>
        public async UniTask<GameObject> LoadPrefab(PrefabInfo info, Transform parent, HTFAction<float> onLoading = null,bool isUI = false)
        {
            return await _helper.LoadAssetAsync<GameObject>(info, onLoading, true, parent, isUI);
        }
        
        /// <summary>
        /// 加载场景（异步）
        /// </summary>
        /// <param name="info">资源信息标记</param>
        /// <param name="sceneMode">场景加载模式</param>
        /// <param name="activateOnLoad">加载完毕时是否主动激活</param>
        /// <param name="onLoading">加载中事件</param>
        /// <returns>加载协程迭代器</returns>
        public async UniTask<Scene> LoadScene(SceneInfo info, LoadSceneMode sceneMode,bool activateOnLoad, HTFAction<float> onLoading = null)
        {
            return await _helper.LoadSceneAsync(info, sceneMode, activateOnLoad,onLoading);
        }
        
        /// <summary>
        /// 异步加载子资源对象
        /// </summary>
        /// <param name="info"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async UniTask<T> LoadSubAssetsAsync<T>(SubAssetInfo info) where T : Object
        {
            return await _helper.LoadSubAssetsAsync<T>(info);
        }

        /// <summary>
        /// 异步获取原生文件二进制数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async UniTask<byte[]> LoadRawFileDataAsync(YooAsset.AssetInfo info)
        {
            return await _helper.LoadRawFileDataAsync(info);
        }

        /// <summary>
        /// 异步获取原生文件文本
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async  UniTask<string> LoadRawFileTextAsync(YooAsset.AssetInfo info)
        {
            return await _helper.LoadRawFileTextAsync(info);
        }
        
        public YooAsset.AssetInfo[] GetAssetInfos(string tag)
        {
            return _helper.GetAssetInfos(tag);
        }

        /// <summary>
        /// 卸载资源（异步，Resource模式：卸载未使用的资源，AssetBundle模式：卸载AB包）
        /// </summary>
        /// <param name="assetBundleName">AB包名称</param>
        /// <param name="unloadAllLoadedObjects">是否同时卸载所有实体对象</param>
        /// <returns>卸载协程</returns>
        public void UnLoadAsset(string assetBundleName)
        {
            _helper.UnLoadAsset(assetBundleName);
        }

        /// <summary>
        ///  释放资源
        /// </summary>
        /// <param name="obj"></param>
        public void UnLoadAsset(Object obj)
        {
            _helper.UnLoadAsset(obj);
        }
        
        /// <summary>
        /// 卸载场景（异步）
        /// </summary>
        /// <param name="info">场景配置信息</param>
        /// <returns>卸载协程</returns>
        public async UniTask UnLoadScene(SceneInfo info)
        {
            await _helper.UnLoadScene(info);
        }
        /// <summary>
        /// 卸载所有场景（异步）
        /// </summary>
        /// <returns>卸载协程</returns>
        public async UniTask UnLoadAllScene()
        {
            await _helper.UnLoadAllScene();
        }
        /// <summary>
        /// 清理内存，释放空闲内存（异步）
        /// </summary>
        /// <returns>协程</returns>
        public async UniTask ClearMemory()
        {
            await _helper.ClearMemory();
        }
    }
}