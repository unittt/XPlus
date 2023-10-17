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
        public AstarPath AstarPath;

        /// <summary>
        /// 场景指定的根目录
        /// </summary>
        public GameObject SceneRoot;

        /// <summary>
        /// 场景相机
        /// </summary>
        public GameObject SceneCam;

        public GameObject GridPrefab;

        /// <summary>
        /// 是否为运行时
        /// </summary>
        public bool IsRuntime = false;

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
            internal set => SetGraph(_graph,_graph.center, value,Width,Depth,true);
        }

        public int Width
        {
            get => _graph.Width;
            internal set => SetGraph(_graph,_graph.center, NodeSize,value,Depth,true);
        }

        public int Depth
        {
            get => _graph.Depth;
            internal set => SetGraph(_graph,_graph.center, NodeSize,Width,value,true);
        }

        /// <summary>
        /// 运行时获取格子贴图
        /// </summary>
        public static Func<int, int, int, Texture> GridTextFunc;

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

            var texture = GetGridTexture(id, x, y);
            if (texture == null) return;
            var mr = grid.GetComponent<MeshRenderer>();
            var propertyBlock = new MaterialPropertyBlock();
            mr.GetPropertyBlock(propertyBlock);
            propertyBlock.SetTexture("_MainTex", GetGridTexture(id, x, y));
            mr.SetPropertyBlock(propertyBlock);
        }

        private void SetGridTransform(Transform grid, int x, int y, float scale)
        {
            grid.localScale = Vector3.one * scale;
            var position = new Vector3((x + 0.5f) * scale, (y + 0.5f) * scale, 0);
            grid.SetLocalPositionAndRotation(position, Quaternion.identity);
        }

        private Texture GetGridTexture(int id, int x, int y)
        {
            return IsRuntime ? GridTextFunc(id, x, y) : MapGlobal.GetGridTexture(_mapData.TextureFolder, id, x, y);
        }

        #endregion

        #region 生成A*节点

        public void SetGraph(MapData mapData)
        {
            if (mapData.GraphData != null)
            {
                //反序列化graphs
                AstarPath.active.data.DeserializeGraphs(mapData.GraphData);
            }
            else
            {
                var nodeSize = GridMapConfig.Instance.NodeSize;
                var centerPoint = GetGraphCenterPoint(mapData.TextureSize);
                var width = (int)(mapData.NumberOfColumns * mapData.TextureSize / nodeSize);
                var height = (int)(mapData.NumberOfRows * mapData.TextureSize / nodeSize);
                var graph = AstarPath.graphs[0] as GridGraph;
                SetGraph(graph, centerPoint, nodeSize, width, height, false);
            }

            _graph = AstarPath.graphs[0] as GridGraph;
            Scan();
        }

        private void SetGraph(GridGraph graph, Vector2 centerPoint, float nodeSize, int width, int depth, bool isScan)
        {
            graph.center = centerPoint;
            graph.SetDimensions(width, depth, nodeSize);
            if (isScan)
            {
                Scan();
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

            //设置中心点
            var centerPoint = GetGraphCenterPoint(newSize);
            SetGraph(_graph, centerPoint, NodeSize, Width, Depth, false);
        }

        private Vector2 GetGraphCenterPoint(float textureSize)
        {
            return new Vector2(_mapData.NumberOfColumns, _mapData.NumberOfRows) * (textureSize * 0.5f);
        }


        private void Scan()
        {
            _graph.Scan();
        }

        /// <summary>
        /// 保存SaveGridGraph
        /// </summary>
        /// <param name="nodeSize"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="nodeInfoDic"></param>
        // public void SaveGridGraph(int nodeSize, int width, int height, Dictionary<Vector2Int,bool> nodeInfoDic)
        // {
        //     var graph = AstarPath.graphs[0] as GridGraph;
        //     // Save to file
        //     foreach (var nodeInfo in nodeInfoDic)
        //     {
        //         var gridNode =  graph.GetNode(nodeInfo.Key.x, nodeInfo.Key.y);
        //         gridNode.Walkable = nodeInfo.Value;
        //     }
        //     
        //     //将所有图形设置序列化为字节数组。
        //     var graphData = AstarPath.active.data.SerializeGraphs();
        //     //将指定的数据保存在指定的路径
        //     Pathfinding.Serialization.AstarSerializer.SaveToFile("",graphData);
        // }

        internal byte[] SerializeGraphs()
        {
            var c = AstarPath.active.data.SerializeGraphs();
            return c;
        }
        #endregion


        public void Xxxxxxx(Vector2 position, int brushType)
        {
            
            var info = AstarPath.active.GetNearest(position);
            var node = info.node;
            var closestPoint = info.position;
            if (node != null)
            {
                node.Walkable = true;
                node.Tag = (uint)brushType;
                node.
            }
           
        }

        // var gridSizeX = 10;
        // var gridSizeZ = 10;
        // var nodeSize = 1;
        // // 创建一个新的GridGraph
        // GridGraph gridGraph = AstarData.active.data.AddGraph(typeof(GridGraph)) as GridGraph;
        // gridGraph.width = gridSizeX; // 设置网格的宽度
        // gridGraph.depth = gridSizeZ; // 设置网格的深度
        // //格子的宽度 和 深度
        // //设置可行走区域
        // //grid的scale
        //
        // // 添加节点
        // for (int x = 0; x < gridSizeX; x++)
        // {
        //     for (int z = 0; z < gridSizeZ; z++)
        //     {
        //         int nodeIndex = x + z * gridSizeX;
        //         bool walkable = true;
        //         Int3 nodePosition = new Int3(x * nodeSize, 0, z * nodeSize); // 节点坐标
        //
        //         // gridGraph.nodes[nodeIndex] = gridGraph.CreateNodes(typeof(GridNode), 1)[0];
        //
        //         var gridGraphNode = gridGraph.nodes[nodeIndex];
        //         gridGraphNode.position = nodePosition;
        //         gridGraphNode.Walkable = walkable;
        //
        //     }
        // }
    }
}