using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using HT.Framework;
using UnityEngine;

namespace GameScripts.RunTime.Hud
{
    public class HudManager : SingletonBase<HudManager>
    {
        private Dictionary<Transform, List<AsyncHud>> hudInstances;


        public async UniTaskVoid ShowHud<T>(Transform container, params object[] args) where T : AsyncHud
        {
            if (container is null)
            {
                 return;
            }
            
            var hud = await Main.m_Entity.CreateEntity<T>(typeof(T).Name);

            if (!hudInstances.TryGetValue(container, out var hues))
            {
                hues = new List<AsyncHud>();
                hudInstances.Add(container,hues);
            }
            
            //移除重复的Hub
            var hubType = typeof(T);
            var maxIndex = hues.Count - 1;
            for (var i = maxIndex; i >= 0; i--)
            {
                var oldHud = hues[i];
                if (oldHud.GetType() == hubType)
                {
                    RemoveHud(oldHud);
                }
            }
            
            //加入hub集合
            hud.Container = container;
            hud.Entity.transform.SetParent(container);
            hues.Add(hud);
            hud.Fill(args);
        }
        
        
        public void ClearHud(Transform container)
        {
            if (hudInstances.TryGetValue(container, out var hues))
            {
                foreach (var hud in hues)
                {
                    hues.Remove(hud);
                    Main.m_Entity.DestroyEntity(hud);
                }
                hues.Clear();
            }
        }

        internal void RemoveHud(AsyncHud hud)
        {
            if (hudInstances.TryGetValue(hud.Container, out var hues))
            {
                if (hues.Contains(hud))
                {
                    hues.Remove(hud);
                    Main.m_Entity.DestroyEntity(hud);
                }
                else
                {
                    Log.Error($"重复移除:{hud.GetType()}");
                }
            }
        }
        
    }
}