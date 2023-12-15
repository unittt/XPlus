using System.Collections.Generic;
using UnityEngine;

namespace HT.Framework
{
    internal sealed class LoadHandle : IReference
    {
        internal string Location { get; private set; }
        internal int Handle { get; private set; }
        internal int RefCount { get; private set; }
        internal Object Object { get; private set; }
        /// <summary>
        /// 是否正在加载中
        /// </summary>
        internal bool WaitAsync { get; private set; }
        
        internal LinkedList<int> InstanceIDList { get; } = new();

        internal void SetLocation(string location)
        {
            Location = location;
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
        }

        internal void AddInstanceID(int instanceID)
        {
            InstanceIDList.AddLast(instanceID);
        }

        internal void RemoveInstanceID(int instanceID)
        {
            InstanceIDList.Remove(instanceID);
        }
        
        internal void SetWaitAsync(bool value)
        {
            WaitAsync = value;
        }
        
        public void Reset()
        {
            Location = string.Empty;
            Handle = 0;
            RefCount = 0;
            Object = null;
            WaitAsync = false;
            InstanceIDList.Clear();
        }
    }
}