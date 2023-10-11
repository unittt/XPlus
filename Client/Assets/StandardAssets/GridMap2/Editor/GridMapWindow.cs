using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public sealed class GridMapWindow :EditorWindow
{
    private List<MapData> _mapDatas = new();
    private ListView _listView;
    //当前选中的mapData
    private MapData _mapData; 

    [MenuItem("Window/UI Toolkit/GridMap")]
    public static void ShowExample()
    {
        var wnd = GetWindow<GridMapWindow>();
        wnd.titleContent = new GUIContent("GridMapWindow");
        wnd.OnShow();
    }
    
    private void OnShow()
    {
        RefreshMapData();
        SelectGridMapData(null);
    }

    public void CreateGUI()
    {
        //加载配置
        var root = rootVisualElement;
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/StandardAssets/GridMap2/Editor/GridMapWindow.uxml");
        var labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);

        MapDataCreatorWindow.CreateMapData = CreateMapData;
        InitTop();
        InitMapInfoList();
        InitMapData();
    }

    /// <summary>
    /// 创建MapData
    /// </summary>
    /// <param name="id"></param>
    /// <param name="textureFolder"></param>
    /// <returns></returns>
    private bool CreateMapData(int id, string textureFolder,float size, int w, int h)
    {
        if (_mapDatas.Any(data => data.ID == id))
        {
            EditorUtility.DisplayDialog("警告", "编号重复", "Yes");
            return false;
        }
        
        var mapData = new MapData
        {
            ID = id,
            TextureFolder = textureFolder,
            AssetPath = GetMapDataPath(id),
            TexturSize =  size,
            NumberOfColumns = w,
            NumberOfRows = h
        };
        
        // 创建并写入文件
        mapData.Save();
        // 刷新Asset数据库，以便Unity编辑器能够检测到新文件
        AssetDatabase.Refresh();
        RefreshMapData();
        
        //选中对象
        SelectGridMapData(mapData);
        return true;
    }
    
    /// <summary>
    /// 获得mapData的路径
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private string GetMapDataPath(int id)
    {
        var fileName = $"mapdata_{id}.bytes"; // 指定文件名
        
        return Path.Combine(GridMapConfig.Instance.DataFolderPath, fileName);
    }

    private void RefreshMapData()
    {
        //读取数据
        _mapDatas.Clear();
        // 遍历目录下的所有文件
        var files = Directory.GetFiles(GridMapConfig.Instance.DataFolderPath);
        foreach (var file in files)
        {
            // 检查文件扩展名是否为 .bytes
            if (!Path.GetExtension(file).Equals(".bytes", System.StringComparison.OrdinalIgnoreCase)) continue;
            var textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(file);

            if (textAsset == null) continue;
            var mapData = MapData.Deserialize(textAsset.bytes);
            _mapDatas.Add(mapData);
        }
        _listView.Rebuild();
    }
    
    /// <summary>
    /// mapconfig数据发生改变
    /// </summary>
    private static void GridMapConfigValueChanged()
    {
        EditorUtility.SetDirty(GridMapConfig.Instance);
        AssetDatabase.SaveAssetIfDirty(GridMapConfig.Instance);
    }
    
    #region Top
    private void InitTop()
    {
        //编辑器场景
        var pathTextField = rootVisualElement.Q<TextField>("ScenePathTextField");
        pathTextField.value = GridMapConfig.Instance.ScenePath;
        rootVisualElement.Q<Button>("SceneBrowseBtn").clicked += () =>
        {
            var path = EditorUtility.OpenFilePanel("选择编辑场景", Application.dataPath, "unity");
            if (string.IsNullOrEmpty(path)) return;

            path = MapGlobal.AbsoluteToRelativePath(path);
            pathTextField.value = path;
            GridMapConfig.Instance.ScenePath = path;
            
            //保存数据
            GridMapConfigValueChanged();
        };
        
        //地图数据存储目录
        var mapDataFolderTextField = rootVisualElement.Q<TextField>("MapDataFolderTextField");
        mapDataFolderTextField.value = GridMapConfig.Instance.DataFolderPath;
 
        rootVisualElement.Q<Button>("MapDataFolderBrowseBtn").clicked += () =>
        {
            var path = EditorUtility.OpenFolderPanel("选择数据存储目录", Application.dataPath, "");
            if (string.IsNullOrEmpty(path)) return;

            path = MapGlobal.AbsoluteToRelativePath(path);
            mapDataFolderTextField.value = path;
            GridMapConfig.Instance.DataFolderPath = path;
            
            //保存数据
            GridMapConfigValueChanged();
        };
        
        //贴图命名规则
        var textureNameRule = rootVisualElement.Q<TextField>("TextureNameRule");
        textureNameRule.value =  GridMapConfig.Instance.TextureNameRule;
        
        textureNameRule.RegisterValueChangedCallback((changeEvent) =>
        {
            if (string.IsNullOrEmpty(changeEvent.newValue)) return;
            GridMapConfig.Instance.TextureNameRule = changeEvent.newValue;
            
            //保存数据
            GridMapConfigValueChanged();
        });
        
        //贴图默认尺寸
        var textureSizeSlider = rootVisualElement.Q<Slider>("TextureSizeSlider");
        textureSizeSlider.value = GridMapConfig.Instance.TextureSize;
        textureSizeSlider.RegisterValueChangedCallback((changeEvent) =>
        {
            GridMapConfig.Instance.TextureSize = changeEvent.newValue;
            GridMapConfigValueChanged();
        });
        
        //节点默认尺寸
        var nodeSizeSlider = rootVisualElement.Q<Slider>("NodeSizeSlider");
        nodeSizeSlider.value = GridMapConfig.Instance.NodeSize;
        nodeSizeSlider.RegisterValueChangedCallback((changeEvent) =>
        {
            GridMapConfig.Instance.NodeSize = changeEvent.newValue;
            GridMapConfigValueChanged();
        });
    }
    #endregion
    
    #region 列表
    /// <summary>
    /// 初始化列表
    /// </summary>
    private void InitMapInfoList()
    {
        _listView = rootVisualElement.Q<ListView>("MapListView");
        _listView.Q<Button>("unity-list-view__add-button").clickable = new Clickable(MapDataCreatorWindow.Show);
        _listView.makeItem = MakeListItem;
        _listView.bindItem = BindListItem;
        _listView.onSelectionChange += OnSelectItem;
        _listView.itemsSource = _mapDatas;
        _listView.itemsRemoved += OnRemovedListItem;
    }
    
    private void OnRemovedListItem(IEnumerable<int> indexes)
    {
        if (_mapData == null) return;
        _mapData.DeleteFile();
        SelectGridMapData(null);
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
        if (index >= _mapDatas.Count)
        {
            return;
        }
        var mapData = _mapDatas[index];
        var label = ve as Label;
        label.text = mapData.ID.ToString();
    }

    private void OnSelectItem(IEnumerable<object> obj)
    {
        SelectGridMapData(_listView.selectedIndex >= 0 ? _mapDatas[_listView.selectedIndex] : null);
    }
    #endregion

    #region 地图信息
    private void InitMapData()
    {
        //瓦片资源目录
        var pathTextField = rootVisualElement.Q<TextField>("TileFolderTextField");
        var tileFolderBrowseBtn = rootVisualElement.Q<Button>("TileFolderBrowseBtn");
        tileFolderBrowseBtn.clicked += () =>
        {
            var path = EditorUtility.OpenFolderPanel("选择瓦片资源目录", Application.dataPath, "");
            if (string.IsNullOrEmpty(path) || _mapData == null) return;

            path = MapGlobal.AbsoluteToRelativePath(path);
            pathTextField.value = path;
            _mapData.TextureFolder = path;
            //保存数据
            _mapData.Save();
        };

        rootVisualElement.Q<Button>("AssetPathBrowseBtn").clicked += () =>
        {
            if (string.IsNullOrEmpty(_mapData.AssetPath))
            {
                return;
            }
            var asset = AssetDatabase.LoadAssetAtPath<Object>(_mapData.AssetPath);
            if (asset != null)
            {
                EditorGUIUtility.PingObject(asset); // 选中文件但不显示在Inspector
            }
        };
        
        rootVisualElement.Q<Button>("EditorBtn").clicked += OnClickEditor;
    }
    
    private void SelectGridMapData(MapData mapData)
    {
        _mapData = mapData;
        rootVisualElement.Q("MapDataContainer").visible = _mapData != null;
        if (_mapData == null) return;
        rootVisualElement.Q<TextField>("TextFieldID").value = mapData.ID.ToString();
        rootVisualElement.Q<TextField>("TileFolderTextField").value = mapData.TextureFolder;
        rootVisualElement.Q<TextField>("AssetPath").value = mapData.AssetPath;
        
        rootVisualElement.Q<FloatField>("TextureSize").value = mapData.TexturSize;
        rootVisualElement.Q<IntegerField>("TextureW").value = mapData.NumberOfColumns;
        rootVisualElement.Q<IntegerField>("TextureH").value = mapData.NumberOfColumns;
    }

    private void OnClickEditor()
    {

        if (string.IsNullOrEmpty(GridMapConfig.Instance.ScenePath))
        {
            EditorUtility.DisplayDialog("警告", "场景路径为空", "Yes");
            return;
        }

        if (_mapData == null)
        {
            EditorUtility.DisplayDialog("警告", "未选择地图", "Yes");
            return;
        }
        
        if (!EditorUtility.DisplayDialog("提示", "需要打开2d编辑场景才能生成2d场景寻路数据,是否继续?", "Yes", "No")) return;
        
        //保存编辑的关卡编号
        EditorPrefs.SetInt(MapGlobal.EDITOR_MAP_ID_KEY, _mapData.ID);
        
        //打开场景
        EditorSceneManager.OpenScene(GridMapConfig.Instance.ScenePath);
      
        //删除所有对象
        var allObjects = FindObjectsOfType<GameObject>();
        foreach (var obj in allObjects)
        {
            DestroyImmediate(obj);
        }
        
        //加载GridMapManager
        var gridMapPrefab = Resources.Load<GameObject>("GridMapManager");
        var gridMapManager = Instantiate(gridMapPrefab);
        gridMapManager.name = "GridMapManager";

        var gridMapEditorSceneManager = gridMapManager.GetComponent<GridMapEditorSceneManager>();
        Selection.activeGameObject = gridMapEditorSceneManager.AstarPath.gameObject;
        gridMapEditorSceneManager.SetGridMapData(_mapData);
    }
    #endregion
}