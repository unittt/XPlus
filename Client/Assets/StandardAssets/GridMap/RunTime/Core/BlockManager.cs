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
        private GameObject BlockPrefab;
        private Transform BlockEntityParent;
        
        private ObjectPool<GameObject> _blockEntityPool;
        private ObjectPool<ChunkData> _blockPool;
        private List<ChunkData> _blocks;
        
        internal override void OnInit()
        {
            _blockEntityPool = new ObjectPool<GameObject>(OnCreateBlockEntity);
            _blockPool = new ObjectPool<ChunkData>(OnCreateBlock);
            _blocks = new List<ChunkData>();
        }
        
        private GameObject OnCreateBlockEntity()
        {
            var result = GameObject.Instantiate(BlockPrefab);
            result.transform.SetParent(BlockEntityParent);
            return result;
        }

        private static ChunkData OnCreateBlock()
        {
            var block = new ChunkData();
            return block;
        }

        internal override void OnSetMapData(MapData mapData)
        {
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

            if (Map.IsDynamics) return;
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
            var block = _blockPool.Get();
            var center = new Vector3((x + 0.5f) * scale, (y + 0.5f) * scale, 0);
            block.Bounds = new Rect(center, new Vector2(scale, scale));
            return block;
        }

        /// <summary>
        /// 当相机坐标发生改变时
        /// </summary>
        /// <param name="cameraRect"></param>
        private void OnCameraMove(Rect cameraRect)
        {
            if (!Map.IsDynamics)
            {
                return;
            }
            
            foreach (var block in _blocks)
            {
                if (!block.IsLoaded && MapGlobal.IsOverlaps(cameraRect, block.Bounds))
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
            Texture texture = null;
            if (texture == null) return;
            var mr = entity.GetComponent<MeshRenderer>();
            var propertyBlock = new MaterialPropertyBlock();
            mr.GetPropertyBlock(propertyBlock);
            propertyBlock.SetTexture("_MainTex", texture);
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
