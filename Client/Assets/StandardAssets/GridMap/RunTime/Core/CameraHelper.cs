using Cinemachine;
using UnityEngine;

namespace GridMap
{
    internal class CameraHelper : MapHelper
    {

        private Camera _mainCamera;
        private CinemachineConfiner2D _confiner2D;
        
        internal override void OnInit()
        {
            _mainCamera =  MapManager.Camera.VirtualCameraGameObject.GetComponentInChildren<Camera>();
            _confiner2D = MapManager.Camera.GetComponent<CinemachineConfiner2D>();
        }

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
            _confiner2D.InvalidateCache();
        }

        public void Release()
        {
            
        }

        /// <summary>
        /// 设置跟随目标
        /// </summary>
        /// <param name="transform"></param>
        public void SetTarget(Transform transform)
        {
            MapManager.Camera.Follow = transform;
        }
        
        
        /// <summary>
        /// 获得相机的可视区域
        /// </summary>
        /// <returns></returns>
        public Rect GetCameraViewRect()
        {
            // 对于正交摄像机，视野的高度是正交大小的两倍
            var height = 2f * _mainCamera.orthographicSize;
            // 视野的宽度是高度乘以宽高比
            var width = height * _mainCamera.aspect;
            // 获取摄像机的世界位置
            Vector2 cameraPosition = _mainCamera.transform.position;
        
            // 创建表示视野的矩形
            var cameraViewRect = new Rect(
                cameraPosition.x - width / 2,
                cameraPosition.y - height / 2,
                width,
                height
            );
            return cameraViewRect;
        }
    }
}