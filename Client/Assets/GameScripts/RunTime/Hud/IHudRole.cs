
using GameScripts.RunTime.Magic;
using UnityEngine;

namespace GameScripts.RunTime.Hud
{
    public interface IHudRole
    {
        public string GetHudName();
        
        /// <summary>
        /// 获得身体节点
        /// </summary>
        /// <param name="bodyPart"></param>
        /// <returns></returns>
        Transform GetBodyNode(BodyPart bodyPart);
    }
}