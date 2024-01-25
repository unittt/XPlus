using System;
using System.Collections.Generic;
using System.Reflection;
using Cysharp.Threading.Tasks;
using GameScripts.RunTime.Hud.Attribute;
using HT.Framework;
using UnityEngine;

namespace GameScripts.RunTime.Hud
{
    public class HudManager : SingletonBase<HudManager>
    {
        
        private const string HUD_NODE_SPAWN_NAME = "HudNode";

        private Dictionary<IHudRole, HudContainerLogic> _hudContainerLogics;
        private Dictionary<Type, HudEntityAttribute> _t2ADic;
        
        private bool _isLoading;    //单线下载中
        private WaitUntil _loadWait;    //单线下载等待;

        
        public HudManager()
        {
            _hudContainerLogics = new Dictionary<IHudRole, HudContainerLogic>();
            _t2ADic = new Dictionary<Type, HudEntityAttribute>();
            
            _loadWait = new WaitUntil(() => !_isLoading);
         
            var types = ReflectionToolkit.GetTypesInRunTimeAssemblies(type =>
                type.IsSubclassOf(typeof(HudEntityLogicBase)) && !type.IsAbstract);
            
            foreach (var type in types)
            {
                var hudEntityAttribute = type.GetCustomAttribute<HudEntityAttribute>();
                _t2ADic.Add(type, hudEntityAttribute);
            }
        }
        
        /// <summary>
        /// 显示Hud
        /// </summary>
        /// <param name="role"></param>
        /// <param name="args"></param>
        /// <typeparam name="T"></typeparam>
        public async UniTask ShowHud<T>(IHudRole role,HTFAction<T> callBack)where T : HudEntityLogicBase
        {
            if (_isLoading)
            {
                await _loadWait;
            }
            _isLoading = true;
            
            //如果没有hud容器 则创建一个
            if (!_hudContainerLogics.TryGetValue(role, out var hudContainerLogic))
            {
                hudContainerLogic = await Main.m_Entity.CreateEntity<HudContainerLogic>(HUD_NODE_SPAWN_NAME);
                hudContainerLogic.Role = role;
                hudContainerLogic.Entity.name = role.GetHudName();
                _hudContainerLogics.Add(role, hudContainerLogic);
            }
            
            var isSingle = _t2ADic[typeof(T)].IsSingle;
            //如果为单一的 并且已经存在了
            if (!isSingle || !hudContainerLogic.TryGetHud(out T hud))
            {
                hud = await Main.m_Entity.CreateEntity<T>(typeof(T).Name);
                hud.Container = hudContainerLogic;
                hudContainerLogic.AddHud(hud);
            }
         
            callBack?.Invoke(hud);
            _isLoading = false;
        }
    }
}