using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace HT.Framework
{
    /// <summary>
    /// 资源管理器的助手接口
    /// </summary>
    public interface IResourceHelper : IInternalModuleHelper
    {
        
        /// <summary>
        /// 当前最新的包裹版本
        /// </summary>
        string PackageVersion { get; }
        
        /// <summary>
        /// 是否完成初始化
        /// </summary>
        bool IsInitialized { get;}

        /// <summary>
        /// 初始化完成
        /// </summary>
        event HTFAction<bool> InitializationCompleted; 

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
        /// <param name="location"></param>
        /// <param name="isPrefab">是否是加载预制体</param>
        /// <param name="parent">预制体加载完成后的父级</param>
        /// <param name="isUI">是否是加载UI</param>
        /// <typeparam name="T">资源类型</typeparam>
        /// <returns></returns>
        UniTask<T> LoadAssetAsync<T>(string location, bool isPrefab, Transform parent, bool isUI) where T : Object;
        
        
        /// <summary>
        /// 获取原生文件文本
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        UniTask<string> LoadRawFileTextAsync(string location);

        /// <summary>
        /// 获取原生文件二进制数据
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        UniTask<byte[]> LoadRawFileDataAsync(string location);

        
        /// <summary>
        /// 异步加载场景
        /// </summary>
        /// <param name="location">场景的定位地址</param>
        /// <param name="sceneMode">场景加载模式</param>
        /// <param name="suspendLoad">场景加载到90%自动挂起</param>
        UniTask<Scene> LoadSceneAsync(string location, HTFAction<float> onLoading = null,
            LoadSceneMode sceneMode = LoadSceneMode.Single, bool suspendLoad = false);


        /// <summary>
        /// 获取资源信息的路径列表
        /// </summary>
        /// <param name="tag">标签</param>
        /// <param name="isAddress">是否为可寻址路径</param>
        /// <returns></returns>
        string[] GetAssetPath(string tag, bool isAddress);
        
        /// <summary>
        ///  卸载资源
        /// </summary>
        /// <param name="obj"></param>
        void UnLoadAsset(Object obj);

        /// <summary>
        /// 释放场景
        /// </summary>
        /// <param name="sceneName"></param>
        /// <returns></returns>
        UniTask UnLoadScene(string sceneName);

        /// <summary>
        /// 清理内存，释放空闲内存（异步）
        /// </summary>
        /// <returns></returns>
        UniTask ClearMemory();
    }
}