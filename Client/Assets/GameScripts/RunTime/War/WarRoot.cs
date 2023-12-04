using HT.Framework;
using UnityEngine;

namespace GameScripts.RunTime.War
{
    [EntityResource("WarRoot")]
    public class WarRoot : EntityLogicBase
    {

        public override void OnInit()
        {
            var camera = Entity.GetComponentByChild<Camera>("WarCamera");
            var warBg = Entity.GetComponentByChild<SpriteRenderer>("WarCamera/WarBg");
            
            var cameraHeight = 2f * camera.orthographicSize;
            var cameraWidth = cameraHeight * camera.aspect;

            warBg.size = new Vector2(cameraWidth, cameraHeight);
        }
    }
}