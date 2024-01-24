using System.Collections.Generic;
using GameScripts.RunTime.Magic;
using HT.Framework;
using UnityEngine;

namespace GameScripts.RunTime.Hud
{
    public sealed class HudContainerLogic : EntityLogicBase
    {  
        private Transform _headHudContainer;
        private Transform _waistHudContainer;
        private Transform _footHudContainer;
        private List<AsyncHud> _huds = new();
        public IHudRole Role;


        public override void OnInit()
        {
            Entity.name = Role.GetHudName();
            _headHudContainer = Entity.FindChildren("HeadHud").transform;
            _waistHudContainer = Entity.FindChildren("WaistHud").transform;
            _footHudContainer = Entity.FindChildren("FootHud").transform;
        }

        public override void OnDestroy()
        {
            foreach (var hud in _huds)
            {
                Main.m_Entity.DestroyEntity(hud);
            }
            _huds.Clear();
        }

        public void AddHud<T>(T hud) where T : AsyncHud
        {
            var maxIndex = _huds.Count - 1;
            for (var i = maxIndex; i >= 0; i--)
            {
                var oldHud = _huds[i];
                if (oldHud.GetType() == typeof(T))
                {
                    oldHud.OnCreateNewHud();
                }
            }
            _huds.Add(hud);
            //设置Hud的父物体
            hud.Entity.transform.SetParent(GetContainer(hud.Part));
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
            _waistHudContainer.position = Role.GetBodyNode(BodyPart.Head).position;
            _footHudContainer.position = Role.GetBodyNode(BodyPart.Head).position;
        }
    }
}