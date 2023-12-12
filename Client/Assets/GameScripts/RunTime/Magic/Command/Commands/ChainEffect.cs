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

        [Argument("时间",1)] 
        public float cale_time;
        
        [Argument("渐变曲线",Ease.Linear)]
        [SelectHandler(typeof(SelectorHandler_Enum<Ease>))]
        public Ease ease_type;
        
        [Argument("存在时间", 1)]
        public float alive_time;
        
        
        [Argument("重复纹理",true)]
        [SelectHandler(typeof(SelectorHandler_Bool))]
        public bool repeat_texture;
    }
}