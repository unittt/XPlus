using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cinemachine;
using Pathfinding;
using UnityEditor;
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
        /// 预制件
        /// </summary>
        [SerializeField]
        internal GameObject BlockPrefab;
        /// <summary>
        /// 块实体容器
        /// </summary>
        [SerializeField]
        internal GameObject BlockContainer;
        /// <summary>
        /// 相机
        /// </summary>
        [SerializeField]
        internal CinemachineVirtualCamera Camera;
        /// <summary>
        /// 相机限制范围
        /// </summary>
        [SerializeField]
        internal PolygonCollider2D Collider2D;

        
        //网格图
        private GridGraph _graph;
        
        //地图数据
        private MapData _mapData;
        
        private GraphHelper GraphHelper { get; set; }
        private BlockHelper BlockHelper { get; set; }
        private CameraHelper CameraHelper { get; set; }

        private readonly List<MapHelper> _helpers = new();
        
        /// <summary>
        /// 是否动态加载地块
        /// </summary>
        internal bool IsDynamics { get; private set; }

        /// <summary>
        /// 序列话数据
        /// </summary>
        internal GridNodeBase[] Nodes => GraphHelper.Nodes;
        
        /// <summary>
        /// 运行时获取格子贴图
        /// </summary>
        public Func<MapData, int, int, Texture> BlockTextureFunc;

        
        void Awake()
        {
            GraphHelper = RegisterHelper<GraphHelper>();
            BlockHelper = RegisterHelper<BlockHelper>();
            CameraHelper = RegisterHelper<CameraHelper>();
            foreach (var helper in _helpers)
            {
                helper.OnInit();
            }
        }

        private T RegisterHelper<T>() where T : MapHelper, new()
        {
            var helper = new T
            {
                MapManager = this
            };
            _helpers.Add(helper);
            return helper;
        }
        
        /// <summary>
        /// 设置GridMapData
        /// </summary>
        /// <param name="mapData"></param>
        public void SetMapData(MapData mapData, bool isDynamics = true)
        {
            IsDynamics = isDynamics;
            _mapData = mapData;
            foreach (var helper in _helpers)
            {
                helper.OnSetMapData(mapData);
            }
        }
        
        /// <summary>
        /// 网格图参数发生改变
        /// </summary>
        /// <param name="mapData"></param>
        internal void GridGraphParamsChanged(MapData mapData)
        {
            foreach (var helper in _helpers)
            {
                helper.OnGridGraphParamsChanged(mapData.NodeWidth,mapData.NodeHeight,mapData.NodeSize);
            }
        }

        /// <summary>
        /// 块参数发生改变
        /// </summary>
        /// <param name="newSize"></param>
        internal void BlockParamsChanged(MapData mapData)
        {
            foreach (var helper in _helpers)
            {
                helper.OnBlockParamsChanged(mapData.BlockWidth,mapData.BlockHeight, mapData.BlockSize);
            }
        }
        
        internal void SetNodeWalkableAndTag(Vector2 position, uint tag)
        {
            GraphHelper.SetNodeTag(position,tag);
        }
        
        /// <summary>
        /// 获取格子贴图
        /// </summary>
        /// <param name="textureFolder"></param>
        /// <param name="id"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        internal Texture LoadBlockTexture(int x, int y)
        {
            if (BlockTextureFunc != null)
            {
                return BlockTextureFunc.Invoke(_mapData, x, y);
            }

#if UNITY_EDITOR
            var textureName = GridMapConfig.Instance.TextureNameRule;
            textureName = textureName.Replace("[ID]", _mapData.ID.ToString());
            textureName = textureName.Replace("[X]", x.ToString());
            textureName = textureName.Replace("[Y]", y.ToString());
            var path = $"{_mapData.BlockTextureFolder}/{textureName}.png";
            return AssetDatabase.LoadAssetAtPath<Texture>(path);
#else
            return null;
#endif
        }
        
        /// <summary>
        /// 释放
        /// </summary>
        public void Release()
        {
            foreach (var helper in _helpers)
            {
                helper.OnRelease();
            }
        }
    }
}