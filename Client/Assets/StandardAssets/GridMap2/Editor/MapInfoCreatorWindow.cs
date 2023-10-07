using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public sealed class MapInfoCreatorWindow : EditorWindow
{
    private Button _confirmBtn;
    private TextField _idTextField;
    
    public static void Show()
    {
        var wnd = GetWindow<MapInfoCreatorWindow>();
        wnd.titleContent = new GUIContent("MapInfoCreatorWindow");
        wnd.OnShow();
    }
    
    private void OnShow()
    {
        _idTextField.value = "";
    }

    public void CreateGUI()
    {
        var root = rootVisualElement;
        
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/StandardAssets/GridMap2/Editor/MapInfoCreatorWindow.uxml");
        var labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);
        
        _idTextField = rootVisualElement.Q<TextField>("IDTextField");
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
        
        var gridMapConfig = AssetDatabase.LoadAssetAtPath<GridMapConfig>("Assets/StandardAssets/GridMap2/GridMapConfig.asset");
        if (gridMapConfig.IsExistsID(id))
        {
            EditorUtility.DisplayDialog("警告", "编号重复", "Yes");
            return;
        }
        
        //创建一个mapinfo
        gridMapConfig.AddMapInfo(id,"");
        
        //关闭界面
        Close();
    }
}
