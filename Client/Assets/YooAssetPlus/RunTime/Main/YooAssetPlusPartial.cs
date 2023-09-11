using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using YooAsset;

namespace YooAssetPlus
{
    public sealed partial class YooAssetPlusManager
    {
        //这里是简单的本地记录YooAsset
        private Dictionary<int, AssetOperationHandle> m_AllHandle = new();
        
        /// <summary>
        /// 加载预制件
        /// </summary>
        /// <param name="location"></param>
        /// <param name="parent"></param>
        /// <param name="isUI"></param>
        /// <returns></returns>
        public async UniTask<GameObject> LoadPrefab(string location, Transform parent, bool isUI)
        {
            var prefab = await LoadAssetAsync<GameObject>(location);
            //实例化预制件
            var result = GameObject.Instantiate(prefab);
            //关联预制件和loadhandle
            return result;
        }
        
        public async UniTask<T> LoadAssetAsync<T>(string location) where T : Object
        {
            T result = null; // 用于存储要返回的结果

            var loadHandle = LoadHelper.GetLoad(location);
            var loadObj = loadHandle.Object;

            if (loadObj != null)
            {
                result = (T)loadObj;
            }
            else
            {
                var assetOperationHandle = YooAssets.LoadAssetAsync<T>(location);
                await assetOperationHandle.ToUniTask();

                var (obj, hashCode) = GetLoadAssetByHandle(assetOperationHandle);

                if (obj != null)
                {
                    if (LoadHelper.AddLoadHandle(obj, loadHandle))
                    {
                        loadHandle.ResetHandle(obj, hashCode);
                        result = (T)obj;
                    }
                }
            }

            if (result != null)
            {
                loadHandle.AddRefCount(); // 只在成功加载资源后增加引用计数
            }

            return result; // 在方法的最后返回结果
        }
        
        
        /// <summary>
        /// 获取资源
        /// </summary>
        /// <param name="handle">句柄</param>
        /// <returns></returns>
        private (Object, int) GetLoadAssetByHandle(AssetOperationHandle handle)
        {
            if (handle.AssetObject != null)
            {
                var hashCode = handle.GetHashCode();
                m_AllHandle.Add(hashCode, handle);
                return (handle.AssetObject, hashCode);
            }
            handle.Release();
            return (null, 0);
        }
        
        
        /// <summary>
        /// 异步获取原生文件文本
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        async UniTask<string> LoadRawFileTextAsync(string location)
        {
            return "";
        }
        
        /// <summary>
        /// 异步获取原生文件二进制数据
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        async UniTask<byte[]> LoadRawFileDataAsync(string location)
        {
            return null;
        }


        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="isInstantiate">是否为实例化的gameObject</param>
        public void Release(Object obj, bool isInstantiate = false)
        {
            if (isInstantiate)
            {
                obj = LoadHelper.RemoveInMap(obj);
            }
            LoadHelper.Release(obj);
        }
    }
}
