using System;
using System.Collections.Generic;
using System.IO;
using HT.Framework;
using Pathfinding;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace GridMap
{
    public enum GridEditorType
    {
        Nothing = 0,
        NpcArea = 1,
        TouchEffect = 2,
    }
    
    public sealed class GridMapGeneratorWindow : HTFEditorWindow
    {
        
        private string _curSceneId;
        private Vector2 _scrollPos;
        
        /// <summary>
        /// GridMapGeneratorWindow
        /// </summary>
        [MenuItem("地图/2D地图编辑工具", false, 0)]
        private static void ShowWindow()
        {
            var window = GetWindow<GridMapGeneratorWindow>(true, "GridMapGeneratorWindow", true);
            window.position = new Rect(200, 200, 600, 350);
            window.minSize = new Vector2(600, 350);
            window.maxSize = new Vector2(600, 350);
            window.Show();
        }
        
        protected override void OnBodyGUI()
        {
            OnPathGUI();
            EditorGUILayout.Space();
            
            _curSceneId = EditorGUILayout.TextField("场景Id:", _curSceneId);
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
            
            OnEditPathfindingGUI();
            EditorGUILayout.EndScrollView();
        }
        
        protected void OnDestroy()
        {
            GridMapGlobalConfig.Instance.SaveAsset();
        }
        
        #region 绘制路径
        /// <summary>
        /// 绘制路径
        /// </summary>
        private void OnPathGUI()
        {
            OnScenePathGUI();
            OnRawDataPathGUI();
            OnNavDataPathGUI();
            OnConfigRootPathGUI();
        }
        /// <summary>
        /// 绘制场景路径
        /// </summary>
        private void OnScenePathGUI()
        {
            GUILayout.BeginHorizontal();
            
            var scenePath = EditorGUILayout.TextField("编辑场景路径", GridMapGlobalConfig.Instance.EditorScenePath);
            if (!string.IsNullOrEmpty(scenePath) && scenePath != GridMapGlobalConfig.Instance.EditorScenePath)
            {
                GridMapGlobalConfig.Instance.EditorScenePath = AbsoluteToRelativePath(scenePath);
            }
            if (GUILayout.Button("Browse", EditorStyles.miniButton, GUILayout.Width(80)))
            {
                var path = EditorUtility.OpenFilePanel("选择编辑场景", Application.dataPath, "unity");
                if (!string.IsNullOrEmpty(path))
                {
                    GridMapGlobalConfig.Instance.EditorScenePath = AbsoluteToRelativePath(path);
                }
            }
            GUILayout.EndHorizontal();
        }
        
        /// <summary>
        /// 绘制原始数据根路径
        /// </summary>
        private void OnRawDataPathGUI()
        {
            GUILayout.BeginHorizontal();
            var rawDataPath = EditorGUILayout.TextField("原始数据根路径", GridMapGlobalConfig.Instance.RawDataPath);
            if (!string.IsNullOrEmpty(rawDataPath) && rawDataPath != GridMapGlobalConfig.Instance.RawDataPath)
            {
                GridMapGlobalConfig.Instance.RawDataPath = AbsoluteToRelativePath(rawDataPath);
            }
            if (GUILayout.Button("Browse", EditorStyles.miniButton, GUILayout.Width(80)))
            {
                var path = EditorUtility.OpenFolderPanel("选择原始数据根路径", Application.dataPath,"");
                if (!string.IsNullOrEmpty(path))
                {
                    GridMapGlobalConfig.Instance.RawDataPath = AbsoluteToRelativePath(path);
                }
            }
            GUILayout.EndHorizontal();
        }
        
        /// <summary>
        /// 绘制导航数据根路径
        /// </summary>
        private void OnNavDataPathGUI()
        {
            GUILayout.BeginHorizontal();
            var navDataPath = EditorGUILayout.TextField("导航数据根路径", GridMapGlobalConfig.Instance.NavDataRootPath);
            if (!string.IsNullOrEmpty(navDataPath) && navDataPath != GridMapGlobalConfig.Instance.NavDataRootPath)
            {
                GridMapGlobalConfig.Instance.NavDataRootPath = AbsoluteToRelativePath(navDataPath);
            }
            if (GUILayout.Button("Browse", EditorStyles.miniButton, GUILayout.Width(80)))
            {
                var path = EditorUtility.OpenFolderPanel("选择导航数据根路径", Application.dataPath,"");
                if (!string.IsNullOrEmpty(path))
                {
                    GridMapGlobalConfig.Instance.NavDataRootPath = AbsoluteToRelativePath(path);
                }
            }
            GUILayout.EndHorizontal();
        }
        
        /// <summary>
        /// 绘制配置根路径
        /// </summary>
        private void OnConfigRootPathGUI()
        {
            GUILayout.BeginHorizontal();
            var configRootPath = EditorGUILayout.TextField("配置根路径", GridMapGlobalConfig.Instance.ConfigRootPath);
            if (!string.IsNullOrEmpty(configRootPath) && configRootPath != GridMapGlobalConfig.Instance.ConfigRootPath)
            {
                GridMapGlobalConfig.Instance.ConfigRootPath = AbsoluteToRelativePath(configRootPath);
            }
            if (GUILayout.Button("Browse", EditorStyles.miniButton, GUILayout.Width(80)))
            {
                var path = EditorUtility.OpenFolderPanel("选择配置根路径", Application.dataPath,"");
                if (!string.IsNullOrEmpty(path))
                {
                    GridMapGlobalConfig.Instance.ConfigRootPath = AbsoluteToRelativePath(path);
                }
            }
            GUILayout.EndHorizontal();
        }

        /// <summary>
        ///  将绝对路径转换为相对于 Application.dataPath 的路径
        /// </summary>
        /// <param name="absolutePath"></param>
        /// <returns></returns>
        string AbsoluteToRelativePath(string absolutePath)
        {
            var absoluteUri = new Uri(absolutePath);
            var dataPathUri = new Uri(Application.dataPath);
            // 使用 Uri 的 MakeRelativeUri 方法来计算相对路径
            var relativeUri = dataPathUri.MakeRelativeUri(absoluteUri);
            // 将 Uri 转换为字符串
            var relativePath = Uri.UnescapeDataString(relativeUri.ToString());
            return relativePath;
        }
        
        #endregion

        private void OnEditPathfindingGUI()
        {
            //生成寻路信息子面板
            EditorGUILayout.BeginVertical("HelpBox", GUILayout.Width(600));
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("2D场景寻路信息编辑", "BoldLabel");
                // _savaNpcArea = GUILayout.Toggle(_savaNpcArea, "重新产生Npc生成区域，给策划用的");
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("编辑寻路信息", "LargeButton", GUILayout.Height(50f)))
                {
                    
                    //如果在场景中 并且 设置A*参数
                    if (ValidateSceneOpen() && SetupAStarPath(_curSceneId))
                    {
                        // gridEditorType = GridEditorType.Notdddhing;
                        // LoadScene(_curSceneId);
                        // if (BrushTool != null)
                        // {
                        //     Selection.activeGameObject = BrushTool.gameObject;
                        //     BrushTool.EndEditMode(false);
                        // }
                        //
                        // if (AreaTool != null)
                        // {
                        //     Selection.activeGameObject = AreaTool.gameObject;
                        //     AreaTool.EndEditMode(false);
                        //     AreaTool.Clear();
                        // }
                    }
                }

                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 检查编辑场景是否打开
        /// </summary>
        /// <returns></returns>
        private bool ValidateSceneOpen()
        {
            var isInScene = EditorSceneManager.GetActiveScene().path == GridMapGlobalConfig.Instance.EditorScenePath;
            if (!isInScene && EditorUtility.DisplayDialog("提示", "需要打开2d测试场景才能生成2d场景寻路数据,是否继续?", "Yes", "No"))
            {
                EditorSceneManager.OpenScene( GridMapGlobalConfig.Instance.EditorScenePath);
            }
            return isInScene;
        }

        /// <summary>
        /// 设置AStarPath参数
        /// </summary>
        /// <returns></returns>
        private bool SetupAStarPath(string sceneId)
        {
            if (!ValidateSceneId(sceneId)) return false;
           
            //加载_gridRef
            var configPath = string.Format("{0}/se_config_{1}.bytes", GridMapGlobalConfig.Instance.ConfigRootPath, sceneId);
            var _gridRef = AssetDatabase.LoadAssetAtPath("Assets/GameRes/Map2d/1010/gridRef_1010.png", typeof(Texture2D)) as Texture2D;
            if (_gridRef == null)
            {
                Debug.LogError("加载gridRef贴图失败 " );
                return false;
            }

            // GridMapEditorSceneManager.Current.SetupAstarPath(_gridRef);
            return  true;
        }
        
        /// <summary>
        /// 验证当前输入场景Id是否正确
        /// </summary>
        /// <returns></returns>
        private bool ValidateSceneId(string sceneId)
        {
            return !string.IsNullOrEmpty(sceneId) && Directory.Exists( GridMapGlobalConfig.Instance.RawDataPath + "/" + sceneId);
        }
        
        #region 预览2d场景地图
        private AstarPath _astarPath;
        private GameObject _sceneRoot;
        private GameObject _target;
        private GameObject _player;
        private GameObject _sceneCam;
        private bool _hideTileGo = true;
        private bool _savaNpcArea = false;

        public AstarPath AstarPath
        {
            get
            {
                if (_astarPath == null)
                {
                    _astarPath = AstarPath.active;
                }
                return _astarPath;
            }
        }

        public GameObject SceneRoot
        {
            get
            {
                if (_sceneRoot == null)
                {
                    
                    _sceneRoot = GameObject.Find("SceneRoot");
                }
                return _sceneRoot;
            }
        }

        public GameObject Target
        {
            get
            {
                if (_target == null)
                {
                    _target = GameObject.Find("_Target");
                }
                return _target;
            }
        }

        public GameObject Player
        {
            get
            {
                if (_player == null)
                {
                    _player = GameObject.Find("_Player");
                }
                return _player;
            }
        }
        

        #endregion
    }
}