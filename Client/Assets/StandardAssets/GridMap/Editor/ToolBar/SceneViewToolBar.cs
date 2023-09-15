using System.Collections.Generic;
using System.Linq;
using HT.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace StandardAssets.GridMap.Editor.ToolBar
{
    public class SceneViewToolBar : HTFEditorWindow
    {
        //toolbar根节点,黑色条
        private static VisualElement _toolbarBg;
        //toolbar面板
        private static VisualElement _toolBarPanel;
        
        private static VisualElement _hideBarBtn;
        private static VisualElement _ShowBarBtn;
        
        private static bool _overToolBarBg;
        private static bool _overToolBar;
        private static bool _haveToolbar;

        private static List<VisualElement> _toolVisualElements = new();

        
        [MenuItem("UIToolkit/ToolBar")]
        public static void ShowExample()
        {
            OpenEditor();
        }
        
        public static void OpenEditor()
        { 
            //在播放预制体的时候打开编辑器，不修改判断播放预制体的值(临时处理手法)
            if (EditorApplication.isPlaying != true)
            {
                PlayerPrefs.SetString("previewStage", "false");
            }
            
            if (!_haveToolbar)
            {
                InitToolBar();
                _haveToolbar = true;
            }
            
            SceneView.lastActiveSceneView.in2DMode = true;
        }
        

        private static void InitToolBar()
        {
            
            SceneView sceneView = SceneView.lastActiveSceneView;
            VisualTreeAsset toolbarTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/StandardAssets/GridMap/Editor/ToolBar/ToolBar.uxml");
            
            _toolbarBg = toolbarTreeAsset.CloneTree().Children().First();
            sceneView.rootVisualElement.Add(_toolbarBg);
            _toolbarBg.style.position = Position.Absolute;
            _toolbarBg.style.bottom = 0;
            _toolbarBg.BringToFront();

            _toolBarPanel = _toolbarBg.Q<VisualElement>("toolbar");
            _toolBarPanel.style.alignSelf = Align.Center;
            
            var btnWalk = _toolbarBg.Q<VisualElement>("Walk");
            btnWalk.tooltip = "行走";
            AddElement(btnWalk);
            btnWalk.RegisterCallback((MouseDownEvent e) =>
            {
                OnClickElement(btnWalk);
            });
            
            var btnFly = _toolbarBg.Q<VisualElement>("Fly");
            btnFly.tooltip = "飞行";
            AddElement(btnFly);
            btnFly.RegisterCallback((MouseDownEvent e) =>
            {
                OnClickElement(btnFly);
            });
            var btnTransparency= _toolbarBg.Q<VisualElement>("Transparency");
            btnTransparency.tooltip = "透明区域";
            AddElement(btnTransparency);
            btnTransparency.RegisterCallback((MouseDownEvent e) =>
            {
                OnClickElement(btnTransparency);
            });
            var btnClear = _toolbarBg.Q<VisualElement>("Clear");
            btnClear.tooltip = "清理";
            AddElement(btnClear);
            btnClear.RegisterCallback((MouseDownEvent e) =>
            {
                OnClickElement(btnClear);
            });
            
            var btnSave = _toolbarBg.Q<VisualElement>("Save");
            btnSave.tooltip = "保存";
            btnSave.RegisterCallback((MouseDownEvent e) =>
            {
       
            });
            
            _hideBarBtn = _toolbarBg.Q<VisualElement>("HideBtn");
            _ShowBarBtn = _toolbarBg.Q<VisualElement>("ShowBtn");
            _hideBarBtn.RegisterCallback((MouseDownEvent e) =>
            {
                SetToolbarExpandState(false);
            });
            _ShowBarBtn.RegisterCallback((MouseDownEvent e) =>
            {
                SetToolbarExpandState(true);
            });

            _toolbarBg.RegisterCallback((PointerOverEvent e) =>
            {
                _overToolBarBg = true;
            });
            _toolbarBg.RegisterCallback((PointerOutEvent e) =>
            {
                _overToolBarBg = false;
            });
            _toolBarPanel.RegisterCallback((PointerOverEvent e) =>
            {
                _overToolBar = true;
            });
            _toolBarPanel.RegisterCallback((PointerOutEvent e) =>
            {
                _overToolBar = false;
            });
            
        }

        private static void AddElement(VisualElement visualElement)
        {
            _toolVisualElements.Add(visualElement);
        }

        private static void OnClickElement(VisualElement visualElement)
        {
            var color = Color.green;
            foreach (var element in _toolVisualElements)
            {
                color.a = element == visualElement ?1 : 0;
                element.style.backgroundColor = new StyleColor(color);
            }
        }
        
        private static void SetToolbarExpandState(bool expand)
        {

            if (expand)
            {
                _toolBarPanel.style.visibility = Visibility.Visible;
                _hideBarBtn.style.visibility = Visibility.Visible;
                _ShowBarBtn.style.visibility = Visibility.Hidden;
                _toolbarBg.style.bottom = 0;
            }
            else
            {
                _toolBarPanel.style.visibility = Visibility.Hidden;
                _hideBarBtn.style.visibility = Visibility.Hidden;
                _ShowBarBtn.style.visibility = Visibility.Visible;
                _toolbarBg.style.bottom = -18f;
            }
        }
        
        #region Quick Create 
        
        
        
        
        
        #endregion
        
    }
}