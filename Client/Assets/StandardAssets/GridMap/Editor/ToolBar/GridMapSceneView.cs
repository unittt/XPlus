using System.Collections.Generic;
using System.Linq;
using HT.Framework;
using Pathfinding;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace StandardAssets.GridMap.Editor.ToolBar
{
    public class GridMapSceneView : HTFEditorWindow
    {
        //根节点,黑色条
        private  VisualElement _root;
        //toolbar面板
        private  VisualElement _toolBarPanel;
        private  VisualElement _infoPanel;
        
        private  VisualElement _hideBarBtn;
        private  VisualElement _ShowBarBtn;
        
        private  bool _overToolBarBg;
        private  bool _overToolBar;

        private  List<VisualElement> _toolVisualElements = new();


        private MapData _mapData;
        private GridGraph _graph;
        private Slider _textureSizeSlider;
        private Slider _nodeSizeSlider;
        private SliderInt _nodeWidth;
        private SliderInt _nodeDepth;
        private GridMapManager _gridMapManager;


        private static GridMapSceneView _instance;
        public static GridMapSceneView Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GridMapSceneView();
                }

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
            SceneView.lastActiveSceneView.in2DMode = true;
            //初始化设置
            var sceneView = SceneView.lastActiveSceneView;
            var toolbarTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/StandardAssets/GridMap/Editor/ToolBar/GridMapSceneView.uxml");
            _root = toolbarTreeAsset.CloneTree().Children().First();
            sceneView.rootVisualElement.Add(_root);
            _root.style.position = Position.Absolute;
            _root.style.bottom = 0;
            _root.style.width = sceneView.position.width;
            _root.style.flexGrow  = 1;
            _root.BringToFront();
            
            _toolBarPanel = _root.Q<VisualElement>("toolbar");
            _toolBarPanel.style.alignSelf = Align.Center;
            
            var btnWalk = _root.Q<VisualElement>("Walk");
            btnWalk.tooltip = "行走";
            AddElement(btnWalk);
            btnWalk.RegisterCallback((MouseDownEvent e) =>
            {
                OnClickElement(btnWalk);
            });
            
            var btnFly = _root.Q<VisualElement>("Fly");
            btnFly.tooltip = "飞行";
            AddElement(btnFly);
            btnFly.RegisterCallback((MouseDownEvent e) =>
            {
                OnClickElement(btnFly);
            });
            var btnTransparency= _root.Q<VisualElement>("Transparency");
            btnTransparency.tooltip = "透明区域";
            AddElement(btnTransparency);
            btnTransparency.RegisterCallback((MouseDownEvent e) =>
            {
                OnClickElement(btnTransparency);
            });
            var btnClear = _root.Q<VisualElement>("Clear");
            btnClear.tooltip = "清理";
            AddElement(btnClear);
            btnClear.RegisterCallback((MouseDownEvent e) =>
            {
                OnClickElement(btnClear);
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

            _root.RegisterCallback((PointerOverEvent e) =>
            {
                _overToolBarBg = true;
            });
            _root.RegisterCallback((PointerOutEvent e) =>
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
       
            
            _infoPanel =  _root.Q<VisualElement>("InfoPanel");
            _textureSizeSlider =  _root.Q<Slider>("TextureSizeSlider");
            _textureSizeSlider.RegisterValueChangedCallback((callback) =>
            {
                _mapData.TexturSize = callback.newValue;
                GridMapManager.Current.ResetTextureSize( callback.newValue);
            });
            
            _nodeSizeSlider =  _root.Q<Slider>("NodeSizeSlider");
            _nodeSizeSlider.RegisterValueChangedCallback((callback) =>
            {
                OnGraphParamsValueChanged();
            });
            
            _nodeWidth =  _root.Q<SliderInt>("NodeWidth");
            _nodeWidth.RegisterValueChangedCallback((callback) =>
            {
                OnGraphParamsValueChanged();
            });
            
            _nodeDepth =  _root.Q<SliderInt>("NodeDepth");
            _nodeDepth.RegisterValueChangedCallback((callback) =>
            {
                OnGraphParamsValueChanged();
            });
            
            //监听场景发生改变
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private void OnPlayModeStateChanged(PlayModeStateChange obj)
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            if (_root == null) return;
            var sceneView = SceneView.lastActiveSceneView;
            sceneView.rootVisualElement.Remove(_root);
            _instance = null;
        }


        private void OnGraphParamsValueChanged()
        {
            GridMapManager.Current.SetGraph(_graph.center, _nodeSizeSlider.value, _nodeWidth.value, _nodeDepth.value);
            GridMapManager.Current.Graph.Scan();
        }
        
        public void Show(MapData mapData)
        {
            _mapData = mapData;
            _graph = AstarPath.active.graphs[0] as GridGraph;
            _textureSizeSlider.value = mapData.TexturSize;
            _nodeSizeSlider.value = _graph.nodeSize;
            _nodeWidth.value = _graph.Width;
            _nodeDepth.value = _graph.Depth;
            
            SetToolbarExpandState(true);
        }

        private  void AddElement(VisualElement visualElement)
        {
            _toolVisualElements.Add(visualElement);
        }

        private  void OnClickElement(VisualElement visualElement)
        {
            var color = Color.green;
            foreach (var element in _toolVisualElements)
            {
                color.a = element == visualElement ?1 : 0;
                element.style.backgroundColor = new StyleColor(color);
            }
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