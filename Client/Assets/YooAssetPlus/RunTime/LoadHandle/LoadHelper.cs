using HT.Framework;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YooAssetPlusA
{
    internal static partial class LoadHelper
    {
        private static string s_NullPkgName = "Default";

        private static Dictionary<string, Dictionary<string, LoadHandle>> m_AllLoadDic =
            new Dictionary<string, Dictionary<string, LoadHandle>>();

        internal static LoadHandle GetLoad(string pkgName, string resName)
        {
            if (string.IsNullOrEmpty(pkgName))
            {
                pkgName = s_NullPkgName;
            }

            if (!m_AllLoadDic.ContainsKey(pkgName))
            {
                m_AllLoadDic.Add(pkgName, new Dictionary<string, LoadHandle>());
            }

            var pkgDic = m_AllLoadDic[pkgName];

            if (!pkgDic.ContainsKey(resName))
            {
                var group = Main.m_ReferencePool.Spawn<LoadHandle>();
                group.SetGroupHandle(pkgName, resName);
                pkgDic.Add(resName, group);
            }

            return pkgDic[resName];
        }

        internal static bool PutLoad(string pkgName, string resName)
        {
            if (string.IsNullOrEmpty(pkgName))
            {
                pkgName = s_NullPkgName;
            }

            if (!m_AllLoadDic.ContainsKey(pkgName))
            {
                return false;
            }

            var pkgDic = m_AllLoadDic[pkgName];

            if (!pkgDic.ContainsKey(resName))
            {
                return false;
            }

            var load = pkgDic[resName];
            pkgDic.Remove(resName);
            RemoveLoadHandle(load);
            Main.m_ReferencePool.Despawn(load);
            return true;
        }

        internal static void PutAll()
        {
            foreach (var pkgDic in m_AllLoadDic.Values)
            {
                foreach (var load in pkgDic.Values)
                {
                    Main.m_ReferencePool.Despawn(load);
                }
            }

            m_AllLoadDic.Clear();
            m_ObjLoadHandle.Clear();
        }
    }

}

