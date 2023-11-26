using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

namespace GridMap.RunTime.Walker
{
    [RequireComponent(typeof(Seeker),typeof(SimpleSmoothModifier))]
    public class MapWalker : MonoBehaviour
    {
        
        //a*路径搜索
        private Seeker _pathSeeker;
        
        #region 移动
        /// <summary>
        /// 移动的Transform
        /// </summary>
        public Transform MoveTransform;
        /// <summary>
        /// 旋转的Transform
        /// </summary>
        public Transform RotateTransform;
        /// 移动速度
        /// </summary>
        public float MoveSpeed = 3.0f;
        /// <summary>
        /// 旋转的速度
        /// </summary>
       public float RotateSpeed = 10.0f;
        /// <summary>
        /// 移动到到下一个的距离
        /// </summary>
        public float MoveNextDist = 0.5f;
        
        private bool _isPathing;
        /// <summary>
        /// 当前是否在移动
        /// </summary>
        public bool IsPathing
        {
            get => _isPathing;
            set
            {

                if (_isPathing == value) return;
                _isPathing = value;
                if (_isPathing)
                {
                    OnStartMove?.Invoke();
                }
                else
                {
                    OnEndMove?.Invoke();
                }
            }
        }

        #endregion

        #region 跟随
        /// <summary>
        /// 跟随的目标
        /// </summary>
        public MapWalker FollowWalker;
        /// <summary>
        /// 跟随的距离
        /// </summary>
        public float FollowDis;
        /// <summary>
        /// 跟随的速率
        /// </summary>
        public float FollowRate = 1.0f;
        /// <summary>
        /// 是否在跟随
        /// </summary>
        public bool IsFollowing { get; private set; }
        #endregion
        

        /// <summary>
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsMoveable = true;
        
        /// <summary>
        /// 路径的索引
        /// </summary>
        public int PathIndex { get; private set; }
        /// <summary>
        /// 路径
        /// </summary>
        public List<Vector3> Path { get; protected set; }


        #region 事件
        /// <summary>
        /// 当开始移动
        /// </summary>
        public event Action OnStartMove;
        /// <summary>
        /// 当停止移动
        /// </summary>
        public event Action OnEndMove;
        /// <summary>
        /// update
        /// </summary>
        public event Action<Vector3,NodeTag> OnUpdateMove;

        #endregion
        
        protected virtual void Awake()
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
            MoveTransform = null;
            RotateTransform = null;
            FollowWalker = null;
            IsFollowing = false;

            OnStartMove = null;
            OnEndMove = null;
            OnUpdateMove = null;
        }

        private void Update()
        {
            if (!IsMoveable) return;

            if (IsPathing)
            {
                var pos = GetNextPos(Time.deltaTime);
                if (pos != Vector3.zero)
                {
                    Translate(pos);
                }
                else
                {	
                    IsPathing = false;
                    ClearPath();
                }
            }
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
            var movement = MoveSpeed * deltaTime;
            //如果当前为跟随 需要乘上跟随系数
            if (FollowWalker != null)
            {
                movement *= FollowRate;
            }

            var curPos = MoveTransform.position;
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

            while (PathIndex != Path.Count - 1 && (curPos - waypoint).sqrMagnitude < MoveNextDist * MoveNextDist)
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
            Path.Clear();
            IsPathing = false;
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
            MoveTransform.Translate(pos, Space.World);
            //方向
            LookRotationByPosition(pos);
            //当前的世界坐标，节点的状态
            var position = MoveTransform.position;
            MapManager.Instance.TryGetNode(position, out var nodeTag);
            //更新
            OnUpdateMove?.Invoke(position,nodeTag);
        }

        private void LookRotationByPosition(Vector3 pos)
        {
            pos = new Vector3(pos.x, 0, pos.y);
            var oldRotation = RotateTransform.localRotation;
            var newRotation = Quaternion.LookRotation(pos);
            var angle = Quaternion.Slerp(oldRotation, newRotation, RotateSpeed * Time.deltaTime).eulerAngles;
            angle.x = 0f;
            angle.z = 0f;
            RotateTransform.localEulerAngles = angle;
        }
        
        #region 移动目标
        public void WalkTo(Vector3 endPos, bool useLine = true)
        {
            var startPos = MoveTransform.position;
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
            Path.Clear();
            Path.Add(pos);
            OnFindPath();
        }
        
        private void OnAStarPathCallback(Path path)
        {
            if (path.error) return;
            Path.Clear();
            Path.AddRange(path.vectorPath);
            OnFindPath();
        }

        /// <summary>
        /// 当找到了路径
        /// </summary>
        private void OnFindPath()
        {
            PathIndex = 0;
            IsPathing = true;
        }
        #endregion
    }
}