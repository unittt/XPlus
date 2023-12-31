using System;
using Pathfinding;
using UnityEngine;

namespace GridMap
{

    /// <summary>
    /// 地图数据
    /// </summary>
    [Serializable]
    public sealed class MapData
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int ID;

        /// <summary>
        /// 块宽高度
        /// </summary>
        public int BlockHeight = 1;
        /// <summary>
        /// 块宽数量
        /// </summary>
        public int BlockWidth = 1;
        /// <summary>
        ///贴图格子的大小
        /// </summary>
        public float BlockSize = 1;
        /// <summary>
        /// 贴图资源文件夹
        /// </summary>
        public string BlockTextureFolder;

        
        /// <summary>
        /// 节点高数量
        /// </summary>
        public int NodeHeight = 1;
        /// <summary>
        /// 节点宽
        /// </summary>
        public int NodeWidth = 1;
        /// <summary>
        /// 节点大小
        /// </summary>
        public float NodeSize = 1;
        
        /// <summary>
        /// 文件地址
        /// </summary>
        public string AssetPath;

        /// <summary>
        /// 节点信息
        /// </summary>
        public uint[] Nodes;
        
        public Vector2 GraphCenter()
        {
            return new Vector2(NodeWidth * NodeSize * 0.5f, NodeHeight * NodeSize * 0.5f);
        }
        
        public bool NodeIsTag(Vector2 worldPos, NodeTag tag)
        {
            var nodePoint = new Vector2Int((int)(worldPos.x / NodeSize), (int)(worldPos.y / NodeSize));
            return NodeIsTag(nodePoint, tag);
        }

        public bool NodeIsTag(Vector2Int point, NodeTag tag)
        {
            var index = point.y * NodeWidth + point.x;
            return NodeIsTag(index, tag);
        }

        public bool NodeIsTag(int index, NodeTag tag)
        {
            if (TryGetNode(index, out var nodeTag))
            {
                return nodeTag == tag;
            }

            return false;
        }


        public bool TryGetNode(Vector2 worldPos, out NodeTag nodeTag)
        {
            var nodePoint = new Vector2Int((int)(worldPos.x / NodeSize), (int)(worldPos.y / NodeSize));
            var index = nodePoint.y * NodeWidth + nodePoint.x;
            return TryGetNode(index, out nodeTag);
        }
        
        public bool TryGetNode(int index, out NodeTag nodeTag)
        {
            nodeTag = 0;
            if (Nodes is null || index < 0 || index >= Nodes.Length)
            {
                return false;
            }
            nodeTag = (NodeTag)Nodes[index];
            return true;
        }
        
        public void WriteNodes(GridNodeBase[] gridNodeBases)
        {
            var length = gridNodeBases.Length;
            Nodes = new uint[length];
            for (var i = 0; i < length; i++)
            {
                Nodes[i] = gridNodeBases[i].Tag;
            }
        }

        public static MapData Deserialize(string jsonText)
        {
            return JsonUtility.FromJson<MapData>(jsonText);
        } 
    }
}