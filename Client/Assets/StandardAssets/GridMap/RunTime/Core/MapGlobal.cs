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
    }
}
