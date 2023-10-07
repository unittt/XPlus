using System;
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
        _idTextField.RegisterValueChangedCallback(OnValueChanged);
        
        _confirmBtn = rootVisualElement.Q<Button>("ConfirmBtn");
        _confirmBtn.clicked += OnClickConfirm;
    }

    private void OnValueChanged(ChangeEvent<string> evt)
    {
        RefreshConfirmBtn();
    }

    private void OnClickConfirm()
    {
        if (!int.TryParse(_idTextField.value, out var id))
        {
            return;
        }
        
        var gridMapConfig = AssetDatabase.LoadAssetAtPath<GridMapConfig>("Assets/StandardAssets/GridMap2/GridMapConfig.asset");
        if (gridMapConfig.IsExistsID(id))
        {
            return;
        }
        
        gridMapConfig.AddMapInfo(id,"");
        
        //关闭界面
        Close();
    }

    private void RefreshConfirmBtn()
    {
        if (!int.TryParse(_idTextField.value, out _))
        {
            //设置为禁用
            return;
        }
    }
}
