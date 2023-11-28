using UnityEngine;

namespace GameScripts.RunTime.Model
{
    /// <summary>
    /// 主模型
    /// </summary>
    public class MainModel : ModelBase
    {
        //1.加载模型
        //2.加载人物动画控制器
        //3.播放动画

        private SkinnedMeshRenderer _sMenderer;
        
        /// <summary>
        /// 武器挂点
        /// </summary>
        public Transform WeaponContainer { get; private set; }
        /// <summary>
        /// 翅膀挂点
        /// </summary>
        public Transform WingContainer { get; private set; }
        
        
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