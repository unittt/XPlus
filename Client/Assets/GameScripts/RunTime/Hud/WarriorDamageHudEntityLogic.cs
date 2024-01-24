using GameScripts.RunTime.Hud.Attribute;
using GameScripts.RunTime.Magic;
using HT.Framework;
using TMPro;
using UnityEngine;

namespace GameScripts.RunTime.Hud
{
    /// <summary>
    /// 战斗伤害
    /// </summary>
    [HudEntity]
    [EntityResource("WarriorDamageHud")]
    public class WarriorDamageHudEntityLogic : HudEntityLogicBase
    {
        private TextMeshPro _numberTMP;
        private GameObject baoji;
        
        public override void OnInit()
        {
            _numberTMP = Entity.GetComponentByChild<TextMeshPro>("number");
            baoji = Entity.FindChildren("baoji");
        }

        public void Show(int number, bool isCritical)
        {
            _numberTMP.text = number.ConvertNumberToSpriteString();
            baoji.SetActive(isCritical);
        }

        public override BodyPart Part { get; }
        
    }
}