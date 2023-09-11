using HT.Framework;
using UnityEngine;

namespace YooAssetPlus
{
    public class LoadHandle : IReference
    {
        internal string ResName { get; private set; }
        internal int Handle { get; private set; }
        internal int RefCount { get; private set; }
        internal Object Object { get; private set; }

        internal void SetGroupHandle(string resName)
        {
            ResName = resName;
        }

        public void Reset()
        {
            ResName = string.Empty;
            Handle = 0;
            RefCount = 0;
            Object = null;
        }

        internal void ResetHandle(Object obj, int handle)
        {
            Object = obj;
            Handle = handle;
        }

        internal void AddRefCount()
        {
            RefCount++;
        }

        internal void RemoveRefCount()
        {
            RefCount--;
            if (RefCount <= 0)
            {
                Release();
            }
        }

        private void Release()
        {
            // if (Handle != 0)
            //     YIUILoadDI.ReleaseAction?.Invoke(Handle);
            LoadHelper.PutLoad(ResName);
        }

        internal bool WaitAsync { get; private set; }

        internal void SetWaitAsync(bool value)
        {
            WaitAsync = value;
        }
    }
}