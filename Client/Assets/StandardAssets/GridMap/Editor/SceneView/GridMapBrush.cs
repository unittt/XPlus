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

           var rect2 = new Rect
           {
               size = new Vector2(_gridMapManager.Width, _gridMapManager.Depth) * _gridMapManager.NodeSize,
               center = _gridMapManager.GraphCenter
           };
           
           //如果相交
           if (EditorGlobalTools.TryGetOverlapsArea(rect1, rect2, out var overlapRect))
           {
               GetTileIndexByWorldPos(overlapRect.min, out var x0, out var y0);
               GetTileIndexByWorldPos( overlapRect.max, out var x1, out var y1);
               
               for (var i = x0; i <= x1; i++)
               {
                   for (var j = y0; j <= y1; j++)
                   {
                       _gridMapManager.SetNodeWalkableAndTag(i,j,(int)_bursh);
                   }
               }
           }
           else
           {
               var mousePos =  EditorGlobalTools.GUIPointToWorldOrigin(current.mousePosition);
               if (rect2.Contains(mousePos))
               {
                   _gridMapManager.SetNodeWalkableAndTag(mousePos,(int)_bursh);
               }
           }
       }
       
       private bool GetTileIndexByWorldPos(Vector3 position, out int x, out int y)
       {
           var localPosition = _gridMapManager.transform.worldToLocalMatrix.MultiplyPoint(position);
           x = (int)(localPosition.x / _gridMapManager.NodeSize);
           y = (int)(localPosition.y / _gridMapManager.NodeSize);
       
           var isInside = x >= 0 && x < _gridMapManager.Width && y >= 0 && y < _gridMapManager.Depth;
           x = Mathf.Clamp(x, 0,  _gridMapManager.Width - 1);
           y = Mathf.Clamp(y, 0,  _gridMapManager.Depth - 1);
           return isInside;
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