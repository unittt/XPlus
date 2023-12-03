using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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

        protected VariableArray VbArray { get; private set; }
        /// <summary>
        /// 模型信息
        /// </summary>
        public ModelInfo ModelInfo { get; private set; }
        /// <summary>
        /// 模型容器
        /// </summary>
        public Transform ModelContainer { get; private set; }


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
        public void AssembleModel(ModelInfo modelInfo)
        {
            ModelInfo = modelInfo;
            AssembleModel().Forget();
        }
        
        /// <summary>
        /// 装配模型
        /// </summary>
        private async UniTaskVoid AssembleModel()
        {
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

        public void AdjustSpeedPlay(string run, float f)
        {
            
        }
        
        public void CrossFade(string code, float iDuration = 0, float normalizedTime = 0)
        {
            foreach (var model in _models)
            {
                model.CrossFade(code,iDuration, normalizedTime);
            }
        }
    }
}