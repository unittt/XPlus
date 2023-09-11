using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using HT.Framework;
using UnityEngine;
using YooAsset;

namespace YooAssetPlus
{
    
    public sealed partial class YooAssetPlusManager
    {
        //这里是简单的本地记录YooAsset 根据你项目应该有一个资源管理器统一管理这里只是演示所以很简陋
        private Dictionary<int, AssetOperationHandle> m_AllHandle = new();
        private Dictionary<Object, Object> g_ObjectMap = new();
        
        
        private bool _isLoading;    //单线下载中
        private WaitUntil _loadWait;    //单线下载等待;
        
        partial void OnAwake()
        {
            _loadWait = new WaitUntil(() => !_isLoading);
        }


        #region 加载资源
        /// <summary>
        ///  加载数据集（异步）
        /// </summary>
        /// <param name="location"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async UniTask<T> LoadDataSetAsync<T>(string location) where T : DataSetBase
        {
            return await LoadAssetAsync<T>(location);
        }
        
       /// <summary>
       /// 加载预制体（异步）
       /// </summary>
       /// <param name="location"></param>
       /// <param name="parent"></param>
       /// <param name="isUI"></param>
       /// <returns></returns>
        public async UniTask<GameObject> LoadPrefabAsync(string location, Transform parent,bool isUI = false)
       {
           var prefab = await LoadAssetAsync<GameObject>(location);
           //实例化后
           return await LoadAssetAsync<GameObject>(location);
        }
       
        /// <summary>
        /// 异步获取原生文件文本
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public async UniTask<string> LoadRawFileTextAsync(string location)
        {
            var rawFileOperationHandle = YooAssets.LoadRawFileAsync(location);
            await rawFileOperationHandle.ToUniTask();

            var str = rawFileOperationHandle.GetRawFileText();
            //释放handle
            rawFileOperationHandle.Release();
            return str;
        }

        /// <summary>
        /// 异步获取原生文件二进制数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public async UniTask<byte[]> LoadRawFileDataAsync(string location)
        {
            var rawFileOperationHandle = YooAssets.LoadRawFileAsync(location);
            await rawFileOperationHandle.ToUniTask();

            var bytes = rawFileOperationHandle.GetRawFileData();
            //释放handle
            rawFileOperationHandle.Release();
            return bytes;
        }
        
        /// <summary>
        /// 加载资源
        /// </summary>
        /// <param name="location">路径</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private async UniTask<T> LoadAssetAsync<T>(string location) where T : Object
        {
            if (_isLoading)
            {
                await _loadWait;
            }
            _isLoading = true;
            var obj = await InternalLoadAssetAsync<T>(location);
            _isLoading = false;
            return obj;
        }
        
        /// <summary>
        /// 内部方法 用来资源加载
        /// </summary>
        /// <param name="location"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private async UniTask<T> InternalLoadAssetAsync<T>(string location) where T : Object
        {
            var load = LoadHelper.GetLoad("", location);
            var loadObj = load.Object;
            if (loadObj != null)
            {
                load.AddRefCount();
                return (T)loadObj;
            }
            //如果不存在
            var handle = YooAssets.LoadAssetAsync<T>(location);
            await handle.ToUniTask();

            var (obj, hashCode) = LoadAssetHandle(handle);
            if (obj == null)
            {
                load.RemoveRefCount();
                return null;
            }

            if (!LoadHelper.AddLoadHandle(obj, load))
            {
                return null;
            }
            load.ResetHandle(obj, hashCode);
            load.AddRefCount();
            return (T)obj;
        }


        private (Object, int) LoadAssetHandle(AssetOperationHandle handle)
        {
            if (handle.AssetObject != null)
            {
                var hashCode = handle.GetHashCode();
                m_AllHandle.Add(hashCode, handle);
                return (handle.AssetObject, hashCode);
            }
            else
            {
                handle.Release();
                return (null, 0);
            }
        }
        
        #endregion

        #region 释放资源
        /// <summary>
        /// 释放由 实例化出来的GameObject
        /// </summary>
        public  void ReleaseInstantiate(Object gameObject)
        {
            if (!g_ObjectMap.TryGetValue(gameObject, out var asset)) return;
            g_ObjectMap.Remove(gameObject);
            LoadHelper.GetLoadHandle(gameObject)?.RemoveRefCount();
        }
        
        
        /// 释放方法
        /// </summary>
        /// <param name="hashCode">加载时所给到的唯一ID</param>
        // private void ReleaseAction(int hashCode)
        // {
        //     if (m_AllHandle.TryGetValue(hashCode, out var value))
        //     {
        //         value.Release();
        //         m_AllHandle.Remove(hashCode);
        //     }
        //     else
        //     {
        //         Debug.LogError($"释放了一个未知Code");
        //     }
        // }
        
        #endregion
    }
}
