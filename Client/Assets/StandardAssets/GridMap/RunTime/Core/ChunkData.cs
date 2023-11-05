using UnityEngine;

namespace GridMap
{
    /// <summary>
    /// 块数据
    /// </summary>
    internal sealed class ChunkData
    {
        /// <summary>
        /// 是否已加载
        /// </summary>
        public bool IsLoaded;

        
        public int X;
        public int Y;
        
        /// <summary>
        /// 界限
        /// </summary>
        public Rect Bounds;
        
        /// <summary>
        /// 实体
        /// </summary>
        public GameObject Entity;
        
        public void Release()
        {
            IsLoaded = false;
            Entity = null;
        }
    }
}
