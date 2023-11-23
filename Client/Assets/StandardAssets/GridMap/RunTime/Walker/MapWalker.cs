using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace GridMap.RunTime.Walker
{
    
    [RequireComponent(typeof(Seeker),typeof(SimpleSmoothModifier))]
    public class MapWalker : MonoBehaviour
    {
        public MapWalker followWalker;
        private Seeker pathSeeker;
        private Path abPath;
       
        private bool isPathing;
        private bool isFollowing;
        private float followDis;
        private float followRate = 1.0f;
        
        private List<Vector3> _linePath;

        
        public Transform moveTransform;
        public Transform rotateTransform;
        
        public bool moveable = true;
        
        public float moveSpeed = 3.0f;
        public float rotateSpeed = 10.0f;
        private float moveNextDist = 0.5f;
        private PosCache _posCache;
        
        
        public int PathIndex { get; private set; }
        
        private void Awake()
        {
            pathSeeker = gameObject.GetComponent<Seeker>();
            _linePath = new List<Vector3>();
            SetPathMode(0);
        }


        public void OnEnable()
        {
            pathSeeker.pathCallback = OnAstarPathCallback;
        }

        
        public void OnDisable()
        {
            ClearPath();
            pathSeeker.pathCallback = null;
        }
        
        
         public virtual void OnDestroy()
        {
            moveTransform = null;
            rotateTransform = null;
            followWalker = null;
            if (!isFollowing) return;
            isFollowing = false;
            StopAllCoroutines();
        }
        
        private void Update()
        {
            if (!moveable) return;

            if (isPathing)
            {
                var pos = GetNextPos(Time.deltaTime);
                if (pos != Vector3.zero)
                {
                    Translate(pos);
                }
                else
                {	
                    isPathing = false;
                    ClearPath();
                }
            }
            UpdateTransparent();
        }

        
        /// <summary>
        /// 用于设置对象在地图上可以移动的区域
        /// </summary>
        /// <param name="traversableTags">搜索者可以遍历的标签</param>
        public void SetPathMode(int traversableTags)
        {
            pathSeeker.traversableTags = traversableTags;
        }
        

        private void UpdateTransparent()
        {
            // if (Map2D.CurrentMap != null && Map2D.CurrentMap.mapId == onMapID)
            // {
            //     if (Map2D.CurrentMap.IsTransparent(moveTransform.position.x, moveTransform.position.y) && (!isIgnoreTransparent))
            //     {
            //         SetTransparent(0.5f);
            //     }
            //     else
            //     {
            //         SetTransparent(1f);
            //     }
            // }
        }

        
        /// <summary>
        /// 获得下一个坐标
        /// </summary>
        /// <param name="deltaTime"></param>
        /// <returns></returns>
        private Vector3 GetNextPos(float deltaTime)
        {
            //获得路径 如果路径为空 或 长度为0 直接返回zero
            var path = GetPath();
            if (path == null || path.Count == 0)
            {
                return Vector3.zero;
            }
            
            //限制index 
            PathIndex = Mathf.Clamp(PathIndex, 0, path.Count);
            
            //移动速度
            var movement = moveSpeed * deltaTime;
            //如果当前为跟随 需要乘上跟随系数
            if (followWalker != null)
            {
                movement *= followRate;
            }

            var curPos = moveTransform.position;
            curPos.z = 0;
            var dir = Vector3.zero;
            var waypoint = path[PathIndex];

            float magnitude = 0;
            if (PathIndex == path.Count - 1)
            {
                dir = waypoint - curPos;
                magnitude = dir.magnitude;
                if (!(magnitude > 0.01)) return Vector3.zero;
                
                var newMagnitude = Mathf.Min(magnitude, movement);
                dir *= newMagnitude / magnitude;
                return dir;
            }

            while (PathIndex != path.Count - 1 && (curPos - waypoint).sqrMagnitude < moveNextDist * moveNextDist)
            {
                PathIndex++;
                waypoint = path[PathIndex];
            }

            dir = waypoint - curPos;
            magnitude = dir.magnitude;
            if (magnitude <= 0) return dir;
            
            var newMagnitude2 = Mathf.Min(magnitude, movement);
            dir *= newMagnitude2 / magnitude;
            return dir;
        }
        
        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="useStraight"></param>
        public void WalkTo(float x, float y, bool useStraight = true)
        {
            _posCache.NodePoint.x = (int)x;
            _posCache.NodePoint.y = (int)y;
            _posCache.Position.x = ( int) moveTransform.position.x;
            _posCache.Position.y = (int)moveTransform.position.y;
            WalkTo(new Vector3(x, y, 0), useStraight);
        }
        
        public void WalkTo2(float x, float y)
        {
            WalkTo2(new Vector3(x, y, 0));
        }

        public void WalkTo3(float x, float y)
        {
            WalkTo3(new Vector3(x, y, 0));
        }
        
        /// <summary>
        /// 停止移动
        /// </summary>
        public void StopWalk()
        {
            ClearPath();
        }
        

        private void ClearPath()
        {
            abPath?.Release(this);
            abPath = null;
            
            PathIndex = 0;
            isPathing = false;
            _linePath.Clear();
        }
        
        /// <summary>
        /// 获得路径
        /// </summary>
        /// <returns></returns>
        public List<Vector3> GetPath()
        {
            if (_linePath.Count > 0) return _linePath;
            
            var path = MapManager.GetCachePath(_posCache);
            return path is { Count: > 0 } ? path : abPath?.vectorPath;
        }

        public Vector3 GetWayPoint()
        {
            var path = GetPath();
            if (path is null || PathIndex < 0 || PathIndex >= path.Count)
            {
                return Vector3.zero;
            }
            return path[PathIndex];
        }
        
        private void Translate(Vector3 pos)
        {
            //位置
            moveTransform.Translate(pos, Space.World);
            //方向
            LookRotationByPosition(pos);
        }

        private void LookRotationByPosition(Vector3 pos)
        {
            pos = new Vector3(pos.x, 0, pos.y);
            var oldRotation = rotateTransform.localRotation;
            var newRotation = Quaternion.LookRotation(pos);
            var angle = Quaternion.Slerp(oldRotation, newRotation, rotateSpeed * Time.deltaTime).eulerAngles;
            angle.x = 0f;
            angle.z = 0f;
            rotateTransform.localEulerAngles = angle;
        }
        
      

     

      

        
        private void WalkTo3(Vector3 pos)
        {
            ClearPath();
            Vector3 startPos = moveTransform.position;
            startPos.z = 0;
            if (GetLinePath3(startPos, pos))
            {
                OnLinePathCallback();
            }
        }
        
        private void WalkTo2(Vector3 pos)
        {
            ClearPath();
            Vector3 startPos = moveTransform.position;
            startPos.z = 0;
            if (GetLinePath2(startPos, pos))
            {
                OnLinePathCallback();
            }
        }
        
        
        private void WalkTo(Vector3 pos, bool useLine)
        {
            // ClearPath();
            // Vector3 startPos = moveTransform.position;
            // startPos.z = 0; 
            //
            // if (useLine && GetLinePath(startPos, pos))
            // {
            //     OnLinePathCallback();
            //     return;
            // }
            // List<Vector3> path = Map2D.CurrentMap.GetCachePath(posRecord);
            // if (path != null)
            // {
            //     // Debug.Log ("use cache path "+path.Count);
            //     OnLinePathCallback();
            // }
            // else
            // {
            //     pathSeeker.StartPath(startPos, pos);
            // }
        }
        
        private bool GetLinePath3(Vector3 startPos, Vector3 endPos)
        {
            // linePath.Clear();
            // if (Map2D.CurrentMap != null && Map2D.CurrentMap.mapId == onMapID)
            // {
            //     linePath.Add(endPos);
            //     return true;
            // }
            return false;
        }

        private bool GetLinePath2(Vector3 startPos, Vector3 endPos)
        {
            // linePath.Clear();
            // if (Map2D.CurrentMap != null && Map2D.CurrentMap.mapId == onMapID && Map2D.CurrentMap.IsInMapArea(startPos.x, startPos.y) && Map2D.CurrentMap.IsInMapArea(endPos.x, endPos.y))
            // {
            //     linePath.Add(endPos);
            //     return true;
            // }
            return false;
        }

        private bool GetLinePath(Vector3 startPos, Vector3 endPos)
        {
            // linePath.Clear();
            // if (Map2D.CurrentMap != null && Map2D.CurrentMap.mapId == onMapID && Map2D.CurrentMap.IsLinePath(startPos, endPos))
            // {
            //     linePath.Add(endPos);
            //     return true;
            // }
            return false;
        }
        
        private void OnLinePathCallback()
        {
            // pathIndex = 0;
            // isPathing = true;
            // if (luaStartCallback != null)
            // {
            //     luaStartCallback.Call();
            // }
        }
        
        private void OnAstarPathCallback(Path p)
        {
            
        }
        
    }
}