using UnityEngine;

namespace GridMap
{
    internal class CameraHelper : MapHelper
    {
        private PolygonCollider2D _collider2D;
        
        internal override void OnInit()
        {
            _collider2D.points = new Vector2[4];
        }
        
        /// <summary>
        /// 设置相机的区间
        /// </summary>
        /// <param name="rect"></param>
        internal void SetBounds(Rect rect)
        {
            _collider2D.points[0] = Vector2.zero;
            _collider2D.points[0] = new Vector2(0,rect.yMax);
            _collider2D.points[0] = rect.max;
            _collider2D.points[0] = new Vector2(rect.xMax, 0);
        }

        public void Release()
        {
            
        }
    }
}