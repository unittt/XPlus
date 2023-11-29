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

        //当前所在的节点
        private NodeTag? _cacheNode;
        
        /// <summary>
        /// 模型信息
        /// </summary>
        public ModelInfo ModelInfo { get; private set; }
      
        /// <summary>
        /// 移动组件
        /// </summary>
        public MapWalker Walker { get; private set; }
        
        /// <summary>
        /// 角色显示容器
        /// </summary>
        public Transform ActorContainer { get; private set; }

        /// <summary>
        /// 是否为乘骑状态
        /// </summary>
        public bool IsRiding => ModelInfo.horse > 0;

        public override void OnInit()
        {
            Walker = Entity.GetComponent<MapWalker>();
            ActorContainer = Entity.FindChildren("ActorNode").transform;
        
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
            
            //播放动画
            
            
            
            //注册监听
            Walker.OnStartMove += OnStartMove;
            Walker.OnEndMove += OnEndMove;
            Walker.OnUpdateMove += OnUpdateMove;
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


        #region 模型切换
        /// <summary>
        /// 切换坐骑
        /// </summary>
        public void SwitchRide()
        {
            InternalSwitchRide().Forget();
        }

        private async UniTaskVoid InternalSwitchRide()
        {
            //1.释放坐骑
            var rideModel = GetModel<RideModel>();
            rideModel.ReleaseEntity();
            
            //2.加载新的坐骑
            await rideModel.CreateEntity();
            
            //3.切换主模型的父物体
            var mainModel = GetModel<MainModel>();
            mainModel.SetParent(mainModel.GetParent());
        }

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
        public void SwitchWeapon()
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
        public void SwitchWing()
        {
            //1.移除翅膀
            var wingModel = GetModel<WingModel>();
            wingModel.ReleaseEntity();
            //2.加载新翅膀
            wingModel.CreateEntity().Forget();
        }
        #endregion
    }
}