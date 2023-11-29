using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GridMap;
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
    public sealed class ActorEntity : EntityLogicBase
    {
       
        private readonly List<ModelBase> _models = new();
        private readonly Dictionary<Type, ModelBase> _modelInstance = new();

        /// <summary>
        /// 模型信息
        /// </summary>
        public ModelInfo ModelInfo { get; private set; }
        /// <summary>
        /// 角色显示容器
        /// </summary>
        public Transform ActorContainer { get; private set; }
        /// <summary>
        /// 移动组件
        /// </summary>
        public MapWalker Walker { get; private set; }

        //当前所在的节点
        private NodeTag? _cacheNode;

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

        public override void Reset()
        {
            _cacheNode = null;
            Walker.OnStartMove -= OnStartMove;
            Walker.OnEndMove -= OnEndMove;
            Walker.OnUpdateMove -= OnUpdateMove;
            Walker = null;
        }
        
        #region 移动
        private void OnStartMove()
        {
            
        }
        
        private void OnUpdateMove(Vector3 arg1, NodeTag nodeTag)
        {
            if (_cacheNode == nodeTag)return;
            _cacheNode = nodeTag;
            SetModelAlpha(nodeTag == NodeTag.Transparent ? 0.5f : 1);
        }

        private void OnEndMove()
        {
            
        }
        #endregion

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
            
            //播放默认动画
            
            //注册监听
            Walker.OnStartMove += OnStartMove;
            Walker.OnEndMove += OnEndMove;
            Walker.OnUpdateMove += OnUpdateMove;
        }
        
        private async UniTaskVoid LoadEntities()
        {
            foreach (var model in _models)
            {
                await model.CreateEntity();
            }
        }

        /// <summary>
        /// 设置模型的透明度
        /// </summary>
        /// <param name="alpha"></param>
        private void SetModelAlpha(float alpha)
        {
            foreach (var model in _models)
            {
                if (model.IsLoadDone)
                {
                    model.SetModelAlpha(alpha);
                }
            }
        }
    }
}