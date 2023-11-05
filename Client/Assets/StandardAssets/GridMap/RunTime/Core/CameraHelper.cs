using UnityEngine;

namespace GridMap
{
    internal class CameraHelper : MapHelper
    {
        internal override void OnSetMapData(MapData mapData)
        {
            SetBounds(mapData.NodeWidth, mapData.NodeHeight, mapData.NodeSize);
        }

        internal override void OnGridGraphParamsChanged(int width, int height, float size)
        {
            SetBounds(width, height, size);
        }

        /// <summary>
        /// 设置相机的区间
        /// </summary>
        /// <param name="rect"></param>
        private void SetBounds(int width, int height, float size)
        {
            var maxX = width * size;
            var maxY = height * size;

            var path = new[]
            {
                Vector2.zero,
                new(0, maxY),
                new(maxX, maxY),
                new(maxX, 0)
            };
            var collider2D = MapManager.Collider2D;
            collider2D.SetPath(0,path);
        }

        public void Release()
        {
            
        }
    }
}