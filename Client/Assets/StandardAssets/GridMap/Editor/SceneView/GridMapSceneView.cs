using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace GridMap
{
    public sealed partial class GridMapSceneView: EditorWindow
    {
        private MapManager _mapManager;
        private MapData _mapData;
        
        //根节点,黑色条
        private  VisualElement _root;
        //toolbar面板
        private  VisualElement _toolBarPanel;
        private  VisualElement _infoPanel;
        
        private  VisualElement _hideBarBtn;
        private  VisualElement _ShowBarBtn;
        
      
        private Slider _blockSizeSlider;
        private SliderInt _blockWidth;
        private SliderInt _blockDepth;
        
        private Slider _nodeSizeSlider;
        private SliderInt _nodeWidth;
        private SliderInt _nodeDepth;
        private SceneView _sceneView;
        
        private readonly Dictionary<NodeTag, VisualElement> _brushDic = new();
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

    
        private void Init()
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
            _brushDic.Add(NodeTag.WALK,btnWalk);
            btnWalk.RegisterCallback((MouseDownEvent e) =>
            {
                SelectBrush(NodeTag.WALK);
            });
            
            var btnTransparency= _root.Q<VisualElement>("Transparency");
            btnTransparency.tooltip = "透明区域";
            _brushDic.Add(NodeTag.Transparent,btnTransparency);
            btnTransparency.RegisterCallback((MouseDownEvent e) =>
            {
                SelectBrush(NodeTag.Transparent);
            });
            var btnClear = _root.Q<VisualElement>("Clear");
            btnClear.tooltip = "障碍区域";
            _brushDic.Add(NodeTag.Obstacle,btnClear);
            btnClear.RegisterCallback((MouseDownEvent e) =>
            {
                SelectBrush(NodeTag.Obstacle);
            });
            
            var btnSave = _root.Q<VisualElement>("Save");
            btnSave.tooltip = "保存";
            btnSave.RegisterCallback((MouseDownEvent e) =>
            {
                _mapData.WriteNodes(_mapManager.Nodes);
                _mapData.Save();
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

            #region 数值变动
            _blockSizeSlider =  _root.Q<Slider>("BlockSizeSlider");
            _blockSizeSlider.RegisterValueChangedCallback((callback) =>
            {
                _mapData.BlockSize = callback.newValue;
                _mapManager.BlockParamsChanged(_mapData);
            });
            
            _blockWidth =  _root.Q<SliderInt>("BlockWidth");
            _blockWidth.RegisterValueChangedCallback((callback) =>
            {
                _mapData.BlockWidth = callback.newValue;
                _mapManager.BlockParamsChanged(_mapData);
            });
            
            _blockDepth =  _root.Q<SliderInt>("BlockDepth");
            _blockDepth.RegisterValueChangedCallback((callback) =>
            {
                _mapData.BlockHeight = callback.newValue;
                _mapManager.BlockParamsChanged(_mapData);
            });
            
            _nodeSizeSlider =  _root.Q<Slider>("NodeSizeSlider");
            _nodeSizeSlider.RegisterValueChangedCallback((callback) =>
            {
                _mapData.NodeSize = callback.newValue;
                _mapManager.GridGraphParamsChanged(_mapData);
            });
            
            _nodeWidth =  _root.Q<SliderInt>("NodeWidth");
            _nodeWidth.RegisterValueChangedCallback((callback) =>
            {
                _mapData.NodeWidth = callback.newValue;
                _mapManager.GridGraphParamsChanged(_mapData);
            });
            
            _nodeDepth =  _root.Q<SliderInt>("NodeDepth");
            _nodeDepth.RegisterValueChangedCallback((callback) =>
            {
                _mapData.NodeHeight = callback.newValue;
                _mapManager.GridGraphParamsChanged(_mapData);
            });
            #endregion
            //监听场景发生改变
            // EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
            EditorSceneManager.activeSceneChangedInEditMode += OnSceneChanged;
        }

        
        private void OnSceneChanged(Scene arg0, Scene arg1)
        {
            EditorSceneManager.activeSceneChangedInEditMode -= OnSceneChanged;
            _sceneView.rootVisualElement.Remove(_root);
            
            OnHide();
            _instance = null;
        }
        
        public void Show(MapManager mapManager, MapData mapData)
        {
            this._mapManager = mapManager;
            _mapData = mapData;

            SetValueWithoutNotify(mapData);
            SetToolbarExpandState(true);
            SelectBrush(NodeTag.WALK);
            OnShow();
        }

        private void SetValueWithoutNotify(MapData mapData)
        {
            _blockSizeSlider.SetValueWithoutNotify(mapData.BlockSize);
            _blockWidth.SetValueWithoutNotify(mapData.BlockWidth);
            _blockDepth.SetValueWithoutNotify(mapData.BlockHeight);
            
            _nodeSizeSlider.SetValueWithoutNotify( mapData.NodeSize);
            _nodeWidth.SetValueWithoutNotify(mapData.NodeWidth);
            _nodeDepth.SetValueWithoutNotify(mapData.NodeHeight);
        }
        

        private void SelectBrush(NodeTag brushType)
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

            _bursh = brushType;
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