using HT.Framework;
using System.Collections.Generic;

namespace YooAssetPlus
{
    /// <summary>
    /// 地址 和 LoadHandle关联
    /// </summary>
    internal static partial class LoadHelper
    {
        private static readonly Dictionary<string, LoadHandle> m_AllLoadDic = new();

        internal static LoadHandle GetLoad(string resName)
        {
            if (m_AllLoadDic.ContainsKey(resName)) return m_AllLoadDic[resName];
            
            var handle = Main.m_ReferencePool.Spawn<LoadHandle>();
            handle.SetGroupHandle(resName);
            m_AllLoadDic.Add(resName, handle);
            return m_AllLoadDic[resName];
        }

        internal static bool PutLoad(string resName)
        {
            if (!m_AllLoadDic.ContainsKey(resName))
            {
                return false;
            }
            var load = m_AllLoadDic[resName];
            m_AllLoadDic.Remove(resName);
            RemoveLoadHandle(load);
            Main.m_ReferencePool.Despawn(load);
            return true;
        }

        internal static void PutAll()
        {
            foreach (var handle in m_AllLoadDic.Values)
            {
                Main.m_ReferencePool.Despawn(handle);
            }
            m_AllLoadDic.Clear();
            m_ObjLoadHandle.Clear();
        }
    }
}

