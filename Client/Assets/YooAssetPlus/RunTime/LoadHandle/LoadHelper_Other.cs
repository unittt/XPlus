using UnityEngine;

namespace YooAssetPlus
{
    internal static partial class LoadHelper
    {
        /// <summary>
        /// 资源验证
        /// </summary>
        internal static bool VerifyAssetValidity(string pkgName, string resName)
        {
            return false;
            // return YIUILoadDI.VerifyAssetValidityFunc(pkgName, resName);
        }

        /// <summary>
        /// 释放某个资源对象
        /// 不包含实例化的对象
        /// 实例化的对象请调用另外一个实例化释放 ReleaseInstantiate
        /// </summary>
        internal static void Release(Object obj)
        {
            LoadHelper.GetLoadHandle(obj)?.RemoveRefCount();
        }

        /// <summary>
        /// 一键释放所有 慎用
        /// </summary>
        internal static void ReleaseAll()
        {
            LoadHelper.PutAll();
            // YIUILoadDI.ReleaseAllAction?.Invoke();
        }
    }
}
