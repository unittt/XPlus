using System;
using DG.Tweening;
using GameScripts.RunTime.Utility.Selector;

namespace GameScripts.RunTime.Magic.Command
{
    [Serializable]
    [Command("射击特效",80)]
    public class ShootEffect:CommandData
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
        
        [Argument("移动时间")]
        public float move_time;
        
        [Argument("延迟移动")]
        public float delay_time;

        [Argument("渐变曲线")] [SelectHandler(typeof(SelectorHandler_Enum<Ease>))]
        public Ease ease_type = Ease.Linear;

        [Argument("存在时间")] public float alive_time = 1;
    }
}