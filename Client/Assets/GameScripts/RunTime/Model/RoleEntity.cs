using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GridMap.RunTime.Walker;
using HT.Framework;
using Pb.Mmo.Common;
using UnityEngine;

namespace GameScripts.RunTime.Model
{
    /// <summary>
    /// 角色实体
    /// </summary>
    [EntityResource("ActorEntity",true)]
    public sealed class RoleEntity : EntityLogicBase
    {
       
        private readonly List<ModelBase> _models = new();
        private readonly Dictionary<Type, ModelBase> _modelInstance = new();

        /// <summary>
        /// 模型信息
        /// </summary>
        public ModelInfo ModelInfo { get; private set; }

        public Transform ActorContainer { get; private set; }

        public MapWalker Walker { get; private set; }

        public override void OnInit()
        {
            Walker = Entity.GetComponent<MapWalker>();
            ActorContainer = Entity.FindChildren("ActorNode").transform;
            
            RegisterModel<MainModel>();
            RegisterModel<WeaponModel>();
            RegisterModel<WingModel>();
            RegisterModel<RideModel>();

            foreach (var model in _models)
            {
                model.OnInit(this);
            }
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

        public void Fill(ModelInfo modelInfo)
        {
            ModelInfo = modelInfo;
            LoadEntities().Forget();
        }
        
        private async UniTaskVoid LoadEntities()
        {
            foreach (var model in _models)
            {
                await model.CreateEntity();
            }
        }
    }
}