namespace GameScripts.RunTime.Magic.Command
{
    
    [Command("震屏",81)]
    public class ShakeScreen:CommandBase
    {
        [Argument("时间")]
        public float shake_time;
        
        [Argument("幅度",0.1f)]
        public float shake_dis;
        
        [Argument("频率",1)]
        public float shake_rate;
    }
}