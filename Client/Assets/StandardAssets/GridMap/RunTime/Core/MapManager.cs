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

        
        /// <summary>
        /// 相机的视角
        /// </summary>
        public Rect CameraViewRect =>CameraHelper.GetCameraViewRect();

        public static MapManager Current { get; private set; }

        void Awake()
        {
            Current = this;
            GraphHelper = RegisterHelper<GraphHelper>();
            BlockHelper = RegisterHelper<BlockHelper>();
            CameraHelper = RegisterHelper<CameraHelper>();
            foreach (var helper in _helpers)
            {
                helper.OnInit();
            }
        }

        private void Update()
        {
            if (_mapData is null) return;
            
            foreach (var helper in _helpers)
            {
                helper.OnUpdate();
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

        public void ShowByPosition(Vector2 position)
        {
            CameraHelper.SetPosition(position);
            BlockHelper.DynamicsRefreshBlockByRect(CameraHelper.GetCameraViewRect());
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
        
        /// <summary>
        /// 设置跟随的目标
        /// </summary>
        public void SetFollow(Transform transform)
        {
            foreach (var helper in _helpers)
            {
                helper.SetFollow(transform);
            }
        }
        
        /// <summary>
        /// 是否存在直线路径
        /// </summary>
        /// <param name="startPos">起始点</param>
        /// <param name="endPos">结束点</param>
        /// <returns></returns>
        public bool IsLinePath(Vector2 startPos, Vector2 endPos)
        {
            var size = _mapData.NodeSize;
            var startGridPos = startPos.WorldToGridPos(size);
            var endGridPos = endPos.WorldToGridPos(size);
            var dis = (int)Vector2.Distance(startGridPos, endGridPos);
            dis = Mathf.Max(dis, 1);

            for (var i = 1; i <= dis; i++)
            {
                var pos = Vector2.Lerp(startGridPos, endGridPos, 1.0f * i / dis);
                var point = new Vector2Int((int)pos.x, (int)pos.y);
                if (_mapData.NodeIsTag(point, NodeTag.Obstacle))
                {
                    return false;
                }
            }
            return true;
        }


        #region mapdata
        public bool TryGetNode(Vector2 worldPos, out NodeTag nodeTag)
        {
            return _mapData.TryGetNode(worldPos, out nodeTag);
        }
        #endregion
    }
}