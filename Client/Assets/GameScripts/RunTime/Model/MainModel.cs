using HT.Framework;
using UnityEngine;

namespace GameScripts.RunTime.Model
{
    /// <summary>
    /// 主模型
    /// </summary>
    public class MainModel : ModelBase
    {
        
        private SkinnedMeshRenderer _renderer;
        private MaterialPropertyBlock _mpBlock;

        /// <summary>
        /// 武器挂点
        /// </summary>
        public Transform WeaponContainer { get; private set; }
        /// <summary>
        /// 翅膀挂点
        /// </summary>
        public Transform WingContainer { get; private set; }
        
        protected override void OnEntityCreationCompleted()
        {
            var variableBehaviour = Entity.GetComponent<VariableBehaviour>();
            _renderer = variableBehaviour.Container.Get<SkinnedMeshRenderer>("renderer");
            WeaponContainer = variableBehaviour.Container.Get<Transform>("mount_righthand");
            _mpBlock = new MaterialPropertyBlock();
        }

        public override void ReleaseEntity()
        {
            base.ReleaseEntity();
            _renderer = null;
            WeaponContainer = null;
        }
        
        public override Transform GetParent()
        {
            var rideModel = ActorEntity.GetModel<RideModel>();
            return rideModel is { IsLoadDone: true } ? rideModel.MountRideContainer : base.GetParent();
        }

        protected override string GetLocation()
        {
            return "model" + Info.shape;
        }
        
        public override void SetModelAlpha(float alpha)
        {
            if (!IsLoadDone) return;
            
            var color = Color.white;
            color.a = alpha;
            
            _renderer.GetPropertyBlock(_mpBlock);
            _mpBlock.SetColor("_Alpha",color);
            _renderer.SetPropertyBlock(_mpBlock);
        }

        protected override string CheckClickState(string sState)
        {
            
            //如果当前为城市闲置
            if (sState == AnimationClipCode.IDLE_CITY)
            {
                var horse = ActorEntity.ModelInfo.horse;
                if (horse > 0 && horse != 4008)
                {
                    return AnimationClipCode.IDLE_RIDE;
                }
            }
            
            if (sState == AnimationClipCode.RUN)
            {
                var horse = ActorEntity.ModelInfo.horse;
                if (horse > 0 && horse != 4008)
                {
                    return AnimationClipCode.RUN_RIDE;
                }
            }
            
            return sState;
        }
    }
}