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
    /// 栅格图旋转
    /// </summary>
    [Label("栅格图形旋转")] public Vector3 GridGraphRotation = new Vector3(-90, 0, 0);

    /// <summary>
    /// 场景指定的根目录
    /// </summary>
    [Label("场景指定的根目录")] public GameObject SceneRoot;

    /// <summary>
    /// 玩家
    /// </summary>
    [Label("玩家")] public GameObject Player;

    /// <summary>
    /// 场景相机
    /// </summary>
    [Label("场景相机")] public GameObject SceneCam;

    /// <summary>
    /// 
    /// </summary>
    public GameObject _fgEffectLayer;

    /// <summary>
    /// 背景特效层
    /// </summary>
    [Label("背景特效层")] public GameObject BgEffectLayer;

    /// <summary>
    /// 
    /// </summary>
    public GameObject _fgBuildLayer;

    /// <summary>
    /// 背景构建层
    /// </summary>
    [Label("背景构建层")] public GameObject BgBuildLayer;

    /// <summary>
    /// 
    /// </summary>
    public GameObject _transferLayer;

    public GameObject _tfEffectLayer;

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
        xxxxxxxx();
        SetupSceneObjReference();
       
    }


    private void LoadGridMapInfo()
    {
        //加载图片
        var mapID = EditorPrefs.GetInt(MapGlobal.EDITOR_MAP_ID_KEY);
        GridMapConfig.Instance.TryGetGridMapInfo(mapID, out _gridMapInfo);
    }

    private void xxxxxxxx()
    {
        _gridMapInfo.ID = 1010;
        _gridMapInfo.Rows = 12;
        _gridMapInfo.Columns = 23;
        _gridMapInfo.TileFolder = "Assets/GameRes/Map2d/1010/tilemap_1010";
        for (var y = 0; y <= _gridMapInfo.Rows; y++)
        {
            for (var x = 0; x <= _gridMapInfo.Columns; x++)
            {
                var path = $"{_gridMapInfo.TileFolder}/tile_{_gridMapInfo.ID}_{x}_{y}.png";
                var texture = AssetDatabase.LoadAssetAtPath<Texture>(path);
                var grid = Instantiate(GridPrefab, new Vector3(x ,y,0) * 2.5f, Quaternion.identity, SceneRoot.transform);
                var mr = grid.GetComponent<MeshRenderer>();
                var propertyBlock = new MaterialPropertyBlock();
                mr.GetPropertyBlock(propertyBlock);
                propertyBlock.SetTexture("_MainTex",texture);
                mr.SetPropertyBlock(propertyBlock);
            }
        }
    }

    public void SetupSceneObjReference()
    {
        Selection.activeGameObject = AstarPath.gameObject;
    }

    /// <summary>
    /// 清理场景上冗余对象,GridEffectEditorScene意外保存时会残留一下冗余对象
    /// </summary>
    public void CleanUp()
    {

    }

    /// <summary>
    /// 设置A*参数
    /// </summary>
    /// <param name="gridRef"></param>
    public void SetupAstarPath(Texture2D gridRef)
    {
        if (AstarPath.graphs.Length == 0)
        {
            //创建网格图
            AstarPath.data.AddGraph(typeof(GridGraph));
        }

        var gridGraph = AstarPath.graphs[0] as GridGraph;

        //配置GridGraph参数 channels[index] = 目前不确定 
        gridGraph.nodeSize = GridGraphNodeSize;
        gridGraph.Width = gridRef.width;
        gridGraph.Depth = gridRef.height;
        gridGraph.UpdateSizeFromWidthDepth();
        gridGraph.center = Vector3.zero -
                           (GridGraphEditor.RoundVector3(gridGraph.transform.Transform(new Vector3(0, 0, 0))) -
                            gridGraph.center);
        gridGraph.rotation = GridGraphRotation;
        gridGraph.textureData.enabled = true;
        gridGraph.textureData.source = gridRef;
        gridGraph.textureData.channels[0] = GridGraph.TextureData.ChannelUse.None;
        gridGraph.textureData.factors[0] = 0;
        gridGraph.textureData.channels[1] = GridGraph.TextureData.ChannelUse.None;
        gridGraph.textureData.factors[1] = 1;
        gridGraph.textureData.channels[2] = GridGraph.TextureData.ChannelUse.WalkablePenalty;
        // gridGraph.textureData.factors[2] =  BrushTool.threshold * 3;;
    }
}
