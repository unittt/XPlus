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
        private static bool isMouseDragging = false;

        private static int cursorX0 = 0, cursorY0 = 0;
        private static int cursorX = 0,cursorY = 0;
        
       private static bool pencilDragActive;

       public static GridMapManager GridMapManager;

       private static Vector2 _mouseDownPosition;
       private static bool _isMouseDown;
       
       

       public static void Update()
       {

           UpdateMouse();
       }

       private static void UpdateMouse()
       {
            var controlID = GridMapBrushHashCode;
           var controlEventType = Event.current.GetTypeForControl(controlID);
           switch (controlEventType)
           {
               case EventType.MouseDown:
               case EventType.MouseDrag:
                   if ((controlEventType == EventType.MouseDrag && GUIUtility.hotControl != controlID) ||
                       (Event.current.button != 0 && Event.current.button != 1))
                   {
                       return;
                   }

                   if (controlEventType == EventType.MouseDown)
                   {
                       _mouseDownPosition = Event.current.mousePosition;
                       OnMouseDown(Event.current);
                   }
                   
                   bool inhibitMouseDown = false;
                   if (Application.platform == RuntimePlatform.OSXEditor)
                   {
                       if (Event.current.command && Event.current.alt)
                       {
                           // pan combination on mac
                           inhibitMouseDown = true;
                       }
                   }

                   if (Event.current.type == EventType.MouseDown && !inhibitMouseDown)
                   {
                       if (!Event.current.shift)
                       {
                           GUIUtility.hotControl = controlID;
                           PencilDrag();
                         
                       }
                   }

                   if (Event.current.type == EventType.MouseDrag && GUIUtility.hotControl == controlID)
                   {
                       PencilDrag();
                       OnMouseDrag(Event.current);
                   }

                   break;

               case EventType.MouseUp:
                   if ((Event.current.button == 0 || Event.current.button == 1) && GUIUtility.hotControl == controlID)
                   {
                       GUIUtility.hotControl = 0;
                       // RectangleDragEnd();

                       cursorX0 = cursorX;
                       cursorY0 = cursorY;


                       OnMouseUp(Event.current);
                       HandleUtility.Repaint();
                   }

                   break;

               case EventType.Layout:
                   //HandleUtility.AddDefaultControl(controlID);
                   break;

               case EventType.MouseMove:
                   // UpdateCursorPosition();
                   cursorX0 = cursorX;
                   cursorY0 = cursorY;
                   break;
           }
       }
       private static void PencilDrag()
       {
           pencilDragActive = true;
       }


       private static void OnMouseDown(Event current)
       {
           _mouseDownPosition = current.mousePosition;
       }

       private static void OnMouseDrag(Event current)
       {
           var mousePosition = current.mousePosition;
           var pos0 = GetWorldPosition(_mouseDownPosition);
           var pos1 = GetWorldPosition(mousePosition);

           Vector3[] v = new Vector3[4];
           v[0] = new Vector3(pos0.x, pos0.y, 0);
           v[1] = new Vector3(pos1.x, pos0.y, 0);
           v[2] = new Vector3(pos1.x, pos1.y, 0);
           v[3] = new Vector3(pos0.x, pos1.y, 0);
           Handles.DrawPolyLine(v);
           Debug.Log("拖拽中");
       }

       private static Vector2 GetWorldPosition(Vector2 mousePosition)
       {
           var worldRay = HandleUtility.GUIPointToWorldRay(mousePosition);
           return worldRay.origin;
       }
       

       private static void OnMouseUp(Event current)
       {
           var mousePosition = current.mousePosition;

    
       }
       
       
       // var controlID = GridMapBrushHashCode;
          //   var current = Event.current;
          //   var controlEventType = Event.current.GetTypeForControl(controlID);
          //
          //   switch (controlEventType)
          //   {
          //       case EventType.MouseDown:
          //           if (controlEventType == EventType.MouseDown && IsInside(out var point))
          //           {
          //               _mouseDownPoint = point;
          //               _isMouseDown = true;
          //               Debug.Log("鼠标按下");
          //           }
          //           break;
          //       
          //       case EventType.MouseUp:
          //
          //           if (_isMouseDown)
          //           {
          //               _isMouseDown = false;
          //
          //               var worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
          //               GridMapManager.GetTileIndexByWorldPos(worldRay.origin, out var x, out var y);
          //               IsInside(out var upPoint);
          //               Debug.Log(_mouseDownPoint + "     " + upPoint);
          //               if (_mouseDownPoint == upPoint)
          //               {
          //                   GridMapManager.SetNodeWalkableAndTag(worldRay.origin, (int)Bursh);
          //               }
          //               else
          //               {
          //                   int x0 = Mathf.Min(_mouseDownPoint.x, upPoint.x);
          //                   int x1 = Mathf.Max(_mouseDownPoint.x, upPoint.x);
          //                   int y0 = Mathf.Min(_mouseDownPoint.y, upPoint.y);
          //                   int y1 = Mathf.Max(_mouseDownPoint.y, upPoint.y);
          //
          //
          //                   for (int i = x0; i < x1; i++)
          //                   {
          //                       for (int j = y0; j < y1; j++)
          //                       {
          //                           GridMapManager.SetNodeWalkableAndTag(new Vector2(i, j), (int)Bursh);
          //                       }
          //                   }
          //               }
          //           }
          //
          //
          //           // current.Use();
          //           break;
          //
          //       case EventType.Layout:
          //           HandleUtility.AddDefaultControl(controlID);
          //           break;
          //   }
        
        
        
     
       static  void RectangleDragEnd()
    {
        if (!pencilDragActive)
            return;

        // if (RectangleDragSize() > 50)
        // {
        //     Undo.RegisterCompleteObjectUndo(tileMap.GridRefTemp, "Edit tile map");
        // }
        // else
        // {
        //     Undo.RecordObject(tileMap.GridRefTemp, "Edit tile map");
        // }

        if ((cursorX == cursorX0) && (cursorY == cursorY0))
        {
            // if (brushMode == BrushMode.WalkableBrush)
            // {
            //     tileMap.SetColorChannel(cursorX, cursorY, clickColorValue, GridMapBrushTool.ColorChannel.R);
            // }
            // else if (brushMode == BrushMode.FlyableBrush)
            // {
            //     //行走区域是飞行区域的子集,所以飞行区域填充黄色
            //     tileMap.SetColorChannel(cursorX, cursorY, clickColorValue, GridMapBrushTool.ColorChannel.G);
            // }
            // else if (brushMode == BrushMode.Transparent)
            // {
            //     tileMap.SetColorChannel(cursorX, cursorY, clickColorValue, GridMapBrushTool.ColorChannel.B);
            // }
            // else if (brushMode == BrushMode.Erase)
            // {
            //     tileMap.SetColor(cursorX, cursorY, Color.black);
            // }
            
            GridMapManager.SetNodeWalkableAndTag(new Vector2(cursorX,cursorY),(int)Bursh);
        }
        else
        {
            int x0 = Mathf.Min(cursorX, cursorX0);
            int x1 = Mathf.Max(cursorX, cursorX0);
            int y0 = Mathf.Min(cursorY, cursorY0);
            int y1 = Mathf.Max(cursorY, cursorY0);


            for (int i = x0; i < x1; i++)
            {
                for (int j = y0; j < y1; j++)
                {
                    GridMapManager.SetNodeWalkableAndTag(new Vector2(i,j),(int)Bursh);
                }
            }
            
            // if (brushMode == BrushMode.WalkableBrush)
            // {
            //     tileMap.SetRectColorChannel(x0, y0, x1, y1, clickColorValue, GridMapBrushTool.ColorChannel.R);
            // }
            // else if (brushMode == BrushMode.FlyableBrush)
            // {
            //     tileMap.SetRectColorChannel(x0, y0, x1, y1, clickColorValue, GridMapBrushTool.ColorChannel.G);
            // }
            // else if (brushMode == BrushMode.Transparent)
            // {
            //     tileMap.SetRectColorChannel(x0, y0, x1, y1, clickColorValue, GridMapBrushTool.ColorChannel.B);
            // }
            // else if (brushMode == BrushMode.Erase)
            // {
            //     tileMap.SetRectColor(x0, y0, x1, y1, Color.black);
            // }
        }

        pencilDragActive = false;
    }

       private static int RectangleDragSize()
       {
           if (!pencilDragActive)
               return 0;

           int x0 = Mathf.Min(cursorX, cursorX0);
           int x1 = Mathf.Max(cursorX, cursorX0);
           int y0 = Mathf.Min(cursorY, cursorY0);
           int y1 = Mathf.Max(cursorY, cursorY0);

           return (x1 - x0) * (y1 - y0);
       }


       private static bool IsInside(out Vector2Int point)
        {
            var worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            var isInside = GridMapManager.GetTileIndexByWorldPos(worldRay.origin, out var x, out var y);
            point = new Vector2Int(x, y);
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