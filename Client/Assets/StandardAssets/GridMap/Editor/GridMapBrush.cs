using UnityEditor;
using UnityEngine;

namespace StandardAssets.GridMap.Editor
{
    /// <summary>
    /// 笔刷
    /// </summary>
    [InitializeOnLoad]
    internal static class GridMapBrush
    {
        private static float Height;
        
        static GridMapBrush()
        {
            SceneView.duringSceneGui += OnGridMapBrushGUI;
        }

        private static void OnGridMapBrushGUI(SceneView sceneView)
        {
            Handles.BeginGUI();
            
            Rect rect = Rect.zero;
            float x = sceneView.position.width - 300;
            float y = sceneView.in2DMode ? 5 : 120;

            rect.Set(x, y, 75, Height);
            GUI.Box(rect, "LnkToolsAAAA", "Window");
           
            
            GUI.enabled = true;
            Handles.EndGUI();
        }
    }
}