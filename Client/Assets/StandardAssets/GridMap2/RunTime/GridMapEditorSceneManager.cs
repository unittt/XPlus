using System.Collections.Generic;
using HT.Framework;
using Pathfinding;
using UnityEditor;
using UnityEngine;


/// <summary>
/// 地图编辑场景管理
/// </summary>
[ExecuteInEditMode]
public sealed class GridMapEditorSceneManager : SingletonBehaviourBase<GridMapEditorSceneManager>
{
    /// <summary>
    /// A*寻路系统的核心组件
    /// </summary>
    [Label(" A*寻路系统的核心组件")] public AstarPath AstarPath;

    /// <summary>
    /// 网格图节点大小
    /// </summary>
    [Label("网格图节点大小")] public float GridGraphNodeSize = 0.32f;
    
    /// <summary>
    /// 场景指定的根目录
    /// </summary>
    [Label("场景指定的根目录")] public GameObject SceneRoot;

    /// <summary>
    /// 场景相机
    /// </summary>
    [Label("场景相机")] public GameObject SceneCam;

    
    public GameObject GridPrefab;

    private List<SpriteRenderer> _activeSpriteList = new List<SpriteRenderer>();
    private Queue<SpriteRenderer> _inactiveSpritePool = new Queue<SpriteRenderer>();
    private const float SpriteTile = 2.56f;
    private int _maxSizeX;
    private int _maxSizeY;



    private GridMapInfo _gridMapInfo;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        LoadGridMapInfo();
        // GenerateGridMap(_gridMapInfo.ID, _gridMapInfo.Rows,_gridMapInfo.Columns,2.5f);
        //生成地图
        GenerateGridMap(1010, 13,24,2.5f);
        //初始化A*
        InitPathfinder(1,1,1);
    }



    private void LoadGridMapInfo()
    {
        //加载图片
        var mapID = EditorPrefs.GetInt(MapGlobal.EDITOR_MAP_ID_KEY);
        GridMapConfig.Instance.TryGetGridMapInfo(mapID, out _gridMapInfo);
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
                GenerateGrid(mapID, x, y, scale);
            }
        }
    }

    private void GenerateGrid(int id, int x, int y, float scale)
    {
        var grid = Instantiate(GridPrefab, new Vector3(x, y, 0) * scale, Quaternion.identity, SceneRoot.transform);
        grid.transform.localScale = Vector3.one * scale;
        var mr = grid.GetComponent<MeshRenderer>();
        var propertyBlock = new MaterialPropertyBlock();
        mr.GetPropertyBlock(propertyBlock);
        propertyBlock.SetTexture("_MainTex", GetGridTexture(id, x, y));
        mr.SetPropertyBlock(propertyBlock);
    }

    private Texture GetGridTexture(int id, int x, int y)
    {
        var path = $"{_gridMapInfo.TileFolder}/tile_{id}_{x}_{y}.png";
        var texture = AssetDatabase.LoadAssetAtPath<Texture>(path);
        return texture;
    }

    #endregion

    #region 生成A*节点



    /// <summary>
    /// 网格图
    /// </summary>
    public GridGraph Graph { get; private set; }

    public void InitPathfinder(int nodeSize, int width, int height)
    {
        //读取
        var graphData = AssetDatabase.LoadAssetAtPath<TextAsset>("");

        if (graphData != null)
        {
            //反序列化graphs
            AstarPath.active.data.DeserializeGraphs(graphData.bytes);  
            // AstarPath.active.Scan();
        }
        else
        {
            SetGraph(nodeSize, width, height);
        }

        Graph = AstarPath.graphs[0] as GridGraph;
    }


    private void SetGraph(int nodeSize, int width, int height)
    {
        var graph = AstarPath.graphs[0] as GridGraph;
        graph.nodeSize = nodeSize;
        graph.Width = width;
        graph.Depth = height;
        //更新size 根据 Width 和 Depth
        graph.UpdateSizeFromWidthDepth();

        //设置中心点 （为图片的中心点）
        graph.center = Vector3.zero -
                       (GridGraphEditor.RoundVector3(graph.transform.Transform(new Vector3(0, 0, 0))) -
                        graph.center);
    }

    /// <summary>
    /// 保存SaveGridGraph
    /// </summary>
    /// <param name="nodeSize"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="nodeInfoDic"></param>
    public void SaveGridGraph(int nodeSize, int width, int height, Dictionary<Vector2Int,bool> nodeInfoDic)
    {
        SetGraph(nodeSize, width, height);
        var graph = AstarPath.graphs[0] as GridGraph;
        // Save to file
        foreach (var nodeInfo in nodeInfoDic)
        {
            var gridNode =  graph.GetNode(nodeInfo.Key.x, nodeInfo.Key.y);
            gridNode.Walkable = nodeInfo.Value;
        }
        
        //将所有图形设置序列化为字节数组。
        var graphData = AstarPath.active.data.SerializeGraphs();
        //将指定的数据保存在指定的路径
        Pathfinding.Serialization.AstarSerializer.SaveToFile("",graphData);
    }
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
