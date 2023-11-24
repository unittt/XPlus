using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace GridMap.RunTime.Walker
{
    [RequireComponent(typeof(Seeker),typeof(SimpleSmoothModifier))]
    public class MapWalker : MonoBehaviour
    {
        public MapWalker followWalker;
        
        //a*路径搜索
        private Seeker _pathSeeker;
        private bool isPathing;
        private bool isFollowing;
        private float followDis;
        private float followRate = 1.0f;

        public List<Vector3> Path { get; protected set; }


        public Transform moveTransform;
        public Transform rotateTransform;
        
        public bool moveable = true;
        
        /// <summary>
        /// 移动速度
        /// </summary>
        public float moveSpeed = 3.0f;
        /// <summary>
        /// 旋转的速度
        /// </summary>
        public float rotateSpeed = 10.0f;
        private float moveNextDist = 0.5f;
   
        
        public int PathIndex { get; private set; }
        
        private void Awake()
        {
            _pathSeeker = gameObject.GetComponent<Seeker>();
            Path = new List<Vector3>();
            SetPathMode(0);
        }


        public void OnEnable()
        {
            _pathSeeker.pathCallback += OnAStarPathCallback;
        }
        
        public void OnDisable()
        {
            ClearPath();
            _pathSeeker.pathCallback -= OnAStarPathCallback;
        }
        
        
         public virtual void OnDestroy()
         {
             _pathSeeker.pathCallback = null;
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
            // pathSeeker.traversableTags = traversableTags;
            _pathSeeker.traversableTags = 1 << LayerGround | 1 << LayerSky;
        }
        public static readonly int LayerGround = 1;
        public static readonly int LayerSky = 2;

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
            if (Path is null || Path.Count == 0)
            {
                return Vector3.zero;
            }

            var count = Path.Count;
            //限制index 
            PathIndex = Mathf.Clamp(PathIndex, 0, count);
            
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
            var waypoint = GetWayPoint(PathIndex);

            float magnitude = 0;
            if (PathIndex == count - 1)
            {
                dir = waypoint - curPos;
                magnitude = dir.magnitude;
                if (!(magnitude > 0.01)) return Vector3.zero;
                
                var newMagnitude = Mathf.Min(magnitude, movement);
                dir *= newMagnitude / magnitude;
                return dir;
            }

            while (PathIndex != Path.Count - 1 && (curPos - waypoint).sqrMagnitude < moveNextDist * moveNextDist)
            {
                PathIndex++;
                waypoint = GetWayPoint(PathIndex);
            }

            dir = waypoint - curPos;
            magnitude = dir.magnitude;
            if (magnitude <= 0) return dir;
            
            var newMagnitude2 = Mathf.Min(magnitude, movement);
            dir *= newMagnitude2 / magnitude;
            return dir;
        }
        
        
        /// <summary>
        /// 停止移动
        /// </summary>
        public virtual void StopWalk()
        {
            ClearPath();
        }
        
        private void ClearPath()
        {
            PathIndex = 0;
            isPathing = false;
            Path.Clear();
        }
        

        public Vector3 GetWayPoint(int index)
        {
            if (Path is null || index >= Path.Count)
            {
                return Vector3.zero;
            }
            return Path[index];
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


        #region 移动目标
        public void WalkTo(Vector3 endPos, bool useLine = true)
        {
            //清理路径
            ClearPath();
            
            var startPos = moveTransform.position;
            startPos.z = 0;
            endPos.z = 0;
     
            if (useLine && MapManager.Instance.IsLinePath(startPos, endPos))
            {
                OnLinePathCallback(endPos);
                return;
            }
            _pathSeeker.StartPath(startPos, endPos);
        }
        
        private void OnLinePathCallback(Vector3 pos)
        {
            Path.Add(pos);
            OnFindPath();
        }
        
        private void OnAStarPathCallback(Path path)
        {
            if (path.error) return;
            
            //防止发生错误
            Path.Clear();
            Path.AddRange(path.vectorPath);
            OnFindPath();
        }

        /// <summary>
        /// 当找到了路径
        /// </summary>
        protected virtual void OnFindPath()
        {
            PathIndex = 0;
            isPathing = true;
        }
        #endregion
    }
}