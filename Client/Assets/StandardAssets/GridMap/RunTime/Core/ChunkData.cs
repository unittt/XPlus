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
        public bool IsLoaded => Entity is not null;

        
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
            Entity = null;
            Bounds = Rect.zero;
        }
    }
}
