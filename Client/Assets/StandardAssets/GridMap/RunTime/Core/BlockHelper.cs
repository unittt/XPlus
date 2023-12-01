using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace GridMap
{
    
    /// <summary>
    /// 格子助手
    /// </summary>
    internal class BlockHelper : MapHelper
    {
        
        /// <summary>
        /// 距离阈值
        /// </summary>
        private const float DIS_THRESHOLD = 0.01f;


        private MapData _mapData;
        private ObjectPool<GameObject> _blockEntityPool;
        private ObjectPool<ChunkData> _blockPool;
        private List<ChunkData> _blocks;
        
        //跟随的目标
        private Transform _target;
        private Vector3 _lasetPosition;
        
        
        internal override void OnInit()
        {
            _blockEntityPool = new ObjectPool<GameObject>(OnCreateBlockEntity, OnGetBlockEntity, OnReleaseBlockEntity);
            _blockPool = new ObjectPool<ChunkData>(OnCreateBlock);
            _blocks = new List<ChunkData>();
        }

        internal override void OnUpdate()
        {
            if (_target is null || Vector3.Distance(_target.position, _lasetPosition) <  DIS_THRESHOLD)
            {
                return;
            }

            _lasetPosition = _target.position;
            //获取相机的视角
            DynamicsRefreshBlockByRect(MapManager.CameraViewRect);
        }
        
        private GameObject OnCreateBlockEntity()
        {
            var result = GameObject.Instantiate(MapManager.BlockPrefab);
            result.transform.SetParent(MapManager.BlockContainer.transform);
            result.hideFlags = HideFlags.HideAndDontSave;
            return result;
        }
        
        private void OnGetBlockEntity(GameObject obj)
        {
            obj.SetActive(true);
        }
        
        private void OnReleaseBlockEntity(GameObject obj)
        {
            obj.SetActive(false);
        }
        
        
        private static ChunkData OnCreateBlock()
        {
            var block = new ChunkData();
            return block;
        }

        internal override void OnSetMapData(MapData mapData)
        {
            _mapData = mapData;
            ResetBlock(mapData.BlockWidth, mapData.BlockHeight,mapData.BlockSize);
        }
        
        internal override void OnBlockParamsChanged(int width, int height, float size)
        {
            ResetBlock(width, height,size);
        }
        
        private void ResetBlock(int width, int height, float size)
        {
            //释放数据
            OnRelease();
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var block = InitBlock(x, y, size);
                    _blocks.Add(block);
                }
            }

            if (MapManager.IsDynamics) return;
            foreach (var chunkData in _blocks)
            {
                InstantiateBlockEntity(chunkData);
            }
        }
        
        /// <summary>
        /// 初始化Block
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        private ChunkData InitBlock(int x, int y, float scale)
        {
            var chunkData = _blockPool.Get();
            chunkData.X = x;
            chunkData.Y = y;
            
            //矩形最小角的位置
            var minPosition = new Vector2(x * scale, y * scale);
            chunkData.Bounds = new Rect(minPosition, new Vector2(scale, scale));
            return chunkData;
        }

        /// <summary>
        /// 动态刷新Block
        /// </summary>
        /// <param name="rect"></param>
        private void DynamicsRefreshBlockByRect(Rect rect)
        {
            if (!MapManager.IsDynamics)return;
            
            foreach (var block in _blocks)
            {
                var isOverlaps = MapGlobal.IsOverlaps(rect, block.Bounds);
                var isLoaded = block.IsLoaded;
                switch (isOverlaps)
                {
                    //如果有交集 并且未加载
                    case true when !isLoaded:
                        //实例化实体
                        InstantiateBlockEntity(block);
                        break;
                    //如果没有交集 并且存在实体
                    case false when isLoaded:
                        //释放实体
                        ReleaseBlockEntity(block);
                        break;
                }
            }
        }

        /// <summary>
        /// 实例化格子实体
        /// </summary>
        /// <param name="chunkData"></param>
        private void InstantiateBlockEntity(ChunkData chunkData)
        {
            if (chunkData.IsLoaded)
            {
                return;
            }
            
            var entity = _blockEntityPool.Get();
            chunkData.Entity = entity;
            
            //设置transform
            entity.transform.SetLocalPositionAndRotation(chunkData.Bounds.center, Quaternion.identity);
            entity.transform.localScale = Vector3.one * _mapData.BlockSize;
            
            //加载贴图
            var texture = MapManager.LoadBlockTexture(chunkData.X, chunkData.Y);
       
            var mr = entity.GetComponent<MeshRenderer>();
            var propertyBlock = new MaterialPropertyBlock();
            mr.GetPropertyBlock(propertyBlock);

            if (texture is not null)
            {
                propertyBlock.SetTexture("_MainTex", texture);
            }
            else
            {
                propertyBlock.Clear();
            }
            
            mr.SetPropertyBlock(propertyBlock);
        }

        internal override void SetFollow(Transform target)
        {
            _target = target;
        }


        /// <summary>
        /// 释放
        /// </summary>
        internal override void OnRelease()
        {
            var maxIndex = _blocks.Count - 1;
            for (var i = maxIndex; i >=0 ; i--)
            {
                ReleaseBlock(_blocks[i]);
            }
            _blocks.Clear();
        }
        
        /// <summary>
        /// 释放Block
        /// </summary>
        /// <param name="chunkData"></param>
        private void ReleaseBlock(ChunkData chunkData)
        {
            //从列表中移除
            _blocks.Remove(chunkData);
            ReleaseBlockEntity(chunkData);
            //调用释放方法
            chunkData.Release();
            //释放Block
            _blockPool.Release(chunkData);
        }

        /// <summary>
        /// 释放实体
        /// </summary>
        /// <param name="chunkData"></param>
        private void ReleaseBlockEntity(ChunkData chunkData)
        {
            //释放实体
            if (!chunkData.IsLoaded) return;
            _blockEntityPool.Release(chunkData.Entity);
            chunkData.Entity = null;
        }
    }
}
