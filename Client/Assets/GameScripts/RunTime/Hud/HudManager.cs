using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using HT.Framework;
using UnityEngine;

namespace GameScripts.RunTime.Hud
{
    public class HudManager : SingletonBase<HudManager>
    {
        
        private const string HUD_NODE_SPAWN_NAME = "HudNode";

        private Dictionary<IHudRole, HudContainerLogic> _hudContainerLogics;
        private bool _isLoading;    //单线下载中
        private WaitUntil _loadWait;    //单线下载等待;
        
        public HudManager()
        {
            _loadWait = new WaitUntil(() => !_isLoading);
        }
        
        /// <summary>
        /// 显示Hud
        /// </summary>
        /// <param name="role"></param>
        /// <param name="args"></param>
        /// <typeparam name="T"></typeparam>
        public async UniTaskVoid ShowHud<T>(IHudRole role, params object[] args)where T : AsyncHud
        {
            if (_isLoading)
            {
                await _loadWait;
            }
            _isLoading = true;
            
            if (!_hudContainerLogics.TryGetValue(role, out var HudContainerLogic))
            {
                HudContainerLogic = await Main.m_Entity.CreateEntity<HudContainerLogic>(HUD_NODE_SPAWN_NAME);
                HudContainerLogic.Role = role;
                _hudContainerLogics.Add(role, HudContainerLogic);
            }
            
            var hud = await Main.m_Entity.CreateEntity<T>(typeof(T).Name);
            HudContainerLogic.AddHud(hud);
            hud.Fill(args);
            _isLoading = false;
        }
    }
}