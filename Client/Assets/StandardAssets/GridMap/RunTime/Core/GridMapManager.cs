using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Pathfinding;
using UnityEngine;
using UnityEngine.Pool;


[assembly: InternalsVisibleTo("GridMap.Editor")]

namespace GridMap
{
    
    /// <summary>
    /// 地图编辑场景管理
    /// </summary>
    [ExecuteInEditMode]
    public sealed class GridMapManager : MonoBehaviour
    {
        /// <summary>
        /// A*寻路系统的核心组件
        /// </summary>
        [SerializeField]
        private AstarPath AstarPath;
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

        private MapData _mapData;

        private ObjectPool<GameObject> _gridObjectPool;
        private Dictionary<GameObject, Vector2Int> _girdEnities;

        /// <summary>
        /// 网格图
        /// </summary>
        private GridGraph _graph;

        /// <summary>
        /// 贴图尺寸
        /// </summary>
        public float TextureSize
        {
            get => _mapData.TextureSize;
            internal set
            {
                _mapData.TextureSize = value;
                OnTextureSizeValueChanged(value);
            }
        }

        /// <summary>
        /// 节点尺寸
        /// </summary>
        public float NodeSize
        {
            get => _graph.nodeSize;
            internal set => SetGraph(_graph, value,Width,Depth,true);
        }

        public int Width
        {
            get => _graph.Width;
            internal set => SetGraph(_graph, NodeSize,value,Depth,true);
        }

        public int Depth
        {
            get => _graph.Depth;
            internal set => SetGraph(_graph, NodeSize,Width,value,true);
        }

        public Vector2 GraphCenter => _graph.center;
        
        /// <summary>
        /// 运行时获取格子贴图
        /// </summary>
        public Func<MapData, int, int, Texture> GridTextFunc;

        void Awake()
        {

#if UNITY_EDITOR
            //在播放模式之外用于初始化AstarPath对象，即使尚未在检查器中选择它。
            AstarPath.FindAstarPath();
            //选择AstarPath
            // Selection.activeGameObject = AstarPath.gameObject;   
#endif
            _gridObjectPool = new ObjectPool<GameObject>(OnCreateGridObj);
            _girdEnities = new Dictionary<GameObject, Vector2Int>();
        }

        private GameObject OnCreateGridObj()
        {
            var result = Instantiate(GridPrefab);
            result.hideFlags = HideFlags.HideAndDontSave;
            return result;
        }

        /// <summary>
        /// 设置GridMapData
        /// </summary>
        /// <param name="mapData"></param>
        public void SetGridMapData(MapData mapData)
        {
            Release();

            _mapData = mapData;
            //生成地图
            GenerateGridMap(mapData.ID, mapData.NumberOfRows, mapData.NumberOfColumns, mapData.TextureSize);
            //初始化A*
            SetGraph(mapData);
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Release()
        {
            _mapData = null;
            //清理所有格子
            _gridObjectPool?.Clear();
            _girdEnities?.Clear();

        }

        #region 生成格子图片

        /// <summary>
        /// 生成格子拼接的地图
        /// </summary>
        private void GenerateGridMap(int mapID, int rows, int columns, float scale)
        {
            for (var y = 0; y < rows; y++)
            {
                for (var x = 0; x < columns; x++)
                {
                    InstantiateGrid(mapID, x, y, scale);
                }
            }
        }

        /// <summary>
        /// 实例化格子
        /// </summary>
        /// <param name="id"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="scale"></param>
        private void InstantiateGrid(int id, int x, int y, float scale)
        {
            var grid = _gridObjectPool.Get();
            grid.transform.SetParent(SceneRoot.transform);
            _girdEnities[grid] = new Vector2Int(x, y);

            SetGridTransform(grid.transform, x, y, scale);

            var texture = GridTextFunc(_mapData, x, y);
            if (texture == null) return;
            var mr = grid.GetComponent<MeshRenderer>();
            var propertyBlock = new MaterialPropertyBlock();
            mr.GetPropertyBlock(propertyBlock);
            propertyBlock.SetTexture("_MainTex", texture);
            mr.SetPropertyBlock(propertyBlock);
        }

        private void SetGridTransform(Transform grid, int x, int y, float scale)
        {
            grid.localScale = Vector3.one * scale;
            var position = new Vector3((x + 0.5f) * scale, (y + 0.5f) * scale, 0);
            grid.SetLocalPositionAndRotation(position, Quaternion.identity);
        }
        #endregion

        #region 生成A*节点

        public void SetGraph(MapData mapData)
        {
            
            AstarPath.graphs[0].Scan();
            if (mapData.GraphData != null)
            {
                //反序列化graphs
                AstarPath.data.DeserializeGraphs(mapData.GraphData);
                // _graph = AstarPath.graphs[0] as GridGraph;
                // _graph.Scan();
            }
            else
            {
                var nodeSize = GridMapConfig.Instance.NodeSize;
                var width = (int)(mapData.NumberOfColumns * mapData.TextureSize / nodeSize);
                var height = (int)(mapData.NumberOfRows * mapData.TextureSize / nodeSize);
                var graph = AstarPath.graphs[0] as GridGraph;
                SetGraph(graph, nodeSize, width, height,true);
            }

            _graph = AstarPath.graphs[0] as GridGraph;
        }

        private void SetGraph(GridGraph graph, float nodeSize, int width, int depth, bool isScan = false)
        {
            graph.center = new Vector3(width * nodeSize * 0.5f, depth * nodeSize * 0.5f, 0);
            graph.SetDimensions(width, depth, nodeSize);
            if (isScan)
            {
                _graph?.Scan();
            }
        }
        
        /// <summary>
        /// 重置贴图尺寸发生改变
        /// </summary>
        /// <param name="newSize"></param>
        private void OnTextureSizeValueChanged(float newSize)
        {
            foreach (var keyValue in _girdEnities)
            {
                SetGridTransform(keyValue.Key.transform, keyValue.Value.x, keyValue.Value.y, newSize);
            }
        }
        
        internal byte[] SerializeGraphs()
        {
            var settings = new Pathfinding.Serialization.SerializeSettings
            {
                nodes = true,
                editorSettings = true
            };
          
            var bytes =   AstarPath.data.SerializeGraphs(settings);
            return bytes;
        }
        #endregion


        internal void SetNodeWalkableAndTag(int x,int y, int brushType)
        {
            var node = _graph.GetNode(x, y);
            if (node == null)
            {
                return;
            }
            node.Walkable = brushType > 0;
            node.Tag = brushType > 0 ? (uint)(1 << brushType) : 0;
            node.SetConnectivityDirty();
        }
        
        internal void SetNodeWalkableAndTag(Vector2 position, int brushType)
        {
            var node = AstarPath.active.GetNearest(position).node;
            if (node == null)
            {
                return;
            }
            node.Walkable = brushType > 0;
            node.Tag = brushType > 0 ? (uint)(1 << brushType) : 0;
            node.SetConnectivityDirty();
        }
    }
}