using DG.Tweening;
using GameScripts.RunTime.Utility.Selector;

namespace GameScripts.RunTime.Magic.Command
{
    
    [Command("链接特效",81)]
    public class ChainEffect:CommandBase
    {
        [Argument("绑定人")]
        [SelectHandler(typeof(SelectorHandler_Enum<ExecutorType>))]
        public ExecutorType executor;
        
        [Argument("特效")]
        public ComplexEffect effect;
        
        [Argument("起始位置")]
        public ComplexPosition begin_pos;
        
        [Argument("终点位置")]
        public ComplexPosition end_pos;

        [Argument("时间")] 
        public float cale_time = 1;

        [Argument("渐变曲线")] [SelectHandler(typeof(SelectorHandler_Enum<Ease>))]
        public Ease ease_type = Ease.Linear;

        [Argument("存在时间")] public float alive_time = 1;


        [Argument("重复纹理")] [SelectHandler(typeof(SelectorHandler_Bool))]
        public bool repeat_texture = true;
    }
}