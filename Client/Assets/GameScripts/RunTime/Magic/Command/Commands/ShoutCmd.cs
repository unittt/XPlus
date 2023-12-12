using GameScripts.RunTime.Utility.Selector;

namespace GameScripts.RunTime.Magic.Command
{
    [Command("喊招",1003)]
    public class ShoutCmd: CommandBase
    {
        [Argument("存在时间")]
        public float alive_time = 1.5f;
        
        [Argument("显示文字")]
        [SelectHandler(typeof(SelectorHandler_Bool))]
        public bool show;
        
        [Argument("播放音效")]
        [SelectHandler(typeof(SelectorHandler_Bool))]
        public bool shot;
    }
}