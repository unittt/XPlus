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
        public GameObject Entity { get; private set; }
        /// <summary>
        /// 模型实体是否加载完成
        /// </summary>
        public bool IsLoadDone => Entity is not null;

        private Animator _animator;


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
            //设置层级
            Entity.SetLayerIncludeChildren(ActorEntity.Layer);
            //获取动画控制器
            Entity.TryGetComponent(out _animator);
            OnEntityCreationCompleted();
            // CrossFade(AnimationClipCode.IDLE_CITY,0, 0);
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
        public virtual Transform GetParent()
        {
            return ActorEntity.ModelContainer;
        }

        /// <summary>
        /// 设置父物体
        /// </summary>
        /// <param name="parent"></param>
        public virtual void SetParent(Transform parent)
        {
            if (IsLoadDone)
            {
                Entity.transform.SetParent(parent, false);
            }
        }
        
        /// <summary>
        /// 释放实体
        /// </summary>
        public virtual void ReleaseEntity()
        {
            if (!IsLoadDone) return;
            Main.m_Resource.UnLoadAsset(Entity);
            Entity = null;
        }
        #endregion
        
        /// <summary>
        ///  加载新的动作控制器(场景：1 战斗:2 创角 3 4: 结婚)
        /// </summary>
        public void SetAnimatorController(RuntimeAnimatorController controller)
        {
            _animator.runtimeAnimatorController = controller;
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

        #region 动画
        /// <summary>
        /// 设置动画速度
        /// </summary>
        /// <param name="speed"></param>
        public void SetAnimationSpeed(float speed)
        {
            if (_animator is null) return;
            _animator.speed = speed;
        }
        
        /// <summary>
        /// 播放某个状态(normalizedTime)
        /// </summary>
        /// <param name="state"></param>
        /// <param name="normalizedTime"></param>
        public virtual void Play(string state, float normalizedTime)
        {
            if (_animator is null) return;
            normalizedTime = Mathf.Max(normalizedTime, 0);
            var iHash = ModelTools.StateToHash(state);
            _animator.Play(iHash,0, normalizedTime);
        }

        /// <summary>
        /// 播放某个状态(fixedTime)
        /// </summary>
        /// <param name="sState"></param>
        /// <param name="fixedTime"></param>
        public void PlayInFixedTime(string state, float fixedTime)
        {
            if (_animator is null) return;
            fixedTime = Mathf.Max(fixedTime, 0);
            var iHash = ModelTools.StateToHash(state);
            _animator.PlayInFixedTime(iHash,0,fixedTime);
        }

        /// <summary>
        /// 渐变某状态
        /// </summary>
        /// <param name="sState"></param>
        /// <param name="iDuration"></param>
        /// <param name="normalizedTime"></param>
        public void CrossFade(string state, float iDuration, float normalizedTime)
        {
            if (_animator is null) return;
            iDuration = Mathf.Max(iDuration, 0);
            state = CheckClickState(state);
            var iHash = ModelTools.StateToHash(state);
            _animator.CrossFade(iHash, iDuration, 0, normalizedTime);
        }

        /// <summary>
        /// 渐变某状态(fixedTime)
        /// </summary>
        /// <param name="sState"></param>
        /// <param name="duration"></param>
        /// <param name="fixedTime"></param>
        public void CrossFadeInFixedTime(string sState,float duration, float fixedTime)
        {
            if (_animator is null) return;
            var iHash = ModelTools.StateToHash(sState);
            _animator.CrossFadeInFixedTime(iHash, duration, 0, fixedTime);
        }

        /// <summary>
        /// 检查动画
        /// </summary>
        /// <param name="sState"></param>
        /// <returns></returns>
        protected virtual string CheckClickState(string sState)
        {
            return sState;
        }
        
        #endregion
        
        /// <summary>
        /// 回池的时候会调用
        /// </summary>
        public virtual void Reset()
        {
            ReleaseEntity();
            ActorEntity = null;
            _animator = null;
        }
    }
}