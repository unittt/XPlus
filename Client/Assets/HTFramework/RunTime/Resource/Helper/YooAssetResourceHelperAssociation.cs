using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YooAsset;

namespace HT.Framework
{

    /// <summary>
    /// Yooasset关联
    /// </summary>
    public sealed partial class YooAssetResourceHelper
    {
        //location LoadHandle
        private readonly Dictionary<string, LoadHandle> s_LoadHandleMap = new();
        //obj LoadHandle
        private readonly Dictionary<Object, LoadHandle> o_LoadHandleMap = new();
        //gameObjectClone  gameObject
        private readonly Dictionary<int, Object> instanceID_ObjectMap = new();
        //资源句柄的hashcode 资源句柄
        private readonly Dictionary<int, AssetOperationHandle> i_AssetOperationHandleMap = new();
        //场景名称  场景加载句柄
        private Dictionary<string, SceneOperationHandle> s_SceneOperationHandleMap = new();

        #region location 2 LoadHandle
        /// <summary>
        /// 根据定位获得LoadHandle
        /// </summary>
        /// <param name="location">定位</param>
        /// <param name="isSpawn">如果不存在是否生成</param>
        /// <returns></returns>
        private LoadHandle TryGetLoadHandleByLocation(string location,bool isSpawn)
        {
            if (!s_LoadHandleMap.TryGetValue(location, out  var handle) && isSpawn)
            {
                handle = Main.m_ReferencePool.Spawn<LoadHandle>();
                handle.SetLocation(location);
                s_LoadHandleMap.Add(location, handle);
            }
            return handle;
        }
        #endregion

        #region obj 2 LoadHandle
        /// <summary>
        /// 将obj 与 loadHandle 关联
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="loadHandle"></param>
        /// <returns></returns>
        private bool AddLoadHandle(Object obj,LoadHandle loadHandle)
        {
            return o_LoadHandleMap.TryAdd(obj, loadHandle);
        }

      
        /// <summary>
        /// 获得LoadHandle
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="handle"></param>
        /// <returns></returns>
        private bool TryGetLoadHandleByObj(Object obj,out LoadHandle handle)
        {
            return o_LoadHandleMap.TryGetValue(obj, out handle);
        }
        #endregion
      

        #region Clone 2 obj
        /// <summary>
        /// 将clone 与 prefab关联
        /// </summary>
        /// <param name="clone"></param>
        /// <param name="prefab"></param>
        private void AddClone(Object clone, Object prefab, LoadHandle handle)
        {
            var instanceID = clone.GetInstanceID();
            instanceID_ObjectMap.Add(clone.GetInstanceID(), prefab);
            handle.AddInstanceID(instanceID);
        }
        /// <summary>
        /// 获取object
        /// </summary>
        /// <param name="clone"></param>
        /// <returns></returns>
        private Object GetObjectByClone(Object clone, bool isRemove = true)
        {
            var instanceID = clone.GetInstanceID();
            var obj = instanceID_ObjectMap[instanceID];
            if (isRemove)
            {
                instanceID_ObjectMap.Remove(instanceID);
            }
            return obj;
        }
        #endregion
       

        /// <summary>
        /// 将hashCode 和 handle 关联
        /// </summary>
        /// <param name="handle"></param>
        private void AddAssetOperationHandle(AssetOperationHandle handle)
        {
            i_AssetOperationHandleMap.Add(handle.GetHashCode(),handle);
        }

        #region 场景
        /// <summary>
        /// 将场景名称 和 SceneOperationHandle 关联
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="handle"></param>
        private void AddSceneOperationHandle(string sceneName, SceneOperationHandle handle)
        {
            s_SceneOperationHandleMap.Add(sceneName, handle);
        }

        /// <summary>
        /// 获取SceneOperationHandle
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="handle"></param>
        /// <returns></returns>
        private bool TryGetSceneOperationHandle(string sceneName, out SceneOperationHandle handle)
        {
            return s_SceneOperationHandleMap.TryGetValue(sceneName, out handle);
        }

        /// <summary>
        /// 移除SceneOperationHandle
        /// </summary>
        /// <param name="sceneName"></param>
        private void RemoveSceneOperationHandle(string sceneName)
        {
            s_SceneOperationHandleMap.Remove(sceneName);
        }
        #endregion

        #region 释放
        /// <summary>
        /// 释放Handle
        /// </summary>
        /// <param name="handle"></param>
        private void ReleaseHandle(LoadHandle handle)
        {
            if (!handle.WaitAsync) return;
            
            s_LoadHandleMap.Remove(handle.Location);
            o_LoadHandleMap.Remove(handle.Object);
            foreach (var instanceID in handle.InstanceIDList)
            {
                instanceID_ObjectMap.Remove(instanceID);
            }

            if (i_AssetOperationHandleMap.ContainsKey(handle.Handle))
            {
                var assetOperationHandle = i_AssetOperationHandleMap[handle.Handle];
                assetOperationHandle.Release();
            }
            
            Main.m_ReferencePool.Despawn(handle);
        }

        /// <summary>
        /// 释放所有Handle
        /// </summary>
        private void ReleaseAllHandle()
        {
            var loadHandles = s_LoadHandleMap.Values.ToArray();
            foreach (var handle in loadHandles)
            {
                ReleaseHandle(handle);
            }
        }
        #endregion
    }
}
