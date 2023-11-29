using Cysharp.Threading.Tasks;
using HT.Framework;
using Pb.Mmo.Common;
using UnityEngine;

namespace GameScripts.RunTime.Model
{
    /// <summary>
    /// 模型基类，负责模型的动画，材质，颜色
    /// 1.设置模型的材质
    /// 2.设置模型的颜色
    /// </summary>
    public abstract class ModelBase : IReference
    {
        /// <summary>
        /// 角色实体
        /// </summary>
        public ActorEntity ActorEntity { get; private set; }
        /// <summary>
        /// 模型信息
        /// </summary>
        public ModelInfo Info => ActorEntity.ModelInfo;
        
        /// <summary>
        /// 模型实体
        /// </summary>
        public GameObject Entity { get; protected set; }
        /// <summary>
        /// 模型实体是否加载完成
        /// </summary>
        public bool IsLoadDone => Entity is not null;

        private Animator _animator;
        private float _normalizedTime;
        private float _fixedTime;
        private float _iDuration;


        public virtual void OnInit(ActorEntity actorEntity)
        {
            ActorEntity = actorEntity;
        }
        
        #region 加载模型
        /// <summary>
        /// 加载模型
        /// </summary>
        public async UniTask CreateEntity()
        {
            var location = GetLocation();
            if (string.IsNullOrEmpty(location))
            {
                return;
            }
            Entity = await Main.m_Resource.LoadPrefab(location,GetParent());
            OnEntityCreationCompleted();
        }

        /// <summary>
        /// 实体创建完成
        /// </summary>
        protected virtual void OnEntityCreationCompleted()
        {
            
        }

        /// <summary>
        /// 获得模型的地址
        /// </summary>
        /// <returns></returns>
        protected abstract string GetLocation();

        /// <summary>
        /// 获取父节点
        /// </summary>
        /// <returns></returns>
        protected virtual Transform GetParent()
        {
            return null;
        }

        /// <summary>
        /// 释放实体
        /// </summary>
        protected virtual void ReleaseEntity()
        {
            if (!IsLoadDone) return;
            Main.m_Resource.UnLoadAsset(Entity);
            Entity = null;
        }
        #endregion
        
        public void SetAnimatorCullModel(AnimatorCullingMode animatorCullingMode)
        {
            _animator.cullingMode = animatorCullingMode;
        }

        /// <summary>
        ///  加载新的动作控制器(场景：1 战斗:2 创角 3 4: 结婚)
        /// </summary>
        public void ReloadAnimator(RuntimeAnimatorController controller)
        {
            _animator.runtimeAnimatorController = controller;
        }

   
        
        /// <summary>
        /// 显示或者隐藏模型
        /// </summary>
        /// <param name="active"></param>
        public virtual void SetActive(bool value)
        {
            if (IsLoadDone)
            {
                Entity.SetActive(value);
            }
        }

        /// <summary>
        /// 设置模型透明度(0 ~ 1)
        /// </summary>
        public virtual void SetModelAlpha(float alpha)
        {
          
        }
        
        /// <summary>
        /// 设置模型的大小
        /// </summary>
        public virtual void SetSize(Vector3 size)
        {
            if (IsLoadDone)
            {
                Entity.transform.localScale = size;
            }
        }

        public void SetAnimationSpeed(float speed)
        {
            
        }

        /// <summary>
        /// 播放动画
        /// </summary>
        /// <param name="sState"></param>
        /// <param name="normalizedTime"></param>
        public void Play(string sState, float normalizedTime)
        {
            _normalizedTime = normalizedTime;

            var iHash = ModelTools.StateToHash(sState);
            _animator?.Play(iHash,0);
        }

        public void PlayInFixedTime(string sState, float fixedTime)
        {
            _fixedTime = fixedTime;
            var iHash = ModelTools.StateToHash(sState);
            _animator?.PlayInFixedTime(iHash,0,fixedTime);
        }

        /// <summary>
        /// 渐变某状态
        /// </summary>
        /// <param name="sState"></param>
        /// <param name="iDuration"></param>
        /// <param name="normalizedTime"></param>
        public void CrossFade(string sState,float iDuration, float normalizedTime)
        {
            _iDuration = iDuration;
            _normalizedTime = normalizedTime;
            var iHash = ModelTools.StateToHash(sState);
            _animator.CrossFade(iHash, iDuration, 0, normalizedTime);
        }
        
        public void CrossFadeInFixedTime(string sState,float iDuration, float fixedTime)
        {
            _iDuration = iDuration;
            _fixedTime = fixedTime;
            var iHash = ModelTools.StateToHash(sState);
            _animator.CrossFadeInFixedTime(iHash, iDuration, 0, _fixedTime);
        }

        /// <summary>
        /// 回池的时候会调用
        /// </summary>
        public virtual void Reset()
        {
            ReleaseEntity();
            ActorEntity = null;
        }
    }
}