using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public sealed class MapInfoCreatorWindow : EditorWindow
{
    private Button _confirmBtn;
    private TextField _idTextField;
    private TextField _pathTextField;
    
    public static void Show()
    {
        var wnd = GetWindow<MapInfoCreatorWindow>();
        wnd.titleContent = new GUIContent("MapInfoCreatorWindow");
        wnd.OnShow();
    }
    
    private void OnShow()
    {
        _idTextField.value = "";
        _pathTextField.value = "";
    }

    public void CreateGUI()
    {
        var root = rootVisualElement;
        
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/StandardAssets/GridMap2/Editor/MapInfoCreatorWindow.uxml");
        var labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);
        
        _idTextField = rootVisualElement.Q<TextField>("IDTextField");
        
        //瓦片资源目录
        _pathTextField = rootVisualElement.Q<TextField>("TilePathTextField");
        var tileFolderBrowseBtn = rootVisualElement.Q<Button>("TileBrowseBtn");
        tileFolderBrowseBtn.clicked += () =>
        {
            var path = EditorUtility.OpenFolderPanel("选择瓦片资源目录", Application.dataPath,"");
            if (string.IsNullOrEmpty(path)) return;

            path = MapGlobal.AbsoluteToRelativePath(path);
            _pathTextField.value = path;
        };
        
        _confirmBtn = rootVisualElement.Q<Button>("ConfirmBtn");
        _confirmBtn.clicked += OnClickConfirm;
    }

    private void OnClickConfirm()
    {
        if (!int.TryParse(_idTextField.value, out var id))
        {
            EditorUtility.DisplayDialog("警告", "请输入正确的编号", "Yes");
            return;
        }

        if (string.IsNullOrEmpty(_pathTextField.value))
        {
            EditorUtility.DisplayDialog("警告", "瓦片资源目录为空", "Yes");
            return;
        }
        
        if (GridMapConfig.Instance.IsExistsID(id))
        {
            EditorUtility.DisplayDialog("警告", "编号重复", "Yes");
            return;
        }

        //创建一个mapinfo
        GridMapConfig.Instance.AddMapInfo(id, _pathTextField.value);

        //关闭界面
        Close();
    }
}
