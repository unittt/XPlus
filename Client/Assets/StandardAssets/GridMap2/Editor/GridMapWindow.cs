using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

public sealed class GridMapWindow : EditorWindow
{
    [MenuItem("Window/UI Toolkit/GridMap2")]
    public static void ShowExample()
    {
        var wnd = GetWindow<GridMapWindow>();
        wnd.titleContent = new GUIContent("GridMapWindow");
        wnd.OnShow();
    }

    private ListView _listView;
    private Button _editorBtn;
    
    private GridMapConfig _gridMapConfig;
    private GridMapInfo _gridMapInfo;
    
    private void OnShow()
    {
        ShowGridMapInfo(null);
    }

    public void CreateGUI()
    {
        //加载配置
        _gridMapConfig = AssetDatabase.LoadAssetAtPath<GridMapConfig>("Assets/StandardAssets/GridMap2/GridMapConfig.asset");
        
        var root = rootVisualElement;
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/StandardAssets/GridMap2/Editor/GridMapWindow.uxml");
        var labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);
        
        InitScenePath();
        InitMapInfoList();
        InitMapInfo();
    }
    
    /// <summary>
    /// 初始化编辑场景路径
    /// </summary>
    private void InitScenePath()
    {
        //编辑器场景
        var pathTextField = rootVisualElement.Q<TextField>("ScenePathTextField");
        pathTextField.value = _gridMapConfig.ScenePath;
        var sceneBrowseBtn = rootVisualElement.Q<Button>("SceneBrowseBtn");
        sceneBrowseBtn.clicked += () =>
        {
            var path = EditorUtility.OpenFilePanel("选择编辑场景", Application.dataPath, "unity");
            if (string.IsNullOrEmpty(path)) return;
            pathTextField.value = path;
            _gridMapConfig.ScenePath = path;
        };
    }

    #region 初始化列表
    /// <summary>
    /// 初始化列表
    /// </summary>
    private void InitMapInfoList()
    {
        _listView = rootVisualElement.Q<ListView>("MapListView");
        _listView.Q<Button>("unity-list-view__add-button").clickable = new Clickable(MapInfoCreatorWindow.Show);
        _listView.makeItem = MakeListItem;
        _listView.bindItem = BindListItem;
        _listView.onSelectionChange += OnSelectItem;
        _listView.itemsSource = _gridMapConfig.InfoList;
        _gridMapConfig.OnInfoListValueChanged +=  _listView.Rebuild;
        _listView.Rebuild();
    }
    
    private VisualElement MakeListItem()
    {
        var label = new Label
        {
            style =
            {
                unityTextAlign = TextAnchor.MiddleLeft,
                marginLeft = 5
            }
        };
        return label;
    }
    
    private void BindListItem(VisualElement ve, int index)
    {
        if (index >= _gridMapConfig.InfoList.Count)
        {
            return;
        }
        var gridMapInfo = _gridMapConfig.InfoList[index];
        var label = ve as Label;
        label.text = gridMapInfo.ID.ToString();
    }
    
    private void OnSelectItem(IEnumerable<object> obj)
    {
        ShowGridMapInfo(_listView.selectedIndex >= 0 ? _gridMapConfig.InfoList[_listView.selectedIndex] : null);
    }
    
    private void ShowGridMapInfo(GridMapInfo gridMapInfo)
    {
        _gridMapInfo = gridMapInfo;
        var isNull = gridMapInfo == null;
        rootVisualElement.Q<TextField>("TextFieldID").value = isNull ? "" : gridMapInfo.ID.ToString();
        rootVisualElement.Q<TextField>("TextFieldCTime").value = isNull ? "" : gridMapInfo.CrateTime;
        rootVisualElement.Q<TextField>("TextFieldDescribe").value = isNull ? "" : gridMapInfo.Describe;
        _editorBtn.visible = !isNull;

    }
    #endregion

    #region 地图信息
    private void InitMapInfo()
    {
        rootVisualElement.Q<TextField>("TextFieldDescribe").RegisterValueChangedCallback(OnDescribeValueChanged);
        _editorBtn = rootVisualElement.Q<Button>("EditorBtn");
        _editorBtn.clicked += OnClickEditor;
    }

    private void OnDescribeValueChanged(ChangeEvent<string> evt)
    {
        if (_gridMapInfo != null)
        {
            _gridMapInfo.Describe = evt.newValue;
            //保存数据
        }
        
    }

    private void OnClickEditor()
    {

        if (string.IsNullOrEmpty( _gridMapConfig.ScenePath))
        {
            EditorUtility.DisplayDialog("警告", "场景路径为空", "Yes");
            return;
        }

        if (_gridMapInfo == null)
        {
            EditorUtility.DisplayDialog("警告", "未选择地图", "Yes");
            return;
        }
        
        if (!EditorUtility.DisplayDialog("提示", "需要打开2d编辑场景才能生成2d场景寻路数据,是否继续?", "Yes", "No")) return;
        EditorSceneManager.OpenScene( _gridMapConfig.ScenePath);
                
        //删除所有对象
        var allObjects = FindObjectsOfType<GameObject>();
        foreach (var obj in allObjects)
        {
            DestroyImmediate(obj);
        }
                
        //加载GridMapManager
        var gridMapPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/StandardAssets/GridMap2/GridMapManager.prefab");
        var gridMapManager = Instantiate(gridMapPrefab);
        gridMapManager.name = "GridMapManager";
    }
    #endregion
}