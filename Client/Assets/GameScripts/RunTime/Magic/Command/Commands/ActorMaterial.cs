using GameScripts.RunTime.Utility.Selector;

namespace GameScripts.RunTime.Magic.Command
{
    [Command("材质球", 93)]
    public class ActorMaterial:CommandBase
    {
        [Argument("执行人")]
        [SelectHandler(typeof(SelectorHandler_Enum<ExecutorType>))]
        public ExecutorType executor;
        
        [Argument("持续时间(?)")]
        public float alive_time;
        
        [Argument("渐变显示时间)")]
        public float ease_show_time;
        
        [Argument("渐变消失时间)")]
        public float ease_hide_time;
        
        [Argument("材质球)")]
        public string mat_path;
    }
}