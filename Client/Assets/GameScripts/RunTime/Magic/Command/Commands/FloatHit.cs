using GameScripts.RunTime.Utility.Selector;

namespace GameScripts.RunTime.Magic.Command
{
    
    [Command("浮空", 99)]
    public class FloatHit:CommandBase
    {
        [Argument("执行人")]
        [SelectHandler(typeof(SelectorHandler_Enum<ExecutorType>))]
        public ExecutorType executor;
        
        [Argument("upFloat速度",10)]
        public float up_speed;
        
        [Argument("upFloat时间",0.2f)]
        public float up_time;
        
        [Argument("hitFloat速度",8)]
        public float hit_speed;
        
        [Argument("hitFloat时间",0.2f)]
        public float hit_time;
        
        [Argument("落地时间",1)]
        public float down_time;
        
        [Argument("躺地时间",0.5f)]
        public float lie_time;
    }
}