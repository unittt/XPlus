
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Random = Unity.Mathematics.Random;

public class GridMap2 : EditorWindow
{
    [MenuItem("Window/UI Toolkit/GridMap2")]
    public static void ShowExample()
    {
        GridMap2 wnd = GetWindow<GridMap2>();
        wnd.titleContent = new GUIContent("GridMap2");
    }

    private ListView _listView;
    private List<int> _tempList;
    
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
        
        root.Q<Button>("AddButton").clicked += OnClickAdd;
        root.Q<Button>("RemoveButton").clicked += OnClickRemove;
       
        
        _tempList = new List<int>();

        _listView = root.Q<ListView>("MapListView");
       
        _listView.makeItem = MakeListItem;
        _listView.bindItem = BindListItem;
        _listView.onSelectionChange += OnSelectItem;
        _listView.itemsSource = _tempList;
        _listView.Rebuild();
    }

    private void OnClickAdd()
    {
        var x = Random.CreateFromIndex(0);
        _tempList.Add(x.NextInt(1,100));
        _listView.Rebuild();
    }

    private void OnClickRemove()
    {
        if (_listView.selectedIndex < 0) return;
        _tempList.RemoveAt(_listView.selectedIndex);
        _listView.Rebuild();
    }
    
    private VisualElement MakeListItem()
    {
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/StandardAssets/GridMap2/MapInfoElement.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        return labelFromUXML;
    }
    
    private void BindListItem(VisualElement arg1, int arg2)
    {
      
    }
    
    private void OnSelectItem(IEnumerable<object> obj)
    {
      
       
    }


    public struct Mapdata
    {
        public int Id;
        public string Des;
        public string Path;
    }
}