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

        public static void Update()
        {
      
            var controlID = GridMapBrushHashCode;
            var controlEventType = Event.current.GetTypeForControl(controlID);

            
            // if (controlEventType ==  EventType.MouseDrag)
            // {
            //     if ((controlEventType == EventType.MouseDrag && GUIUtility.hotControl != controlID) ||
            //         (Event.current.button != 0 && Event.current.button != 1))
            //     {
            //         return;
            //     }
            //     
            //     // make sure we don't use up reserved combinations
            //     bool inhibitMouseDown = false;
            //     if (Application.platform == RuntimePlatform.OSXEditor)
            //     {
            //         if (Event.current.command && Event.current.alt)
            //         { // pan combination on mac
            //             inhibitMouseDown = true;
            //         }
            //     }
            //     
            //     if (Event.current.type == EventType.MouseDown && !inhibitMouseDown)
            //     {
            //         if (UpdateCursorPosition() && !Event.current.shift)
            //         {
            //             GUIUtility.hotControl = controlID;
            //             PencilDrag();
            //         }
            //     }
            //
            //     if (Event.current.type == EventType.MouseDrag && GUIUtility.hotControl == controlID)
            //     {
            //         UpdateCursorPosition();
            //         PencilDrag();
            //     }
            // }
            // else if (controlEventType == EventType.MouseUp)
            // {
            //     if ((Event.current.button == 0 || Event.current.button == 1) && GUIUtility.hotControl == controlID)
            //     {
            //         GUIUtility.hotControl = 0;
            //         RectangleDragEnd();
            //
            //         cursorX0 = cursorX;
            //         cursorY0 = cursorY;
            //
            //         // HandleUtility.Repaint();
            //     }
            // }
            // else if (controlEventType == EventType.MouseMove)
            // {
            //     UpdateCursorPosition();
            //     cursorX0 = cursorX;
            //     cursorY0 = cursorY;
            // }

            // var controlEventType = Event.current.GetTypeForControl(controlID);
            var e = Event.current.type;
            if (e == EventType.MouseDown)
            {
                Debug.Log("鼠标按下");
            }

            if (e == EventType.Used)
            {
                Debug.Log("鼠标抬起");
            }
          

       
        }
        
        static void PencilDrag()
        {
            pencilDragActive = true;
        }
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


       private static bool UpdateCursorPosition()
        {
            var worldRay = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            var isInside = GridMapManager.GetTileIndexByWorldPos(worldRay.origin, out cursorX, out cursorY);
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