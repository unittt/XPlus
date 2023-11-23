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
        private ObjectPool<GameObject> _blockEntityPool;
        private ObjectPool<ChunkData> _blockPool;
        private List<ChunkData> _blocks;
        private MapData _mapData;
        
        internal override void OnInit()
        {
            _blockEntityPool = new ObjectPool<GameObject>(OnCreateBlockEntity, OnGetBlockEntity, OnReleaseBlockEntity);
            _blockPool = new ObjectPool<ChunkData>(OnCreateBlock);
            _blocks = new List<ChunkData>();
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
            var center = new Vector3((x + 0.5f) * scale, (y + 0.5f) * scale, 0);
            chunkData.X = x;
            chunkData.Y = y;
            chunkData.Bounds = new Rect(center, new Vector2(scale, scale));
            return chunkData;
        }

        /// <summary>
        /// 按矩形实例化块
        /// </summary>
        /// <param name="rect"></param>
        public void InstantiateBlockByRect(Rect rect)
        {
            if (!MapManager.IsDynamics)
            {
                return;
            }
            
            foreach (var block in _blocks)
            {
                if (!block.IsLoaded && MapGlobal.IsOverlaps(rect, block.Bounds))
                {
                    InstantiateBlockEntity(block);
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
            
            chunkData.IsLoaded = true;
            var entity = _blockEntityPool.Get();
            chunkData.Entity = entity;
            
            //设置transform
            entity.transform.SetLocalPositionAndRotation(chunkData.Bounds.position, Quaternion.identity);
            entity.transform.localScale = Vector3.one * chunkData.Bounds.size.x;
            
            //加载贴图
            var texture = MapManager.LoadBlockTexture(chunkData.X, chunkData.Y);
       
            var mr = entity.GetComponent<MeshRenderer>();
            var propertyBlock = new MaterialPropertyBlock();
            mr.GetPropertyBlock(propertyBlock);

            if (texture != null)
            {
                propertyBlock.SetTexture("_MainTex", texture);
            }
            else
            {
                propertyBlock.Clear();
            }
            
            mr.SetPropertyBlock(propertyBlock);
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
            //释放实体
            _blockEntityPool.Release(chunkData.Entity);
            //调用释放方法
            chunkData.Release();
            //释放Block
            _blockPool.Release(chunkData);
        }
    }
}
