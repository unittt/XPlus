using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Pathfinding;
using UnityEngine;


[assembly: InternalsVisibleTo("GridMap.Editor")]
namespace GridMap
{
    /// <summary>
    /// 地图管理
    /// </summary>
    [ExecuteInEditMode]
    public sealed class MapManager : MonoBehaviour
    {
        /// <summary>
        /// 场景指定的根目录
        /// </summary>
        [SerializeField]
        private GameObject SceneRoot;
        /// <summary>
        /// 预制件
        /// </summary>
        [SerializeField]
        public GameObject GridPrefab;

        /// <summary>
        /// 是否动态加载
        /// </summary>
        [SerializeField]
        private bool _isDynamicsLoad;

        /// <summary>
        /// 是否动态加载地块
        /// </summary>
        public bool IsDynamics => !Application.isEditor && _isDynamicsLoad;

        private MapData _mapData;
        
        /// <summary>
        /// 网格图
        /// </summary>
        private GridGraph _graph;

        /// <summary>
        /// 块尺寸
        /// </summary>
        public float BlockSize
        {
            get => _mapData.BlockSize;
            internal set
            {
                _mapData.BlockSize = value;
                BlockParamsChanged(_mapData);
            }
        }
        
        public int BlockWidth
        {
            get => _mapData.BlockWidth;
            internal set
            {
                _mapData.BlockWidth = value;
                BlockParamsChanged(_mapData);
            }
        }
        
        public int BlockHeight
        {
            get => _mapData.BlockHeight;
            internal set
            {
                _mapData.BlockHeight = value;
                BlockParamsChanged(_mapData);
            }
        }
        

        /// <summary>
        /// 节点尺寸
        /// </summary>
        public float NodeSize
        {
            get => GraphHelper.NodeSize;
            internal set => GridGraphParamsChanged(NodeWidth,NodeDepth,value);
        }

        /// <summary>
        /// 格子宽个数
        /// </summary>
        public int NodeWidth
        {
            get => GraphHelper.Width;
            internal set => GridGraphParamsChanged(value,NodeDepth,NodeSize);
        }

        /// <summary>
        /// 格子高个数
        /// </summary>
        public int NodeDepth
        {
            get => GraphHelper.Depth;
            internal set => GridGraphParamsChanged(NodeWidth, value,NodeSize);
        }

        /// <summary>
        /// 序列话数据
        /// </summary>
        internal byte[] SerializeGraphs => GraphHelper.SerializeGraphs;
        
        public Vector2 GraphCenter => GraphHelper.GraphCenter;
        
        /// <summary>
        /// 运行时获取格子贴图
        /// </summary>
        public Func<MapData, int, int, Texture> GridTextFunc;
        
        internal GraphHelper GraphHelper { get; private set; }
        internal BlockHelper BlockHelper { get; private set; }
        internal CameraHelper CameraHelper { get; private set; }

        private List<MapHelper> _helpers;

        void Awake()
        {
            
            GraphHelper = new GraphHelper();
            GraphHelper = RegisterHelper<GraphHelper>();
            BlockHelper = RegisterHelper<BlockHelper>();
            CameraHelper = RegisterHelper<CameraHelper>();
            
            foreach (var helper in _helpers)
            {
                helper.Map = this;
                helper.OnInit();
            }
        }

        private T RegisterHelper<T>() where T : MapHelper, new()
        {
            var helper = new T
            {
                Map = this
            };
            _helpers.Add(helper);
            return helper;
        }
        
        /// <summary>
        /// 设置GridMapData
        /// </summary>
        /// <param name="mapData"></param>
        public void SetMapData(MapData mapData)
        {
            _mapData = mapData;
            foreach (var helper in _helpers)
            {
                helper.OnSetMapData(mapData);
            }
        }
        
        private void GridGraphParamsChanged( int width, int height,float nodeSize)
        {
            foreach (var helper in _helpers)
            {
                helper.OnGridGraphParamsChanged(width,height,nodeSize);
            }
        }

        /// <summary>
        /// 块参数发生改变
        /// </summary>
        /// <param name="newSize"></param>
        private void BlockParamsChanged(MapData mapData)
        {
            foreach (var helper in _helpers)
            {
                helper.OnBlockParamsChanged(mapData.BlockWidth,mapData.BlockHeight, mapData.BlockSize);
            }
        }
        
        internal void SetNodeWalkableAndTag(Vector2 position, int tag)
        {
            GraphHelper.SetNodeWalkableAndTag(position,tag);
        }
        
        /// <summary>
        /// 释放
        /// </summary>
        public void Release()
        {
            _mapData = null;
            foreach (var helper in _helpers)
            {
                helper.OnRelease();
            }
        }
    }
}