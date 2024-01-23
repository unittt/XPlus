using HT.Framework;
using TMPro;
using UnityEngine;

namespace GameScripts.RunTime.Hud
{
    /// <summary>
    /// 战斗伤害
    /// </summary>
    [EntityResource("WarriorDamageHud")]
    public class WarriorDamageHud : AsyncHud
    {
        private TextMeshPro _numberTMP;
        private GameObject baoji;
        
        public override void OnInit()
        {
            _numberTMP = Entity.GetComponentByChild<TextMeshPro>("number");
            baoji = Entity.FindChildren("baoji");
        }

        public void Fill(int number, bool isCritical)
        {
            _numberTMP.text = number.ConvertNumberToSpriteString();
            baoji.SetActive(isCritical);
        }
    }
}