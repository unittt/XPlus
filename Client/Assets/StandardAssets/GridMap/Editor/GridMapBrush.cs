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
        private static Vector3 mouseDownPosition;
        private static Vector3 mouseUpPosition;
        private static bool isMouseDragging = false;

        public static void Update(GridMapManager gridMapManager)
        {
            if (Event.current.type == EventType.MouseDown)
            {
                // 在鼠标按下时执行操作
                Ray worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                gridMapManager.SetNodeWalkableAndTag(worldRay.origin,(int)Bursh);
            }
            
            Event e = Event.current;
            if (e.type == EventType.MouseDown && e.button == 0)
            {
                mouseDownPosition = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin;
                isMouseDragging = true;
            }

            if (isMouseDragging && e.type == EventType.MouseDrag)
            {
                // 鼠标拖动中的操作
                mouseUpPosition = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin;

                // 计算并绘制矩形
                Handles.DrawWireCube((mouseDownPosition + mouseUpPosition) * 0.5f, new Vector3(Mathf.Abs(mouseDownPosition.x - mouseUpPosition.x), Mathf.Abs(mouseDownPosition.y - mouseUpPosition.y), 0));

                // 根据需要执行其他操作
            }

            if (e.type == EventType.MouseUp && e.button == 0)
            {
                isMouseDragging = false;
                // 鼠标释放后的操作
            }

        }
    }

    /// <summary>
    /// 笔刷类似
    /// </summary>
    public enum BrushType
    {
        /// <summary>
        /// 清理
        /// </summary>
        None,
        /// <summary>
        /// 行走
        /// </summary>
        Walk,
        /// <summary>
        /// 透明
        /// </summary>
        Transparent,
    }
}