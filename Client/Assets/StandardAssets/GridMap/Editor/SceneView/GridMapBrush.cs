using UnityEditor;
using UnityEngine;

namespace GridMap
{
    /// <summary>
    /// 笔刷
    /// </summary>
    public sealed partial class GridMapSceneView
    {
        
        private BrushType _bursh; 
        private bool _isSelecting;
        private  Vector2 _startMousePosition;
        private  Rect _selectionRect;
        
       private void OnShow()
       {
           _isSelecting = false;
           SceneView.duringSceneGui += OnSceneGUI; 
       }

       private void OnHide()
       {
           SceneView.duringSceneGui -= OnSceneGUI; 
       }

       private void OnSceneGUI(SceneView obj)
       {
           var e = Event.current;
           var controlID = GUIUtility.GetControlID(FocusType.Passive);
           var eventType = e.GetTypeForControl(controlID);
           if (eventType == EventType.MouseUp && e.button == 0)
           {
               OnMouseUp(e);
               GUIUtility.hotControl = controlID;
               e.Use();
               _isSelecting = false;
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
           if (!_isSelecting) return;
           Handles.BeginGUI();
           GUI.Box(_selectionRect, "");

           Handles.EndGUI();
       }

       
       private void OnMouseDown(Event current)
       {
           _isSelecting = true;
           _startMousePosition = current.mousePosition;
           _selectionRect = new Rect(_startMousePosition, Vector2.zero);
       }

       private void OnMouseDrag(Event current)
       {
           // 更新选择框的位置和大小
           _selectionRect.size = current.mousePosition - _startMousePosition;
       }

       private Vector2 GetWorldPosition(Vector2 mousePosition)
       {
           var worldRay = HandleUtility.GUIPointToWorldRay(mousePosition);
           return worldRay.origin;
       }
       

       private void OnMouseUp(Event current)
       {
           if (!_isSelecting)return;
           /* topLeft
            *       #########
            *       #########
            *       #########
            *               bottomRight
            */
           
           var bottomRight = EditorGlobalTools.GUIPointToWorldOrigin(_selectionRect.min);
           var topLeft =  EditorGlobalTools.GUIPointToWorldOrigin(_selectionRect.max);
           
           //判断
           var rect1 = new Rect(
               Mathf.Min(bottomRight.x, topLeft.x),  // 左上角的x坐标
               Mathf.Min(bottomRight.y, topLeft.y),  // 左上角的y坐标
               Mathf.Abs(bottomRight.x - topLeft.x), // 宽度
               Mathf.Abs(bottomRight.y - topLeft.y)  // 高度
           );

           var nodeSize = mapManager.NodeSize;
           var rect2 = new Rect
           {
               size = new Vector2(mapManager.NodeWidth, mapManager.NodeDepth) * nodeSize,
               center = mapManager.GraphCenter
           };
           
           //如果相交
           if (EditorGlobalTools.TryGetOverlapsArea(rect1, rect2, out var overlapRect))
           {
               var width = (int)(overlapRect.size.x /nodeSize);
               var high =(int)(overlapRect.size.y / nodeSize);

               for (var i = 0; i <= width; i++)
               {
                   for (var j = 0; j <= high; j++)
                   {
                       var pos = overlapRect.min + new Vector2(mapManager.NodeSize * i, mapManager.NodeSize * j);
                       mapManager.SetNodeWalkableAndTag(pos, (int)_bursh);
                   }
               }
           }
           else
           {
               var mousePos =  EditorGlobalTools.GUIPointToWorldOrigin(current.mousePosition);
               if (rect2.Contains(mousePos))
               {
                   mapManager.SetNodeWalkableAndTag(mousePos,(int)_bursh);
               }
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