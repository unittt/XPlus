using System;
using GameScripts.RunTime.Utility.Selector;

namespace GameScripts.RunTime.Magic.Command
{
    [Serializable]
    [Command("定点特效",70)]
    public class StandEffect: CommandBase
    {
        [Argument("绑定人")]
        [SelectHandler(typeof(SelectorHandler_Enum<ExecutorType>))]
        public ExecutorType executor;
        
        [Argument("特效")]
        public ComplexEffect effect;
        
        [Argument("特效位置")]
        public ComplexPosition effect_pos;
        
        
        [Argument("方向(?)")]
        [SelectHandler(typeof(SelectorHandler_Enum<ExecutorDirection>))]
        public ExecutorDirection effect_dir_type;

        [Argument("位置","IsShowComplexPosition")]
        private ComplexPosition relative_dir;
        
        [Argument("存在时间")]
        private float alive_time;


        private bool IsShowComplexPosition()
        {
            return effect_dir_type == ExecutorDirection.Custom;
        }
    }
}