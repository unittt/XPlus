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
        private MaterialPropertyBlock _mpBlock = new();

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
        }

        public override void Reset()
        {
            _renderer = null;
            WeaponContainer = null;
            base.Reset();
        }
        
        protected override Transform GetParent()
        {
            return ActorEntity.ActorContainer;
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
    }
}