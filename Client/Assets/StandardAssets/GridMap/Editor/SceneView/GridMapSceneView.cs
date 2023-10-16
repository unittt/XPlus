using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace GridMap
{
    public sealed class GridMapSceneView: EditorWindow
    {
        //根节点,黑色条
        private  VisualElement _root;
        //toolbar面板
        private  VisualElement _toolBarPanel;
        private  VisualElement _infoPanel;
        
        private  VisualElement _hideBarBtn;
        private  VisualElement _ShowBarBtn;
        
        private GridMapManager _gridMapManager;
        private MapData _mapData;
        private Slider _textureSizeSlider;
        private Slider _nodeSizeSlider;
        private SliderInt _nodeWidth;
        private SliderInt _nodeDepth;
        private SceneView _sceneView;
        
        private readonly Dictionary<BrushType, VisualElement> _brushDic = new();

        private static GridMapSceneView _instance;
        public static GridMapSceneView Instance
        {
            get
            {
                _instance ??= CreateInstance<GridMapSceneView>();
                
                if (_instance._root == null)
                {
                    //初始化
                    _instance.Init();
                }

                return _instance;
            }
        }

    
        private  void Init()
        {
            _sceneView = SceneView.lastActiveSceneView;
            _sceneView.in2DMode = true;
            _sceneView.showGrid = false;
            
            //初始化设置
            var toolbarTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/StandardAssets/GridMap/Editor/SceneView/GridMapSceneView.uxml");
            _root = toolbarTreeAsset.CloneTree().Children().First();
            _sceneView.rootVisualElement.Add(_root);
            _root.style.position = Position.Absolute;
            _root.style.bottom = 0;
            _root.style.width = _sceneView.position.width;
            _root.style.flexGrow  = 1;
            _root.BringToFront();
            
            _toolBarPanel = _root.Q<VisualElement>("toolbar");
            _toolBarPanel.style.alignSelf = Align.Center;
            
            var btnWalk = _root.Q<VisualElement>("Walk");
            btnWalk.tooltip = "行走";
            _brushDic.Add(BrushType.Walk,btnWalk);
            btnWalk.RegisterCallback((MouseDownEvent e) =>
            {
                SelectBrush(BrushType.Walk);
            });
            
            var btnTransparency= _root.Q<VisualElement>("Transparency");
            btnTransparency.tooltip = "透明区域";
            _brushDic.Add(BrushType.Transparent,btnTransparency);
            btnTransparency.RegisterCallback((MouseDownEvent e) =>
            {
                SelectBrush(BrushType.Transparent);
            });
            var btnClear = _root.Q<VisualElement>("Clear");
            btnClear.tooltip = "清理";
            _brushDic.Add(BrushType.Clear,btnClear);
            btnClear.RegisterCallback((MouseDownEvent e) =>
            {
                SelectBrush(BrushType.Clear);
            });
            
            var btnSave = _root.Q<VisualElement>("Save");
            btnSave.tooltip = "保存";
            btnSave.RegisterCallback((MouseDownEvent e) =>
            {
       
            });
            
            _hideBarBtn = _root.Q<VisualElement>("HideBtn");
            _ShowBarBtn = _root.Q<VisualElement>("ShowBtn");
            _hideBarBtn.RegisterCallback((MouseDownEvent e) =>
            {
                SetToolbarExpandState(false);
            });
            _ShowBarBtn.RegisterCallback((MouseDownEvent e) =>
            {
                SetToolbarExpandState(true);
            });
            
            _infoPanel =  _root.Q<VisualElement>("InfoPanel");
            _textureSizeSlider =  _root.Q<Slider>("TextureSizeSlider");
            _textureSizeSlider.RegisterValueChangedCallback((callback) =>
            {
                _gridMapManager.TextureSize = callback.newValue;
            });
            
            _nodeSizeSlider =  _root.Q<Slider>("NodeSizeSlider");
            _nodeSizeSlider.RegisterValueChangedCallback((callback) =>
            {
                _gridMapManager.NodeSize = callback.newValue;
            });
            
            _nodeWidth =  _root.Q<SliderInt>("NodeWidth");
            _nodeWidth.RegisterValueChangedCallback((callback) =>
            {
                _gridMapManager.Width = callback.newValue;
            });
            
            _nodeDepth =  _root.Q<SliderInt>("NodeDepth");
            _nodeDepth.RegisterValueChangedCallback((callback) =>
            {
                _gridMapManager.Depth = callback.newValue;
            });
            
            //监听场景发生改变
            // EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
            EditorSceneManager.activeSceneChangedInEditMode += OnSceneChanged;
            SceneView.duringSceneGui += OnSceneGUI; 
        }

        private void OnSceneGUI(SceneView obj)
        {
           
            GridMapBrush.Update(_gridMapManager);
        }

        private void OnSceneChanged(Scene arg0, Scene arg1)
        {
            EditorSceneManager.activeSceneChangedInEditMode -= OnSceneChanged;
            _sceneView.rootVisualElement.Remove(_root);
            _instance = null;
        }
        
        public void Show(GridMapManager gridMapManager)
        {
            _gridMapManager = gridMapManager;
            _textureSizeSlider.value = gridMapManager.TextureSize;
            _nodeSizeSlider.value =  gridMapManager.NodeSize;
            _nodeWidth.value =  gridMapManager.Width;
            _nodeDepth.value =  gridMapManager.Depth;
            
            SetToolbarExpandState(true);
            SelectBrush(BrushType.Walk);
        }
        
        private void SelectBrush(BrushType brushType)
        {
            var normalColor = new StyleColor
            {
                value = new Color(0, 0, 0, 0)
            };
            var selectedColor = new StyleColor
            {
                value = Color.white
            };
            foreach (var (key, element) in _brushDic)
            {
                if (key == brushType)
                {
                    element.style.unityBackgroundImageTintColor = normalColor;
                    element[0].style.unityBackgroundImageTintColor = selectedColor;
                }
                else
                {
                    element.style.unityBackgroundImageTintColor = selectedColor;
                    element[0].style.unityBackgroundImageTintColor = normalColor;
                }
            }

            GridMapBrush.Bursh = brushType;
        }
        
        private void SetToolbarExpandState(bool expand)
        {

            if (expand)
            {
                _toolBarPanel.style.visibility = Visibility.Visible;
                _infoPanel.style.visibility = Visibility.Visible;
                
                _hideBarBtn.style.visibility = Visibility.Visible;
                _ShowBarBtn.style.visibility = Visibility.Hidden;
                _root.style.bottom = 0;
            }
            else
            {
                _toolBarPanel.style.visibility = Visibility.Hidden;
                _infoPanel.style.visibility = Visibility.Hidden;
                
                _hideBarBtn.style.visibility = Visibility.Hidden;
                _ShowBarBtn.style.visibility = Visibility.Visible;
                _root.style.bottom = -18f;
            }
        }
    }
}