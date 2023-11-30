using Cysharp.Threading.Tasks;
using GridMap;
using GridMap.RunTime.Walker;
using HT.Framework;
using UnityEngine;

namespace GameScripts.RunTime.Model
{
    
    /// <summary>
    /// 地图移动者
    /// </summary>
    [EntityResource("MapWalker",true)]
    public sealed class MapWalker : ActorEntity
    {
        //当前所在的节点
        private NodeTag? _cacheNode;
        
        /// <summary>
        /// 移动组件
        /// </summary>
        public PathfinderAgent Agent { get; private set; }

        public override void OnInit()
        {
            base.OnInit();
            Agent = Entity.GetComponent<PathfinderAgent>();
            Agent.IsMoveable = false;
        }

        public override void OnDestroy()
        {
            _cacheNode = null;
            Agent.IsMoveable = false;
            Agent.OnStartMove -= OnStartMove;
            Agent.OnEndMove -= OnEndMove;
            Agent.OnUpdateMove -= OnUpdateMove;
            Agent = null;
            base.Reset();
        }

        protected override void OnAssembleModelFinish()
        {
            //注册监听
            Agent.OnStartMove += OnStartMove;
            Agent.OnEndMove += OnEndMove;
            Agent.OnUpdateMove += OnUpdateMove;
            Agent.IsMoveable = true;
        }
        
        #region 移动
        private void OnStartMove()
        {
            CrossFade(AnimationClipCode.RUN);
        }
        
        private void OnUpdateMove(Vector3 arg1, NodeTag nodeTag)
        {
            if (_cacheNode == nodeTag)return;
            _cacheNode = nodeTag;
            SetModelAlpha(nodeTag == NodeTag.Transparent ? 0.5f : 1);
        }

        private void OnEndMove()
        {
            CrossFade(AnimationClipCode.IDLE_CITY);
        }
        #endregion
        
        
        /// <summary>
        /// 切换坐骑
        /// </summary>
        public void SwitchRide()
        {
            InternalSwitchRide().Forget();
        }
        
        private async UniTaskVoid InternalSwitchRide()
        {
            //1.释放坐骑
            var rideModel = GetModel<RideModel>();
            rideModel.ReleaseEntity();
            
            //2.加载新的坐骑
            await rideModel.CreateEntity();
            
            //3.切换主模型的父物体
            var mainModel = GetModel<MainModel>();
            mainModel.SetParent(mainModel.GetParent());
        }
    }
}