using GameScripts.RunTime.Hud.Attribute;
using GameScripts.RunTime.Magic;
using HT.Framework;
using TMPro;

namespace GameScripts.RunTime.Hud
{
    [HudEntity]
    [EntityResource("")]
    public class WarNameHudEntityLogic : HudEntityLogicBase
    {
        private TextMeshPro _nameTMP;
        
        public override void OnInit()
        {
            _nameTMP = Entity.GetComponentByChild<TextMeshPro>("Name");
        }

        public void Fill(string name)
        {
            _nameTMP.text = name;
        }

        public override BodyPart Part { get; }
    }
}