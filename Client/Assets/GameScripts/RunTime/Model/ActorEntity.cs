using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameScripts.RunTime.DataUser;
using GameScripts.RunTime.Utility.Timer;
using HT.Framework;
using Pb.Mmo.Common;
using UnityEngine;

namespace GameScripts.RunTime.Model
{
    /// <summary>
    /// 角色实体
    /// </summary>
    public abstract class ActorEntity : EntityLogicBase
    {
       
        private readonly List<ModelBase> _models = new();
        private readonly Dictionary<Type, ModelBase> _modelInstance = new();
        private readonly List<int> _timeKeys = new();

        protected VariableArray VbArray { get; private set; }
        /// <summary>
        /// 模型信息
        /// </summary>
        public ModelInfo ModelInfo { get; private set; }
        /// <summary>
        /// 模型容器
        /// </summary>
        public Transform ModelContainer { get; private set; }


        #region 组合动作
        private event Action _comboHitEvent;
        private ComboActionInfo[]  _comboActionInfos;
        private int _comboIndex;

        private int _shape;

        #endregion
        
        
        /// <summary>
        /// 实体层级
        /// </summary>
        public abstract int Layer { get; }

        public override void OnInit()
        {
            VbArray = Entity.GetComponent<VariableBehaviour>().Container;
            ModelContainer = VbArray.Get<Transform>("modelNode");
            //坐骑
            RegisterModel<RideModel>();
            //主模型
            RegisterModel<MainModel>();
            //武器
            RegisterModel<WeaponModel>();
            //翅膀
            RegisterModel<WingModel>();
            
            foreach (var model in _models)
            {
                model.OnInit(this);
            }
            
        }
        
        public override void OnDestroy()
        {
            var maxIndex = _models.Count -1;
            for (var i = maxIndex; i >= 0; i--)
            {
                Main.m_ReferencePool.Despawn(_models[i]);
            }
            _models.Clear();
            _modelInstance.Clear();
        }
        
        /// <summary>
        /// 注册模型逻辑
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private void RegisterModel<T>() where T : ModelBase, new()
        {
            var model = Main.m_ReferencePool.Spawn<T>();
            _models.Add(model);
            _modelInstance.Add(model.GetType(), model);
        }

        /// <summary>
        /// 获得模型逻辑
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetModel<T>() where T: ModelBase
        {
            var type = typeof(T);
            return _modelInstance.ContainsKey(type) ? _modelInstance[type].Cast<T>() : null;
        }
        
        /// <summary>
        /// 装配演员
        /// </summary>
        /// <param name="modelInfo"></param>
        public async UniTask AssembleModel(ModelInfo modelInfo)
        {
            ModelInfo = modelInfo;
            foreach (var model in _models)
            {
                await model.CreateEntity();
            }
            OnAssembleModelFinish();
        }
        
        protected virtual void OnAssembleModelFinish()
        {
            
        }
        
        /// <summary>
        /// 设置模型的透明度
        /// </summary>
        /// <param name="alpha"></param>
        public virtual void SetModelAlpha(float alpha)
        {
            foreach (var model in _models)
            {
                if (model.IsLoadDone)
                {
                    model.SetModelAlpha(alpha);
                }
            }
        }


        #region 模型切换
        /// <summary>
        /// 切换主模型
        /// </summary>
        public void SwitchMainModel()
        {
            InternalSwitchMainModel().Forget();
        }
        
        private async UniTaskVoid InternalSwitchMainModel()
        {
            //1.移除主模型
            var mainModel = GetModel<MainModel>();
            mainModel.ReleaseEntity();
            
            //2.加载新的主模型
            await mainModel.CreateEntity();
            
            //3.设置武器和翅膀的父物体
            var weaponModel = GetModel<WeaponModel>();
            weaponModel.SetParent(weaponModel.GetParent());
            
            var wingModel = GetModel<WingModel>();
            wingModel.SetParent(wingModel.GetParent());
        }

        /// <summary>
        /// 切换武器
        /// </summary>
        public virtual void SwitchWeapon()
        {
            //1.移除武器
            var weaponModel = GetModel<WeaponModel>();
            weaponModel.ReleaseEntity();
            //2.加载新武器
            weaponModel.CreateEntity().Forget();
        }

        /// <summary>
        /// 切换翅膀
        /// </summary>
        public virtual void SwitchWing()
        {
            //1.移除翅膀
            var wingModel = GetModel<WingModel>();
            wingModel.ReleaseEntity();
            //2.加载新翅膀
            wingModel.CreateEntity().Forget();
        }
        #endregion

        
        public virtual string GetName()
        {
            return String.Empty;
        }


        #region 动画
        
        public void Play(string state,float startNormalized = 0, float endNormalized = 0, HTFAction callBack = null)
        {
            //1.清理动画时间事件
            ClearAnimTimeEvent();
            //2.播放动画
            foreach (var model in _models)
            {
                model.Play(state, startNormalized);
            }
            //3.注册结束事件
            if (endNormalized <= 0 || callBack is null) return;
            var fixedTime = ModelTools.NormalizedToFixed(_shape, state, endNormalized - startNormalized);
            FixedEvent(fixedTime, callBack);
        }

        public void PlayInFixedTime(string state, float startFixed, float endFixed,HTFAction callBack = null)
        {
            //1.清理动画时间事件
            ClearAnimTimeEvent();
            //2.播放动画
            foreach (var model in _models)
            {
                model.PlayInFixedTime(state, startFixed);
            }
            //3.注册结束事件
            if (endFixed >startFixed && endFixed > 0)
            {
                FixedEvent(startFixed - endFixed, callBack);
            }
        }
        
        /// <summary>
        /// 帧播放动画
        /// </summary>
        /// <param name="state"></param>
        /// <param name="startFrame"></param>
        /// <param name="endFrame"></param>
        /// <param name="callBack"></param>
        public void PlayInFrame(string state, int startFrame, int endFrame, HTFAction callBack = null)
        {
            //1.如果不存在帧动画 跳出
            if (!AnimClipData.TryGetAnimClipInfo(_shape, state, out var dClipInfo)) return;
            //2.播放动画
            var startNormalized = startFrame / dClipInfo.Frame;
            Play(state, startNormalized);
            //3.注册结束事件
            if (endFrame <= 0 || endFrame <= startFrame) return;
            var fixedTime = ModelTools.FrameToTime(endFrame - startFrame);
            FixedEvent(fixedTime, callBack);
        }
        
      
        /// <summary>
        /// 渐入动画
        /// </summary>
        /// <param name="state"></param>
        /// <param name="duration"></param>
        /// <param name="startNormalized"></param>
        /// <param name="endNormalized"></param>
        /// <param name="callBack"></param>
        public void CrossFade(string state, float duration, float startNormalized, float endNormalized, HTFAction callBack = null)
        {
            //1.清理动画时间事件
            ClearAnimTimeEvent();
            //2.播放动画
            foreach (var model in _models)
            {
                model.CrossFade(state, duration, startNormalized);
            }
            //3.注册结束事件
            if (endNormalized <= 0|| endNormalized < startNormalized) return;
            var fixedTime = ModelTools.NormalizedToFixed(_shape, state, endNormalized - startNormalized);
            FixedEvent(fixedTime, callBack);
        }

        public void CrossFadeInFixedTime(string state, float duration, float startFixed, float endFixed, HTFAction callBack = null)
        {
            //1.清理动画时间事件
            ClearAnimTimeEvent();
            //2.播放动画
            foreach (var model in _models)
            {
                model.CrossFadeInFixedTime(state, duration, startFixed);
            }
            //3.注册结束事件
            if (endFixed <= 0|| endFixed < startFixed) return;
            FixedEvent(endFixed - startFixed, callBack);
        }
        
        public void AdjustSpeedPlay(string state, float adjustTime)
        {
            PlayInFixedTime(state,0,0);
            if (AnimClipData.TryGetAnimClipInfo(_shape, state, out var dClipInfo))
            {
                // SetSpeed(dClipInfo.Length / adjustTime);
            }
        }
        
        public void AdjustSpeedPlayInFrame(string state, float adjustTime, int startFrame, int endFrame)
        {
            if (endFrame == 0)
            {
                if (AnimClipData.TryGetAnimClipInfo(_shape, state, out var dClipInfo))
                {
                    endFrame = dClipInfo.Frame;
                    // SetSpeed(dClipInfo.Length / adjustTime);
                }
            }

            var time = ModelTools.FrameToTime(endFrame - startFrame);
            var speed = time / adjustTime;
            //callback = SetSpeed(0);
            PlayInFrame(state, startFrame, startFrame + (int)((endFrame - startFrame) / speed));
            // SetSpeed(speed);
        }

        public void Pause(int frame, HTFAction callBack)
        {
            //1.清理所有事件
            ClearAnimTimeEvent();
            // SetSpeed(0)
            //触发暂停事件
            FrameEvent(frame, callBack);
        }

        #region 组合动画
        /// <summary>
        /// 连续动画的播放
        /// </summary>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public bool PlayCombo(string actionName)
        {
            var list = ComboActionData.GetComboActionInfos(0, actionName);
            if (list is null)
            {
                return false;
            }

            _comboActionInfos = list;
            _comboIndex = 0;
            ComboStep();
            return true;
        }
        
        /// <summary>
        /// 组合动作步骤
        /// </summary>
        private void ComboStep()
        {
            if (_comboActionInfos is null || _comboIndex >= _comboActionInfos.Length)
            {
                return;
            }

            var act = _comboActionInfos[_comboIndex];
            _comboIndex++;
            var speed = act.speed;
            if (act.action == "pause")
            {
                //触发暂停 暂时没有这个
            }
            else
            {
                PlayInFrame(act.action, act.start_frame/ speed, act.end_frame/speed, ComboStep);
                if (act.hit_frame > 0)
                {
                    FrameEvent((act.hit_frame - act.start_frame)/speed, NotifyComboHit);
                }
            }
        }

        /// <summary>
        /// 设置组合动作命中事件
        /// </summary>
        /// <param name="callBack"></param>
        public void SetComboHitEvent(Action callBack)
        {
            _comboHitEvent += callBack;
        }
        
        /// <summary>
        /// 触发组合动作命中
        /// </summary>
        private void NotifyComboHit()
        {
            _comboHitEvent.Invoke();
            _comboHitEvent = null;
        }
        #endregion
        
        #region 动画事件
        private void FrameEvent(int frame, HTFAction callBack)
        {
            var fixedTime = ModelTools.FrameToTime(frame);
            FixedEvent(fixedTime,callBack);
        }

        private void NormalizedEvent(string sState, float normalizedTime, HTFAction callBack)
        {
            if (!AnimClipData.TryGetAnimClipInfo(_shape, sState, out var dClipInfo)) return;
            var fixedTime = dClipInfo.Length * normalizedTime;
            FixedEvent(fixedTime,callBack);
        }
        
        private void FixedEvent(float fixedTime, HTFAction callBack)
        {
            if (callBack is null) return;
            fixedTime = Mathf.Max((fixedTime == 0 ? 0.01f : fixedTime), 0);
            var key = TimerManager.RegisterTimer(fixedTime, callBack);
            _timeKeys.Add(key);
        }
        
        /// <summary>
        /// 清理动画事件
        /// </summary>
        private void ClearAnimTimeEvent()
        {
            foreach (var key in _timeKeys)
            {
                TimerManager.StopTimer(key);
            }
            _timeKeys.Clear();
        }
        #endregion
        #endregion
    }
}