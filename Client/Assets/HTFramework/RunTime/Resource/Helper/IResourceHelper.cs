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
        /// 初始化package
        /// </summary>
        InitializationOperation InitPackage(string hostServerURL, string fallbackHostServerURL);


        /// <summary>
        /// 异步更新最新包的版本。
        /// </summary>
        /// <param name="appendTimeTicks">请求URL是否需要带时间戳。</param>
        /// <param name="timeout">超时时间。</param>
        /// <returns>请求远端包裹的最新版本操作句柄。</returns>
        UniTask<UpdatePackageVersionOperation> UpdatePackageVersionAsync(bool appendTimeTicks, int timeout);
        
        /// <summary>
        /// 向网络端请求并更新清单
        /// </summary>
        /// <param name="packageVersion">更新的包裹版本</param>
        /// <param name="autoSaveVersion">更新成功后自动保存版本号，作为下次初始化的版本。</param>
        /// <param name="timeout">超时时间（默认值：60秒）</param>
        UniTask<UpdatePackageManifestOperation>  UpdatePackageManifestAsync(bool autoSaveVersion, int timeout);
        
        /// <summary>
        ///  创建资源下载器，用于下载当前资源版本所有的资源包文件。
        /// </summary>
        /// <returns></returns>
        ResourceDownloaderOperation CreateResourceDownloader();
        
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
        UniTask<byte[]> LoadRawFileDataAsync(ResourceInfoBase info);
        
        /// <summary>
        /// 加载原生文件二文本数据（异步）
        /// </summary>
        /// <param name="info">资源信息标记</param>
        /// <returns></returns>
        UniTask<string> LoadRawFileTextAsync(ResourceInfoBase info);
        
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