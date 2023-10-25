using UnityEditor;
using UnityEngine;

namespace GridMap
{
    /// <summary>
    /// 笔刷
    /// </summary>
    public static class GridMapBrush
    {

        private static int GridMapBrushHashCode = "GridMapBrush".GetHashCode();
        public static BrushType Bursh;
        private static Vector3 mouseDownPosition;
        private static Vector3 mouseUpPosition;
        
       public static GridMapManager GridMapManager;
       
       private static bool isSelecting;
       private static Vector2 startMousePosition;
       private static Rect selectionRect;
       

       public static void Update()
       {
           UpdateMouse();
       }

       private static void UpdateMouse()
       {
           var e = Event.current;
           var controlID = GUIUtility.GetControlID(FocusType.Passive);
           var eventType = e.GetTypeForControl(controlID);
           if (eventType == EventType.MouseUp && e.button == 0)
           {
               OnMouseUp(e);
               GUIUtility.hotControl = controlID;
               e.Use();
               isSelecting = false;
           }
           else if (eventType == EventType.MouseDrag && e.button == 0)
           {
               OnMouseDrag(e);
               e.Use();
           }
           else if (eventType == EventType.MouseDown && e.button == 0)
           {
               OnMouseDown(e);
               GUIUtility.hotControl = 0;
               e.Use();
           }
           
           // 绘制选择框
           if (!isSelecting) return;
           Handles.BeginGUI();
           GUI.Box(selectionRect, "");

           Handles.EndGUI();
       }



       private static void OnMouseDown(Event current)
       {
           isSelecting = true;
           startMousePosition = current.mousePosition;
           selectionRect = new Rect(startMousePosition, Vector2.zero);
       }

       private static void OnMouseDrag(Event current)
       {
           // 更新选择框的位置和大小
           selectionRect.size = current.mousePosition - startMousePosition;
       }

       private static Vector2 GetWorldPosition(Vector2 mousePosition)
       {
           var worldRay = HandleUtility.GUIPointToWorldRay(mousePosition);
           return worldRay.origin;
       }
       

       private static void OnMouseUp(Event current)
       {
           if (!isSelecting)return;
           /* topLeft
            *       #########
            *       #########
            *       #########
            *               bottomRight
            */
           
           var bottomRight = MousePositionToWorld(selectionRect.min);
           var topLeft = MousePositionToWorld(selectionRect.max);
           
           //判断
           var rect1 = new Rect(
               Mathf.Min(bottomRight.x, topLeft.x),  // 左上角的x坐标
               Mathf.Min(bottomRight.y, topLeft.y),  // 左上角的y坐标
               Mathf.Abs(bottomRight.x - topLeft.x), // 宽度
               Mathf.Abs(bottomRight.y - topLeft.y)  // 高度
           );

           var rect2 = new Rect
           {
               size = new Vector2(GridMapManager.Width, GridMapManager.Depth) * GridMapManager.NodeSize,
               center = GridMapManager.GraphCenter
           };
           
           //如果相交
           if (Overlaps(rect1, rect2, out var overlapRect))
           {
               GetTileIndexByWorldPos(overlapRect.min, out var x0, out var y0);
               GetTileIndexByWorldPos( overlapRect.max, out var x1, out var y1);
               
               for (var i = x0; i <= x1; i++)
               {
                   for (var j = y0; j <= y1; j++)
                   {
                       GridMapManager.SetNodeWalkableAndTag(i,j,(int)Bursh);
                   }
               }
           }
           else
           {
               var mousePos = MousePositionToWorld(current.mousePosition);
               if (rect2.Contains(mousePos))
               {
                   GridMapManager.SetNodeWalkableAndTag(mousePos,(int)Bursh);
               }
           }
       }
       
       private static bool Overlaps(Rect a, Rect b, out Rect overlapsArea)
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

       
       private static bool GetTileIndexByWorldPos(Vector3 position, out int x, out int y)
       {
           var localPosition = GridMapManager.transform.worldToLocalMatrix.MultiplyPoint(position);
           x = (int)(localPosition.x / GridMapManager.NodeSize);
           y = (int)(localPosition.y / GridMapManager.NodeSize);
       
           var isInside = x >= 0 && x < GridMapManager.Width && y >= 0 && y < GridMapManager.Depth;
           x = Mathf.Clamp(x, 0,  GridMapManager.Width - 1);
           y = Mathf.Clamp(y, 0,  GridMapManager.Depth - 1);
           return isInside;
       }
       

       private static Vector2 MousePositionToWorld(Vector2 mousePosition)
       {
           var worldRay = HandleUtility.GUIPointToWorldRay(mousePosition);
           return worldRay.origin;
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