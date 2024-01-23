using HT.Framework;
using TMPro;

namespace GameScripts.RunTime.Hud
{
    [EntityResource("")]
    public class WarNameHud : AsyncHud
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
    }
}