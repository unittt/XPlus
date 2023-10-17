using UnityEditor;
using UnityEngine;

namespace GridMap
{
    /// <summary>
    /// 笔刷
    /// </summary>
    public static class GridMapBrush
    {

        public static BrushType Bursh;


        public static void Update(GridMapManager gridMapManager)
        {
            if (Event.current.type == EventType.MouseDown)
            {
                // 在鼠标按下时执行操作
                Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                gridMapManager.Xxxxxxx(worldRay.origin,(int)Bursh);
            }
        }
    }

    /// <summary>
    /// 笔刷类似
    /// </summary>
    public enum BrushType
    {
        /// <summary>
        /// 行走
        /// </summary>
        Walk,
        /// <summary>
        /// 透明
        /// </summary>
        Transparent,
        /// <summary>
        /// 清理
        /// </summary>
        Clear
    }
}