using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GridMap
{
    
    public sealed class MapDataCreatorWindow : EditorWindow
    {
        private Button _confirmBtn;
        private TextField _idTextField;
        private TextField _pathTextField;
        private Slider _textureSize;
        private IntegerField _textureW;
        private IntegerField _textureH;


        public static Func<int, string, float, int, int, bool> CreateMapData;
        
        
        
        public static void ShowWindow()
        {
            var wnd = GetWindow<MapDataCreatorWindow>();
            wnd.titleContent = new GUIContent("MapDataCreatorWindow");
            wnd.OnShow();
        }

        private void OnShow()
        {
            _idTextField.value = "";
            _pathTextField.value = "";
            _textureSize.value = GridMapConfig.Instance.TextureSize;
            _textureW.value = 1;
            _textureH.value = 1;
        }

        public void CreateGUI()
        {
            var root = rootVisualElement;

            var visualTree =
                AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                    "Assets/StandardAssets/GridMap/Editor/Window/MapDataCreatorWindow.uxml");
            var labelFromUXML = visualTree.Instantiate();
            root.Add(labelFromUXML);

            _idTextField = rootVisualElement.Q<TextField>("IDTextField");

            //瓦片资源目录
            _pathTextField = rootVisualElement.Q<TextField>("TilePathTextField");
            var tileFolderBrowseBtn = rootVisualElement.Q<Button>("TileBrowseBtn");
            tileFolderBrowseBtn.clicked += () =>
            {
                var path = EditorUtility.OpenFolderPanel("选择瓦片资源目录", Application.dataPath, "");
                if (string.IsNullOrEmpty(path)) return;

                path = MapGlobal.AbsoluteToRelativePath(path);
                _pathTextField.value = path;
            };

            _textureSize = rootVisualElement.Q<Slider>("TextureSize");
            _textureW = rootVisualElement.Q<IntegerField>("TextureW");
            _textureH = rootVisualElement.Q<IntegerField>("TextureH");

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

            if (_textureW.value <= 0 || _textureH.value <= 0)
            {
                EditorUtility.DisplayDialog("警告", "宽和高必须大于0", "Yes");
                return;
            }

            if (CreateMapData.Invoke(id, _pathTextField.value, _textureSize.value, _textureW.value, _textureH.value))
            {
                Close();
            }

            // if (GridMapConfig.Instance.IsExistsID(id))
            // {
            //     EditorUtility.DisplayDialog("警告", "编号重复", "Yes");
            //     return;
            // }
        }
    }
}
