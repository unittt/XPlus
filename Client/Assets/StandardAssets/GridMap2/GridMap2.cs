using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GridMap2 : EditorWindow
{
    [MenuItem("Window/UI Toolkit/GridMap2")]
    public static void ShowExample()
    {
        GridMap2 wnd = GetWindow<GridMap2>();
        wnd.titleContent = new GUIContent("GridMap2");
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;
        
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/StandardAssets/GridMap2/GridMap2.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);

        var sceneBrowseBtn = root.Q<Button>("SceneBrowseBtn");
        sceneBrowseBtn.clicked += () =>
        {
            var path = EditorUtility.OpenFilePanel("选择编辑场景", Application.dataPath, "unity");
            if (!string.IsNullOrEmpty(path))
            {
                var pathTextField = root.Q<TextField>("ScenePathTextField");
                pathTextField.value = path;
                // GridMapGlobalConfig.Instance.EditorScenePath = AbsoluteToRelativePath(path);
            }
        };
    }
    
    
    public struct Mapdata
    {
        public int Id;
        public string Des;
        public string Path;
    }
}