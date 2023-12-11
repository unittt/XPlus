using GameScripts.RunTime.Utility.Selector;

namespace GameScripts.RunTime.Magic.Command
{
    /// <summary>
    /// 受击信息
    /// </summary>
    [Command("受击信息",1)]
    public class VicHitInfo : CommandBase
    {
        [Argument("掉血间隔")]
        public float hurt_delta;
        
        [Argument("面向攻击者")]
        [SelectHandler(typeof(EnumSelectorHandler<BoolType>))]
        public BoolType face_atk;
        
        [Argument("受击动作")]
        [SelectHandler(typeof(EnumSelectorHandler<BoolType>))]
        public BoolType play_anim;
        
        [Argument("伤害跟随")]
        [SelectHandler(typeof(EnumSelectorHandler<BoolType>))]
        public BoolType damage_follow;
        
        [Argument("考虑高度")]
        [SelectHandler(typeof(EnumSelectorHandler<BoolType>))]
        public BoolType consider_hight;
        
        [Argument("播放音效")]
        [SelectHandler(typeof(EnumSelectorHandler<BoolType>))]
        public BoolType shot;
    }
}