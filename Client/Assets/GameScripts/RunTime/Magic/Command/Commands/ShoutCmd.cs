using GameScripts.RunTime.Utility.Selector;

namespace GameScripts.RunTime.Magic.Command
{
    [Command("喊招",1003)]
    public class ShoutCmd
    {
        [Argument("存在时间")]
        public float alive_time;
        
        [Argument("显示文字")]
        [SelectHandler(typeof(EnumSelectorHandler<BoolType>))]
        public BoolType show;
        
        [Argument("播放音效")]
        [SelectHandler(typeof(EnumSelectorHandler<BoolType>))]
        public BoolType shot;
    }
}