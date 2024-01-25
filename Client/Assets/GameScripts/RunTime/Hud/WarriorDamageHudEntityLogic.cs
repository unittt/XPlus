using GameScripts.RunTime.Hud.Attribute;
using GameScripts.RunTime.Magic;
using GameScripts.RunTime.Utility.Timer;
using HT.Framework;
using TMPro;
using UnityEngine;

namespace GameScripts.RunTime.Hud
{
    /// <summary>
    /// 战斗伤害
    /// </summary>
    [HudEntityAttribute("WarriorDamageHud",true)]
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
            TimerManager.RegisterTimer(1, Kill);
        }
        

        public override BodyPart Part { get; }


        public override void OnDestroy()
        {
            Container = null;
            base.OnDestroy();
        }
    }
}