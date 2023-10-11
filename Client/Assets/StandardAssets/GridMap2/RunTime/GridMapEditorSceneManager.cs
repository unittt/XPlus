using System;
using System.Collections.Generic;
using HT.Framework;
using Pathfinding;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;


/// <summary>
/// 地图编辑场景管理
/// </summary>
[ExecuteInEditMode]
public sealed class GridMapEditorSceneManager : MonoBehaviour
{
    /// <summary>
    /// A*寻路系统的核心组件
    /// </summary>
    [Label(" A*寻路系统的核心组件")] public AstarPath AstarPath;
    
    /// <summary>
    /// 场景指定的根目录
    /// </summary>
    [Label("场景指定的根目录")] public GameObject SceneRoot;

    /// <summary>
    /// 场景相机
    /// </summary>
    [Label("场景相机")] public GameObject SceneCam;

    
    public GameObject GridPrefab;
    /// <summary>
    /// 是否为运行时
    /// </summary>
    public bool IsRuntime = false;

    private MapData _mapData;
    
    private List<SpriteRenderer> _activeSpriteList = new List<SpriteRenderer>();
    private Queue<SpriteRenderer> _inactiveSpritePool = new Queue<SpriteRenderer>();

    /// <summary>
    /// 运行时获取格子贴图
    /// </summary>
    public Func<int, int, int, Texture> GridTextFunc;

   
    private ObjectPool<GameObject> _gridObjectPool;

    void Awake()
    {
       
#if UNITY_EDITOR
        //在播放模式之外用于初始化AstarPath对象，即使尚未在检查器中选择它。
        AstarPath.FindAstarPath();
        //选择AstarPath
        // Selection.activeGameObject = AstarPath.gameObject;   
#endif
        _gridObjectPool = new ObjectPool<GameObject>(OnCreateGridObj);
    }
    
    private GameObject OnCreateGridObj()
    {
        var result = Instantiate(GridPrefab);
        result.hideFlags = HideFlags.HideAndDontSave;
        return result;
    }

    private void Start()
    {
        // if (!IsRuntime)
        // {
        //     //加载图片
        //     var mapID = EditorPrefs.GetInt(MapGlobal.EDITOR_MAP_ID_KEY);
        //     // GridMapConfig.Instance.TryGetGridMapInfo(mapID, out _gridMapInfo);
        //     // LoadGridMapInfo();
        //     // GenerateGridMap(_gridMapInfo.ID, _gridMapInfo.Rows,_gridMapInfo.Columns,2.5f);
        //     //生成地图
        //     // GenerateGridMap(1010, 13,24,2.5f);
        //     //初始化A*
        //     // InitPathfinder(1,1,1);
        // }

        // if (Application.isEditor)
        // {
        //     
        //     //选择
        //     Selection.activeGameObject = AstarPath.gameObject;
        //     var mapID = EditorPrefs.GetInt(MapGlobal.EDITOR_MAP_ID_KEY);
        //     var path = $"{GridMapConfig.Instance.DataFolderPath}/mapdata_{mapID}.bytes";
        //     var textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
        //     var mapData = MapData.Deserialize(textAsset.bytes) ;
        //     SetGridMapData(mapData);
        // }
        // else  if (Application.isPlaying && SceneManager.GetActiveScene().path == GridMapConfig.Instance.ScenePath)
        // {
        //     //测试模式
        // }
        // else
        // {
        //     //运行模式
        // }
    }

    // void OnDestroy()
    // {
    //     Release();
    // }

    
    /// <summary>
    /// 设置GridMapData
    /// </summary>
    /// <param name="mapData"></param>
    public void SetGridMapData(MapData mapData)
    {
        Release();

        _mapData = mapData;
        //生成地图
        GenerateGridMap(mapData.ID,mapData.NumberOfRows,mapData.NumberOfColumns,mapData.TexturSize);
        //初始化A*
        SetGraphs(mapData);
    }

    /// <summary>
    /// 释放
    /// </summary>
    public void Release()
    {
        _mapData = null;
        //清理所有格子
        _gridObjectPool?.Clear();

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
        grid.transform.localScale = Vector3.one * scale;
        var position = new Vector3((x + 0.5f) * scale, (y + 0.5f) * scale, 0);
        grid.transform.SetLocalPositionAndRotation(position, Quaternion.identity);

        var texture = GetGridTexture(id, x, y);
        if (texture != null)
        {
            var mr = grid.GetComponent<MeshRenderer>();
            var propertyBlock = new MaterialPropertyBlock();
            mr.GetPropertyBlock(propertyBlock);
            propertyBlock.SetTexture("_MainTex", GetGridTexture(id, x, y));
            mr.SetPropertyBlock(propertyBlock);
        }
    }

    private Texture GetGridTexture(int id, int x, int y)
    {
        return IsRuntime ? GridTextFunc(id, x, y) : MapGlobal.GetGridTexture(_mapData.TextureFolder,id,x,y);
    }
    #endregion

    #region 生成A*节点



    /// <summary>
    /// 网格图
    /// </summary>
    public GridGraph Graph { get; private set; }

    private void SetGraphs(MapData mapData)
    {
        if (mapData.GraphData != null)
        {
            //反序列化graphs
            AstarPath.active.data.DeserializeGraphs(mapData.GraphData);
        }
        else
        {
            var nodeSize = GridMapConfig.Instance.NodeSize;
            var centerPoint = new Vector2(mapData.NumberOfColumns , mapData.NumberOfRows) *  (mapData.TexturSize * 0.5f);
            var width = (int)(mapData.NumberOfColumns * mapData.TexturSize  / nodeSize);
            var height =  (int)(mapData.NumberOfRows * mapData.TexturSize /nodeSize);
            SetGraph(centerPoint, nodeSize, width, height);
        }
        Graph = AstarPath.graphs[0] as GridGraph;
        Graph.Scan();
    }
    
    private void SetGraph(Vector2 centerPoint, float nodeSize, int width, int height)
    {
        var graph = AstarPath.graphs[0] as GridGraph;
        graph.nodeSize = nodeSize;
        graph.Width = width;
        graph.Depth = height;
        graph.center = centerPoint;
        //更新size 根据 Width 和 Depth
        graph.UpdateSizeFromWidthDepth();
        AstarPath.unwalkableNodeDebugSize = nodeSize;
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
    //     SetGraph(nodeSize, width, height);
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
    #endregion
    
    
    
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
