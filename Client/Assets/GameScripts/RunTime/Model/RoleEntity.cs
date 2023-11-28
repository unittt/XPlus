using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using HT.Framework;
using Pb.Mmo.Common;

namespace GameScripts.RunTime.Model
{
    /// <summary>
    /// 角色实体
    /// </summary>
    [EntityResource("RoleEntity",true)]
    public class RoleEntity : EntityLogicBase
    {
       
        private readonly List<ModelBase> _models = new();
        private readonly Dictionary<Type, ModelBase> _modelInstance = new();

        /// <summary>
        /// 模型信息
        /// </summary>
        public ModelInfo ModelInfo { get; private set; }
        
        public override void OnInit()
        {
            RegisterModel<MainModel>();
            RegisterModel<WeaponModel>();
            RegisterModel<WingModel>();
            RegisterModel<RideModel>();
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
            model.Role = this;
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
                await model.LoadEntity();
            }
        }
    }
}