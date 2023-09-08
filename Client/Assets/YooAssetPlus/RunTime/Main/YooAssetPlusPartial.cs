using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using YooAsset;

namespace YooAssetPlusA
{
    
    public sealed partial class YooAssetPlusManager
    {
        //这里是简单的本地记录YooAsset 根据你项目应该有一个资源管理器统一管理这里只是演示所以很简陋
        private Dictionary<int, AssetOperationHandle> m_AllHandle = new Dictionary<int, AssetOperationHandle>();
        

        public async UniTask<T> LoadAssetAsync<T>(string location) where T : Object
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

            var (obj,hashCode) = LoadAssetHandle(handle);
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
        

        public async UniTask<T> LoadAssetAsync<T>(string location, bool isPrefab, Transform parent, bool isUI)
            where T : Object
        {
            return null;
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
    }
}
