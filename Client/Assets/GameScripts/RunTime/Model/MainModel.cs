using HT.Framework;
using UnityEngine;

namespace GameScripts.RunTime.Model
{
    /// <summary>
    /// 主模型
    /// </summary>
    public class MainModel : ModelBase
    {
        
        private SkinnedMeshRenderer _sMenderer;
        
        /// <summary>
        /// 武器挂点
        /// </summary>
        public Transform WeaponContainer { get; private set; }
        /// <summary>
        /// 翅膀挂点
        /// </summary>
        public Transform WingContainer { get; private set; }


        public override void OnInit(RoleEntity roleEntity)
        {
            base.OnInit(roleEntity);
        
        }

        protected override void OnEntityCreationCompleted()
        {
            var variableBehaviour = Entity.GetComponent<VariableBehaviour>();
            WeaponContainer = variableBehaviour.Container.Get<Transform>("mount_righthand");
        }

        protected override Transform GetParent()
        {
            return RoleEntity.ActorContainer;
        }

        protected override string GetLocation()
        {
            return "model" + Info.shape;
        }
    }
}