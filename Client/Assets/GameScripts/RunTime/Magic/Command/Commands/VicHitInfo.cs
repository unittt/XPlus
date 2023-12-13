using System;
using GameScripts.RunTime.Utility.Selector;

namespace GameScripts.RunTime.Magic.Command
{
    /// <summary>
    /// 受击信息
    /// </summary>
    [Serializable]
    [Command("受击信息",1)]
    public class VicHitInfo : CommandBase
    {
        [Argument("掉血间隔")]
        public float hurt_delta;
        
        [Argument("面向攻击者")]
        [SelectHandler(typeof(SelectorHandler_Bool))]
        public bool face_atk;
        
        [Argument("受击动作")]
        [SelectHandler(typeof(SelectorHandler_Bool))]
        public bool play_anim;
        
        [Argument("伤害跟随")]
        [SelectHandler(typeof(SelectorHandler_Bool))]
        public bool damage_follow;
        
        [Argument("考虑高度")]
        [SelectHandler(typeof(SelectorHandler_Bool))]
        public bool consider_hight;
        
        [Argument("播放音效")]
        [SelectHandler(typeof(SelectorHandler_Bool))]
        public bool shot;
    }
}