using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameScripts.RunTime.DataUser;
using GameScripts.RunTime.Hud;
using GameScripts.RunTime.Magic;
using GameScripts.RunTime.Utility.Timer;
using HT.Framework;
using Pb.Mmo.Common;
using UnityEngine;

namespace GameScripts.RunTime.Model
{
    /// <summary>
    /// 角色实体
    /// </summary>
    public abstract class ActorEntity : EntityLogicBase, IHudRole
    {
       
        private readonly List<ModelBase> _models = new();
        private readonly Dictionary<Type, ModelBase> _modelInstance = new();
        private readonly List<int> _timeKeys = new();

        protected VariableArray VbArray { get; private set; }
        /// <summary>
        /// 模型信息
        /// </summary>
        public ModelInfo ModelInfo { get; private set; }

        public int Shape => ModelInfo.shape;
        /// <summary>
        /// 模型容器
        /// </summary>
        public Transform ModelContainer { get; private set; }


        #region 组合动作
        private event Action _comboHitEvent;
        private ComboActionInfo[]  _comboActionInfos;
        private int _comboIndex;
        #endregion

        /// <summary>
        /// 是否忙碌的 (主要在于模型加载)
        /// </summary>
        public bool IsBusy { get; protected set; }

        public WaitUntil IsBusyWait { get; private set; }

        
        #region Entity 基础属性

        public Transform transform => Entity?.transform;
        public Transform Parent => Entity?.transform.parent;

        /// <summary>
        /// 世界坐标
        /// </summary>
        public Vector3 Position
        {
            get => Entity is null ? Vector3.zero : Entity.transform.position;
            set
            {
                if (Entity)
                {
                    Entity.transform.position = value;
                }
            }
        }

        /// <summary>
        /// 本地坐标
        /// </summary>
        public Vector3 LocalPosition
        {
            get => Entity is null ? Vector3.zero : Entity.transform.localPosition;
            set
            {
                if (Entity)
                {
                    Entity.transform.localPosition = value;
                }
            }
        }

        /// <summary>
        /// 欧拉角
        /// </summary>
        public Vector3 LocalEulerAngles
        {
            get => Entity is null ? Vector3.zero : Entity.transform.localEulerAngles;
            set
            {
                if (Entity)
                {
                    Entity.transform.localEulerAngles = value;
                }
            }
        }
        

        /// <summary>
        /// 实体层级
        /// </summary>
        public abstract int Layer { get; }
        #endregion

        private Transform footHudNode;
        
        
        #region 初始化
        public override void OnInit()
        {
            IsBusyWait ??= new(() => !IsBusy);
            
            VbArray = Entity.GetComponent<VariableBehaviour>().Container;
            ModelContainer = VbArray.Get<Transform>("modelNode");
            footHudNode =  VbArray.Get<Transform>("footHudnode");
            
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

        /// <summary>
        /// 填充数据
        /// </summary>
        /// <param name="modelInfo"></param>
        public virtual void Fill(ModelInfo modelInfo)
        {
            ModelInfo = modelInfo;
            LoadAllModel().Forget();
        }
        
        /// <summary>
        /// 初始化时加载所有模型
        /// </summary>
        private async UniTaskVoid LoadAllModel()
        {
            IsBusy = true;
            foreach (var model in _models)
            {
                await model.CreateEntity();
            }
            await OnAllModelLoadDone();
            IsBusy = false;
        }

        /// <summary>
        /// 当所有模型都加载完成
        /// </summary>
        protected abstract UniTask OnAllModelLoadDone();
        #endregion


        #region 模型
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


        #region 切换主模型
        /// <summary>
        /// 切换主模型
        /// </summary>
        public void SwitchMainModel()
        {
            InternalSwitchMainModel().Forget();
        }
        
        private async UniTaskVoid InternalSwitchMainModel()
        {
            if (IsBusy)
            {
                await IsBusyWait;
            }

            IsBusy = true;
            //1.移除主模型
            var mainModel = GetModel<MainModel>();
            mainModel.ReleaseEntity();
            
            //2.加载新的主模型
            await mainModel.CreateEntity();
            
            //3.设置武器父物体
            var weaponModel = GetModel<WeaponModel>();
            weaponModel.SetParent(weaponModel.GetParent());
            
            //4.设置翅膀的父物体
            var wingModel = GetModel<WingModel>();
            wingModel.SetParent(wingModel.GetParent());

            IsBusy = false;
        }
        #endregion

        #region 切换武器
        /// <summary>
        /// 切换武器
        /// </summary>
        public void SwitchWeapon()
        {
            InternalSwitchWeapon().Forget();
        }

        private async UniTaskVoid InternalSwitchWeapon()
        {
            if (IsBusy)
            {
                await IsBusyWait;
            }

            IsBusy = true;
            //1.移除武器
            var weaponModel = GetModel<WeaponModel>();
            weaponModel.ReleaseEntity();
            //2.加载新武器
            await weaponModel.CreateEntity();
            IsBusy = false;
        }
        #endregion
        
        #region 切换翅膀
        /// <summary>
        /// 切换翅膀
        /// </summary>
        public virtual void SwitchWing()
        {
            InternalSwitchWing().Forget();
        }

        private async UniTaskVoid InternalSwitchWing()
        {
            if (IsBusy)
            {
                await IsBusyWait;
            }
            IsBusy = true;
            //1.移除翅膀
            var wingModel = GetModel<WingModel>();
            wingModel.ReleaseEntity();
            //2.加载新翅膀
            await wingModel.CreateEntity();
            IsBusy = false;
        }
        #endregion
        
        #endregion
        
        
        public void SetParent(Transform parent)
        {
            Entity.transform.SetParent(parent);
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
        

        
        public virtual string GetName()
        {
            return String.Empty;
        }


        #region 动画
        
        public void Play(string state,float startNormalized, float endNormalized, HTFAction callBack)
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
            var fixedTime = ModelTools.NormalizedToFixed(Shape, state, endNormalized - startNormalized);
            FixedEvent(fixedTime, callBack);
        }

        public void PlayInFixedTime(string state, float startFixed, float endFixed,HTFAction callBack)
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
        public void PlayInFrame(string state, int startFrame, int endFrame, HTFAction callBack)
        {
            //1.如果不存在帧动画 跳出
            if (!AnimClipData.TryGetAnimClipInfo(Shape, state, out var dClipInfo)) return;
            //2.播放动画
            var startNormalized = startFrame / dClipInfo.Frame;
            Play(state, startNormalized,0, null);
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
        public void CrossFade(string state, float duration, float startNormalized, float endNormalized, HTFAction callBack)
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
            var fixedTime = ModelTools.NormalizedToFixed(Shape, state, endNormalized - startNormalized);
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
        
        public void AdjustSpeedPlay(string state, float adjustTime,HTFAction callBack)
        {
            PlayInFixedTime(state,0,0,callBack);
            if (AnimClipData.TryGetAnimClipInfo(Shape, state, out var dClipInfo))
            {
                // SetSpeed(dClipInfo.Length / adjustTime);
            }
        }
        
        public void AdjustSpeedPlayInFrame(string state, float adjustTime, int startFrame, int endFrame, HTFAction callBack)
        {
            if (endFrame == 0)
            {
                if (AnimClipData.TryGetAnimClipInfo(Shape, state, out var dClipInfo))
                {
                    endFrame = dClipInfo.Frame;
                    // SetSpeed(dClipInfo.Length / adjustTime);
                }
            }

            var time = ModelTools.FrameToTime(endFrame - startFrame);
            var speed = time / adjustTime;
            //callback = SetSpeed(0);
            PlayInFrame(state, startFrame, startFrame + (int)((endFrame - startFrame) / speed),callBack);
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
            if (!AnimClipData.TryGetAnimClipInfo(Shape, sState, out var dClipInfo)) return;
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

        #region Hub

        public void AddHud()
        {
            
        }
        

        #endregion

        public  string GetHudName()
        {
            return Entity.name;
        }

        public Transform GetBodyNode(BodyPart bodyPart)
        {
            return footHudNode;
        }
    }
}