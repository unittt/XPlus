using UnityEngine;

namespace GridMap
{
    public static class MapGlobal
    {
        /// <summary>
        /// 获得两个Rect相交的区域
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="overlapsArea"></param>
        /// <returns></returns>
        public static bool TryGetOverlapsArea(Rect a, Rect b, out Rect overlapsArea)
        {
            var min = Vector2.Max(a.min, b.min);
            var max = Vector2.Min(a.max, b.max);
            if (min.x < max.x && min.y < max.y)
            {
                overlapsArea = new Rect(min, max - min);
                return true;
            }
            overlapsArea = Rect.zero;
            return false;
        }

        /// <summary>
        /// 矩形是否相交
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool IsOverlaps(Rect a, Rect b)
        {
            var min = Vector2.Max(a.min, b.min);
            var max = Vector2.Min(a.max, b.max);
            return min.x < max.x && min.y < max.y;
        }

        /// <summary>
        /// 世界坐标 转换为 地图节点坐标
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="nodeSize"></param>
        /// <returns></returns>
        public static Vector2Int WorldToGridPos(this Vector2 pos, float nodeSize)
        {
            return new Vector2Int((int)(pos.x / nodeSize), (int)(pos.y / nodeSize));
        }
    }
}
