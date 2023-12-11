using GameScripts.RunTime.Utility.Selector;

namespace GameScripts.RunTime.Magic.Command
{
    [Command("播放动作",10)]
    public class PlayAction:CommandBase
    {
        [Argument("动作")]
        [SelectHandler(typeof(EnumSelectorHandler<BoolType>))]
        public string action_name;
        
        [Argument("执行人")]
        [SelectHandler(typeof(EnumSelectorHandler<ExecutorType>))]
        public ExecutorType executor;

        [Argument("起始帧(?)")]
        public float start_frame;
        
        [Argument("结束帧(?)")]
        public float end_frame;
        
        [Argument("动作时间(?)")]
        public float action_time;
        
        [Argument("备用动作(?)")]
        [SelectHandler(typeof(EnumSelectorHandler<BoolType>))]
        public string bak_action_name;
    }
}