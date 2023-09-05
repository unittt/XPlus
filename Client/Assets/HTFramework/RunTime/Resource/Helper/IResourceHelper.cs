using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using YooAsset;
using Object = UnityEngine.Object;

namespace HT.Framework
{
    /// <summary>
    /// 资源管理器的助手接口
    /// </summary>
    public interface IResourceHelper : IInternalModuleHelper
    {
        /// <summary>
        /// 所有AssetBundle资源包清单的名称
        /// </summary>
        string PackageName { get; }

        /// <summary>
        /// 当前最新的包裹版本
        /// </summary>
        string PackageVersion { get; }

        /// <summary>
        /// 已加载的所有场景【场景名称、场景】
        /// </summary>
        Dictionary<string, Scene> Scenes { get; }

        /// <summary>
        /// 是否完成初始化
        /// </summary>
        bool IsInitialized { get;}
        
        /// <summary>
        /// 初始化
        /// </summary>
        void Initialize(IUpdateHandler handler);
        
        /// <summary>
        /// 更新package
        /// </summary>
        /// <param name="handler"></param>
        void UpdatePackage(IUpdateHandler handler);
      
        
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
         UniTask<T> LoadAssetAsync<T>(ResourceInfoBase info, HTFAction<float> onLoading, bool isPrefab, Transform parent, bool isUI) where T : Object;
        /// <summary>
        /// 加载场景（异步）
        /// </summary>
        /// <param name="info">资源信息标记</param>
        /// <param name="sceneMode">场景加载模式</param>
        /// <param name="activateOnLoad">加载完毕时是否主动激活</param>
        /// <param name="onLoading">加载中事件</param>
        /// <returns>加载协程迭代器</returns>
        UniTask<Scene> LoadSceneAsync(SceneInfo info, LoadSceneMode sceneMode,bool activateOnLoad, HTFAction<float> onLoading);

        /// <summary>
        /// 加载子资源（异步）
        /// </summary>
        /// <param name="info">资源信息标记</param>
        /// <typeparam name="T">资源类型</typeparam>
        /// <returns></returns>
        UniTask<T> LoadSubAssetsAsync<T>(SubAssetInfo info) where T : Object;

        /// <summary>
        /// 加载原生文件二进制数据（异步）
        /// </summary>
        /// <param name="info">资源信息标记</param>
        /// <returns></returns>
        UniTask<byte[]> LoadRawFileDataAsync(YooAsset.AssetInfo info);
        
        /// <summary>
        /// 加载原生文件二文本数据（异步）
        /// </summary>
        /// <param name="info">资源信息标记</param>
        /// <returns></returns>
        UniTask<string> LoadRawFileTextAsync(YooAsset.AssetInfo info);
        
        /// <summary>
        /// 获取资源信息列表
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        YooAsset.AssetInfo[] GetAssetInfos(string tag);
        
        /// <summary>
        /// 卸载资源（异步，Resource模式：卸载未使用的资源，AssetBundle模式：卸载AB包）
        /// </summary>
        /// <param name="assetBundleName">AB包名称</param>
        /// <returns>卸载协程迭代器</returns>
        void UnLoadAsset(string assetBundleName);
        
        /// <summary>
        ///  释放资源
        /// </summary>
        /// <param name="obj"></param>
        void UnLoadAsset(Object obj);
        
        /// <summary>
        /// 卸载场景（异步）
        /// </summary>
        /// <param name="info">资源信息标记</param>
        /// <returns>卸载协程迭代器</returns>
        UniTask UnLoadScene(SceneInfo info);
        
        /// <summary>
        /// 卸载所有场景（异步）
        /// </summary>
        /// <returns>卸载协程迭代器</returns>
        UniTask UnLoadAllScene();
        
        /// <summary>
        /// 清理内存，释放空闲内存（异步）
        /// </summary>
        /// <returns>协程迭代器</returns>
        UniTask ClearMemory();

       
    }
}