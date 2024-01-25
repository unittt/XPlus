using System.Collections.Generic;
using GameScripts.RunTime.Magic;
using HT.Framework;
using UnityEngine;

namespace GameScripts.RunTime.Hud
{
    [EntityResource("HudContainer", true)]
    public sealed class HudContainerLogic : EntityLogicBase
    {  
        private Transform _headHudContainer;
        private Transform _waistHudContainer;
        private Transform _footHudContainer;
        private List<HudEntityLogicBase> _huds = new();
        public IHudRole Role;


        public override void OnInit()
        {
            _headHudContainer = Entity.FindChildren("HeadHud").transform;
            _waistHudContainer = Entity.FindChildren("WaistHud").transform;
            _footHudContainer = Entity.FindChildren("FootHud").transform;
        }

        public bool TryGetHud<T>(out T t) where T: HudEntityLogicBase
        {
            t = null;
            foreach (var hud in _huds)
            {
                if (hud is not T) continue;
                t = hud.Cast<T>();
                return true;
            }
            return false;
        }
        
        public override void OnDestroy()
        {
            foreach (var hud in _huds)
            {
                Main.m_Entity.DestroyEntity(hud);
            }
            _huds.Clear();
        }

        public void AddHud<T>(T hud) where T : HudEntityLogicBase
        {
            //设置Hud的父物体
            hud.Entity.transform.SetParent(GetContainer(hud.Part),false);
            hud.Entity.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            _huds.Add(hud);
        }

        private Transform GetContainer(BodyPart bodyPart)
        {
            return bodyPart switch
            {
                BodyPart.Head => _headHudContainer,
                BodyPart.Waist => _waistHudContainer,
                _ => _footHudContainer
            };
        }

        public override void OnUpdate()
        {
            if (Role == null) return;
            _headHudContainer.position = Role.GetBodyNode(BodyPart.Head).position;
            _waistHudContainer.position = Role.GetBodyNode(BodyPart.Waist).position;
            _footHudContainer.position = Role.GetBodyNode(BodyPart.Foot).position;
        }

        public void RemoveHud(HudEntityLogicBase hudEntityLogic)
        {
            if (_huds.Contains(hudEntityLogic))
            {
                _huds.Remove(hudEntityLogic);
                Main.m_Entity.DestroyEntity(hudEntityLogic);
            }
        }

        public void OnAddNewHud(HudEntityLogicBase hud)
        {
            var hudType = hud.GetType();
            var maxIndex = _huds.Count - 1;
            for (var i = maxIndex; i >= 0; i--)
            {
                var oldHud = _huds[i];
                if (oldHud.GetType() == hudType && oldHud != hud)
                {
                    oldHud.OnCreateNewHud(hud);
                }
            }
        }
    }
}