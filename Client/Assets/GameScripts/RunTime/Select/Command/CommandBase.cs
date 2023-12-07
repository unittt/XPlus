using GameScripts.RunTime.Select.Attributes;
using UnityEngine;

namespace GameScripts.RunTime.Select.Command
{
    public class CommandBase
    {
        public void xxxx()
        {
            JsonUtility.ToJson(this);
        }
    }

    public class VicHitInfo : CommandBase
    {

        [Argument("掉血间隔")]
        public float hurt_delta;
        [Argument("面向攻击者")]
        public bool face_atk;
        [Argument("受击动作")]
        public bool play_anim;
    }
    
}