using Pathfinding;
using UnityEngine;

namespace GridMap
{
    internal class GraphHelper : MapHelper
    {
        
        private AstarPath _astarPath;
        private GridGraph _gridGraph;
        
        /// <summary>
        /// 节点尺寸
        /// </summary>
        public float NodeSize => _gridGraph.nodeSize;
        /// <summary>
        /// 格子宽个数
        /// </summary>
        public int Width => _gridGraph.width;
        /// <summary>
        /// 格子高个数
        /// </summary>
        public int Depth => _gridGraph.depth;
        /// <summary>
        /// graph中心点
        /// </summary>
        public Vector2 GraphCenter => _gridGraph.center;
        
        
        /// <summary>
        /// 序列化数据
        /// </summary>
        internal GridNodeBase[] Nodes
        {
            get
            {
                // var settings = new Pathfinding.Serialization.SerializeSettings
                // {
                //     nodes = true,
                //     editorSettings = true
                // };
                //
                // return _astarPath.data.SerializeGraphs(settings);
                return _gridGraph.nodes;
            }
        }
        
 
        
        internal override void OnInit()
        {
            base.OnInit();
#if UNITY_EDITOR
            //在播放模式之外用于初始化AstarPath对象，即使尚未在检查器中选择它。
            AstarPath.FindAstarPath();
#endif
            _astarPath = AstarPath.active;
        }
        
        internal override void OnSetMapData(MapData mapData)
        {
            base.OnSetMapData(mapData);
            // if (Application.isEditor)
            // {
            //     AstarPath.graphs[0].Scan();
            // }
            
            
         
            
            // if (mapData.GraphData != null)
            // {
            //     //反序列化graphs
            //     _astarPath.data.DeserializeGraphs(mapData.GraphData);
            //     _gridGraph = _astarPath.graphs[0] as GridGraph;
            // }
            // else
            // {
            //     // _gridGraph.nodes[0].Walkable
            //     _gridGraph= _astarPath.graphs[0] as GridGraph;
            //     SetDimensions(_gridGraph, mapData.NodeSize, mapData.NodeWidth, mapData.NodeHeight);
            // }
            
            
            _gridGraph= _astarPath.graphs[0] as GridGraph;
            SetDimensions(_gridGraph, mapData.NodeSize, mapData.NodeWidth, mapData.NodeHeight);

            if (mapData.Nodes is { Length: > 0 } )
            {
                var length = mapData.Nodes.Length;
                for (var i = 0; i < length; i++)
                {
                    SetNodeTag(i, mapData.Nodes[i]);
                }
            }
        }
        
        /// <summary>
        /// 当网格图参数发生变更
        /// </summary>
        /// <param name="width"></param>
        /// <param name="depth"></param>
        /// <param name="nodeSize"></param>
        internal override void OnGridGraphParamsChanged(int width, int depth, float nodeSize)
        {
            SetDimensions(_gridGraph, nodeSize, width, depth);
        }

        private void SetDimensions(GridGraph graph, float nodeSize, int width, int depth)
        {
            graph.center = new Vector3(width * nodeSize * 0.5f, depth * nodeSize * 0.5f, 0);
            graph.SetDimensions(width, depth, nodeSize);
            //重新计算图形
            graph.Scan();
            
            //重置GraphNode 为不可行走
            foreach (var graphNode in graph.nodes)
            {
                graphNode.Walkable = false;
            }
        }
        
        internal void SetNodeTag(Vector2 position, uint tag)
        {
            var node =_astarPath.GetNearest(position).node;
            if (node == null)return;
            SetNodeTag(node,tag);
        }

        private void SetNodeTag(int index, uint tag)
        {
           var node = _gridGraph.nodes[index];
           SetNodeTag(node, tag);
        }

        private void SetNodeTag(GraphNode node, uint tag)
        {
            node.Walkable = tag > 0;
            node.Tag = tag;
            node.SetConnectivityDirty();
        }
    }
}
